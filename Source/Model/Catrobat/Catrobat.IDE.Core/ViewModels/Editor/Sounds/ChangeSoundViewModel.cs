using Catrobat.IDE.Core.Xml;
using Catrobat.IDE.Core.Xml.XmlObjects;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Editor.Sounds
{
    public class ChangeSoundViewModel : ViewModelBase
    {
        #region Private Members

        private XmlSound _receivedSound;
        private string _soundName;

        #endregion

        #region Properties

        public XmlSound ReceivedSound
        {
            get { return _receivedSound; }
            set
            {
                if (value == _receivedSound)
                {
                    return;
                }
                _receivedSound = value;
                RaisePropertyChanged(() => ReceivedSound);
            }
        }

        public string SoundName
        {
            get { return _soundName; }
            set
            {
                if (value == _soundName)
                {
                    return;
                }
                _soundName = value;
                RaisePropertyChanged(() => SoundName);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Commands

        public RelayCommand EditSoundCommand { get; private set; }

        public RelayCommand SaveCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool SaveCommand_CanExecute()
        {
            return SoundName != null && SoundName.Length >= 2;
        }

        #endregion

        #region Actions

        private void SaveAction()
        {
            ReceivedSound.Name = SoundName;
            base.GoBackAction();
        }

        private void CancelAction()
        {
            base.GoBackAction();
        }

        private void EditSoundAction()
        {
            //TODO: navigate to App that can change sounds
        }

        protected override void GoBackAction()
        {
            ResetViewModel();
            base.GoBackAction();
        }

        #endregion

        #region MessageActions

        private void ChangeSoundNameMessageAction(GenericMessage<XmlSound> message)
        {
            ReceivedSound = message.Content;
            SoundName = ReceivedSound.Name;
        }

        #endregion

        public ChangeSoundViewModel()
        {
            EditSoundCommand = new RelayCommand(EditSoundAction);
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<XmlSound>>(this, ViewModelMessagingToken.SoundNameListener, ChangeSoundNameMessageAction);
        }

        private void ResetViewModel()
        {
            SoundName = "";
        }
    }
}