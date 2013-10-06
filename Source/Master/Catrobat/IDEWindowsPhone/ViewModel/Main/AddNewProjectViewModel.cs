using Catrobat.Core;
using Catrobat.Core.CatrobatObjects;
using Catrobat.Core.Services;
using Catrobat.IDEWindowsPhone.Misc;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDEWindowsPhone.ViewModel.Main
{
    public class AddNewProjectViewModel : ViewModelBase
    {
        #region private Members

        private Project _currentProject;
        private string _projectName;

        #endregion

        #region Properties

        public Project CurrentProject
        {
            get { return _currentProject; }
            set { _currentProject = value; RaisePropertyChanged(() => CurrentProject); }
        }
        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                if (_projectName != value)
                {
                    _projectName = value;

                    RaisePropertyChanged(() => ProjectName);
                    SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        #endregion

        #region Commands

        public RelayCommand SaveCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public RelayCommand ResetViewModelCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool SaveCommand_CanExecute()
        {
            return ProjectName != null && ProjectName.Length >= 2;
        }

        #endregion

        #region Actions

        private void SaveAction()
        {
            CurrentProject.Save();
            CurrentProject = CatrobatContext.RestoreDefaultProjectStatic(_projectName);
            CurrentProject.Save();

            var projectChangedMessage = new GenericMessage<Project>(CurrentProject);
            Messenger.Default.Send<GenericMessage<Project>>(projectChangedMessage, ViewModelMessagingToken.CurrentProjectChangedListener);

            ServiceLocator.NavigationService.NavigateBack();
        }

        private void CancelAction()
        {
            ServiceLocator.NavigationService.NavigateBack();
        }

        private void ResetViewModelAction()
        {
            ResetViewModel();
        }

        #endregion

        #region MessageActions

        private void CurrentProjectChangedAction(GenericMessage<Project> message)
        {
            CurrentProject = message.Content;
        }

        #endregion

        public AddNewProjectViewModel()
        {
            // Commands
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

            Messenger.Default.Register<GenericMessage<Project>>(this,
                 ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedAction);
        }

        private void ResetViewModel()
        {
            ProjectName = "";
        }
    }
}