using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Bricks;
using Catrobat.IDE.Core.CatrobatObjects.Scripts;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModel.Editor.Scripts
{
    public class NewBroadcastMessageViewModel : ViewModelBase
    {
        #region private Members

        private string _broadcastMessage;
        private DataObject _broadcastObject;

        #endregion

        #region Properties

        public string BroadcastMessage
        {
            get { return _broadcastMessage; }
            set
            {
                if (_broadcastMessage != value)
                {
                    _broadcastMessage = value;

                    RaisePropertyChanged(() => BroadcastMessage);
                    SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        #endregion

        #region Commands

        public RelayCommand SaveCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public RelayCommand ResetViewModelCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool SaveCommand_CanExecute()
        {
            return BroadcastMessage != null && BroadcastMessage.Length >= 2;
        }

        #endregion

        #region Actions

        private void SaveAction()
        {
            var message = new GenericMessage<string>(BroadcastMessage);
            Messenger.Default.Send<GenericMessage<string>>(message, ViewModelMessagingToken.BroadcastMessageListener);

            if (_broadcastObject is BroadcastScript)
            {
                (_broadcastObject as BroadcastScript).ReceivedMessage = BroadcastMessage;
            }
            if (_broadcastObject is BroadcastBrick)
            {
                (_broadcastObject as BroadcastBrick).BroadcastMessage = BroadcastMessage;
            }
            if (_broadcastObject is BroadcastWaitBrick)
            {
                (_broadcastObject as BroadcastWaitBrick).BroadcastMessage = BroadcastMessage;
            }

            ServiceLocator.NavigationService.NavigateBack();
        }

        private void ReceiveBroadcastObjectAction(GenericMessage<DataObject> message)
        {
            _broadcastObject = message.Content;
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

        public NewBroadcastMessageViewModel()
        {
            // Commands
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

            Messenger.Default.Register<GenericMessage<DataObject>>(this, ViewModelMessagingToken.BroadcastObjectListener, ReceiveBroadcastObjectAction);
        }

        private void ResetViewModel()
        {
            BroadcastMessage = "";
        }
    }
}