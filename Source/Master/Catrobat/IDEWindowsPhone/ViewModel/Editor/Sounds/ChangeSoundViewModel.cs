using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Sounds;
using Catrobat.Core.Storage;
using Catrobat.IDEWindowsPhone.Annotations;
using Catrobat.IDEWindowsPhone.Views.Editor.Sounds;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using GalaSoft.MvvmLight.Command;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;
using Catrobat.IDECommon.Resources.Editor;
using Microsoft.Phone.Tasks;
using Catrobat.IDEWindowsPhone.Misc;
using GalaSoft.MvvmLight.Messaging;
using Catrobat.Core.Objects.Costumes;

namespace Catrobat.IDEWindowsPhone.ViewModel.Editor.Sounds
{
    public class ChangeSoundViewModel : ViewModelBase
    {
        #region Private Members

        private Sound _receivedSound;
        private string _soundName;

        #endregion

        #region Properties

        public Sound ReceivedSound
        {
            get { return _receivedSound; }
            set
            {
                if (value == _receivedSound) return;
                _receivedSound = value;
                RaisePropertyChanged("ReceivedSound");
            }
        }

        public string SoundName
        {
            get { return _soundName; }
            set
            {
                if (value == _soundName) return;
                _soundName = value;
                RaisePropertyChanged("SoundName");
                RaisePropertyChanged("IsSoundNameValid");
            }
        }

        public bool IsSoundNameValid
        {
            get
            {
                return SoundName != null && SoundName.Length >= 2;
            }
        }

        #endregion

        #region Commands

        public RelayCommand EditSoundCommand
        {
            get;
            private set;
        }

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

        #endregion

        #region Actions

        private void SaveAction()
        {
            ReceivedSound.Name = SoundName;

            ResetViewModel();
            Navigation.NavigateBack();
        }

        private void CancelAction()
        {
            ResetViewModel();
            Navigation.NavigateBack();
        }

        private void EditSoundAction()
        {
            //TODO: navigate to App that can change sounds
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
            SaveCommand = new RelayCommand(SaveAction);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<Sound>>(this, ViewModelMessagingToken.SoundNameListener, ChangeSoundNameMessageAction);
        }

        public void ResetViewModel()
        {
            SoundName = "";
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}