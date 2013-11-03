using System;
using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.VersionConverter;

namespace Catrobat.IDE.Core.Services.Common
{
    public class ProjectImporterService
    {
        private ProjectDummyHeader _tempProjectHeader;
        private Project _project;

        public async Task<ProjectDummyHeader> ImportProject(Stream projectZipStream)
        {
            try
            {
                await CatrobatZipService.UnzipCatrobatPackageIntoIsolatedStorage(projectZipStream, CatrobatContextBase.TempProjectImportPath);

                PortableImage projectScreenshot = null;
                string projectCode = "";

                using (var storage = StorageSystem.GetStorage())
                {
                    projectScreenshot = await storage.LoadImageAsync(Path.Combine(CatrobatContextBase.TempProjectImportPath, Project.ScreenshotPath)) ??
                                        await storage.LoadImageAsync(Path.Combine(CatrobatContextBase.TempProjectImportPath, Project.AutomaticScreenshotPath));
                }

                CatrobatVersionConverter.VersionConverterError error;
                var projectCodePath = Path.Combine(CatrobatContextBase.TempProjectImportPath, Project.ProjectCodePath);
                
                
                var result = await CatrobatVersionConverter.ConvertToXmlVersion(projectCodePath, Constants.TargetIDEVersion);

                projectCode = result.Xml;

                //TODO: error handling

                _project = new Project(projectCode);
                await _project.Save();

                _tempProjectHeader = new ProjectDummyHeader
                {
                    Screenshot = projectScreenshot,
                    ProjectName = _project.ProjectHeader.ProgramName
                };
            }
            catch (Exception)
            {
                _tempProjectHeader = null;
            }


            if (_tempProjectHeader == null)
            {
                using (var storage = StorageSystem.GetStorage())
                {
                    if (storage.FileExists(CatrobatContextBase.TempProjectImportZipPath))
                    {
                        await storage.DeleteDirectoryAsync(CatrobatContextBase.TempProjectImportZipPath);
                    }
                }
            }


            using (var storage = StorageSystem.GetStorage())
            {
                if (storage.FileExists(CatrobatContextBase.TempProjectImportZipPath))
                {
                    await storage.DeleteDirectoryAsync(CatrobatContextBase.TempProjectImportZipPath);
                }
            }

            return _tempProjectHeader;
        }

        public async Task<string> AcceptTempProject()
        {
            var newProjectName = "";

            using (var storage = StorageSystem.GetStorage())
            {
                var counter = 0;
                while (true)
                {
                    var projectPath = Path.Combine(CatrobatContextBase.ProjectsPath, _tempProjectHeader.ProjectName);

                    if (counter != 0)
                    {
                        projectPath = Path.Combine(CatrobatContextBase.ProjectsPath,
                            StringExtensions.Concat(_tempProjectHeader.ProjectName, counter.ToString()));
                    }

                    if (!await storage.DirectoryExistsAsync(projectPath))
                    {
                        break;
                    }

                    counter++;
                }

                newProjectName = _tempProjectHeader.ProjectName;

                if (counter != 0)
                {
                    newProjectName = _tempProjectHeader.ProjectName + counter;
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

            _tempProjectHeader = null;
            _project = null;

            return newProjectName;
        }

        public async Task CancelImport()
        {
            _tempProjectHeader = null;
            _project = null;

            using (var storage = StorageSystem.GetStorage())
            {
                await storage.DeleteDirectoryAsync(CatrobatContextBase.TempProjectImportPath);
                await storage.DeleteDirectoryAsync(CatrobatContextBase.TempProjectImportZipPath);
            }

            _tempProjectHeader = null;
            _project = null;
        }
    }
}
