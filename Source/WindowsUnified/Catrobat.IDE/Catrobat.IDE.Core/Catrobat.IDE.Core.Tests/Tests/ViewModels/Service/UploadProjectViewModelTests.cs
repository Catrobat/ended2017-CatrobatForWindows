using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;
using Catrobat.IDE.Core.ViewModels.Service;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Service
{
    [TestClass]
    public class UploadProjectViewModelTests
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
            var viewModel = new UploadProjectViewModel();
            viewModel.InitializeCommand.Execute(null);

            Assert.IsTrue(viewModel.ProjectName == "");
            Assert.IsTrue(viewModel.ProjectDescription == "");
        }

        [TestMethod, TestCategory("GatedTests")]
        public void InitializeActionFullProjectTest()
        {
            var viewModel = new UploadProjectViewModel();            
            var project = new Project
            {
                Name = "TestProject",
                Description = "TestProjectDescription"
            };
            var messageContext = new GenericMessage<Project>(project);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.CurrentProjectChangedListener);
            viewModel.InitializeCommand.Execute(null);

            Assert.IsTrue(viewModel.ProjectName == "TestProject");
            Assert.IsTrue(viewModel.ProjectDescription == "TestProjectDescription");
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void UploadActionTest()
        {
            //TODO to be tested
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void ChangeUserActionTest()
        {
            Assert.AreEqual(0, "also check for context");

            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProjectViewModel);

            var viewModel = new UploadProjectViewModel
            {
                ProjectName = "TestProjectName",
                ProjectDescription = "TestProjectDescription"
            };
            viewModel.ChangeUserCommand.Execute(null);

            Assert.IsTrue(viewModel.ProjectName == "");
            Assert.IsTrue(viewModel.ProjectDescription == "");
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(UploadProjectLoginViewModel), navigationService.CurrentView);
            Assert.AreEqual(1, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void CancelActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProjectViewModel);

            var viewModel = new UploadProjectViewModel
            {
                ProjectName = "TestProjectName",
                ProjectDescription = "TestProjectDescription"
            };
            viewModel.CancelCommand.Execute(null);

            Assert.IsTrue(viewModel.ProjectName == "");
            Assert.IsTrue(viewModel.ProjectDescription == "");
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(ProjectDetailViewModel), navigationService.CurrentView);
            Assert.AreEqual(1, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(UploadProjectViewModel);

            var viewModel = new UploadProjectViewModel
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
