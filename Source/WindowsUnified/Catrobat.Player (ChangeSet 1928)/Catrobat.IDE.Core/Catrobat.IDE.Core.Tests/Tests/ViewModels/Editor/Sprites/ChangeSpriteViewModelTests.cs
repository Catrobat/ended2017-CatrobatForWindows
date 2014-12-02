using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Catrobat.IDE.Core.Models;
using GalaSoft.MvvmLight.Messaging;

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

        [TestMethod, TestCategory("ViewModels.Editor")]
        public void SaveActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ChangeSpriteViewModel);

            Sprite sprite = new Sprite { Name = "TestSpriteName" };
            Sprite sprite2 = new Sprite { Name = "TestSpriteName2" };
            
            var viewModel = new ChangeSpriteViewModel
            {
                SelectedSprite = sprite,
                SpriteName = "TestNewSpriteName"
            };

            var program = new Program
            {
                Name = "TestProgramName",
                Description = "TestProgramDescription"
            };
            program.Sprites.Add(sprite);
            program.Sprites.Add(sprite2);
            var messageContext = new GenericMessage<Program>(program);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.CurrentProgramChangedListener);

            viewModel.SaveCommand.Execute(null);

            Assert.AreEqual("TestNewSpriteName", viewModel.SpriteName);
            Assert.AreEqual(viewModel.SelectedSprite.Name, viewModel.SpriteName);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("ViewModels.Editor")]
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

        [TestMethod, TestCategory("ViewModels.Editor")]
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
