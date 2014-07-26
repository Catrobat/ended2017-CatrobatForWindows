using System;
using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Xml;
using Catrobat.IDE.Core.Xml.VersionConverter;
using Catrobat.IDE.Core.Xml.XmlObjects;

namespace Catrobat.IDE.Core.Services.Common
{
    public class ProgramImporterService : IProgramImporterService
    {
        private Stream _projectStream;
        private ExtractProgramResult _extractResult;
        private CheckProgramImportResult _checkResult;
        private OnlineProjectHeader _onlineProjectHeader;
        private XmlProject _convertedProject;

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
                    _projectStream, StorageConstants.TempProjectImportPath);
            }
            catch (Exception)
            {
                _extractResult.Status = ExtractProgramStatus.Error;
            }

            using (var storage = StorageSystem.GetStorage())
            {
                if (await storage.FileExistsAsync(StorageConstants.TempProjectImportZipPath))
                {
                    await storage.DeleteDirectoryAsync(StorageConstants.TempProjectImportZipPath);
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
                    StorageConstants.TempProjectImportPath, Project.ScreenshotPath)) ??
                    await storage.LoadImageAsync(Path.Combine(
                    StorageConstants.TempProjectImportPath, Project.AutomaticScreenshotPath));
            }

            var projectCodePath = Path.Combine(
                StorageConstants.TempProjectImportPath, Project.ProjectCodePath);

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
                _convertedProject = new XmlProject(converterResult.Xml);
            }
            catch (Exception)
            {
                _checkResult.Status = ProgramImportStatus.Damaged;
                _checkResult.ProjectHeader = null;
                return _checkResult;
            }

            _checkResult.ProjectHeader = new LocalProjectHeader
            {
                Screenshot = projectScreenshot,
                ProjectName = _onlineProjectHeader.ProjectName
            };

            _checkResult.Status = ProgramImportStatus.Valid;
            return _checkResult;
        }

        public async Task<string> AcceptTempProject()
        {
            var uniqueProgramName = await ServiceLocator.ContextService.
                FindUniqueName(_onlineProjectHeader.ProjectName);


            if (_convertedProject != null) // if previour conversion was OK
            {
                await _convertedProject.Save(Path.Combine(
                    StorageConstants.TempProjectImportPath, Project.ProjectCodePath));
            }

            // if previour conversion was not OK
            var renameResult = await ServiceLocator.ContextService.RenameProgramFromFile(
                Path.Combine(StorageConstants.TempProjectImportPath,
                Project.ProjectCodePath),
                uniqueProgramName);

            if (_checkResult != null)
                _checkResult.ProjectHeader.ProjectName = renameResult.NewProjectName;

            using (var storage = StorageSystem.GetStorage())
            {
                var newPath = Path.Combine(StorageConstants.ProjectsPath,
                    renameResult.NewProjectName);
                await storage.MoveDirectoryAsync(StorageConstants.TempProjectImportPath,
                    newPath);
            }

            _extractResult = null;
            _checkResult = null;

            return renameResult.NewProjectName;
        }

        public async Task CancelImport()
        {
            // TODO: cancel import

            //_extractResult = null;

            //using (var storage = StorageSystem.GetStorage())
            //{
            //    await storage.DeleteDirectoryAsync(CatrobatContextBase.TempProjectImportPath);
            //    await storage.DeleteDirectoryAsync(CatrobatContextBase.TempProjectImportZipPath);
            //}
        }
    }
}
