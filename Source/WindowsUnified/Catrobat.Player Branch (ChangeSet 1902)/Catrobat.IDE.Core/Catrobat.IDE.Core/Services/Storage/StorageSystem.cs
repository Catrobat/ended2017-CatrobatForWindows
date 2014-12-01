namespace Catrobat.IDE.Core.Services.Storage
{
    public class StorageSystem
    {
        public static IStorage GetStorage(StorageLocation storageLocation = StorageLocation.Local)
        {
            return ServiceLocator.StorageFactory.CreateStorage(storageLocation);
        }
    }
}