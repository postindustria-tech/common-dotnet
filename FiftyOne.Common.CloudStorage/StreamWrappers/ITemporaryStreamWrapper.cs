using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FiftyOne.Common.CloudStorage.StreamWrappers
{
    public interface ITemporaryStreamWrapper: IDisposable
    {
        Stream WritableStream { get; }
        Stream ReadableStream { get; }
    }
}
