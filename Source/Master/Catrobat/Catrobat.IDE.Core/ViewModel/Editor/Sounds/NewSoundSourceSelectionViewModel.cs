using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModel.Editor.Sounds
{
    public class NewSoundSourceSelectionViewModel : ViewModelBase
    {
        #region Private Members

        private Sprite _receivedSelectedSprite;

        #endregion

        #region Properties

        #endregion

        #region Commands

        public RelayCommand AudioLibraryCommand { get; private set; }

        public RelayCommand RecorderCommand { get; private set; }

        #endregion

        #region Actions

        private void AudioLibraryAction()
        {
            var message = new GenericMessage<Sprite>(_receivedSelectedSprite);
            Messenger.Default.Send(message, ViewModelMessagingToken.CurrentSpriteChangedListener);

            //ServiceLocator.NavigationService.NavigateTo(typeof (AudioLibrary));
        }

        private void RecorderAction()
        {
            var message = new GenericMessage<Sprite>(_receivedSelectedSprite);
            Messenger.Default.Send(message, ViewModelMessagingToken.CurrentSpriteChangedListener);

            ServiceLocator.NavigationService.NavigateTo(typeof(SoundRecorderViewModel));
        }

        private void ReceiveSelectedSpriteMessageAction(GenericMessage<Sprite> message)
        {
            _receivedSelectedSprite = message.Content;
        }

        #endregion

        public NewSoundSourceSelectionViewModel()
        {
            AudioLibraryCommand = new RelayCommand(AudioLibraryAction);
            RecorderCommand = new RelayCommand(RecorderAction);

            Messenger.Default.Register<GenericMessage<Sprite>>(this, ViewModelMessagingToken.CurrentSpriteChangedListener, ReceiveSelectedSpriteMessageAction);
        }
    }
}