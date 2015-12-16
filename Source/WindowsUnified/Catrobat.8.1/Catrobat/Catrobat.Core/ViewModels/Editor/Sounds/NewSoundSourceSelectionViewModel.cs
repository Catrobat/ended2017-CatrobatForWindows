using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Editor.Sounds
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
            //var spriteMessage = new GenericMessage<Sprite>(_receivedSelectedSprite);
            //Messenger.Default.Send(spriteMessage, ViewModelMessagingToken.CurrentSpriteChangedListener);

            ServiceLocator.SoundService.CreateSoundFromMediaLibrary(_receivedSelectedSprite);

            //if (result.Status == SoundServiceStatus.Success)
            //{
            //    var message = new GenericMessage<Stream>(result.Result);
            //    Messenger.Default.Send(message, ViewModelMessagingToken.SoundStreamListener);

            //    ServiceLocator.NavigationService.NavigateTo<SoundNameChooserViewModel>();
            //}
        }

        private void RecorderAction()
        {
            ServiceLocator.SoundService.CreateSoundFromRecorder(_receivedSelectedSprite);

            //if (result.Status == SoundServiceStatus.Success)
            //{
            //    var message = new GenericMessage<Stream>(result.Result);
            //    Messenger.Default.Send(message, ViewModelMessagingToken.SoundStreamListener);
            
            //    ServiceLocator.NavigationService.NavigateTo<SoundNameChooserViewModel>();
            //}
        }
        #endregion

        #region Message Actions
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