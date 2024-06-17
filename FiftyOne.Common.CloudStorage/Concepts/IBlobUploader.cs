using System;
using System.IO;

namespace FiftyOne.Common.CloudStorage.Concepts
{
    public interface IBlobUploader: IDisposable
    {
        Stream WritableStream { get; }
    }
}
