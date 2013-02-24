using Catrobat.Core.ZIP;
using System.IO;
using Catrobat.Core.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.TestsCommon.Misc;

namespace Catrobat.TestsCommon.Tests.Misc
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

      using (var resourceLoader = ResourceLoader.CreateResourceLoader())
      {
        Stream originalStream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, path);
        CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(originalStream, "Projects/TestProject");
        originalStream.Close();
        originalStream.Dispose();
      }

      using (IStorage storage = StorageSystem.GetStorage())
      {
        Assert.AreEqual(storage.DirectoryExists("/Projects/TestProject"), true);

        Assert.AreEqual(storage.FileExists("/Projects/TestProject/.nomedia"), true);

        Assert.AreEqual(storage.FileExists("/Projects/TestProject/projectcode.xml"), true);

        Assert.AreEqual(storage.FileExists("/Projects/TestProject/screenshot.png"), true);


        Assert.AreEqual(storage.DirectoryExists("/Projects/TestProject/images"), true);

        Assert.AreEqual(storage.FileExists("/Projects/TestProject/images/.nomedia"), true);

        Assert.AreEqual(storage.FileExists("/Projects/TestProject/images/5A71C6F41035979503BA294F78A09336_background"), true);

        Assert.AreEqual(storage.FileExists("/Projects/TestProject/images/34A109A82231694B6FE09C216B390570_normalCat"), true);

        Assert.AreEqual(storage.FileExists("/Projects/TestProject/images/395CD6389BD601812BDB299934A0CCB4_banzaiCat"), true);

        Assert.AreEqual(storage.FileExists("/Projects/TestProject/images/B4497E87AC34B1329DD9B14C08EEAFF0_cheshireCat"), true);


        Assert.AreEqual(storage.DirectoryExists("/Projects/TestProject/sounds"), true);

        Assert.AreEqual(storage.FileExists("/Projects/TestProject/sounds/.nomedia"), true);
      }
    }

    [TestMethod]
    public void ZipSimpleTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();
      string path = "SampleData/SampleProjects/test.catroid";

      using (var resourceLoader = ResourceLoader.CreateResourceLoader())
      {
        Stream originalStream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, path);
        CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(originalStream, "Projects/TestProject");
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
          CatrobatZip.ZipCatrobatPackage(fileStream, sourcePath);
        }

        Assert.AreEqual(storage.FileExists("/Projects/TestProjectZipped/test.catroid"), true);
      }
    }
  }
}
