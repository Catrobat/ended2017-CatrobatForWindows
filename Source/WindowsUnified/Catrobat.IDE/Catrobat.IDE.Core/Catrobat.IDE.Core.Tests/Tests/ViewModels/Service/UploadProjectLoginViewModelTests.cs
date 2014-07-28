using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.Tests.Services.Common;
using Catrobat.IDE.Core.ViewModels.Service;
using System.Globalization;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Service
{
    [TestClass]
    public class UploadProjectLoginViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.UnRegisterAll();
            ServiceLocator.Register<WebCommunicationTest>(TypeCreationMode.Lazy);
            ServiceLocator.Register<CultureServiceTest>(TypeCreationMode.Lazy);
            ServiceLocator.CultureService.SetCulture(new CultureInfo("en"));
        }

        [TestMethod, TestCategory("GatedTests")]
        public void LoginActionTest()
        {
            //TODO check message for too few inputs
            //TODO check messages for different responses - e.g. wrong password
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProjectLoginViewModel);

            var viewModel = new UploadProjectLoginViewModel
            {
                Username = "TestUser",
                Password = "TestPassword",
            };
            var localSettings = new LocalSettings();
            var context = new CatrobatContext
            {
                LocalSettings = localSettings,
                CurrentToken = "",
                CurrentUserName = "",
                CurrentUserEmail = ""
            };
            var messageContext = new GenericMessage<CatrobatContextBase>(context);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.ContextListener);

            viewModel.LoginCommand.Execute(null);

            Assert.IsTrue(viewModel.Username == "");
            Assert.IsTrue(viewModel.Password == "");
            Assert.IsTrue(viewModel.Context.CurrentToken == "TestTokenFromTestSystem_en");
            Assert.IsTrue(viewModel.Context.CurrentUserName == "TestUser");
            Assert.IsTrue(viewModel.Context.CurrentUserEmail == "");
            Assert.IsTrue(viewModel.Context.LocalSettings.CurrentToken == "TestTokenFromTestSystem_en");
            Assert.IsTrue(viewModel.Context.LocalSettings.CurrentUserName == "TestUser");
            Assert.IsTrue(viewModel.Context.LocalSettings.CurrentUserEmail == "");
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(UploadProjectViewModel), navigationService.CurrentView);
            Assert.AreEqual(1, navigationService.PageStackCount);
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
