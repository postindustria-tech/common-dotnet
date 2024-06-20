using FiftyOne.Common.CloudStorage.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiftyOne.Common.CloudStorage.Config
{
    /// <summary>
    /// Marks which actual implementations are backing certain property
    /// of the <see cref="CloudStorageConnectionOptions"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ForwardedToAttribute : Attribute
    {
        /// <summary>
        /// Types that back the attributed property.
        /// </summary>
        public IEnumerable<Type> BuilderTypes { get; private set; }

        /// <summary>
        /// Marks which actual implementations are backing certain property
        /// of the <see cref="CloudStorageConnectionOptions"/>.
        /// </summary>
        /// <param name="builderTypes">Types that back the attributed property.</param>
        /// <exception cref="ArgumentException">If any of the passed in types do not implement <see cref="IBlobClientBuilder"/></exception>
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
