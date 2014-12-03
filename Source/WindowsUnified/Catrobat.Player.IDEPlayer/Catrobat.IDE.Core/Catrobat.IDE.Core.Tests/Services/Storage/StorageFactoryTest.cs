using Catrobat.IDE.Core.Services.Storage;

namespace Catrobat.IDE.Core.Tests.Services.Storage
{
  public class StorageFactoryTest : IStorageFactory
  {
    public IStorage CreateStorage(StorageLocation storageLocation = StorageLocation.Temp)
    {
      return new StorageTest();
    }
  }
}
