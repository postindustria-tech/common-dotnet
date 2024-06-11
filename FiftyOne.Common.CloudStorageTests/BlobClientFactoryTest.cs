using FiftyOne.Common.CloudStorage.Factory;
using FiftyOne.Common.CloudStorage.Imps;

namespace FiftyOne.Common.CloudStorageTests
{
    public class BlobClientFactoryTest
    {
        [Test]
        [TestCase(typeof(AzureStorageSettings), "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1")]
        [TestCase(typeof(S3StorageSettings), "S3AccessKey=alpha;S3SecretKey=omega;S3Region=atlantis;S3BucketName=pufferfish")]
        public void TestUnpackingType(Type expectedResultType, string packedConnectionString)
        {
            var builder = BlobClientFactory.ParseSettings(packedConnectionString);
            Assert.IsInstanceOf(expectedResultType, builder);
        }
    }
}