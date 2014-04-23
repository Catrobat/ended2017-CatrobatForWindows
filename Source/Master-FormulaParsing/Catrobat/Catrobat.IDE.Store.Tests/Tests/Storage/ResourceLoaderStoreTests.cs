using System.IO;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Store.Services.Storage;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.IDE.Store.Tests.Tests.Storage
{
    [TestClass]
    public class ResourceLoaderStoreTests
    {
        [TestMethod]
        public void ResourceLoaderTest()
        {
            var factory = new ResourceLoaderFactoryStore();

            using (var resourceLoader = factory.CreateResourceLoader())
            {
                Stream stream = resourceLoader.OpenResourceStream(ResourceScope.TestsPhone,
                                                                  "SampleData/SampleProjects/simple.xml");
                Assert.IsNotNull(stream);
                stream.Dispose();
            }
        }
    }
}
