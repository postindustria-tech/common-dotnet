using FiftyOne.Common.CloudStorage.Concepts;
using FiftyOne.Common.CloudStorage.StreamWrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
            uploadDelegate.Invoke(temporaryStreamWrapper.ReadableStream);
            temporaryStreamWrapper.Dispose();
        }
    }
}
