using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.ViewModels.Service;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Service
{
    [TestClass]
    public class UploadProjectRegisterViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void RegisterActionTest()
        {
            //TODO to be tested
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProjectRegisterViewModel);

            var viewModel = new UploadProjectRegisterViewModel
            {
                Username = "TestUser",
                Password = "TestPassword",
                Email = "TestEmail"
            };
            viewModel.GoBackCommand.Execute(null);

            Assert.IsTrue(viewModel.Username == "");
            Assert.IsTrue(viewModel.Password == "");
            Assert.IsTrue(viewModel.Email == "");
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
