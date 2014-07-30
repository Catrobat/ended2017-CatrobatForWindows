using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.Tests.Services.Storage;
using Catrobat.IDE.Core.Resources;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;
using Catrobat.IDE.Core.ViewModels.Service;
using Catrobat.IDE.Core.ViewModels.Settings;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Tests.SampleData;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Main
{
    [TestClass]
    public class OnlineProjectViewModelTests
    {
        private LocalProjectHeader _currentProjectHeader;

        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.UnRegisterAll();
            ServiceLocator.Register<NotificationServiceTest>(TypeCreationMode.Normal);
            //ServiceLocator.Register<StorageFactoryTest>(TypeCreationMode.Normal);
            //ServiceLocator.Register<StorageTest>(TypeCreationMode.Normal);
            //ServiceLocator.Register<ResourceLoaderFactoryTest>(TypeCreationMode.Normal);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void OpenProjectCommandActionTest()
        {
            Messenger.Default.Register<GenericMessage<LocalProjectHeader>>(this,
                 ViewModelMessagingToken.CurrentProjectHeaderChangedListener, CurrentProjectHeaderChangedMessageAction);
            
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(MainViewModel);

            var localProjectHeader = new LocalProjectHeader
            {
                ProjectName = "TestProject"
            };
            var viewModel = new MainViewModel();
            viewModel.OpenProjectCommand.Execute(localProjectHeader);

            Assert.AreEqual(localProjectHeader, _currentProjectHeader);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(ProjectDetailViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void DeleteLocalProjectActionTest()
        {
            // unchecked private members and unchecked callback-action result
            Assert.AreEqual(0, "result of MessageCallbackAction should be tested - involvs app.savecontext");

            var notificationService = (NotificationServiceTest)ServiceLocator.NotifictionService;
            notificationService.SentMessageBoxes = 0;
            notificationService.SentToastNotifications = 0;
            notificationService.NextMessageboxResult = MessageboxResult.Cancel; //.OK to execute callback

            var viewModel = new MainViewModel();
            string projectName = "TestProject";
            viewModel.DeleteLocalProjectCommand.Execute(projectName);

            Assert.AreEqual(1, notificationService.SentMessageBoxes);
            Assert.AreEqual(0, notificationService.SentToastNotifications);
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void CopyLocalProjectActionTest()
        {
            // unchecked private members and unchecked callback-action result
            Assert.AreEqual(0, "result of MessageCallbackAction should be tested - involvs app.savecontext");

            var notificationService = (NotificationServiceTest)ServiceLocator.NotifictionService;
            notificationService.SentMessageBoxes = 0;
            notificationService.SentToastNotifications = 0;
            notificationService.NextMessageboxResult = MessageboxResult.Cancel; //.OK to execute callback

            var viewModel = new MainViewModel();
            string projectName = "TestProject";
            viewModel.CopyLocalProjectCommand.Execute(projectName);

            Assert.AreEqual(1, notificationService.SentMessageBoxes);
            Assert.AreEqual(0, notificationService.SentToastNotifications);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void CreateNewProjectActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(MainViewModel);

            var viewModel = new MainViewModel();
            viewModel.CreateNewProjectCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(AddNewProjectViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void SettingsActionTest()
        {      
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(MainViewModel);

            var viewModel = new MainViewModel();
            viewModel.SettingsCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(SettingsViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void OnlineProjectTapActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(MainViewModel);

            var onlineProjectHeader = SampleLoader.GetSampleOnlineProjectHeader();
            var viewModel = new MainViewModel();
            viewModel.OnlineProjectTapCommand.Execute(onlineProjectHeader);

            Assert.AreEqual(viewModel.SelectedOnlineProject, onlineProjectHeader);
            Assert.AreEqual("5", viewModel.SelectedOnlineProject.Downloads);
            Assert.AreEqual(ApplicationResources.POCEKTCODE_BASE_ADDRESS + "resources/thumbnails/1769_small.png", viewModel.SelectedOnlineProject.ScreenshotSmall);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(OnlineProjectViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void LicenseActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(MainViewModel);

            var viewModel = new MainViewModel();
            viewModel.LicenseCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateToWebPage, navigationService.CurrentNavigationType);
            Assert.AreEqual("http://developer.catrobat.org/licenses", navigationService.CurrentView);
            Assert.AreEqual(1, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void AboutActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(MainViewModel);

            var viewModel = new MainViewModel();
            viewModel.AboutCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateToWebPage, navigationService.CurrentNavigationType);
            Assert.AreEqual("http://www.catrobat.org/", navigationService.CurrentView);
            Assert.AreEqual(1, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void TouAction()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(MainViewModel);

            var viewModel = new MainViewModel();
            viewModel.TouCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateToWebPage, navigationService.CurrentNavigationType);
            Assert.AreEqual("http://catrob.at/tou", navigationService.CurrentView);
            Assert.AreEqual(1, navigationService.PageStackCount);
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void ShowMessagesActionDownloadMessageTest()
        {
            // TODO issue with portable-Image
            Assert.AreEqual(0, "issue with portable-Image");
            var notificationService = (NotificationServiceTest)ServiceLocator.NotifictionService;
            notificationService.SentMessageBoxes = 0;
            notificationService.SentToastNotifications = 0;

            var viewModel = new MainViewModel();
            var messageContext = new MessageBase();
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.DownloadProjectStartedListener);

            //viewModel.ShowMessagesCommand.Execute(null);

            Assert.AreEqual(0, notificationService.SentMessageBoxes);
            Assert.AreEqual(1, notificationService.SentToastNotifications);
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void ShowMessagesActionUploadMessageTest()
        {
            // TODO issue with portable-Image
            Assert.AreEqual(0, "issue with portable-Image");
            var notificationService = (NotificationServiceTest)ServiceLocator.NotifictionService;
            notificationService.SentMessageBoxes = 0;
            notificationService.SentToastNotifications = 0;

            var viewModel = new MainViewModel();
            var messageContext = new MessageBase();
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.UploadProjectStartedListener);

            //viewModel.ShowMessagesCommand.Execute(null);

            Assert.AreEqual(0, notificationService.SentMessageBoxes);
            Assert.AreEqual(1, notificationService.SentToastNotifications);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(MainViewModel);

            var viewModel = new MainViewModel();
            viewModel.GoBackCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }


        #region MessageActions
        private void CurrentProjectHeaderChangedMessageAction(GenericMessage<LocalProjectHeader> message)
        {
            _currentProjectHeader = message.Content;
        }

        #endregion
    }
}
