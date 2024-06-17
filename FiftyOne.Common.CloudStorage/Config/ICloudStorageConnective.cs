namespace FiftyOne.Common.CloudStorage.Config
{
    public interface ICloudStorageConnective
    {
        CloudStorageConnectionOptions GetOrMakeConnectionOptions();
    }
}
