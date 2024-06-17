using System;
using System.IO;
using System.Collections.Generic;

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
            var errors = new List<Exception>();
            try
            {
                _writeStream?.Dispose();
            }
            catch (Exception ex)
            {
                errors.Add(ex);
            }
            _writeStream = null;
            try
            {
                _readStream?.Dispose();
            }
            catch (Exception e)
            {
                errors.Add(e);
            }
            _readStream = null;
            try
            {
                File.Delete(_path);
            }
            catch (Exception e)
            {
                errors.Add(e);
            }
            if (errors.Count != 0)
            {
                throw new AggregateException($"Failed to dispose of {this.GetType().Name}.", errors);
            }
        }
    }
}
