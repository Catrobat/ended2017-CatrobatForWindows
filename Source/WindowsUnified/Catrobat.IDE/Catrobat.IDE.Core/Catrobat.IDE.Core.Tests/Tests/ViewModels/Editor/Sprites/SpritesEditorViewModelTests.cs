using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Catrobat.IDE.Core.ViewModels.Editor.Sounds;
using Catrobat.IDE.Core.ViewModels.Editor.Costumes;
using Catrobat.IDE.Core.Models;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Editor.Sprites
{
    [TestClass]
    public class SpritesEditorViewModelTests
    {
        private Sprite _selectedSprite;
        private Program _currentProjectHeader; // TODO Program <-> Project-Header issue in Project Settings Action

        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.Register<DispatcherServiceTest>(TypeCreationMode.Normal);
        }

        [TestMethod, TestCategory("GatedTests")]
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

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void AddNewScriptBrickActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void CopyScriptBrickActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void DeleteScriptBrickActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void AddBroadcastMessageActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod, TestCategory("GatedTests")]
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

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void EditSoundActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void DeleteSoundActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void AddNewCostumeActionTest()
        {
            if (_selectedSprite != null)
                _selectedSprite.Name = "AddNewCostumeActionTest";

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
            viewModel.AddNewCostumeCommand.Execute(null);

            Assert.AreEqual("TestSpriteName", _selectedSprite.Name);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(NewCostumeSourceSelectionViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void EditCostumeActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void CopyCostumeActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void DeleteCostumeActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod, TestCategory("GatedTests")]
        public void ClearObjectSelectionActionTest()
        {
            var viewModel = new SpriteEditorViewModel
            {
                SelectedSprite = new Sprite { Name = "TestSpriteName" }
            };
            viewModel.ClearObjectsSelectionCommand.Execute(null);
            Assert.IsNull(viewModel.SelectedSprite);
        }

        [TestMethod, TestCategory("GatedTests")]
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

        [TestMethod, TestCategory("GatedTests")]
        public void ClearCostumesSelectionActionTest()
        {
            var viewModel = new SpriteEditorViewModel
            {
                SelectedSprite = new Sprite { Name = "TestSpriteName" }
            };
            var costume = new Costume();
            viewModel.SelectedCostumes.Add(costume);
            viewModel.ClearCostumesSelectionCommand.Execute(null);
            Assert.AreEqual(0, viewModel.SelectedCostumes.Count);
        }

        [TestMethod, TestCategory("GatedTests")]
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

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void StartPlayerActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod, TestCategory("GatedTests")]
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

        [TestMethod, TestCategory("GatedTests")]
        public void ProjectSettingsActionTest()
        {
            Messenger.Default.Register<GenericMessage<Program>>(this,
                 ViewModelMessagingToken.CurrentProjectHeaderChangedListener, CurrentProjectHeaderChangedMessageAction);

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
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.CurrentProjectChangedListener);
            viewModel.ProjectSettingsCommand.Execute(null);

            Assert.AreEqual("TestProgram", viewModel.CurrentProgram.Name); // message action test
            Assert.AreEqual("TestProgram", _currentProjectHeader.Name);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(ProjectSettingsViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void UndoActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void RedoActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void NothingItemHackActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod, TestCategory("GatedTests")]
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
