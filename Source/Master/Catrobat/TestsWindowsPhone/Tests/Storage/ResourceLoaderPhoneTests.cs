using Catrobat.Core.Storage;
using Catrobat.IDEWindowsPhone.Misc.Storage;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.TestsWindowsPhone.Tests.Storage
{
  [TestClass]
  public class ResourceLoaderPhoneTests
  {
    [TestMethod]
    public void ResourceLoaderTest()
    {
      ResourceLoader.SetResourceLoader(new ResourcesPhone());

      var stream = ResourceLoader.GetResourceStream(ResourceScope.TestsPhone, "SampleData/SampleProjects/simple.xml");
      Assert.AreNotEqual(stream, null);
    }
  }
}
