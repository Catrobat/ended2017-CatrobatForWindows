using System.IO;
using Catrobat.Core.Services.Storage;
using Catrobat.Core.Utilities;
using Catrobat.Core.Services;
using Catrobat.Core.Services.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Tests.Misc;

namespace Catrobat.IDE.Tests.Tests.Misc
{
  [TestClass]
  public class ZipTests
  {
    [ClassInitialize()]
    public static void TestClassInitialize(TestContext testContext)
    {
      TestHelper.InitializeTests();
    }

    [TestMethod]
    public void UnZipSimpleTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();
      string path = "SampleData/SampleProjects/test.catroid";

      using (var resourceLoader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
      {
        Stream originalStream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, path);
        CatrobatZipService.UnzipCatrobatPackageIntoIsolatedStorage(originalStream, "Projects/TestProject");
        originalStream.Close();
        originalStream.Dispose();
      }

      using (IStorage storage = StorageSystem.GetStorage())
      {
        Assert.IsTrue(storage.DirectoryExists("/Projects/TestProject"));

        Assert.IsTrue(storage.FileExists("/Projects/TestProject/.nomedia"));

        Assert.IsTrue(storage.FileExists("/Projects/TestProject/projectcode.xml"));

        Assert.IsTrue(storage.FileExists("/Projects/TestProject/screenshot.png"));


        Assert.IsTrue(storage.DirectoryExists("/Projects/TestProject/images"));

        Assert.IsTrue(storage.FileExists("/Projects/TestProject/images/.nomedia"));

        Assert.IsTrue(storage.FileExists("/Projects/TestProject/images/5A71C6F41035979503BA294F78A09336_background"));

        Assert.IsTrue(storage.FileExists("/Projects/TestProject/images/34A109A82231694B6FE09C216B390570_normalCat"));

        Assert.IsTrue(storage.FileExists("/Projects/TestProject/images/395CD6389BD601812BDB299934A0CCB4_banzaiCat"));

        Assert.IsTrue(storage.FileExists("/Projects/TestProject/images/B4497E87AC34B1329DD9B14C08EEAFF0_cheshireCat"));


        Assert.IsTrue(storage.DirectoryExists("/Projects/TestProject/sounds"));

        Assert.IsTrue(storage.FileExists("/Projects/TestProject/sounds/.nomedia"));
      }
    }

    [TestMethod]
    public void ZipSimpleTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();
      string path = "SampleData/SampleProjects/test.catroid";

      using (var resourceLoader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
      {
        Stream originalStream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, path);
        CatrobatZipService.UnzipCatrobatPackageIntoIsolatedStorage(originalStream, "Projects/TestProject");
        originalStream.Close();
        originalStream.Dispose();
      }

      using (IStorage storage = StorageSystem.GetStorage())
      {
        string writePath = "/Projects/TestProjectZipped/test.catroid";
        string sourcePath = "Projects/TestProject";

        if (storage.FileExists(writePath))
          storage.DeleteFile(writePath);

        using (Stream fileStream = storage.OpenFile(writePath, StorageFileMode.Create, StorageFileAccess.Write))
        {
          CatrobatZipService.ZipCatrobatPackage(fileStream, sourcePath);
          fileStream.Close();
          fileStream.Dispose();
        }

        Assert.IsTrue(storage.FileExists("/Projects/TestProjectZipped/test.catroid"));
      }
    }
  }
}
