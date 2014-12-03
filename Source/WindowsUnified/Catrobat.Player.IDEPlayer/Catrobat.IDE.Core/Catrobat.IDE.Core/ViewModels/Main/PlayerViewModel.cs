using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Input;

using Catrobat_Player;


namespace Catrobat.IDE.Core.ViewModels.Main
{
    public class PlayerViewModel : ViewModelBase
    {
        #region private Members

        private Catrobat_PlayerAdapter _playerObject = null;
        private string _projectName = "";
        private bool _isLaunchFromTile = false;

        #endregion

        #region Properties    

        public Catrobat_PlayerAdapter PlayerObject
        {
            get { return _playerObject; }
            set
            {
                _playerObject = value;
                RaisePropertyChanged(() => PlayerObject);
            }    
        }

        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                _projectName = value;
                RaisePropertyChanged(() => ProjectName);
            }
        }

        public bool IsLaunchFromTile
        {
            get { return _isLaunchFromTile; }
            set
            {
                _isLaunchFromTile = value;
                RaisePropertyChanged(() => IsLaunchFromTile);
            }
        }

        #endregion

        #region Commands

        public ICommand RestartProgramCommand { get; private set; }
        public ICommand PlayProgramCommand { get; private set; }
        public ICommand SetThumbnailCommand { get; private set; }
        public ICommand EnableAxisCommand { get; private set; }
        public ICommand TakeScreenshotCommand { get; private set; }

        #endregion

        #region Actions

        private void RestartProgramAction()
        {
            PlayerObject.RestartButtonClicked();
        }

        private void PlayProgramAction()
        {
            PlayerObject.PlayButtonClicked();
        }

        private void SetThumbnailAction()
        {
            PlayerObject.ThumbnailButtonClicked();

        }

        private void EnableAxisAction()
        {
            PlayerObject.EnableAxisButtonClicked();
        }

        private void TakeScreenshotAction()
        {
            PlayerObject.ScreenshotButtonClicked();
        }

        protected override void GoBackAction()
        {
            if (PlayerObject.HardwareBackButtonPressed() == true)
            {
                PlayerObject = null;

                if (IsLaunchFromTile)
                {

                }
                else
                {
                    base.GoBackAction();
                }
            }
        }

        #endregion

        #region MessageActions

        private void IsStartFromTileMessageAction(GenericMessage<bool> message)
        {
            IsLaunchFromTile = message.Content;
        }

        private void ProjectNameMessageAction(GenericMessage<string> message)
        {
            ProjectName = message.Content;
        }

        #endregion

        public PlayerViewModel()
        {
            RestartProgramCommand   = new RelayCommand(RestartProgramAction);
            PlayProgramCommand      = new RelayCommand(PlayProgramAction);
            SetThumbnailCommand     = new RelayCommand(SetThumbnailAction);
            EnableAxisCommand       = new RelayCommand(EnableAxisAction);
            TakeScreenshotCommand   = new RelayCommand(TakeScreenshotAction);

            Messenger.Default.Register<GenericMessage<string>>(this,
                ViewModelMessagingToken.PlayerProjectNameListener, ProjectNameMessageAction);

            Messenger.Default.Register<GenericMessage<bool>>(this,
                 ViewModelMessagingToken.PlayerIsStartFromTileListener, IsStartFromTileMessageAction);
        }
    }
}