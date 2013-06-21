using System.Runtime.CompilerServices;
using Catrobat.Core;
using Catrobat.IDEWindowsPhone.Annotations;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using GalaSoft.MvvmLight.Command;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.Views.Main;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Catrobat.IDEWindowsPhone.ViewModel.Main
{
    public class ProjectImportViewModel : ViewModelBase, INotifyPropertyChanged
    {
        #region private Members

        private ProjectImporter _importer;
        private bool _isWorking = false;

        private bool _contentPanelVisibility = false;
        private bool _loadingPanelVisibility = true;
        private bool _progressBarLoadingIsIndeterminate = true;
        private bool _checkBoxMakeActiveIsChecked = true;
        private bool _buttonAddIsEnabled = true;
        private bool _buttonCancelIsEnabled = true;

        #endregion

        #region Properties

        public string ProjectName { get; set; }

        public ImageSource ImageProjectScreenshotSource { get; set; }

        public bool ContentPanelVisibility
        {
            get { return _contentPanelVisibility; }
            set
            {
                if (value.Equals(_contentPanelVisibility)) return;
                _contentPanelVisibility = value;
                RaisePropertyChanged("ContentPanelVisibility");
            }
        }

        public bool LoadingPanelVisibility
        {
            get { return _loadingPanelVisibility; }
            set
            {
                if (value.Equals(_loadingPanelVisibility)) return;
                _loadingPanelVisibility = value;
                RaisePropertyChanged("LoadingPanelVisibility");
            }
        }

        public bool ProgressBarLoadingIsIndeterminate
        {
            get { return _progressBarLoadingIsIndeterminate; }
            set
            {
                if (value.Equals(_progressBarLoadingIsIndeterminate)) return;
                _progressBarLoadingIsIndeterminate = value;
                RaisePropertyChanged("ProgressBarLoadingIsIndeterminate");
            }
        }

        public bool CheckBoxMakeActiveIsChecked
        {
            get { return _checkBoxMakeActiveIsChecked; }
            set
            {
                if (value.Equals(_checkBoxMakeActiveIsChecked)) return;
                _checkBoxMakeActiveIsChecked = value;
                RaisePropertyChanged("CheckBoxMakeActiveIsChecked");
            }
        }

        public bool ButtonAddIsEnabled
        {
            get { return _buttonAddIsEnabled; }
            set
            {
                if (value.Equals(_buttonAddIsEnabled)) return;
                _buttonAddIsEnabled = value;
                RaisePropertyChanged("ButtonAddIsEnabled");
            }
        }

        public bool ButtonCancelIsEnabled
        {
            get { return _buttonCancelIsEnabled; }
            set
            {
                if (value.Equals(_buttonCancelIsEnabled)) return;
                _buttonCancelIsEnabled = value;
                RaisePropertyChanged("ButtonCancelIsEnabled");
            }
        }

        #endregion

        #region Commands

        public RelayCommand AddCommand
        {
            get;
            private set;
        }

        public RelayCommand DismissCommand
        {
            get;
            private set;
        }

        public ICommand OnLoadCommand
        {
            get;
            private set;
        }

        #endregion

        #region Actions

        private void AddAction()
        {
            if (_isWorking)
                return;

            _isWorking = true;

            ButtonAddIsEnabled = false;
            ButtonCancelIsEnabled = false;
            ContentPanelVisibility = false;
            LoadingPanelVisibility = true;
            ProgressBarLoadingIsIndeterminate = true;

            var setActive = CheckBoxMakeActiveIsChecked != null && CheckBoxMakeActiveIsChecked;

            var task = Task.Run(() =>
            {
                try
                {
                    if (_importer != null)
                        _importer.AcceptTempProject(setActive);

                    Navigation.NavigateTo(typeof(MainView));
                }
                catch
                {
                    ShowErrorMessage();
                }
            });

            _isWorking = false;
        }

        private void DismissAction()
        {
            Navigation.NavigateTo(typeof(MainView));
        }

        private async void OnLoadAction(NavigationContext navigationContext)
        {
            _isWorking = true;
            var fileToken = string.Empty;
            if (navigationContext.QueryString.TryGetValue("fileToken", out fileToken))
            {
                _importer = new ProjectImporter();
                var projectHeader = await _importer.ImportProjects(fileToken);

                if (projectHeader != null)
                {
                    ProjectName = projectHeader.ProjectName;

                    ImageProjectScreenshotSource = projectHeader.Screenshot as ImageSource;
                    ContentPanelVisibility = true;
                    LoadingPanelVisibility = false;
                    ProgressBarLoadingIsIndeterminate = false;
                }
                else
                {
                    ShowErrorMessage();
                }
            }
            _isWorking = false;
        }

        #endregion


        public ProjectImportViewModel()
        {
            // Commands
            AddCommand = new RelayCommand(AddAction);
            DismissCommand = new RelayCommand(DismissAction);
            OnLoadCommand = new RelayCommand<NavigationContext>(OnLoadAction);
        }

        private void ShowErrorMessage()
        {
            var message = new DialogMessage("Sorry! The project is not valid or not compatible with this version of Catrobat.", ProjectNotValidMessageResult)
            {
                Button = MessageBoxButton.OK,
                Caption = "Project can not be opened"
            };
            Messenger.Default.Send(message);
        }

        private void ProjectNotValidMessageResult(MessageBoxResult obj)
        {
            Navigation.NavigateTo(typeof(MainView));
        }

        public void ResetViewModel()
        {
            ProjectName = "";
            _contentPanelVisibility = false;
            _loadingPanelVisibility = true;
            _progressBarLoadingIsIndeterminate = true;
            _checkBoxMakeActiveIsChecked = true;
            _buttonAddIsEnabled = true;
            _buttonCancelIsEnabled = true;
        }
    }
}
