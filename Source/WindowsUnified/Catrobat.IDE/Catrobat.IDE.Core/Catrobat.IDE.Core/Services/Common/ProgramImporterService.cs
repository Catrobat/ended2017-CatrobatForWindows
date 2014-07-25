using System;
using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Xml.VersionConverter;
using Catrobat.IDE.Core.Xml.XmlObjects;

namespace Catrobat.IDE.Core.Services.Common
{
    public class ProgramImporterService : IProgramImporterService
    {
        private XmlProject _project;
        private Stream _projectStream;
        private ProgramImportResult _importResult;

        public void SetProjectStream(Stream projectStream)
        {
            _projectStream = projectStream;
        }

        public async Task<ProgramImportResult> StartImportProject()
        {
            _importResult = new ProgramImportResult {Status = ProgramImportStatus.Damaged};
            
            try
            {
                if(_projectStream == null)
                    throw new Exception(
                        "SetProjectStream was not called to set the project stream.");

                await ServiceLocator.ZipService.UnzipCatrobatPackageIntoIsolatedStorage(
                    _projectStream, CatrobatContextBase.TempProjectImportPath);

                PortableImage projectScreenshot = null;
                string projectCode = "";

                using (var storage = StorageSystem.GetStorage())
                {
                    projectScreenshot = 
                        await storage.LoadImageAsync(Path.Combine(
                        CatrobatContextBase.TempProjectImportPath, Project.ScreenshotPath)) ??
                        await storage.LoadImageAsync(Path.Combine(
                        CatrobatContextBase.TempProjectImportPath, Project.AutomaticScreenshotPath));
                }
                var projectCodePath = Path.Combine(
                    CatrobatContextBase.TempProjectImportPath, Project.ProjectCodePath);
                
                var converterResult = await CatrobatVersionConverter.
                    ConvertToXmlVersion(projectCodePath, Constants.TargetIDEVersion);

                if (converterResult.Error != CatrobatVersionConverter.VersionConverterError.NoError)
                {
                    switch (converterResult.Error)
                    {
                        case CatrobatVersionConverter.VersionConverterError.VersionTooNew:
                            _importResult.Status = ProgramImportStatus.VersionTooNew;
                            break;
                        case CatrobatVersionConverter.VersionConverterError.VersionTooOld:
                            _importResult.Status = ProgramImportStatus.VersionTooOld;
                            break;
                        default:
                            _importResult.Status = ProgramImportStatus.Damaged;
                            break;
                    }
                    return _importResult;
                }

                var project = new XmlProject(converterResult.Xml);
                await project.Save();

                _importResult.ProjectHeader = new LocalProjectHeader
                {
                    Screenshot = projectScreenshot,
                    ProjectName = project.ProjectHeader.ProgramName
                };

                _importResult.Status = ProgramImportStatus.Valid;
            }
            catch (Exception)
            {
                _importResult.ProjectHeader = null;
                _importResult.Status = ProgramImportStatus.Damaged;
            }

            using (var storage = StorageSystem.GetStorage())
            {
                if (await storage.FileExistsAsync(CatrobatContextBase.TempProjectImportZipPath))
                {
                    await storage.DeleteDirectoryAsync(CatrobatContextBase.TempProjectImportZipPath);
                }
            }

            return _importResult;
        }

        public async Task<string> AcceptTempProject()
        {
            var newProjectName = "";

            using (var storage = StorageSystem.GetStorage())
            {
                var counter = 0;
                while (true)
                {
                    var projectPath = Path.Combine(CatrobatContextBase.ProjectsPath, 
                        _importResult.ProjectHeader.ProjectName);

                    if (counter != 0)
                    {
                        projectPath = Path.Combine(CatrobatContextBase.ProjectsPath,
                            StringExtensions.Concat(_importResult.ProjectHeader.ProjectName, 
                            counter.ToString()));
                    }

                    if (!await storage.DirectoryExistsAsync(projectPath))
                    {
                        break;
                    }

                    counter++;
                }

                newProjectName = _importResult.ProjectHeader.ProjectName;

                if (counter != 0)
                {
                    newProjectName = _importResult.ProjectHeader.ProjectName + counter;
                    _project.ProjectHeader.ProgramName = newProjectName;
                    var saveToPath = Path.Combine(CatrobatContextBase.TempProjectImportPath, Project.ProjectCodePath);
                    await _project.Save(saveToPath);
                }
            }

            using (var storage = StorageSystem.GetStorage())
            {
                var tempPath = Path.Combine(CatrobatContextBase.ProjectsPath, _project.ProjectHeader.ProgramName);
                await storage.MoveDirectoryAsync(CatrobatContextBase.TempProjectImportPath, tempPath);
            }

            _importResult = null;
            _project = null;

            return newProjectName;
        }

        public async Task CancelImport()
        {
            _importResult = null;
            _project = null;

            using (var storage = StorageSystem.GetStorage())
            {
                await storage.DeleteDirectoryAsync(CatrobatContextBase.TempProjectImportPath);
                await storage.DeleteDirectoryAsync(CatrobatContextBase.TempProjectImportZipPath);
            }
        }
    }
}
