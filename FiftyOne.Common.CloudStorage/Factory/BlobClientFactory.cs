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
        private static Type[] settingsTypes =
        {
            typeof(AzureStorageSettings),
            typeof(S3StorageSettings),
        };

        public static IBlobClientBuilder ParseSettings(string packedConnectionString)
        {
            var errors = new List<Exception>();
            var results = new List<IBlobClientBuilder>();
            var d = new Dictionary<string, string>(
                packedConnectionString.Split(";")
                .Select(s => s.Split("=", 2))
                .Where(x => x.Length == 2)
                .Select(x => new KeyValuePair<string, string>(x[0], x[1])));
            foreach (Type type in settingsTypes)
            {
                try
                {
                    results.Add(ConstructFromDictionary<IBlobClientBuilder>(d, type));
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }
            switch (results.Count)
            {
                case 1:
                    return results[0];
                case 0:
                    throw new AggregateException($"Failed to process {nameof(packedConnectionString)}", errors);
                default:
                    string availableResultTypes = string.Join(", ", results.Select(x => x.GetType()));
                    throw new ArgumentException(
                        $"Ambiguous {nameof(packedConnectionString)} -- properties provided for: {availableResultTypes}",
                        packedConnectionString);
            }
        }

        public static T ConstructFromDictionary<T>(Dictionary<string, string> dict, Type type)
        {
            if (!typeof(T).IsAssignableFrom(type))
            {
                throw new ArgumentException($"{typeof(T).FullName} is not assignable from {type.FullName}.", nameof(type));
            }

            var constructor = type.GetConstructors().First();
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
                    if (value is null && contextualParameter.Nullability == Nullability.NotNullable)
                    {
                        errors.Add(new ArgumentNullException($"The non-nullable property '{param.Name}' cannot be null.", param.Name));
                        continue;
                    }
                    args[param.Position] = value;
                }
                else
                {
                    errors.Add(new KeyNotFoundException($"The property '{param.Name}' is missing in the dictionary."));
                }
            }

            switch (sinks.Count)
            {
                case 0:
                    break;
                case 1:
                    args[sinks.First()] = string.Join(", ", dict
                        .Where(kvp => !usedUpParameters.Contains(kvp.Key))
                        .Select(kvp => $"{kvp.Key}={kvp.Value}"));
                    break;
                default:
                    var sinkDescriptions = string.Join(", ", sinks.Select(j => $"({j}: {parameters[j].Name})"));
                    errors.Add(new ArgumentException($"Multiple occurrences of {nameof(UnusedParametersSinkAttribute)} found: {sinkDescriptions}", type.FullName));
                    break;
            }

            if (errors.Count > 0)
            {
                throw new AggregateException($"Failed to construct an instance of {type.Name}", errors);
            }

            var result = constructor.Invoke(args);
            if (result is T t)
            {
                return t;
            }
            throw new ArgumentException($"Result type '{result.GetType().FullName}' does not implement '{typeof(T).Name}'.", typeof(T).FullName);
        }
    }
}
