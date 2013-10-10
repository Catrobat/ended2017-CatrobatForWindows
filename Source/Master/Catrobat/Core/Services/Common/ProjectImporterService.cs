using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.Core.CatrobatObjects;
using Catrobat.Core.ExtensionMethods;
using Catrobat.Core.Services.Data;
using Catrobat.Core.Services.Storage;
using Catrobat.Core.VersionConverter;

namespace Catrobat.Core.Services.Common
{
    public class ProjectImporterService : IProjectImporterService
    {
        private ProjectDummyHeader _tempProjectHeader;
        private Project _project;

        public async Task<ProjectDummyHeader> ImportProjects(Stream projectZipStream)
        {
            try
            {
                CatrobatZipService.UnzipCatrobatPackageIntoIsolatedStorage(projectZipStream, CatrobatContextBase.TempProjectImportPath);

                PortableImage projectScreenshot = null;
                string projectCode = "";

                using (var storage = StorageSystem.GetStorage())
                {
                    projectScreenshot = storage.LoadImage(Path.Combine(CatrobatContextBase.TempProjectImportPath, Project.ScreenshotPath)) ??
                                        storage.LoadImage(Path.Combine(CatrobatContextBase.TempProjectImportPath, Project.AutomaticScreenshotPath));
                }

                CatrobatVersionConverter.VersionConverterError error;
                var projectCodePath = Path.Combine(CatrobatContextBase.TempProjectImportPath, Project.ProjectCodePath);
                projectCode = CatrobatVersionConverter.ConvertToXmlVersion(projectCodePath, Constants.TargetIDEVersion, out error);
                //TODO: error handling

                _project = new Project(projectCode);

                _tempProjectHeader = new ProjectDummyHeader
                {
                    Screenshot = projectScreenshot,
                    ProjectName = _project.ProjectHeader.ProgramName
                };
            }
            catch (Exception)
            {
                using (var storage = StorageSystem.GetStorage())
                {
                    if (storage.FileExists(CatrobatContextBase.TempProjectImportZipPath))
                    {
                        storage.DeleteDirectory(CatrobatContextBase.TempProjectImportZipPath);
                    }
                }

                _tempProjectHeader = null;
            }
            finally
            {
                using (var storage = StorageSystem.GetStorage())
                {
                    if (storage.FileExists(CatrobatContextBase.TempProjectImportZipPath))
                    {
                        storage.DeleteDirectory(CatrobatContextBase.TempProjectImportZipPath);
                    }
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
                        projectPath = Path.Combine(CatrobatContextBase.ProjectsPath, StringExtensions.Concat(_tempProjectHeader.ProjectName, counter.ToString()));
                    }

                    if (!storage.DirectoryExists(projectPath))
                    {
                        break;
                    }

                    counter++;
                }

                newProjectName = _tempProjectHeader.ProjectName;

                if (counter != 0)
                {
                    newProjectName = _tempProjectHeader.ProjectName + counter;
                    _project.SetProgramName(newProjectName);
                    var saveToPath = Path.Combine(CatrobatContextBase.TempProjectImportPath, Project.ProjectCodePath);
                    _project.Save(saveToPath);
                }
            }

            using (var storage = StorageSystem.GetStorage())
            {
                var tempPath = Path.Combine(CatrobatContextBase.ProjectsPath, _project.ProjectHeader.ProgramName);
                storage.MoveDirectory(CatrobatContextBase.TempProjectImportPath, tempPath);
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
                storage.DeleteDirectory(CatrobatContextBase.TempProjectImportPath);
                storage.DeleteDirectory(CatrobatContextBase.TempProjectImportZipPath);
            }

            _tempProjectHeader = null;
            _project = null;
        }
    }
}
