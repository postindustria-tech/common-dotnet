using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Namotion.Reflection;

namespace FiftyOne.Common.CloudStorage.Factory
{
    public static class BlobClientFactory
    {
        public static IBlobClientBuilder ParseSettings(string packedConnectionString)
        {
            var errors = new List<Exception>();
            var results = new List<Tuple<IBlobClientBuilder, ISet<string>>>();
            var d = new Dictionary<string, string>(
                packedConnectionString.Split(";")
                .Select(s => s.Split("=", 2))
                .Where(x => x.Length == 2)
                .Select(x => new KeyValuePair<string, string>(x[0], x[1])));
            foreach (var constructor in GetApplicableConstructors(d.Keys, errors))
            {
                try
                {
                    ISet<string> consumedParameters;
                    results.Add(new Tuple<IBlobClientBuilder, ISet<string>>(
                        (IBlobClientBuilder)ConstructFromDictionary(d, constructor, out consumedParameters),
                        consumedParameters));
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }
            switch (results.Count)
            {
                case 1:
                    return results[0].Item1;
                case 0:
                    throw new AggregateException($"Failed to process {nameof(packedConnectionString)}", errors);
                default:
                    break;
            }
            var usedUpIntersection = results.SelectMany(x => x.Item2).Distinct().ToList();
            var bestMatches = results.Where(x => x.Item2.SetEquals(usedUpIntersection)).ToList();
            if (bestMatches.Count == 1)
            {
                return bestMatches[0].Item1;
            }
            // multiple matches using same key sets
            // or
            // multiple matches using different key subsets
            string availableResultTypes = string.Join(", ", results.Select(x => x.GetType()));
            throw new ArgumentException(
                $"Ambiguous {nameof(packedConnectionString)} -- properties provided for: {availableResultTypes}",
                packedConnectionString);
        }

        private static IEnumerable<ConstructorInfo> GetAllBuilderConstructors()
        {
            var allTypes = typeof(BlobClientFactory).Assembly
                .GetTypes()
                .Where(t => !t.IsAbstract && typeof(IBlobClientBuilder).IsAssignableFrom(t));
            foreach (var nextType in allTypes)
            {
                var constructors = nextType.GetConstructors();
#if DEBUG
                if (constructors.Length == 0)
                {
                    throw new InvalidOperationException($"Type {nextType.FullName} has no accessible constructors.");
                }
#endif
                foreach (var constructor in constructors)
                {
#if DEBUG
                    var nonStringParams = constructor.GetParameters()
                        .Where(p => p.ParameterType != typeof(string))
                        .Select(p => p.Name)
                        .ToList();
                    if (nonStringParams.Count > 0)
                    {
                        string nonStringParamNames = string.Join(", ", nonStringParams);
                        throw new InvalidOperationException($"Constructor {constructor} has non-string parameters: {nonStringParamNames}.");
                    }
                    var sinks = constructor.GetParameters()
                        .Where(p => p.IsDefined(typeof(UnusedParametersSinkAttribute), false))
                        .Select(p => p.Name)
                        .ToList();
                    if (sinks.Count > 1)
                    {
                        string sinkParamNames = string.Join(", ", sinks);
                        throw new InvalidOperationException($"Constructor {constructor} has multiple parameters attributed with {nameof(UnusedParametersSinkAttribute)}: {sinkParamNames}.");
                    }
#endif
                    yield return constructor;
                }
            }
        }

        private static IEnumerable<ConstructorInfo> GetApplicableConstructors(
            IReadOnlyCollection<string> keys,
            ICollection<Exception> errors)
        {
            var allConstructors = GetAllBuilderConstructors();
            var failures = allConstructors
                .Select(c => c.ReflectedType)
                .Distinct()
                .SelectMany(t =>
                {
                    var forbiddenKeys = keys.Where(k => t.GetProperty(k) is null).ToList();
                    return forbiddenKeys.Count > 0
                        ? new[] { new Tuple<Type, IEnumerable<string>>(t, forbiddenKeys) }
                        : Enumerable.Empty<Tuple<Type, IEnumerable<string>>>();
                })
                .ToList();
            foreach (var nextFailure in failures)
            {
                string unusedKeys = string.Join(", ", nextFailure.Item2);
                errors.Add(new ArgumentException($"Type {nextFailure.Item1.FullName} does not support: {unusedKeys}.", unusedKeys));
            }
            var failedTypes = failures.Select(p => p.Item1);
            return allConstructors.Where(c => !failedTypes.Contains(c.ReflectedType));
        }

        private static object ConstructFromDictionary(Dictionary<string, string> dict, ConstructorInfo constructor, out ISet<string> consumedParameters) {
            var parameters = constructor.GetParameters();
            var args = new object?[parameters.Length];
            int? sinkPosition = null;
            var usedUpParameters = new HashSet<string>();
            var errors = new List<Exception>();

            foreach (var param in parameters)
            {
                if (param.ParameterType.IsGenericType)
                {
                    errors.Add(new ArgumentException($"The parameter '{param.Name}' is generic.", param.Name));
                    continue;
                }
                if (param.GetCustomAttribute<UnusedParametersSinkAttribute>() != null)
                {
                    sinkPosition = param.Position;
                    continue;
                }
                var contextualParameter = param.ToContextualParameter();
                if (dict.TryGetValue(param.Name, out string value))
                {
                    args[param.Position] = value;
                    usedUpParameters.Add(param.Name);
                    continue;
                }
                if (contextualParameter.Nullability == Nullability.NotNullable)
                {
                    errors.Add(new ArgumentNullException(param.Name, $"The non-nullable property '{param.Name}' cannot be null."));
                    continue;
                }
            }

            if (sinkPosition.HasValue)
            {
                consumedParameters = new HashSet<string>(dict.Keys);
                args[sinkPosition.Value] = string.Join(";", dict
                    .Where(kvp => !usedUpParameters.Contains(kvp.Key))
                    .Select(kvp => $"{kvp.Key}={kvp.Value}"));
            } 
            else
            {
                consumedParameters = usedUpParameters;
                foreach (var key in dict.Keys
                    .Where(k => !usedUpParameters.Contains(k))
                    .Where(k => constructor.ReflectedType.GetProperty(k) is null))
                {
                    errors.Add(new ArgumentException($"Unused argument: {key}", key));
                }
            }

            if (errors.Count > 0)
            {
                throw new AggregateException($"Failed to invoke {constructor}.", errors);
            }

            return constructor.Invoke(args);
        }
    }
}
