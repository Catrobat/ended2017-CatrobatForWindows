using Catrobat.Core.Services;

namespace Catrobat.Core.Misc.Storage
{
    public class StorageSystem
    {
        public static IStorage GetStorage()
        {
            return ServiceLocator.StorageFactory.CreateStorage();
        }
    }
}