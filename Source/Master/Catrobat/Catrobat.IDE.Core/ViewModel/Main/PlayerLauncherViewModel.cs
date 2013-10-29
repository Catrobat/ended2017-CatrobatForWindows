using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModel.Main
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
            private set
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

        protected override void GoBackAction()
        {
            ResetViewModel();
            ServiceLocator.NavigationService.NavigateBack();
        }

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