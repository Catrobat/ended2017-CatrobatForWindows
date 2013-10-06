using System.IO;
using Catrobat.Core.Storage;
using Catrobat.IDEWindowsPhone.Misc.Storage;
using Catrobat.IDEWindowsPhone.Utilities.Storage;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.TestsWindowsPhone.Tests.Storage
{
  [TestClass]
  public class ResourceLoaderPhoneTests
  {
    [TestMethod]
    public void ResourceLoaderTest()
    {
      ResourceLoader.SetResourceLoaderFactory(new ResourceLoaderFactoryPhone());

      using (var resourceLoader = ResourceLoader.CreateResourceLoader())
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
