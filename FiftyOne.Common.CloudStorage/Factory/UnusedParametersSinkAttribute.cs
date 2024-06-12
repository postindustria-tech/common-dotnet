using System;
using System.Collections.Generic;
using System.Text;

namespace FiftyOne.Common.CloudStorage.Factory
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class UnusedParametersSinkAttribute: Attribute
    {
    }
}
