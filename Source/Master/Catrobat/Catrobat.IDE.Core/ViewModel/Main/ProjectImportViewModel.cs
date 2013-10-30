using System;
using System.Windows.Input;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModel.Main
{
    public class ProjectImportViewModel : ViewModelBase
    {
        #region private Members

        private bool _isWorking = false;

        private bool _contentPanelVisibility = false;
        private bool _loadingPanelVisibility = true;
        private bool _progressBarLoadingIsIndeterminate = true;
        private bool _checkBoxMakeActiveIsChecked = true;
        private bool _buttonAddIsEnabled = true;
        private bool _buttonCancelIsEnabled = true;
        private string _projectName = "";
        private PortableImage _screenshotImageSource = null;
        private bool _errorPanelVisibility;

        #endregion

        #region Properties

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
            }
        }

        public PortableImage ScreenshotImageSource
        {
            get { return _screenshotImageSource; }
            set
            {
                if (_screenshotImageSource == value)
                {
                    return;
                }
                _screenshotImageSource = value;
                RaisePropertyChanged(() => ScreenshotImageSource);
            }
        }

        public bool ContentPanelVisibility
        {
            get { return _contentPanelVisibility; }
            set
            {
                if (value == _contentPanelVisibility)
                {
                    return;
                }
                _contentPanelVisibility = value;
                RaisePropertyChanged(() => ContentPanelVisibility);
            }
        }

        public bool LoadingPanelVisibility
        {
            get { return _loadingPanelVisibility; }
            set
            {
                if (value == _loadingPanelVisibility)
                {
                    return;
                }
                _loadingPanelVisibility = value;
                RaisePropertyChanged(() => LoadingPanelVisibility);
            }
        }

        public bool ErrorPanelVisibility
        {
            get { return _errorPanelVisibility; }
            set
            {
                _errorPanelVisibility = value;
                RaisePropertyChanged(() => ErrorPanelVisibility);
            }
        }

        public bool ProgressBarLoadingIsIndeterminate
        {
            get { return _progressBarLoadingIsIndeterminate; }
            set
            {
                if (value == _progressBarLoadingIsIndeterminate)
                {
                    return;
                }
                _progressBarLoadingIsIndeterminate = value;
                RaisePropertyChanged(() => ProgressBarLoadingIsIndeterminate);
            }
        }

        public bool CheckBoxMakeActiveIsChecked
        {
            get { return _checkBoxMakeActiveIsChecked; }
            set
            {
                if (value == _checkBoxMakeActiveIsChecked)
                {
                    return;
                }
                _checkBoxMakeActiveIsChecked = value;
                RaisePropertyChanged(() => CheckBoxMakeActiveIsChecked);
            }
        }

        public bool ButtonAddIsEnabled
        {
            get { return _buttonAddIsEnabled; }
            set
            {
                if (value == _buttonAddIsEnabled)
                {
                    return;
                }
                _buttonAddIsEnabled = value;
                RaisePropertyChanged(() => ButtonAddIsEnabled);
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public bool ButtonCancelIsEnabled
        {
            get { return _buttonCancelIsEnabled; }
            set
            {
                if (value == _buttonCancelIsEnabled)
                {
                    return;
                }
                _buttonCancelIsEnabled = value;
                RaisePropertyChanged(() => ButtonCancelIsEnabled);
                CancelCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Commands

        public RelayCommand AddCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public ICommand OnLoadCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool AddCommand_CanExecute()
        {
            return ButtonAddIsEnabled;
        }

        private bool CancelCommand_CanExecute()
        {
            return ButtonCancelIsEnabled;
        }

        #endregion

        #region Actions

        private async void AddAction()
        {
            if (_isWorking)
            {
                return;
            }

            _isWorking = true;
            ShowPanel(VisiblePanel.Loading);

            try
            {
                var newProjectName = await ServiceLocator.ProjectImporterService.AcceptTempProject();

                    if (CheckBoxMakeActiveIsChecked)
                    {
                        var newProject = await Catrobat.IDE.Core.CatrobatContext.LoadNewProjectByNameStatic(newProjectName);

                        var projectChangedMessage = new GenericMessage<Project>(newProject);
                        Messenger.Default.Send(projectChangedMessage, ViewModelMessagingToken.CurrentProjectChangedListener);
                    }
                    else
                    {
                        var localProjectsChangedMessage = new MessageBase();
                        Messenger.Default.Send(localProjectsChangedMessage, ViewModelMessagingToken.LocalProjectsChangedListener);
                    }

                ServiceLocator.DispatcherService.RunOnMainThread(() => ServiceLocator.NavigationService.NavigateTo<MainViewModel>());
            }
            catch
            {
                ShowPanel(VisiblePanel.Error);
            }

            _isWorking = false;
        }

        private static void CancelAction()
        {
            ServiceLocator.ProjectImporterService.CancelImport();
            ServiceLocator.NavigationService.NavigateTo<MainViewModel>();
        }

        private async void OnLoadAction(string fileToken)
        {
            _isWorking = true;
            if (fileToken != null)
            {
                var projectHeader = await ServiceLocator.ProjectImporterService.ImportProject(fileToken);

                if (projectHeader != null)
                {
                    ProjectName = projectHeader.ProjectName;

                    ScreenshotImageSource = projectHeader.Screenshot;

                    ShowPanel(VisiblePanel.Content);
                }
                else
                {
                    ShowPanel(VisiblePanel.Error);
                }
            }
            _isWorking = false;
        }

        protected override void GoBackAction()
        {
            ServiceLocator.ProjectImporterService.CancelImport();
            ResetViewModel();
            base.GoBackAction();
        }

        #endregion

        public ProjectImportViewModel()
        {
            // Commands
            AddCommand = new RelayCommand(AddAction, AddCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction, CancelCommand_CanExecute);
            OnLoadCommand = new RelayCommand<string>(OnLoadAction);
        }

        private enum VisiblePanel { Error, Content, Loading }

        private void ShowPanel(VisiblePanel panel)
        {
            switch (panel)
            {
                case VisiblePanel.Error:
                    ErrorPanelVisibility = true;
                    ContentPanelVisibility = false;
                    LoadingPanelVisibility = false;
                    ProgressBarLoadingIsIndeterminate = false;
                    ButtonAddIsEnabled = false;
                    ButtonCancelIsEnabled = true;
                    break;
                case VisiblePanel.Content:
                    ErrorPanelVisibility = false;
                    ContentPanelVisibility = true;
                    LoadingPanelVisibility = false;
                    ProgressBarLoadingIsIndeterminate = false;
                    ButtonAddIsEnabled = true;
                    ButtonCancelIsEnabled = true;
                    break;
                case VisiblePanel.Loading:
                    ErrorPanelVisibility = false;
                    ContentPanelVisibility = false;
                    LoadingPanelVisibility = true;
                    ProgressBarLoadingIsIndeterminate = true;
                    ButtonAddIsEnabled = false;
                    ButtonCancelIsEnabled = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("panel");
            }

            //ServiceLocator.DispatcherService.RunOnMainThread(() =>
            //{
            //    var message = new DialogMessage("Sorry! The project is not valid or not compatible with this version of Catrobat.", ProjectNotValidMessageResult)
            //    {
            //        Button = MessageBoxButton.OK,
            //        Caption = "Project can not be opened"
            //    };
            //    Messenger.Default.Send(message);
            //});
        }

        private void ResetViewModel()
        {
            ProjectName = "";
            ContentPanelVisibility = false;
            LoadingPanelVisibility = true;
            ProgressBarLoadingIsIndeterminate = true;
            CheckBoxMakeActiveIsChecked = true;
            ButtonAddIsEnabled = true;
            ButtonCancelIsEnabled = true;
            ProjectName = "";
            ScreenshotImageSource = null;
        }
    }
}