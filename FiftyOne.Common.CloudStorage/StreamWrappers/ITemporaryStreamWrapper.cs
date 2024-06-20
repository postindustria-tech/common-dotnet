using System;
using System.IO;

namespace FiftyOne.Common.CloudStorage.StreamWrappers
{
    /// <summary>
    /// Encapsulates resources used to provide
    /// both writable and readable stream
    /// to a temporary buffer (memory, file etc.)
    /// </summary>
    public interface ITemporaryStreamWrapper: IDisposable
    {
        /// <summary>
        /// Writable stream for the resource.
        /// </summary>
        Stream WritableStream { get; }

        /// <summary>
        /// Readable stream to the written resource.
        /// May close/dispose of the writable stream when called.
        /// </summary>
        Stream ReadableStream { get; }
    }
}
