using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;
using Catrobat.IDE.Core.ViewModels.Service;
using Catrobat.IDE.Core.Tests.Services.Common;
using System.Globalization;
using System.Threading.Tasks;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Service
{
    [TestClass]
    public class UploadProgramViewModelTests
    {
        private bool _uploadStarted = false;

        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.UnRegisterAll();
            ServiceLocator.Register<DispatcherServiceTest>(TypeCreationMode.Lazy);
            ServiceLocator.Register<NotificationServiceTest>(TypeCreationMode.Normal);
            ServiceLocator.Register<WebCommunicationTest>(TypeCreationMode.Lazy);
            ServiceLocator.Register<CultureServiceTest>(TypeCreationMode.Lazy);
            ServiceLocator.CultureService.SetCulture(new CultureInfo("en"));
        }

        [TestMethod, TestCategory("ViewModels.Service")]
        public void NavigateToEmptyProgramTest()
        {
            var viewModel = new UploadProgramViewModel();
            viewModel.NavigateTo();

            Assert.IsTrue(viewModel.ProgramName == "");
            Assert.IsTrue(viewModel.ProgramDescription == "");
        }

        [TestMethod, TestCategory("ViewModels.Service")]
        public void NavigatToFullProgramTest()
        {
            var viewModel = new UploadProgramViewModel();            
            var project = new Program
            {
                Name = "TestProgram",
                Description = "TestProgramDescription"
            };
            var messageContext = new GenericMessage<Program>(project);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.CurrentProgramChangedListener);
            viewModel.NavigateTo();

            Assert.AreEqual("TestProgram", viewModel.ProgramName);
            Assert.AreEqual("TestProgramDescription", viewModel.ProgramDescription);
        }

        [TestMethod, TestCategory("ViewModels.Service"), TestCategory("ExcludeGated")]
        public void UploadActionTest()
        {
            //TODO check messages for different responses - e.g. wrong token or http-request failed
            Assert.AreEqual(0, "test async-command");
            Messenger.Default.Register<GenericMessage<string>>(this,
               ViewModelMessagingToken.UploadProgramStartedListener, UploadProgramStartedMessageAction);

            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProgramViewModel);

            var notificationService = (NotificationServiceTest)ServiceLocator.NotifictionService;
            notificationService.SentMessageBoxes = 0;
            notificationService.SentToastNotifications = 0;

            var viewModel = new UploadProgramViewModel
            {
                ProgramName = "TestProgramName",
                ProgramDescription = "TestProgramDescription"
            };
            var localSettings = new LocalSettings();
            var context = new CatrobatContext
            {
                LocalSettings = localSettings,
                CurrentToken = "TestTokenValid",
                CurrentUserName = "TestUser",
                CurrentUserEmail = ""
            };
            var messageContext = new GenericMessage<CatrobatContextBase>(context);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.ContextListener);

            viewModel.UploadCommand.Execute(null);

            Assert.IsTrue(_uploadStarted);
            Assert.AreEqual("", viewModel.ProgramName);
            Assert.AreEqual("", viewModel.ProgramDescription);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
            Assert.AreEqual(0, notificationService.SentMessageBoxes);
            Assert.AreEqual(1, notificationService.SentToastNotifications);
            Assert.IsNull(notificationService.LastNotificationTitle);
        }

        [TestMethod, TestCategory("ViewModels.Service"), TestCategory("ExcludeGated")]
        public void UploadActionTest()
        {
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod, TestCategory("ViewModels.Service")]
        public void ChangeUserActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProgramViewModel);

            var viewModel = new UploadProgramViewModel
            {
                ProgramName = "TestProgramName",
                ProgramDescription = "TestProgramDescription"
            };
            var localSettings = new LocalSettings();
            var context = new CatrobatContext
            {
                LocalSettings = localSettings,
                CurrentToken = "TestToken",
                CurrentUserName = "TestUserName",
                CurrentUserEmail = "TestUserEmail"
            };
            var messageContext = new GenericMessage<CatrobatContextBase>(context);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.ContextListener);
            
            viewModel.ChangeUserCommand.Execute(null);

            Assert.AreEqual("", viewModel.ProgramName);
            Assert.AreEqual("", viewModel.ProgramDescription);
            Assert.AreEqual("", viewModel.Context.CurrentToken);
            Assert.AreEqual("", viewModel.Context.CurrentUserName);
            Assert.AreEqual("", viewModel.Context.CurrentUserEmail);
            Assert.AreEqual("", viewModel.Context.LocalSettings.CurrentToken);
            Assert.AreEqual("", viewModel.Context.LocalSettings.CurrentUserName);
            Assert.AreEqual("", viewModel.Context.LocalSettings.CurrentUserEmail);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(UploadProgramLoginViewModel), navigationService.CurrentView);
            Assert.AreEqual(1, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("ViewModels.Service")]
        public void CancelActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProgramViewModel);

            var viewModel = new UploadProgramViewModel
            {
                ProgramName = "TestProgramName",
                ProgramDescription = "TestProgramDescription"
            };
            viewModel.CancelCommand.Execute(null);

            Assert.AreEqual("", viewModel.ProgramName);
            Assert.AreEqual("", viewModel.ProgramDescription);
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
            navigationService.CurrentView = typeof(UploadProgramViewModel);

            var viewModel = new UploadProgramViewModel
            {
                ProgramName = "TestProgramName",
                ProgramDescription = "TestProgramDescription"
            };
            viewModel.GoBackCommand.Execute(null);

            Assert.AreEqual("", viewModel.ProgramName);
            Assert.AreEqual("", viewModel.ProgramDescription);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        #region MessageActions
        private void UploadProgramStartedMessageAction(GenericMessage<string> message)
        {
            _uploadStarted = true;
        }
        #endregion
    }
}
