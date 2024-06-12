using Blobject.Core;
using FiftyOne.Common.CloudStorage.Concepts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace FiftyOne.Common.CloudStorage.Imps.Adapters
{
    public class BlobClientAdapter: Concepts.IBlobClient
    {
        private readonly Blobject.Core.IBlobClient _client;

        public BlobClientAdapter(Blobject.Core.IBlobClient client)
        {
            _client = client;
        }

        public Task WriteAsync(string blobName, string contentType, Stream stream, CancellationToken token = default)
            => _client.WriteAsync(blobName, contentType, stream.Length, stream, token);

        public IEnumerable<IBlobMetadata> GetBlobs() 
            => _client.Enumerate().Select(blobMetadata => new BlobMetadataAdapter(blobMetadata));

        public async Task<IBlobData> GetStreamAsync(string blobName, CancellationToken token = default)
            => new BlobDataAdapter(await _client.GetStreamAsync(blobName, token));

        public Task DeleteAsync(string blobName, CancellationToken token = default) 
            => _client.DeleteAsync(blobName, token);
    }
}
