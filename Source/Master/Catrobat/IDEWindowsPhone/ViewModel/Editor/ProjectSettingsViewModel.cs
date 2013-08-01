using Catrobat.Core.Objects;
using Catrobat.IDEWindowsPhone.Misc;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDEWindowsPhone.ViewModel.Editor
{
    public class ProjectSettingsViewModel : ViewModelBase
    {
        #region Private Members

        private Project _receivedProject;
        private string _projectName;

        #endregion

        #region Properties

        public Project ReceivedProject
        {
            get { return _receivedProject; }
            set
            {
                if (value == _receivedProject)
                {
                    return;
                }
                _receivedProject = value;
                RaisePropertyChanged(() => ReceivedProject);
            }
        }

        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                if (value == _projectName)
                {
                    return;
                }
                _projectName = value;
                RaisePropertyChanged(() => ProjectName);
                SaveCommand.RaiseCanExecuteChanged();
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
            ReceivedProject.ProjectHeader.ProgramName = ProjectName;
            Navigation.NavigateBack();
        }

        private void CancelAction()
        {
            Navigation.NavigateBack();
        }

        private void ChangeProjectNameMessageAction(GenericMessage<Project> message)
        {
            ReceivedProject = message.Content;
            ProjectName = ReceivedProject.ProjectHeader.ProgramName;
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