using System.Threading.Tasks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.Tests.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Looks;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Editor.Looks
{
    [TestClass]
    public class NewLookSourceSelectionViewModelTests
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

        //[TestMethod, TestCategory("ViewModels.Editor")] //  // TODO: fix test takes very long time on server
        //public async Task OpenGalleryActionTest()
        //{
        //    Messenger.Default.Register<GenericMessage<PortableImage>>(this,
        //        ViewModelMessagingToken.LookImageListener, LookImageReceivedMessageAction);

        //    var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
        //    navigationService.PageStackCount = 0;
        //    navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
        //    navigationService.CurrentView = typeof(NewLookSourceSelectionViewModel);

        //    var viewModel = new NewLookSourceSelectionViewModel();

        //    var pictureService = (PictureServiceTest) ServiceLocator.PictureService;
        //    var notificationService = (NotificationServiceTest) ServiceLocator.NotifictionService;

        //    //success
        //    pictureService.NextMethodAction = PictureServiceStatus.Success;
        //    _imageToSave = null;

        //    await viewModel.OpenGalleryCommand.ExecuteAsync(null);

        //    Assert.IsNotNull(_imageToSave);
        //    Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
        //    Assert.AreEqual(typeof(LookNameChooserViewModel), navigationService.CurrentView);
        //    Assert.AreEqual(1, navigationService.PageStackCount);

        //    //cancel
        //    //no action

        //    //error
        //    pictureService.NextMethodAction = PictureServiceStatus.Error;
        //    notificationService.SentMessageBoxes = 0;
        //    notificationService.SentToastNotifications = 0;
        //    notificationService.NextMessageboxResult = MessageboxResult.Ok;
        //    navigationService.PageStackCount = 1;
        //    navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
        //    navigationService.CurrentView = typeof(NewLookSourceSelectionViewModel);
        //    _imageToSave = null;

        //    await viewModel.OpenGalleryCommand.ExecuteAsync(null);

        //    Assert.IsNull(_imageToSave);
        //    Assert.AreEqual(1, notificationService.SentMessageBoxes);
        //    Assert.AreEqual(0, notificationService.SentToastNotifications);
        //    Assert.AreEqual(MessageBoxOptions.Ok, notificationService.LastMessageboxOption);
        //    Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
        //    Assert.AreEqual(null, navigationService.CurrentView);
        //    Assert.AreEqual(0, navigationService.PageStackCount);
        //}

        //[TestMethod, TestCategory("ViewModels.Editor")] //  // TODO: fix test takes very long time on server
        //public async Task OpenCameraActionTest()
        //{
        //    Messenger.Default.Register<GenericMessage<PortableImage>>(this,
        //        ViewModelMessagingToken.LookImageListener, LookImageReceivedMessageAction);

        //    var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
        //    navigationService.PageStackCount = 0;
        //    navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
        //    navigationService.CurrentView = typeof(NewLookSourceSelectionViewModel);

        //    var viewModel = new NewLookSourceSelectionViewModel();

        //    var pictureService = (PictureServiceTest)ServiceLocator.PictureService;
        //    var notificationService = (NotificationServiceTest)ServiceLocator.NotifictionService;

        //    //success
        //    pictureService.NextMethodAction = PictureServiceStatus.Success;
        //    _imageToSave = null;

        //    await viewModel.OpenCameraCommand.ExecuteAsync(null);

        //    Assert.IsNotNull(_imageToSave);
        //    Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
        //    Assert.AreEqual(typeof(LookNameChooserViewModel), navigationService.CurrentView);
        //    Assert.AreEqual(1, navigationService.PageStackCount);

        //    //cancel
        //    //no action

        //    //error
        //    pictureService.NextMethodAction = PictureServiceStatus.Error;
        //    notificationService.SentMessageBoxes = 0;
        //    notificationService.SentToastNotifications = 0;
        //    notificationService.NextMessageboxResult = MessageboxResult.Ok;
        //    navigationService.PageStackCount = 1;
        //    navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
        //    navigationService.CurrentView = typeof(NewLookSourceSelectionViewModel);
        //    _imageToSave = null;

        //    await viewModel.OpenCameraCommand.ExecuteAsync(null);

        //    Assert.IsNull(_imageToSave);
        //    Assert.AreEqual(1, notificationService.SentMessageBoxes);
        //    Assert.AreEqual(0, notificationService.SentToastNotifications);
        //    Assert.AreEqual(MessageBoxOptions.Ok, notificationService.LastMessageboxOption);
        //    Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
        //    Assert.AreEqual(null, navigationService.CurrentView);
        //    Assert.AreEqual(0, navigationService.PageStackCount);
        //}

        //[TestMethod, TestCategory("ViewModels.Editor")] //  // TODO: fix test takes very long time on server
        //public async Task OpenPaintActionTest()
        //{
        //    Messenger.Default.Register<GenericMessage<PortableImage>>(this,
        //        ViewModelMessagingToken.LookImageListener, LookImageReceivedMessageAction);

        //    var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
        //    navigationService.PageStackCount = 0;
        //    navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
        //    navigationService.CurrentView = typeof(NewLookSourceSelectionViewModel);

        //    var viewModel = new NewLookSourceSelectionViewModel();

        //    var pictureService = (PictureServiceTest)ServiceLocator.PictureService;
        //    var notificationService = (NotificationServiceTest)ServiceLocator.NotifictionService;

        //    //success
        //    pictureService.NextMethodAction = PictureServiceStatus.Success;
        //    _imageToSave = null;

        //    await viewModel.OpenPaintCommand.ExecuteAsync(null);

        //    Assert.IsNotNull(_imageToSave);
        //    Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
        //    Assert.AreEqual(typeof(LookNameChooserViewModel), navigationService.CurrentView);
        //    Assert.AreEqual(0, navigationService.PageStackCount);

        //    //cancel
        //    //no action

        //    //error
        //    pictureService.NextMethodAction = PictureServiceStatus.Error;
        //    notificationService.SentMessageBoxes = 0;
        //    notificationService.SentToastNotifications = 0;
        //    notificationService.NextMessageboxResult = MessageboxResult.Ok;
        //    navigationService.PageStackCount = 2;
        //    navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
        //    navigationService.CurrentView = typeof(NewLookSourceSelectionViewModel);
        //    _imageToSave = null;

        //    await viewModel.OpenPaintCommand.ExecuteAsync(null);

        //    Assert.IsNull(_imageToSave);
        //    Assert.AreEqual(1, notificationService.SentMessageBoxes);
        //    Assert.AreEqual(0, notificationService.SentToastNotifications);
        //    Assert.AreEqual(MessageBoxOptions.Ok, notificationService.LastMessageboxOption);
        //    Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
        //    Assert.AreEqual(null, navigationService.CurrentView);
        //    Assert.AreEqual(0, navigationService.PageStackCount);
        //}


        //private void LookImageReceivedMessageAction(GenericMessage<PortableImage> message)
        //{
        //    _imageToSave = message.Content;
        //}
    }
}
