using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Xml;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;
using Catrobat.IDE.Core.Xml.XmlObjects.Scripts;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Editor.Scripts
{
    public class NewBroadcastMessageViewModel : ViewModelBase
    {
        #region private Members

        private string _broadcastMessage;
        private XmlObject _broadcastObject;

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

            if (_broadcastObject is XmlBroadcastScript)
            {
                (_broadcastObject as XmlBroadcastScript).ReceivedMessage = BroadcastMessage;
            }
            if (_broadcastObject is XmlBroadcastBrick)
            {
                (_broadcastObject as XmlBroadcastBrick).BroadcastMessage = BroadcastMessage;
            }
            if (_broadcastObject is XmlBroadcastWaitBrick)
            {
                (_broadcastObject as XmlBroadcastWaitBrick).BroadcastMessage = BroadcastMessage;
            }

            base.GoBackAction();
        }

        private void ReceiveBroadcastObjectAction(GenericMessage<XmlObject> message)
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

        public NewBroadcastMessageViewModel()
        {
            // Commands
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<XmlObject>>(this, ViewModelMessagingToken.BroadcastObjectListener, ReceiveBroadcastObjectAction);
        }

        private void ResetViewModel()
        {
            BroadcastMessage = "";
        }
    }
}