using FiftyOne.Common.CloudStorage.Concepts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FiftyOne.Common.CloudStorage.Imps.Adapters
{
    public class FullBlobData : IBlobData
    {
        public Stream Data { get; private set; }

        public FullBlobData(byte[] bytes)
        {
            Data = new MemoryStream(bytes);
        }

        public void Dispose() 
            => Data.Dispose();
    }
}
