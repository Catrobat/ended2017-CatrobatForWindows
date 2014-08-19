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
        private ExtractProgramResult _extractResult;
        private CheckProgramResult _checkResult;
        private OnlineProgramHeader _onlineProjectHeader;
        private XmlProgram _convertedProject;
        private string _programName;
        private CancellationTokenSource _cancellationTokenSource;

        public void SetProgramStream(Stream programStream)
        {
            _programStream = programStream;
        }

        public void SetDownloadHeader(OnlineProgramHeader projectHeader)
        {
            _onlineProjectHeader = projectHeader;
        }

        public async Task<ExtractProgramResult> ExtractProgram()
        {
            _extractResult = new ExtractProgramResult { Status = ExtractProgramStatus.Success };

            try
            {
                if (_programStream == null && _onlineProjectHeader == null)
                    throw new Exception(
                        "SetProgramStream or SetDownloadHeader have to be called before calling StartImportProject.");

                if (_programStream == null && _onlineProjectHeader == null)
                    throw new Exception("SetProgramStream and SetDownloadHeader cannot be used together.");

                if (_onlineProjectHeader != null)
                {
                    _programStream = await Task.Run(() => ServiceLocator.WebCommunicationService.DownloadAsync(
                        _onlineProjectHeader.DownloadUrl, _onlineProjectHeader.ProjectName));
                }
                //if (_onlineProjectHeader != null)
                //{
                //    await Task.Run(() => ServiceLocator.WebCommunicationService.DownloadAsyncAlternativ(
                //        _onlineProjectHeader.DownloadUrl, _onlineProjectHeader.ProjectName));
                //}
                //using (var storage = StorageSystem.GetStorage())
                //{
                //    var stream = await storage.OpenFileAsync(Path.Combine(StorageConstants.TempProgramImportZipPath, _onlineProjectHeader.ProjectName + ".catrobat"),
                //            StorageFileMode.Open, StorageFileAccess.Read); // TODO move to resources
                //    await ServiceLocator.ZipService.UnzipCatrobatPackageIntoIsolatedStorage(
                //       stream, StorageConstants.TempProgramImportPath);
                //}

                await ServiceLocator.ZipService.UnzipCatrobatPackageIntoIsolatedStorage(
                    _programStream, StorageConstants.TempProgramImportPath);
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

        public async Task<CheckProgramResult> CheckProgram()
        {
            return await ServiceLocator.ProgramValidationService.CheckProgram(StorageConstants.TempProgramImportPath);


            //_checkResult = new CheckProgramImportResult();
            //PortableImage projectScreenshot = null;

            //using (var storage = StorageSystem.GetStorage())
            //{
            //    projectScreenshot =
            //        await storage.LoadImageAsync(Path.Combine(
            //        StorageConstants.TempProgramImportPath, 
            //        StorageConstants.ProgramManualScreenshotPath)) ??
            //        await storage.LoadImageAsync(Path.Combine(
            //        StorageConstants.TempProgramImportPath, 
            //        StorageConstants.ProgramAutomaticScreenshotPath));
            //}

            //var projectCodePath = Path.Combine(
            //    StorageConstants.TempProgramImportPath, StorageConstants.ProgramCodePath);

            //var converterResult = await CatrobatVersionConverter.
            //    ConvertToXmlVersion(projectCodePath, Constants.TargetIDEVersion);

            //if (converterResult.Error != CatrobatVersionConverter.VersionConverterStatus.NoError)
            //{
            //    switch (converterResult.Error)
            //    {
            //        case CatrobatVersionConverter.VersionConverterStatus.VersionTooNew:
            //            _checkResult.Status = ProgramImportStatus.VersionTooNew;
            //            break;
            //        case CatrobatVersionConverter.VersionConverterStatus.VersionTooOld:
            //            _checkResult.Status = ProgramImportStatus.VersionTooOld;
            //            break;
            //        default:
            //            _checkResult.Status = ProgramImportStatus.Damaged;
            //            break;
            //    }
            //    return _checkResult;
            //}

            //try
            //{
            //    _convertedProject = new XmlProgram(converterResult.Xml);
            //}
            //catch (Exception)
            //{
            //    _checkResult.Status = ProgramImportStatus.Damaged;
            //    _checkResult.ProjectHeader = null;
            //    return _checkResult;
            //}

            //_programName = _onlineProjectHeader != null ? 
            //    _onlineProjectHeader.ProjectName : 
            //    XmlProgramHelper.GetProgramName(converterResult.Xml);

            //_checkResult.ProjectHeader = new LocalProjectHeader
            //{
            //    Screenshot = projectScreenshot,
            //    ProjectName = _programName
            //};

            //_checkResult.Status = ProgramImportStatus.Valid;
            //return _checkResult;
        }

        public async Task<string> AcceptTempProject()
        {
            var uniqueProgramName = await ServiceLocator.ContextService.
                FindUniqueProgramName(_programName);

            Debug.WriteLine("Starting with _convertedProject.Save in AcceptTempProject");
            if (_convertedProject != null) // if previour conversion was OK
            {
                await _convertedProject.Save(Path.Combine(
                    StorageConstants.TempProgramImportPath, StorageConstants.ProgramCodePath));
            }

            // if previour conversion was not OK
            var renameResult = await ServiceLocator.ContextService.RenameProgram(
                Path.Combine(StorageConstants.TempProgramImportPath,
                StorageConstants.ProgramCodePath),
                uniqueProgramName);

            if (_checkResult != null)
                _checkResult.ProjectHeader.ProjectName = renameResult.NewProjectName;

            Debug.WriteLine("Starting with storage.MoveDirectoryAsyn in AcceptTempProject");
            using (var storage = StorageSystem.GetStorage())
            {
                var newPath = Path.Combine(StorageConstants.ProgramsPath,
                    renameResult.NewProjectName);
                await storage.MoveDirectoryAsync(StorageConstants.TempProgramImportPath,
                    newPath);
            }
            Debug.WriteLine("Starting with CreateThumbnailsForNewProgram in AcceptTempProject");
            await ServiceLocator.ContextService.
                CreateThumbnailsForNewProgram(uniqueProgramName);

            _extractResult = null;
            _checkResult = null;

            return uniqueProgramName;
        }

        public async Task CancelImport()
        {
            _cancellationTokenSource.Cancel();
        }


        public async Task TryImportWithStatusNotifications()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Debug.WriteLine("Starting with ExtractProgram");
            var extracionResult = await ServiceLocator.ProgramImportService.ExtractProgram();

            if (extracionResult.Status == ExtractProgramStatus.Error)
            {
                ServiceLocator.NotifictionService.ShowToastNotification(
                        AppResources.Import_ProgramDamaged,
                        AppResources.Import_CatrobatDamagedText,
                        ToastDisplayDuration.Long);
                return;
            }
            if (_cancellationTokenSource.Token.IsCancellationRequested == true)
            {
                ServiceLocator.NotifictionService.ShowToastNotification(
                        "Import aborted",
                        "Import was aborted by a user-interaction.",
                        ToastDisplayDuration.Long);  // localize
                return;
                //_cancellationTokenSource.Token.ThrowIfCancellationRequested();
            }
            Debug.WriteLine("Starting with CheckProgram");
            var validateResult = await ServiceLocator.ProgramImportService.CheckProgram();
            _programName = validateResult.ProjectHeader.ProjectName;
            var acceptProject = true;

            switch (validateResult.State)
            {
                case ProgramState.Valid:
                    acceptProject = true;
                    break;
                case ProgramState.Damaged:
                    ServiceLocator.NotifictionService.ShowToastNotification(
                        AppResources.Import_ProgramDamaged,
                        AppResources.Import_ProgramDamagedText,
                        ToastDisplayDuration.Long);

                    acceptProject = false;
                    break;
                case ProgramState.VersionTooOld:
                    ServiceLocator.NotifictionService.ShowToastNotification(
                        AppResources.Import_ProgramOutdated,
                        AppResources.Import_ProgramOutdatedText,
                        ToastDisplayDuration.Long);

                    acceptProject = false;
                    break;
                case ProgramState.VersionTooNew:
                    ServiceLocator.NotifictionService.ShowToastNotification(
                        AppResources.Import_AppTooOld,
                        AppResources.Import_AppTooOldText,
                        ToastDisplayDuration.Long);

                    acceptProject = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (_cancellationTokenSource.Token.IsCancellationRequested == true)
            {
                ServiceLocator.NotifictionService.ShowToastNotification(
                        "Import aborted",
                        "Import was aborted by a user-interaction.",
                        ToastDisplayDuration.Long);  // localize
                return;
                //_cancellationTokenSource.Token.ThrowIfCancellationRequested();
            }

            Debug.WriteLine("Starting with AcceptTempProject");
            if (acceptProject)
            {
                await ServiceLocator.ProgramImportService.AcceptTempProject();
                var localProjectsChangedMessage = new MessageBase();
                Messenger.Default.Send(localProjectsChangedMessage,
                    ViewModelMessagingToken.LocalProgramsChangedListener);

                ServiceLocator.NotifictionService.ShowToastNotification(
                    AppResources.Import_ProgramAdded,
                    AppResources.Import_ProgramAddedText,
                    ToastDisplayDuration.Long,
                    ToastTag.ImportFinished);
            }
            Debug.WriteLine("Finished");
        }
    }
}
