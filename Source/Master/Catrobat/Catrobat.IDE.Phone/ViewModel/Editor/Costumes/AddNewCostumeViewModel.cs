using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Data;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Phone.Controls.Misc;
using Catrobat.IDE.Phone.Views.Editor.Costumes;
using Catrobat.Paint;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Tasks;

namespace Catrobat.IDE.Phone.ViewModel.Editor.Costumes
{
    public class AddNewCostumeViewModel : ViewModelBase
    {
        #region Private Members

        private string _costumeName;
        private Sprite _receivedSelectedSprite;
        private ImageDimention _dimention;
        private ImageSizeEntry _selectedSize;
        private PortableImage _image;
        private Project _currentProject;

        #endregion

        #region Properties

        public Project CurrentProject
        {
            get { return _currentProject; }
            private set
            {
                _currentProject = value;
                RaisePropertyChanged(() => CurrentProject);
            }
        }

        public string CostumeName
        {
            get { return _costumeName; }
            set
            {
                if (value == _costumeName)
                {
                    return;
                }
                _costumeName = value;
                RaisePropertyChanged(() => CostumeName);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public PortableImage Image
        {
            get { return _image; }
            set
            {
                _image = value;
                RaisePropertyChanged(() => Image);
            }
        }

        public ImageDimention Dimention
        {
            get { return _dimention; }
            set
            {
                _dimention = value;

                int visibleCounter = 0;
                foreach (var size in ImageSizes)
                {
                    size.Dimention = Dimention;
                    if (size.IsVisible)
                        visibleCounter++;
                }

                switch (visibleCounter)
                {
                    case 1:
                        SelectedSize = ImageSizes[3];
                        break;
                    case 2:
                        SelectedSize = ImageSizes[3];
                        break;
                    case 3:
                        SelectedSize = ImageSizes[1];
                        break;
                    case 4:
                        SelectedSize = ImageSizes[1];
                        break;
                }
                RaisePropertyChanged(() => Dimention);
            }
        }

        public ImageSizeEntry SelectedSize
        {
            get { return _selectedSize; }
            set
            {
                _selectedSize = value;
                RaisePropertyChanged(() => SelectedSize);
            }
        }

        public ObservableCollection<ImageSizeEntry> ImageSizes { get; set; }

        #endregion

        #region Commands

        public RelayCommand OpenGalleryCommand { get; private set; }

        public RelayCommand OpenCameraCommand { get; private set; }

        public RelayCommand OpenPaintCommand { get; private set; }

        public RelayCommand SaveCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public RelayCommand ResetViewModelCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool SaveCommand_CanExecute()
        {
            return CostumeName != null && CostumeName.Length >= 2;
        }

        #endregion

        #region Actions

        private void OpenGalleryAction()
        {
            lock (this)
            {
                ServiceLocator.PictureService.ChoosePictureFromLibrary(PictureSuccess, PictureCanceled, PictureError);
            }
        }

        private void PictureSuccess(PortableImage image)
        {
            ServiceLocator.NavigationService.RemoveBackEntry();

            Image = image;
            CostumeName = AppResources.Editor_Image;
            Dimention = new ImageDimention { Height = image.Height, Width = image.Width };

            Deployment.Current.Dispatcher.BeginInvoke(() => ServiceLocator.NavigationService.NavigateTo(typeof(CostumeNameChooserView)));
        }

        private void PictureCanceled()
        {
            // No action here
        }

        private void PictureError()
        {
            ShowLoadingImageFailure();
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
            ServiceLocator.PictureService.DrawPicture(PictureSuccess, PictureCanceled, PictureError);

            //var newBitmap = new WriteableBitmap(
            //    ServiceLocator.SystemInformationService.ScreenWidth, ServiceLocator.SystemInformationService.ScreenHeight);

            //var task = new PaintLauncherTask { CurrentImage = newBitmap };
            //task.OnImageChanged += OnPaintLauncherImageChanged;
            //PaintLauncher.Launche(task);
        }

        //private async void OnPaintLauncherImageChanged(PortableImage image)
        //{
        //    try
        //    {
        //        Image = image;
        //        CostumeName = AppResources.Editor_Image;
        //        Dimention = new ImageDimention { Height = image.Height, Width = image.Width };

        //        Deployment.Current.Dispatcher.BeginInvoke(() => ServiceLocator.NavigationService.NavigateTo(typeof(CostumeNameChooserView)));
        //    }
        //    catch (Exception)
        //    {
        //        ShowLoadingImageFailure();
        //    }
        //}

        private async void SaveAction()
        {
            ServiceLocator.NavigationService.NavigateTo(typeof(CostumeSavingView));

            await Task.Run(() =>
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    var newDimention = new ImageDimention
                    {
                        Height = SelectedSize.NewHeight,
                        Width = SelectedSize.NewWidth
                    };
                    var costume = CostumeHelper.Save(Image, CostumeName, newDimention, CurrentProject.BasePath);
                    _receivedSelectedSprite.Costumes.Costumes.Add(costume);

                    ServiceLocator.NavigationService.RemoveBackEntry();
                    ServiceLocator.NavigationService.RemoveBackEntry();
                    ServiceLocator.NavigationService.NavigateBack();
                });
            });
        }

