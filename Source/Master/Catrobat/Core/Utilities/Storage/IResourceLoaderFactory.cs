namespace Catrobat.Core.Utilities.Storage
{
    public enum ResourceScope
    {
        Core,
        IdePhone,
        IdeStore,
        TestsPhone,
        TestsStore,
        IdeCommon,
        TestCommon,
        Resources
    }

    public interface IResourceLoaderFactory
    {
        IResourceLoader CreateResourceLoader();
    }
}