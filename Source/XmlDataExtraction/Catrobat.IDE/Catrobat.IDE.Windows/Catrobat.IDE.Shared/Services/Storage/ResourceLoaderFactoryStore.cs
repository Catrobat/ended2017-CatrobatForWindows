using Catrobat.IDE.Core.Services.Storage;

namespace Catrobat.IDE.WindowsShared.Services.Storage
{
    public class ResourceLoaderFactoryStore : IResourceLoaderFactory
    {
        public IResourceLoader CreateResourceLoader()
        {
            return new ResourcesStore();
        }
    }
}