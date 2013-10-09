using Catrobat.Core.Utilities.Storage;

namespace Catrobat.IDEWindowsPhone.Utilities.Storage
{
    public class ResourceLoaderFactoryPhone : IResourceLoaderFactory
    {
        public IResourceLoader CreateResourceLoader()
        {
            return new ResourcesPhone();
        }
    }
}