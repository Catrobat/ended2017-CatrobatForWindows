using System;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Resources.Localization;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModel.Main
{
    public class AddNewProjectViewModel : ViewModelBase
    {
        #region private Members

        private Project _currentProject;
        private string _projectName;
        private bool _copyCurrentProjectAsTemplate;

        #endregion

        #region Properties

        public Project CurrentProject
        {
            get { return _currentProject; }
            set { _currentProject = value; RaisePropertyChanged(() => CurrentProject); }
        }

        public string TextCopyCurrentProjectAsTemplate
        {
            get { return String.Format(AppResources.Main_UseCurrentProjectAsTemplate, CurrentProject.ProjectHeader.ProgramName); }
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

        public bool CopyCurrentProjectAsTemplate
        {
            get { return _copyCurrentProjectAsTemplate; }
            set
            {
                _copyCurrentProjectAsTemplate = value;
                RaisePropertyChanged(() => CopyCurrentProjectAsTemplate);
            }
        }

        #endregion

        #region Commands

        public RelayCommand SaveCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool SaveCommand_CanExecute()
        {
            return ProjectName != null && ProjectName.Length >= 2;
        }

        #endregion

        #region Actions

        private async void SaveAction()
        {
            await CurrentProject.Save();

            CurrentProject = CopyCurrentProjectAsTemplate ?
                await CatrobatContext.CopyProject(CurrentProject.ProjectHeader.ProgramName, _projectName) :
                await CatrobatContext.CreateEmptyProject(_projectName);

            if (CurrentProject != null)
            {
                await CurrentProject.Save();

                var projectChangedMessage = new GenericMessage<Project>(CurrentProject);
                Messenger.Default.Send(projectChangedMessage, ViewModelMessagingToken.CurrentProjectChangedListener);
            }

            base.GoBackAction();
        }

        private void CancelAction()
        {
            base.GoBackAction();
        }

        protected override void GoBackAction()
        {
            ResetViewModel();
            base.GoBackAction();
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

            Messenger.Default.Register<GenericMessage<Project>>(this,
                 ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedAction);
        }

        public void ResetViewModel()
        {
            ProjectName = "";
            CopyCurrentProjectAsTemplate = false;
        }
    }
}