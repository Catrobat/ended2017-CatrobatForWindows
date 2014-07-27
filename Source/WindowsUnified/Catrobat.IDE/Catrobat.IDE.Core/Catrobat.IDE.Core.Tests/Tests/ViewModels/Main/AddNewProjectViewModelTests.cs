//using System.Threading.Tasks;
//using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Main
{
    [TestClass]
    public class AddNewProjectViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void SaveActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod, TestCategory("GatedTests")]
        public void CancelActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(AddNewProjectViewModel);

            var viewModel = new AddNewProjectViewModel
            {
                ProjectName = "TestProject",
                CreateEmptyProject = false,
                CreateCopyOfCurrentProject = true,
                CreateTemplateProject = true
            };
            viewModel.CancelCommand.Execute(null);

            Assert.IsTrue(viewModel.ProjectName == "");
            Assert.IsTrue(viewModel.CreateEmptyProject);
            Assert.IsFalse(viewModel.CreateCopyOfCurrentProject);
            Assert.IsFalse(viewModel.CreateTemplateProject);
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
            navigationService.CurrentView = typeof(AddNewProjectViewModel);

            var viewModel = new AddNewProjectViewModel
            {
                ProjectName = "TestProject",
                CreateEmptyProject = false,
                CreateCopyOfCurrentProject = true,
                CreateTemplateProject = true
            };
            viewModel.GoBackCommand.Execute(null);

            Assert.IsTrue(viewModel.ProjectName == "");
            Assert.IsTrue(viewModel.CreateEmptyProject);
            Assert.IsFalse(viewModel.CreateCopyOfCurrentProject);
            Assert.IsFalse(viewModel.CreateTemplateProject);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
