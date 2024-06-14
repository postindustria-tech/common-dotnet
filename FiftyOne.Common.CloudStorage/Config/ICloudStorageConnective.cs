using System;
using System.Collections.Generic;
using System.Text;

namespace FiftyOne.Common.CloudStorage.Config
{
    public interface ICloudStorageConnective
    {
        CloudStorageConnectionOptions GetOrMakeConnectionOptions();
    }
}
