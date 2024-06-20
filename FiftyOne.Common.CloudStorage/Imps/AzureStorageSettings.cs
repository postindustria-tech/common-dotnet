using Blobject.AzureBlob;
using FiftyOne.Common.CloudStorage.Concepts;
using FiftyOne.Common.CloudStorage.Factory;
using FiftyOne.Common.CloudStorage.Imps.Adapters;

namespace FiftyOne.Common.CloudStorage.Imps
{
    /// <summary>
    /// Azure connection settings.
    /// Backed by <see cref="AzureBlobSettings"/>.
    /// </summary>
    public class AzureStorageSettings: IBlobClientBuilder
    {
        /// <summary>
        /// See <see cref="AzureBlobSettings.AccountName"/>.
        /// </summary>
        public string AccountName { get; private set; }

        /// <summary>
        /// See <see cref="AzureBlobSettings.AccessKey"/>.
        /// </summary>
        public string AccountKey { get; private set; }

        /// <summary>
        /// See <see cref="AzureBlobSettings.Endpoint"/>.
        /// </summary>
        public string? BlobEndpoint { get; private set; }

        /// <summary>
        /// See <see cref="AzureBlobSettings.Container"/>.
        /// </summary>
        public string ContainerName { get; private set; }

        /// <summary>
        /// Default protocol for endpoints.
        /// http or https.
        /// </summary>
        public string? DefaultEndpointsProtocol { get; private set; }
        
        /// <summary>
        /// Suffix for endpoints.
        /// </summary>
        public string? EndpointSuffix { get; private set; }

        /// <summary>
        /// Constructor that uses explicit BlobEndpoint.
        /// </summary>
        /// <param name="ContainerName">See <see cref="AzureBlobSettings.Container"/>.</param>
        /// <param name="AccountName">See <see cref="AzureBlobSettings.AccountName"/>.</param>
        /// <param name="AccountKey">See <see cref="AzureBlobSettings.AccessKey"/>.</param>
        /// <param name="BlobEndpoint">See <see cref="AzureBlobSettings.Endpoint"/>.</param>
        public AzureStorageSettings(
            string ContainerName,
            string AccountName,
            string AccountKey,
            string BlobEndpoint)
        {
            this.AccountName = AccountName;
            this.AccountKey = AccountKey;
            this.BlobEndpoint = BlobEndpoint;
            this.ContainerName = ContainerName;
        }

        /// <summary>
        /// Constructor that uses 
        /// DefaultEndpointsProtocol and EndpointSuffix
        /// to build BlobEndpoint.
        /// </summary>
        /// <param name="ContainerName">See <see cref="AzureBlobSettings.Container"/>.</param>
        /// <param name="AccountName">See <see cref="AzureBlobSettings.AccountName"/>.</param>
        /// <param name="AccountKey">See <see cref="AzureBlobSettings.AccessKey"/>.</param>
        /// <param name="DefaultEndpointsProtocol">Default protocol for endpoints. http or https.</param>
        /// <param name="EndpointSuffix">Suffix for endpoints.</param>
        public AzureStorageSettings(
            string ContainerName,
            string AccountName,
            string AccountKey,
            string DefaultEndpointsProtocol,
            string EndpointSuffix)
        {
            this.AccountName = AccountName;
            this.AccountKey = AccountKey;
            this.ContainerName = ContainerName;
            this.DefaultEndpointsProtocol = DefaultEndpointsProtocol;
            this.EndpointSuffix = EndpointSuffix;
        }

        /// <summary>
        /// Uses properties to build the client.
        /// </summary>
        /// <returns>See <see cref="IBlobClient"/>.</returns>
        public IBlobClient Build()
        {
            return
                new BlobClientAdapter(
                    new AzureBlobClient(
                        new AzureBlobSettings(
                            AccountName, 
                            AccountKey, 
                            BlobEndpoint ?? $"{DefaultEndpointsProtocol}://{AccountName}.blob.{EndpointSuffix}/", 
                            ContainerName
                        )
                    ),
                    true,
                    AccountName,
                    ContainerName
                );
        }
    }
}
