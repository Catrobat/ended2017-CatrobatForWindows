using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.Resources;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;
using Catrobat.IDE.Core.ViewModels.Service;
using Catrobat.IDE.Core.ViewModels.Settings;
using Catrobat.IDE.Core.CatrobatObjects;

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
            ServiceLocator.Register<NotificationServiceTest>(TypeCreationMode.Normal);
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
            // unchecked private members and unchecks callback-action result
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
            // unchecked private members and unchecks callback-action result
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

            var onlineProjectHeader = new OnlineProjectHeader
            {
                ProjectId = "1769",
                ProjectName = "Radio Fun City",
                ProjectNameShort = "Radio Fun City",
                ScreenshotBig = "resources/thumbnails/1769_large.png",
                ScreenshotSmall = "resources/thumbnails/1769_small.png",
                Author = "Skater5",
                Description = "This is my radio channel . Just downlad and listen",
                Uploaded = "1406382848.5354",
                UploadedString = "1 Stunde",
                Version = "0.9.9",
                Views = "2",
                Downloads = "5",
                ProjectUrl = "details/1769",
                DownloadUrl = "download/1769.catrobat"
            };
            var viewModel = new MainViewModel();
            viewModel.OnlineProjectTapCommand.Execute(onlineProjectHeader);

            Assert.AreEqual(viewModel.SelectedOnlineProject, onlineProjectHeader);
            Assert.IsTrue(viewModel.SelectedOnlineProject.Downloads == "5");
            Assert.IsTrue(viewModel.SelectedOnlineProject.ScreenshotSmall == ApplicationResources.POCEKTCODE_BASE_ADDRESS + "resources/thumbnails/1769_small.png");
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
            Assert.AreEqual(ApplicationResources.CATROBAT_LICENSES_URL, navigationService.CurrentView);
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
            Assert.AreEqual(ApplicationResources.CATROBAT_URL, navigationService.CurrentView);
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
            Assert.AreEqual(ApplicationResources.CATROBAT_TOU_URL, navigationService.CurrentView);
            Assert.AreEqual(1, navigationService.PageStackCount);
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void ShowMessagesActionTest()
        {
            //TODO to be tested
            Assert.AreEqual(0, "test not implemented");
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
