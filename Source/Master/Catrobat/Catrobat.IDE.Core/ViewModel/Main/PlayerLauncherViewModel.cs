using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModel.Main
{
    public class PlayerLauncherViewModel : ViewModelBase
    {
        #region private Members

        private string _playProjectName;
        private bool _isLauncheFromTile;

        #endregion

        #region Properties

        public string PlayProjectName
        {
            get { return _playProjectName; }
            set
            {
                _playProjectName = value;
                RaisePropertyChanged(() => PlayProjectName);
            }
        }

        public bool IsLauncheFromTile
        {
            get { return _isLauncheFromTile; }
            set
            {
                _isLauncheFromTile = value;
                RaisePropertyChanged(() => IsLauncheFromTile);
            }
        }

        #endregion

        #region Commands


        #endregion

        #region Actions

        protected override void GoBackAction()
        {
            //base.GoBackAction();
        }

        #endregion

        #region MessageActions

        private void IsPlayerStartFromTileMessageAction(GenericMessage<bool> message)
        {
            IsLauncheFromTile = message.Content;
        }

        private void PlayProjectNameMessageAction(GenericMessage<string> message)
        {
            PlayProjectName = message.Content;
        }

        #endregion

        public PlayerLauncherViewModel()
        {
            Messenger.Default.Register<GenericMessage<string>>(this,
                ViewModelMessagingToken.PlayProjectNameListener, PlayProjectNameMessageAction);

            Messenger.Default.Register<GenericMessage<bool>>(this,
                 ViewModelMessagingToken.IsPlayerStartFromTileListener, IsPlayerStartFromTileMessageAction);
        }
    }
}