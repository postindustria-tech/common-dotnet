using FiftyOne.Common.CloudStorage.Concepts;
using FiftyOne.Common.CloudStorage.StreamWrappers;
using FiftyOne.Common.CloudStorage.Uploader;
using System.Threading;

namespace FiftyOne.Common.CloudStorage.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IBlobClient"/>.
    /// </summary>
    public static class BlobClientExtensions
    {
        /// <summary>
        /// Convenience method to get a writable stream for upload.
        /// </summary>
        /// <typeparam name="T">Type of temporary storage to use (memory, file etc.).</typeparam>
        /// <param name="blobClient">Extended <see cref="IBlobClient"/>.</param>
        /// <param name="blobName">Name of the blob to replace.</param>
        /// <param name="contentType">MIME-Type of the content written.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns></returns>
        public static IBlobUploader GetWritableStream<T>(
            this IBlobClient blobClient, 
            string blobName, 
            string contentType, 
            CancellationToken token = default) where T: ITemporaryStreamWrapper, new()
            => new BlobUploader(stream => blobClient.WriteAsync(blobName, contentType, stream, token).Wait(), new T());
    }
}
