using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Resources;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.VersionConverter;
using Catrobat.IDE.Phone.Views.Share;
using Coding4Fun.Toolkit.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Tasks;

namespace Catrobat.IDE.Phone.ViewModel.Share
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
            ServiceLocator.NavigationService.NavigateTo(typeof(UploadToSkyDriveViewModel));
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