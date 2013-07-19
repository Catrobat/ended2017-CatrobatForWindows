using System;
using System.Windows.Media.Imaging;
using Catrobat.Core.Objects.Costumes;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.Paint;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDEWindowsPhone.ViewModel.Editor.Costumes
{
    public class ChangeCostumeViewModel : ViewModelBase
    {
        #region Private Members

        private Costume _receivedCostume;
        private string _costumeName;

        #endregion

        #region Properties

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
                RaisePropertyChanged("ReceivedCostume");
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
                RaisePropertyChanged("CostumeName");
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
            Navigation.NavigateBack();
        }

        private void CancelAction()
        {
            Navigation.NavigateBack();
        }

        private void EditCostumeAction()
        {
            PaintLauncher.CurrentImage = ReceivedCostume.Image as BitmapImage;
            PaintLauncher.Launche();
        }

        private void ChangeCostumeNameMessageAction(GenericMessage<Costume> message)
        {
            ReceivedCostume = message.Content;
            CostumeName = ReceivedCostume.Name;
        }

        private void ResetViewModelAction()
        {
            ResetViewModel();
        }

        #endregion

        public ChangeCostumeViewModel()
        {
            EditCostumeCommand = new RelayCommand(EditCostumeAction);
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

            Messenger.Default.Register<GenericMessage<Costume>>(this, ViewModelMessagingToken.CostumeNameListener, ChangeCostumeNameMessageAction);
        }

        private void ResetViewModel()
        {
            CostumeName = "";
        }
    }
}