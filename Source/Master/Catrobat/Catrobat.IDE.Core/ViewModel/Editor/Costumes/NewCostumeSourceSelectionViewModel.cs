using System.Threading.Tasks;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModel.Editor.Costumes
{
    public class NewCostumeSourceSelectionViewModel : ViewModelBase
    {
        #region Private Members

        #endregion

        #region Properties



        #endregion

        #region Commands

        public AsyncRelayCommand OpenGalleryCommand { get; private set; }

        public AsyncRelayCommand OpenCameraCommand { get; private set; }

        public AsyncRelayCommand OpenPaintCommand { get; private set; }

        #endregion

        #region CommandCanExecute


        #endregion

        #region Actions

        public async Task OpenGalleryAction()
        {
            var result = await ServiceLocator.PictureService.ChoosePictureFromLibraryAsync();
            if (result.Status == PictureServiceStatus.Success)
                PictureSuccess(result.Image);
            else if (result.Status == PictureServiceStatus.Error)
                PictureError();
        }

        public async Task OpenCameraAction()
        {
            var result = await ServiceLocator.PictureService.TakePictureAsync();
            if (result.Status == PictureServiceStatus.Success)
                PictureSuccess(result.Image);
            else if (result.Status == PictureServiceStatus.Error)
                PictureError();
        }

        private async Task OpenPaintAction()
        {
            var result = await ServiceLocator.PictureService.DrawPictureAsync();

            ServiceLocator.DispatcherService.RunOnMainThread(() => 
                ServiceLocator.NavigationService.RemoveBackEntry());
            
            if (result.Status == PictureServiceStatus.Success)
                PictureSuccess(result.Image);
            else if (result.Status == PictureServiceStatus.Error)
                PictureError();

            
        }

        #endregion

        #region MessageActions


        #endregion

        public NewCostumeSourceSelectionViewModel()
        {
            OpenGalleryCommand = new AsyncRelayCommand(OpenGalleryAction, () => { /* no action  */ });
            OpenCameraCommand = new AsyncRelayCommand(OpenCameraAction, () => { /* no action  */ });
            OpenPaintCommand = new AsyncRelayCommand(OpenPaintAction, () => { /* no action  */ });
        }


        #region ImageFunctions

        private static void PictureSuccess(PortableImage image)
        {
            var message = new GenericMessage<PortableImage>(image);
            Messenger.Default.Send(message, ViewModelMessagingToken.CostumeImageListener);

            ServiceLocator.DispatcherService.RunOnMainThread(() =>
                ServiceLocator.NavigationService.NavigateTo<CostumeNameChooserViewModel>());
        }

        private void PictureError()
        {
            ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Editor_MessageBoxWrongImageFormatHeader,
                AppResources.Editor_MessageBoxWrongImageFormatText, WrongImageFormatResult, MessageBoxOptions.Ok);
        }

        private void WrongImageFormatResult(MessageboxResult result)
        {
            base.GoBackAction();
        }

        #endregion
    }
}