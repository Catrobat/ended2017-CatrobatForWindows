using Catrobat.IDE.Core;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Phone.ViewModel.Editor
{
    public class ProjectSettingsViewModel : ViewModelBase
    {
        #region Private Members

        private Project _selectedProjectToEdit;
        private ProjectDummyHeader _selectedProjectHeaderToEdit;
        private string _projectName;
        private string _projectDescription;
        private Project _currentProject;

        #endregion

        #region Properties

        public Project CurrentProject
        {
            get { return _currentProject; }
            set
            {
                _currentProject = value;
                RaisePropertyChanged(() => CurrentProject);
            }
        }

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

        public ProjectDummyHeader SelectedProjectHeaderToEdit
        {
            get { return _selectedProjectHeaderToEdit; }
            set
            {
                if (value == _selectedProjectHeaderToEdit)
                {
                    return;
                }
                _selectedProjectHeaderToEdit = value;
                RaisePropertyChanged(() => SelectedProjectHeaderToEdit);
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
            if (CurrentProject.ProjectDummyHeader == SelectedProjectHeaderToEdit)
            {
                CurrentProject.ProjectHeader.ProgramName = ProjectName;
                CurrentProject.ProjectHeader.Description = ProjectDescription;
            }
            else
            {
                SelectedProjectHeaderToEdit.ProjectName = ProjectName;
                SelectedProjectToEdit.ProjectHeader.ProgramName = ProjectName;
                SelectedProjectToEdit.ProjectHeader.Description = ProjectDescription;
                SelectedProjectToEdit.Save();
            }



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

        #region Message Actions

        private void CurrentProjectChangedMessageAction(GenericMessage<Project> message)
        {
            CurrentProject = message.Content;
        }

        private void ChangeProjectNameMessageAction(GenericMessage<ProjectDummyHeader> message)
        {
            SelectedProjectHeaderToEdit = message.Content;

            SelectedProjectToEdit = CatrobatContext.LoadNewProjectByNameStatic(SelectedProjectHeaderToEdit.ProjectName);
            ProjectName = SelectedProjectToEdit.ProjectHeader.ProgramName;
            ProjectDescription = SelectedProjectToEdit.ProjectHeader.Description;
        }

        #endregion

        public ProjectSettingsViewModel()
        {
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

            Messenger.Default.Register<GenericMessage<ProjectDummyHeader>>(this, 
                ViewModelMessagingToken.ChangeLocalProjectListener, ChangeProjectNameMessageAction);
            Messenger.Default.Register<GenericMessage<Project>>(this, 
                ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);
        }

        private void ResetViewModel()
        {
            ProjectName = "";
        }
    }
}