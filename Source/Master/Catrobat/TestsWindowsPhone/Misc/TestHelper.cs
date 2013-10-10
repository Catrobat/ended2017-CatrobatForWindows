using System.IO.IsolatedStorage;
using System.Linq.Expressions;
using Catrobat.Core;
using Catrobat.Core.Services.Common;
using Catrobat.Core.Services;
using Catrobat.IDEWindowsPhone.Services;
using Catrobat.IDEWindowsPhone.Services.Storage;
using Catrobat.IDEWindowsPhone.Utilities.Sounds;
using Catrobat.IDEWindowsPhone.Utilities.Storage;

namespace Catrobat.TestsWindowsPhone.Misc
{
  public static class TestHelper
  {
    public static void DeleteFolder(this IsolatedStorageFile iso, string path)
    {
      if (!iso.DirectoryExists(path))
        return;
      var folders = iso.GetDirectoryNames(path + "/" + "*.*");

      foreach (var folder in folders)
      {
        string folderName = path + "/" + folder;
        DeleteFolder(iso, folderName);
      }

      foreach (var file in iso.GetFileNames(path + "/" + "*.*"))
      {
        iso.DeleteFile(path + "/" + file);
      }

      if (path != "")
        iso.DeleteDirectory(path);
    }

    internal static void InitializeTests()
    {
        Core.Services.ServiceLocator.SetServices(
                        new NavigationServicePhone(),
                        new SystemInformationServicePhone(),
                        new CultureServicePhone(),
                        new ImageResizeServicePhone(),
                        new PlayerLauncherServicePhone(),
                        new ResourceLoaderFactoryPhone(),
                        new StorageFactoryPhone(),
                        new ServerCommunicationServicePhone(),
                        new ImageSourceConversionServicePhone(),
                        new ProjectImporterService(),
                        new SoundPlayerServicePhone(),
                        new SoundRecorderServicePhone()
                        );
    }
  }
}
