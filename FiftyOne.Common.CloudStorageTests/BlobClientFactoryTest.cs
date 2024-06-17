using FiftyOne.Common.CloudStorage.Factory;
using FiftyOne.Common.CloudStorage.Imps;

namespace FiftyOne.Common.CloudStorageTests
{
    public class BlobClientFactoryTest
    {
        [Test]
        
        [TestCase("azure with unused arg", typeof(AzureStorageSettings), "ContainerName=data;DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1")]
        [TestCase("azure", typeof(AzureStorageSettings), "ContainerName=data;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1")]
        [TestCase("azure with endpoint suffix", typeof(AzureStorageSettings), "ContainerName=data;DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;EndpointSuffix=core.chinacloudapi.cn")]
        
        [TestCase("aws s3 + ssl", typeof(S3StorageSettings), "S3AccessKey=alpha;S3SecretKey=omega;S3Region=atlantis;S3BucketName=pufferfish;S3UseSSL=true")]
        [TestCase("aws s3", typeof(S3StorageSettings), "S3AccessKey=alpha;S3SecretKey=omega;S3Region=atlantis;S3BucketName=pufferfish")]
        [TestCase("aws s3 + extra semicolons", typeof(S3StorageSettings), ";S3AccessKey=alpha;S3SecretKey=omega;;;S3Region=atlantis;S3BucketName=pufferfish;")]
        
        [TestCase("s3-compatible + region", typeof(S3CompatibleStorageSettings), "S3Endpoint=http://end.poi.nt/;S3UseSSL=true;S3AccessKey=alpha;S3SecretKey=omega;S3Region=atlantis;S3BucketName=pufferfish;S3BaseUrl=http://base.url/")]
        [TestCase("s3-compatible", typeof(S3CompatibleStorageSettings), "S3Endpoint=http://end.poi.nt/;S3UseSSL=true;S3AccessKey=alpha;S3SecretKey=omega;S3BucketName=pufferfish;S3BaseUrl=http://base.url/")]
        public void TestUnpackingType(string _, Type expectedResultType, string packedConnectionString)
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
        [TestCase("S3SecretKey=omega;S3Region=atlantis;S3BucketName=pufferfish;S3UseSSL=true")]
        [TestCase("S3SecretKey=omega;S3Region=atlantis;S3BucketName=pufferfish")]
        [TestCase("S3AccessKey=alpha;S3Region=atlantis;S3BucketName=pufferfish;S3UseSSL=true")]
        [TestCase("S3AccessKey=alpha;S3Region=atlantis;S3BucketName=pufferfish")]
        [TestCase("S3AccessKey=alpha;S3SecretKey=omega;S3BucketName=pufferfish;S3UseSSL=true")]
        [TestCase("S3AccessKey=alpha;S3SecretKey=omega;S3BucketName=pufferfish")]
        [TestCase("S3AccessKey=alpha;S3SecretKey=omega;S3Region=atlantis;S3UseSSL=true")]
        [TestCase("S3AccessKey=alpha;S3SecretKey=omega;S3Region=atlantis")]
        // not enough info for S3 compatible
        [TestCase("S3UseSSL=true;S3AccessKey=alpha;S3SecretKey=omega;S3Region=atlantis;S3BucketName=pufferfish;S3BaseUrl=http://base.url/")]
        [TestCase("S3UseSSL=true;S3AccessKey=alpha;S3SecretKey=omega;S3BucketName=pufferfish;S3BaseUrl=http://base.url/")]
        [TestCase("S3Endpoint=http://end.poi.nt/;S3AccessKey=alpha;S3SecretKey=omega;S3Region=atlantis;S3BucketName=pufferfish;S3BaseUrl=http://base.url/")]
        [TestCase("S3Endpoint=http://end.poi.nt/;S3AccessKey=alpha;S3SecretKey=omega;S3BucketName=pufferfish;S3BaseUrl=http://base.url/")]
        [TestCase("S3Endpoint=http://end.poi.nt/;S3UseSSL=true;S3SecretKey=omega;S3Region=atlantis;S3BucketName=pufferfish;S3BaseUrl=http://base.url/")]
        [TestCase("S3Endpoint=http://end.poi.nt/;S3UseSSL=true;S3SecretKey=omega;S3BucketName=pufferfish;S3BaseUrl=http://base.url/")]
        [TestCase("S3Endpoint=http://end.poi.nt/;S3UseSSL=true;S3AccessKey=alpha;S3Region=atlantis;S3BaseUrl=http://base.url/")]
        [TestCase("S3Endpoint=http://end.poi.nt/;S3UseSSL=true;S3AccessKey=alpha;S3BaseUrl=http://base.url/")]
        [TestCase("S3Endpoint=http://end.poi.nt/;S3UseSSL=true;S3AccessKey=alpha;S3SecretKey=omega;S3Region=atlantis;S3BucketName=pufferfish")]
        [TestCase("S3Endpoint=http://end.poi.nt/;S3UseSSL=true;S3AccessKey=alpha;S3SecretKey=omega;S3BucketName=pufferfish")]
        // enough for both Azure and S3
        [TestCase("S3AccessKey=alpha;S3SecretKey=omega;S3Region=atlantis;S3BucketName=pufferfish;ContainerName=data;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1")]
        public void TestFailingToUnpack(string packedConnectionString)
        {
            Assert.Throws<AggregateException>(() => BlobClientFactory.ParseSettings(packedConnectionString));
        }
    }
}