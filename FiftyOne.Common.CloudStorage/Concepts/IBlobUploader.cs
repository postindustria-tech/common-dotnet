using System;
using System.IO;

namespace FiftyOne.Common.CloudStorage.Concepts
{
    /// <summary>
    /// Encapsulated the resources to keep writable stream open for upload purposes.
    /// The provided stream may point to temporary buffer (memory, file etc.) and 
    /// the actual upload might happen upon disposal.
    /// </summary>
    public interface IBlobUploader: IDisposable
    {
        /// <summary>
        /// Stream to write data to (for uploading).
        /// </summary>
        Stream WritableStream { get; }
    }
}
