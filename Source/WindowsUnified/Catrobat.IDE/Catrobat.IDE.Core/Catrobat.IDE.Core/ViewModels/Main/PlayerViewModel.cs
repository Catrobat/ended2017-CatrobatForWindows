using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Input;  


namespace Catrobat.IDE.Core.ViewModels.Main
{
    public class PlayerViewModel : ViewModelBase
    {
        #region private Members

        private string _projectName;
        private bool _isLaunchFromTile;

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
        public ICommand TakeScreenshotCommand { get; private set; }

        #endregion

        #region Actions

        private void RestartProgramAction()
        {
            // TODO: call appropriate method in PlayerAdapter class
        }

        private void TakeScreenshotAction()
        {
            // TODO: call appropriate method in PlayerAdapter class
        }

        protected override void GoBackAction()
        {
            // TODO: call appropriate method of PlayerAdapter class
            if (/* playerAdapterOrHowever.goBackActionOrHowever*/true == true && !IsLaunchFromTile)
            {
                base.GoBackAction();
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
            RestartProgramCommand = new RelayCommand(RestartProgramAction);
            TakeScreenshotCommand = new RelayCommand(TakeScreenshotAction);

            Messenger.Default.Register<GenericMessage<string>>(this,
                ViewModelMessagingToken.PlayerProjectNameListener, ProjectNameMessageAction);

            Messenger.Default.Register<GenericMessage<bool>>(this,
                 ViewModelMessagingToken.PlayerIsStartFromTileListener, IsStartFromTileMessageAction);
        }
    }
}
