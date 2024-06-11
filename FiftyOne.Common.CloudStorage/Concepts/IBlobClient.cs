using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FiftyOne.Common.CloudStorage.Concepts
{
    public interface IBlobClient
    {
        /// <summary>
        /// Writes the data from the specified stream to the BLOB with the specified key.
        /// </summary>
        /// <param name="blobName">The key of the BLOB to write to.</param>
        /// <param name="contentType">The content type of the BLOB.</param>
        /// <param name="stream">The stream containing the data to write to the BLOB.</param>
        /// <param name="token">A cancellation token to observe while waiting for the task to complete.</param>
        Task WriteAsync(string blobName, string contentType, Stream stream, CancellationToken token = default);

        /// <summary>
        /// Enumerate all BLOBs within the repository asynchronously.
        /// </summary>
        /// <returns>IEnumerable of <see cref="IBlobMetadata"/>.</returns>
        IEnumerable<IBlobMetadata> GetBlobs();

        /// <summary>
        /// Gets the stream of the BLOB with the specified key.
        /// </summary>
        /// <param name="blobName">The key of the BLOB to get.</param>
        /// <param name="token">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>An <see cref="IBlobData"/> object containing the stream of the BLOB.</returns>
        Task<IBlobData> GetStreamAsync(string blobName, CancellationToken token = default);

        /// <summary>
        /// Deletes an object from the BLOB storage asynchronously.
        /// </summary>
        /// <param name="blobName">The key of the object to delete from the BLOB storage.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(string blobName, CancellationToken token = default);
    }
}
