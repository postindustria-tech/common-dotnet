using FiftyOne.Common.CloudStorage.Factory;
using FiftyOne.Common.CloudStorage.Imps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FiftyOne.Common.CloudStorage.Config
{
    /// <summary>
    /// Config subsection with all the properties
    /// potentially apllicable to any (at least one)
    /// of the actual <see cref="IBlobClientBuilder"/>
    /// implementations.
    /// </summary>
    public class CloudStorageConnectionOptions
    {
        /// <summary>
        /// Returns packed Settings (all set values).
        /// Semicolon-separated array of equals-separated key-value pairs.
        /// </summary>
        public string PackedConnectionString => string.Join(";", EnumerateOptionFragments());


        /// <summary>
        /// Azure connection string.
        /// Or any set of options in the same format.
        /// Semicolon-separated array of equals-separated key-value pairs.
        /// </summary>
        public string? ConnectionString { get; set; }


        /// <summary>
        /// Azure container name.
        /// </summary>
        [ForwardedTo(
            typeof(AzureStorageSettings))]
        public string? ContainerName { get; set; }

        
        /// <summary>
        /// S3 Access Key.
        /// </summary>
        [ForwardedTo(
            typeof(S3StorageSettings),
            typeof(S3CompatibleStorageSettings))]
        public string? S3AccessKey { get; set; }

        /// <summary>
        /// S3 Secret Key.
        /// </summary>
        [ForwardedTo(
            typeof(S3StorageSettings),
            typeof(S3CompatibleStorageSettings))]
        public string? S3SecretKey { get; set; }

        /// <summary>
        /// S3 Region.
        /// </summary>
        [ForwardedTo(
            typeof(S3StorageSettings),
            typeof(S3CompatibleStorageSettings))]
        public string? S3Region { get; set; }

        /// <summary>
        /// S3 Bucket Name.
        /// </summary>
        [ForwardedTo(
            typeof(S3StorageSettings),
            typeof(S3CompatibleStorageSettings))]
        public string? S3BucketName { get; set; }
        
        /// <summary>
        /// S3-compatible storage endpoint.
        /// </summary>
        [ForwardedTo(
            typeof(S3CompatibleStorageSettings))]
        public string? S3Endpoint { get; set; }

        /// <summary>
        /// Base URL for S3-compatible storage.
        /// </summary>
        [ForwardedTo(
            typeof(S3CompatibleStorageSettings))]
        public string? S3BaseUrl { get; set; }
        
        /// <summary>
        /// Whether SSL should be used when interacting with S3/-compatible storage.
        /// </summary>
        [ForwardedTo(
            typeof(S3StorageSettings),
            typeof(S3CompatibleStorageSettings))]
        public bool? S3UseSSL { get; set; }

        #region Private Helpers

        /// <summary>
        /// Iterates over all writable properties.
        /// </summary>
        /// <returns>
        /// Equals-separated key-value pair for each non-null writable property.
        /// </returns>
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
                    yield return $"{property.Name}={value}";
                }
                else
                {
                    yield return value.ToString();
                }
            }
        }

        #endregion
    }
}
