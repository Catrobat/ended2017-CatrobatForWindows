using System.IO.IsolatedStorage;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Tests.Misc.Storage;

namespace Catrobat.IDE.Tests.Misc
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
            ServiceLocator.SetServices(null, new PlatformInformationHelperTests(), null, null,
                null, new ResourceLoaderFactoryTest(), new StorageFactoryTest(), null, null,
                new ProjectImporterService(), null, null, null, null, null, null, null);
        }
    }
}
