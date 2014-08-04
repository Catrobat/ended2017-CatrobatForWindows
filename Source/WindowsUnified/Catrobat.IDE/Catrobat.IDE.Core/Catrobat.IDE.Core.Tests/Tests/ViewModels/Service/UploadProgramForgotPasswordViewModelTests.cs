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
    public class UploadProgramForgotPasswordViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.UnRegisterAll();
            ServiceLocator.Register<DispatcherServiceTest>(TypeCreationMode.Lazy);
            ServiceLocator.Register<NotificationServiceTest>(TypeCreationMode.Lazy);
            ServiceLocator.Register<WebCommunicationTest>(TypeCreationMode.Lazy);
            ServiceLocator.Register<CultureServiceTest>(TypeCreationMode.Lazy);
            ServiceLocator.CultureService.SetCulture(new CultureInfo("en"));
        }

        [TestMethod, TestCategory("ViewModels.Service")]
        public void RecoverActionTest()
        {
            //TODO check messages for different responses - e.g. wrong recoverydata or http-request failed
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProgramForgotPasswordViewModel);

            var viewModel = new UploadProgramForgotPasswordViewModel
            {
                RecoveryData = "TestRecoveryData"
            };
            var localSettings = new LocalSettings();
            var context = new CatrobatContext
            {
                LocalSettings = localSettings,
                CurrentToken = "TestTokenFromTestSystem_en",
                CurrentUserName = "TestUser",
                CurrentUserEmail = ""
            };
            var messageContext = new GenericMessage<CatrobatContextBase>(context);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.ContextListener);

            viewModel.RecoverCommand.Execute(null);

            Assert.AreEqual("", viewModel.RecoveryData);
            Assert.AreEqual("TestHashFromTestSystem", viewModel.Context.LocalSettings.CurrentUserRecoveryHash);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(UploadProgramNewPasswordViewModel), navigationService.CurrentView);
            Assert.AreEqual(1, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("ViewModels.Service")]
        public void RecoverActionMissingReasonTest()
        {
            var notificationService = (NotificationServiceTest)ServiceLocator.NotifictionService;
            notificationService.SentMessageBoxes = 0;
            notificationService.SentToastNotifications = 0;
            notificationService.NextMessageboxResult = MessageboxResult.Ok;

            var viewModel = new UploadProgramForgotPasswordViewModel
            {
                RecoveryData = ""
            };
            viewModel.RecoverCommand.Execute(null);

            Assert.AreEqual(1, notificationService.SentMessageBoxes);
            Assert.AreEqual(0, notificationService.SentToastNotifications);
            Assert.AreEqual("Recovery failed", notificationService.LastNotificationTitle);
        }

        [TestMethod, TestCategory("ViewModels.Service")]
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProgramForgotPasswordViewModel);

            var viewModel = new UploadProgramForgotPasswordViewModel
            {
                RecoveryData = "TestUser"
            };
            viewModel.GoBackCommand.Execute(null);

            Assert.AreEqual("", viewModel.RecoveryData);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
