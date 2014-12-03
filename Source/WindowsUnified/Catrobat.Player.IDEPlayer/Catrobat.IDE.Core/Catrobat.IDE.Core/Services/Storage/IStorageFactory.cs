namespace Catrobat.IDE.Core.Services.Storage
{
    public enum StorageLocation
    {
        Local,
        Roaming,
        Temp
    }
    public interface IStorageFactory
    {
        IStorage CreateStorage(StorageLocation storageLocation);
    }
}