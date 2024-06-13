using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FiftyOne.Common.CloudStorage.Concepts
{
    public interface IBlobUploader: IDisposable
    {
        Stream WritableStream { get; }
    }
}
