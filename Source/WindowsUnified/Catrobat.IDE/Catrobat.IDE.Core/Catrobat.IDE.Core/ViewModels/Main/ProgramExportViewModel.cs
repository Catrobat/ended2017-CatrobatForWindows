using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities.JSON;
using Catrobat.IDE.Core.ViewModels.Service;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows.Input;

namespace Catrobat.IDE.Core.ViewModels.Main
{
    public class ProgramExportViewModel : ViewModelBase
    {
        #region private Members
        private string _tempShareFilePath;
        #endregion

        #region Properties

        private CatrobatContextBase _context;
        public CatrobatContextBase Context
        {
            get { return _context; }
            set
            {
                _context = value;
                RaisePropertyChanged(() => Context);
            }
        }

        private Program _currentProgram;
        public Program CurrentProgram
        {
            get
            {
                return _currentProgram;
            }
            set
            {
                if (value == _currentProgram)
                    return;

                _currentProgram = value;
                //ServiceLocator.DispatcherService.RunOnMainThread(
                //    () => RaisePropertyChanged(() => CurrentProgram));
            }
        }

        private bool _isLoading = true;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (value == _isLoading)
                {
                    return;
                }
                _isLoading = value;
                ServiceLocator.DispatcherService.RunOnMainThread(()=>
                    RaisePropertyChanged(() => IsLoading));
                
            }
        }

        #endregion

        #region Commands

        public RelayCommand UploadToPocketCodeOrgCommand { get; private set; }

        public RelayCommand ShareWithOtherAppCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        #endregion

        #region Actions

        private async void UploadToPocketCodeOrgAction()
        {
            ServiceLocator.NavigationService.NavigateTo<UploadProgramLoadingViewModel>();

            JSONStatusResponse statusResponse = 
                await ServiceLocator.WebCommunicationService.CheckTokenAsync(
                Context.CurrentUserName, Context.CurrentToken, 
                ServiceLocator.CultureService.GetCulture().TwoLetterISOLanguageName);

            if (statusResponse.statusCode == StatusCodes.ServerResponseOk)
            {
                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    ServiceLocator.NavigationService.NavigateTo<UploadProgramViewModel>();
                    ServiceLocator.NavigationService.RemoveBackEntry();
                });
            }
            else
            {
                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    ServiceLocator.NavigationService.NavigateTo<UploadProgramLoginViewModel>();
                    ServiceLocator.NavigationService.RemoveBackEntry();
                });
            }
            ServiceLocator.NavigationService.RemoveBackEntry();
        }

        private async void ShareWithOtherAppAction()
        {
            await ServiceLocator.ShareService.ShareFile(_tempShareFilePath);   
        }

        protected override void GoBackAction()
        {
            if (IsLoading == false)
            {
                ResetViewModel();
                base.GoBackAction();
            }
        }

        #endregion

        #region Message Actions

        private void ContextChangedMessageAction(GenericMessage<CatrobatContextBase> message)
        {
            Context = message.Content;
        }

        private void CurrentProgramMessageAction(GenericMessage<Program> message)
        {
            CurrentProgram = message.Content;
        }

        #endregion

        public ProgramExportViewModel()
        {
            UploadToPocketCodeOrgCommand = new RelayCommand(
                UploadToPocketCodeOrgAction);
            ShareWithOtherAppCommand = new RelayCommand(
                ShareWithOtherAppAction);

            Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
                ViewModelMessagingToken.ContextListener, ContextChangedMessageAction);

            Messenger.Default.Register<GenericMessage<Program>>(this,
                ViewModelMessagingToken.CurrentProgramChangedListener, CurrentProgramMessageAction);
        }

        public async override void NavigateTo()
        {
            IsLoading = true;
            var programName = CurrentProgram.Name;

            string fileName = programName + StorageConstants.SupportedCatrobatFileTypes.ElementAt(0);

            _tempShareFilePath = Path.Combine(StorageConstants.TempProgramExportZipPath, fileName);
            using (var storage = StorageSystem.GetStorage())
            {
                var tempFileStream = await storage.OpenFileAsync(_tempShareFilePath,
                    StorageFileMode.Create, StorageFileAccess.ReadWrite);

                var programPackageStream = await ServiceLocator.ProgramExportService.
                  CreateProgramPackageForExport(programName);
                await programPackageStream.CopyToAsync(tempFileStream);
            }

            IsLoading = false;
        }

        public async override void NavigateFrom()
        {
            await ServiceLocator.ProgramExportService.CleanUpExport();
            base.NavigateFrom();
        }

        private void ResetViewModel()
        {
            IsLoading = true;
        }
    }
}