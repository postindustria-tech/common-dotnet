using System;
using System.IO;

namespace FiftyOne.Common.CloudStorage.StreamWrappers
{
    public interface ITemporaryStreamWrapper: IDisposable
    {
        Stream WritableStream { get; }
        Stream ReadableStream { get; }
    }
}