        private void CancelAction()
        {
            ServiceLocator.NavigationService.NavigateBack();
        }

        private void ReceiveSelectedSpriteMessageAction(GenericMessage<Sprite> message)
        {
            _receivedSelectedSprite = message.Content;
        }

        private void ResetViewModelAction()
        {
            ResetViewModel();
        }

        #endregion

        #region MessageActions
        private void CurrentProjectChangedMessageAction(GenericMessage<Project> message)
        {
            CurrentProject = message.Content;
        }

        #endregion

        public AddNewCostumeViewModel()
        {
            OpenGalleryCommand = new RelayCommand(OpenGalleryAction);
            OpenCameraCommand = new RelayCommand(OpenCameraAction);
            OpenPaintCommand = new RelayCommand(OpenPaintAction);
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

            Messenger.Default.Register<GenericMessage<Sprite>>(this,
                ViewModelMessagingToken.CurrentSpriteChangedListener, ReceiveSelectedSpriteMessageAction);

            Messenger.Default.Register<GenericMessage<Project>>(this,
                ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);

            InitImageSizes();
            if (IsInDesignMode)
                InitDesignData();
        }

        private void InitDesignData()
        {
            Dimention = new ImageDimention { Width = 500, Height = 500 };
            _selectedSize = ImageSizes[1];
        }

        private void PictureAvailable(Stream stream)
        {
            try
            {
                CostumeName = AppResources.Editor_Image;

                var image = new BitmapImage();
                image.SetSource(stream);
                Dimention = new ImageDimention { Height = image.PixelHeight, Width = image.PixelWidth };

                var writeableBitmap = new WriteableBitmap(image);
                var portableImage = new PortableImage(writeableBitmap.ToByteArray(), writeableBitmap.PixelWidth, writeableBitmap.PixelHeight);

                Image = portableImage;

                Deployment.Current.Dispatcher.BeginInvoke(() => ServiceLocator.NavigationService.NavigateTo(typeof(CostumeNameChooserView)));
            }
            catch (Exception)
            {
                ShowLoadingImageFailure();
            }
        }

        private void ShowLoadingImageFailure()
        {
            ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Editor_MessageBoxWrongImageFormatHeader,
                AppResources.Editor_MessageBoxWrongImageFormatText, WrongImageFormatResult, MessageBoxOptions.Ok);
        }

        private void WrongImageFormatResult(MessageboxResult result)
        {
            ServiceLocator.NavigationService.NavigateBack();
        }

        private void InitImageSizes()
        {
            ImageSizes = new ObservableCollection<ImageSizeEntry>
            {
                new ImageSizeEntry {Size = ImageSize.Small},
                new ImageSizeEntry {Size = ImageSize.Medium},
                new ImageSizeEntry {Size = ImageSize.Large},
                new ImageSizeEntry {Size = ImageSize.FullSize}
            };

            Dimention = new ImageDimention { Width = 0, Height = 0 };
        }

        private void ResetViewModel()
        {
            //CostumeName = AppResources.Editor_Image;

            //InitImageSizes();
            //_builder = null;
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}