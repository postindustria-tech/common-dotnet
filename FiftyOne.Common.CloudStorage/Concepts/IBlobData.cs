using System;
using System.IO;

namespace FiftyOne.Common.CloudStorage.Concepts
{
    public interface IBlobData: IDisposable
    {
        public Stream Data { get; }
    }
}
