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
using Catrobat.IDE.Core.Resources.Localization;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class ProgramImportService : IProgramImportService
    {
        private Stream _programStream;
        private OnlineProgramHeader _onlineProgramHeader;
        private string _programName;
        private CancellationTokenSource _cancellationTokenSource;

        public void SetProgramStream(Stream programStream)
        {
            _programStream = programStream;
        }

        public void SetDownloadHeader(OnlineProgramHeader programHeader)
        {
            _onlineProgramHeader = programHeader;
            _programName = _onlineProgramHeader.ProjectName;
        }

        public async Task<ExtractProgramResult> ExtractProgram(CancellationToken taskCancellationToken)
        {
            ExtractProgramResult _extractResult = new ExtractProgramResult { Status = ExtractProgramStatus.Success };

            try
            {
                if (_programStream == null && _onlineProgramHeader == null)
                    throw new Exception(
                        "SetProgramStream or SetDownloadHeader have to be called before calling StartImportProgram.");

                if (_programStream == null && _onlineProgramHeader == null)
                    throw new Exception("SetProgramStream and SetDownloadHeader cannot be used together.");

                // old simple portable downloader
                var cancellationTokenSource = new CancellationTokenSource();
                if (_onlineProgramHeader != null)
                {
                    _programStream = await Task.Run(() => ServiceLocator.WebCommunicationService.DownloadAsync(
                        _onlineProgramHeader.DownloadUrl, _onlineProgramHeader.ProjectName, cancellationTokenSource.Token), taskCancellationToken);
                }
                // new downloader
                //if (_onlineProgramHeader != null)
                //{
                //    await Task.Run(() => ServiceLocator.WebCommunicationService.DownloadAsyncAlternativ(
                //        _onlineProgramHeader.DownloadUrl, _onlineProgramHeader.ProjectName), taskCancellationToken);
                //}
                //using (var storage = StorageSystem.GetStorage())
                //{
                //    var stream = await storage.OpenFileAsync(Path.Combine(StorageConstants.TempProgramImportZipPath, _onlineProgramHeader.ProjectName + ".catrobat"),
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
                if (await storage.DirectoryExistsAsync(StorageConstants.TempProgramImportZipPath))
                {
                    await storage.DeleteDirectoryAsync(StorageConstants.TempProgramImportZipPath);
                }
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
            Debug.WriteLine("Starting with CreateThumbnailsForNewProgram in AcceptTempProgram");
            await ServiceLocator.ContextService.
                CreateThumbnailsForNewProgram(uniqueProgramName);

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
                        AppResources.Import_ProgramDamaged,
                        AppResources.Import_CatrobatDamagedText,
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
                        AppResources.Import_ProgramDamaged,
                        ToastDisplayDuration.Long);
                    break;
                case ProgramState.VersionTooOld:
                    ServiceLocator.NotifictionService.ShowToastNotification("",
                        AppResources.Import_ProgramOutdated,
                        ToastDisplayDuration.Long);
                    break;
                case ProgramState.VersionTooNew:
                    ServiceLocator.NotifictionService.ShowToastNotification("",
                        AppResources.Import_AppTooOld,
                        ToastDisplayDuration.Long);
                    break;
                case ProgramState.ErrorInThisApp:
                    ServiceLocator.NotifictionService.ShowToastNotification("",
                        AppResources.Import_GeneralError,
                        ToastDisplayDuration.Long);
                    break;
                case ProgramState.Unknown:
                    ServiceLocator.NotifictionService.ShowToastNotification("",
                        AppResources.Import_GeneralError,
                        ToastDisplayDuration.Long);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Debug.WriteLine("Starting with AcceptTempProgram");
            await ServiceLocator.ProgramImportService.AcceptTempProgram();
            var localProgramsChangedMessage = new MessageBase();
            Messenger.Default.Send(localProgramsChangedMessage,
                ViewModelMessagingToken.LocalProgramsChangedListener);

            ServiceLocator.NotifictionService.ShowToastNotification("",
                AppResources.Import_ProgramAdded,
                ToastDisplayDuration.Long,
                ToastTag.ImportFin);

            Debug.WriteLine("Finished");
        }

        private async Task CleanUpImport()
        {
            ServiceLocator.NotifictionService.ShowToastNotification(
                        AppResources.Import_CanceledText,
                        AppResources.Import_CanceledText,
                        ToastDisplayDuration.Long);
            using (var storage = StorageSystem.GetStorage())
            {
                if (await storage.DirectoryExistsAsync(StorageConstants.TempProgramImportPath))
                {
                    await storage.DeleteDirectoryAsync(StorageConstants.TempProgramImportPath);
                }
            }
        }
    }
}
