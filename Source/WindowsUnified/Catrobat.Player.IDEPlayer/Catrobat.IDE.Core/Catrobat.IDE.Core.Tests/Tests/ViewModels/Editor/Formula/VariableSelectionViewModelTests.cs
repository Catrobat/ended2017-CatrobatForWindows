using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.Tests.Services.Storage;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Formula;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Editor.Formula
{
    [TestClass]
    public class VariableSelectionViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
        }

        [TestMethod, TestCategory("ViewModels.Formula"), TestCategory("ExcludeGated")]
        public void FinishedActionTest()
        {
            Assert.AreEqual(0, "Test not implemented");
        }

        [TestMethod, TestCategory("ViewModels.Formula"), TestCategory("ExcludeGated")]
        public void AddVariableActionTest()
        {
            Assert.AreEqual(0, "Test not implemented");
        }

        [TestMethod, TestCategory("ViewModels.Formula"), TestCategory("ExcludeGated")]
        public void DeleteVariableActionTest()
        {
            Assert.AreEqual(0, "Test not implemented");
        }

        [TestMethod, TestCategory("ViewModels.Formula"), TestCategory("ExcludeGated")]
        public void EditVariableActionTest()
        {
            Assert.AreEqual(0, "Test not implemented");
        }

        [TestMethod, TestCategory("ViewModels.Formula")]
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(VariableSelectionViewModel);

            var viewModel = new VariableSelectionViewModel();
            viewModel.GoBackCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
