using System.Windows.Input;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml;
using Catrobat.IDE.Core.Xml.XmlObjects;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Share
{
    public class ShareProjectServiceSelectionViewModel : ViewModelBase
    {
        #region private Members

        private XmlProgram _projectToShare;

        #endregion

        #region Properties

        public XmlProgram ProjectToShare
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

        private void ProjectToShareChangedMessageAction(GenericMessage<XmlProgram> message)
        {
            ProjectToShare = message.Content;
        }

        #endregion

        public ShareProjectServiceSelectionViewModel()
        {
            // Commands
            UploadToSkyDriveCommand = new RelayCommand(ShareWithSkydriveAction, ShareWithSkydriveCommand_CanExecute);
 
            Messenger.Default.Register<GenericMessage<XmlProgram>>(this,
                 ViewModelMessagingToken.ShareProjectHeaderListener, ProjectToShareChangedMessageAction);
        }
    }
}