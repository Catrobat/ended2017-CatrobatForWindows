using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catrobat.Core.Storage;

namespace Catrobat.IDEWindowsPhone7.Misc.Storage
{
  class StorageFactoryPhone7 : IStorageFactory
  {
    public IStorage CreateStorage()
    {
      return new StoragePhone7();
    }
  }
}
