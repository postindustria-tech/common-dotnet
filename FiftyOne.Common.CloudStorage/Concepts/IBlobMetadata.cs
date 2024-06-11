using System;
using System.Collections.Generic;
using System.Text;

namespace FiftyOne.Common.CloudStorage.Concepts
{
    public interface IBlobMetadata: IDisposable
    {
        string Name { get; }
        DateTime? LastModified { get; }
    }
}
