using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Media.Imaging;
using Catrobat.Core.Storage;
using ImageTools;
using ImageTools.Filtering;

namespace Catrobat.IDEWindowsPhone.Misc.Storage
{
    public class StoragePhone : IStorage
    {
        private const string ThumbnailExtension = "_thumb.png";
        private static int _imageThumbnailDefaultMaxWidthHeight = 200;
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

        public void DeleteDirectory(string path)
        {
            if (DirectoryExists(path))
            {
                DeleteDirectory(path, _iso);
            }
        }

        public void DeleteFile(string path)
        {
            using (var iso = IsolatedStorageFile.GetUserStoreForApplication())
            {
                iso.DeleteFile(path);
            }
        }

        public void CopyDirectory(string sourcePath, string destinationPath)
        {
            if (DirectoryExists(destinationPath))
            {
                throw new Exception("Destination directory " + destinationPath + " does already exist.");
            }

            CreateFoldersIfNotExist(destinationPath, false);
            CopyDirectory(sourcePath, destinationPath, _iso);
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
            {
                switch (mode)
                {
                    case StorageFileMode.Create:
                    case StorageFileMode.CreateNew:
                    case StorageFileMode.OpenOrCreate:
                        CreateFoldersIfNotExist(path, true);
                        break;
                }
            }

            var storageFileStream = _iso.OpenFile(path, fileMode, fileAccess);
            _openedStreams.Add(storageFileStream);

            return storageFileStream;
        }

        public void RenameDirectory(string directoryPath, string newDirectoryName)
        {
            var newDirectoryPath = directoryPath.Remove(directoryPath.LastIndexOf('/'));
            newDirectoryPath += "/" + newDirectoryName;

            _iso.CreateDirectory(newDirectoryPath);

            var folders = _iso.GetDirectoryNames(directoryPath + "/*");

            foreach (string folder in folders)
            {
                _iso.MoveDirectory(directoryPath + "/" + folder, newDirectoryPath + "/" + folder);
            }

            foreach (string file in _iso.GetFileNames(directoryPath + "/*"))
            {
                if (_iso.FileExists(directoryPath + "/" + file))
                {
                    _iso.MoveFile(directoryPath + "/" + file, newDirectoryPath + "/" + file);
                }
            }

            _iso.DeleteDirectory(directoryPath);
        }


        public void WriteTextFile(string path, string content)
        {
            var writer = new StreamWriter(OpenFile(path, StorageFileMode.Create, StorageFileAccess.Write), Encoding.UTF8);
            writer.Write(content);
            writer.Close();
            writer.Dispose();
        }

        public object ReadSerializableObject(string path, Type type)
        {
            using (var fileStream = OpenFile(path, StorageFileMode.Open, StorageFileAccess.Read))
            {
                var serializer = new DataContractSerializer(type);
                var serialireableObject = serializer.ReadObject(fileStream); // TODO: does not working any more
                fileStream.Close();
                fileStream.Dispose();
                return serialireableObject;
            }
        }

        public void WriteSerializableObject(string path, object serializableObject)
        {
            using (var fileStream = OpenFile(path, StorageFileMode.Create, StorageFileAccess.Write))
            {
                var serializer = new DataContractSerializer(serializableObject.GetType());
                serializer.WriteObject(fileStream, serializableObject);
                fileStream.Close();
                fileStream.Dispose();
            }
        }

        public string ReadTextFile(string path)
        {
            var fileStream = OpenFile(path, StorageFileMode.Open, StorageFileAccess.Read);
            var reader = new StreamReader(fileStream);
            var text = reader.ReadToEnd();
            fileStream.Close();
            fileStream.Dispose();
            return text;
        }

        public object LoadImage(string pathToImage)
        {
            if (FileExists(pathToImage))
            {
                try
                {
                    var bitmapImage = new BitmapImage();

                    using (var storageFileStream = _iso.OpenFile(pathToImage,
                                                                 FileMode.Open,
                                                                 FileAccess.Read))
                    {
                        bitmapImage.SetSource(storageFileStream);
                        storageFileStream.Close();
                        storageFileStream.Dispose();

                        return bitmapImage;
                    }
                }
                catch {} //TODO: Exception message. Maybe logging?
            }

            return null;
        }

        public object LoadImageThumbnail(string pathToImage)
        {
            object retVal = null;
            var thumbnailPath = string.Format("{0}{1}", pathToImage, ThumbnailExtension);

