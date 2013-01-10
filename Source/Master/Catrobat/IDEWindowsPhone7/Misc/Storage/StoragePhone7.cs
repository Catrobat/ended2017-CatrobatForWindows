using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Media.Imaging;
using Catrobat.Core.Storage;
using System;

namespace Catrobat.IDEWindowsPhone7.Misc.Storage
{
  public class StoragePhone7 : IStorage
  {
    private IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication();

    public bool FileExists(string path)
    {
      return iso.FileExists(path);
    }

    public bool DirectoryExists(string path)
    {
      return iso.DirectoryExists(path);
    }

    public string[] GetDirectoryNames(string path)
    {
      return iso.GetDirectoryNames(path);
    }

    public string[] GetFileNames(string path)
    {
      return iso.GetFileNames(path);
    }

    private void CreateFoldersIfNotExist(string path, bool isFilePath)
    {
      string[] splitPath = path.Split('/');
      int offset = isFilePath ? 1 : 0;
      for (int index = 0; index < splitPath.Length - 1; index++)
      {
        string subPath = "";

        for (int subIndex = 0; subIndex <= index; subIndex++)
          subPath += "/" + splitPath[subIndex];

        if (subPath != "")
          iso.CreateDirectory(subPath);
      }
    }

    public void DeleteDirectory(string path)
    {
      DeleteDirectory(path, iso);
    }

    private void DeleteDirectory(string path, IsolatedStorageFile iso)
    {
      if (!iso.DirectoryExists(path))
        return;

      var folders = iso.GetDirectoryNames(path + "/" + "*.*");

      foreach (var folder in folders)
      {
        string folderPath = path + "/" + folder;
        DeleteDirectory(folderPath, iso);
      }

      foreach (var file in iso.GetFileNames(path + "/" + "*.*"))
      {
        iso.DeleteFile(path + "/" + file);
      }

      if (path != "")
        iso.DeleteDirectory(path);
    }

    public void DeleteFile(string path)
    {
      using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
      {
        iso.DeleteFile(path);
      }
    }

    public void CopyDirectory(string sourcePath, string destinationPath)
    {
      CreateFoldersIfNotExist(destinationPath, false);
      CopyDirectory(sourcePath, destinationPath, iso);
    }

    public void CopyDirectory(string sourcePath, string destinationPath, IsolatedStorageFile iso)
    {
      if (!iso.DirectoryExists(sourcePath))
        return;

      var folders = iso.GetDirectoryNames(sourcePath + "/" + "*.*");

      foreach (var folder in folders)
      {
        string sourceFolderPath = sourcePath + "/" + folder;
        string destinationFolderPath = destinationPath + "/" + folder;

        iso.CreateDirectory(destinationFolderPath);
        CopyDirectory(sourceFolderPath, destinationFolderPath, iso);
      }

      foreach (var file in iso.GetFileNames(sourcePath + "/" + "*.*"))
      {
        string sourceFilePath = sourcePath + "/" + file;
        string destinationFilePath = destinationPath + "/" + file;

        iso.CopyFile(sourceFilePath, destinationFilePath);
      }
    }

    public void CopyFile(string sourcePath, string destinationPath)
    {
      CreateFoldersIfNotExist(destinationPath, true);
      iso.CopyFile(sourcePath, destinationPath);
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
            CreateFoldersIfNotExist(path, true);
            break;
        }

      IsolatedStorageFileStream isfs = iso.OpenFile(path, fileMode, fileAccess);
      return isfs;
    }

    public void RenameDirectory(string directoryPath, string newDirectoryName)
    {
      string newDirectoryPath = directoryPath.Remove(directoryPath.LastIndexOf('/'));
      newDirectoryPath += "/" + newDirectoryName;

      iso.CreateDirectory(newDirectoryPath);

      var folders = iso.GetDirectoryNames(directoryPath + "/*");

      foreach (var folder in folders)
      {
        iso.MoveDirectory(directoryPath + "/" + folder, newDirectoryPath + "/" + folder);
      }

      foreach (var file in iso.GetFileNames(directoryPath + "/*"))
      {
        if (iso.FileExists(directoryPath + "/" + file))
          iso.MoveFile(directoryPath + "/" + file, newDirectoryPath + "/" + file);
      }

      iso.DeleteDirectory(directoryPath);
    }

    public BitmapImage LoadImage(string pathToImage)
    {
      try
      {
        BitmapImage bitmapImage = new BitmapImage();

        using (IsolatedStorageFileStream isfs = iso.OpenFile(pathToImage, FileMode.Open, FileAccess.Read))
        {
          bitmapImage.SetSource(isfs);
          isfs.Close();
        }

        return bitmapImage;
      }
      catch
      {
        return null;
      }
    }

    public void SaveImage(string path, BitmapImage image)
    {
      Stream fileStream = this.OpenFile(path, StorageFileMode.Create, StorageFileAccess.Write);
      WriteableBitmap wb = new WriteableBitmap(image);
      System.Windows.Media.Imaging.Extensions.SaveJpeg(wb, fileStream, wb.PixelWidth, wb.PixelHeight, 0, 85);
      fileStream.Close();
    }

    public string ReadTextFile(string path)
    {
      Stream fileStream = this.OpenFile(path, StorageFileMode.Open, StorageFileAccess.Read);
      StreamReader reader = new StreamReader(fileStream);
      string text = reader.ReadToEnd();
      fileStream.Close();
      return text;
    }

    public void WriteTextFile(string path, string content)
    {
      StreamWriter writer = new StreamWriter(this.OpenFile(path, StorageFileMode.Create, StorageFileAccess.Write), Encoding.UTF8);
      writer.Write(content);
      writer.Close();
    }

    public object ReadSerializableObject(string path, Type type)
    {
      using (Stream fileStream = this.OpenFile(path, StorageFileMode.Open, StorageFileAccess.Read))
      {
        DataContractSerializer serializer = new DataContractSerializer(type);
        object serialireableObject = serializer.ReadObject(fileStream); // TODO: does not working any more
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
      iso.Dispose();
    }

    public long Quota
    {
      get { throw new NotImplementedException(); }
    }

    byte[] IStorage.LoadImage(string pathToImage)
    {
      throw new NotImplementedException();
    }

    public bool IncreaseQuotaTo(long quota)
    {
      throw new NotImplementedException();
    }
  }
}
