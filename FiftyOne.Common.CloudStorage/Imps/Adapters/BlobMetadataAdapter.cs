using Blobject.Core;
using FiftyOne.Common.CloudStorage.Concepts;
using System;

namespace FiftyOne.Common.CloudStorage.Imps.Adapters
{
    public class BlobMetadataAdapter: IBlobMetadata
    {
        private readonly BlobMetadata _blobMetadata;

        public string Name => _blobMetadata.Key;
        public DateTime? LastModified => _blobMetadata.LastUpdateUtc;

        public BlobMetadataAdapter(BlobMetadata blobMetadata)
        {
            _blobMetadata = blobMetadata;
        }

        public void Dispose() => _blobMetadata.Dispose();
    }
}
