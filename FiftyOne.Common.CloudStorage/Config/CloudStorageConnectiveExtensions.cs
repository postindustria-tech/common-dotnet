namespace FiftyOne.Common.CloudStorage.Config
{
    public static class CloudStorageConnectiveExtensions
    {
        public static T SetConnectionString<T>(this T connective, string connectionString) where T: ICloudStorageConnective
        {
            connective.GetOrMakeConnectionOptions().ConnectionString = connectionString;
            return connective;
        }
        public static T SetContainerName<T>(this T connective, string ContainerName) where T : ICloudStorageConnective
        {
            connective.GetOrMakeConnectionOptions().ContainerName = ContainerName;
            return connective;
        }
        public static T SetS3AccessKey<T>(this T connective, string S3AccessKey) where T : ICloudStorageConnective
        {
            connective.GetOrMakeConnectionOptions().S3AccessKey = S3AccessKey;
            return connective;
        }
        public static T SetS3SecretKey<T>(this T connective, string S3SecretKey) where T : ICloudStorageConnective
        {
            connective.GetOrMakeConnectionOptions().S3SecretKey = S3SecretKey;
            return connective;
        }
        public static T SetS3Region<T>(this T connective, string S3Region) where T : ICloudStorageConnective
        {
            connective.GetOrMakeConnectionOptions().S3Region = S3Region;
            return connective;
        }
        public static T SetS3BucketName<T>(this T connective, string S3BucketName) where T : ICloudStorageConnective
        {
            connective.GetOrMakeConnectionOptions().S3BucketName = S3BucketName;
            return connective;
        }
        public static T SetS3Endpoint<T>(this T connective, string S3Endpoint) where T : ICloudStorageConnective
        {
            connective.GetOrMakeConnectionOptions().S3Endpoint = S3Endpoint;
            return connective;
        }
        public static T SetS3BaseUrl<T>(this T connective, string S3BaseUrl) where T : ICloudStorageConnective
        {
            connective.GetOrMakeConnectionOptions().S3BaseUrl = S3BaseUrl;
            return connective;
        }
        public static T SetS3UseSSL<T>(this T connective, bool S3UseSSL) where T : ICloudStorageConnective
        {
            connective.GetOrMakeConnectionOptions().S3UseSSL = S3UseSSL;
            return connective;
        }
    }
}
