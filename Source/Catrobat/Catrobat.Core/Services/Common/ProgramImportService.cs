using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.Xml;
using Catrobat.IDE.Core.Xml.VersionConverter;
using Catrobat.IDE.Core.Xml.XmlObjects;
using GalaSoft.MvvmLight.Messaging;
using Catrobat.Core.Resources.Localization;
using Catrobat.Core.Models.OnlinePrograms;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class ProgramImportService : IProgramImportService
    {
        private Stream _programStream;
        private ProgramInfo _onlineProgram;
        private string _programName;
        private CancellationTokenSource _cancellationTokenSource;
        private MessageboxResult _missingFilesCallbackResult;

        public void SetProgramStream(Stream programStream)
        {
            _programStream = programStream;
        }

        public void SetDownloadHeader(ProgramInfo program)
        {
            _onlineProgram = program;
            _programName = _onlineProgram.Name;
        }

        public async Task<ExtractProgramResult> ExtractProgram(CancellationToken taskCancellationToken)
        {
            ExtractProgramResult _extractResult = new ExtractProgramResult { Status = ExtractProgramStatus.Success };

            try
            {
                if (_programStream == null && _onlineProgram == null)
                    throw new Exception(
                        "SetProgramStream or SetDownloadHeader have to be called before calling StartImportProgram.");

                if (_programStream == null && _onlineProgram == null)
                    throw new Exception("SetProgramStream and SetDownloadHeader cannot be used together.");

                // old simple portable downloader
                var cancellationTokenSource = new CancellationTokenSource();
                if (_onlineProgram != null)
                {
                    _programStream = await Task.Run(() => ServiceLocator.WebCommunicationService.DownloadAsync(
                        _onlineProgram.DownloadUrl, _onlineProgram.Name, cancellationTokenSource.Token), taskCancellationToken);
                }
                // new downloader
                //if (_onlineProgram != null)
                //{
                //    await Task.Run(() => ServiceLocator.WebCommunicationService.DownloadAsyncAlternativ(
                //        _onlineProgram.DownloadUrl, _onlineProgram.ProjectName), taskCancellationToken);
                //}
                //using (var storage = StorageSystem.GetStorage())
                //{
                //    var stream = await storage.OpenFileAsync(Path.Combine(StorageConstants.TempProgramImportZipPath, _onlineProgram.ProjectName + ".catrobat"),
                //            StorageFileMode.Open, StorageFileAccess.Read); 
                //    await ServiceLocator.ZipService.UnzipCatrobatPackageIntoIsolatedStorage(
                //       stream, StorageConstants.TempProgramImportPath);
                //}

                if (_cancellationTokenSource.Token.IsCancellationRequested == false)
                {
                    await ServiceLocator.ZipService.UnzipCatrobatPackageIntoIsolatedStorage(
                        _programStream, StorageConstants.TempProgramImportPath);
                }
            }
            catch (Exception)
            {
                _extractResult.Status = ExtractProgramStatus.Error;
            }

            using (var storage = StorageSystem.GetStorage())
            {
                await storage.DeleteDirectoryAsync(StorageConstants.TempProgramImportZipPath);
            }
            return _extractResult;
        }

        public async Task<string> AcceptTempProgram()
        {
            XmlProgramRenamerResult renameResult = new XmlProgramRenamerResult { NewProgramName = _programName };
            var uniqueProgramName = await ServiceLocator.ContextService.
                FindUniqueProgramName(_programName);

            if (_programName != uniqueProgramName)
            {
                renameResult = await ServiceLocator.ContextService.RenameProgram(
                    Path.Combine(StorageConstants.TempProgramImportPath,
                    StorageConstants.ProgramCodePath),
                    uniqueProgramName);
            }

            using (var storage = StorageSystem.GetStorage())
            {
                var newPath = Path.Combine(StorageConstants.ProgramsPath,
                    renameResult.NewProgramName);
                await storage.MoveDirectoryAsync(StorageConstants.TempProgramImportPath,
                    newPath);
            }
            return uniqueProgramName;
        }

        public async Task CancelImport()
        {
            if (_cancellationTokenSource != null)
                _cancellationTokenSource.Cancel();
        }


        public async Task TryImportWithStatusNotifications()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Debug.WriteLine("Starting with ExtractProgram");
            ExtractProgramResult extractionResult = await ServiceLocator.ProgramImportService.ExtractProgram(_cancellationTokenSource.Token);

            if (_cancellationTokenSource.Token.IsCancellationRequested == true)
            {
                await CleanUpImport();
                return;
            }
            if (extractionResult.Status == ExtractProgramStatus.Error)
            {
                ServiceLocator.NotifictionService.ShowToastNotification(
                        AppResourcesHelper.Get("Import_ProgramDamaged"),
                        AppResourcesHelper.Get("Import_CatrobatDamagedText"),
                        ToastDisplayDuration.Long);
                return;
            }
            
            Debug.WriteLine("Starting with CheckProgram");
            var validateResult = await ServiceLocator.ProgramValidationService.CheckProgram(StorageConstants.TempProgramImportPath);

            if (_cancellationTokenSource.Token.IsCancellationRequested == true)
            {
                await CleanUpImport();
                return;
            }

            switch (validateResult.State)
            {
                case ProgramState.Valid:
                    _programName = validateResult.ProgramHeader.ProjectName;
                    break;
                case ProgramState.Damaged:
                    ServiceLocator.NotifictionService.ShowToastNotification("",
                        AppResourcesHelper.Get("Import_ProgramDamaged"),
                        ToastDisplayDuration.Long);
                    break;
                case ProgramState.VersionTooOld:
                    ServiceLocator.NotifictionService.ShowToastNotification("",
                        AppResourcesHelper.Get("Import_ProgramOutdated"),
                        ToastDisplayDuration.Long);
                    break;
                case ProgramState.VersionTooNew:
                    ServiceLocator.NotifictionService.ShowToastNotification("",
                        AppResourcesHelper.Get("Import_AppTooOld"),
                        ToastDisplayDuration.Long);
                    break;
                case ProgramState.ErrorInThisApp:
                    ServiceLocator.NotifictionService.ShowToastNotification("",
                        AppResourcesHelper.Get("Import_GeneralError"),
                        ToastDisplayDuration.Long);
                    break;
                case ProgramState.Unknown:
                    ServiceLocator.NotifictionService.ShowToastNotification("",
                        AppResourcesHelper.Get("Import_GeneralError"),
                        ToastDisplayDuration.Long);
                    break;
                case ProgramState.FilesMissing:
                    ServiceLocator.NotifictionService.ShowMessageBox(AppResourcesHelper.Get("Import_Canceled"),
                    AppResourcesHelper.Get("Import_FilesMissing"), MissingFilesCallback, MessageBoxOptions.Ok);
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Debug.WriteLine("Starting with AcceptTempProgram");
            await ServiceLocator.ProgramImportService.AcceptTempProgram();
            var localProgramsChangedMessage = new MessageBase();
            Messenger.Default.Send(localProgramsChangedMessage,
                ViewModelMessagingToken.LocalProgramsChangedListener);

            ServiceLocator.NotifictionService.ShowToastNotification("",
                AppResourcesHelper.Get("Import_ProgramAdded"),
                ToastDisplayDuration.Long,
                ToastTag.ImportFin);

            Debug.WriteLine("Finished");
        }

        private async Task CleanUpImport()
        {
            ServiceLocator.NotifictionService.ShowToastNotification(
                        AppResourcesHelper.Get("Import_CanceledText"),
                        AppResourcesHelper.Get("Import_CanceledText"),
                        ToastDisplayDuration.Long);
            using (var storage = StorageSystem.GetStorage())
            {
                await storage.DeleteDirectoryAsync(StorageConstants.TempProgramImportPath);
            }
        }

        private async void MissingFilesCallback(MessageboxResult result)
        {
            _missingFilesCallbackResult = result;
            await CleanUpImport();
        }
    }
}
