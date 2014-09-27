using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.Tests.Services.Common;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Sounds;
using Catrobat.IDE.Core.Models;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Editor.Sounds
{
    [TestClass]
    public class ChangeSoundViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.UnRegisterAll();
            ServiceLocator.Register<ContextServiceTest>(TypeCreationMode.Lazy);
        }

        [TestMethod, TestCategory("ViewModels.Editor")]
        public void SaveActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ChangeSoundViewModel);

            var viewModel = new ChangeSoundViewModel();
            var sound = new Sound
            {
                Name = "TestSoundName"
            };
            var sound2 = new Sound
            {
                Name = "TestSoundName2"
            };
            var sprite = new Sprite();
            sprite.Sounds.Add(sound);
            sprite.Sounds.Add(sound2);
            var messageContext = new GenericMessage<Sprite>(sprite);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.CurrentSpriteChangedListener);

            var messageContext2 = new GenericMessage<Sound>(sound);
            Messenger.Default.Send(messageContext2, ViewModelMessagingToken.SoundNameListener);
            viewModel.SoundName = "TestNewSoundName";
            viewModel.SaveCommand.Execute(null);

            Assert.AreEqual("TestNewSoundName", viewModel.ReceivedSound.Name);
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
            navigationService.CurrentView = typeof(ChangeSoundViewModel);

            var viewModel = new ChangeSoundViewModel();
            viewModel.CancelCommand.Execute(null);

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
            navigationService.CurrentView = typeof(ChangeSoundViewModel);

            var viewModel = new ChangeSoundViewModel
            {
                SoundName = "TestSoundName"
            };
            viewModel.GoBackCommand.Execute(null);

            Assert.AreEqual("", viewModel.SoundName);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
