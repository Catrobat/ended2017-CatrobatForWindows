using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Settings
{
    public class SettingsBrickViewModel : ViewModelBase
    {
        #region Private Members

        private CatrobatContextBase _context;

        #endregion

        #region Properties

        public CatrobatContextBase Context
        {
            get { return _context; }
            set
            {
                _context = value;
                RaisePropertyChanged(() => Context);
            }
        }

        public bool IsNxtBricksEnabled
        {
            get { return _context.LocalSettings.IsNxtBricksEnabled; }
            set 
            {
                _context.LocalSettings.IsNxtBricksEnabled = value; 
                RaisePropertyChanged(() => IsNxtBricksEnabled); 
            }
        }

        #endregion

        #region Commands

        #endregion

        #region Actions

        protected override void GoBackAction()
        {
            base.GoBackAction();
        }

        #endregion

        #region MessageActions

        private void ContextChangedAction(GenericMessage<CatrobatContextBase> message)
        {
            _context = message.Content;
        }

        #endregion

        public SettingsBrickViewModel()
        {
            Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
               ViewModelMessagingToken.ContextListener, ContextChangedAction);
        }
    }
}