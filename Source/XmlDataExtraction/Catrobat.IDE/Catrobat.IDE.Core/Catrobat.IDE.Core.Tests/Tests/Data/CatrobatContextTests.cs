using System.Threading.Tasks;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Tests.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.Data
{
  [TestClass]
  public class CatrobatContextTests
  {
    [ClassInitialize()]
    public static void TestClassInitialize(TestContext testContext)
    {
      TestHelper.InitializeTests();
    }

    [TestMethod, TestCategory("GatedTests")]
    public void InitializeCatrobatContextTest()
    {
        //TestHelper.InitializeAndClearCatrobatContext();
        //CatrobatContext.SetContextHolder(new ContextHolderTests(new CatrobatContext()));

        //const string newProjectName1 = "DefaultProject";
        //CatrobatContext.GetContext().CreateNewProject(newProjectName1);

        //Assert.AreEqual("DefaultProject", CatrobatContext.GetContext().LocalSettings.CurrentProjectName);

        //CatrobatContext.SetContextHolder(null);
    }

    [TestMethod, TestCategory("GatedTests")]
    public async Task StoreLocalSettingsTest()
    {
        // TODO: fix test
        var localSettings = new LocalSettings
            {
                CurrentLanguageString = "de-DE",
                CurrentProjectName = "DefaultProject",
                CurrentThemeIndex = 1,
                CurrentToken = "tokentoken",
                CurrentUserEmail = "e-mail@gmail.com"
            };

        using (var storage = StorageSystem.GetStorage())
        {
            storage.DeleteFile(CatrobatContextBase.LocalSettingsFilePath);
            Assert.IsFalse(storage.FileExists(CatrobatContextBase.LocalSettingsFilePath));
        }

        await CatrobatContext.StoreLocalSettingsStatic(localSettings);
        using (var storage = StorageSystem.GetStorage())
        {
            Assert.IsTrue(storage.FileExists(CatrobatContextBase.LocalSettingsFilePath));
        }

        var newLocalSetting = await CatrobatContext.RestoreLocalSettingsStatic();
        Assert.AreEqual(localSettings.CurrentLanguageString, newLocalSetting.CurrentLanguageString);
        Assert.AreEqual(localSettings.CurrentProjectName, newLocalSetting.CurrentProjectName);
        Assert.AreEqual(localSettings.CurrentThemeIndex, newLocalSetting.CurrentThemeIndex);
        Assert.AreEqual(localSettings.CurrentToken, newLocalSetting.CurrentToken);
        Assert.AreEqual(localSettings.CurrentUserEmail, newLocalSetting.CurrentUserEmail);
    }
  }
}
