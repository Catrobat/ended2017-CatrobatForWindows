using Catrobat.IDE.Core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Phone.ViewModel.Settings
{
    public class SettingsBrickViewModel : ViewModelBase
    {
        #region Private Members

        private CatrobatContextBase _context;

        #endregion

        #region Properties

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