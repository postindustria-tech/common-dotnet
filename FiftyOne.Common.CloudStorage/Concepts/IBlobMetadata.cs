using System;

namespace FiftyOne.Common.CloudStorage.Concepts
{
    public interface IBlobMetadata: IDisposable
    {
        string Name { get; }
        DateTime? LastModified { get; }
    }
}
