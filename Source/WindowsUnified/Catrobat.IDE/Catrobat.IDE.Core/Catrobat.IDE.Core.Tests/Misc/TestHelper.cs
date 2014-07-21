using System.IO.IsolatedStorage;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Tests.Services.Storage;

namespace Catrobat.IDE.Core.Tests.Misc
{
    public static class TestHelper
    {
        public static void InitializeAndClearCatrobatContext()
        {
            InitializeTests();

            using (var storage = StorageSystem.GetStorage())
            {
                storage.DeleteDirectoryAsync("");
            }
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
            ServiceLocator.UnRegisterAll();
            ServiceLocator.Register<PlatformInformationHelperTests>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ResourceLoaderFactoryTest>(TypeCreationMode.Lazy);
            ServiceLocator.Register<StorageFactoryTest>(TypeCreationMode.Lazy);
        }
    }
}
