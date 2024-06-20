using FiftyOne.Common.CloudStorage.Concepts;

namespace FiftyOne.Common.CloudStorage.Factory
{
    /// <summary>
    /// Uses some settings to connect to a specific blob-like storage.
    /// </summary>
    public interface IBlobClientBuilder
    {
        /// <summary>
        /// Instantiates a new client to manipulate described blob-like storage.
        /// </summary>
        /// <returns>
        /// A client that enables interaction with the described blob-like storage.
        /// </returns>
        IBlobClient Build();
    }
}
