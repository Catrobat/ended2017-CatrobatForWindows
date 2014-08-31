using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.Tests.Services.Common;
using Catrobat.IDE.Core.ViewModels.Service;
using Catrobat.IDE.Core.ViewModels.Main;
using Catrobat.IDE.Core.ViewModels;
using System.Globalization;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Service
{
    [TestClass]
    public class UploadProgramNewPasswordViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testcontext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.UnRegisterAll();
            ServiceLocator.Register<NotificationServiceTest>(TypeCreationMode.Lazy);
            ServiceLocator.Register<WebCommunicationTest>(TypeCreationMode.Lazy);
            ServiceLocator.Register<CultureServiceTest>(TypeCreationMode.Lazy);
            ServiceLocator.CultureService.SetCulture(new CultureInfo("en"));
        }

        [TestMethod, TestCategory("ViewModels.Service")]
        public void NewPasswordActionTest()
        {
            //TODO check messages for different responses - e.g. wrong hash or http-request failed
            //TODO check reading of hash
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProgramNewPasswordViewModel);

            var notificationService = (NotificationServiceTest)ServiceLocator.NotifictionService;
            notificationService.SentMessageBoxes = 0;
            notificationService.SentToastNotifications = 0;
            notificationService.NextMessageboxResult = MessageboxResult.Ok;

            var viewModel = new UploadProgramNewPasswordViewModel
            {
                NewPassword = "TestPassword",
                RepeatedPassword = "TestRepeatedPassword"
            };

            viewModel.NewPasswordCommand.Execute(null);

            Assert.AreEqual("", viewModel.NewPassword);
            Assert.AreEqual("", viewModel.RepeatedPassword);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
            Assert.AreEqual(1, notificationService.SentMessageBoxes);
            Assert.AreEqual(0, notificationService.SentToastNotifications);
            Assert.AreEqual("New password:", notificationService.LastNotificationTitle);
        }

        [TestMethod, TestCategory("ViewModels.Service")]
        public void NewPasswordActionMissingPasswordTest()
        {
            var notificationService = (NotificationServiceTest)ServiceLocator.NotifictionService;
            notificationService.SentMessageBoxes = 0;
            notificationService.SentToastNotifications = 0;
            notificationService.NextMessageboxResult = MessageboxResult.Ok;

            var viewModel = new UploadProgramNewPasswordViewModel
            {
                NewPassword = "",
                RepeatedPassword = "TestRepeatedPassword"
            };
            viewModel.NewPasswordCommand.Execute(null);

            Assert.AreEqual(1, notificationService.SentMessageBoxes);
            Assert.AreEqual(0, notificationService.SentToastNotifications);
            Assert.AreEqual("Recovery failed", notificationService.LastNotificationTitle);
        }

        [TestMethod, TestCategory("ViewModels.Service")]
        public void NewPasswordActionMissingRepeatedPasswordTest()
        {
            var notificationService = (NotificationServiceTest)ServiceLocator.NotifictionService;
            notificationService.SentMessageBoxes = 0;
            notificationService.SentToastNotifications = 0;
            notificationService.NextMessageboxResult = MessageboxResult.Ok;

            var viewModel = new UploadProgramNewPasswordViewModel
            {
                NewPassword = "TestPassword",
                RepeatedPassword = ""
            };
            viewModel.NewPasswordCommand.Execute(null);

            Assert.AreEqual(1, notificationService.SentMessageBoxes);
            Assert.AreEqual(0, notificationService.SentToastNotifications);
            Assert.AreEqual("Recovery failed", notificationService.LastNotificationTitle);
        }

        [TestMethod, TestCategory("ViewModels.Service")]
        public void CancelActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProgramNewPasswordViewModel);

            var viewModel = new UploadProgramNewPasswordViewModel
            {
                NewPassword = "TestPassword",
                RepeatedPassword = "TestRepeatedPassword"
            };
            viewModel.CancelCommand.Execute(null);

            Assert.AreEqual("", viewModel.NewPassword);
            Assert.AreEqual("", viewModel.RepeatedPassword);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(MainViewModel), navigationService.CurrentView);
        }

        [TestMethod, TestCategory("ViewModels.Service")]
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProgramNewPasswordViewModel);

            var viewModel = new UploadProgramNewPasswordViewModel
            {
                NewPassword = "TestPassword",
                RepeatedPassword = "TestRepeatedPassword"
            };
            viewModel.GoBackCommand.Execute(null);

            Assert.AreEqual("", viewModel.NewPassword);
            Assert.AreEqual("", viewModel.RepeatedPassword);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
