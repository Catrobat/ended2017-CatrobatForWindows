using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.Helpers;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Editor.Costumes
{
    public class ChangeCostumeViewModel : ViewModelBase
    {
        #region Private Members

        private Costume _receivedCostume;
        private string _costumeName;
        private Program _currentProject;

        #endregion

        #region Properties

        public Program CurrentProject
        {
            get { return _currentProject; }
            set
            {
                _currentProject = value;
                                ServiceLocator.DispatcherService.RunOnMainThread(() => RaisePropertyChanged(() => CurrentProject));
            }
        }

        public Costume ReceivedCostume
        {
            get { return _receivedCostume; }
            set
            {
                if (ReferenceEquals(value, _receivedCostume))
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

        public AsyncRelayCommand EditCostumeCommand { get; private set; }

        public RelayCommand SaveCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

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
            base.GoBackAction();
        }

        private void CancelAction()
        {
            base.GoBackAction();
        }

        private async Task EditCostumeAction()
        {
            await ServiceLocator.PictureService.DrawPictureAsync(ReceivedCostume.Image);
            //var result = await ServiceLocator.PictureService.DrawPictureAsync(ReceivedCostume.Image);

            //if (result.Status == PictureServiceStatus.Success)
            //{
            //    await CostumeHelper.ReplaceImageInStorage(CurrentProject, ReceivedCostume, result.Image);

            //    ServiceLocator.DispatcherService.RunOnMainThread(() => {
            //        ServiceLocator.NavigationService.RemoveBackEntry();
            //        base.GoBackAction();
            //    });
            //}
        }

        protected override void GoBackAction()
        {
            ResetViewModel();
            base.GoBackAction();
        }

        #endregion

        #region MessageActions

        private void CurrentProjectChangedMessageAction(GenericMessage<Program> message)
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
            EditCostumeCommand = new AsyncRelayCommand(EditCostumeAction, () => { /* no action  */ });
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<Program>>(this, 
                ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);
            Messenger.Default.Register<GenericMessage<Costume>>(this, 
                ViewModelMessagingToken.CostumeListener, ChangeCostumeNameMessageAction);
        }

        private void ResetViewModel()
        {
            CostumeName = "";
        }
    }
}