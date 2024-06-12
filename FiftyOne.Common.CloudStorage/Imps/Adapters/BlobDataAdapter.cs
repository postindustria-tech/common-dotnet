using Blobject.Core;
using FiftyOne.Common.CloudStorage.Concepts;
using System.IO;

namespace FiftyOne.Common.CloudStorage.Imps.Adapters
{
    public class BlobDataAdapter: IBlobData
    {
        private readonly BlobData _blobData;

        public Stream Data => _blobData.Data;

        public BlobDataAdapter(BlobData blobData)
        {
            _blobData = blobData;
        }

        public void Dispose() => _blobData.Dispose();
    }
}
