using System.Windows.Input;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModel.Share
{
    public class ShareProjectServiceSelectionViewModel : ViewModelBase
    {
        #region private Members

        private Project _projectToShare;

        #endregion

        #region Properties

        public Project ProjectToShare
        {
            get { return _projectToShare; }
            private set { _projectToShare = value; RaisePropertyChanged(() => ProjectToShare); }
        }

        #endregion

        #region Commands

        public ICommand UploadToSkyDriveCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool ShareWithSkydriveCommand_CanExecute()
        {
            return true;
        }

        #endregion

        #region Actions

        private void ShareWithSkydriveAction()
        {
            ServiceLocator.NavigationService.NavigateTo<UploadToSkyDriveViewModel>();
        }

        protected override void GoBackAction()
        {
            base.GoBackAction();
        }

        #endregion

        #region MessageActions

        private void ProjectToShareChangedMessageAction(GenericMessage<Project> message)
        {
            ProjectToShare = message.Content;
        }

        #endregion

        public ShareProjectServiceSelectionViewModel()
        {
            // Commands
            UploadToSkyDriveCommand = new RelayCommand(ShareWithSkydriveAction, ShareWithSkydriveCommand_CanExecute);
 
            Messenger.Default.Register<GenericMessage<Project>>(this,
                 ViewModelMessagingToken.ShareProjectHeaderListener, ProjectToShareChangedMessageAction);
        }
    }
}