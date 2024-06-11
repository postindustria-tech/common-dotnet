using Blobject.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Namotion.Reflection;
using System.Data.Common;
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
                    results.Add(ConstructFromDictionary(d, type));
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

        private static IBlobClientBuilder ConstructFromDictionary(Dictionary<string, string> dict, Type type)
        {
            var constructor = type.GetConstructors().First();
            var parameters = constructor.GetParameters();
            var args = new object?[parameters.Length];

            foreach (var param in parameters)
            {
                if (param.ParameterType.IsGenericType)
                {
                    throw new ArgumentException($"The parameter '{param.Name}' is generic.", param.Name);
                }
                var contextualParameter = param.ToContextualParameter();
                if (dict.TryGetValue(param.Name, out string value))
                {
                    if (value is null && contextualParameter.Nullability == Nullability.NotNullable)
                    {
                        throw new ArgumentNullException($"The non-nullable property '{param.Name}' cannot be null.", param.Name);
                    }
                    args[param.Position] = value;
                }
                else
                {
                    throw new KeyNotFoundException($"The property '{param.Name}' is missing in the dictionary.");
                }
            }

            if (constructor.Invoke(args) is IBlobClientBuilder blobClientBuilder)
            {
                return blobClientBuilder;
            }
            throw new ArgumentException($"Type '{type.FullName}' does not implement '{nameof(IBlobClientBuilder)}'.", nameof(type));
        }
    }
}
