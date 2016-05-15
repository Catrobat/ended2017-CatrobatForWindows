using Catrobat.IDE.Core.Services.Storage;

namespace Catrobat.IDE.WindowsShared.Services.Storage
{
    public class ResourceLoaderWindowsShared : IResourceLoaderFactory
    {
        public IResourceLoader CreateResourceLoader()
        {
            return new ResourcesWindowsShared();
        }
    }
}