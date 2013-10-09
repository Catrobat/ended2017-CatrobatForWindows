using Catrobat.Core.Services;

namespace Catrobat.Core.Utilities.Storage
{
    public class StorageSystem
    {
        public static IStorage GetStorage()
        {
            return ServiceLocator.StorageFactory.CreateStorage();
        }
    }
}