using System;
using System.IO;

namespace Catrobat.Core.Storage
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

  public interface IStorage : IDisposable
  {
    long Quota { get; }

    bool DirectoryExists(string path);

    bool FileExists(string path);

    string[] GetDirectoryNames(string path);

    string[] GetFileNames(string path);

    void DeleteDirectory(string path);

    void DeleteFile(string path);

    void CopyDirectory(string sourcePath, string destinationPath);

    void CopyFile(string sourcePath, string destinationPath);

    Stream OpenFile(string path, StorageFileMode mode, StorageFileAccess access);

    void RenameDirectory(string directoryPath, string newDirectoryName);

    byte[] LoadImage(string pathToImage);

    bool IncreaseQuotaTo(long quota);

    string ReadTestFile(string path);

    void WriteTestFile(string path, string content);

    object ReadSerializableObject(string path, Type type);

    void WriteSerializableObject(string path, object serializableObject);

    Stream OpenResourcesStream(string path);
  }
}