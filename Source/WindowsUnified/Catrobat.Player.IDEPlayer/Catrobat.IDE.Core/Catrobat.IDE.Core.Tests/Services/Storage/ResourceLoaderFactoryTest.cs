using Catrobat.IDE.Core.Services.Storage;

namespace Catrobat.IDE.Core.Tests.Services.Storage
{
  public class ResourceLoaderFactoryTest : IResourceLoaderFactory
  {
    public IResourceLoader CreateResourceLoader()
    {
      return new ResourcesTest();
    }
  }
}
