using FiftyOne.Common.CloudStorage.Config;
using System.Reflection;

namespace FiftyOne.Common.CloudStorageTests
{
    public class CloudStorageConnectionOptionsTest
    {
        [Test]
        public void TestFragmentBacking()
        {
            var options = new CloudStorageConnectionOptions();
            int propertiesSet = 0;
            foreach (var property in options.GetType()
                .GetProperties()
                .Where(p => p.CanWrite))
            {
                if (property.GetCustomAttribute<ForwardedToAttribute>() is ForwardedToAttribute fwdTo)
                {
                    foreach (var nextType in fwdTo.BuilderTypes)
                    {
                        var backedProp = nextType.GetProperty(property.Name);
                        if (backedProp is null)
                        {
                            throw new InvalidOperationException(
                                $"Property {property.Name} isn't actually backed by {nextType.FullName}.");
                        }
                    }
                }
                if (property.PropertyType == typeof(string))
                {
                    property.SetValue(options, property.Name);
                    ++propertiesSet;
                }
            }
            var packedOptions = options.PackedConnectionString;
            Assert.That(
                packedOptions.Split(";").Count, 
                Is.EqualTo(propertiesSet));
        }
    }
}
