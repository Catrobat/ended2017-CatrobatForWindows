using System.IO;
using System.Reflection;
using Catrobat.Core.Storage;
using System;

namespace Catrobat.TestsCommon.Misc.Storage
{
  public class StorageTest : IStorage
  {
    private string basepath = "";
    public long Quota
    {
      get { throw new NotImplementedException(); }
    }

    public bool DirectoryExists(string path)
    {
      if (Directory.Exists(basepath + path))
        return true;
      else
        return false;
    }

    public bool FileExists(string path)
    {
      if (File.Exists(basepath + path))
        return true;
      else
        return false;
    }

    public string[] GetDirectoryNames(string path)
    {
      return Directory.GetDirectories(basepath + path);
    }

    public string[] GetFileNames(string path)
    {
      return Directory.GetFiles(basepath + path);
    }

    public void DeleteDirectory(string path)
    {
      if (Directory.Exists(path))
        Directory.Delete(basepath + path);
    }

    public void DeleteFile(string path)
    {
      if (File.Exists(path))
        File.Delete(basepath + path);
    }

    private string getName(string path)
    {
      int start = path.LastIndexOf("/") + 1;
      return path.Substring(start);
    }

    public void CopyDirectory(string sourcePath, string destinationPath)
    {
      if (Directory.Exists(sourcePath))
      {
        Directory.CreateDirectory(basepath + destinationPath);

        foreach(string directory in GetDirectoryNames(basepath + sourcePath))
        {
          string directoryName = getName(directory);
          CopyDirectory(sourcePath + "/" + directoryName, destinationPath + "/" + directoryName);
        }

        foreach (string file in GetFileNames(basepath + sourcePath))
        {
          string fileName = getName(file);
          CopyFile(sourcePath + "/" + fileName, destinationPath + "/" + fileName);
        }
      }
    }

    public void CopyFile(string sourcePath, string destinationPath)
    {
      if (File.Exists(basepath + sourcePath))
      {
        string content = File.ReadAllText(basepath + sourcePath, System.Text.Encoding.UTF8);
        File.WriteAllText(basepath + destinationPath, content, System.Text.Encoding.UTF8);
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
            Directory.CreateDirectory(basepath + path);
            break;
        }

      return File.Open(basepath + path, fileMode, fileAccess);
    }

    public void RenameDirectory(string directoryPath, string newDirectoryName)
    {
      if (Directory.Exists(basepath + directoryPath))
      {
        Directory.Move(basepath + directoryPath, basepath + newDirectoryName);
        Directory.Delete(basepath + directoryPath);
      }
    }

    public byte[] LoadImage(string pathToImage)
    {
      if(File.Exists(basepath + pathToImage))
        return File.ReadAllBytes(basepath + pathToImage);
      else
        return null;
    }

    public bool IncreaseQuotaTo(long quota)
    {
      throw new NotImplementedException();
    }

    public string ReadTextFile(string path)
    {
      if (File.Exists(basepath + path))
        return File.ReadAllText(basepath + path, System.Text.Encoding.UTF8);
      else
        return null;
    }

    public void WriteTextFile(string path, string content)
    {
      File.WriteAllText(basepath + path, content, System.Text.Encoding.UTF8);
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
      get { return Assembly.GetExecutingAssembly().CodeBase; }
    }
  }
}
