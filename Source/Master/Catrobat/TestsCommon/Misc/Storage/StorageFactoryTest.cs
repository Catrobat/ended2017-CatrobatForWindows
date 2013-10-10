using Catrobat.Core.Services.Storage;

namespace Catrobat.TestsCommon.Misc.Storage
{
  public class StorageFactoryTest : IStorageFactory
  {
    public IStorage CreateStorage()
    {
      return new StorageTest();
    }
  }
}
