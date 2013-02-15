using System.IO;
using Catrobat.Core.Storage;
using System;

namespace Catrobat.TestsCommon.Misc.Storage
{
  public class StorageTest : IStorage
  {
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

    public byte[] LoadImage(string pathToImage)
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
      throw new NotImplementedException();
    }

    public string BasePath
    {
      get { return ""; }
    }
  }
}
