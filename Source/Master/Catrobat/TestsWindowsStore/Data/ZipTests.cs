using System;
using Catrobat.Core.ZIP;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.UI.Xaml;

namespace TestsWindowsStore.Data
{
  [TestClass]
  public class ZipTests
  {
    [TestMethod]
    public void UnZipSimpleTest()
    {
      throw new NotImplementedException("Implement for WindowsStore");

      //TestHelper.InitializeAndClearCatrobatContext();
      //string path = "Tests/Data/SampleData/SampleProjects/test.catroid";
      //Uri uri = new Uri("/MetroCatUT;component/" + path, UriKind.Relative);
      //var resourceStreamInfo = Application.GetResourceStream(uri);
      //CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(resourceStreamInfo.Stream, "Projects/TestProject");

      //IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();

      //Assert.AreEqual(isolatedStorage.DirectoryExists("/Projects/TestProject"), true);

      //Assert.AreEqual(isolatedStorage.FileExists("/Projects/TestProject/.nomedia"), true);

      //Assert.AreEqual(isolatedStorage.FileExists("/Projects/TestProject/projectcode.xml"), true);

      //Assert.AreEqual(isolatedStorage.FileExists("/Projects/TestProject/screenshot.png"), true);



      //Assert.AreEqual(isolatedStorage.DirectoryExists("/Projects/TestProject/images"), true);

      //Assert.AreEqual(isolatedStorage.FileExists("/Projects/TestProject/images/.nomedia"), true);

      //Assert.AreEqual(isolatedStorage.FileExists("/Projects/TestProject/images/5A71C6F41035979503BA294F78A09336_background"), true);

      //Assert.AreEqual(isolatedStorage.FileExists("/Projects/TestProject/images/34A109A82231694B6FE09C216B390570_normalCat"), true);

      //Assert.AreEqual(isolatedStorage.FileExists("/Projects/TestProject/images/395CD6389BD601812BDB299934A0CCB4_banzaiCat"), true);

      //Assert.AreEqual(isolatedStorage.FileExists("/Projects/TestProject/images/B4497E87AC34B1329DD9B14C08EEAFF0_cheshireCat"), true);
      


      //Assert.AreEqual(isolatedStorage.DirectoryExists("/Projects/TestProject/sounds"), true);

      //Assert.AreEqual(isolatedStorage.FileExists("/Projects/TestProject/sounds/.nomedia"), true);
    }

    [TestMethod]
    public void ZipSimpleTest()
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //TestHelper.InitializeAndClearCatrobatContext();
      //string path = "Tests/Data/SampleData/SampleProjects/test.catroid";
      //Uri uri = new Uri("/MetroCatUT;component/" + path, UriKind.Relative);
      //var resourceStreamInfo = Application.GetResourceStream(uri);
      //CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(resourceStreamInfo.Stream, "Projects/TestProject");

      //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
      //{
      //  isolatedStorage.CreateDirectory("/Projects");
      //  isolatedStorage.CreateDirectory("/Projects/TestProjectZipped");

      //  string writePath = "/Projects/TestProjectZipped/test.catroid";
      //  string sourcePath = "Projects/TestProject";

      //  if (isolatedStorage.FileExists(writePath))
      //    isolatedStorage.DeleteFile(writePath);
        
      //  IsolatedStorageFileStream fileStream = isolatedStorage.CreateFile(writePath);
      //  CatrobatZip.ZipCatrobatPackage(fileStream, sourcePath);
      //  fileStream.Close();

      //  Assert.AreEqual(isolatedStorage.FileExists("/Projects/TestProjectZipped/test.catroid"), true);
      //}
    }
  }
}
