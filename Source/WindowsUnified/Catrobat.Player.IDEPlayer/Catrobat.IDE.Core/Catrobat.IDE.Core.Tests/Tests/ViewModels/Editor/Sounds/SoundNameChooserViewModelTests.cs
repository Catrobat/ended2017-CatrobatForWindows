using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Sounds;
using Catrobat.IDE.Core.Models;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Editor.Sounds
{
    [TestClass]
    public class SoundNameChooserViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
        }

        [TestMethod, TestCategory("ViewModels.Editor")]
        public void NavigateToTest()
        {
            var viewModel = new SoundNameChooserViewModel
            {
                SoundName = "TestSoundName"
            };
            viewModel.NavigateTo();

            Assert.AreEqual("", viewModel.SoundName);
        }

        [TestMethod, TestCategory("ViewModels.Editor"), TestCategory("ExcludeGated")]
        public void SaveActionTest()
        {
            //TODO
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod, TestCategory("ViewModels.Editor")]
        public void CancelActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(SoundNameChooserViewModel);

            var viewModel = new SoundNameChooserViewModel
            {
                SoundName = "TestSoundName"
            };
            viewModel.CancelCommand.Execute(null);

            Assert.AreEqual("", viewModel.SoundName);
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
            navigationService.CurrentView = typeof(SoundNameChooserViewModel);

            var viewModel = new SoundNameChooserViewModel();
            viewModel.CancelCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
