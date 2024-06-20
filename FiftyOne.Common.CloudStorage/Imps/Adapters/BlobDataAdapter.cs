using Blobject.Core;
using FiftyOne.Common.CloudStorage.Concepts;
using System.IO;

namespace FiftyOne.Common.CloudStorage.Imps.Adapters
{
    /// <summary>
    /// Wraps <see cref="BlobData"/>
    /// into <see cref="IBlobData"/>.
    /// </summary>
    internal class BlobDataAdapter: IBlobData
    {
        private readonly BlobData _blobData;

        /// <summary>
        /// Readable stream to download blob content.
        /// See <see cref="BlobData.Data"/>.
        /// </summary>
        public Stream Data => _blobData.Data;

        /// <summary>
        /// Wraps <see cref="BlobData"/>
        /// into <see cref="IBlobData"/>.
        /// </summary>
        public BlobDataAdapter(BlobData blobData)
        {
            _blobData = blobData;
        }

        /// <summary>
        /// Disposes of the underlying data.
        /// </summary>
        public void Dispose() => _blobData.Dispose();
    }
}
