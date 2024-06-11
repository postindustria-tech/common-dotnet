using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FiftyOne.Common.CloudStorage.Concepts
{
    public interface IBlobData: IDisposable
    {
        public Stream Data { get; }
    }
}
