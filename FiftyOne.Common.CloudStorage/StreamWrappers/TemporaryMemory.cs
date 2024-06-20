using System.IO;

namespace FiftyOne.Common.CloudStorage.StreamWrappers
{
    /// <summary>
    /// Encapsulated MemoryStream as a temporary read-write resource.
    /// </summary>
    public class TemporaryMemory : ITemporaryStreamWrapper
    {
        private readonly MemoryStream _stream = new MemoryStream();

        /// <summary>
        /// Return stream for writing.
        /// </summary>
        public Stream WritableStream => _stream;

        /// <summary>
        /// Reset position and return stream for reading.
        /// </summary>
        public Stream ReadableStream
        {
            get
            {
                _stream.Seek(0, SeekOrigin.Begin);
                return _stream;
            }
        }

        /// <summary>
        /// Disposes of the underlying stream.
        /// </summary>
        public void Dispose()
        {
            _stream.Dispose();
        }
    }
}
