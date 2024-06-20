using Blobject.Core;
using FiftyOne.Common.CloudStorage.Concepts;
using System;

namespace FiftyOne.Common.CloudStorage.Imps.Adapters
{
    /// <summary>
    /// Wraps <see cref="BlobMetadata"/>
    /// into <see cref="IBlobMetadata"/>.
    /// </summary>
    internal class BlobMetadataAdapter: IBlobMetadata
    {
        private readonly BlobMetadata _blobMetadata;

        /// <summary>
        /// Name of the blob.
        /// See <see cref="BlobMetadata.Key"/>.
        /// </summary>
        public string Name => _blobMetadata.Key;

        /// <summary>
        /// Date of last modification.
        /// See <see cref="BlobMetadata.LastUpdateUtc"/>.
        /// </summary>
        public DateTime? LastModified => _blobMetadata.LastUpdateUtc;


        /// <summary>
        /// Wraps <see cref="BlobMetadata"/>
        /// into <see cref="IBlobMetadata"/>.
        /// </summary>
        public BlobMetadataAdapter(BlobMetadata blobMetadata)
        {
            _blobMetadata = blobMetadata;
        }


        /// <summary>
        /// Disposes of the underlying metadata.
        /// </summary>
        public void Dispose() => _blobMetadata.Dispose();
    }
}
