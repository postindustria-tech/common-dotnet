using FiftyOne.Common.CloudStorage.Concepts;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace FiftyOne.Common.CloudStorage.Imps.Adapters
{
    public class BlobClientAdapter: IBlobClient
    {
        private readonly Blobject.Core.IBlobClient _client;
        private readonly bool _streamReading;

        public BlobClientAdapter(Blobject.Core.IBlobClient client,  bool streamReading = true)
        {
            _client = client;
            _streamReading = streamReading;
        }

        public Task WriteAsync(string blobName, string contentType, Stream stream, CancellationToken token = default)
            => _client.WriteAsync(blobName, contentType, stream.Length, stream, token);

        public IEnumerable<IBlobMetadata> GetBlobs() 
            => _client.Enumerate().Select(blobMetadata => new BlobMetadataAdapter(blobMetadata));

        public async Task<IBlobData> GetStreamAsync(string blobName, CancellationToken token = default)
            => _streamReading
            ? new BlobDataAdapter(await _client.GetStreamAsync(blobName, token))
            : new FullBlobData(await _client.GetAsync(blobName, token)) 
            as IBlobData;

        public Task DeleteAsync(string blobName, CancellationToken token = default) 
            => _client.DeleteAsync(blobName, token);
    }
}
