using System.Threading.Tasks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor.Costumes;
using Catrobat.IDE.Tests.Services;
using Catrobat.IDE.Tests.Services.Storage;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.ViewModel.Editor.Costumes
{
    [TestClass]
    public class NewCostumeSourceSelectionViewModelTests
    {
        private PortableImage _imageToSave;

        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.Register<PictureServiceTest>(TypeCreationMode.Normal);
            ServiceLocator.Register<NotificationServiceTest>(TypeCreationMode.Normal);
            ServiceLocator.Register<DispatcherServiceTest>(TypeCreationMode.Normal);
        }

        [TestMethod] //, TestCategory("GatedTests")]
        public async Task OpenGalleryActionTest()
        {
            Messenger.Default.Register<GenericMessage<PortableImage>>(this,
                ViewModelMessagingToken.CostumeImageListener, CostumeImageReceivedMessageAction);

            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 0;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(NewCostumeSourceSelectionViewModel);

            var viewModel = new NewCostumeSourceSelectionViewModel();

            var pictureService = (PictureServiceTest) ServiceLocator.PictureService;
            var notificationService = (NotificationServiceTest) ServiceLocator.NotifictionService;

            //success
            pictureService.NextMethodAction = PictureServiceStatus.Success;
            _imageToSave = null;

            await viewModel.OpenGalleryCommand.ExecuteAsync(null);

            Assert.IsNotNull(_imageToSave);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(CostumeNameChooserViewModel), navigationService.CurrentView);
            Assert.AreEqual(1, navigationService.PageStackCount);

            //cancel
            //no action

            //error
            pictureService.NextMethodAction = PictureServiceStatus.Error;
            notificationService.SentMessageBoxes = 0;
            notificationService.SentToastNotifications = 0;
            notificationService.NextMessageboxResult = MessageboxResult.Ok;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(NewCostumeSourceSelectionViewModel);
            _imageToSave = null;

            await viewModel.OpenGalleryCommand.ExecuteAsync(null);

            Assert.IsNull(_imageToSave);
            Assert.AreEqual(1, notificationService.SentMessageBoxes);
            Assert.AreEqual(0, notificationService.SentToastNotifications);
            Assert.AreEqual(MessageBoxOptions.Ok, notificationService.LastMessageboxOption);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod] //, TestCategory("GatedTests")]
        public async Task OpenCameraActionTest()
        {
            Messenger.Default.Register<GenericMessage<PortableImage>>(this,
                ViewModelMessagingToken.CostumeImageListener, CostumeImageReceivedMessageAction);

            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 0;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(NewCostumeSourceSelectionViewModel);

            var viewModel = new NewCostumeSourceSelectionViewModel();

            var pictureService = (PictureServiceTest)ServiceLocator.PictureService;
            var notificationService = (NotificationServiceTest)ServiceLocator.NotifictionService;

            //success
            pictureService.NextMethodAction = PictureServiceStatus.Success;
            _imageToSave = null;

            await viewModel.OpenCameraCommand.ExecuteAsync(null);

            Assert.IsNotNull(_imageToSave);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(CostumeNameChooserViewModel), navigationService.CurrentView);
            Assert.AreEqual(1, navigationService.PageStackCount);

            //cancel
            //no action

            //error
            pictureService.NextMethodAction = PictureServiceStatus.Error;
            notificationService.SentMessageBoxes = 0;
            notificationService.SentToastNotifications = 0;
            notificationService.NextMessageboxResult = MessageboxResult.Ok;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(NewCostumeSourceSelectionViewModel);
            _imageToSave = null;

            await viewModel.OpenCameraCommand.ExecuteAsync(null);

            Assert.IsNull(_imageToSave);
            Assert.AreEqual(1, notificationService.SentMessageBoxes);
            Assert.AreEqual(0, notificationService.SentToastNotifications);
            Assert.AreEqual(MessageBoxOptions.Ok, notificationService.LastMessageboxOption);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod] //, TestCategory("GatedTests")]
        public async Task OpenPaintActionTest()
        {
            Messenger.Default.Register<GenericMessage<PortableImage>>(this,
                ViewModelMessagingToken.CostumeImageListener, CostumeImageReceivedMessageAction);

            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 0;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(NewCostumeSourceSelectionViewModel);

            var viewModel = new NewCostumeSourceSelectionViewModel();

            var pictureService = (PictureServiceTest)ServiceLocator.PictureService;
            var notificationService = (NotificationServiceTest)ServiceLocator.NotifictionService;

            //success
            pictureService.NextMethodAction = PictureServiceStatus.Success;
            _imageToSave = null;

            await viewModel.OpenPaintCommand.ExecuteAsync(null);

            Assert.IsNotNull(_imageToSave);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(CostumeNameChooserViewModel), navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);

            //cancel
            //no action

            //error
            pictureService.NextMethodAction = PictureServiceStatus.Error;
            notificationService.SentMessageBoxes = 0;
            notificationService.SentToastNotifications = 0;
            notificationService.NextMessageboxResult = MessageboxResult.Ok;
            navigationService.PageStackCount = 2;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(NewCostumeSourceSelectionViewModel);
            _imageToSave = null;

            await viewModel.OpenPaintCommand.ExecuteAsync(null);

            Assert.IsNull(_imageToSave);
            Assert.AreEqual(1, notificationService.SentMessageBoxes);
            Assert.AreEqual(0, notificationService.SentToastNotifications);
            Assert.AreEqual(MessageBoxOptions.Ok, notificationService.LastMessageboxOption);
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
