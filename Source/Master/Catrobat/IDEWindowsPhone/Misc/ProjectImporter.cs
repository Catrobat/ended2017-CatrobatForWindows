using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Catrobat.Core;
using Catrobat.Core.ExtensionMethods;
using Catrobat.Core.Objects;
using Catrobat.Core.Storage;
using Catrobat.Core.VersionConverter;
using Catrobat.Core.ZIP;
using Windows.Phone.Storage.SharedAccess;
using Windows.Storage;
using Catrobat.IDEWindowsPhone.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDEWindowsPhone.Misc
{
    public class ProjectImporter
    {
        private ProjectDummyHeader _tempProjectHeader;
        private Project _project;

        public CatrobatContextBase CatrobatContext { get; set; } 

        public async Task<ProjectDummyHeader> ImportProjects(String fileToken)
        {
            try
            {
                using (var storage = StorageSystem.GetStorage())
                {
                    storage.DeleteDirectory(CatrobatContextBase.TempProjectImportPath);
                    storage.DeleteDirectory(CatrobatContextBase.TempProjectImportZipPath);
                }

                const string tempProjectZipName = "temp_project.catrobat";

                var tempSplitList = CatrobatContextBase.TempProjectImportPath.Split('/');
                var tempZipSplitList = CatrobatContextBase.TempProjectImportZipPath.Split('/');

                var localFolder = ApplicationData.Current.LocalFolder;
                var tempFolder = await localFolder.CreateFolderAsync(tempSplitList[0], CreationCollisionOption.OpenIfExists);
                var projectTempFolder = await tempFolder.CreateFolderAsync(tempSplitList[1], CreationCollisionOption.OpenIfExists);
                var projectTempZipFolder = await tempFolder.CreateFolderAsync(tempZipSplitList[1], CreationCollisionOption.OpenIfExists);

                await SharedStorageAccessManager.CopySharedFileAsync(projectTempZipFolder, tempProjectZipName, NameCollisionOption.ReplaceExisting, fileToken);

                var projectZipFile = await projectTempZipFolder.GetFileAsync(tempProjectZipName);
                var projectZipStream = await projectZipFile.OpenStreamForReadAsync();

                CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(projectZipStream, CatrobatContextBase.TempProjectImportPath);

                object projectScreenshot = null;
                string projectCode = "";

                using (var storage = StorageSystem.GetStorage())
                {
                    projectScreenshot = storage.LoadImage(Path.Combine(CatrobatContextBase.TempProjectImportPath, Project.ScreenshotPath)) ??
                                        storage.LoadImage(Path.Combine(CatrobatContextBase.TempProjectImportPath, Project.AutomaticScreenshotPath));
                }

                CatrobatVersionConverter.VersionConverterError error;
                var projectCodePath = Path.Combine(CatrobatContextBase.TempProjectImportPath, Project.ProjectCodePath);
                projectCode = CatrobatVersionConverter.ConvertToInternalXmlVersion(projectCodePath, out error);
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

        public void AcceptTempProject(bool setActive)
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

            Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    if (setActive)
                    {
                        var newProject = Core.CatrobatContext.LoadNewProjectByNameStatic(newProjectName);

                        var projectChangedMessage = new GenericMessage<Project>(newProject);
                        Messenger.Default.Send(projectChangedMessage, ViewModelMessagingToken.CurrentProjectChangedListener);
                    }
                    else
                    {
                        var localProjectsChangedMessage = new MessageBase();
                        Messenger.Default.Send(localProjectsChangedMessage, ViewModelMessagingToken.LocalProjectsChangedListener);
                    }
                });
        }

        public void CancelImport(bool setActive)
        {
            _tempProjectHeader = null;
            _project = null;

            using (var storage = StorageSystem.GetStorage())
            {
                storage.DeleteDirectory(CatrobatContextBase.TempProjectImportPath);
                storage.DeleteDirectory(CatrobatContextBase.TempProjectImportZipPath);
            }
        }
    }
}