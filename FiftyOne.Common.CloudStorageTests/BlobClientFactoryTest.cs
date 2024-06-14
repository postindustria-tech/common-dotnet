using FiftyOne.Common.CloudStorage.Factory;
using FiftyOne.Common.CloudStorage.Imps;

namespace FiftyOne.Common.CloudStorageTests
{
    public class BlobClientFactoryTest
    {
        [Test]
        [TestCase(typeof(AzureStorageSettings), "ContainerName=data;DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1")]
        [TestCase(typeof(AzureStorageSettings), "ContainerName=data;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1")]
        [TestCase(typeof(AzureStorageSettings), "ContainerName=data;DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;EndpointSuffix=core.chinacloudapi.cn")]
        [TestCase(typeof(S3StorageSettings), "S3AccessKey=alpha;S3SecretKey=omega;S3Region=atlantis;S3BucketName=pufferfish")]
        [TestCase(typeof(S3StorageSettings), ";S3AccessKey=alpha;S3SecretKey=omega;S3Region=atlantis;S3BucketName=pufferfish;")]
        public void TestUnpackingType(Type expectedResultType, string packedConnectionString)
        {
            var builder = BlobClientFactory.ParseSettings(packedConnectionString);
            Assert.IsInstanceOf(expectedResultType, builder);
        }

        [Test]
        // not enough info for Azure (BlobEndpoint)
        [TestCase("AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1")]
        [TestCase("ContainerName=data;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1")]
        [TestCase("ContainerName=data;AccountName=devstoreaccount1;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1")]
        [TestCase("ContainerName=data;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==")]
        // not enough info for Azure (EndpointSuffix)
        [TestCase("DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;EndpointSuffix=core.chinacloudapi.cn")]
        [TestCase("ContainerName=data;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;EndpointSuffix=core.chinacloudapi.cn")]
        [TestCase("ContainerName=data;DefaultEndpointsProtocol=http;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;EndpointSuffix=core.chinacloudapi.cn")]
        [TestCase("ContainerName=data;DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;EndpointSuffix=core.chinacloudapi.cn")]
        [TestCase("ContainerName=data;DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==")]
        // not enough info for S3
        [TestCase("S3SecretKey=omega;S3Region=atlantis;S3BucketName=pufferfish")]
        [TestCase("S3AccessKey=alpha;S3Region=atlantis;S3BucketName=pufferfish")]
        [TestCase("S3AccessKey=alpha;S3SecretKey=omega;S3BucketName=pufferfish")]
        [TestCase("S3AccessKey=alpha;S3SecretKey=omega;S3Region=atlantis")]
        public void TestFailingToUnpack(string packedConnectionString)
        {
            Assert.Throws<AggregateException>(() => BlobClientFactory.ParseSettings(packedConnectionString));
        }

        [Test]
        // enough for both
        [TestCase("S3AccessKey=alpha;S3SecretKey=omega;S3Region=atlantis;S3BucketName=pufferfish;ContainerName=data;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1")]
        public void TestAmbiguousSettings(string packedConnectionString)
        {
            Assert.Throws<ArgumentException>(() => BlobClientFactory.ParseSettings(packedConnectionString));
        }
    }
}