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

        public RelayCommand OpenGalleryCommand { get; private set; }

        public RelayCommand OpenCameraCommand { get; private set; }

        public RelayCommand OpenPaintCommand { get; private set; }

        #endregion

        #region CommandCanExecute

      
        #endregion

        #region Actions

        private void OpenGalleryAction()
        {
            lock (this)
            {
                ServiceLocator.PictureService.ChoosePictureFromLibrary(PictureSuccess, PictureCanceled, PictureError);
            }
        }

        private void OpenCameraAction()
        {
            lock (this)
            {
                ServiceLocator.PictureService.TakePicture(PictureSuccess, PictureCanceled, PictureError);
            }
        }

        private void OpenPaintAction()
        {
            lock (this)
            {
                ServiceLocator.PictureService.DrawPicture(PictureSuccess, PictureCanceled, PictureError);
                ServiceLocator.NavigationService.RemoveBackEntry();
            }
        }

        protected override void GoBackAction()
        {
            base.GoBackAction();
        }

        #endregion

        #region MessageActions


        #endregion

        public NewCostumeSourceSelectionViewModel()
        {
            OpenGalleryCommand = new RelayCommand(OpenGalleryAction);
            OpenCameraCommand = new RelayCommand(OpenCameraAction);
            OpenPaintCommand = new RelayCommand(OpenPaintAction);
        }


        #region ImageFunctions

        private static void PictureSuccess(PortableImage image)
        {
            var message = new GenericMessage<PortableImage>(image);
            Messenger.Default.Send(message, ViewModelMessagingToken.CostumeImageListener);

            ServiceLocator.DispatcherService.RunOnMainThread(() => 
                ServiceLocator.NavigationService.NavigateTo<CostumeNameChooserViewModel>());
        }

        private void PictureCanceled()
        {
            // No action here
        }

        private void PictureError()
        {
            ShowLoadingImageFailure();
        }

        private void ShowLoadingImageFailure()
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