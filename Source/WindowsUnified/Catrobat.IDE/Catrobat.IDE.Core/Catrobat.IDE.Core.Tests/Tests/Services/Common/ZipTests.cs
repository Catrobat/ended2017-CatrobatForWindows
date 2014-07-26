using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Tests.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.Services.Common
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
            var zipService = new ZipService();

            TestHelper.InitializeAndClearCatrobatContext();
            const string path = "SampleData/SampleProjects/test.catroid";

            using (var resourceLoader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
            {
                var originalStream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, path);
                await zipService.UnzipCatrobatPackageIntoIsolatedStorage(
                    originalStream, "Projects/TestProject");
                originalStream.Dispose();
            }

            using (var storage = StorageSystem.GetStorage())
            {
                Assert.IsTrue(storage.DirectoryExists("/Projects/TestProject"));

                Assert.IsFalse(storage.FileExists("/Projects/TestProject/.nomedia"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject/code.xml"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject/screenshot.png"));


                Assert.IsTrue(storage.DirectoryExists("/Projects/TestProject/images"));

                Assert.IsFalse(storage.FileExists("/Projects/TestProject/images/.nomedia"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject/images/5A71C6F41035979503BA294F78A09336_background"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject/images/34A109A82231694B6FE09C216B390570_normalCat"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject/images/395CD6389BD601812BDB299934A0CCB4_banzaiCat"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject/images/B4497E87AC34B1329DD9B14C08EEAFF0_cheshireCat"));


                Assert.IsFalse(storage.DirectoryExists("/Projects/TestProject/sounds"));

                Assert.IsFalse(storage.FileExists("/Projects/TestProject/sounds/.nomedia"));
            }
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task ZipSimpleTest()
        {
            var zipService = new ZipService();

            TestHelper.InitializeAndClearCatrobatContext();
            const string path = "SampleData/SampleProjects/test.catroid";

            using (var resourceLoader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
            {
                Stream originalStream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, path);
                await zipService.UnzipCatrobatPackageIntoIsolatedStorage(originalStream, "Projects/TestProject");
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
                    await zipService.ZipCatrobatPackage(fileStream, sourcePath);
                    fileStream.Dispose();
                }

                const string newPath = "/Projects/TestProjectZipped/test.catroid";
                Assert.IsTrue(storage.FileExists(newPath));


                Stream originalStream = storage.OpenFile(newPath, StorageFileMode.Open, StorageFileAccess.Read);
                await zipService.UnzipCatrobatPackageIntoIsolatedStorage(originalStream, "Projects/TestProject1");
                originalStream.Dispose();



                Assert.IsTrue(storage.DirectoryExists("/Projects/TestProject1"));

                Assert.IsFalse(storage.FileExists("/Projects/TestProject1/.nomedia"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject1/code.xml"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject1/screenshot.png"));


                Assert.IsTrue(storage.DirectoryExists("/Projects/TestProject1/images"));

                Assert.IsFalse(storage.FileExists("/Projects/TestProject1/images/.nomedia"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject1/images/5A71C6F41035979503BA294F78A09336_background"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject1/images/34A109A82231694B6FE09C216B390570_normalCat"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject1/images/395CD6389BD601812BDB299934A0CCB4_banzaiCat"));

                Assert.IsTrue(storage.FileExists("/Projects/TestProject1/images/B4497E87AC34B1329DD9B14C08EEAFF0_cheshireCat"));


                Assert.IsFalse(storage.DirectoryExists("/Projects/TestProject1/sounds"));

                Assert.IsFalse(storage.FileExists("/Projects/TestProject1/sounds/.nomedia"));
            }
        }
    }
}
