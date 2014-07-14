namespace Catrobat.IDE.Core.Services.Storage
{
    public enum ResourceScope
    {
        Core,
        Ide,
        TestsPhone,
        TestsStore,
        TestCommon,
        Resources
    }

    public interface IResourceLoaderFactory
    {
        IResourceLoader CreateResourceLoader();
    }
}