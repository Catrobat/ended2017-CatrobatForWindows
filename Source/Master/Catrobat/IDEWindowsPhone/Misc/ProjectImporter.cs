using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Catrobat.Core;
using Catrobat.Core.ExtensionMethods;
using Catrobat.Core.Objects;
using Catrobat.Core.Storage;
using Catrobat.Core.ZIP;
using Windows.Phone.Storage.SharedAccess;
using Windows.Storage;

namespace Catrobat.IDEWindowsPhone.Misc
{
    public class ProjectImporter
    {
        private ProjectDummyHeader _tempProjectHeader;
        private Project _project;

        public async Task<ProjectDummyHeader> ImportProjects(String fileToken)
        {
            try
            {
                using (var storage = StorageSystem.GetStorage())
                {
                    storage.DeleteDirectory(CatrobatContext.TempProjectImportPath);
                    storage.DeleteDirectory(CatrobatContext.TempProjectImportZipPath);
                }

                const string tempProjectZipName = "temp_project.catrobat";

                var tempSplitList = CatrobatContext.TempProjectImportPath.Split('/');
                var tempZipSplitList = CatrobatContext.TempProjectImportZipPath.Split('/');

                var localFolder = ApplicationData.Current.LocalFolder;
                var tempFolder = await localFolder.CreateFolderAsync(tempSplitList[0], CreationCollisionOption.OpenIfExists);
                var projectTempFolder = await tempFolder.CreateFolderAsync(tempSplitList[1], CreationCollisionOption.OpenIfExists);
                var projectTempZipFolder = await tempFolder.CreateFolderAsync(tempZipSplitList[1], CreationCollisionOption.OpenIfExists);

                await SharedStorageAccessManager.CopySharedFileAsync(projectTempZipFolder, tempProjectZipName, NameCollisionOption.ReplaceExisting, fileToken);

                var projectZipFile = await projectTempZipFolder.GetFileAsync(tempProjectZipName);
                var projectZipStream = await projectZipFile.OpenStreamForReadAsync();

                CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(projectZipStream, CatrobatContext.TempProjectImportPath);

                object projectScreenshot = null;
                string projectCode = "";

                using (var storage = StorageSystem.GetStorage())
                {
                    projectScreenshot = storage.LoadImage(Path.Combine(CatrobatContext.TempProjectImportPath, Project.ScreenshotPath));
                    projectCode = storage.ReadTextFile(Path.Combine(CatrobatContext.TempProjectImportPath, Project.ProjectCodePath));
                }

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
                    if (storage.FileExists(CatrobatContext.TempProjectImportZipPath))
                    {
                        storage.DeleteDirectory(CatrobatContext.TempProjectImportZipPath);
                    }
                }

                _tempProjectHeader = null;
            }
            finally
            {
                using (var storage = StorageSystem.GetStorage())
                {
                    if (storage.FileExists(CatrobatContext.TempProjectImportZipPath))
                    {
                        storage.DeleteDirectory(CatrobatContext.TempProjectImportZipPath);
                    }
                }
            }

            return _tempProjectHeader;
        }

        public void AcceptTempProject(bool setActive)
        {
            var newProjectName = "";

            using (var storage = StorageSystem.GetStorage())
            {
                var counter = 0;
                while (true)
                {
                    var projectPath = Path.Combine(CatrobatContext.ProjectsPath, _tempProjectHeader.ProjectName);

                    if (counter != 0)
                    {
                        projectPath = Path.Combine(CatrobatContext.ProjectsPath, StringExtensions.Concat(_tempProjectHeader.ProjectName, counter.ToString()));
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
                    _project.SetSetProgramName(newProjectName);
                    var saveToPath = Path.Combine(CatrobatContext.TempProjectImportPath, Project.ProjectCodePath);
                    _project.Save(saveToPath);
                }
            }

            using (var storage = StorageSystem.GetStorage())
            {
                var tempPath = Path.Combine(CatrobatContext.ProjectsPath, _project.ProjectHeader.ProgramName);
                storage.MoveDirectory(CatrobatContext.TempProjectImportPath, tempPath);
            }

            Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    if (setActive)
                    {
                        CatrobatContext.GetContext().SetCurrentProject(newProjectName);
                    }
                    else
                    {
                        CatrobatContext.GetContext().UpdateLocalProjects();
                    }
                });
        }

        public void CancelImport(bool setActive)
        {
            _tempProjectHeader = null;
            _project = null;

            using (var storage = StorageSystem.GetStorage())
            {
                storage.DeleteDirectory(CatrobatContext.TempProjectImportPath);
                storage.DeleteDirectory(CatrobatContext.TempProjectImportZipPath);
            }
        }
    }
}