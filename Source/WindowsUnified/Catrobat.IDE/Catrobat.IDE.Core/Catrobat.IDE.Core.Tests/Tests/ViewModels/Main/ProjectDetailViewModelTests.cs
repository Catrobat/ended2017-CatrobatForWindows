using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Catrobat.IDE.Core.ViewModels.Main;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Main
{
    [TestClass]
    public class ProjectDetailViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
        }

        [TestMethod, TestCategory("GatedTests")]
        public void EditCurrentProjectActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ProjectDetailViewModel);

            var viewModel = new ProjectDetailViewModel();
            viewModel.EditCurrentProjectCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(SpritesViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount); 
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void UploadCurrentProjectActionTest()
        {
            //Upload Test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void PlayCurrentProjectActionTest()
        {
            //Launch Player
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod, TestCategory("GatedTests")]
        public void RenameProjectActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ProjectDetailViewModel);

            var viewModel = new ProjectDetailViewModel();
            viewModel.RenameProjectCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(ProjectSettingsViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);  
        }

        //[TestMethod, TestCategory("GatedTests")]
        //public void PinLocalProjectActionTest()
        //{
        //    // currently not in use
        //}

        //[TestMethod, TestCategory("GatedTests")]
        //public void ShareLocalProjectActionTest()
        //{
        //    // currently not in use
        //}
    }
}
