using Blobject.AmazonS3;
using FiftyOne.Common.CloudStorage.Concepts;
using FiftyOne.Common.CloudStorage.Factory;
using FiftyOne.Common.CloudStorage.Imps.Adapters;
using System;

namespace FiftyOne.Common.CloudStorage.Imps
{
    /// <summary>
    /// Connection settings to S3-compatible blob storage.
    /// Backed by <see cref="AwsSettings"/>.
    /// </summary>
    public class S3CompatibleStorageSettings : IBlobClientBuilder
    {
        /// <summary>
        /// See <see cref="AwsSettings.Endpoint"/>
        /// </summary>
        public string S3Endpoint { get; private set; }
        /// <summary>
        /// See <see cref="AwsSettings.Ssl"/>
        /// </summary>
        public bool S3UseSSL { get; private set; }
        /// <summary>
        /// See <see cref="AwsSettings.AccessKey"/>
        /// </summary>
        public string S3AccessKey { get; private set; }
        /// <summary>
        /// See <see cref="AwsSettings.SecretKey"/>
        /// </summary>
        public string S3SecretKey { get; private set; }
        /// <summary>
        /// See <see cref="AwsSettings.Region"/>
        /// </summary>
        public string? S3Region { get; private set; }
        /// <summary>
        /// See <see cref="AwsSettings.Bucket"/>
        /// </summary>
        public string S3BucketName { get; private set; }
        /// <summary>
        /// See <see cref="AwsSettings.BaseUrl"/>
        /// </summary>
        public string S3BaseUrl { get; private set; }

        /// <summary>
        /// Primary constructor for
        /// connection settings to S3-compatible blob storage.
        /// Backed by <see cref="AwsSettings"/>.
        /// </summary>
        /// <param name="S3Endpoint">See <see cref="AwsSettings.Endpoint"/></param>
        /// <param name="S3UseSSL">See <see cref="AwsSettings.Ssl"/></param>
        /// <param name="S3AccessKey">See <see cref="AwsSettings.AccessKey"/></param>
        /// <param name="S3SecretKey">See <see cref="AwsSettings.SecretKey"/></param>
        /// <param name="S3Region">See <see cref="AwsSettings.Region"/></param>
        /// <param name="S3BucketName">See <see cref="AwsSettings.Bucket"/></param>
        /// <param name="S3BaseUrl">See <see cref="AwsSettings.BaseUrl"/></param>
        public S3CompatibleStorageSettings(
            string S3Endpoint,
            string S3UseSSL,
            string S3AccessKey,
            string S3SecretKey,
            string? S3Region,
            string S3BucketName,
            string S3BaseUrl)
        {
            this.S3Endpoint = S3Endpoint;
            this.S3UseSSL = bool.Parse(S3UseSSL);
            this.S3AccessKey = S3AccessKey;
            this.S3SecretKey = S3SecretKey;
            this.S3Region = S3Region;
            this.S3BucketName = S3BucketName;
            this.S3BaseUrl = S3BaseUrl;
        }

        /// <summary>
        /// Uses properties to build the client.
        /// </summary>
        /// <returns>See <see cref="IBlobClient"/>.</returns>
        public IBlobClient Build()
            => new BlobClientAdapter(
                new AmazonS3BlobClient(
                    new AwsSettings(
                        S3Endpoint,
                        S3UseSSL,
                        S3AccessKey,
                        S3SecretKey,
                        S3Region,
                        S3BucketName,
                        S3BaseUrl
                    )
                ),
                false,
                S3AccessKey,
                S3BucketName
            );
    }
}
