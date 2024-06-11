using FiftyOne.Common.CloudStorage.Concepts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiftyOne.Common.CloudStorage.Factory
{
    public interface IBlobClientBuilder
    {
        IBlobClient Build();
    }
}
