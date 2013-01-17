using System.IO.IsolatedStorage;
using Catrobat.Core;
using Catrobat.Core.Storage;
using Catrobat.IDEWindowsPhone7.Misc.Storage;

namespace TestsWindowsPhone7
{
  public static class TestHelper
  {
    public static void InitializeAndClearCatrobatContext()
    {
      StorageSystem.SetStorageFactory(new StorageFactoryPhone7());

      using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
      {
        isolatedStorage.DeleteFolder("");
      }

      CatrobatContext.Instance.CurrentProject = null;
    }

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
  }
}
