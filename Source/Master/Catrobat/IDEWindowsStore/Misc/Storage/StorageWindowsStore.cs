using System.Collections.Generic;
using System.IO;
using Catrobat.Core.Storage;
using System;

namespace Catrobat.IDEWindowsStore.Misc.Storage
{
  public class StorageWindowsStore : IStorage
  {
    private readonly List<Stream> _openedStreams = new List<Stream>();

    public long Quota
    {
      get { throw new NotImplementedException(); }
    }

    public bool DirectoryExists(string path)
    {
      throw new NotImplementedException();
    }

    public bool FileExists(string path)
    {
      throw new NotImplementedException();
    }

    public string[] GetDirectoryNames(string path)
    {
      throw new NotImplementedException();
    }

    public string[] GetFileNames(string path)
    {
      throw new NotImplementedException();
    }

    public void DeleteDirectory(string path)
    {
      throw new NotImplementedException();
    }

    public void DeleteFile(string path)
    {
      throw new NotImplementedException();
    }

    public void CopyDirectory(string sourcePath, string destinationPath)
    {
      throw new NotImplementedException();
    }

    public void CopyFile(string sourcePath, string destinationPath)
    {
      throw new NotImplementedException();
    }

    public Stream OpenFile(string path, StorageFileMode mode, StorageFileAccess access)
    {
      throw new NotImplementedException();
    }

    public void RenameDirectory(string directoryPath, string newDirectoryName)
    {
      throw new NotImplementedException();
    }

    public object LoadImage(string pathToImage)
    {
      throw new NotImplementedException();
    }

    public bool IncreaseQuotaTo(long quota)
    {
      throw new NotImplementedException();
    }

    public string ReadTextFile(string path)
    {
      throw new NotImplementedException();
    }

    public void WriteTextFile(string path, string content)
    {
      throw new NotImplementedException();
    }

    public object ReadSerializableObject(string path, Type type)
    {
      throw new NotImplementedException();
    }

    public void WriteSerializableObject(string path, object serializableObject)
    {
      throw new NotImplementedException();
    }

    public void Dispose()
    {
      foreach (var stream in _openedStreams)
        stream.Dispose();

      throw new NotImplementedException();
    }


    public string BasePath
    {
      get { return ""; }
    }


    public void SaveImage(string path, object image)
    {
      throw new NotImplementedException();
    }


    public object LoadImageThumbnail(string pathToImage)
    {
      throw new NotImplementedException();
    }


    public void MoveDirectory(string sourcePath, string destinationPath)
    {
        throw new NotImplementedException();
    }

    public void MoveFile(string sourcePath, string destinationPath)
    {
        throw new NotImplementedException();
    }
  }
}
