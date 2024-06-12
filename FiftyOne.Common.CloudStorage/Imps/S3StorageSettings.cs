using Blobject.AmazonS3;
using FiftyOne.Common.CloudStorage.Concepts;
using FiftyOne.Common.CloudStorage.Factory;
using FiftyOne.Common.CloudStorage.Imps.Adapters;

namespace FiftyOne.Common.CloudStorage.Imps
{
    public class S3StorageSettings: IBlobClientBuilder
    {
        public string S3AccessKey { get; private set; }
        public string S3SecretKey { get; private set; }
        public string S3Region { get; private set; }
        public string S3BucketName { get; private set; }

        public S3StorageSettings(string S3AccessKey, string S3SecretKey, string S3Region, string S3BucketName)
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
                        new AwsSettings(S3AccessKey, S3SecretKey, S3Region, S3BucketName)
                    )
                );
        }
    }
}
