using FiftyOne.Common.CloudStorage.Concepts;
using FiftyOne.Common.CloudStorage.StreamWrappers;
using System;
using System.Collections.Generic;
using System.IO;

namespace FiftyOne.Common.CloudStorage.Uploader
{
    public class BlobUploader: IBlobUploader
    {
        private readonly Action<Stream> uploadDelegate;
        private readonly ITemporaryStreamWrapper temporaryStreamWrapper;

        public Stream WritableStream => temporaryStreamWrapper.WritableStream;

        public BlobUploader(Action<Stream> uploadDelegate, ITemporaryStreamWrapper temporaryStreamWrapper)
        {
            this.uploadDelegate = uploadDelegate;
            this.temporaryStreamWrapper = temporaryStreamWrapper;
        }

        public void Dispose()
        {
            var errors = new List<Exception>();
            try
            {
                uploadDelegate.Invoke(temporaryStreamWrapper.ReadableStream);
            }
            catch (Exception ex)
            {
                errors.Add(ex);
            }
            try
            {
                temporaryStreamWrapper.Dispose();
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
