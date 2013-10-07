namespace Catrobat.Core.Misc.Storage
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