using Amazon.S3;
using FiftyOne.Common.CloudStorage.Concepts;
using FiftyOne.Common.CloudStorage.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiftyOne.Common.CloudStorage.Imps
{
    public class S3StorageSettings: IBlobClientBuilder
    {
        public readonly string S3AccessKey;
        public readonly string S3SecretKey;
        public readonly string S3Region;
        public readonly string S3BucketName;

        public S3StorageSettings(string S3AccessKey, string S3SecretKey, string S3Region, string S3BucketName)
        {
            this.S3AccessKey = S3AccessKey;
            this.S3SecretKey = S3SecretKey;
            this.S3Region = S3Region;
            this.S3BucketName = S3BucketName;
        }

        public IBlobClient Build()
        {
            return null;
        }
    }
}
