using System;
using System.Collections.Generic;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using Catrobat.IDE.Core.Services;


namespace Catrobat.IDE.Core.ViewModels.Main
{
    public class PlayerViewModel : ViewModelBase
    {
        #region private Members

        private string _projectName = "";
        private bool _isLaunchFromTile = false;

        #endregion

        #region Properties    

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
            ServiceLocator.PlayerLauncherService.RestartProgramAction();
        }

        private void PlayProgramAction()
        {
            ServiceLocator.PlayerLauncherService.PlayProgramAction();
        }

        private void SetThumbnailAction()
        {
            ServiceLocator.PlayerLauncherService.SetThumbnailAction();
        }

        private void EnableAxisAction()
        {
            ServiceLocator.PlayerLauncherService.SetThumbnailAction();
        }

        private void TakeScreenshotAction()
        {
            ServiceLocator.PlayerLauncherService.SetThumbnailAction();
        }

        protected override void GoBackAction()
        {
            if (ServiceLocator.PlayerLauncherService.HardwareBackButtonPressed() == true)
            {
                if (IsLaunchFromTile)
                {
                    // TODO: what to do when the Player has been started from a tile: terminate, terminate with confirmation question or default behavior?
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