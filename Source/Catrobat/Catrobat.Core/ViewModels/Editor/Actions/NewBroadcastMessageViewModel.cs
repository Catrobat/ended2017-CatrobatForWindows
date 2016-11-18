using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Scripts;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Editor.Actions
{
    public class NewBroadcastMessageViewModel : ViewModelBase
    {
        #region private Members

        private Program _currentProgram;
        private BroadcastMessage _broadcastMessage;
        private ModelBase _broadcastObject;

        #endregion

        #region Properties

        public Program CurrentProgram
        {
            get { return _currentProgram; }
            private set
            {
                _currentProgram = value;
            }
        }

        public BroadcastMessage BroadcastMessage
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

        #endregion

        #region CommandCanExecute

        private bool SaveCommand_CanExecute()
        {
            return BroadcastMessage != null && BroadcastMessage.Content != null && BroadcastMessage.Content.Length >= 2;
        }

        #endregion

        #region Actions

        private void SaveAction()
        {
            var message = new GenericMessage<BroadcastMessage>(BroadcastMessage);
            Messenger.Default.Send(message, ViewModelMessagingToken.BroadcastMessageListener);

            if (_broadcastObject is BroadcastReceivedScript)
            {
                (_broadcastObject as BroadcastReceivedScript).Message = BroadcastMessage;
            }
            if (_broadcastObject is BroadcastSendBrick)
            {
                (_broadcastObject as BroadcastSendBrick).Message = BroadcastMessage;
            }
            if (_broadcastObject is BroadcastSendBlockingBrick)
            {
                (_broadcastObject as BroadcastSendBlockingBrick).Message = BroadcastMessage;
            }

            CurrentProgram.Save();

            base.GoBackAction();
        }

        private void ReceiveBroadcastObjectAction(GenericMessage<ModelBase> message)
        {
            _broadcastObject = message.Content;
        }

        private void CancelAction()
        {
            base.GoBackAction();
        }

        protected override void GoBackAction()
        {
            ResetViewModel();
            base.GoBackAction();
        }

        #endregion

        private void CurrentProgramChangedMessageAction(GenericMessage<Program> message)
        {
            CurrentProgram = message.Content;
        }

        public NewBroadcastMessageViewModel()
        {
            // Commands
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<ModelBase>>(this, 
                ViewModelMessagingToken.BroadcastObjectListener, ReceiveBroadcastObjectAction);

            Messenger.Default.Register<GenericMessage<Program>>(this,
                ViewModelMessagingToken.CurrentProgramChangedListener, CurrentProgramChangedMessageAction);
        }

        private void ResetViewModel()
        {
            BroadcastMessage = null;
        }
    }
}