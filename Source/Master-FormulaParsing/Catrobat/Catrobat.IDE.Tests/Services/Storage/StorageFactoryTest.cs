using Catrobat.IDE.Core.Services.Storage;

namespace Catrobat.IDE.Tests.Services.Storage
{
  public class StorageFactoryTest : IStorageFactory
  {
    public IStorage CreateStorage()
    {
      return new StorageTest();
    }
  }
}
