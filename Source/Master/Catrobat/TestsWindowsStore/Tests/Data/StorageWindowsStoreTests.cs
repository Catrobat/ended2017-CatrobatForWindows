//using System;
//using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

//namespace Catrobat.TestsWindowsStore.Tests.Data
//{
//  [TestClass]
//  public class StorageWindowsStoreTests
//  {
//    [TestMethod]
//    public void DeleteDirectoryTest()
//    {
//      throw new NotImplementedException("Implement for WindowsStore");
//      //TestHelper.InitializeAndClearCatrobatContext();
//      //IStorage storage = new Phone7Storage();

//      //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
//      //{
//      //  isolatedStorage.CreateDirectory("TestFolder1");
//      //  isolatedStorage.CreateDirectory("TestFolder1/f2");
//      //  isolatedStorage.CreateDirectory("TestFolder1/f2/f3");
//      //  IsolatedStorageFileStream file1 = isolatedStorage.CreateFile("TestFolder1/f2/f3/file1.txt");
//      //  file1.Close();
//      //  IsolatedStorageFileStream file2 = isolatedStorage.CreateFile("TestFolder1/f2/file2.bin");
//      //  file2.Close();

//      //  storage.DeleteDirectory("TestFolder1/f2");

//      //  Assert.IsTrue(isolatedStorage.DirectoryExists("TestFolder1"));
//      //  Assert.IsFalse(isolatedStorage.DirectoryExists("TestFolder1/f2"));
//      //}

//      //TestHelper.InitializeAndClearCatrobatContext();
//    }

//    [TestMethod]
//    public void DeleteFileTest()
//    {
//      throw new NotImplementedException("Implement for WindowsStore");
//      //TestHelper.InitializeAndClearCatrobatContext();
//      //IStorage storage = new Phone7Storage();

//      //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
//      //{
//      //  isolatedStorage.CreateDirectory("TestFolder1");
//      //  IsolatedStorageFileStream file1 = isolatedStorage.CreateFile("TestFolder1/file1.txt");
//      //  file1.Close();
//      //  IsolatedStorageFileStream file2 = isolatedStorage.CreateFile("TestFolder1/file2.bin");
//      //  file2.Close();

//      //  storage.DeleteFile("TestFolder1/file1.txt");

//      //  Assert.IsFalse(isolatedStorage.FileExists("TestFolder1/file1.txt"));
//      //  Assert.IsTrue(isolatedStorage.FileExists("TestFolder1/file2.bin"));
//      //}

//      //TestHelper.InitializeAndClearCatrobatContext();
//    }

//    [TestMethod]
//    public void CopyDirectoryTest()
//    {
//      throw new NotImplementedException("Implement for WindowsStore");
//      //TestHelper.InitializeAndClearCatrobatContext();
//      //IStorage storage = new Phone7Storage();

//      //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
//      //{
//      //  isolatedStorage.CreateDirectory("CopyDirectoryTestFolder1");
//      //  isolatedStorage.CreateDirectory("CopyDirectoryTestFolder1/f2");
//      //  isolatedStorage.CreateDirectory("CopyDirectoryTestFolder1/f2/f3");
//      //  IsolatedStorageFileStream file1 = isolatedStorage.CreateFile("CopyDirectoryTestFolder1/f2/f3/file1.txt");
//      //  file1.Close();
//      //  IsolatedStorageFileStream file2 = isolatedStorage.CreateFile("CopyDirectoryTestFolder1/f2/file2.bin");
//      //  file2.Close();

//      //  storage.CopyDirectory("CopyDirectoryTestFolder1", "CopyDirectoryTestFolder1_copy");

//      //  Assert.IsTrue(isolatedStorage.DirectoryExists("CopyDirectoryTestFolder1_copy"));
//      //  Assert.IsTrue(isolatedStorage.DirectoryExists("CopyDirectoryTestFolder1_copy/f2"));
//      //  Assert.IsTrue(isolatedStorage.DirectoryExists("CopyDirectoryTestFolder1_copy/f2/f3"));

//      //  Assert.IsTrue(isolatedStorage.FileExists("CopyDirectoryTestFolder1_copy/f2/file2.bin"));
//      //  Assert.IsTrue(isolatedStorage.FileExists("CopyDirectoryTestFolder1_copy/f2/f3/file1.txt"));
//      //}
//    }

//    [TestMethod]
//    public void CopyFileTest()
//    {
//      throw new NotImplementedException("Implement for WindowsStore");
//      //TestHelper.InitializeAndClearCatrobatContext();
//      //IStorage storage = new Phone7Storage();

//      //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
//      //{
//      //  isolatedStorage.CreateDirectory("TestFolder1");
//      //  IsolatedStorageFileStream file1 = isolatedStorage.CreateFile("TestFolder1/file1.txt");
//      //  file1.Close();

//      //  storage.CopyFile("TestFolder1/file1.txt", "TestFolder1/file2.txt");

//      //  Assert.IsTrue(isolatedStorage.FileExists("TestFolder1/file1.txt"));
//      //  Assert.IsTrue(isolatedStorage.FileExists("TestFolder1/file2.txt"));
//      //}
//    }

//    [TestMethod]
//    public void OpenFileTest()
//    {
//      throw new NotImplementedException("Implement for WindowsStore");
//      //TestHelper.InitializeAndClearCatrobatContext();
//      //IStorage storage = new Phone7Storage();

//      //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
//      //{
//      //  isolatedStorage.CreateDirectory("TestFolder1");
//      //  IsolatedStorageFileStream file1 = isolatedStorage.CreateFile("TestFolder1/file1.txt");
//      //  file1.Close();
//      //  IsolatedStorageFileStream file2 = isolatedStorage.CreateFile("TestFolder1/file2.txt");
//      //  file2.Close();

