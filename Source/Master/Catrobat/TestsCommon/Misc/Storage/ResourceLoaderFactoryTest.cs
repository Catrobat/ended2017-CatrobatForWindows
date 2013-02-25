using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.Core.Storage;

namespace Catrobat.TestsCommon.Misc.Storage
{
  public class ResourceLoaderFactoryTest : IResourceLoaderFactory
  {
    public IResources CreateResoucreLoader()
    {
      return new ResourcesTest();
    }
  }
}
