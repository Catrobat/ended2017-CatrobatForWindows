using System;
using System.IO;
using Catrobat.Core.Services.Data;

namespace Catrobat.Core.Services.Storage
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

        PortableImage CreateThumbnail(PortableImage image);

        void SaveImage(string path, PortableImage image, bool deleteExisting, ImageFormat format);

        string ReadTextFile(string path);

        void WriteTextFile(string path, string content);

        object ReadSerializableObject(string path, Type type);

        void WriteSerializableObject(string path, object serializableObject);

        string BasePath { get; }
    }
}