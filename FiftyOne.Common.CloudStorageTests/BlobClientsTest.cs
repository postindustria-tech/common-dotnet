using FiftyOne.Common.CloudStorage.Concepts;
using FiftyOne.Common.CloudStorage.Extensions;
using FiftyOne.Common.CloudStorage.Factory;
using FiftyOne.Common.CloudStorage.StreamWrappers;
using System.Text;

namespace FiftyOne.Common.CloudStorageTests
{
    public class BlobClientsTest
    {
        [Ignore("For local testing only")]
        [Test]
        [TestCase("azurite", "ContainerName=unit-tests;DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;")]
        [TestCase("s3 ninja", "S3AccessKey=AKIAIOSFODNN7EXAMPLE;S3SecretKey=wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY;S3BucketName=unit-tests;S3Endpoint=http://localhost:9444/;S3BaseUrl=http://localhost:9444/{bucket}/{key};S3UseSSL=true;S3Region=s3")]
        public async Task TestBlobClient(string _, string packedConnectionString)
        {
            const string mimeType = "text/plain";
            string ts = DateTime.Now.ToString("O").Replace(":", null);
            string blobText = $"me is da blob ({ts})";
            string blobName = $"test-blob-{ts}.txt";

            var client = BlobClientFactory.ParseSettings(packedConnectionString).Build();
            
            // write

            using (var uploader = client.GetWritableStream<TemporaryFile>(blobName, mimeType))
            using (var writer = new StreamWriter(uploader.WritableStream, Encoding.UTF8))
            {
                writer.Write(blobText);
            }

            // enumerate

            Assert.That(client.GetBlobs().Select(b => b.Name).ToArray(), Is.EquivalentTo(new[] { blobName }));

            // read

            string receivedText;
            using (var blobData = await client.GetStreamAsync(blobName))
            using (var reader = new StreamReader(blobData.Data))
            {
                receivedText = reader.ReadToEnd();
            }

            Assert.That(receivedText, Is.EqualTo(blobText));

            // delete

            await client.DeleteAsync(blobName);

            Assert.That(client.GetBlobs().Select(b => b.Name).ToArray(), Is.Empty);
        }
    }
}
