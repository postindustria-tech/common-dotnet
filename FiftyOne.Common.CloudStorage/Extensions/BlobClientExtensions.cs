using FiftyOne.Common.CloudStorage.Concepts;
using FiftyOne.Common.CloudStorage.StreamWrappers;
using FiftyOne.Common.CloudStorage.Uploader;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace FiftyOne.Common.CloudStorage.Extensions
{
    public static class BlobClientExtensions
    {
        public static IBlobUploader GetWritableStream<T>(
            this IBlobClient blobClient, 
            string blobName, 
            string contentType, 
            CancellationToken token = default) where T: ITemporaryStreamWrapper, new()
            => new BlobUploader(stream => blobClient.WriteAsync(blobName, contentType, stream, token).Wait(), new T());
    }
}
