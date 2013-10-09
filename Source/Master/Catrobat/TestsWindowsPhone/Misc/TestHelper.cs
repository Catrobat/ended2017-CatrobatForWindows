using System.IO.IsolatedStorage;
using Catrobat.Core;
using Catrobat.Core.Misc.Storage;
using Catrobat.Core.Services;
using Catrobat.IDEWindowsPhone.Misc.Storage;
using Catrobat.IDEWindowsPhone.Services;
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
      ServiceLocator.SetServices(new NavigationServicePhone(), 
          new SystemInformationServicePhone(), new CultureServicePhone(), 
          new ImageResizeServicePhone(), new PlayerLauncherServicePhone(), 
          new ResourceLoaderFactoryPhone(), new StorageFactoryPhone(), 
          new ServerCommunicationServicePhone(), new ImageSourceConversionServicePhone());
    }
  }
}
