using System;
using System.IO;

namespace FiftyOne.Common.CloudStorage.Concepts
{
    /// <summary>
    /// Encapsulates the resources to keep the download stream available.
    /// </summary>
    public interface IBlobData: IDisposable
    {
        /// <summary>
        /// Readable stream to download blob content.
        /// </summary>
        public Stream Data { get; }
    }
}
