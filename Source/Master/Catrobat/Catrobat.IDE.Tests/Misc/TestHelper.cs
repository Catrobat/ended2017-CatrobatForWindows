using System.IO.IsolatedStorage;
using Catrobat.Core;
using Catrobat.Core.Services.Common;
using Catrobat.Core.Services.Storage;
using Catrobat.Core.Utilities.Helpers;
using Catrobat.Core.Services;
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

            //CatrobatContext.GetContext().CurrentProject = null;
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
                new ProjectImporterService(), null, null);
        }
    }
}
