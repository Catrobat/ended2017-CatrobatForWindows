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
    public class ProjectImportViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.Register<DispatcherServiceTest>(TypeCreationMode.Lazy);
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void AddActionTest()
        {
            //TODO to be tested
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void CancelActionTest()
        {
            //TODO to be tested
            Assert.AreEqual(0, "test also ProjectImporter-Service");

            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ProjectImportViewModel);

            var viewModel = new ProjectImportViewModel();
            viewModel.CancelCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(MainViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void GoBackActionTest()
        {
            //TODO to be tested
            Assert.AreEqual(0, "test also ProjectImporter-Service");

            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ProjectImportViewModel);

            var viewModel = new ProjectImportViewModel
            {
                ProjectHeader = null,
                ContentPanelVisibility = true,
                LoadingPanelVisibility = false,
                ProgressBarLoadingIsIndeterminate = false,
                CheckBoxMakeActiveIsChecked = false,
                ButtonAddIsEnabled = false,
                ButtonCancelIsEnabled = false
            };
            viewModel.GoBackCommand.Execute(null);

            Assert.IsNull(viewModel.ProjectHeader);
            Assert.IsFalse(viewModel.ContentPanelVisibility);
            Assert.IsTrue(viewModel.LoadingPanelVisibility);
            Assert.IsTrue(viewModel.ProgressBarLoadingIsIndeterminate);
            Assert.IsTrue(viewModel.CheckBoxMakeActiveIsChecked);
            Assert.IsTrue(viewModel.ButtonAddIsEnabled);
            Assert.IsTrue(viewModel.ButtonCancelIsEnabled);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
