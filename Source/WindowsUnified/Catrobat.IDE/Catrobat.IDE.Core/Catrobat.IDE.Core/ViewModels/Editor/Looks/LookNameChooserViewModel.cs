using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities.Helpers;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Catrobat.IDE.Core.ViewModels.Editor.Looks
{
    public class LookNameChooserViewModel : ViewModelBase
    {
        #region Private Members

        private static ObservableCollection<ImageSizeEntry> _allImageSizes =
            new ObservableCollection<ImageSizeEntry>
            {
                new ImageSizeEntry {Size = ImageSize.Small},
                new ImageSizeEntry {Size = ImageSize.Medium},
                new ImageSizeEntry {Size = ImageSize.Large},
                new ImageSizeEntry {Size = ImageSize.FullSize}
            };

        private string _lookName = "";
        private Sprite _receivedSelectedSprite;
        private ImageDimension _dimension;
        private ImageSizeEntry _selectedSize;
        private PortableImage _image;
        private Program _currentProject;

        #endregion

        #region Properties

        public Program CurrentProject
        {
            get { return _currentProject; }
            private set
            {
                _currentProject = value;
                                ServiceLocator.DispatcherService.RunOnMainThread(() => RaisePropertyChanged(() => CurrentProject));
            }
        }

        public string LookName
        {
            get { return _lookName; }
            set
            {
                if (value == _lookName)
                    return;

                _lookName = value;
                RaisePropertyChanged(() => LookName);
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

        public ImageDimension Dimension
        {
            get { return _dimension; }
            set
            {
                InitImageSizes();
                _dimension = value;

                int visibleCounter = 0;
                foreach (var size in _allImageSizes)
                {
                    size.Dimension = Dimension;
                    if (size.IsVisible)
                        visibleCounter++;
                }

                UpdateAvailableImageSizes();

                //switch (visibleCounter)
                //{
                //    case 1:
                //        SelectedSize = AllImageSizes[3];
                //        break;
                //    case 2:
                //        SelectedSize = AllImageSizes[3];
                //        break;
                //    case 3:
                //        SelectedSize = AllImageSizes[1];
                //        break;
                //    case 4:
                //        SelectedSize = AllImageSizes[1];
                //        break;
                //}


                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    RaisePropertyChanged(() => Dimension);
                    RaisePropertyChanged(() => ImageSizes);

                    SelectedSize = visibleCounter >= 2 ? ImageSizes[1] : ImageSizes[0]; 
                });

            }
        }

        public ImageSizeEntry SelectedSize
        {
            get { return _selectedSize; }
            set
            {
                _selectedSize = value;

                RaisePropertyChanged(() => SelectedSize);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        private ObservableCollection<ImageSizeEntry> _imageSizes;
        public ObservableCollection<ImageSizeEntry> ImageSizes
        {
            get { return _imageSizes; }
        }

        public void UpdateAvailableImageSizes()
        {
            var availableSizes = new ObservableCollection<ImageSizeEntry>();
            foreach (var entry in _allImageSizes)
                if (entry.IsVisible)
                    availableSizes.Add(entry);

            _imageSizes = availableSizes;
        }

        #endregion

        #region Commands

        public AsyncRelayCommand SaveCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool SaveCommand_CanExecute()
        {
            var saveEnabled = LookName != null && LookName.Length >= 2;
            saveEnabled &= SelectedSize != null;

            return saveEnabled;
        }

        #endregion

        #region Actions

        private async Task SaveAction()
        {
            var message = new GenericMessage<PortableImage>(Image);
            Messenger.Default.Send(message, ViewModelMessagingToken.LookImageToSaveListener);

            ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                ServiceLocator.NavigationService.RemoveBackEntry();
                ServiceLocator.NavigationService.NavigateBack(this.GetType());
            });

            //ServiceLocator.DispatcherService.RunOnMainThread(() =>
            //    ServiceLocator.NavigationService.NavigateTo<LookSavingViewModel>());

            var newDimention = new ImageDimension
            {
                Height = SelectedSize.NewHeight,
                Width = SelectedSize.NewWidth
            };

            var look = await LookHelper.Save(Image, LookName, newDimention, CurrentProject.BasePath);

            ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                _receivedSelectedSprite.Looks.Add(look);
                //ServiceLocator.NavigationService.RemoveBackEntryForPlatform(NavigationPlatform.WindowsPhone);
                //ServiceLocator.NavigationService.RemoveBackEntryForPlatform(NavigationPlatform.WindowsPhone);
                ResetViewModel();
                //ServiceLocator.NavigationService.NavigateBack(
                //    new List<NavigationPlatform>{NavigationPlatform.WindowsStore});
            });
        }

        private void CancelAction()
        {
            ServiceLocator.NavigationService.RemoveBackEntry();
            GoBackAction();
        }


        protected override void GoBackAction()
        {
            ResetViewModel();
            base.GoBackAction();
        }

        #endregion

        #region MessageActions

        private void ReceiveSelectedSpriteMessageAction(GenericMessage<Sprite> message)
        {
            _receivedSelectedSprite = message.Content;
        }

        private void CurrentProgramChangedMessageAction(GenericMessage<Program> message)
        {
            CurrentProject = message.Content;
        }

        private void LookImageReceivedMessageAction(GenericMessage<PortableImage> message)
        {
            Image = message.Content;
            Dimension = new ImageDimension { Height = Image.Height, Width = Image.Width };
        }

        #endregion

        public LookNameChooserViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SaveAction, () => { /* no action  */ }, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<Sprite>>(this,
                ViewModelMessagingToken.CurrentSpriteChangedListener, ReceiveSelectedSpriteMessageAction);

            Messenger.Default.Register<GenericMessage<Program>>(this,
                ViewModelMessagingToken.CurrentProgramChangedListener, CurrentProgramChangedMessageAction);

            Messenger.Default.Register<GenericMessage<PortableImage>>(this,
                ViewModelMessagingToken.LookImageListener, LookImageReceivedMessageAction);

            //if (IsInDesignMode)
            //    InitDesignData();
        }

        private void InitDesignData()
        {
            Dimension = new ImageDimension { Width = 500, Height = 500 };
            _selectedSize = ImageSizes[1];
        }

        private void InitImageSizes()
        {

            //Dimension = new ImageDimension { Width = 0, Height = 0 };
        }

        private void ResetViewModel()
        {
            InitImageSizes();

            //LookName = AppResources.Editor_Image;

            //InitImageSizes();
            //_builder = null;
        }
    }
}