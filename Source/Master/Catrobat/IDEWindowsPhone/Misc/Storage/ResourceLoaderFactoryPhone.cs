using Catrobat.Core.Storage;

namespace Catrobat.IDEWindowsPhone.Misc.Storage
{
  public class ResourceLoaderFactoryPhone : IResourceLoaderFactory
  {
    public IResources CreateResoucreLoader()
    {
      return new ResourcesPhone();
    }
  }
}
