using Catrobat.IDE.Core.CatrobatObjects;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Main
{
    public class TileGeneratorViewModel : ViewModelBase
    {
        #region private Members

        private ProjectDummyHeader _pinProjectHeader;

        #endregion

        #region Properties
        public ProjectDummyHeader PinProjectHeader
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

        private void PinProjectHeaderChangedAction(GenericMessage<ProjectDummyHeader> message)
        {
            PinProjectHeader = message.Content;
        }

        #endregion

        public TileGeneratorViewModel()
        {
            Messenger.Default.Register<GenericMessage<ProjectDummyHeader>>(this,
                 ViewModelMessagingToken.PinProjectHeaderListener, PinProjectHeaderChangedAction);
        }


        private void ResetViewModel()
        {
            
        }
    }
}