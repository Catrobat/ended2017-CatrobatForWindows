using System.Threading;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows.Input;

namespace Catrobat.IDE.Core.ViewModels.Main
{
    public class ProgramImportViewModel : ViewModelBase
    {
        private enum VisiblePanel { Error, Content, Loading }

        #region private Members

        #endregion

        #region Properties

        private LocalProjectHeader _projectHeader;
        public LocalProjectHeader ProjectHeader
        {
            get { return _projectHeader; }
            set
            {
                _projectHeader = value; 
                RaisePropertyChanged(()=>ProjectHeader);
            }
        }

        private bool _loadingPanelVisibility = true;
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

        private bool _contentPanelVisibility = false;
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

        private bool _errorPanelVisibility = false;
        public bool ErrorPanelVisibility
        {
            get { return _errorPanelVisibility; }
            set
            {
                _errorPanelVisibility = value;
                RaisePropertyChanged(() => ErrorPanelVisibility);
            }
        }

        private bool _progressBarLoadingIsIndeterminate = true;
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

        private bool _checkBoxMakeActiveIsChecked = true;
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

        private bool _buttonAddIsEnabled = true;
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

        private bool _buttonCancelIsEnabled = true;
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
            try
            {
                ServiceLocator.NavigationService.RemoveBackEntry();
                ServiceLocator.DispatcherService.RunOnMainThread(() => 
                    ServiceLocator.NavigationService.NavigateTo<MainViewModel>());

                await ServiceLocator.ProjectImporterService.AcceptTempProject();
                var localProjectsChangedMessage = new MessageBase();
                Messenger.Default.Send(localProjectsChangedMessage, 
                    ViewModelMessagingToken.LocalProgramsChangedListener);

                ResetViewModel();
            }
            catch
            {
                //ShowPanel(VisiblePanel.Error);
            }
        }

        private static void CancelAction()
        {
            ServiceLocator.ProjectImporterService.CancelImport();
            ServiceLocator.NavigationService.NavigateTo<MainViewModel>();
        }

        protected override void GoBackAction()
        {
            ServiceLocator.ProjectImporterService.CancelImport();
            ResetViewModel();
            base.GoBackAction();
        }

        #endregion

        public ProgramImportViewModel()
        {
            AddCommand = new RelayCommand(AddAction, AddCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction, CancelCommand_CanExecute);
        }

        public async override void NavigateTo()
        {
            ShowPanel(VisiblePanel.Loading);

            await ImportProject();
        }

        private async Task ImportProject()
        {
            var extracionResult = await ServiceLocator.ProjectImporterService.ExtractProgram();

            if (extracionResult.Status == ExtractProgramStatus.Error)
            {
                ShowPanel(VisiblePanel.Error);
                ButtonAddIsEnabled = false;
                base.NavigateTo();
                return;
            }

            var validateResult = await ServiceLocator.ProjectImporterService.CheckProgram();

            switch (validateResult.Status)
            {
                case ProgramImportStatus.Valid:
                    ProjectHeader = validateResult.ProjectHeader;
                    ShowPanel(VisiblePanel.Content);
                    ButtonAddIsEnabled = true;
                    break;
                case ProgramImportStatus.Damaged:
                    // TODO: show right error
                    ShowPanel(VisiblePanel.Error);
                    ButtonAddIsEnabled = true;
                    break;
                case ProgramImportStatus.VersionTooOld:
                    // TODO: show right error
                    ShowPanel(VisiblePanel.Error);
                    ButtonAddIsEnabled = true;
                    break;
                case ProgramImportStatus.VersionTooNew:
                    // TODO: show right error
                    ShowPanel(VisiblePanel.Error);
                    ButtonAddIsEnabled = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void NavigateFrom()
        {
            ShowPanel(VisiblePanel.Loading);
            base.NavigateFrom();
        }

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
            ProjectHeader = null;
            ContentPanelVisibility = false;
            LoadingPanelVisibility = true;
            ProgressBarLoadingIsIndeterminate = true;
            CheckBoxMakeActiveIsChecked = true;
            ButtonAddIsEnabled = true;
            ButtonCancelIsEnabled = true;
        }
    }
}