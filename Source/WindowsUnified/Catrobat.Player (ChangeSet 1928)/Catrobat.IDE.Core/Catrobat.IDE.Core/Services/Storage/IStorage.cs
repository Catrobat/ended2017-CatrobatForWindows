using System;
using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.Services.Storage
{
    public enum StorageFileMode
    {
        Append,
        Create,
        CreateNew,
        Open,
        OpenOrCreate,
        Truncate
    }

    public enum StorageFileAccess
    {
        Read,
        ReadWrite,
        Write
    }

    public enum ImageFormat
    {
        Png,
        Jpg
    }

    public interface IStorage : IDisposable
    {
        //#region Synchron

        //// File manipulation

        //string[] GetFileNames(string path);

        //bool FileExists(string path);

        //Stream OpenFile(string path, StorageFileMode mode, StorageFileAccess access);

        //void DeleteFile(string path);

        //void MoveFile(string sourcePath, string destinationPath);

        //void CopyFile(string sourcePath, string destinationPath);


        //// Directory manipulation
        //string[] GetDirectoryNames(string path);

        //bool DirectoryExists(string path);

        //void CreateDirectory(string path);

        //void DeleteDirectory(string path);

        //void MoveDirectory(string sourcePath, string destinationPath);

        //void CopyDirectory(string sourcePath, string destinationPath);

        //void RenameDirectory(string directoryPath, string newDirectoryName);



        //// Specialized reading and writing
        //object ReadSerializableObject(string path, Type type);

        //void WriteSerializableObject(string path, object serializableObject);

        //string ReadTextFile(string path);

        //void WriteTextFile(string path, string content);



        //// Image reading and writing
        //PortableImage LoadImage(string pathToImage);

        //void SaveImage(string path, PortableImage image, bool deleteExisting, ImageFormat format);

        //void DeleteImage(string pathToImage);

        //PortableImage LoadImageThumbnail(string pathToImage);

        //void TryCreateThumbnail(string file);

        //#endregion

        #region Async

        #region File manipulation

        Task<string[]> GetFileNamesAsync(string path);

        Task<bool> FileExistsAsync(string path);

        Task<Stream> OpenFileAsync(string path, StorageFileMode mode, StorageFileAccess access);

        Task DeleteFileAsync(string path);

        Task MoveFileAsync(string sourcePath, string destinationPath);

        Task CopyFileAsync(string sourcePath, string destinationPath);

        #endregion

        #region Directory manipulation

        Task<string[]> GetDirectoryNamesAsync(string path);

        Task<bool> DirectoryExistsAsync(string path);

        Task CreateDirectoryAsync(string path);

        Task DeleteDirectoryAsync(string path);

        Task MoveDirectoryAsync(string sourcePath, string destinationPath);
        
        Task CopyDirectoryAsync(string sourcePath, string destinationPath);

        Task RenameDirectoryAsync(string directoryPath, string newDirectoryName);

        #endregion

        #region Specialized reading and writing
        Task<object> ReadSerializableObjectAsync(string path, Type type);

        Task WriteSerializableObjectAsync(string path, object serializableObject);

        Task<string> ReadTextFileAsync(string path);

        Task WriteTextFileAsync(string path, string content);

        #endregion

        #region Image reading and writing
        Task<PortableImage> LoadImageAsync(string pathToImage);

        Task SaveImageAsync(string path, PortableImage image, bool deleteExisting, ImageFormat format);

        Task DeleteImageAsync(string pathToImage);

        Task<PortableImage> LoadImageThumbnailAsync(string pathToImage);

        Task TryCreateThumbnailAsync(string file);

        #endregion

        #endregion

        string BasePath { get; }
    }
}