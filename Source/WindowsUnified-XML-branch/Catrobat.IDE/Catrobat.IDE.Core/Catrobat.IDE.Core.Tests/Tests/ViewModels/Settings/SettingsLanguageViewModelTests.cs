using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Settings;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Settings
{
    [TestClass]
    public class SettingsLanguageViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
        }

        [TestMethod, TestCategory("ViewModels.Settings"), TestCategory("ExcludeGated")]
        public void CultureTest()
        {
            // test culture info
            Assert.AreEqual(0, "test not implemented");
        }
    }
}
