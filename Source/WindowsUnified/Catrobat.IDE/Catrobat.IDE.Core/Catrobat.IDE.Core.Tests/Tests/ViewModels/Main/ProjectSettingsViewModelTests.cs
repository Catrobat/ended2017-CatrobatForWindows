using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
//using Catrobat.IDE.Core.Tests.Services.Storage;
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
            ServiceLocator.Register<DispatcherServiceTest>(TypeCreationMode.Normal);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void InitializeActionEmptyProjectTest()
        {
            var viewModel = new ProjectSettingsViewModel();
            viewModel.InitializeCommand.Execute(null);
            Assert.IsTrue(viewModel.ProjectName == "");
            Assert.IsTrue(viewModel.ProjectDescription == "");
        }

        [TestMethod, TestCategory("GatedTests")]
        public void InitializeActionFullProjectTest()
        {
            var project = new Project 
            { 
                Name = "TestProject",
                Description = "TestProjectDescription"
            };
            var viewModel = new ProjectSettingsViewModel
            {
                CurrentProject = project
            };
            viewModel.InitializeCommand.Execute(null);
            Assert.IsTrue(viewModel.ProjectName == "TestProject");
            Assert.IsTrue(viewModel.ProjectDescription == "TestProjectDescription");
            Assert.AreEqual(viewModel.ProjectName, "TestProject");
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
            viewModel.SaveCommand.Execute(null);

            Assert.IsTrue(viewModel.ProjectName == "TestProjectName");
            Assert.IsTrue(viewModel.ProjectDescription == "TestProjectDescription");
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

            var viewModel = new ProjectSettingsViewModel();
            viewModel.CancelCommand.Execute(null);

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

            Assert.IsTrue(viewModel.ProjectName == "");
            Assert.IsTrue(viewModel.ProjectDescription == "");
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
