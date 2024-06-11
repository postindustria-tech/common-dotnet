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
        public readonly string Container;

        public AzureStorageSettings(string ConnectionString, string Container)
        {
            this.ConnectionString = ConnectionString;
            this.Container = Container;
        }

        public IBlobClient Build()
        {
            return null;
        }
    }
}