            if (FileExists(thumbnailPath))
            {
                retVal = LoadImage(thumbnailPath) as BitmapImage;
            }
            else
            {
                var fullSizeBitmapImage = LoadImage(pathToImage) as BitmapImage;

                if (fullSizeBitmapImage != null)
                {
                    var fullSizeImage = new WriteableBitmap(fullSizeBitmapImage).ToImage();
                    var thumbnailImage = CreateThumbnailImage(fullSizeImage,
                                                              _imageThumbnailDefaultMaxWidthHeight);

                    try
                    {
                        var fileStream = OpenFile(thumbnailPath,
                                                  StorageFileMode.Create,
                                                  StorageFileAccess.Write);

                        thumbnailImage.WriteToStream(fileStream, thumbnailPath);
                        fileStream.Close();
                        fileStream.Dispose();

                        retVal = thumbnailImage.ToBitmap();
                    }

                    catch
                    {
                        retVal = null;
                    }
                }
            }

            return retVal;
        }

        public void SaveImage(string path, object image)
        {
            //TODO: Implement!
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _iso.Dispose();

            foreach (Stream stream in _openedStreams)
            {
                stream.Dispose();
            }
        }

        public string BasePath
        {
            get { return ""; }
        }

        private void CreateFoldersIfNotExist(string path, bool isFilePath)
        {
            var splitPath = path.Split('/');
            var offset = isFilePath ? 1 : 0;
            for (var index = 0; index < splitPath.Length - 1; index++)
            {
                var subPath = "";

                for (var subIndex = 0; subIndex <= index; subIndex++)
                {
                    subPath += "/" + splitPath[subIndex];
                }

                if (subPath != "")
                {
                    _iso.CreateDirectory(subPath);
                }
            }
        }

        private void DeleteDirectory(string path, IsolatedStorageFile iso)
        {
            if (!iso.DirectoryExists(path))
            {
                return;
            }

            var folders = iso.GetDirectoryNames(path + "/" + "*.*");

            foreach (string folder in folders)
            {
                var folderPath = path + "/" + folder;
                DeleteDirectory(folderPath, iso);
            }

            foreach (string file in iso.GetFileNames(path + "/" + "*.*"))
            {
                iso.DeleteFile(path + "/" + file);
            }

            if (path != "")
            {
                iso.DeleteDirectory(path);
            }
        }

        public void CopyDirectory(string sourcePath, string destinationPath, IsolatedStorageFile iso)
        {
            if (!iso.DirectoryExists(sourcePath))
            {
                return;
            }


            var folders = iso.GetDirectoryNames(sourcePath + "/" + "*.*");

            foreach (string folder in folders)
            {
                var sourceFolderPath = sourcePath + "/" + folder;
                var destinationFolderPath = destinationPath + "/" + folder;

                iso.CreateDirectory(destinationFolderPath);
                CopyDirectory(sourceFolderPath, destinationFolderPath, iso);
            }

            var sourceFilePath = "";
            var destinationFilePath = "";
            try
            {
                foreach (string file in iso.GetFileNames(sourcePath + "/" + "*.*"))
                {
                    if (file.StartsWith("."))
                    {
                        continue;
                    }

                    sourceFilePath = sourcePath + "/" + file;
                    destinationFilePath = destinationPath + "/" + file;
                    iso.CopyFile(sourceFilePath, destinationFilePath);
                }
            }
            catch (Exception)
            {
                throw new Exception("Cannot coppy " + sourceFilePath + "  to " + destinationFilePath);
            }
        }

        public void SetImageMaxThumbnailWidthHeight(int maxWidthHeight)
        {
            _imageThumbnailDefaultMaxWidthHeight = maxWidthHeight;
        }

        public static ExtendedImage CreateThumbnailImage(ExtendedImage image, int maxWidthHeight)
        {
            int width, height;

            if (image.PixelWidth > image.PixelHeight)
            {
                width = maxWidthHeight;
                height = (int) ((image.PixelHeight/(double) image.PixelWidth)*maxWidthHeight);
            }
            else
            {
                height = maxWidthHeight;
                width = (int) ((image.PixelWidth/(double) image.PixelHeight)*maxWidthHeight);
            }

            var resizedImage = ExtendedImage.Resize(image, width, height, new NearestNeighborResizer());

            return resizedImage;
        }

        public void SaveBitmapImage(string path, BitmapImage image)
        {
            var fileStream = OpenFile(path, StorageFileMode.Create, StorageFileAccess.Write);
            var wb = new WriteableBitmap(image);
            wb.SaveJpeg(fileStream, wb.PixelWidth, wb.PixelHeight, 0, 85);
            fileStream.Close();
            fileStream.Dispose();
        }
    }
}