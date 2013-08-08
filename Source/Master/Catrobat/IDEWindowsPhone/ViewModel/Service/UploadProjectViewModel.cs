using System.ComponentModel;
using System.Threading;
using System.Windows;
using Catrobat.Core;
using Catrobat.Core.Misc.ServerCommunication;
using Catrobat.Core.Objects;
using Catrobat.IDEWindowsPhone.Content.Localization;
using Catrobat.IDEWindowsPhone.Misc;
using Coding4Fun.Toolkit.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDEWindowsPhone.ViewModel.Service
{
    public class UploadProjectViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly CatrobatContextBase _catrobatContext;

        #region private Members

        private string _projectName;
        private string _projectDescription;
        private CatrobatContextBase _context;
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


        public CatrobatContextBase Context
        {
            get { return _context; }
            set
            {
                _context = value; 
                RaisePropertyChanged(() => Context);
            }
        }

        public string ProjectName
        {
            get
            {
                return _projectName;
            }
            set
            {
                if (_projectName != value)
                {
                    _projectName = value;
                    RaisePropertyChanged(() => ProjectName);
                    UploadCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string ProjectDescription
        {
            get { return _projectDescription; }
            set
            {
                if (_projectDescription != value)
                {
                    _projectDescription = value;
                    RaisePropertyChanged(() => ProjectDescription);
                }
            }
        }

        #endregion

        #region Commands

        public RelayCommand InitializeCommand { get; private set; }
        public RelayCommand UploadCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public RelayCommand ResetViewModelCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool UploadCommand_CanExecute()
        {
            return ProjectName != null && ProjectName.Length >= 2;
        }

        #endregion

        #region Actions

        private void InitializeAction()
        {
            if (Context != null)
                ProjectName = CurrentProject.ProjectHeader.ProgramName;
            else
                ProjectName = "";
        }
        private void UploadAction()
        {
            CurrentProject.ProjectHeader.ProgramName = ProjectName;

            ServerCommunication.UploadProject(_projectName, _projectDescription,
                                              Context.CurrentUserEmail,
                                              Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName,
                                              Context.CurrentToken, UploadCallback);

            var message = new MessageBase();
            Messenger.Default.Send(message, ViewModelMessagingToken.UploadProjectStartedListener);

            Navigation.RemoveBackEntry();
            Navigation.NavigateBack();
        }

        private void CancelAction()
        {
            Navigation.RemoveBackEntry();
            Navigation.NavigateBack();
        }

        private void ResetViewModelAction()
        {
            ResetViewModel();
        }

        #endregion

        #region MessageActions
        private void ContextChangedAction(GenericMessage<CatrobatContextBase> message)
        {
            Context = message.Content;
        }

        private void CurrentProjectChangedChangedAction(GenericMessage<Project> message)
        {
            CurrentProject = message.Content;
        }

        #endregion

        public UploadProjectViewModel()
        {
            InitializeCommand = new RelayCommand(InitializeAction);
            UploadCommand = new RelayCommand(UploadAction, UploadCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);


            Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
                 ViewModelMessagingToken.ContextListener, ContextChangedAction);

            Messenger.Default.Register<GenericMessage<Project>>(this,
                ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedChangedAction);
        }


        private void UploadCallback(bool successful)
        {
            if (ServerCommunication.NoUploadsPending())
            {
                var toast = new ToastPrompt
                {
                    Message = AppResources.Main_NoUploadsPending
                };
                toast.Show();
            }
        }

        public void ResetViewModel()
        {
            ProjectName = "";
            ProjectDescription = "";
        }
    }
}