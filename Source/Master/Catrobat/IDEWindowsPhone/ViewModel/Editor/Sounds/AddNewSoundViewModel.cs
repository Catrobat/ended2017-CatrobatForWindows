﻿using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Sounds;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.Views.Editor.Sounds;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catrobat.IDEWindowsPhone.ViewModel.Editor.Sounds
{
    public class AddNewSoundViewModel : ViewModelBase
    {
        #region Private Members

        private Sprite _receivedSelectedSprite;

        #endregion

        #region Properties

        #endregion

        #region Commands

        public RelayCommand AudioLibraryCommand
        {
            get;
            private set;
        }

        public RelayCommand RecorderCommand
        {
            get;
            private set;
        }

        #endregion

        #region Actions

        private void AudioLibraryAction()
        {
            GenericMessage<Sprite> message = new GenericMessage<Sprite>(_receivedSelectedSprite);
            Messenger.Default.Send<GenericMessage<Sprite>>(message, ViewModelMessagingToken.SelectedSpriteListener);

            Navigation.NavigateTo(typeof(AudioLibrary));
        }

        private void RecorderAction()
        {
            GenericMessage<Sprite> message = new GenericMessage<Sprite>(_receivedSelectedSprite);
            Messenger.Default.Send<GenericMessage<Sprite>>(message, ViewModelMessagingToken.SelectedSpriteListener);

            Navigation.NavigateTo(typeof(SoundRecorderView));
        }

        private void ReceiveSelectedSpriteMessageAction(GenericMessage<Sprite> message)
        {
            _receivedSelectedSprite = message.Content;
        }

        #endregion


        public AddNewSoundViewModel()
        {
            AudioLibraryCommand  = new RelayCommand(AudioLibraryAction);
            RecorderCommand = new RelayCommand(RecorderAction);

            Messenger.Default.Register<GenericMessage<Sprite>>(this, ViewModelMessagingToken.SelectedSpriteListener, ReceiveSelectedSpriteMessageAction);
        }

        public void ResetViewModel()
        {
            
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}