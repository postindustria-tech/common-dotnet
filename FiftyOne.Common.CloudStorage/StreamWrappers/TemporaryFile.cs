using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FiftyOne.Common.CloudStorage.StreamWrappers
{
    public class TemporaryFile: ITemporaryStreamWrapper
    {
        private readonly string _path = Path.GetTempFileName();
        private Stream? _writeStream;
        private Stream? _readStream;

        public Stream WritableStream
        {
            get
            {
                if (_writeStream is null)
                {
                    _writeStream = File.OpenWrite(_path);
                }
                return _writeStream;
            }
        }
        public Stream ReadableStream
        {
            get
            {
                _writeStream?.Dispose();
                _writeStream = null;
                if (_readStream is null)
                {
                    _readStream = File.OpenRead(_path);
                }
                return _readStream;
            }
        }

        public void Dispose()
        {
            _writeStream?.Dispose();
            _writeStream = null;
            _readStream?.Dispose();
            _readStream = null;
            File.Delete(_path);
        }
    }
}
