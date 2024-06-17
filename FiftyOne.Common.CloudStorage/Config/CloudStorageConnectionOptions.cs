using FiftyOne.Common.CloudStorage.Factory;
using FiftyOne.Common.CloudStorage.Imps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FiftyOne.Common.CloudStorage.Config
{
    public class CloudStorageConnectionOptions
    {
        // Packed Settings (all set values)

        public string PackedConnectionString => string.Join(";", EnumerateOptionFragments());

        // Actual Settings

        public string? ConnectionString { get; set; }

        [ForwardedTo(typeof(AzureStorageSettings))] public string? ContainerName { get; set; }

        
        [ForwardedTo(typeof(S3StorageSettings))] public string? S3AccessKey { get; set; }
        [ForwardedTo(typeof(S3StorageSettings))] public string? S3SecretKey { get; set; }
        [ForwardedTo(typeof(S3StorageSettings))] public string? S3Region { get; set; }
        [ForwardedTo(typeof(S3StorageSettings))] public string? S3BucketName { get; set; }
        [ForwardedTo(typeof(S3StorageSettings))] public string? S3Endpoint { get; set; }
        [ForwardedTo(typeof(S3StorageSettings))] public string? S3BaseUrl { get; set; }
        [ForwardedTo(typeof(S3StorageSettings))] public bool? S3UseSSL { get; set; }

        #region Private Helpers

        private IEnumerable<string> EnumerateOptionFragments()
        {
            foreach (var property in GetType().GetProperties().Where(p => p.CanWrite))
            {
                var value = property.GetValue(this);
                if (value is null)
                {
                    continue;
                }
                if (property.GetCustomAttribute<ForwardedToAttribute>() is ForwardedToAttribute fwdTo) {
#if DEBUG
                    var backedProp = fwdTo.BuilderType.GetProperty(property.Name);
                    if (backedProp is null)
                    {
                        throw new InvalidOperationException(
                            $"Property {property.Name} isn't actually backed by {fwdTo.BuilderType.FullName}.");
                    }
#endif
                    yield return $"{property.Name}={value}";
                }
                else
                {
                    yield return value.ToString();
                }
            }
        }

        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
        private class ForwardedToAttribute: Attribute
        {
            public Type BuilderType { get; private set; }

            public ForwardedToAttribute(Type builderType) 
            {
                if (!typeof(IBlobClientBuilder).IsAssignableFrom(builderType))
                {
                    throw new ArgumentException($"Type {builderType.FullName} does not implement {typeof(IBlobClientBuilder).Name}", nameof(builderType));
                }
                BuilderType = builderType;
            }
        }

        #endregion
    }
}
