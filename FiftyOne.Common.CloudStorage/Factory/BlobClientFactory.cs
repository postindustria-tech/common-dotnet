using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Namotion.Reflection;
using FiftyOne.Common.CloudStorage.Imps;

namespace FiftyOne.Common.CloudStorage.Factory
{
    public static class BlobClientFactory
    {
        private static IEnumerable<Type> settingsTypes = typeof(BlobClientFactory).Assembly
            .GetTypes()
            .Where(t => !t.IsAbstract && typeof(IBlobClientBuilder).IsAssignableFrom(t))
            .ToList();

        public static IBlobClientBuilder ParseSettings(string packedConnectionString)
        {
            var errors = new List<Exception>();
            var results = new List<Tuple<IBlobClientBuilder, ISet<string>>>();
            var d = new Dictionary<string, string>(
                packedConnectionString.Split(";")
                .Select(s => s.Split("=", 2))
                .Where(x => x.Length == 2)
                .Select(x => new KeyValuePair<string, string>(x[0], x[1])));
            foreach (Type type in settingsTypes)
            {
                try
                {
                    ISet<string> consumedParameters;
                    results.Add(new Tuple<IBlobClientBuilder, ISet<string>>(
                        ConstructFromDictionary<IBlobClientBuilder>(d, type, out consumedParameters),
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

        public static T ConstructFromDictionary<T>(Dictionary<string, string> dict, Type type, out ISet<string> consumedParameters)
        {
            if (!typeof(T).IsAssignableFrom(type))
            {
                throw new ArgumentException($"{typeof(T).FullName} is not assignable from {type.FullName}.", nameof(type));
            }

            var constructors = type.GetConstructors();
            if (constructors.Length == 0)
            {
                throw new ArgumentException($"{typeof(T).FullName} has no accessible constructors.", nameof(type));
            }
            var errors = new List<Exception>();
            foreach (var constructor in constructors)
            {
                object result;
                try
                {
                    result = ConstructFromDictionary(dict, constructor, out consumedParameters);
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                    continue;
                }
                if (result is T t)
                {
                    return t;
                }
                throw new ArgumentException($"Result type '{result.GetType().FullName}' does not implement '{typeof(T).Name}'.", typeof(T).FullName);
            }
            throw new AggregateException($"Failed to find suitable constructor for '{type.FullName}'.", errors);
        }

        private static object ConstructFromDictionary(Dictionary<string, string> dict, ConstructorInfo constructor, out ISet<string> consumedParameters) {
            var parameters = constructor.GetParameters();
            var args = new object?[parameters.Length];
            var sinks = new HashSet<int>();
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
                    sinks.Add(param.Position);
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

            switch (sinks.Count)
            {
                case 0:
                    consumedParameters = usedUpParameters;
                    foreach (var key in dict.Keys
                        .Where(k => !usedUpParameters.Contains(k))
                        .Where(k => constructor.ReflectedType.GetProperty(k) is null))
                    {
                        errors.Add(new ArgumentException($"Unused argument: {key}", key));
                    }
                    break;
                case 1:
                    consumedParameters = new HashSet<string>(dict.Keys);
                    args[sinks.First()] = string.Join(";", dict
                        .Where(kvp => !usedUpParameters.Contains(kvp.Key))
                        .Select(kvp => $"{kvp.Key}={kvp.Value}"));
                    break;
                default:
                    consumedParameters = new HashSet<string>();
                    var sinkDescriptions = string.Join(", ", sinks.Select(j => $"({j}: {parameters[j].Name})"));
                    errors.Add(new ArgumentException($"Multiple occurrences of {nameof(UnusedParametersSinkAttribute)} found: {sinkDescriptions}", constructor.ToString()));
                    break;
            }

            if (errors.Count > 0)
            {
                throw new AggregateException($"Failed to invoke {constructor}.", errors);
            }

            return constructor.Invoke(args);
        }
    }
}
