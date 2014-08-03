using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Catrobat.IDE.Core.ViewModels.Editor.Sounds;
using Catrobat.IDE.Core.ViewModels.Editor.Looks;
using Catrobat.IDE.Core.Models;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Editor.Sprites
{
    [TestClass]
    public class SpritesEditorViewModelTests
    {
        private Sprite _selectedSprite;
        private Program _currentProjectHeader; // TODO Program <-> Project-Header issue in Project Settings Action

        [TestInitialize]
        public void TestClassInitialize()
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.Register<DispatcherServiceTest>(TypeCreationMode.Normal);
        }

        [TestMethod]
        public void RenameSpriteActionTest()
        {
            Messenger.Default.Register<GenericMessage<Sprite>>(this,
                 ViewModelMessagingToken.SpriteNameListener, SpriteNameChangedMessageAction);

            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(SpriteEditorViewModel);

            var viewModel = new SpriteEditorViewModel
            {
                SelectedSprite = new Sprite { Name = "TestSpriteName" }
            };
            viewModel.RenameSpriteCommand.Execute(null);

            Assert.AreEqual("TestSpriteName", _selectedSprite.Name);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(ChangeSpriteViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void AddNewScriptBrickActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void CopyScriptBrickActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void DeleteScriptBrickActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void AddBroadcastMessageActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod]
        public void AddNewSoundActionTest()
        {
            if (_selectedSprite != null)
                _selectedSprite.Name = "AddNewSoundActionTest";

            Messenger.Default.Register<GenericMessage<Sprite>>(this,
                 ViewModelMessagingToken.CurrentSpriteChangedListener, CurrentSpriteChangedMessageAction);

            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(SpriteEditorViewModel);

            var viewModel = new SpriteEditorViewModel
            {
                SelectedSprite = new Sprite { Name = "TestSpriteName" }
            };
            viewModel.AddNewSoundCommand.Execute(null);

            Assert.AreEqual("TestSpriteName", _selectedSprite.Name);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(NewSoundSourceSelectionViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void EditSoundActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void DeleteSoundActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void AddNewLookActionTest()
        {
            if (_selectedSprite != null)
                _selectedSprite.Name = "AddNewLookActionTest";

            Messenger.Default.Register<GenericMessage<Sprite>>(this,
                 ViewModelMessagingToken.CurrentSpriteChangedListener, CurrentSpriteChangedMessageAction);

            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(SpriteEditorViewModel);

            var viewModel = new SpriteEditorViewModel
            {
                SelectedSprite = new Sprite { Name = "TestSpriteName" }
            };
            viewModel.AddNewLookCommand.Execute(null);

            Assert.AreEqual("TestSpriteName", _selectedSprite.Name);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(NewLookSourceSelectionViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void EditLookActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void CopyLookActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void DeleteLookActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod]
        public void ClearObjectSelectionActionTest()
        {
            var viewModel = new SpriteEditorViewModel
            {
                SelectedSprite = new Sprite { Name = "TestSpriteName" }
            };
            viewModel.ClearObjectsSelectionCommand.Execute(null);
            Assert.IsNull(viewModel.SelectedSprite);
        }

        [TestMethod]
        public void ClearScriptsSelectionActionTest()
        {
            var viewModel = new SpriteEditorViewModel
            {
                SelectedSprite = new Sprite { Name = "TestSpriteName" }
            };
            var action = new Models.Bricks.CommentBrick();
            viewModel.SelectedActions.Add(action);
            viewModel.ClearScriptsSelectionCommand.Execute(null);
            Assert.AreEqual(0, viewModel.SelectedActions.Count);
        }

        [TestMethod]
        public void ClearLooksSelectionActionTest()
        {
            var viewModel = new SpriteEditorViewModel
            {
                SelectedSprite = new Sprite { Name = "TestSpriteName" }
            };
            var look = new Look();
            viewModel.SelectedLooks.Add(look);
            viewModel.ClearLooksSelectionCommand.Execute(null);
            Assert.AreEqual(0, viewModel.SelectedLooks.Count);
        }

        [TestMethod]
        public void ClearSoundsSelectionActionTest()
        {
            var viewModel = new SpriteEditorViewModel
            {
                SelectedSprite = new Sprite { Name = "TestSpriteName" }
            };
            var sound = new Sound();
            viewModel.SelectedSounds.Add(sound);
            viewModel.ClearSoundsSelectionCommand.Execute(null);
            Assert.AreEqual(0, viewModel.SelectedSounds.Count);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void StartPlayerActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod]
        public void GoToMainViewActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(SpriteEditorViewModel);

            var viewModel = new SpriteEditorViewModel();
            viewModel.GoToMainViewCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(MainViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount); ;
        }

        [TestMethod]
        public void ProjectSettingsActionTest()
        {
            Messenger.Default.Register<GenericMessage<Program>>(this,
                 ViewModelMessagingToken.CurrentProgramHeaderChangedListener, CurrentProjectHeaderChangedMessageAction);

            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(SpriteEditorViewModel);

            var viewModel = new SpriteEditorViewModel
            {
                SelectedSprite = new Sprite { Name = "TestSpriteName" }
            };
            var program = new Program
            {
                Name = "TestProgram", 
            };
            var messageContext = new GenericMessage<Program>(program);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.CurrentProgramChangedListener);
            viewModel.ProjectSettingsCommand.Execute(null);

            Assert.AreEqual("TestProgram", viewModel.CurrentProgram.Name); // message action test
            Assert.AreEqual("TestProgram", _currentProjectHeader.Name);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(ProgramSettingsViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void UndoActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void RedoActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void NothingItemHackActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod]
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(SpriteEditorViewModel);

            var viewModel = new SpriteEditorViewModel();
            viewModel.GoBackCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }




        #region MessageActions
        private void SpriteNameChangedMessageAction(GenericMessage<Sprite> message)
        {
            _selectedSprite = message.Content;
        }

        private void CurrentSpriteChangedMessageAction(GenericMessage<Sprite> message)
        {
            _selectedSprite = message.Content;
        }

        private void CurrentProjectHeaderChangedMessageAction(GenericMessage<Program> message)
        {
            _currentProjectHeader = message.Content;
        }
        #endregion
    }
}
