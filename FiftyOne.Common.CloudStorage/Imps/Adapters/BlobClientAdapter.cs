using FiftyOne.Common.CloudStorage.Concepts;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace FiftyOne.Common.CloudStorage.Imps.Adapters
{
    /// <summary>
    /// Wraps <see cref="Blobject.Core.IBlobClient"/>
    /// into <see cref="IBlobClient"/>.
    /// Also exposes some metadata properties
    /// for logging and analytics purposes.
    /// </summary>
    internal class BlobClientAdapter: IBlobClient
    {
        private readonly Blobject.Core.IBlobClient _client;
        private readonly bool _streamReading;

        /// <summary>
        /// See <see cref="IBlobClient.EffectiveAccountName"/>
        /// </summary>
        public string EffectiveAccountName { get; private set; }

        /// <summary>
        /// See <see cref="IBlobClient.EffectiveContainerName"/>
        /// </summary>
        public string EffectiveContainerName { get; private set; }

        /// <summary>
        /// Wraps <see cref="Blobject.Core.IBlobClient"/>
        /// into <see cref="IBlobClient"/>.
        /// Also exposes some metadata properties
        /// for logging and analytics purposes.
        /// </summary>
        /// <param name="client"><see cref="Blobject.Core.IBlobClient"/> to wrap.</param>
        /// <param name="streamReading">Whether the client supports streaming download.</param>
        /// <param name="effectiveAccountName">See <see cref="IBlobClient.EffectiveAccountName"/>.</param>
        /// <param name="effectiveContainerName">See <see cref="IBlobClient.EffectiveAccountName"/>.</param>
        public BlobClientAdapter(
            Blobject.Core.IBlobClient client,
            bool streamReading,
            string effectiveAccountName,
            string effectiveContainerName)
        {
            _client = client;
            _streamReading = streamReading;
            EffectiveAccountName = effectiveAccountName;
            EffectiveContainerName = effectiveContainerName;
        }

        /// <summary>
        /// See <see cref="IBlobClient.WriteAsync(string, string, Stream, CancellationToken)"/>
        /// </summary>
        public Task WriteAsync(string blobName, string contentType, Stream stream, CancellationToken token = default)
            => _client.WriteAsync(blobName, contentType, stream.Length, stream, token);


        /// <summary>
        /// See <see cref="IBlobClient.GetBlobs"/>
        /// </summary>
        public IEnumerable<IBlobMetadata> GetBlobs() 
            => _client.Enumerate().Select(blobMetadata => new BlobMetadataAdapter(blobMetadata));


        /// <summary>
        /// See <see cref="IBlobClient.GetStreamAsync(string, CancellationToken)"/>
        /// </summary>
        public async Task<IBlobData> GetStreamAsync(string blobName, CancellationToken token = default)
            => _streamReading
            ? new BlobDataAdapter(await _client.GetStreamAsync(blobName, token))
            : new FullBlobData(await _client.GetAsync(blobName, token)) 
            as IBlobData;


        /// <summary>
        /// See <see cref="IBlobClient.DeleteAsync(string, CancellationToken)"/>
        /// </summary>
        public Task DeleteAsync(string blobName, CancellationToken token = default) 
            => _client.DeleteAsync(blobName, token);
    }
}
