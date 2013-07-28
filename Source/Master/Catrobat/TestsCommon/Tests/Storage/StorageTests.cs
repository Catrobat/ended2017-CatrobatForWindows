using System;
using System.IO;
using System.IO.IsolatedStorage;
using Catrobat.Core.Storage;
using Catrobat.Core.ZIP;
using Catrobat.TestsCommon.Misc;
using Catrobat.TestsCommon.Misc.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using Catrobat.Core;

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
      using (IStorage storage = new StorageTest())
      {
        var basePath = storage.BasePath + "DeleteDirectoryTest/";

        if (Directory.Exists(basePath))
          Directory.Delete(basePath, true);
        Directory.CreateDirectory(basePath);

        Directory.CreateDirectory(basePath + "Folder1/F2/F3");

        var file1 = File.Create(basePath + "Folder1/F2/F3/file1.txt");
        file1.Close();
        file1.Dispose();

        var file2 = File.Create(basePath + "Folder1/F2/F3/file2.bin");
        file2.Close();
        file2.Dispose();

        storage.DeleteDirectory("DeleteDirectoryTest/Folder1/F2");

        Assert.IsTrue(Directory.Exists(basePath + "Folder1"));
        Assert.IsFalse(Directory.Exists(basePath + "Folder1/F2"));
      }
    }

    [TestMethod]
    public void DeleteFileTest()
    {
      using (IStorage storage = new StorageTest())
      {
        var basePath = storage.BasePath + "DeleteFileTest/";
        if (Directory.Exists(basePath))
          Directory.Delete(basePath, true);

        Directory.CreateDirectory(basePath);

        var file1 = File.Create(basePath + "file1.txt");
        file1.Close();
        file1.Dispose();

        var file2 = File.Create(basePath + "file2.bin");
        file2.Close();
        file2.Dispose();

        storage.DeleteFile("DeleteFileTest/file1.txt");

        Assert.IsFalse(File.Exists(basePath + "file1.txt"));
        Assert.IsTrue(File.Exists(basePath + "file2.bin"));
      }
    }

    [TestMethod]
    public void CopyDirectoryTest()
    {
      using (IStorage storage = new StorageTest())
      {
        var basePath = storage.BasePath + "CopyDirectoryTest/";
        if (Directory.Exists(basePath))
          Directory.Delete(basePath, true);

        Directory.CreateDirectory(basePath + "CopyDirectoryTestFolder1/f2/f3");

        var file1 = File.Create(basePath + "CopyDirectoryTestFolder1/f2/f3/file1.txt");
        file1.Close();
        file1.Dispose();

        var file2 = File.Create(basePath + "CopyDirectoryTestFolder1/f2/file2.bin");
        file2.Close();
        file2.Dispose();

        storage.CopyDirectory("CopyDirectoryTest/CopyDirectoryTestFolder1", "CopyDirectoryTest/CopyDirectoryTestFolder1_copy");

        Assert.IsTrue(Directory.Exists(basePath + "CopyDirectoryTestFolder1_copy"));
        Assert.IsTrue(Directory.Exists(basePath + "CopyDirectoryTestFolder1_copy/f2"));
        Assert.IsTrue(Directory.Exists(basePath + "CopyDirectoryTestFolder1_copy/f2/f3"));

        Assert.IsTrue(File.Exists(basePath + "CopyDirectoryTestFolder1_copy/f2/file2.bin"));
        Assert.IsTrue(File.Exists(basePath + "CopyDirectoryTestFolder1_copy/f2/f3/file1.txt"));
      }
    }

    [TestMethod]
    public void CopyFileTest()
    {
      using (IStorage storage = new StorageTest())
      {
        var basePath = storage.BasePath + "CopyFileTest/";
        if (Directory.Exists(basePath))
          Directory.Delete(basePath, true);

        Directory.CreateDirectory(basePath);

        File.WriteAllText(basePath + "file1.txt", "TESTING\n123");

        storage.CopyFile("CopyFileTest/file1.txt", "CopyFileTest/Copy/copied_file1.txt");
        storage.CopyFile("CopyFileTest/file1.txt", "CopyFileTest/copied_file2.txt");

        Assert.IsTrue(Directory.Exists(basePath + "Copy/"));

        Assert.IsTrue(File.Exists(basePath + "file1.txt"));
        Assert.IsTrue(File.Exists(basePath + "Copy/copied_file1.txt"));
        Assert.IsTrue(File.Exists(basePath + "copied_file2.txt"));

        string file1Content = File.ReadAllText((basePath + "file1.txt"), System.Text.Encoding.UTF8);
        string copiedFile1Content = File.ReadAllText((basePath + "Copy/copied_file1.txt"), System.Text.Encoding.UTF8);
        string copiedFile2Content = File.ReadAllText((basePath + "copied_file2.txt"), System.Text.Encoding.UTF8);

        Assert.AreEqual(file1Content, copiedFile1Content);
        Assert.AreEqual(file1Content, copiedFile2Content);
      }
    }

    [TestMethod]
    public void OpenFileTest()
    {
      using (IStorage storage = new StorageTest())
      {
        var basePath = storage.BasePath + "OpenFileTest/";
        if (Directory.Exists(basePath))
          Directory.Delete(basePath, true);

        Directory.CreateDirectory(basePath);

        var file1 = File.Create(basePath + "file1.txt");
        file1.Close();
        file1.Dispose();

        Stream fileStream1 = storage.OpenFile("OpenFileTest/file1.txt", StorageFileMode.OpenOrCreate, StorageFileAccess.ReadWrite);
        Assert.IsTrue(fileStream1.CanRead);
        Assert.IsTrue(fileStream1.CanWrite);
        fileStream1.Close();
        fileStream1.Dispose();

        Stream fileStream2 = storage.OpenFile("OpenFileTest/file2.txt", StorageFileMode.OpenOrCreate, StorageFileAccess.Read);
        Assert.IsTrue(fileStream2.CanRead);
        Assert.IsFalse(fileStream2.CanWrite);
        fileStream2.Close();
        fileStream2.Dispose();

        Stream fileStream3 = storage.OpenFile("OpenFileTest/file2.txt", StorageFileMode.OpenOrCreate, StorageFileAccess.Write);
        Assert.IsFalse(fileStream3.CanRead);
        Assert.IsTrue(fileStream3.CanWrite);
        fileStream3.Close();
        fileStream3.Dispose();
      }
    }

    [TestMethod]
    public void RenameDirectoryTest()
    {
      using (IStorage storage = new StorageTest())
      {
        var basePath = storage.BasePath + "RenameDirectoryTest/";
        if (Directory.Exists(basePath))
          Directory.Delete(basePath, true);

        Directory.CreateDirectory(basePath);

        Directory.CreateDirectory(basePath + "Folder1/F2/F3");

        var file1 = File.Create(basePath + "Folder1/F2/F3/file1.txt");
        file1.Close();
        file1.Dispose();

        var file2 = File.Create(basePath + "Folder1/F2/file2.bin");
        file2.Close();
        file2.Dispose();

        storage.RenameDirectory("RenameDirectoryTest/Folder1/F2", "renamed2");

        Assert.IsTrue(Directory.Exists(basePath + "Folder1/renamed2"));
        Assert.IsTrue(Directory.Exists(basePath + "Folder1/renamed2/F3"));
        Assert.IsTrue(File.Exists(basePath + "Folder1/renamed2/file2.bin"));
        Assert.IsTrue(File.Exists(basePath + "Folder1/renamed2/F3/file1.txt"));
      }
    }

    [TestMethod]
    public void LoadImageTest()
    {
      using (IStorage storage = new StorageTest())
      {
        var basePath = "LoadImageTest/";
        if (Directory.Exists(basePath))
          Directory.Delete(basePath, true);

        var sampleProjectsPath = BasePathHelper.GetSampleProjectsPath();

        Directory.CreateDirectory(storage.BasePath + basePath);

        using (var resourceLoader = ResourceLoader.CreateResourceLoader())
        {
          Stream stream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon,
                                                            sampleProjectsPath + "test.catroid");
          CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(stream, basePath);
          stream.Close();
          stream.Dispose();
        }

        var image = storage.LoadImage("LoadImageTest/screenshot.png");
        Assert.IsNotNull(image);
      }
    }

    //[TestMethod]
    //public void SaveImageTest()
    //{
    //  using (IStorage storage = new StorageTest())
    //  {
    //    var basePath = storage.BasePath + "SaveImageTest/";
    //    var sampleProjectsPath = BasePathHelper.GetSampleProjectsPath();

    //    Directory.CreateDirectory(basePath);

    //    using (var resourceLoader = ResourceLoader.CreateResourceLoader())
    //    {
    //        Stream stream = resourceLoader.OpenResourceStream((Projects.TestCommon, sampleProjectsPath + "test.catroid");
    //        CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(stream, basePath);
    //        stream.Close();
    //        stream.Dispose();
    //    }

    //    var image = storage.LoadImage("LoadImageTest/screenshot.png");
    //    storage.SaveImage("TestLoadImage2/screenshot.png", image);
    //    BitmapImage image2 = storage.LoadImage("TestLoadImage2/screenshot.png");

    //     TODO: Maybe check if pixels are corect?

    //    Assert.IsNotNull(image);
    //  }
    //}

    [TestMethod]
    public void ReadWriteTextFileTest()
    {
      using (IStorage storage = new StorageTest())
      {
        var basePath = storage.BasePath + "ReadWriteTextFileTest/";
        if (Directory.Exists(basePath))
          Directory.Delete(basePath, true);

        Directory.CreateDirectory(basePath);

        storage.WriteTextFile("ReadWriteTextFileTest/test.txt", "test123");
        Assert.AreEqual("test123", storage.ReadTextFile("ReadWriteTextFileTest/test.txt"));
      }
    }

    [TestMethod]
    public void ReadWriteSerializableObjectTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();

      using (var storage = StorageSystem.GetStorage())
      {
        LocalSettings settingsWrite = new LocalSettings();
        settingsWrite.CurrentProjectName = "ProjectName";

        storage.WriteSerializableObject("testobject", settingsWrite);
        LocalSettings settingsRead = (LocalSettings)storage.ReadSerializableObject("testobject", settingsWrite.GetType());

        Assert.AreEqual("ProjectName", settingsRead.CurrentProjectName);
      }
    }
  }
}
