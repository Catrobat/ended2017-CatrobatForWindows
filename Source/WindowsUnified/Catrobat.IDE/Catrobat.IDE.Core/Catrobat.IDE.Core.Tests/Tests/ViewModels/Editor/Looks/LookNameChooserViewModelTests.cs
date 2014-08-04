using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.Tests.Services.Storage;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Looks;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Editor.Looks
{
    [TestClass]
    public class LookNameChooserViewModelTests
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

        [TestMethod, TestCategory("ExcludeGated")]
        public async Task SaveActionTest()
        {
            _imageToSave = null;
            Messenger.Default.Register<GenericMessage<PortableImage>>(this,
                ViewModelMessagingToken.LookImageToSaveListener, LookImageReceivedMessageAction);
            
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 2;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(LookNameChooserViewModel);

            var viewModel = new LookNameChooserViewModel
            {
                SelectedSize = new ImageSizeEntry {NewHeight = 100, NewWidth = 100},
                LookName = "TestLook"
            };

            var project = new Program {Name = "TestProject"};
            var messageContext = new GenericMessage<Program>(project);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.CurrentProgramChangedListener);

            var sprite = new Sprite();
            var messageContext2 = new GenericMessage<Sprite>(sprite);
            Messenger.Default.Send(messageContext2, ViewModelMessagingToken.CurrentSpriteChangedListener);
            
            var messageContext3 = new GenericMessage<PortableImage>(new PortableImage());
            Messenger.Default.Send(messageContext3, ViewModelMessagingToken.LookImageListener);

            await viewModel.SaveCommand.ExecuteAsync(null);

            Assert.IsNotNull(_imageToSave);
            Assert.AreEqual(1, sprite.Looks.Count);
            Assert.IsNotNull(sprite.Looks[0].Image);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void CancelActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 2;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(LookNameChooserViewModel);

            var viewModel = new LookNameChooserViewModel();

            viewModel.CancelCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(LookNameChooserViewModel);

            var viewModel = new LookNameChooserViewModel();

            viewModel.GoBackCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        private void LookImageReceivedMessageAction(GenericMessage<PortableImage> message)
        {
            _imageToSave = message.Content;
        }
    }
}
