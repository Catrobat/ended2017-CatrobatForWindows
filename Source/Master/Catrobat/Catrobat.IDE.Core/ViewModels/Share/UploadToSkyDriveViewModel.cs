using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Share
{
    public class UploadToSkyDriveViewModel : ViewModelBase
    {
        #region private Members

        private ProjectDummyHeader _projectToShare;
        private bool _isUploading;
        private bool _isUploadSuccess;
        private bool _isUploadError;

        #endregion

        #region Properties

        public ProjectDummyHeader ProjectToShare
        {
            get { return _projectToShare; }
            private set { _projectToShare = value; RaisePropertyChanged(() => ProjectToShare); }
        }

        public bool IsUploading
        {
            get { return _isUploading; }
            set
            {
                _isUploading = value;
                RaisePropertyChanged(() => IsUploading);
            }
        }

        public bool IsUploadSuccess
        {
            get { return _isUploadSuccess; }
            set
            {
                _isUploadSuccess = value;
                RaisePropertyChanged(() => IsUploadSuccess);
            }
        }

        public bool IsUploadError
        {
            get { return _isUploadError; }
            set
            {
                _isUploadError = value;
                RaisePropertyChanged((() => IsUploadError));
            }
        }

        #endregion

        #region Commands

        public RelayCommand<object> UploadToSkyDriveCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool UploadToSkyDrive_CanExecute(object arguments)
        {
            return true;
        }

        #endregion

        #region Actions

        private void UploadToSkyDriveAction(object arguments)
        {
            ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                IsUploading = true;
                IsUploadSuccess = false;
                IsUploadError = false;
            });

            Task.Run(
                () => ServiceLocator.DispatcherService.RunOnMainThread(() =>
                    ServiceLocator.ShareService.UploadProjectToSkydrive(arguments, ProjectToShare, Success, Error)));
        }

        protected override void GoBackAction()
        {
            base.GoBackAction();
        }

        #endregion

        #region MessageActions

        private void ProjectToShareChangedMessageAction(GenericMessage<ProjectDummyHeader> message)
        {
            ProjectToShare = message.Content;
        }

        #endregion

        public UploadToSkyDriveViewModel()
        {
            // Commands
            UploadToSkyDriveCommand = new RelayCommand<object>(UploadToSkyDriveAction, UploadToSkyDrive_CanExecute);

            Messenger.Default.Register<GenericMessage<ProjectDummyHeader>>(this,
                 ViewModelMessagingToken.ShareProjectHeaderListener, ProjectToShareChangedMessageAction);
        }

        private void Error()
        {
            ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                IsUploading = false;
                IsUploadSuccess = false;
                IsUploadError = true;
            });

        }

        private void Success()
        {
            ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                IsUploading = false;
                IsUploadSuccess = true;
                IsUploadError = false;
            });
        }
    }
}