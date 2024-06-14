using Blobject.AmazonS3;
using FiftyOne.Common.CloudStorage.Concepts;
using FiftyOne.Common.CloudStorage.Factory;
using FiftyOne.Common.CloudStorage.Imps.Adapters;
using System;

namespace FiftyOne.Common.CloudStorage.Imps
{
    public class S3StorageSettings: IBlobClientBuilder
    {
        public string S3AccessKey { get; private set; }
        public string S3SecretKey { get; private set; }
        public string? S3Region { get; private set; }
        public string S3BucketName { get; private set; }
        public string? S3Endpoint { get; private set; }
        public string? S3BaseUrl { get; private set; }
        public string? S3UseSSL { get; private set; }

        public S3StorageSettings(
            string S3AccessKey,
            string S3SecretKey,
            string S3BucketName,
            string S3Endpoint,
            string S3BaseUrl,
            string? S3Region,
            string S3UseSSL)
        {
            this.S3AccessKey = S3AccessKey;
            this.S3SecretKey = S3SecretKey;
            this.S3BucketName = S3BucketName;
            this.S3Endpoint = S3Endpoint;
            this.S3BaseUrl = S3BaseUrl;
            this.S3Region = S3Region;
            this.S3UseSSL = S3UseSSL;
        }

        public S3StorageSettings(
            string S3AccessKey,
            string S3SecretKey,
            string S3Region,
            string S3BucketName)
        {
            this.S3AccessKey = S3AccessKey;
            this.S3SecretKey = S3SecretKey;
            this.S3Region = S3Region;
            this.S3BucketName = S3BucketName;
        }

        public IBlobClient Build()
        {
            return
                new BlobClientAdapter(
                    new AmazonS3BlobClient(
                        BuildSettings()
                    )
                );
        }

        private AwsSettings BuildSettings()
        {
            bool? useSsl = null;
            if (S3UseSSL != null)
            {
                if (bool.TryParse(S3UseSSL, out var doUseSsl))
                {
                    useSsl = doUseSsl;
                }
                else
                {
                    throw new ArgumentException($"Failed to parse into boolean {nameof(S3UseSSL)} value: '{S3UseSSL}'.", nameof(S3UseSSL));
                }
            }
            if (S3Endpoint != null)
            {
                return new AwsSettings(
                    S3Endpoint,
                    useSsl.GetValueOrDefault(),
                    S3AccessKey,
                    S3SecretKey,
                    S3Region,
                    S3BucketName,
                    S3BaseUrl);
            }
            return useSsl.HasValue
                ? new AwsSettings(S3AccessKey, S3SecretKey, S3Region, S3BucketName, useSsl.Value)
                : new AwsSettings(S3AccessKey, S3SecretKey, S3Region, S3BucketName);
        }
    }
}
