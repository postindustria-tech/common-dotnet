using Blobject.AmazonS3;
using FiftyOne.Common.CloudStorage.Concepts;
using FiftyOne.Common.CloudStorage.Factory;
using FiftyOne.Common.CloudStorage.Imps.Adapters;
using System;

namespace FiftyOne.Common.CloudStorage.Imps
{
    /// <summary>
    /// Connection settings to AWS S3 blob storage.
    /// Backed by <see cref="AwsSettings"/>.
    /// </summary>
    public class S3StorageSettings : IBlobClientBuilder
    {
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
        public string S3Region { get; private set; }

        /// <summary>
        /// See <see cref="AwsSettings.Bucket"/>
        /// </summary>
        public string S3BucketName { get; private set; }

        /// <summary>
        /// See <see cref="AwsSettings.Ssl"/>
        /// </summary>
        public bool? S3UseSSL { get; private set; }

        /// <summary>
        /// Primary constructor for
        /// connection settings to AWS S3 blob storage.
        /// Backed by <see cref="AwsSettings"/>.
        /// </summary>
        /// <param name="S3Endpoint">See <see cref="AwsSettings.Endpoint"/></param>
        /// <param name="S3UseSSL">See <see cref="AwsSettings.Ssl"/></param>
        /// <param name="S3AccessKey">See <see cref="AwsSettings.AccessKey"/></param>
        /// <param name="S3SecretKey">See <see cref="AwsSettings.SecretKey"/></param>
        /// <param name="S3Region">See <see cref="AwsSettings.Region"/></param>
        /// <param name="S3BucketName">See <see cref="AwsSettings.Bucket"/></param>
        /// <param name="S3BaseUrl">See <see cref="AwsSettings.BaseUrl"/></param>
        public S3StorageSettings(
            string S3AccessKey,
            string S3SecretKey,
            string S3Region,
            string S3BucketName,
            string? S3UseSSL)
        {
            this.S3AccessKey = S3AccessKey;
            this.S3SecretKey = S3SecretKey;
            this.S3Region = S3Region;
            this.S3BucketName = S3BucketName;
            if (S3UseSSL != null)
            {
                this.S3UseSSL = bool.Parse(S3UseSSL);
            }
        }

        /// <summary>
        /// Uses properties to build the client.
        /// </summary>
        /// <returns>See <see cref="IBlobClient"/>.</returns>
        public IBlobClient Build()
            => new BlobClientAdapter(
                new AmazonS3BlobClient(
                    S3UseSSL.HasValue
                    ? new AwsSettings(
                        S3AccessKey,
                        S3SecretKey,
                        S3Region,
                        S3BucketName,
                        S3UseSSL.Value)
                    : new AwsSettings(
                        S3AccessKey,
                        S3SecretKey,
                        S3Region,
                        S3BucketName)
                    ),
                false,
                S3AccessKey,
                S3BucketName
            );
    }
}
