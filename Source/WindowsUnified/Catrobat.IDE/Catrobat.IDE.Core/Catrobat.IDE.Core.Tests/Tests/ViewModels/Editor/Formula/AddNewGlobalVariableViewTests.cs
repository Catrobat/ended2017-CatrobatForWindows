using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.Tests.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Formula;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Resources.Localization;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Editor.Formula
{
    [TestClass]
    public class AddNewGlobalVariableViewTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.UnRegisterAll();
            ServiceLocator.Register<DispatcherServiceTest>(TypeCreationMode.Lazy);
        }

        [TestMethod, TestCategory("ViewModels.Formula"), TestCategory("ExcludeGated")]
        public void SaveActionTest()
        {
            Assert.AreEqual(0, "Add test-support for VariableHelper");
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(AddNewGlobalVariableViewModel);

            var viewModel = new AddNewGlobalVariableViewModel
            {
                UserVariableName = "TestUserVariableName"
            };
            var globalVariable = new GlobalVariable
            {
                Name = "TestGlobalVariable"
            };
            var program = new Program
            {
                Name = "TestProgram",
            };
            program.GlobalVariables.Add(globalVariable);
            var messageContext = new GenericMessage<Program>(program);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.CurrentProgramChangedListener);

            viewModel.SaveCommand.Execute(null);

            Assert.AreEqual(2, viewModel.CurrentProgram.GlobalVariables.Count);
            Assert.AreEqual("TestGlobalVariable", viewModel.CurrentProgram.GlobalVariables[1].Name);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("ViewModels.Formula")]
        public void CancelActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(AddNewGlobalVariableViewModel);

            var viewModel = new AddNewGlobalVariableViewModel();
            viewModel.CancelCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("ViewModels.Formula")]
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(AddNewGlobalVariableViewModel);

            var viewModel = new AddNewGlobalVariableViewModel
            {
                UserVariableName = "TestUserVariableName"
            };
            viewModel.GoBackCommand.Execute(null);

            Assert.AreEqual(AppResources.Editor_DefaultGlobalVariableName, viewModel.UserVariableName);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
