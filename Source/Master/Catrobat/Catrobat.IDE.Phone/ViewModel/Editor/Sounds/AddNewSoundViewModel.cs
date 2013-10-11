using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Phone.Views.Editor.Sounds;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Phone.ViewModel.Editor.Sounds
{
    public class AddNewSoundViewModel : ViewModelBase
    {
        #region Private Members

        private Sprite _receivedSelectedSprite;

        #endregion

        #region Properties

        #endregion

        #region Commands

        public RelayCommand AudioLibraryCommand { get; private set; }

        public RelayCommand RecorderCommand { get; private set; }

        public RelayCommand ResetViewModelCommand { get; private set; }

        #endregion

        #region Actions

        private void AudioLibraryAction()
        {
            var message = new GenericMessage<Sprite>(_receivedSelectedSprite);
            Messenger.Default.Send<GenericMessage<Sprite>>(message, ViewModelMessagingToken.CurrentSpriteChangedListener);

            //ServiceLocator.NavigationService.NavigateTo(typeof (AudioLibrary));
        }

        private void RecorderAction()
        {
            var message = new GenericMessage<Sprite>(_receivedSelectedSprite);
            Messenger.Default.Send<GenericMessage<Sprite>>(message, ViewModelMessagingToken.CurrentSpriteChangedListener);

            ServiceLocator.NavigationService.NavigateTo(typeof (SoundRecorderView));
        }

        private void ReceiveSelectedSpriteMessageAction(GenericMessage<Sprite> message)
        {
            _receivedSelectedSprite = message.Content;
        }

        private void ResetViewModelAction()
        {
            ResetViewModel();
        }

        #endregion

        public AddNewSoundViewModel()
        {
            AudioLibraryCommand = new RelayCommand(AudioLibraryAction);
            RecorderCommand = new RelayCommand(RecorderAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

            Messenger.Default.Register<GenericMessage<Sprite>>(this, ViewModelMessagingToken.CurrentSpriteChangedListener, ReceiveSelectedSpriteMessageAction);
        }

        private void ResetViewModel() {}
    }
}