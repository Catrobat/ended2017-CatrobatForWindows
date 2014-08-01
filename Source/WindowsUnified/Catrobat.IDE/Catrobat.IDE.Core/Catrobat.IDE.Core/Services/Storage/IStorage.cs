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
        #region Synchron

        void CreateDirectory(string path);

        bool DirectoryExists(string path);

        bool FileExists(string path);

        string[] GetDirectoryNames(string path);

        string[] GetFileNames(string path);

        void DeleteDirectory(string path);

        void DeleteFile(string path);

        void CopyDirectory(string sourcePath, string destinationPath);

        void MoveDirectory(string sourcePath, string destinationPath);

        void CopyFile(string sourcePath, string destinationPath);

        void MoveFile(string sourcePath, string destinationPath);

        Stream OpenFile(string path, StorageFileMode mode, StorageFileAccess access);

        void RenameDirectory(string directoryPath, string newDirectoryName);

        PortableImage LoadImage(string pathToImage);

        PortableImage LoadImageThumbnail(string pathToImage);

        void DeleteImage(string pathToImage);

        void SaveImage(string path, PortableImage image, bool deleteExisting, ImageFormat format);

        string ReadTextFile(string path);

        void WriteTextFile(string path, string content);

        object ReadSerializableObject(string path, Type type);

        void WriteSerializableObject(string path, object serializableObject);

        #endregion

        #region Async

        Task CreateDirectoryAsync(string path);

        Task<bool> DirectoryExistsAsync(string path);

        Task<bool> FileExistsAsync(string path);

        Task<string[]> GetDirectoryNamesAsync(string path);

        Task<string[]> GetFileNamesAsync(string path);

        Task DeleteDirectoryAsync(string path);

        Task DeleteFileAsync(string path);

        Task CopyDirectoryAsync(string sourcePath, string destinationPath);

        Task MoveDirectoryAsync(string sourcePath, string destinationPath);

        Task CopyFileAsync(string sourcePath, string destinationPath);

        Task MoveFileAsync(string sourcePath, string destinationPath);

        Task<Stream> OpenFileAsync(string path, StorageFileMode mode, StorageFileAccess access);

        Task RenameDirectoryAsync(string directoryPath, string newDirectoryName);

        Task<PortableImage> LoadImageAsync(string pathToImage);

        Task<PortableImage> LoadImageThumbnailAsync(string pathToImage);

        Task TryCreateThumbnailAsync(string file);

        Task DeleteImageAsync(string pathToImage);

        Task SaveImageAsync(string path, PortableImage image, bool deleteExisting, ImageFormat format);

        Task<string> ReadTextFileAsync(string path);

        Task WriteTextFileAsync(string path, string content);

        Task<object> ReadSerializableObjectAsync(string path, Type type);

        Task WriteSerializableObjectAsync(string path, object serializableObject);

        #endregion

        string BasePath { get; }
    }
}