using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.Tests.Services.Common;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Service;
using System.Globalization;
using Catrobat.IDE.Core.Tests.SampleData;
using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Service
{
    [TestClass]
    public class OnlineProgramReportViewModelTests
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

        [TestMethod, TestCategory("ViewModels.Service"), TestCategory("ExcludeGated")]
        public void ReportActionTest()
        {
            //TODO check messages for different responses - e.g. http-request failed
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(OnlineProgramReportViewModel);

            var notificationService = (NotificationServiceTest)ServiceLocator.NotifictionService;
            notificationService.SentMessageBoxes = 0;
            notificationService.SentToastNotifications = 0;
            notificationService.NextMessageboxResult = MessageboxResult.Ok;

            var onlineProjectHeader = SampleLoader.GetSampleOnlineProjectHeader();
            var viewModel = new OnlineProgramReportViewModel
            {
                Reason = "TestReason"
            };
            var messageContext = new GenericMessage<OnlineProgramHeader>(onlineProjectHeader);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.SelectedOnlineProgramChangedListener);
            viewModel.ReportCommand.Execute(null);

            Assert.AreEqual("", viewModel.Reason);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
            Assert.AreEqual(1, notificationService.SentMessageBoxes);
            Assert.AreEqual(0, notificationService.SentToastNotifications);
            Assert.AreEqual("Report program", notificationService.LastNotificationTitle);
        }

        [TestMethod, TestCategory("ViewModels.Service")]
        public void ReportActionMissingReasonTest()
        {
            var notificationService = (NotificationServiceTest)ServiceLocator.NotifictionService;
            notificationService.SentMessageBoxes = 0;
            notificationService.SentToastNotifications = 0;
            notificationService.NextMessageboxResult = MessageboxResult.Ok;

            var viewModel = new OnlineProgramReportViewModel
            {
                Reason = ""
            };
            viewModel.ReportCommand.Execute(null);

            Assert.AreEqual(1, notificationService.SentMessageBoxes);
            Assert.AreEqual(0, notificationService.SentToastNotifications);
            Assert.AreEqual("Report failed", notificationService.LastNotificationTitle);
        }

        [TestMethod, TestCategory("ViewModels.Service")]
        public void CancelActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(OnlineProgramReportViewModel);

            var viewModel = new OnlineProgramReportViewModel
            {
                Reason = "TestReason"
            };
            viewModel.CancelCommand.Execute(null);

            Assert.AreEqual("", viewModel.Reason);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("ViewModels.Service")]
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(OnlineProgramReportViewModel);

            var viewModel = new OnlineProgramReportViewModel
            {
                Reason = "TestReason"
            };
            viewModel.GoBackCommand.Execute(null);

            Assert.AreEqual("", viewModel.Reason);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
