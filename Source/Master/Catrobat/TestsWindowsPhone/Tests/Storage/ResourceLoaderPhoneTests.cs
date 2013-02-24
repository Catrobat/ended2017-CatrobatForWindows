using System.IO;
using Catrobat.Core.Storage;
using Catrobat.IDEWindowsPhone.Misc.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        Stream stream = resourceLoader.OpenResourceStream(ResourceScope.TestsPhone,"SampleData/SampleProjects/simple.xml");
        Assert.AreNotEqual(stream, null);
        stream.Close();
        stream.Dispose();
      }
    }
  }
}
