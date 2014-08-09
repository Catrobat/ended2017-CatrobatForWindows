using System;
using System.IO;
using Windows.Storage;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.WindowsShared.Services.Storage;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.IDE.WindowsPhone.Tests.Tests.Storage
{
    [TestClass]
    public class StorageStoreTests
    {
        [TestMethod]
        public void DeleteDirectoryTest()
        {
            throw new NotImplementedException();

            //IStorage storage = new StorageStore();

            //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            //{
            //    isolatedStorage.CreateDirectory("TestFolder1");
            //    isolatedStorage.CreateDirectory("TestFolder1/f2");
            //    isolatedStorage.CreateDirectory("TestFolder1/f2/f3");
            //    IsolatedStorageFileStream file1 = isolatedStorage.CreateFile("TestFolder1/f2/f3/file1.txt");
            //    file1.Close();
            //    IsolatedStorageFileStream file2 = isolatedStorage.CreateFile("TestFolder1/f2/file2.bin");
            //    file2.Close();

            //    storage.DeleteDirectory("TestFolder1/f2");

            //    Assert.IsTrue(isolatedStorage.DirectoryExists("TestFolder1"));
            //    Assert.IsFalse(isolatedStorage.DirectoryExists("TestFolder1/f2"));
            //}
        }

        [TestMethod]
        public void DeleteFileTest()
        {
            throw new NotImplementedException();

            //IStorage storage = new StorageStore();

            //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            //{
            //    isolatedStorage.CreateDirectory("TestFolder1");
            //    IsolatedStorageFileStream file1 = isolatedStorage.CreateFile("TestFolder1/file1.txt");
            //    file1.Close();
            //    IsolatedStorageFileStream file2 = isolatedStorage.CreateFile("TestFolder1/file2.bin");
            //    file2.Close();

            //    storage.DeleteFile("TestFolder1/file1.txt");

            //    Assert.IsFalse(isolatedStorage.FileExists("TestFolder1/file1.txt"));
            //    Assert.IsTrue(isolatedStorage.FileExists("TestFolder1/file2.bin"));
            //}
        }

        [TestMethod]
        public void CopyDirectoryTest()
        {
            throw new NotImplementedException();

            //IStorage storage = new StorageStore();

            //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            //{
            //    if (isolatedStorage.DirectoryExists("CopyDirectoryTestFolder1_copy"))
            //        storage.DeleteDirectory("CopyDirectoryTestFolder1_copy");

            //    isolatedStorage.CreateDirectory("CopyDirectoryTestFolder1");
            //    isolatedStorage.CreateDirectory("CopyDirectoryTestFolder1/f2");
            //    isolatedStorage.CreateDirectory("CopyDirectoryTestFolder1/f2/f3");
            //    IsolatedStorageFileStream file1 = isolatedStorage.CreateFile("CopyDirectoryTestFolder1/f2/f3/file1.txt");
            //    file1.Close();
            //    IsolatedStorageFileStream file2 = isolatedStorage.CreateFile("CopyDirectoryTestFolder1/f2/file2.bin");
            //    file2.Close();

            //    storage.CopyDirectory("CopyDirectoryTestFolder1", "CopyDirectoryTestFolder1_copy");

            //    Assert.IsTrue(isolatedStorage.DirectoryExists("CopyDirectoryTestFolder1_copy"));
            //    Assert.IsTrue(isolatedStorage.DirectoryExists("CopyDirectoryTestFolder1_copy/f2"));
            //    Assert.IsTrue(isolatedStorage.DirectoryExists("CopyDirectoryTestFolder1_copy/f2/f3"));

            //    Assert.IsTrue(isolatedStorage.FileExists("CopyDirectoryTestFolder1_copy/f2/file2.bin"));
            //    Assert.IsTrue(isolatedStorage.FileExists("CopyDirectoryTestFolder1_copy/f2/f3/file1.txt"));
            //}
        }


        [TestMethod]
        public void MoveDirectoryTest()
        {
            throw new NotImplementedException();

            //IStorage storage = new StorageStore();

            //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            //{
            //    isolatedStorage.CreateDirectory("MoveDirectoryTestFolder1");
            //    isolatedStorage.CreateDirectory("MoveDirectoryTestFolder1/f2");
            //    isolatedStorage.CreateDirectory("MoveDirectoryTestFolder1/f2/f3");
            //    IsolatedStorageFileStream file1 = isolatedStorage.CreateFile("MoveDirectoryTestFolder1/f2/f3/file1.txt");
            //    file1.Close();
            //    IsolatedStorageFileStream file2 = isolatedStorage.CreateFile("MoveDirectoryTestFolder1/f2/file2.bin");
            //    file2.Close();

            //    storage.MoveDirectory("MoveDirectoryTestFolder1", "MoveDirectoryTestFolder1_moved");

            //    Assert.IsTrue(isolatedStorage.DirectoryExists("MoveDirectoryTestFolder1_moved"));
            //    Assert.IsTrue(isolatedStorage.DirectoryExists("MoveDirectoryTestFolder1_moved/f2"));
            //    Assert.IsTrue(isolatedStorage.DirectoryExists("MoveDirectoryTestFolder1_moved/f2/f3"));

            //    Assert.IsTrue(isolatedStorage.FileExists("MoveDirectoryTestFolder1_moved/f2/file2.bin"));
            //    Assert.IsTrue(isolatedStorage.FileExists("MoveDirectoryTestFolder1_moved/f2/f3/file1.txt"));

            //    Assert.IsFalse(isolatedStorage.DirectoryExists("MoveDirectoryTestFolder1"));
            //}
        }

        [TestMethod]
        public void CopyFileTest()
        {
            throw new NotImplementedException();

            //IStorage storage = new StorageStore();

            //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            //{
            //    isolatedStorage.CreateDirectory("TestFolder1");
            //    IsolatedStorageFileStream file1 = isolatedStorage.CreateFile("TestFolder1/file1.txt");
            //    file1.Close();

            //    storage.CopyFile("TestFolder1/file1.txt", "TestFolder1/file2.txt");

            //    Assert.IsTrue(isolatedStorage.FileExists("TestFolder1/file1.txt"));
            //    Assert.IsTrue(isolatedStorage.FileExists("TestFolder1/file2.txt"));
            //}
        }

        [TestMethod]
        public void MoveFileTest()
        {
            throw new NotImplementedException();

            //IStorage storage = new StorageStore();

            //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            //{
            //    isolatedStorage.CreateDirectory("MoveTestFolder1");
            //    IsolatedStorageFileStream file1 = isolatedStorage.CreateFile("MoveTestFolder1/file1.txt");
            //    file1.Close();

            //    storage.MoveFile("MoveTestFolder1/file1.txt", "MoveTestFolder1/file2.txt");

            //    Assert.IsFalse(isolatedStorage.FileExists("MoveTestFolder1/file1.txt"));
            //    Assert.IsTrue(isolatedStorage.FileExists("MoveTestFolder1/file2.txt"));
            //}
        }

        [TestMethod]
        public void OpenFileTest()
        {
            throw new NotImplementedException();

            //IStorage storage = new StorageStore();

            //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            //{
            //    isolatedStorage.CreateDirectory("TestFolder1");
            //    IsolatedStorageFileStream file1 = isolatedStorage.CreateFile("TestFolder1/file1.txt");
            //    file1.Close();
            //    IsolatedStorageFileStream file2 = isolatedStorage.CreateFile("TestFolder1/file2.txt");
            //    file2.Close();

            //    Stream fileStream1 = storage.OpenFile("TestFolder1/file1.txt", StorageFileMode.OpenOrCreate, StorageFileAccess.ReadWrite);
            //    Assert.IsTrue(fileStream1.CanRead);
            //    Assert.IsTrue(fileStream1.CanWrite);
            //    fileStream1.Close();

            //    Stream fileStream2 = storage.OpenFile("TestFolder1/file1.txt", StorageFileMode.OpenOrCreate, StorageFileAccess.Read);
            //    Assert.IsTrue(fileStream2.CanRead);
            //    Assert.IsFalse(fileStream2.CanWrite);
            //    fileStream2.Close();

            //    Stream fileStream3 = storage.OpenFile("TestFolder1/file1.txt", StorageFileMode.OpenOrCreate, StorageFileAccess.Write);
            //    Assert.IsFalse(fileStream3.CanRead);
            //    Assert.IsTrue(fileStream3.CanWrite);
            //    fileStream3.Close();
            //}
        }

        [TestMethod]
        public void RenameDirectoryTest()
        {
            throw new NotImplementedException();
            //IStorage storage = new StorageStore();

            //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            //{
            //    isolatedStorage.CreateDirectory("RenameDirectoryTestFolder1");
            //    isolatedStorage.CreateDirectory("RenameDirectoryTestFolder1/f2");
            //    isolatedStorage.CreateDirectory("RenameDirectoryTestFolder1/f2/f3");
            //    IsolatedStorageFileStream file1 = isolatedStorage.CreateFile("RenameDirectoryTestFolder1/f2/f3/file1.txt");
            //    file1.Close();
            //    IsolatedStorageFileStream file2 = isolatedStorage.CreateFile("RenameDirectoryTestFolder1/f2/file2.bin");
            //    file2.Close();

            //    storage.RenameDirectory("RenameDirectoryTestFolder1/f2", "renamed2");

            //    Assert.IsTrue(isolatedStorage.DirectoryExists("RenameDirectoryTestFolder1/renamed2"));
            //    Assert.IsTrue(isolatedStorage.DirectoryExists("RenameDirectoryTestFolder1/renamed2/f3"));
            //    Assert.IsTrue(isolatedStorage.FileExists("RenameDirectoryTestFolder1/renamed2/file2.bin"));
            //    Assert.IsTrue(isolatedStorage.FileExists("RenameDirectoryTestFolder1/renamed2/f3/file1.txt"));
            //}
        }

        [TestMethod]
        public async void LoadImageTest()
        {
            var zipService = new ZipService();

            using (var resource = (new ResourceLoaderWindowsShared()).CreateResourceLoader())
            {
                var resourceStream = resource.OpenResourceStream(ResourceScope.TestsPhone, "SampleData/SamplePrograms/test.catroid");
                await zipService.UnzipCatrobatPackageIntoIsolatedStorage(resourceStream, "TestLoadImage");

                using (IStorage storage = new StorageWindowsShared())
                {
                    var image = storage.LoadImage("TestLoadImage/screenshot.png");
                    Assert.IsNotNull(image);
                }
            }
        }

        [TestMethod]
        public async void SaveImageTest()
        {
            var zipService = new ZipService();
            IStorage storage = new StorageWindowsShared();

            using (var resource = (new ResourceLoaderWindowsShared()).CreateResourceLoader())
            {
                var resourceStream = resource.OpenResourceStream(ResourceScope.TestsPhone, "SampleData/SamplePrograms/test.catroid");
                await zipService.UnzipCatrobatPackageIntoIsolatedStorage(resourceStream, "TestLoadImage");

                var image = storage.LoadImage("TestLoadImage/screenshot.png");

                //throw new NotImplementedException("TODO: check next line");
                storage.SaveImage("TestLoadImage2/screenshot.png", image, true, ImageFormat.Png);
                var image2 = storage.LoadImage("TestLoadImage2/screenshot.png");

                // TODO: Maybe check if pixels are corect?

                Assert.IsNotNull(image2);
            }
        }

        [TestMethod]
        public void ReadWriteTextFileTest()
        {
            IStorage storage = new StorageWindowsShared();

            storage.WriteTextFile("test.txt", "test123");
            Assert.AreEqual("test123", storage.ReadTextFile("test.txt"));
        }

        [TestMethod]
        public void ReadWriteSerializableObjectTest()
        {
            IStorage storage = new StorageWindowsShared();

            var settingsWrite = new LocalSettings
            {
                CurrentProjectName = "ProjectName"
            };

            storage.WriteSerializableObject("testobject", settingsWrite);
            var settingsRead = (LocalSettings)storage.ReadSerializableObject("testobject", settingsWrite.GetType());

            Assert.AreEqual("ProjectName", settingsRead.CurrentProjectName);
        }

        [TestMethod]
        public async void CreateFolderTest()
        {
            const string folderName1 = "StorageStoreTests.CreateFolderTest";
            const string folderName2 = "Folder2";
            const string folderName3 = "Folder3";

            var storage = new StorageWindowsShared();
            await storage.CreateFolderPathAsync(Path.Combine(folderName1, folderName2, folderName3));

            var localFolder = ApplicationData.Current.LocalFolder;
            var folder1 = await localFolder.GetFolderAsync(folderName1);
            var folder2 = await folder1.GetFolderAsync(folderName2);
            var folder3 = await folder2.GetFolderAsync(folderName3);

            Assert.IsNotNull(folder3);
        }

        [TestMethod]
        public async void GetFolderTest()
        {
            const string folderName1 = "StorageStoreTests.GetFolderTest";
            const string folderName2 = "Folder2";
            const string folderName3 = "Folder3";

            var storage = new StorageWindowsShared();
            await storage.CreateFolderPathAsync(Path.Combine(folderName1, folderName2, folderName3));

            var localFolder = ApplicationData.Current.LocalFolder;
            await localFolder.CreateFolderAsync(folderName1);
            var folder1 = await localFolder.GetFolderAsync(folderName1);
            await localFolder.CreateFolderAsync(folderName2);
            var folder2 = await localFolder.GetFolderAsync(folderName2);
            await localFolder.CreateFolderAsync(folderName3);
            var folder3 = await localFolder.GetFolderAsync(folderName3);
        }

        [TestMethod]
        public async void GetFileTest()
        {
            IStorage storage = new StorageWindowsShared();


        }
    }
}
