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
    public class ProgramSettingsViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.Register<DispatcherServiceTest>(TypeCreationMode.Lazy);
        }

        [TestMethod]
        public void NavigateToEmptyProgramTest()
        {
            var viewModel = new ProgramSettingsViewModel();
            viewModel.NavigateTo();
            Assert.AreEqual("", viewModel.ProgramName);
            Assert.AreEqual("", viewModel.ProgramDescription);
        }

        [TestMethod]
        public void NavigateToFullProgramTest()
        {
            var viewModel = new ProgramSettingsViewModel();
            var project = new Program
            {
                Name = "TestProgramName",
                Description = "TestProgramDescription"
            };
            var messageContext = new GenericMessage<Program>(project);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.CurrentProgramChangedListener);
            viewModel.NavigateTo();

            Assert.AreEqual("TestProgramName", viewModel.ProgramName);
            Assert.AreEqual("TestProgramDescription", viewModel.ProgramDescription);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void SaveActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");

            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ProgramSettingsViewModel);

            var viewModel = new ProgramSettingsViewModel
            {
                ProgramName = "TestProgramName",
                ProgramDescription = "TestProgramDescription"
            };
            //viewModel.SaveCommand.Execute(null);

            Assert.AreEqual("TestProgramName", viewModel.ProgramName);
            Assert.AreEqual("TestProgramDescription", viewModel.ProgramDescription);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod]
        public void CancelActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ProgramSettingsViewModel);

            var viewModel = new ProgramSettingsViewModel
            {
                ProgramName = "TestProgramName",
                ProgramDescription = "TestProgramDescription"
            };
            viewModel.CancelCommand.Execute(null);

            Assert.AreEqual("", viewModel.ProgramName);
            Assert.AreEqual("", viewModel.ProgramDescription);
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
            navigationService.CurrentView = typeof(ProgramSettingsViewModel);

            var viewModel = new ProgramSettingsViewModel
            {
                ProgramName = "TestProgramName",
                ProgramDescription = "TestProgramDescription"
            };
            viewModel.GoBackCommand.Execute(null);

            Assert.AreEqual("", viewModel.ProgramName);
            Assert.AreEqual("", viewModel.ProgramDescription);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
