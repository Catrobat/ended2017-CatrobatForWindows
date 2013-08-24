namespace Catrobat.Interpreter.Storage
{
    public class ResourceLoader
    {
        private static IResourceLoaderFactory _resourceFactory;

        public static void SetResourceLoaderFactory(IResourceLoaderFactory resourceFactory)
        {
            _resourceFactory = resourceFactory;
        }

        public static IResources CreateResourceLoader()
        {
            return _resourceFactory.CreateResoucreLoader();
        }
    }
}