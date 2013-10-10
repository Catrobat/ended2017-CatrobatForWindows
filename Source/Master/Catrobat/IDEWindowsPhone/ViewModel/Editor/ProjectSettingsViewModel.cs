using Catrobat.Core.CatrobatObjects;
using Catrobat.Core.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDEWindowsPhone.ViewModel.Editor
{
    public class ProjectSettingsViewModel : ViewModelBase
    {
        #region Private Members

        private Project _selectedProjectToEdit;
        private string _projectName;
        private string _projectDescription;

        #endregion

        #region Properties

        public Project SelectedProjectToEdit
        {
            get { return _selectedProjectToEdit; }
            set
            {
                if (value == _selectedProjectToEdit)
                {
                    return;
                }
                _selectedProjectToEdit = value;
                RaisePropertyChanged(() => SelectedProjectToEdit);
            }
        }

        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                if (value == _projectName) return;
                _projectName = value;
                RaisePropertyChanged(() => ProjectName);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public string ProjectDescription
        {
            get { return _projectDescription; }
            set
            {
                if (value == _projectDescription) return;
                _projectDescription = value;
                RaisePropertyChanged(() => ProjectDescription);
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
            SelectedProjectToEdit.ProjectHeader.ProgramName = ProjectName;
            SelectedProjectToEdit.ProjectHeader.Description = ProjectDescription;
            ServiceLocator.NavigationService.NavigateBack();
        }

        private void CancelAction()
        {
            ServiceLocator.NavigationService.NavigateBack();
        }

        private void ChangeProjectNameMessageAction(GenericMessage<Project> message)
        {
            SelectedProjectToEdit = message.Content;
            ProjectName = SelectedProjectToEdit.ProjectHeader.ProgramName;
            ProjectDescription = SelectedProjectToEdit.ProjectHeader.Description;
        }

        private void ResetViewModelAction()
        {
            ResetViewModel();
        }

        #endregion

        public ProjectSettingsViewModel()
        {
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

            Messenger.Default.Register<GenericMessage<Project>>(this, ViewModelMessagingToken.ProjectNameListener, ChangeProjectNameMessageAction);
        }

        private void ResetViewModel()
        {
            ProjectName = "";
        }
    }
}