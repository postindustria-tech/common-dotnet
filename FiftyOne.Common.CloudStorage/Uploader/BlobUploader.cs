using FiftyOne.Common.CloudStorage.Concepts;
using FiftyOne.Common.CloudStorage.StreamWrappers;
using System;
using System.Collections.Generic;
using System.IO;

namespace FiftyOne.Common.CloudStorage.Uploader
{
    /// <summary>
    /// Encapsulates both temporary resource
    /// and readable stream handler into
    /// a provider of writable stream
    /// that invokes the handler on written data
    /// once disposed of.
    /// </summary>
    internal class BlobUploader: IBlobUploader
    {
        private readonly Action<Stream> uploadDelegate;
        private readonly ITemporaryStreamWrapper temporaryStreamWrapper;

        /// <summary>
        /// Exposes writable stream.
        /// </summary>
        public Stream WritableStream => temporaryStreamWrapper.WritableStream;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uploadDelegate">
        /// Action to read and handle pre-written data,
        /// e.g. upload callback.
        /// </param>
        /// <param name="temporaryStreamWrapper">
        /// A temporary resource that provides write/read streams.
        /// </param>
        public BlobUploader(Action<Stream> uploadDelegate, ITemporaryStreamWrapper temporaryStreamWrapper)
        {
            this.uploadDelegate = uploadDelegate;
            this.temporaryStreamWrapper = temporaryStreamWrapper;
        }

        /// <summary>
        /// Invokes the data handler action
        /// and disposes of underlying resources.
        /// </summary>
        /// <exception cref="AggregateException"></exception>
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
