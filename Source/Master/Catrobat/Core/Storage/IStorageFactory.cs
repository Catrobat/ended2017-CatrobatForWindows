namespace Catrobat.Core.Storage
{
    public interface IStorageFactory
    {
        IStorage CreateStorage();
    }
}