//      //  Stream fileStream1 = storage.OpenFile("TestFolder1/file1.txt", StorageFileMode.OpenOrCreate, StorageFileAccess.ReadWrite);
//      //  Assert.IsTrue(fileStream1.CanRead);
//      //  Assert.IsTrue(fileStream1.CanWrite);
//      //  fileStream1.Close();

//      //  Stream fileStream2 = storage.OpenFile("TestFolder1/file1.txt", StorageFileMode.OpenOrCreate, StorageFileAccess.Read);
//      //  Assert.IsTrue(fileStream2.CanRead);
//      //  Assert.IsFalse(fileStream2.CanWrite);
//      //  fileStream2.Close();

//      //  Stream fileStream3 = storage.OpenFile("TestFolder1/file1.txt", StorageFileMode.OpenOrCreate, StorageFileAccess.Write);
//      //  Assert.IsFalse(fileStream3.CanRead);
//      //  Assert.IsTrue(fileStream3.CanWrite);
//      //  fileStream3.Close();
//      //}
//    }

//    [TestMethod]
//    public void RenameDirectoryTest()
//    {
//      throw new NotImplementedException("Implement for WindowsStore");
//      //TestHelper.InitializeAndClearCatrobatContext();
//      //IStorage storage = new Phone7Storage();

//      //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
//      //{
//      //  isolatedStorage.CreateDirectory("RenameDirectoryTestFolder1");
//      //  isolatedStorage.CreateDirectory("RenameDirectoryTestFolder1/f2");
//      //  isolatedStorage.CreateDirectory("RenameDirectoryTestFolder1/f2/f3");
//      //  IsolatedStorageFileStream file1 = isolatedStorage.CreateFile("RenameDirectoryTestFolder1/f2/f3/file1.txt");
//      //  file1.Close();
//      //  IsolatedStorageFileStream file2 = isolatedStorage.CreateFile("RenameDirectoryTestFolder1/f2/file2.bin");
//      //  file2.Close();

//      //  storage.RenameDirectory("RenameDirectoryTestFolder1/f2", "renamed2");

//      //  Assert.IsTrue(isolatedStorage.DirectoryExists("RenameDirectoryTestFolder1/renamed2"));
//      //  Assert.IsTrue(isolatedStorage.DirectoryExists("RenameDirectoryTestFolder1/renamed2/f3"));
//      //  Assert.IsTrue(isolatedStorage.FileExists("RenameDirectoryTestFolder1/renamed2/file2.bin"));
//      //  Assert.IsTrue(isolatedStorage.FileExists("RenameDirectoryTestFolder1/renamed2/f3/file1.txt"));
//      //}
//    }

//    [TestMethod]
//    public void LoadImageTest()
//    {
//      throw new NotImplementedException("Implement for WindowsStore");
//      //TestHelper.InitializeAndClearCatrobatContext();
//      //IStorage storage = new Phone7Storage();

//      //string projectPath = "Tests/Data/SampleData/SampleProjects/test.catroid";
//      //Uri uri = new Uri("/MetroCatUT;component/" + projectPath, UriKind.Relative);
//      //var resourceStreamInfo = Application.GetResourceStream(uri);
//      //CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(resourceStreamInfo.Stream, "TestLoadImage");

//      //BitmapImage image = storage.LoadImage("TestLoadImage/screenshot.png");
//      //Assert.IsNotNull(null, image);
//    }

//    [TestMethod]
//    public void SaveImageTest()
//    {
//      throw new NotImplementedException("Implement for WindowsStore");
//      //TestHelper.InitializeAndClearCatrobatContext();
//      //IStorage storage = new Phone7Storage();

//      //string projectPath = "Tests/Data/SampleData/SampleProjects/test.catroid";
//      //Uri uri = new Uri("/MetroCatUT;component/" + projectPath, UriKind.Relative);
//      //var resourceStreamInfo = Application.GetResourceStream(uri);
//      //CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(resourceStreamInfo.Stream, "TestLoadImage");

//      //BitmapImage image = storage.LoadImage("TestLoadImage/screenshot.png");
//      //storage.SaveImage("TestLoadImage2/screenshot.png", image);
//      //BitmapImage image2 = storage.LoadImage("TestLoadImage2/screenshot.png");

//      //// TODO: Maybe check if pixels are corect?

//      //Assert.IsNotNull(null, image);
//    }

//    [TestMethod]
//    public void ReadWriteTextFileTest()
//    {
//      throw new NotImplementedException("Implement for WindowsStore");
//      //TestHelper.InitializeAndClearCatrobatContext();
//      //IStorage storage = new Phone7Storage();

//      //storage.WriteTextFile("test.txt", "test123");
//      //Assert.AreEqual("test123", storage.ReadTextFile("test.txt")) ;
//    }

//    [TestMethod]
//    public void ReadWriteSerializableObjectTest()
//    {
//      throw new NotImplementedException("Implement for WindowsStore");
//      //TestHelper.InitializeAndClearCatrobatContext();
//      //IStorage storage = new Phone7Storage();

//      //LocalSettings settingsWrite = new LocalSettings();
//      //settingsWrite.CurrentProjectName = "ProjectName";

//      //storage.WriteSerializableObject("testobject", settingsWrite);
//      //LocalSettings settingsRead = (LocalSettings) storage.ReadSerializableObject("testobject", settingsWrite.GetType());

//      //Assert.AreEqual("ProjectName", settingsRead.CurrentProjectName);
//    }
//  }
//}
