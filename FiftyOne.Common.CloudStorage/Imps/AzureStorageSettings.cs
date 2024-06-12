using FiftyOne.Common.CloudStorage.Concepts;
using FiftyOne.Common.CloudStorage.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiftyOne.Common.CloudStorage.Imps
{
    public class AzureStorageSettings: IBlobClientBuilder
    {
        public readonly string ConnectionString;
        public readonly string ContainerName;

        public AzureStorageSettings([UnusedParametersSink] string ConnectionString, string ContainerName)
        {
            this.ConnectionString = ConnectionString;
            this.ContainerName = ContainerName;
        }

        public IBlobClient Build()
        {
            return null;
        }
    }
}
