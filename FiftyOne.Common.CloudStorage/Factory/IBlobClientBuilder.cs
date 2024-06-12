using FiftyOne.Common.CloudStorage.Concepts;

namespace FiftyOne.Common.CloudStorage.Factory
{
    public interface IBlobClientBuilder
    {
        IBlobClient Build();
    }
}
