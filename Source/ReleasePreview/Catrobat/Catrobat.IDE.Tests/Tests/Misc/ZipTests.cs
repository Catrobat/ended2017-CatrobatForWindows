using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Utilities;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
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

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task UnZipSimpleTest()
        {
            TestHelper.InitializeAndClearCatrobatContext();
            const string path = "SampleData/SampleProjects/test.catroid";

            using (var resourceLoader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
            {
                var originalStream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, path);
                await CatrobatZipService.UnzipCatrobatPackageIntoIsolatedStorage(originalStream, "Projects/TestProject");
                originalStream.Close();
                originalStream.Dispose();
            }

            using (IStorage storage = StorageSystem.GetStorage())
            {
                Assert.IsTrue(storage.DirectoryExists("/Projects/TestProject"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject/.nomedia"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject/code.xml"));

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

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task ZipSimpleTest()
        {
            TestHelper.InitializeAndClearCatrobatContext();
            const string path = "SampleData/SampleProjects/test.catroid";

            using (var resourceLoader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
            {
                Stream originalStream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, path);
                await CatrobatZipService.UnzipCatrobatPackageIntoIsolatedStorage(originalStream, "Projects/TestProject");
                originalStream.Close();
                originalStream.Dispose();
            }

            using (IStorage storage = StorageSystem.GetStorage())
            {
                const string writePath = "/Projects/TestProjectZipped/test.catroid";
                const string sourcePath = "Projects/TestProject";

                if (storage.FileExists(writePath))
                    storage.DeleteFile(writePath);

                using (Stream fileStream = storage.OpenFile(writePath, StorageFileMode.Create, StorageFileAccess.Write))
                {
                    await CatrobatZipService.ZipCatrobatPackage(fileStream, sourcePath);
                    fileStream.Close();
                    fileStream.Dispose();
                }

                const string newPath = "/Projects/TestProjectZipped/test.catroid";
                Assert.IsTrue(storage.FileExists(newPath));


                    Stream originalStream = storage.OpenFile(newPath, StorageFileMode.Open, StorageFileAccess.Read);
                    await CatrobatZipService.UnzipCatrobatPackageIntoIsolatedStorage(originalStream, "Projects/TestProject1");
                    originalStream.Close();
                    originalStream.Dispose();



                Assert.IsTrue(storage.DirectoryExists("/Projects/TestProject1"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject1/.nomedia"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject1/code.xml"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject1/screenshot.png"));


                Assert.IsTrue(storage.DirectoryExists("/Projects/TestProject1/images"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject1/images/.nomedia"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject1/images/5A71C6F41035979503BA294F78A09336_background"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject1/images/34A109A82231694B6FE09C216B390570_normalCat"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject1/images/395CD6389BD601812BDB299934A0CCB4_banzaiCat"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject1/images/B4497E87AC34B1329DD9B14C08EEAFF0_cheshireCat"));


                Assert.IsTrue(storage.DirectoryExists("/Projects/TestProject1/sounds"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject1/sounds/.nomedia"));
            }
        }
    }
}
