using System.IO;
using System.Reflection;
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
      if (Directory.Exists(BasePath + path))
        return true;
      else
        return false;
    }

    public bool FileExists(string path)
    {
      if (File.Exists(BasePath + path))
        return true;
      else
        return false;
    }

    public string[] GetDirectoryNames(string path)
    {
      return Directory.GetDirectories(BasePath + path);
    }

    public string[] GetFileNames(string path)
    {
      return Directory.GetFiles(BasePath + path);
    }

    public void DeleteDirectory(string path)
    {
      if (Directory.Exists(BasePath + path))
        Directory.Delete(BasePath + path);
    }

    public void DeleteFile(string path)
    {
      if (File.Exists(BasePath + path))
        File.Delete(BasePath + path);
    }

    private string getName(string path)
    {
      int startSlash = path.LastIndexOf("/", System.StringComparison.Ordinal) + 1;
      int startBackSlash = path.LastIndexOf("\\", System.StringComparison.Ordinal) + 1;

      if (startSlash > startBackSlash)
        return path.Substring(startSlash);
      else
        return path.Substring(startBackSlash);
    }

    public void CopyDirectory(string sourcePath, string destinationPath)
    {
      if (Directory.Exists(BasePath + sourcePath))
      {
        Directory.CreateDirectory(BasePath + destinationPath);

        foreach (string directory in Directory.GetDirectories(BasePath + sourcePath))
        {
          string directoryName = getName(directory);
          CopyDirectory(sourcePath + "/" + directoryName, destinationPath + "/" + directoryName);
        }

        foreach (string file in Directory.GetFiles(BasePath + sourcePath))
        {
          string fileName = getName(file);
          CopyFile(sourcePath + "/" + fileName, destinationPath + "/" + fileName);
        }
      }
    }

    public void CopyFile(string sourcePath, string destinationPath)
    {
      if (File.Exists(BasePath + sourcePath))
      {
        string content = File.ReadAllText(BasePath + sourcePath, System.Text.Encoding.UTF8);
        File.WriteAllText(BasePath + destinationPath, content, System.Text.Encoding.UTF8);
      }
    }

    public Stream OpenFile(string path, StorageFileMode mode, StorageFileAccess access)
    {
      FileMode fileMode = FileMode.Append;
      FileAccess fileAccess = FileAccess.Read;

      switch (mode)
      {
        case StorageFileMode.Append:
          fileMode = FileMode.Append;
          break;

        case StorageFileMode.Create:
          fileMode = FileMode.Create;
          break;

        case StorageFileMode.CreateNew:
          fileMode = FileMode.CreateNew;
          break;

        case StorageFileMode.Open:
          fileMode = FileMode.Open;
          break;

        case StorageFileMode.OpenOrCreate:
          fileMode = FileMode.OpenOrCreate;
          break;

        case StorageFileMode.Truncate:
          fileMode = FileMode.Truncate;
          break;
      }

      switch (access)
      {
        case StorageFileAccess.Read:
          fileAccess = FileAccess.Read;
          break;

        case StorageFileAccess.ReadWrite:
          fileAccess = FileAccess.ReadWrite;
          break;

        case StorageFileAccess.Write:
          fileAccess = FileAccess.Write;
          break;
      }

      if (access == StorageFileAccess.Write || access == StorageFileAccess.ReadWrite)
        switch (mode)
        {
          case StorageFileMode.Create:
          case StorageFileMode.CreateNew:
          case StorageFileMode.OpenOrCreate:
            Directory.CreateDirectory(BasePath + path);
            break;
        }

      return File.Open(BasePath + path, fileMode, fileAccess);
    }

    public void RenameDirectory(string directoryPath, string newDirectoryName)
    {
      if (Directory.Exists(BasePath + directoryPath))
      {
        Directory.Move(BasePath + directoryPath, BasePath + newDirectoryName);
        Directory.Delete(BasePath + directoryPath);
      }
    }

    public byte[] LoadImage(string pathToImage)
    {
      if (File.Exists(BasePath + pathToImage))
        return File.ReadAllBytes(BasePath + pathToImage);
      else
        return null;
    }

    public bool IncreaseQuotaTo(long quota)
    {
      throw new NotImplementedException();
    }

    public string ReadTextFile(string path)
    {
      if (File.Exists(BasePath + path))
        return File.ReadAllText(BasePath + path, System.Text.Encoding.UTF8);
      else
        return null;
    }

    public void WriteTextFile(string path, string content)
    {
      File.WriteAllText(BasePath + path, content, System.Text.Encoding.UTF8);
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
      
    }

    public string BasePath
    {
      get
      {
        string basePath = Assembly.GetExecutingAssembly().CodeBase;
        int end = basePath.IndexOf(("Catrobat/"), System.StringComparison.Ordinal) + 9;
        basePath = basePath.Substring(8, end-8) + "TestStorage/";

        return basePath;
      }
    }
  }
}
