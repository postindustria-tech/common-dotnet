using FiftyOne.Common.CloudStorage.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiftyOne.Common.CloudStorage.Config
{

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ForwardedToAttribute : Attribute
    {
        public IEnumerable<Type> BuilderTypes { get; private set; }

        public ForwardedToAttribute(params Type[] builderTypes)
        {
#if DEBUG
            foreach (var builderType in builderTypes)
            {
                if (!typeof(IBlobClientBuilder).IsAssignableFrom(builderType))
                {
                    throw new ArgumentException($"Type {builderType.FullName} does not implement {typeof(IBlobClientBuilder).Name}", nameof(builderTypes));
                }
            }
#endif
            BuilderTypes = builderTypes;
        }
    }
}
