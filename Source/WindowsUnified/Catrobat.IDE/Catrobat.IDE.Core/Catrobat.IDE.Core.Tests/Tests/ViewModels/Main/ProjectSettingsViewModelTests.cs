using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
//using Catrobat.IDE.Core.Tests.Services.Storage;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Main
{
    [TestClass]
    public class ProjectSettingsViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.Register<DispatcherServiceTest>(TypeCreationMode.Lazy);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void InitializeActionEmptyProjectTest()
        {
            var viewModel = new ProjectSettingsViewModel();
            viewModel.InitializeCommand.Execute(null);
            Assert.AreEqual("", viewModel.ProjectName);
            Assert.AreEqual("", viewModel.ProjectDescription);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void InitializeActionFullProjectTest()
        {
            var viewModel = new ProjectSettingsViewModel();
            var project = new Program
            {
                Name = "TestProjectName",
                Description = "TestProjectDescription"
            };
            var messageContext = new GenericMessage<Program>(project);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.CurrentProjectChangedListener);
            viewModel.InitializeCommand.Execute(null);

            Assert.AreEqual("TestProjectName", viewModel.ProjectName);
            Assert.AreEqual("TestProjectDescription", viewModel.ProjectDescription);
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void SaveActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");

            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ProjectSettingsViewModel);

            var viewModel = new ProjectSettingsViewModel
            {
                ProjectName = "TestProjectName",
                ProjectDescription = "TestProjectDescription"
            };
            //viewModel.SaveCommand.Execute(null);

            Assert.AreEqual("TestProjectName", viewModel.ProjectName);
            Assert.AreEqual("TestProjectDescription", viewModel.ProjectDescription);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void CancelActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ProjectSettingsViewModel);

            var viewModel = new ProjectSettingsViewModel
            {
                ProjectName = "TestProjectName",
                ProjectDescription = "TestProjectDescription"
            };
            viewModel.CancelCommand.Execute(null);

            Assert.AreEqual("", viewModel.ProjectName);
            Assert.AreEqual("", viewModel.ProjectDescription);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ProjectSettingsViewModel);

            var viewModel = new ProjectSettingsViewModel
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
    }
}
