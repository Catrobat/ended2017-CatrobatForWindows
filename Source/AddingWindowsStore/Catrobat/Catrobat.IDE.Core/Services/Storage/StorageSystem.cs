namespace Catrobat.IDE.Core.Services.Storage
{
    public class StorageSystem
    {
        public static IStorage GetStorage()
        {
            return ServiceLocator.StorageFactory.CreateStorage();
        }
    }
}