using Catrobat.Core.Storage;

namespace Catrobat.IDEWindowsPhone.Utilities.Storage
{
    public class ResourceLoaderFactoryPhone : IResourceLoaderFactory
    {
        public IResources CreateResoucreLoader()
        {
            return new ResourcesPhone();
        }
    }
}