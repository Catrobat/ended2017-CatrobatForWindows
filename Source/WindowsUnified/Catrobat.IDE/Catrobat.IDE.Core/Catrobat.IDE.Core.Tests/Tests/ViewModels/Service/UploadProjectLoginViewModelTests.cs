using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.ViewModels.Service;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Service
{
    [TestClass]
    public class UploadProjectLoginViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void LoginActionTest()
        {
            //TODO to be tested
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod, TestCategory("GatedTests")]
        public void ForgottenActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProjectLoginViewModel);

            var viewModel = new UploadProjectLoginViewModel
            {
                Username = "TestUser",
                Password = "TestPassword",
            };
            viewModel.ForgottenCommand.Execute(null);

            Assert.IsTrue(viewModel.Username == "");
            Assert.IsTrue(viewModel.Password == "");
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(UploadProjectForgotPasswordViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void RegisterActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProjectLoginViewModel);

            var viewModel = new UploadProjectLoginViewModel
            {
                Username = "TestUser",
                Password = "TestPassword",
            };
            viewModel.RegisterCommand.Execute(null);

            Assert.IsTrue(viewModel.Username == "");
            Assert.IsTrue(viewModel.Password == "");
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(UploadProjectRegisterViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProjectLoginViewModel);

            var viewModel = new UploadProjectLoginViewModel
            {
                Username = "TestUser",
                Password = "TestPassword",
            };
            viewModel.GoBackCommand.Execute(null);

            Assert.IsTrue(viewModel.Username == "");
            Assert.IsTrue(viewModel.Password == "");
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
