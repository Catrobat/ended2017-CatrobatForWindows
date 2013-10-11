using System.IO;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Phone.Services.Storage;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.IDE.Phone.Tests.Tests.Storage
{
    [TestClass]
    public class ResourceLoaderPhoneTests
    {
        [TestMethod]
        public void ResourceLoaderTest()
        {
            var factory = new ResourceLoaderFactoryPhone();

            using (var resourceLoader = factory.CreateResourceLoader())
            {
                Stream stream = resourceLoader.OpenResourceStream(ResourceScope.TestsPhone,
                                                                  "SampleData/SampleProjects/simple.xml");
                Assert.IsNotNull(stream);
                stream.Close();
                stream.Dispose();
            }
        }
    }
}
