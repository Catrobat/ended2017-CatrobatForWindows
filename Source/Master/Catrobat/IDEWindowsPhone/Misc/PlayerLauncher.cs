using System;
using System.IO;
using System.Threading.Tasks;
using Catrobat.Core;
using Catrobat.Core.Storage;
using Catrobat.Core.ZIP;
using Windows.Storage;
using Windows.System;

namespace Catrobat.IDEWindowsPhone.Misc
{
    public class PlayerLauncher
    {
        public static async Task<bool> LaunchPlayer(String projectName)
        {
            try
            {
                var projectFolder = Path.Combine(CatrobatContext.ProjectsPath, projectName);

                using (var storage = StorageSystem.GetStorage())
                {
                    var stream = storage.OpenFile(CatrobatContext.PlayerActiveProjectZipPath, StorageFileMode.Create, StorageFileAccess.Write);
                    CatrobatZip.ZipCatrobatPackage(stream, projectFolder);
                }

                var localFolder = ApplicationData.Current.LocalFolder;
                var splitList = CatrobatContext.PlayerActiveProjectZipPath.Split('/');

                if (splitList.Length > 1)
                {
                    var folder = await localFolder.GetFolderAsync(splitList[0]);
                    var catrobatZipFile = await folder.GetFileAsync(splitList[1]);
                    await Launcher.LaunchFileAsync(catrobatZipFile);
                    await Launcher.LaunchFileAsync(catrobatZipFile); //TODO: one call too much?
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}