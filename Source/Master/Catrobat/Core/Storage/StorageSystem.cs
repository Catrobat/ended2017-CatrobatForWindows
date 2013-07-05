namespace Catrobat.Core.Storage
{
    //public enum StorageType
    //{
    //  Phone7,
    //  Windows8
    //}

    public class StorageSystem
    {
        private static IStorageFactory _storageFactory;

        public static void SetStorageFactory(IStorageFactory storage)
        {
            _storageFactory = storage;
        }

        public static IStorage GetStorage()
        {
            return _storageFactory.CreateStorage();
        }
    }
}