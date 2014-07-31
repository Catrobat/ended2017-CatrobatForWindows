using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.Xml;
using Catrobat.IDE.Core.Xml.VersionConverter;
using Catrobat.IDE.Core.Xml.XmlObjects;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.Services.Common
{
    public class ProgramImporterService : IProgramImporterService
    {
        private Stream _projectStream;
        private ExtractProgramResult _extractResult;
        private CheckProgramImportResult _checkResult;
        private OnlineProjectHeader _onlineProjectHeader;
        private XmlProgram _convertedProject;
        private string _programName;

        public void SetProjectStream(Stream projectStream)
        {
            _projectStream = projectStream;
        }

        public void SetDownloadHeader(OnlineProjectHeader projectHeader)
        {
            _onlineProjectHeader = projectHeader;
        }

        public async Task<ExtractProgramResult> ExtractProgram()
        {
            _extractResult = new ExtractProgramResult { Status = ExtractProgramStatus.Success };

            try
            {
                var projectName = "";

                if (_projectStream == null && _onlineProjectHeader == null)
                    throw new Exception(
                        "SetProjectStream or SetDownloadHeader have to be called before calling StartImportProject.");

                if (_projectStream == null && _onlineProjectHeader == null)
                    throw new Exception("SetProjectStream and SetDownloadHeader cannot be used together.");

                if (_onlineProjectHeader != null)
                {
                    _projectStream = await ServiceLocator.WebCommunicationService.DownloadAsync(
                        _onlineProjectHeader.DownloadUrl, _onlineProjectHeader.ProjectName);
                }

                await ServiceLocator.ZipService.UnzipCatrobatPackageIntoIsolatedStorage(
                    _projectStream, StorageConstants.TempProgramImportPath);
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

        public async Task<CheckProgramImportResult> CheckProgram()
        {
            _checkResult = new CheckProgramImportResult();
            PortableImage projectScreenshot = null;

            using (var storage = StorageSystem.GetStorage())
            {
                projectScreenshot =
                    await storage.LoadImageAsync(Path.Combine(
                    StorageConstants.TempProgramImportPath, 
                    StorageConstants.ProgramManualScreenshotPath)) ??
                    await storage.LoadImageAsync(Path.Combine(
                    StorageConstants.TempProgramImportPath, 
                    StorageConstants.ProgramAutomaticScreenshotPath));
            }

            var projectCodePath = Path.Combine(
                StorageConstants.TempProgramImportPath, StorageConstants.ProgramCodePath);

            var converterResult = await CatrobatVersionConverter.
                ConvertToXmlVersion(projectCodePath, Constants.TargetIDEVersion);

            if (converterResult.Error != CatrobatVersionConverter.VersionConverterError.NoError)
            {
                switch (converterResult.Error)
                {
                    case CatrobatVersionConverter.VersionConverterError.VersionTooNew:
                        _checkResult.Status = ProgramImportStatus.VersionTooNew;
                        break;
                    case CatrobatVersionConverter.VersionConverterError.VersionTooOld:
                        _checkResult.Status = ProgramImportStatus.VersionTooOld;
                        break;
                    default:
                        _checkResult.Status = ProgramImportStatus.Damaged;
                        break;
                }
                return _checkResult;
            }

            try
            {
                _convertedProject = new XmlProgram(converterResult.Xml);
            }
            catch (Exception)
            {
                _checkResult.Status = ProgramImportStatus.Damaged;
                _checkResult.ProjectHeader = null;
                return _checkResult;
            }

            _programName = _onlineProjectHeader != null ? 
                _onlineProjectHeader.ProjectName : 
                XmlProgramHelper.GetProgramName(converterResult.Xml);

            _checkResult.ProjectHeader = new LocalProjectHeader
            {
                Screenshot = projectScreenshot,
                ProjectName = _programName
            };

            _checkResult.Status = ProgramImportStatus.Valid;
            return _checkResult;
        }

        public async Task<string> AcceptTempProject()
        {
            var uniqueProgramName = await ServiceLocator.ContextService.
                FindUniqueProgramName(_programName);

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

            using (var storage = StorageSystem.GetStorage())
            {
                var newPath = Path.Combine(StorageConstants.ProgramsPath,
                    renameResult.NewProjectName);
                await storage.MoveDirectoryAsync(StorageConstants.TempProgramImportPath,
                    newPath);
            }

            await ServiceLocator.ContextService.
                CreateThumbnailsForNewProgram(uniqueProgramName);

            _extractResult = null;
            _checkResult = null;

            return uniqueProgramName;
        }

        public async Task CancelImport()
        {
            throw new NotImplementedException();
            // TODO: cancel import

            //_extractResult = null;

            //using (var storage = StorageSystem.GetStorage())
            //{
            //    await storage.DeleteDirectoryAsync(CatrobatContextBase.TempProjectImportPath);
            //    await storage.DeleteDirectoryAsync(CatrobatContextBase.TempProjectImportZipPath);
            //}
        }


        public async Task TryImportWithStatusNotifications()
        {
            var extracionResult = await ServiceLocator.ProjectImporterService.ExtractProgram();

            if (extracionResult.Status == ExtractProgramStatus.Error)
            {
                ServiceLocator.NotifictionService.ShowToastNotification(
                        "Program damaged",
                        "The catrobat file is dameged.",
                        ToastDisplayDuration.Long); // TODO: localize me
                return;
            }

            var validateResult = await ServiceLocator.ProjectImporterService.CheckProgram();

            var acceptProject = true;

            switch (validateResult.Status)
            {
                case ProgramImportStatus.Valid:
                    acceptProject = true;
                    break;
                case ProgramImportStatus.Damaged:
                    ServiceLocator.NotifictionService.ShowToastNotification(
                        "Program damaged",
                        "Program damaged and cannot be added!",
                        ToastDisplayDuration.Long); // TODO: localize me

                    acceptProject = false;
                    break;
                case ProgramImportStatus.VersionTooOld:
                    ServiceLocator.NotifictionService.ShowToastNotification(
                        "Program outdated",
                        "Program is too old and cannot be added!",
                        ToastDisplayDuration.Long); // TODO: localize me

                    acceptProject = false;
                    break;
                case ProgramImportStatus.VersionTooNew:
                    ServiceLocator.NotifictionService.ShowToastNotification(
                        "App version too old",
                        "The downloaded program requires a newer version of this App!",
                        ToastDisplayDuration.Long); // TODO: localize me

                    acceptProject = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (acceptProject)
            {
                await ServiceLocator.ProjectImporterService.AcceptTempProject();
                var localProjectsChangedMessage = new MessageBase();
                Messenger.Default.Send(localProjectsChangedMessage,
                    ViewModelMessagingToken.LocalProjectsChangedListener);

                ServiceLocator.NotifictionService.ShowToastNotification(
                    "Program added",
                    "Program successfully added to your program list.",
                    ToastDisplayDuration.Long); // TODO: localize me
            }
        }
    }
}
