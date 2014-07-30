using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Catrobat.IDE.Core.Models;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Editor.Sprites
{
    [TestClass]
    public class SpritesViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
        }

        [TestMethod, TestCategory("GatedTests")]
        public void AddNewSpriteActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(SpritesViewModel);

            var viewModel = new SpritesViewModel();
            viewModel.AddNewSpriteCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(AddNewSpriteViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void EditSpriteActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(SpritesViewModel);

            var viewModel = new SpritesViewModel
            {
                SelectedSprites = new ObservableCollection<Sprite>()
            };
            viewModel.EditSpriteCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(SpriteEditorViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void CopySpriteActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void DeleteSpriteActionTest()
        {
            //TODO test deleting sprites from collection
            Assert.AreEqual(0, "test not fully implemented");
            var notificationService = (NotificationServiceTest)ServiceLocator.NotifictionService;
            notificationService.SentMessageBoxes = 0;
            notificationService.SentToastNotifications = 0;
            notificationService.NextMessageboxResult = MessageboxResult.Ok;

            var viewModel = new SpritesViewModel
            {
                SelectedSprites = new ObservableCollection<Sprite>()
            };
            //viewModel.DeleteSpriteCommand.Execute(null);

            Assert.AreEqual(1, notificationService.SentMessageBoxes);
            Assert.AreEqual(0, notificationService.SentToastNotifications);
            Assert.AreEqual("Delete object", notificationService.LastNotificationTitle);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void ClearObjectSelectionActionTest()
        {
            var viewModel = new SpritesViewModel
            {
                SelectedSprites = new ObservableCollection<Sprite>
                {
                    new Sprite()
                }
            };
            viewModel.ClearObjectsSelectionCommand.Execute(null);

            Assert.AreEqual(0, viewModel.SelectedSprites.Count);
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void StartPlayerActionTest()
        {
            //TODO refactor this test
            Assert.AreEqual(0, "test not implemented");
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

        [TestMethod, TestCategory("GatedTests")]
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(SpritesViewModel);

            var viewModel = new SpritesViewModel
            {
                SelectedSprites = new ObservableCollection<Sprite>()
            };
            viewModel.GoBackCommand.Execute(null);

            Assert.AreEqual(0, viewModel.SelectedSprites.Count);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
