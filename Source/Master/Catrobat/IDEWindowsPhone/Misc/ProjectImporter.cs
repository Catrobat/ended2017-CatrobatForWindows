using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Storage;
using Catrobat.Core.ZIP;
using Windows.Phone.Storage.SharedAccess;
using Windows.Storage;

namespace Catrobat.IDEWindowsPhone.Misc
{
    public class ProjectImporter
    {
        private ProjectHeader _tempProjectHeader;
        private Project _project;

        public async Task<ProjectHeader> ImportProjects(String fileToken)
        {
            try
            {
                using (var storage = StorageSystem.GetStorage())
                {
                    storage.DeleteDirectory(CatrobatContext.TempProjectImportPath);
                    storage.DeleteDirectory(CatrobatContext.TempProjectImportZipPath);
                }

                const string tempProjectZipName = "temp_project.catrobat";

                var localFolder = ApplicationData.Current.LocalFolder;
                var tempFolder =
                    await localFolder.CreateFolderAsync(CatrobatContext.TempProjectImportPath.Split('/')[0],
                                                        CreationCollisionOption.OpenIfExists);
                var projectTempFolder =
                    await tempFolder.CreateFolderAsync(CatrobatContext.TempProjectImportPath.Split('/')[1],
                                                       CreationCollisionOption.OpenIfExists);
                var projectTempZipFolder =
                    await tempFolder.CreateFolderAsync(CatrobatContext.TempProjectImportZipPath.Split('/')[1],
                                                       CreationCollisionOption.OpenIfExists);

                await SharedStorageAccessManager.CopySharedFileAsync(projectTempZipFolder, tempProjectZipName,
                                                                     NameCollisionOption.ReplaceExisting, fileToken);

                var projectZipFile = await projectTempZipFolder.GetFileAsync(tempProjectZipName);
                var projectZipStream = await projectZipFile.OpenStreamForReadAsync();

                CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(projectZipStream,
                                                                    CatrobatContext.TempProjectImportPath);

                object projectScreenshot = null;
                string projectCode = null;

                using (var storage = StorageSystem.GetStorage())
                {
                    projectScreenshot =
                        storage.LoadImage(CatrobatContext.TempProjectImportPath + "/" + Project.ScreenshotPath);
                    projectCode =
                        storage.ReadTextFile(CatrobatContext.TempProjectImportPath + "/" + Project.ProjectCodePath);
                }

                _project = new Project(projectCode);

                _tempProjectHeader = new ProjectHeader
                {
                    Screenshot = projectScreenshot,
                    ProjectName = _project.ProjectName
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

                return null;
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
                var projectPath = "";
                var counter = 0;
                while (true)
                {
                    projectPath = CatrobatContext.ProjectsPath + "/" + _tempProjectHeader.ProjectName;

                    if (counter != 0)
                    {
                        projectPath = CatrobatContext.ProjectsPath + "/" + _tempProjectHeader.ProjectName + counter;
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
                    _project.SetProjectName(newProjectName);

                    _project.Save(CatrobatContext.TempProjectImportPath + "/" + Project.ProjectCodePath);
                }
            }

            using (var storage = StorageSystem.GetStorage())
            {
                storage.CopyDirectory(CatrobatContext.TempProjectImportPath,
                                      CatrobatContext.ProjectsPath + "/" + _project.ProjectName);
            }

            using (var storage = StorageSystem.GetStorage())
            {
                storage.DeleteDirectory(CatrobatContext.TempProjectImportPath);
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