using System.IO.IsolatedStorage;
using Catrobat.Core;
using Catrobat.Core.Storage;
using Catrobat.TestsCommon.Misc.Storage;

namespace Catrobat.TestsCommon.Misc
{
  public static class TestHelper
  {
    public static void InitializeAndClearCatrobatContext()
    {
      InitializeTests();

      using (var storage = StorageSystem.GetStorage())
      {
        storage.DeleteDirectory("");
      }

      //CatrobatContext.Instance.CurrentProject = null;
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

    internal static void InitializeTests()
    {
      StorageSystem.SetStorageFactory(new StorageFactoryTest());
      ResourceLoader.SetResourceLoader(new ResourcesTest());
    }
  }
}
