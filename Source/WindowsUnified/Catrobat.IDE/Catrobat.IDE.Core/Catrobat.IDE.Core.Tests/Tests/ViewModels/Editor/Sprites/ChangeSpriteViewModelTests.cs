using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Catrobat.IDE.Core.Models;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Editor.Sprites
{
    [TestClass]
    public class ChangeSpriteViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
        }
        
        [TestMethod]
        public void SaveActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ChangeSpriteViewModel);

            var viewModel = new ChangeSpriteViewModel
            {
                SelectedSprite = new Sprite { Name = "TestSpriteName" },
                SpriteName = "TestNewSpriteName"
            };
            viewModel.SaveCommand.Execute(null);

            Assert.AreEqual("TestNewSpriteName", viewModel.SpriteName);
            Assert.AreEqual(viewModel.SelectedSprite.Name, viewModel.SpriteName);
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
            navigationService.CurrentView = typeof(ChangeSpriteViewModel);

            var viewModel = new ChangeSpriteViewModel
            {
                SelectedSprite = new Sprite { Name = "TestSpriteName" },
                SpriteName = ""
            };
            viewModel.CancelCommand.Execute(null);

            Assert.AreEqual("TestSpriteName", viewModel.SpriteName);
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
            navigationService.CurrentView = typeof(ChangeSpriteViewModel);

            var viewModel = new ChangeSpriteViewModel
            {
                SelectedSprite = new Sprite { Name = "TestSpriteName" },
                SpriteName = ""
            };
            viewModel.GoBackCommand.Execute(null);

            Assert.AreEqual("TestSpriteName", viewModel.SpriteName);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
