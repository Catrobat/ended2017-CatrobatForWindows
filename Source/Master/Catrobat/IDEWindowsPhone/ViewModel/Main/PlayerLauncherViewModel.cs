using Catrobat.Core.Objects;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDEWindowsPhone.ViewModel.Main
{
    public class PlayerLauncherViewModel : ViewModelBase
    {
        #region private Members

        private Project _currentProject;

        #endregion

        #region Properties

        public Project CurrentProject
        {
            get
            {
                return _currentProject;
            }
            set
            {
                if (value == _currentProject) return;

                _currentProject = value;

                RaisePropertyChanged(() => CurrentProject);
            }
        }

        #endregion

        #region Commands


        #endregion

        #region Actions


        #endregion

        #region MessageActions

        private void CurrentProjectChangedChangedAction(GenericMessage<Project> message)
        {
            CurrentProject = message.Content;
        }

        #endregion

        public PlayerLauncherViewModel()
        {

            Messenger.Default.Register<GenericMessage<Project>>(this,
                 ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedChangedAction);
        }


        private void ResetViewModel()
        {
            
        }
    }
}