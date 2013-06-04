using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Storage;
using Catrobat.Core.ZIP;
using Windows.Storage;

namespace Catrobat.IDEWindowsPhone.Misc
{
  public class PlayerLauncher
  {
    public async static Task<bool> LaunchPlayer(String projectName)
    {
      try
      {
        var projectFolder = CatrobatContext.ProjectsPath + "/" + projectName;

        using (IStorage storage = StorageSystem.GetStorage())
        {
          if(!storage.DirectoryExists(CatrobatContext.ProjectsPath))
            Debugger.Break();

          var stream = storage.OpenFile(CatrobatContext.PlayerActiveProjectZipPath, StorageFileMode.Create, StorageFileAccess.Write);
          CatrobatZip.ZipCatrobatPackage(stream, projectFolder);
        }

        StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        var folder = await localFolder.GetFolderAsync(CatrobatContext.PlayerActiveProjectZipPath.Split('/')[0]);

        StorageFile catrobatZipFile = await folder.GetFileAsync(CatrobatContext.PlayerActiveProjectZipPath.Split('/')[1]);
        await Windows.System.Launcher.LaunchFileAsync(catrobatZipFile);

        return true;
      }
      catch (Exception)
      {
        return false;
      }

    }
  }
}
