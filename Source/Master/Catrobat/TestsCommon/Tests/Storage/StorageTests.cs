using System;
using System.IO;
using System.IO.IsolatedStorage;
using Catrobat.Core.Storage;
using Catrobat.TestsCommon.Misc;
using Catrobat.TestsCommon.Misc.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.TestsCommon.Tests.Storage
{
  [TestClass]
  public class StorageWindowsStoreTests
  {
    [ClassInitialize()]
    public static void TestClassInitialize(TestContext testContext)
    {
      TestHelper.InitializeTests();
    }


    [TestMethod]
    public void DeleteDirectoryTest()
    {
      throw new NotImplementedException("Implement for TestStorage");
      //TestHelper.InitializeAndClearCatrobatContext();
      //IStorage storage = new Phone7Storage();

      //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
      //{
      //  isolatedStorage.CreateDirectory("TestFolder1");
      //  isolatedStorage.CreateDirectory("TestFolder1/f2");
      //  isolatedStorage.CreateDirectory("TestFolder1/f2/f3");
      //  IsolatedStorageFileStream file1 = isolatedStorage.CreateFile("TestFolder1/f2/f3/file1.txt");
      //  file1.Close();
      //  IsolatedStorageFileStream file2 = isolatedStorage.CreateFile("TestFolder1/f2/file2.bin");
      //  file2.Close();

      //  storage.DeleteDirectory("TestFolder1/f2");

      //  Assert.IsTrue(isolatedStorage.DirectoryExists("TestFolder1"));
      //  Assert.IsFalse(isolatedStorage.DirectoryExists("TestFolder1/f2"));
      //}

      //TestHelper.InitializeAndClearCatrobatContext();
    }

    [TestMethod]
    public void DeleteFileTest()
    {
      throw new NotImplementedException("Implement for TestStorage");
      //TestHelper.InitializeAndClearCatrobatContext();
      //IStorage storage = new Phone7Storage();

      //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
      //{
      //  isolatedStorage.CreateDirectory("TestFolder1");
      //  IsolatedStorageFileStream file1 = isolatedStorage.CreateFile("TestFolder1/file1.txt");
      //  file1.Close();
      //  IsolatedStorageFileStream file2 = isolatedStorage.CreateFile("TestFolder1/file2.bin");
      //  file2.Close();

      //  storage.DeleteFile("TestFolder1/file1.txt");

      //  Assert.IsFalse(isolatedStorage.FileExists("TestFolder1/file1.txt"));
      //  Assert.IsTrue(isolatedStorage.FileExists("TestFolder1/file2.bin"));
      //}

      //TestHelper.InitializeAndClearCatrobatContext();
    }

    [TestMethod]
    public void CopyDirectoryTest()
    {
      IStorage storage = new StorageTest();
      var basePath = storage.BasePath;

      //Directory.CreateDirectory(basePath + "CopyDirectoryTestFolder1");
      //Directory.CreateDirectory(basePath + "CopyDirectoryTestFolder1/f2");
      Directory.CreateDirectory(basePath + "CopyDirectoryTestFolder1/f2/f3");

      var fileStream1 = File.Create(basePath + "CopyDirectoryTestFolder1/f2/f3/file1.txt");
      fileStream1.Close();

      var fileStream2 = File.Create(basePath + "CopyDirectoryTestFolder1/f2/file2.bin");
      fileStream2.Close();

      storage.CopyDirectory("CopyDirectoryTestFolder1", "CopyDirectoryTestFolder1_copy");

      Assert.IsTrue(Directory.Exists(basePath + "CopyDirectoryTestFolder1_copy"));
      Assert.IsTrue(Directory.Exists(basePath + "CopyDirectoryTestFolder1_copy/f2"));
      Assert.IsTrue(Directory.Exists(basePath + "CopyDirectoryTestFolder1_copy/f2/f3"));

      Assert.IsTrue(File.Exists(basePath + "CopyDirectoryTestFolder1_copy/f2/file2.bin"));
      Assert.IsTrue(File.Exists(basePath + "CopyDirectoryTestFolder1_copy/f2/f3/file1.txt"));
    }

    [TestMethod]
    public void CopyFileTest()
    {
      throw new NotImplementedException("Implement for TestStorage");
      //TestHelper.InitializeAndClearCatrobatContext();
      //IStorage storage = new Phone7Storage();

      //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
      //{
      //  isolatedStorage.CreateDirectory("TestFolder1");
      //  IsolatedStorageFileStream file1 = isolatedStorage.CreateFile("TestFolder1/file1.txt");
      //  file1.Close();

      //  storage.CopyFile("TestFolder1/file1.txt", "TestFolder1/file2.txt");

      //  Assert.IsTrue(isolatedStorage.FileExists("TestFolder1/file1.txt"));
      //  Assert.IsTrue(isolatedStorage.FileExists("TestFolder1/file2.txt"));
      //}
    }

    [TestMethod]
    public void OpenFileTest()
    {
      throw new NotImplementedException("Implement for TestStorage");
      //TestHelper.InitializeAndClearCatrobatContext();
      //IStorage storage = new Phone7Storage();

      //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
      //{
      //  isolatedStorage.CreateDirectory("TestFolder1");
      //  IsolatedStorageFileStream file1 = isolatedStorage.CreateFile("TestFolder1/file1.txt");
      //  file1.Close();
      //  IsolatedStorageFileStream file2 = isolatedStorage.CreateFile("TestFolder1/file2.txt");
      //  file2.Close();

      //  Stream fileStream1 = storage.OpenFile("TestFolder1/file1.txt", StorageFileMode.OpenOrCreate, StorageFileAccess.ReadWrite);
      //  Assert.IsTrue(fileStream1.CanRead);
      //  Assert.IsTrue(fileStream1.CanWrite);
      //  fileStream1.Close();

      //  Stream fileStream2 = storage.OpenFile("TestFolder1/file1.txt", StorageFileMode.OpenOrCreate, StorageFileAccess.Read);
      //  Assert.IsTrue(fileStream2.CanRead);
      //  Assert.IsFalse(fileStream2.CanWrite);
      //  fileStream2.Close();

      //  Stream fileStream3 = storage.OpenFile("TestFolder1/file1.txt", StorageFileMode.OpenOrCreate, StorageFileAccess.Write);
      //  Assert.IsFalse(fileStream3.CanRead);
      //  Assert.IsTrue(fileStream3.CanWrite);
      //  fileStream3.Close();
      //}
    }

    [TestMethod]
    public void RenameDirectoryTest()
    {
      throw new NotImplementedException("Implement for TestStorage");
      //TestHelper.InitializeAndClearCatrobatContext();
      //IStorage storage = new Phone7Storage();

      //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
      //{
      //  isolatedStorage.CreateDirectory("RenameDirectoryTestFolder1");
      //  isolatedStorage.CreateDirectory("RenameDirectoryTestFolder1/f2");
      //  isolatedStorage.CreateDirectory("RenameDirectoryTestFolder1/f2/f3");
      //  IsolatedStorageFileStream file1 = isolatedStorage.CreateFile("RenameDirectoryTestFolder1/f2/f3/file1.txt");
      //  file1.Close();
      //  IsolatedStorageFileStream file2 = isolatedStorage.CreateFile("RenameDirectoryTestFolder1/f2/file2.bin");
      //  file2.Close();

      //  storage.RenameDirectory("RenameDirectoryTestFolder1/f2", "renamed2");

      //  Assert.IsTrue(isolatedStorage.DirectoryExists("RenameDirectoryTestFolder1/renamed2"));
      //  Assert.IsTrue(isolatedStorage.DirectoryExists("RenameDirectoryTestFolder1/renamed2/f3"));
      //  Assert.IsTrue(isolatedStorage.FileExists("RenameDirectoryTestFolder1/renamed2/file2.bin"));
      //  Assert.IsTrue(isolatedStorage.FileExists("RenameDirectoryTestFolder1/renamed2/f3/file1.txt"));
      //}
    }

    [TestMethod]
    public void LoadImageTest()
    {
      throw new NotImplementedException("Implement for TestStorage");
      //TestHelper.InitializeAndClearCatrobatContext();
      //IStorage storage = new Phone7Storage();

      //string projectPath = "Tests/Data/SampleData/SampleProjects/test.catroid";
      //Uri uri = new Uri("/MetroCatUT;component/" + projectPath, UriKind.Relative);
      //var resourceStreamInfo = Application.GetResourceStream(uri);
      //CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(resourceStreamInfo.Stream, "TestLoadImage");

      //BitmapImage image = storage.LoadImage("TestLoadImage/screenshot.png");
      //Assert.AreNotEqual(image, null);
    }

    [TestMethod]
    public void SaveImageTest()
    {
      throw new NotImplementedException("Implement for TestStorage");
      //TestHelper.InitializeAndClearCatrobatContext();
      //IStorage storage = new Phone7Storage();

      //string projectPath = "Tests/Data/SampleData/SampleProjects/test.catroid";
      //Uri uri = new Uri("/MetroCatUT;component/" + projectPath, UriKind.Relative);
      //var resourceStreamInfo = Application.GetResourceStream(uri);
      //CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(resourceStreamInfo.Stream, "TestLoadImage");

      //BitmapImage image = storage.LoadImage("TestLoadImage/screenshot.png");
      //storage.SaveImage("TestLoadImage2/screenshot.png", image);
      //BitmapImage image2 = storage.LoadImage("TestLoadImage2/screenshot.png");

      //// TODO: Maybe check if pixels are corect?

      //Assert.AreNotEqual(image, null);
    }

    [TestMethod]
    public void ReadWriteTextFileTest()
    {
      throw new NotImplementedException("Implement for TestStorage");
      //TestHelper.InitializeAndClearCatrobatContext();
      //IStorage storage = new Phone7Storage();

      //storage.WriteTextFile("test.txt", "test123");
      //Assert.AreEqual(storage.ReadTextFile("test.txt"), "test123") ;
    }

    [TestMethod]
    public void ReadWriteSerializableObjectTest()
    {
      throw new NotImplementedException("Implement for TestStorage");
      //TestHelper.InitializeAndClearCatrobatContext();
      //IStorage storage = new Phone7Storage();

      //LocalSettings settingsWrite = new LocalSettings();
      //settingsWrite.CurrentProjectName = "ProjectName";

      //storage.WriteSerializableObject("testobject", settingsWrite);
      //LocalSettings settingsRead = (LocalSettings) storage.ReadSerializableObject("testobject", settingsWrite.GetType());

      //Assert.AreEqual(settingsRead.CurrentProjectName, "ProjectName");
    }
  }
}
