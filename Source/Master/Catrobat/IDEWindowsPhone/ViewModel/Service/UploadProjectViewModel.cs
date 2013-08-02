using System.ComponentModel;
using System.Threading;
using System.Windows;
using Catrobat.Core;
using Catrobat.Core.Misc.ServerCommunication;
using Catrobat.IDEWindowsPhone.Content.Localization;
using Catrobat.IDEWindowsPhone.Misc;
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

        #endregion

        #region Properties

        public string ProjectName
        {
            get { return _projectName; }
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

        private void UploadAction()
        {
            _catrobatContext.CurrentProject.ProjectHeader.ProgramName = _projectName;

            ServerCommunication.UploadProject(_projectName, _projectDescription,
                                              CatrobatContextBase.GetContext().CurrentUserEmail,
                                              Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName,
                                              CatrobatContextBase.GetContext().CurrentToken, UploadCallback);

            Messenger.Default.Send(new DialogMessage(AppResources.Main_UploadQueueMessage, null)
            {
                Caption = AppResources.Main_MessageBoxInformation,
                Button = MessageBoxButton.OK,
            });

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

        public UploadProjectViewModel()
        {
            // Commands
            UploadCommand = new RelayCommand(UploadAction, UploadCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

            if (IsInDesignMode)
            {
                _catrobatContext = new CatrobatContextDesign();
            }
            else
            {
                _catrobatContext = CatrobatContextBase.GetContext();
            }

            _projectName = _catrobatContext.CurrentProject.ProjectHeader.ProgramName;
        }


        private void UploadCallback(bool successful)
        {
            if (ServerCommunication.NoUploadsPending())
            {
                Messenger.Default.Send(new DialogMessage(AppResources.Main_NoUploadsPending, null)
                {
                    Caption = AppResources.Main_MessageBoxInformation,
                    Button = MessageBoxButton.OK,
                });
            }
        }

        public void ResetViewModel()
        {
            ProjectName = "";
            ProjectDescription = "";
        }
    }
}