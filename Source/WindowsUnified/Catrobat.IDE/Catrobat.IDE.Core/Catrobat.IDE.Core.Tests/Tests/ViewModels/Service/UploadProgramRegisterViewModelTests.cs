using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.Tests.Services.Common;
using Catrobat.IDE.Core.ViewModels.Service;
using Catrobat.IDE.Core.ViewModels;
using System.Globalization;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Service
{
    [TestClass]
    public class UploadProgramRegisterViewModelTests
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

        [TestMethod]
        public void RegisterActionTest()
        {
            //TODO check messages for different responses - e.g. wrong password or http-request failed
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProgramRegisterViewModel);

            var viewModel = new UploadProgramRegisterViewModel
            {
                Username = "TestUser",
                Password = "TestPassword",
                Email = "TestEmail@catrob.at"
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

            viewModel.RegisterCommand.Execute(null);

            Assert.AreEqual("", viewModel.Username);
            Assert.AreEqual("", viewModel.Password);
            Assert.AreEqual("", viewModel.Email);
            Assert.AreEqual("TestTokenFromTestSystem_en", viewModel.Context.CurrentToken);
            Assert.AreEqual("TestUser", viewModel.Context.CurrentUserName);
            Assert.AreEqual("TestEmail@catrob.at", viewModel.Context.CurrentUserEmail);
            Assert.AreEqual("TestTokenFromTestSystem_en", viewModel.Context.LocalSettings.CurrentToken);
            Assert.AreEqual("TestUser", viewModel.Context.LocalSettings.CurrentUserName);
            Assert.AreEqual("TestEmail@catrob.at", viewModel.Context.LocalSettings.CurrentUserEmail);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(UploadProgramViewModel), navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod]
        public void RegisterActionMissingUsernameTest()
        {
            var notificationService = (NotificationServiceTest)ServiceLocator.NotifictionService;
            notificationService.SentMessageBoxes = 0;
            notificationService.SentToastNotifications = 0;
            notificationService.NextMessageboxResult = MessageboxResult.Ok;

            var viewModel = new UploadProgramRegisterViewModel
            {
                Username = "",
                Password = "TestPassword",
                Email = "TestEmail@catrob.at"
            };
            viewModel.RegisterCommand.Execute(null);

            Assert.AreEqual(1, notificationService.SentMessageBoxes);
            Assert.AreEqual(0, notificationService.SentToastNotifications);
            Assert.AreEqual("Login failed", notificationService.LastNotificationTitle);
        }

        [TestMethod]
        public void RegisterActionMissingPasswordTest()
        {
            var notificationService = (NotificationServiceTest)ServiceLocator.NotifictionService;
            notificationService.SentMessageBoxes = 0;
            notificationService.SentToastNotifications = 0;
            notificationService.NextMessageboxResult = MessageboxResult.Ok;

            var viewModel = new UploadProgramRegisterViewModel
            {
                Username = "TestUser",
                Password = "",
                Email = "TestEmail@catrob.at"
            };
            viewModel.RegisterCommand.Execute(null);

            Assert.AreEqual(1, notificationService.SentMessageBoxes);
            Assert.AreEqual(0, notificationService.SentToastNotifications);
            Assert.AreEqual("Login failed", notificationService.LastNotificationTitle);
        }

        [TestMethod]
        public void RegisterActionMissingEmailTest()
        {
            var notificationService = (NotificationServiceTest)ServiceLocator.NotifictionService;
            notificationService.SentMessageBoxes = 0;
            notificationService.SentToastNotifications = 0;
            notificationService.NextMessageboxResult = MessageboxResult.Ok;

            var viewModel = new UploadProgramRegisterViewModel
            {
                Username = "TestUser",
                Password = "TestPassword",
                Email = ""
            };
            viewModel.RegisterCommand.Execute(null);

            Assert.AreEqual(1, notificationService.SentMessageBoxes);
            Assert.AreEqual(0, notificationService.SentToastNotifications);
            Assert.AreEqual("Login failed", notificationService.LastNotificationTitle);
        }

        [TestMethod]
        public void CancelActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProgramRegisterViewModel);

            var viewModel = new UploadProgramRegisterViewModel
            {
                Username = "TestUser",
                Password = "TestPassword",
                Email = "TestEmail"
            };
            viewModel.CancelCommand.Execute(null);

            Assert.AreEqual("", viewModel.Username);
            Assert.AreEqual("", viewModel.Password);
            Assert.AreEqual("", viewModel.Email);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod]
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProgramRegisterViewModel);

            var viewModel = new UploadProgramRegisterViewModel
            {
                Username = "TestUser",
                Password = "TestPassword",
                Email = "TestEmail"
            };
            viewModel.GoBackCommand.Execute(null);

            Assert.AreEqual("", viewModel.Username);
            Assert.AreEqual("", viewModel.Password);
            Assert.AreEqual("", viewModel.Email);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
