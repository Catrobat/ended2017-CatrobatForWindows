using Catrobat.Core.Services.Storage;

namespace Catrobat.IDE.Tests.Misc.Storage
{
  public class StorageFactoryTest : IStorageFactory
  {
    public IStorage CreateStorage()
    {
      return new StorageTest();
    }
  }
}
