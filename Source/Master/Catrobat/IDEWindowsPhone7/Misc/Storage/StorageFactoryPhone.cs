using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catrobat.Core.Storage;

namespace Catrobat.IDEWindowsPhone.Misc.Storage
{
  public class StorageFactoryPhone : IStorageFactory
  {
    public IStorage CreateStorage()
    {
      return new StoragePhone();
    }
  }
}
