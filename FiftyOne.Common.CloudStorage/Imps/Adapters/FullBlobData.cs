using FiftyOne.Common.CloudStorage.Concepts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FiftyOne.Common.CloudStorage.Imps.Adapters
{
    /// <summary>
    /// Wraps `byte[]` into <see cref="IBlobData"/>.
    /// </summary>
    internal class FullBlobData : IBlobData
    {
        /// <summary>
        /// Readable stream to pre-downloaded blob content.
        /// </summary>
        public Stream Data { get; private set; }

        /// <summary>
        /// Wraps `byte[]` into <see cref="IBlobData"/>.
        /// </summary>
        public FullBlobData(byte[] bytes)
        {
            Data = new MemoryStream(bytes);
        }

        /// <summary>
        /// Disposes of underlying resources.
        /// </summary>
        public void Dispose() 
            => Data.Dispose();
    }
}
