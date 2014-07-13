using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class ProjectImporterServiceWindowsShared : IProjectImporterService
    {
        private readonly ProjectImporterService _importer = new ProjectImporterService();

        public async Task<ProjectDummyHeader> ImportProject(object systemSpeciticObject)
        {
            //try
            //{
            //    var fileToken = (string)systemSpeciticObject;

            //    using (var storage = StorageSystem.GetStorage())
            //    {
            //        storage.DeleteDirectory(CatrobatContextBase.TempProjectImportPath);
            //        storage.DeleteDirectory(CatrobatContextBase.TempProjectImportZipPath);
            //    }

            //    const string tempProjectZipName = "temp_project.catrobat";

            //    var tempSplitList = CatrobatContextBase.TempProjectImportPath.Split('/');
            //    var tempZipSplitList = CatrobatContextBase.TempProjectImportZipPath.Split('/');

            //    var localFolder = ApplicationData.Current.LocalFolder;
            //    var tempFolder =
            //        await localFolder.CreateFolderAsync(tempSplitList[0], CreationCollisionOption.OpenIfExists);
            //    var projectTempZipFolder =
            //        await tempFolder.CreateFolderAsync(tempZipSplitList[1], CreationCollisionOption.OpenIfExists);

            //    await SharedStorageAccessManager.CopySharedFileAsync(projectTempZipFolder, tempProjectZipName,
            //            NameCollisionOption.ReplaceExisting, fileToken);

            //    var projectZipFile = await projectTempZipFolder.GetFileAsync(tempProjectZipName);
            //    var projectZipStream = await projectZipFile.OpenStreamForReadAsync();

            //    var newProjectDummyHeader = await _importer.ImportProject(projectZipStream);

            //    projectZipStream.Close();

            //    return newProjectDummyHeader;
            //}
            //catch
            //{
            //    return null;
            //}

            return new ProjectDummyHeader();
        }

        public async Task<string> AcceptTempProject()
        {
            var newProjectName = await _importer.AcceptTempProject();
            return newProjectName;
        }

        public async Task CancelImport()
        {
            await _importer.CancelImport();
        }
    }
}
