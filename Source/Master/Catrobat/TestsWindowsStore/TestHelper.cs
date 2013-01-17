using System;
using Catrobat.Core.Storage;
using Catrobat.Core;
using Catrobat.IDEWindowsStore.Misc.Storage;

namespace TestsWindowsStore
{
  public static class TestHelper
  {
    public static void InitializeAndClearCatrobatContext()
    {
      StorageSystem.SetStorageFactory(new StorageFactoryWindowsStore());

      throw new NotImplementedException("Implement for WindowsStore");
      //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
      //{
      //  isolatedStorage.DeleteFolder("");
      //}

      CatrobatContext.Instance.CurrentProject = null;
    }

    public static void DeleteFolder(string path)
    {
      throw new NotImplementedException("Implement for WindowsStore");

      //if (!iso.DirectoryExists(path))
      //  return;
      //var folders = iso.GetDirectoryNames(path + "/" + "*.*");

      //foreach (var folder in folders)
      //{
      //  string folderName = path + "/" + folder;
      //  DeleteFolder(iso, folderName);
      //}

      //foreach (var file in iso.GetFileNames(path + "/" + "*.*"))
      //{
      //  iso.DeleteFile(path + "/" + file);
      //}

      //if (path != "")
      //  iso.DeleteDirectory(path);
    }
  }
}
