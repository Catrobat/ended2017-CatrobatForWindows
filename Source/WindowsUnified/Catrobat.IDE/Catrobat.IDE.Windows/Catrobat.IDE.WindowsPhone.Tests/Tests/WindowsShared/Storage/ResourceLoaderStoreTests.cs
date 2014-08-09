using System.IO;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.WindowsShared.Services.Storage;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.IDE.WindowsPhone.Tests.Tests.Storage
{
    [TestClass]
    public class ResourceLoaderStoreTests
    {
        [TestMethod]
        public void ResourceLoaderTest()
        {
            var factory = new ResourceLoaderWindowsShared();

            using (var resourceLoader = factory.CreateResourceLoader())
            {
                Stream stream = resourceLoader.OpenResourceStream(ResourceScope.TestsPhone,
                                                                  "SampleData/SamplePrograms/simple.xml");
                Assert.IsNotNull(stream);
                stream.Dispose();
            }
        }
    }
}
