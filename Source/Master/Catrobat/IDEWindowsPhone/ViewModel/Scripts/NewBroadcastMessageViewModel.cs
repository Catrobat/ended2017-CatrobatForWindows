using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.IDEWindowsPhone.Misc;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catrobat.IDEWindowsPhone.ViewModel.Scripts
{
    public class NewBroadcastMessageViewModel : ViewModelBase
    {
        #region private Members

        private string _broadcastMessage;
        private DataObject _broadcastObject;
        private Project _currentProject;

        #endregion

        #region Properties

        public string BroadcastMessage
        {
            get
            {
                return _broadcastMessage;
            }
            set
            {
                if (_broadcastMessage != value)
                {
                    _broadcastMessage = value;

                    RaisePropertyChanged("BroadcastMessage");
                    SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        #endregion

        #region Commands

        public RelayCommand SaveCommand
        {
            get;
            private set;
        }

        public RelayCommand CancelCommand
        {
            get;
            private set;
        }

        public RelayCommand ResetViewModelCommand
        {
            get;
            private set;
        }

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
            GenericMessage<string> message = new GenericMessage<string>(BroadcastMessage);
            Messenger.Default.Send<GenericMessage<string>>(message, ViewModelMessagingToken.BroadcastMessageListener);

            if (_broadcastObject is BroadcastScript)
                (_broadcastObject as BroadcastScript).ReceivedMessage = BroadcastMessage;
            if (_broadcastObject is BroadcastBrick)
                (_broadcastObject as BroadcastBrick).BroadcastMessage = BroadcastMessage;
            if (_broadcastObject is BroadcastWaitBrick)
                (_broadcastObject as BroadcastWaitBrick).BroadcastMessage = BroadcastMessage;

            Navigation.NavigateBack();
        }

        private void ReceiveBroadcastObjectAction(GenericMessage<DataObject> message)
        {
            _broadcastObject = message.Content;
        }

        private void CancelAction()
        {
            Navigation.NavigateBack();
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
