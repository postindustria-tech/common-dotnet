using System.IO;

namespace FiftyOne.Common.CloudStorage.StreamWrappers
{
    public class TemporaryMemory : ITemporaryStreamWrapper
    {
        private readonly MemoryStream _stream = new MemoryStream();

        public Stream WritableStream => _stream;
        public Stream ReadableStream
        {
            get
            {
                _stream.Seek(0, SeekOrigin.Begin);
                return _stream;
            }
        }

        public void Dispose()
        {
            _stream.Dispose();
        }
    }
}
