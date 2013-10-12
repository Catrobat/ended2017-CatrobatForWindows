namespace Catrobat.IDE.Core.Services.Storage
{
    public enum ResourceScope
    {
        Core,
        IdePhone,
        IdeStore,
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