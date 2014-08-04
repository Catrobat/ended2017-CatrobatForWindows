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
    public class AddNewProgramViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void SaveActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod]
        public void CancelActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(AddNewProgramViewModel);

            var viewModel = new AddNewProgramViewModel
            {
                ProgramName = "TestProject",
                CreateEmptyProgram = false,
                CreateTemplateProgram = true
            };
            viewModel.CancelCommand.Execute(null);

            //Assert.IsTrue(viewModel.ProgramName == ""); TODO check in NavigateTo
            //Assert.IsTrue(viewModel.CreateEmptyProgram);
            //Assert.IsFalse(viewModel.CreateTemplateProgram);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod]
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(AddNewProgramViewModel);

            var viewModel = new AddNewProgramViewModel
            {
                ProgramName = "TestProject",
                CreateEmptyProgram = false,
                CreateTemplateProgram = true
            };
            viewModel.GoBackCommand.Execute(null);

            //Assert.AreEqual("", viewModel.ProgramName); TODO check in NavigateTo
            //Assert.IsTrue(viewModel.CreateEmptyProgram);
            //Assert.IsFalse(viewModel.CreateTemplateProgram);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
