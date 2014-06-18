using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Costumes;
using Catrobat.IDE.Tests.Services;
using Catrobat.IDE.Tests.Services.Storage;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.ViewModel.Editor.Costumes
{
    [TestClass]
    public class CostumeNameChooserViewModelTests
    {
        private PortableImage _imageToSave;

        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.Register<StorageFactoryTest>(TypeCreationMode.Normal);
            ServiceLocator.Register<StorageTest>(TypeCreationMode.Normal);
            ServiceLocator.Register<ImageResizeServiceTest>(TypeCreationMode.Normal);
            ServiceLocator.Register<SensorServiceTest>(TypeCreationMode.Normal);
            ServiceLocator.Register<DispatcherServiceTest>(TypeCreationMode.Normal);
        }

        [TestMethod] // , TestCategory("GatedTests") // TODO: fix test takes very long time on server
        public async Task SaveActionTest()
        {
            _imageToSave = null;
            Messenger.Default.Register<GenericMessage<PortableImage>>(this,
                ViewModelMessagingToken.CostumeImageToSaveListener, CostumeImageReceivedMessageAction);
            
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 2;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(CostumeNameChooserViewModel);

            var viewModel = new CostumeNameChooserViewModel
            {
                SelectedSize = new ImageSizeEntry {NewHeight = 100, NewWidth = 100},
                CostumeName = "TestCostume"
            };

            var project = new Project {Name = "TestProject"};
            var messageContext = new GenericMessage<Project>(project);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.CurrentProjectChangedListener);

            var sprite = new Sprite();
            var messageContext2 = new GenericMessage<Sprite>(sprite);
            Messenger.Default.Send(messageContext2, ViewModelMessagingToken.CurrentSpriteChangedListener);
            
            var messageContext3 = new GenericMessage<PortableImage>(new PortableImage());
            Messenger.Default.Send(messageContext3, ViewModelMessagingToken.CostumeImageListener);

            await viewModel.SaveCommand.ExecuteAsync(null);

            Assert.IsNotNull(_imageToSave);
            Assert.AreEqual(1, sprite.Costumes.Count);
            Assert.IsNotNull(sprite.Costumes[0].Image);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod] // , TestCategory("GatedTests") // TODO: fix test takes very long time on server
        public void CancelActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 2;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(CostumeNameChooserViewModel);

            var viewModel = new CostumeNameChooserViewModel();

            viewModel.CancelCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod] // , TestCategory("GatedTests") // TODO: fix test takes very long time on server
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(CostumeNameChooserViewModel);

            var viewModel = new CostumeNameChooserViewModel();

            viewModel.GoBackCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        private void CostumeImageReceivedMessageAction(GenericMessage<PortableImage> message)
        {
            _imageToSave = message.Content;
        }
    }
}
