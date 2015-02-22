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

        private string _programName = "";
        private bool _isLaunchFromTile = false;

        #endregion

        #region Properties    

        public string ProgramName
        {
            get { return _programName; }
            set
            {
                _programName = value;
                RaisePropertyChanged(() => ProgramName);
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
        public ICommand EnableAxesCommand { get; private set; }
        public ICommand TakeScreenshotCommand { get; private set; }

        #endregion

        #region Actions

        private void RestartProgramAction()
        {
            ServiceLocator.PlayerLauncherService.RestartProgramAction();
        }

        private void ResumeProgramAction()
        {
            ServiceLocator.PlayerLauncherService.ResumeProgramAction();
        }

        private void SetThumbnailAction()
        {
            ServiceLocator.PlayerLauncherService.SetThumbnailAction();
        }

        private void AxesAction()
        {
            ServiceLocator.PlayerLauncherService.AxesAction();
        }

        private void TakeScreenshotAction()
        {
            ServiceLocator.PlayerLauncherService.TakeScreenshotAction();
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

        private void ProgramNameMessageAction(GenericMessage<string> message)
        {
            ProgramName = message.Content;
        }

        #endregion

        public PlayerViewModel()
        {
            RestartProgramCommand = new RelayCommand(RestartProgramAction);
            PlayProgramCommand = new RelayCommand(ResumeProgramAction);
            SetThumbnailCommand = new RelayCommand(SetThumbnailAction);
            EnableAxesCommand = new RelayCommand(AxesAction);
            TakeScreenshotCommand = new RelayCommand(TakeScreenshotAction);

            Messenger.Default.Register<GenericMessage<string>>(this,
                ViewModelMessagingToken.PlayerProgramNameListener, ProgramNameMessageAction);

            Messenger.Default.Register<GenericMessage<bool>>(this,
                 ViewModelMessagingToken.PlayerIsStartFromTileListener, IsStartFromTileMessageAction);
        }
    }
}