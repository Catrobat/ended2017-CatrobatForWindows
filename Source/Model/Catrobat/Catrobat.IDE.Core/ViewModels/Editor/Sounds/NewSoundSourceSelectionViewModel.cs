using System.IO;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml;
using Catrobat.IDE.Core.Xml.XmlObjects;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Editor.Sounds
{
    public class NewSoundSourceSelectionViewModel : ViewModelBase
    {
        #region Private Members

        private XmlSprite _receivedSelectedSprite;

        #endregion

        #region Properties

        #endregion

        #region Commands

        public RelayCommand AudioLibraryCommand { get; private set; }

        public RelayCommand RecorderCommand { get; private set; }

        #endregion

        #region Actions

        private async void AudioLibraryAction()
        {
            var spriteMessage = new GenericMessage<XmlSprite>(_receivedSelectedSprite);
            Messenger.Default.Send(spriteMessage, ViewModelMessagingToken.CurrentSpriteChangedListener);

            var result = await ServiceLocator.SoundService.CreateSoundFromMediaLibrary(_receivedSelectedSprite);

            if (result.Status == SoundServiceStatus.Success)
            {
                var message = new GenericMessage<Stream>(result.Result);
                Messenger.Default.Send(message, ViewModelMessagingToken.SoundStreamListener);

                ServiceLocator.NavigationService.NavigateTo<SoundNameChooserViewModel>();
            }
        }

        private async void RecorderAction()
        {
            var result = await ServiceLocator.SoundService.CreateSoundFromRecorder(_receivedSelectedSprite);

            if (result.Status == SoundServiceStatus.Success)
            {
                var message = new GenericMessage<Stream>(result.Result);
                Messenger.Default.Send(message, ViewModelMessagingToken.SoundStreamListener);
            
                ServiceLocator.NavigationService.NavigateTo<SoundNameChooserViewModel>();
            }
        }

        private void ReceiveSelectedSpriteMessageAction(GenericMessage<XmlSprite> message)
        {
            _receivedSelectedSprite = message.Content;
        }

        protected override void GoBackAction()
        {
            base.GoBackAction();
        }

        #endregion

        public NewSoundSourceSelectionViewModel()
        {
            AudioLibraryCommand = new RelayCommand(AudioLibraryAction);
            RecorderCommand = new RelayCommand(RecorderAction);

            Messenger.Default.Register<GenericMessage<XmlSprite>>(this, ViewModelMessagingToken.CurrentSpriteChangedListener, ReceiveSelectedSpriteMessageAction);
        }
    }
}