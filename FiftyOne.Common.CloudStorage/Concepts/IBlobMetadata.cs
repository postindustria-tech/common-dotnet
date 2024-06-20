using System;

namespace FiftyOne.Common.CloudStorage.Concepts
{
    /// <summary>
    /// Encapsulates resources needed to query blob metadata during storage enumeration.
    /// </summary>
    public interface IBlobMetadata: IDisposable
    {
        /// <summary>
        /// Name of the blob.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Date of last modification.
        /// </summary>
        DateTime? LastModified { get; }
    }
}
