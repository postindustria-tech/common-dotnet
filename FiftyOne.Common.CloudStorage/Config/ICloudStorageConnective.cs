namespace FiftyOne.Common.CloudStorage.Config
{
    /// <summary>
    /// A "trait" that allows for convenient adoption of
    /// new properties supported by underlying config subsection.
    /// </summary>
    public interface ICloudStorageConnective
    {
        /// <summary>
        /// Returns the existing or creates new underlying <see cref="CloudStorageConnectionOptions"/>
        /// </summary>
        /// <returns>The underlying config subsection.</returns>
        CloudStorageConnectionOptions GetOrMakeConnectionOptions();
    }
}
