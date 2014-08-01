using Catrobat.IDE.Core.CatrobatObjects;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Main
{
    public class TileGeneratorViewModel : ViewModelBase
    {
        #region private Members

        private LocalProjectHeader _pinProjectHeader;

        #endregion

        #region Properties
        public LocalProjectHeader PinProjectHeader
        {
            get
            {
                return _pinProjectHeader;
            }
            set
            {
                if (value == _pinProjectHeader) return;

                _pinProjectHeader = value;

                RaisePropertyChanged(() => PinProjectHeader);
            }
        }

        #endregion

        #region Commands


        #endregion

        #region Actions

        protected override void GoBackAction()
        {
            ResetViewModel();
            base.GoBackAction();
        }

        #endregion

        #region MessageActions

        private void PinProjectHeaderChangedAction(GenericMessage<LocalProjectHeader> message)
        {
            PinProjectHeader = message.Content;
        }

        #endregion

        public TileGeneratorViewModel()
        {
            Messenger.Default.Register<GenericMessage<LocalProjectHeader>>(this,
                 ViewModelMessagingToken.PinProgramHeaderListener, PinProjectHeaderChangedAction);
        }


        private void ResetViewModel()
        {
            
        }
    }
}