﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Resources.Localization;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModel.Editor.Costumes
{
    public class CostumeNameChooserViewModel : ViewModelBase
    {
        #region Private Members

        private string _costumeName = AppResources.Editor_Image;
        private Sprite _receivedSelectedSprite;
        private ImageDimension _dimension;
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

        public ImageDimension Dimension
        {
            get { return _dimension; }
            set
            {
                _dimension = value;

                int visibleCounter = 0;
                foreach (var size in ImageSizes)
                {
                    size.Dimension = Dimension;
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
                RaisePropertyChanged(() => Dimension);
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

        private async void SaveAction()
        {
            var message = new GenericMessage<PortableImage>(Image);
            Messenger.Default.Send(message, ViewModelMessagingToken.CostumeImageToSaveListener);

            ServiceLocator.NavigationService.NavigateTo(typeof(CostumeSavingViewModel));

            await Task.Run(() => ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                var newDimention = new ImageDimension
                {
                    Height = SelectedSize.NewHeight,
                    Width = SelectedSize.NewWidth
                };
                var costume = CostumeHelper.Save(Image, CostumeName, newDimention, CurrentProject.BasePath);
                _receivedSelectedSprite.Costumes.Costumes.Add(costume);

                ServiceLocator.NavigationService.RemoveBackEntry();
                ServiceLocator.NavigationService.RemoveBackEntry();
                ServiceLocator.NavigationService.NavigateBack();
            }));
        }

        private void CancelAction()
        {
            ServiceLocator.NavigationService.NavigateBack();
        }
       

        private void ResetViewModelAction()
        {
            ResetViewModel();
        }

        #endregion

        #region MessageActions

        private void ReceiveSelectedSpriteMessageAction(GenericMessage<Sprite> message)
        {
            _receivedSelectedSprite = message.Content;
        }

        private void CurrentProjectChangedMessageAction(GenericMessage<Project> message)
        {
            CurrentProject = message.Content;
        }

        private void CostumeImageReceivedMessageAction(GenericMessage<PortableImage> message)
        {
            Image = message.Content;
            Dimension = new ImageDimension { Height = Image.Height, Width = Image.Width };
        }

        #endregion

        public CostumeNameChooserViewModel()
        {
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

            Messenger.Default.Register<GenericMessage<Sprite>>(this,
                ViewModelMessagingToken.CurrentSpriteChangedListener, ReceiveSelectedSpriteMessageAction);

            Messenger.Default.Register<GenericMessage<Project>>(this,
                ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);

            Messenger.Default.Register<GenericMessage<PortableImage>>(this,
                ViewModelMessagingToken.CostumeImageListener, CostumeImageReceivedMessageAction);

            InitImageSizes();
            if (IsInDesignMode)
                InitDesignData();
        }

        private void InitDesignData()
        {
            Dimension = new ImageDimension { Width = 500, Height = 500 };
            _selectedSize = ImageSizes[1];
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

            Dimension = new ImageDimension { Width = 0, Height = 0 };
        }

        private void ResetViewModel()
        {
            //CostumeName = AppResources.Editor_Image;

            //InitImageSizes();
            //_builder = null;
        }
    }
}