using System;
using System.Diagnostics;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Costumes;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModel.Editor.Costumes
{
    public class ChangeCostumeViewModel : ViewModelBase
    {
        #region Private Members

        private Costume _receivedCostume;
        private string _costumeName;
        private Project _currentProject;

        #endregion

        #region Properties

        public Project CurrentProject
        {
            get { return _currentProject; }
            set
            {
                _currentProject = value;
                RaisePropertyChanged(() => CurrentProject);
            }
        }

        public Costume ReceivedCostume
        {
            get { return _receivedCostume; }
            set
            {
                if (value == _receivedCostume)
                {
                    return;
                }
                _receivedCostume = value;
                RaisePropertyChanged(() => ReceivedCostume);
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

        #endregion

        #region Commands

        public RelayCommand EditCostumeCommand { get; private set; }

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

        private void SaveAction()
        {
            ReceivedCostume.Name = CostumeName;
            ServiceLocator.NavigationService.NavigateBack();
        }

        private void CancelAction()
        {
            ServiceLocator.NavigationService.NavigateBack();
        }

        private void EditCostumeAction()
        {
            
            ServiceLocator.PictureService.DrawPicture(ChangedPictureSuccess, () => {/* No action here */}, () => {/* No action here */},
                ReceivedCostume.Image);
            ServiceLocator.NavigationService.RemoveBackEntry();

            //var writeableBitmap = new WriteableBitmap(ReceivedCostume.Image.Width, ReceivedCostume.Image.Height);
            //writeableBitmap.FromByteArray(ReceivedCostume.Image.Data);

            //var task = new PaintLauncherTask { CurrentImage = new WriteableBitmap(writeableBitmap) };
            //task.OnImageChanged += OnPaintLauncherTaskImageChanged;
            //PaintLauncher.Launche(task);
        }

        private void ChangedPictureSuccess(PortableImage image)
        {
            try
            {
                
                CostumeHelper.ReplaceImageInStorage(CurrentProject, ReceivedCostume, image);
            }
            catch (Exception)
            {
                // TODO: fix error on changing the same costume twice in a short time

                if (Debugger.IsAttached)
                    Debugger.Break();
            }

            ServiceLocator.NavigationService.RemoveBackEntry();
            ServiceLocator.NavigationService.NavigateBack();
        }

        //private void OnPaintLauncherTaskImageChanged(PaintLauncherTask task)
        //{
        //    try
        //    {
        //        var writeableBitmap = new WriteableBitmap(task.CurrentImage);
        //        var portableImage = new PortableImage(writeableBitmap.ToByteArray(), writeableBitmap.PixelWidth,
        //            writeableBitmap.PixelHeight);
        //        CostumeHelper.ReplaceImageInStorage(CurrentProject, ReceivedCostume, portableImage);
        //    }
        //    catch (Exception)
        //    {
        //        // TODO: fix error on changing the same costume twice in a short time

        //        if (Debugger.IsAttached)
        //            Debugger.Break();
        //    }

        //    ServiceLocator.NavigationService.RemoveBackEntry();
        //    ServiceLocator.NavigationService.NavigateBack();
        //}

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

        private void ChangeCostumeNameMessageAction(GenericMessage<Costume> message)
        {
            ReceivedCostume = message.Content;
            CostumeName = ReceivedCostume.Name;
        }

        #endregion

        public ChangeCostumeViewModel()
        {
            EditCostumeCommand = new RelayCommand(EditCostumeAction);
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

            Messenger.Default.Register<GenericMessage<Project>>(this, ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);
            Messenger.Default.Register<GenericMessage<Costume>>(this, ViewModelMessagingToken.CostumeNameListener, ChangeCostumeNameMessageAction);
        }

        private void ResetViewModel()
        {
            CostumeName = "";
        }
    }
}