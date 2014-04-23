using Catrobat.IDE.Core.Services.Storage;

namespace Catrobat.IDE.Phone.Services.Storage
{
    public class ResourceLoaderFactoryPhone : IResourceLoaderFactory
    {
        public IResourceLoader CreateResourceLoader()
        {
            return new ResourcesPhone();
        }
    }
}