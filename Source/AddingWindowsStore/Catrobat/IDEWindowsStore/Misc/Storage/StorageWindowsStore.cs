using System.Collections.Generic;
using System.IO;
using Catrobat.Core.Services.Data;
using Catrobat.Core.Services.Storage;
using System;

namespace Catrobat.IDEWindowsStore.Misc.Storage
{
  public class StorageWindowsStore : IStorage
  {

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

      public void MoveDirectory(string sourcePath, string destinationPath)
      {
          throw new NotImplementedException();
      }

      public void CopyFile(string sourcePath, string destinationPath)
      {
          throw new NotImplementedException();
      }

      public void MoveFile(string sourcePath, string destinationPath)
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

      public Core.Services.Data.PortableImage LoadImage(string pathToImage)
      {
          throw new NotImplementedException();
      }

      public Core.Services.Data.PortableImage LoadImageThumbnail(string pathToImage)
      {
          throw new NotImplementedException();
      }

      public void SaveImage(string path, Core.Services.Data.PortableImage image, bool deleteExisting, ImageFormat format)
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

      public string BasePath
      {
          get { throw new NotImplementedException(); }
      }

      public void Dispose()
      {
          throw new NotImplementedException();
      }

      PortableImage IStorage.CreateThumbnail(Core.Services.Data.PortableImage image)
      {
          throw new NotImplementedException();
      }
  }
}
