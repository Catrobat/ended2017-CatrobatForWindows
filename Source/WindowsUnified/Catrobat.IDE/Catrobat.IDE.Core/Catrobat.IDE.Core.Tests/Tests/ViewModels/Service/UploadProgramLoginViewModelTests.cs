using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.Tests.Services.Common;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Service;
using System.Globalization;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Service
{
    [TestClass]
    public class UploadProgramLoginViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.UnRegisterAll();
            ServiceLocator.Register<NotificationServiceTest>(TypeCreationMode.Lazy);
            ServiceLocator.Register<WebCommunicationTest>(TypeCreationMode.Lazy);
            ServiceLocator.Register<CultureServiceTest>(TypeCreationMode.Lazy);
            ServiceLocator.CultureService.SetCulture(new CultureInfo("en"));
        }

        [TestMethod, TestCategory("GatedTests")]
        public void LoginActionTest()
        {
            //TODO check messages for different responses - e.g. wrong password or http-request failed
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProgramLoginViewModel);

            var viewModel = new UploadProgramLoginViewModel
            {
                Username = "TestUser",
                Password = "TestPassword"
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

            Assert.AreEqual("", viewModel.Username);
            Assert.AreEqual("", viewModel.Password);
            Assert.AreEqual("TestTokenFromTestSystem_en", viewModel.Context.CurrentToken);
            Assert.AreEqual("TestUser", viewModel.Context.CurrentUserName);
            Assert.AreEqual("", viewModel.Context.CurrentUserEmail);
            Assert.AreEqual("TestTokenFromTestSystem_en", viewModel.Context.LocalSettings.CurrentToken);
            Assert.AreEqual("TestUser", viewModel.Context.LocalSettings.CurrentUserName);
            Assert.AreEqual("", viewModel.Context.LocalSettings.CurrentUserEmail);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(UploadProgramViewModel), navigationService.CurrentView);
            Assert.AreEqual(1, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void LoginActionMissingUsernameTest()
        {
            var notificationService = (NotificationServiceTest)ServiceLocator.NotifictionService;
            notificationService.SentMessageBoxes = 0;
            notificationService.SentToastNotifications = 0;
            notificationService.NextMessageboxResult = MessageboxResult.Ok;

            var viewModel = new UploadProgramLoginViewModel
            {
                Username = "",
                Password = "TestPassword"
            };
            viewModel.LoginCommand.Execute(null);

            Assert.AreEqual(1, notificationService.SentMessageBoxes);
            Assert.AreEqual(0, notificationService.SentToastNotifications);
            Assert.AreEqual("Login failed", notificationService.LastNotificationTitle);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void LoginActionMissingPasswordTest()
        {
            var notificationService = (NotificationServiceTest)ServiceLocator.NotifictionService;
            notificationService.SentMessageBoxes = 0;
            notificationService.SentToastNotifications = 0;
            notificationService.NextMessageboxResult = MessageboxResult.Ok;

            var viewModel = new UploadProgramLoginViewModel
            {
                Username = "TestUsername",
                Password = ""
            };
            viewModel.LoginCommand.Execute(null);

            Assert.AreEqual(1, notificationService.SentMessageBoxes);
            Assert.AreEqual(0, notificationService.SentToastNotifications);
            Assert.AreEqual("Login failed", notificationService.LastNotificationTitle);
        }


        [TestMethod, TestCategory("GatedTests")]
        public void ForgottenActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProgramLoginViewModel);

            var viewModel = new UploadProgramLoginViewModel
            {
                Username = "TestUser",
                Password = "TestPassword",
            };
            viewModel.ForgottenCommand.Execute(null);

            Assert.AreEqual("", viewModel.Username);
            Assert.AreEqual("", viewModel.Password);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(UploadProgramForgotPasswordViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void RegisterActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProgramLoginViewModel);

            var viewModel = new UploadProgramLoginViewModel
            {
                Username = "TestUser",
                Password = "TestPassword",
            };
            viewModel.RegisterCommand.Execute(null);

            Assert.AreEqual("", viewModel.Username);
            Assert.AreEqual("", viewModel.Password);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(UploadProgramRegisterViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProgramLoginViewModel);

            var viewModel = new UploadProgramLoginViewModel
            {
                Username = "TestUser",
                Password = "TestPassword",
            };
            viewModel.GoBackCommand.Execute(null);

            Assert.AreEqual("", viewModel.Username);
            Assert.AreEqual("", viewModel.Password);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
