namespace FiftyOne.Common.CloudStorage.Config
{
    /// <summary>
    /// Extends <see cref="ICloudStorageConnective"/>
    /// to expose all <see cref="CloudStorageConnectionOptions"/> properties
    /// as builder methods.
    /// </summary>
    public static class CloudStorageConnectiveExtensions
    {
        /// <summary>
        /// Extension builder method for conveniently setting underlying
        /// <see cref="CloudStorageConnectionOptions.ConnectionString"/>
        /// </summary>
        /// <typeparam name="T">Type implemeting <see cref="ICloudStorageConnective"/></typeparam>
        /// <param name="connective">The builder implemeting <see cref="ICloudStorageConnective"/></param>
        /// <param name="ConnectionString">See <see cref="CloudStorageConnectionOptions.ConnectionString"/></param>
        /// <returns>The initial builder.</returns>
        public static T SetConnectionString<T>(this T connective, string ConnectionString) where T: ICloudStorageConnective
        {
            connective.GetOrMakeConnectionOptions().ConnectionString = ConnectionString;
            return connective;
        }

        /// <summary>
        /// Extension builder method for conveniently setting underlying
        /// <see cref="CloudStorageConnectionOptions.ContainerName"/>
        /// </summary>
        /// <typeparam name="T">Type implemeting <see cref="ICloudStorageConnective"/></typeparam>
        /// <param name="connective">The builder implemeting <see cref="ICloudStorageConnective"/></param>
        /// <param name="ContainerName">See <see cref="CloudStorageConnectionOptions.ContainerName"/></param>
        /// <returns>The initial builder.</returns>
        public static T SetContainerName<T>(this T connective, string ContainerName) where T : ICloudStorageConnective
        {
            connective.GetOrMakeConnectionOptions().ContainerName = ContainerName;
            return connective;
        }

        /// <summary>
        /// Extension builder method for conveniently setting underlying
        /// <see cref="CloudStorageConnectionOptions.S3AccessKey"/>
        /// </summary>
        /// <typeparam name="T">Type implemeting <see cref="ICloudStorageConnective"/></typeparam>
        /// <param name="connective">The builder implemeting <see cref="ICloudStorageConnective"/></param>
        /// <param name="S3AccessKey">See <see cref="CloudStorageConnectionOptions.S3AccessKey"/></param>
        /// <returns>The initial builder.</returns>
        public static T SetS3AccessKey<T>(this T connective, string S3AccessKey) where T : ICloudStorageConnective
        {
            connective.GetOrMakeConnectionOptions().S3AccessKey = S3AccessKey;
            return connective;
        }

        /// <summary>
        /// Extension builder method for conveniently setting underlying
        /// <see cref="CloudStorageConnectionOptions.S3SecretKey"/>
        /// </summary>
        /// <typeparam name="T">Type implemeting <see cref="ICloudStorageConnective"/></typeparam>
        /// <param name="connective">The builder implemeting <see cref="ICloudStorageConnective"/></param>
        /// <param name="S3SecretKey">See <see cref="CloudStorageConnectionOptions.S3SecretKey"/></param>
        /// <returns>The initial builder.</returns>
        public static T SetS3SecretKey<T>(this T connective, string S3SecretKey) where T : ICloudStorageConnective
        {
            connective.GetOrMakeConnectionOptions().S3SecretKey = S3SecretKey;
            return connective;
        }

        /// <summary>
        /// Extension builder method for conveniently setting underlying
        /// <see cref="CloudStorageConnectionOptions.S3Region"/>
        /// </summary>
        /// <typeparam name="T">Type implemeting <see cref="ICloudStorageConnective"/></typeparam>
        /// <param name="connective">The builder implemeting <see cref="ICloudStorageConnective"/></param>
        /// <param name="S3Region">See <see cref="CloudStorageConnectionOptions.S3Region"/></param>
        /// <returns>The initial builder.</returns>
        public static T SetS3Region<T>(this T connective, string S3Region) where T : ICloudStorageConnective
        {
            connective.GetOrMakeConnectionOptions().S3Region = S3Region;
            return connective;
        }

        /// <summary>
        /// Extension builder method for conveniently setting underlying
        /// <see cref="CloudStorageConnectionOptions.S3BucketName"/>
        /// </summary>
        /// <typeparam name="T">Type implemeting <see cref="ICloudStorageConnective"/></typeparam>
        /// <param name="connective">The builder implemeting <see cref="ICloudStorageConnective"/></param>
        /// <param name="S3BucketName">See <see cref="CloudStorageConnectionOptions.S3BucketName"/></param>
        /// <returns>The initial builder.</returns>
        public static T SetS3BucketName<T>(this T connective, string S3BucketName) where T : ICloudStorageConnective
        {
            connective.GetOrMakeConnectionOptions().S3BucketName = S3BucketName;
            return connective;
        }

        /// <summary>
        /// Extension builder method for conveniently setting underlying
        /// <see cref="CloudStorageConnectionOptions.S3Endpoint"/>
        /// </summary>
        /// <typeparam name="T">Type implemeting <see cref="ICloudStorageConnective"/></typeparam>
        /// <param name="connective">The builder implemeting <see cref="ICloudStorageConnective"/></param>
        /// <param name="S3Endpoint">See <see cref="CloudStorageConnectionOptions.S3Endpoint"/></param>
        /// <returns>The initial builder.</returns>
        public static T SetS3Endpoint<T>(this T connective, string S3Endpoint) where T : ICloudStorageConnective
        {
            connective.GetOrMakeConnectionOptions().S3Endpoint = S3Endpoint;
            return connective;
        }

        /// <summary>
        /// Extension builder method for conveniently setting underlying
        /// <see cref="CloudStorageConnectionOptions.S3BaseUrl"/>
        /// </summary>
        /// <typeparam name="T">Type implemeting <see cref="ICloudStorageConnective"/></typeparam>
        /// <param name="connective">The builder implemeting <see cref="ICloudStorageConnective"/></param>
        /// <param name="S3BaseUrl">See <see cref="CloudStorageConnectionOptions.S3BaseUrl"/></param>
        /// <returns>The initial builder.</returns>
        public static T SetS3BaseUrl<T>(this T connective, string S3BaseUrl) where T : ICloudStorageConnective
        {
            connective.GetOrMakeConnectionOptions().S3BaseUrl = S3BaseUrl;
            return connective;
        }

        /// <summary>
        /// Extension builder method for conveniently setting underlying
        /// <see cref="CloudStorageConnectionOptions.S3UseSSL"/>
        /// </summary>
        /// <typeparam name="T">Type implemeting <see cref="ICloudStorageConnective"/></typeparam>
        /// <param name="connective">The builder implemeting <see cref="ICloudStorageConnective"/></param>
        /// <param name="S3UseSSL">See <see cref="CloudStorageConnectionOptions.S3UseSSL"/></param>
        /// <returns>The initial builder.</returns>
        public static T SetS3UseSSL<T>(this T connective, bool S3UseSSL) where T : ICloudStorageConnective
        {
            connective.GetOrMakeConnectionOptions().S3UseSSL = S3UseSSL;
            return connective;
        }
    }
}
