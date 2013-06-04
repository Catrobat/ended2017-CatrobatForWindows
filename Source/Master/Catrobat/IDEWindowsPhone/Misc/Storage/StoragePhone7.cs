using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Catrobat.Core.Storage;
using System;
using ImageTools;
using ImageTools.Filtering;
using ImageTools.IO;
using ImageTools.IO.Bmp;
using ImageTools.IO.Gif;
using ImageTools.IO.Png;

namespace Catrobat.IDEWindowsPhone.Misc.Storage
{
  public class StoragePhone : IStorage
  {
    private readonly IsolatedStorageFile _iso = IsolatedStorageFile.GetUserStoreForApplication();
    private readonly List<Stream> _openedStreams = new List<Stream>();

    public bool FileExists(string path)
    {
      return _iso.FileExists(path);
    }

    public bool DirectoryExists(string path)
    {
      return _iso.DirectoryExists(path);
    }

    public string[] GetDirectoryNames(string path)
    {
      return _iso.GetDirectoryNames(path + "/*");
    }

    public string[] GetFileNames(string path)
    {
      return _iso.GetFileNames(path + "/*.*");
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
          _iso.CreateDirectory(subPath);
      }
    }

    public void DeleteDirectory(string path)
    {
      DeleteDirectory(path, _iso);
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
      CopyDirectory(sourcePath, destinationPath, _iso);
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
      _iso.CopyFile(sourcePath, destinationPath);
    }

    public Stream OpenFile(string path, StorageFileMode mode, StorageFileAccess access)
    {
      var fileMode = FileMode.Append;
      var fileAccess = FileAccess.Read;

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

      IsolatedStorageFileStream storageFileStream = _iso.OpenFile(path, fileMode, fileAccess);
      _openedStreams.Add(storageFileStream);

      return storageFileStream;
    }

    public void RenameDirectory(string directoryPath, string newDirectoryName)
    {
      string newDirectoryPath = directoryPath.Remove(directoryPath.LastIndexOf('/'));
      newDirectoryPath += "/" + newDirectoryName;

      _iso.CreateDirectory(newDirectoryPath);

      var folders = _iso.GetDirectoryNames(directoryPath + "/*");

      foreach (var folder in folders)
      {
        _iso.MoveDirectory(directoryPath + "/" + folder, newDirectoryPath + "/" + folder);
      }

      foreach (var file in _iso.GetFileNames(directoryPath + "/*"))
      {
        if (_iso.FileExists(directoryPath + "/" + file))
          _iso.MoveFile(directoryPath + "/" + file, newDirectoryPath + "/" + file);
      }

      _iso.DeleteDirectory(directoryPath);
    }


    public void WriteTextFile(string path, string content)
    {
      var writer = new StreamWriter(this.OpenFile(path, StorageFileMode.Create, StorageFileAccess.Write), Encoding.UTF8);
      writer.Write(content);
      writer.Close();
      writer.Dispose();
    }

    public object ReadSerializableObject(string path, Type type)
    {
      using (Stream fileStream = this.OpenFile(path, StorageFileMode.Open, StorageFileAccess.Read))
      {
        var serializer = new DataContractSerializer(type);
        object serialireableObject = serializer.ReadObject(fileStream); // TODO: does not working any more
        fileStream.Close();
        fileStream.Dispose();
        return serialireableObject;
      }
    }

    public void WriteSerializableObject(string path, object serializableObject)
    {
      using (Stream fileStream = this.OpenFile(path, StorageFileMode.Create, StorageFileAccess.Write))
      {
        var serializer = new DataContractSerializer(serializableObject.GetType());
        serializer.WriteObject(fileStream, serializableObject);
        fileStream.Close();
        fileStream.Dispose();
      }
    }

    public string ReadTextFile(string path)
    {
      var fileStream = this.OpenFile(path, StorageFileMode.Open, StorageFileAccess.Read);
      var reader = new StreamReader(fileStream);
      var text = reader.ReadToEnd();
      fileStream.Close();
      fileStream.Dispose();
      return text;
    }

    public object LoadImage(string pathToImage)
    {
      try
      {
        var bitmapImage = new BitmapImage();

        byte[] myByteArray = null;

        using (IsolatedStorageFileStream storageFileStream = _iso.OpenFile(pathToImage, FileMode.Open, FileAccess.Read))
        {
          bitmapImage.SetSource(storageFileStream);
          storageFileStream.Close();
          storageFileStream.Dispose();

          return bitmapImage;
        }
      }
      catch
      {
        return null;
      }
    }

    public BitmapImage LoadImageAsBitmapImage(string pathToImage)
    {
      try
      {
        var bitmapImage = new BitmapImage();

        using (IsolatedStorageFileStream storageFileStream = _iso.OpenFile(pathToImage, FileMode.Open, FileAccess.Read))
        {
          bitmapImage.SetSource(storageFileStream);
          storageFileStream.Close();
          storageFileStream.Dispose();
        }

        return bitmapImage;
      }
      catch
      {
        return null;
      }
    }

    private const string ThumbnailExtension = "_thumb.png";
    private static int _imageThumbnailDefaultMaxWidthHeight = 200;

    public void SetImageMaxThumbnailWidthHeight(int maxWidthHeight)
    {
      _imageThumbnailDefaultMaxWidthHeight = maxWidthHeight;
    }

    public object LoadImageThumbnail(string pathToImage)
    {
      try
      {
        var thumbnailPath = pathToImage + ThumbnailExtension;

        if (this.FileExists(thumbnailPath))
        {
          var imageBitmapThumbnail = LoadImageAsBitmapImage(thumbnailPath);
          return imageBitmapThumbnail;
        }

        var fullSizeBitmapImage = LoadImageAsBitmapImage(pathToImage);
        var fullSizeImage = new WriteableBitmap(fullSizeBitmapImage).ToImage();

        var thumbnailImage = CreateThumbnailImage(fullSizeImage, _imageThumbnailDefaultMaxWidthHeight);

        var fileStream1 = OpenFile(pathToImage + ThumbnailExtension, StorageFileMode.Create, StorageFileAccess.Write);
        thumbnailImage.WriteToStream(fileStream1, pathToImage + ThumbnailExtension);
        fileStream1.Close();
        fileStream1.Dispose();

        return thumbnailImage.ToBitmap();
      }
      catch
      {
        return null;
      }
    }

    public static ExtendedImage CreateThumbnailImage(ExtendedImage image, int maxWidthHeight)
    {
      int width;
      int height;

      if(image.PixelWidth > image.PixelHeight)
      {
        width = maxWidthHeight;
        height = (int)((image.PixelHeight / (double)image.PixelWidth) * maxWidthHeight);
      }
      else
      {
        height = maxWidthHeight;
        width = (int)((image.PixelWidth / (double)image.PixelHeight) * maxWidthHeight);
      }

      var resizedImage = ExtendedImage.Resize(image, width, height, new NearestNeighborResizer());
      return resizedImage;
    }

    public void SaveImage(string path, object image)
    {
      throw new NotImplementedException();
    }

    public void SaveBitmapImage(string path, BitmapImage image)
    {
      var fileStream = this.OpenFile(path, StorageFileMode.Create, StorageFileAccess.Write);
      var wb = new WriteableBitmap(image);
      wb.SaveJpeg(fileStream, wb.PixelWidth, wb.PixelHeight, 0, 85);
      fileStream.Close();
      fileStream.Dispose();
    }

    public void Dispose()
    {
      _iso.Dispose();

      foreach (var stream in _openedStreams)
        stream.Dispose();
    }

    public string BasePath { get { return ""; } }
  }
}
