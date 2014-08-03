using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Tests.Misc;
using Catrobat.IDE.Core.Tests.Misc.Storage;
using Catrobat.IDE.Core.Tests.Services.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.Services.Storage
{
    [TestClass]
    public class StorageWindowsStoreTests
    {
        [ClassInitialize]
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

                var fileStream1 = storage.OpenFile("OpenFileTest/file1.txt", StorageFileMode.OpenOrCreate, StorageFileAccess.ReadWrite);
                Assert.IsTrue(fileStream1.CanRead);
                Assert.IsTrue(fileStream1.CanWrite);
                fileStream1.Dispose();

                var fileStream2 = storage.OpenFile("OpenFileTest/file2.txt", StorageFileMode.OpenOrCreate, StorageFileAccess.Read);
                Assert.IsTrue(fileStream2.CanRead);
                Assert.IsFalse(fileStream2.CanWrite);
                fileStream2.Dispose();

                var fileStream3 = storage.OpenFile("OpenFileTest/file2.txt", StorageFileMode.OpenOrCreate, StorageFileAccess.Write);
                Assert.IsFalse(fileStream3.CanRead);
                Assert.IsTrue(fileStream3.CanWrite);
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

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task LoadImageTest()
        {
            var zipService = new ZipService();

            using (IStorage storage = new StorageTest())
            {
                const string basePath = "LoadImageTest/";
                if (Directory.Exists(basePath))
                    Directory.Delete(basePath, true);

                var sampleProjectsPath = BasePathHelper.GetSampleProjectsPath();

                Directory.CreateDirectory(storage.BasePath + basePath);

                using (var resourceLoader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
                {
                    var stream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon,
                                                                      sampleProjectsPath + "test.catroid");
                    await zipService.UnzipCatrobatPackageIntoIsolatedStorage(stream, basePath);
                    stream.Dispose();
                }

                var image = storage.LoadImage("LoadImageTest/screenshot.png");
                Assert.IsNotNull(image);
            }
        }

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
        public async Task ReadWriteSerializableObjectTest()
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var settingsWrite = new LocalSettings { CurrentProjectName = "ProjectName" };

                await storage.WriteSerializableObjectAsync("testobject", settingsWrite);
                var settingsRead = (LocalSettings)storage.ReadSerializableObject("testobject", settingsWrite.GetType());

                Assert.AreEqual("ProjectName", settingsRead.CurrentProjectName);
            }
        }
    }
}
