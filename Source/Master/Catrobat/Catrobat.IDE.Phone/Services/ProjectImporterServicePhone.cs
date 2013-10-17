using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Phone.Storage.SharedAccess;
using Windows.Storage;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Phone.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Phone.Services
{
    public class ProjectImporterServicePhone : IProjectImporterService
    {
        public async Task<ProjectDummyHeader> ImportProject(object systemSpeciticObject)
        {
            try
            {
                var fileToken = (string)systemSpeciticObject;

                using (var storage = StorageSystem.GetStorage())
                {
                    storage.DeleteDirectory(CatrobatContextBase.TempProjectImportPath);
                    storage.DeleteDirectory(CatrobatContextBase.TempProjectImportZipPath);
                }

                const string tempProjectZipName = "temp_project.catrobat";

                var tempSplitList = CatrobatContextBase.TempProjectImportPath.Split('/');
                var tempZipSplitList = CatrobatContextBase.TempProjectImportZipPath.Split('/');

                var localFolder = ApplicationData.Current.LocalFolder;
                var tempFolder =
                    await localFolder.CreateFolderAsync(tempSplitList[0], CreationCollisionOption.OpenIfExists);
                var projectTempZipFolder =
                    await tempFolder.CreateFolderAsync(tempZipSplitList[1], CreationCollisionOption.OpenIfExists);

                await SharedStorageAccessManager.CopySharedFileAsync(projectTempZipFolder, tempProjectZipName,
                        NameCollisionOption.ReplaceExisting, fileToken);

                var projectZipFile = await projectTempZipFolder.GetFileAsync(tempProjectZipName);
                var projectZipStream = await projectZipFile.OpenStreamForReadAsync();

                var newProjectDummyHeader = await ServiceLocator.ProjectImporterService.ImportProject(projectZipStream);

                projectZipStream.Close();

                return newProjectDummyHeader;
            }
            catch
            {
                return null;
            }
        }

        public async Task<string> AcceptTempProject(bool setActive)
        {
            var newProjectName = await ServiceLocator.ProjectImporterService.AcceptTempProject(setActive);

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                if (setActive)
                {
                    var newProject = Catrobat.IDE.Core.CatrobatContext.LoadNewProjectByNameStatic(newProjectName);

                    var projectChangedMessage = new GenericMessage<Project>(newProject);
                    Messenger.Default.Send(projectChangedMessage, ViewModelMessagingToken.CurrentProjectChangedListener);
                }
                else
                {
                    var localProjectsChangedMessage = new MessageBase();
                    Messenger.Default.Send(localProjectsChangedMessage, ViewModelMessagingToken.LocalProjectsChangedListener);
                }
            });

            return newProjectName;
        }

        public void CancelImport()
        {
            ServiceLocator.ProjectImporterService.CancelImport();
        }
    }
}
