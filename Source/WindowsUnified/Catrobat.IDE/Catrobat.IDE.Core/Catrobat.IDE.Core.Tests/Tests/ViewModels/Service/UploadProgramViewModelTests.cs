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

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Service
{
    [TestClass]
    public class UploadProgramViewModelTests
    {
        private bool _uploadStarted;

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

        [TestMethod, TestCategory("GatedTests")]
        public void InitializeActionEmptyProjectTest()
        {
            var viewModel = new UploadProgramViewModel();
            viewModel.InitializeCommand.Execute(null);

            Assert.IsTrue(viewModel.ProjectName == "");
            Assert.IsTrue(viewModel.ProjectDescription == "");
        }

        [TestMethod, TestCategory("GatedTests")]
        public void InitializeActionFullProjectTest()
        {
            var viewModel = new UploadProgramViewModel();            
            var project = new Program
            {
                Name = "TestProject",
                Description = "TestProjectDescription"
            };
            var messageContext = new GenericMessage<Program>(project);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.CurrentProjectChangedListener);
            viewModel.InitializeCommand.Execute(null);

            Assert.AreEqual("TestProject", viewModel.ProjectName);
            Assert.AreEqual("TestProjectDescription", viewModel.ProjectDescription);
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void UploadActionTest()
        {
            //TODO saving of context and renaming of directory not tested
            //TODO check messages for different responses - e.g. wrong token or http-request failed
            Assert.AreEqual(0, "test not fully implemented");

            Messenger.Default.Register<MessageBase>(this,
               ViewModelMessagingToken.UploadProjectStartedListener, UploadProjectStartedMessageAction);

            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProgramViewModel);

            var notificationService = (NotificationServiceTest)ServiceLocator.NotifictionService;
            notificationService.SentMessageBoxes = 0;
            notificationService.SentToastNotifications = 0;

            var viewModel = new UploadProgramViewModel
            {
                ProjectName = "TestProjectName",
                ProjectDescription = "TestProjectDescription"
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

            //viewModel.UploadCommand.Execute(null);

            Assert.IsTrue(_uploadStarted);
            Assert.AreEqual("", viewModel.ProjectName);
            Assert.AreEqual("", viewModel.ProjectDescription);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
            Assert.AreEqual(0, notificationService.SentMessageBoxes);
            Assert.AreEqual(1, notificationService.SentToastNotifications);
            Assert.IsNull(notificationService.LastNotificationTitle);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void ChangeUserActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProgramViewModel);

            var viewModel = new UploadProgramViewModel
            {
                ProjectName = "TestProjectName",
                ProjectDescription = "TestProjectDescription"
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

            Assert.AreEqual("", viewModel.ProjectName);
            Assert.AreEqual("", viewModel.ProjectDescription);
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

        [TestMethod, TestCategory("GatedTests")]
        public void CancelActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProgramViewModel);

            var viewModel = new UploadProgramViewModel
            {
                ProjectName = "TestProjectName",
                ProjectDescription = "TestProjectDescription"
            };
            viewModel.CancelCommand.Execute(null);

            Assert.AreEqual("", viewModel.ProjectName);
            Assert.AreEqual("", viewModel.ProjectDescription);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(ProgramDetailViewModel), navigationService.CurrentView);
            Assert.AreEqual(1, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProgramViewModel);

            var viewModel = new UploadProgramViewModel
            {
                ProjectName = "TestProjectName",
                ProjectDescription = "TestProjectDescription"
            };
            viewModel.GoBackCommand.Execute(null);

            Assert.AreEqual("", viewModel.ProjectName);
            Assert.AreEqual("", viewModel.ProjectDescription);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        #region MessageActions
        private void UploadProjectStartedMessageAction(MessageBase message)
        {
            _uploadStarted = true;
        }
        #endregion
    }
}
