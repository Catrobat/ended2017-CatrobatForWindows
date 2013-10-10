using Catrobat.Core.Services.Storage;

namespace Catrobat.IDEWindowsPhone.Services.Storage
{
    public class ResourceLoaderFactoryPhone : IResourceLoaderFactory
    {
        public IResourceLoader CreateResourceLoader()
        {
            return new ResourcesPhone();
        }
    }
}