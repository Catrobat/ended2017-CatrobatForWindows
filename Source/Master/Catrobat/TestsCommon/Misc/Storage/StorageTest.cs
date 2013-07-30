using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using Catrobat.Core.Storage;
using System;
using System.Runtime.Serialization;

namespace Catrobat.TestsCommon.Misc.Storage
{
  public class StorageTest : IStorage
  {
    private readonly List<Stream> _openedStreams = new List<Stream>();

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
      path = FormatPath(path);

      var directoryPaths = Directory.GetDirectories(BasePath + path);

      var directoryNames = new List<string>();

      foreach (string directoryPath in directoryPaths)
        directoryNames.Add(GetName(directoryPath));

      return directoryNames.ToArray();
    }

    public string[] GetFileNames(string path)
    {
      path = FormatPath(path);

      var filePaths = Directory.GetFiles(BasePath + path);

      var fileNames = new List<string>();

      foreach(string filePath in filePaths)
        fileNames.Add(GetName(filePath));

      return fileNames.ToArray();
    }

    public void DeleteDirectory(string path)
    {
      if (Directory.Exists(BasePath + path))
        Directory.Delete(BasePath + path, true);
    }

    public void DeleteFile(string path)
    {
      if (File.Exists(BasePath + path))
        File.Delete(BasePath + path);
    }

    public void CopyDirectory(string sourcePath, string destinationPath)
    {
      if (Directory.Exists(BasePath + sourcePath))
      {
        Directory.CreateDirectory(BasePath + destinationPath);

        foreach (string directory in Directory.GetDirectories(BasePath + sourcePath))
        {
          string directoryName = GetName(directory);
          CopyDirectory(sourcePath + "/" + directoryName, destinationPath + "/" + directoryName);
        }

        foreach (string file in Directory.GetFiles(BasePath + sourcePath))
        {
          string fileName = GetName(file);
          CopyFile(sourcePath + "/" + fileName, destinationPath + "/" + fileName);
        }
      }
    }

    public void CopyFile(string sourcePath, string destinationPath)
    {
      if (File.Exists(BasePath + sourcePath))
      {
        string content = File.ReadAllText(BasePath + sourcePath, System.Text.Encoding.UTF8);
        
        if(!Directory.Exists(BasePath + GetPath(destinationPath)))
          Directory.CreateDirectory(BasePath + GetPath(destinationPath));

        File.WriteAllText(BasePath + destinationPath, content, System.Text.Encoding.UTF8);
      }
    }

    public Stream OpenFile(string path, StorageFileMode mode, StorageFileAccess access)
    {
      path = path.Replace('\\', '/');

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
            {
              if (!Directory.Exists(BasePath + GetPath(path)))
                Directory.CreateDirectory(BasePath + GetPath(path));
              var file = File.Create(BasePath + path);
              file.Close();
            }
            break;
          case StorageFileMode.CreateNew:
            {
              if (!Directory.Exists(BasePath + GetPath(path)))
                Directory.CreateDirectory(BasePath + GetPath(path));
              if (!File.Exists(BasePath + path))
              {
                var file = File.Create(BasePath + path);
                file.Close();
              }
              else
                throw new IOException();
            }
            break;
          case StorageFileMode.OpenOrCreate:
            {
              if (!Directory.Exists(BasePath + GetPath(path)))
                Directory.CreateDirectory(BasePath + GetPath(path));
              if (!File.Exists(BasePath + path))
              {
                var file = File.Create(BasePath + path);
                file.Close();
              }
            }
            break;
        }

      var stream = File.Open(BasePath + path, fileMode, fileAccess);
      _openedStreams.Add(stream);
      return stream;
    }

    public void RenameDirectory(string directoryPath, string newDirectoryName)
    {
      if (Directory.Exists(BasePath + directoryPath))
        Directory.Move(BasePath + directoryPath, BasePath + GetPath(directoryPath) + newDirectoryName);
    }

    public object LoadImage(string pathToImage)
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
      if (!Directory.Exists(BasePath + GetPath(path)))
        Directory.CreateDirectory(BasePath + GetPath(path));

      File.WriteAllText(BasePath + path, content, System.Text.Encoding.UTF8);
    }

    public object ReadSerializableObject(string path, Type type)
    {
      using (Stream fileStream = this.OpenFile(path, StorageFileMode.Open, StorageFileAccess.Read))
      {
        DataContractSerializer serializer = new DataContractSerializer(type);
        object serialireableObject = serializer.ReadObject(fileStream); // TODO: does not work any more
        fileStream.Close();
        return serialireableObject;
      }
    }

    public void WriteSerializableObject(string path, object serializableObject)
    {
      using (Stream fileStream = this.OpenFile(path, StorageFileMode.Create, StorageFileAccess.Write))
      {
        DataContractSerializer serializer = new DataContractSerializer(serializableObject.GetType());
        serializer.WriteObject(fileStream, serializableObject);
        fileStream.Close();
      }
    }

    public void Dispose()
    {
      foreach (var stream in _openedStreams)
      {
        stream.Close();
        stream.Dispose();
      }
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

    private string GetName(string path)
    {
      int startSlash = path.LastIndexOf("/", System.StringComparison.Ordinal) + 1;
      int startBackSlash = path.LastIndexOf("\\", System.StringComparison.Ordinal) + 1;

      if (startSlash > startBackSlash)
        return path.Substring(startSlash);
      else
        return path.Substring(startBackSlash);
    }

    private string GetPath(string path)
    {
      return path.Substring(0, path.Length - GetName(path).Length);
    }

    private string FormatPath(string pathToFormat)
    {
      return pathToFormat.Replace('\\', '/');
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
