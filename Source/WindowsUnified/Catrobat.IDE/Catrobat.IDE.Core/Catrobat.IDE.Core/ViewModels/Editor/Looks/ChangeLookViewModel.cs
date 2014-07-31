using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.Helpers;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Editor.Looks
{
    public class ChangeLookViewModel : ViewModelBase
    {
        #region Private Members

        private Look _receivedLook;
        private string _lookName;
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

        public Look ReceivedLook
        {
            get { return _receivedLook; }
            set
            {
                if (ReferenceEquals(value, _receivedLook))
                {
                    return;
                }
                _receivedLook = value;
                RaisePropertyChanged(() => ReceivedLook);
            }
        }

        public string LookName
        {
            get { return _lookName; }
            set
            {
                if (value == _lookName)
                {
                    return;
                }
                _lookName = value;
                RaisePropertyChanged(() => LookName);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Commands

        public AsyncRelayCommand EditLookCommand { get; private set; }

        public RelayCommand SaveCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool SaveCommand_CanExecute()
        {
            return LookName != null && LookName.Length >= 2;
        }

        #endregion

        #region Actions

        private void SaveAction()
        {
            ReceivedLook.Name = LookName;
            base.GoBackAction();
        }

        private void CancelAction()
        {
            base.GoBackAction();
        }

        private async Task EditLookAction()
        {
            await ServiceLocator.PictureService.DrawPictureAsync(ReceivedLook.Image);
            //var result = await ServiceLocator.PictureService.DrawPictureAsync(ReceivedLook.Image);

            //if (result.Status == PictureServiceStatus.Success)
            //{
            //    await LookHelper.ReplaceImageInStorage(CurrentProject, ReceivedLook, result.Image);

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

        private void ChangeLookNameMessageAction(GenericMessage<Look> message)
        {
            ReceivedLook = message.Content;
            LookName = ReceivedLook.Name;
        }

        #endregion

        public ChangeLookViewModel()
        {
            EditLookCommand = new AsyncRelayCommand(EditLookAction, () => { /* no action  */ });
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<Program>>(this, 
                ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);
            Messenger.Default.Register<GenericMessage<Look>>(this, 
                ViewModelMessagingToken.LookListener, ChangeLookNameMessageAction);
        }

        private void ResetViewModel()
        {
            LookName = "";
        }
    }
}