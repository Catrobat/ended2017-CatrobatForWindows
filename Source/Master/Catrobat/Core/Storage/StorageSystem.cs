namespace Catrobat.Core.Storage
{
    public enum StorageType
    {
        Phone7,
        Windows8
    }

    public class StorageSystem
    {
        private static IStorageFactory storageFactory;

        public static void SetStorageFactory(IStorageFactory storage)
        {
            storageFactory = storage;
        }

        public static IStorage GetStorage()
        {
            return storageFactory.CreateStorage();
        }
    }
}