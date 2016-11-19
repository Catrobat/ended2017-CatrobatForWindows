using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;

namespace Catrobat.IDE.Core.ViewModels.Editor.Sounds
{
    public class ChangeSoundViewModel : ViewModelBase
    {
        #region Private Members

        private Sprite _receivedSelectedSprite;
        private Sound _receivedSound;
        private string _soundName;
        private Program _currentProgram;


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

        public Sound ReceivedSound
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

        private async void SaveAction()
        {
            string validName = await ServiceLocator.ContextService.ConvertToValidFileName(SoundName);
            if (validName != ReceivedSound.Name)
            {
                List<string> nameList = new List<string>();
                foreach (var soundItem in _receivedSelectedSprite.Sounds)
                {
                    nameList.Add(soundItem.Name);
                }
                SoundName = await ServiceLocator.ContextService.FindUniqueName(validName, nameList);
                ReceivedSound.Name = SoundName;
                CurrentProgram.Save();
            }
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
        private void ReceiveSelectedSpriteMessageAction(GenericMessage<Sprite> message)
        {
            _receivedSelectedSprite = message.Content;
        }

        private void ChangeSoundNameMessageAction(GenericMessage<Sound> message)
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

            Messenger.Default.Register<GenericMessage<Sprite>>(this,
                ViewModelMessagingToken.CurrentSpriteChangedListener, ReceiveSelectedSpriteMessageAction);
            Messenger.Default.Register<GenericMessage<Sound>>(this, 
                ViewModelMessagingToken.SoundNameListener, ChangeSoundNameMessageAction);
            Messenger.Default.Register<GenericMessage<Program>>(this,
                ViewModelMessagingToken.CurrentProgramChangedListener, CurrentProgramChangedMessageAction);
        }

        private void ResetViewModel()
        {
            SoundName = "";
        }

        private void CurrentProgramChangedMessageAction(GenericMessage<Program> message)
        {
            CurrentProgram = message.Content;
        }
    }
}