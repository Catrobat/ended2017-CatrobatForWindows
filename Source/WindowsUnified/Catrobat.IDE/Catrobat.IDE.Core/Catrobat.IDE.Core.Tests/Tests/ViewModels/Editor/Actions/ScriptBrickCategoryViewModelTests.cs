using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Actions;
using Catrobat.IDE.Core.Models;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Editor.Actions
{
    [TestClass]
    public class ScriptBrickCategoryViewModelTests
    {
        private BrickCategory _selectedBrickCategory;

        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
        }

        [TestMethod]
        public void MovementActionTest()
        {
            _selectedBrickCategory = BrickCategory.Control;
            Messenger.Default.Register<GenericMessage<BrickCategory>>(this,
                 ViewModelMessagingToken.ScriptBrickCategoryListener, ScriptBrickCategoryReceivedMessageAction);

            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ScriptBrickCategoryViewModel);

            var viewModel = new ScriptBrickCategoryViewModel();
            viewModel.MovementCommand.Execute(null);

            Assert.AreEqual(BrickCategory.Motion, _selectedBrickCategory);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(AddNewScriptBrickViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod]
        public void LooksActionTest()
        {
            _selectedBrickCategory = BrickCategory.Control;
            Messenger.Default.Register<GenericMessage<BrickCategory>>(this,
                 ViewModelMessagingToken.ScriptBrickCategoryListener, ScriptBrickCategoryReceivedMessageAction);

            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ScriptBrickCategoryViewModel);

            var viewModel = new ScriptBrickCategoryViewModel();
            viewModel.LooksCommand.Execute(null);

            Assert.AreEqual(BrickCategory.Looks, _selectedBrickCategory);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(AddNewScriptBrickViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod]
        public void SoundActionTest()
        {
            _selectedBrickCategory = BrickCategory.Control;
            Messenger.Default.Register<GenericMessage<BrickCategory>>(this,
                 ViewModelMessagingToken.ScriptBrickCategoryListener, ScriptBrickCategoryReceivedMessageAction);

            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ScriptBrickCategoryViewModel);

            var viewModel = new ScriptBrickCategoryViewModel();
            viewModel.SoundCommand.Execute(null);

            Assert.AreEqual(BrickCategory.Sounds, _selectedBrickCategory);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(AddNewScriptBrickViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod]
        public void ControlActionTest()
        {
            _selectedBrickCategory = BrickCategory.Looks;
            Messenger.Default.Register<GenericMessage<BrickCategory>>(this,
                 ViewModelMessagingToken.ScriptBrickCategoryListener, ScriptBrickCategoryReceivedMessageAction);

            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ScriptBrickCategoryViewModel);

            var viewModel = new ScriptBrickCategoryViewModel();
            viewModel.ControlCommand.Execute(null);

            Assert.AreEqual(BrickCategory.Control, _selectedBrickCategory);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(AddNewScriptBrickViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod]
        public void VariablesActionTest()
        {
            _selectedBrickCategory = BrickCategory.Control;
            Messenger.Default.Register<GenericMessage<BrickCategory>>(this,
                 ViewModelMessagingToken.ScriptBrickCategoryListener, ScriptBrickCategoryReceivedMessageAction);

            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ScriptBrickCategoryViewModel);

            var viewModel = new ScriptBrickCategoryViewModel();
            viewModel.VariablesCommand.Execute(null);

            Assert.AreEqual(BrickCategory.Variables, _selectedBrickCategory);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(AddNewScriptBrickViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod]
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ScriptBrickCategoryViewModel);

            var viewModel = new ScriptBrickCategoryViewModel();
            viewModel.GoBackCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        #region MessageActions
        private void ScriptBrickCategoryReceivedMessageAction(GenericMessage<BrickCategory> message)
        {
            _selectedBrickCategory = message.Content;
        }

        #endregion
    }
}
