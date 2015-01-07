using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Tests.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Tests.Extensions;

namespace Catrobat.IDE.Core.Tests.Tests.Services.Common
{
    [TestClass]
    public class ContextServiceTests
    {
        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod, TestCategory("Services")]
        public async Task StoreLocalSettingsTest()
        {
            var localSettings = new LocalSettings
            {
                CurrentLanguageString = "de-DE",
                CurrentProgramName = "DefaultProject",
                CurrentThemeIndex = 1,
                CurrentToken = "tokentoken",
                CurrentUserEmail = "e-mail@gmail.com"
            };

            using (var storage = StorageSystem.GetStorage())
            {
                storage.DeleteFile(StorageConstants.LocalSettingsFilePath);
            }

            await ServiceLocator.ContextService.StoreLocalSettings(localSettings);
            using (var storage = StorageSystem.GetStorage())
            {
                Assert.IsTrue(storage.FileExists(StorageConstants.LocalSettingsFilePath));
            }

            var newLocalSetting = await ServiceLocator.ContextService.RestoreLocalSettings();
            Assert.AreEqual(localSettings.CurrentLanguageString, newLocalSetting.CurrentLanguageString);
            Assert.AreEqual(localSettings.CurrentProgramName, newLocalSetting.CurrentProgramName);
            Assert.AreEqual(localSettings.CurrentThemeIndex, newLocalSetting.CurrentThemeIndex);
            Assert.AreEqual(localSettings.CurrentToken, newLocalSetting.CurrentToken);
            Assert.AreEqual(localSettings.CurrentUserEmail, newLocalSetting.CurrentUserEmail);
        }

        // TODO: add more tests
    }
}
