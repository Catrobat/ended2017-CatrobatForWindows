using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Editor.Looks
{
    public class LookSavingViewModel : ViewModelBase
    {
        #region Private Members

        private PortableImage _image;

        #endregion

        #region Properties

        public PortableImage Image
        {
            get { return _image; }
            set
            {
                _image = value;
                RaisePropertyChanged(() => Image);
            }
        }

        #endregion

        #region Commands

        
        #endregion

        #region CommandCanExecute

      
        #endregion

        #region Actions



        #endregion

        #region MessageActions

        private void LookImageReceivedMessageAction(GenericMessage<PortableImage> message)
        {
            ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                Image = message.Content;
            });
        }

        #endregion

        public LookSavingViewModel()
        {
            SkipAndNavigateTo = typeof(SpriteEditorViewModel);

            Messenger.Default.Register<GenericMessage<PortableImage>>(this,
                ViewModelMessagingToken.LookImageToSaveListener, LookImageReceivedMessageAction);
        }
    }
}