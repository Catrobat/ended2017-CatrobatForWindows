using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Media.Imaging;
using Catrobat.Core;
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
            return _iso.GetDirectoryNames(StringExtensions.Concat(path, "/*"));
        }

        public string[] GetFileNames(string path)
        {
            return _iso.GetFileNames(StringExtensions.Concat(path, "/*.*"));
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
                throw new Exception(string.Format("Destination directory {0} already exists.", destinationPath));
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
            newDirectoryPath = Path.Combine(newDirectoryPath, newDirectoryName);

            _iso.CreateDirectory(newDirectoryPath);

            var folders = _iso.GetDirectoryNames(StringExtensions.Concat(directoryPath, "/*"));

            foreach (string folder in folders)
            {
                var tempFrom = Path.Combine(directoryPath, folder);
                var tempTo = Path.Combine(newDirectoryPath, folder);
                _iso.MoveDirectory(tempFrom, tempTo);
            }

            foreach (string file in _iso.GetFileNames(StringExtensions.Concat(directoryPath, "/*")))
            {
                var source = Path.Combine(directoryPath, file);

                if (_iso.FileExists(source))
                {
                    var destination = Path.Combine(newDirectoryPath, file);
                    _iso.MoveFile(source, destination);
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
                var serializeableObject = serializer.ReadObject(fileStream); // TODO: does not work any more
                fileStream.Close();
                fileStream.Dispose();
                return serializeableObject;
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
            var withoutExtension = Path.GetFileNameWithoutExtension(pathToImage);
            var thumbnailPath = string.Format("{0}{1}",withoutExtension, ThumbnailExtension);

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
                    var thumbnailImage = CreateThumbnailImage(fullSizeImage, _imageThumbnailDefaultMaxWidthHeight);

                    try
                    {
                        var fileStream = OpenFile(thumbnailPath, StorageFileMode.Create, StorageFileAccess.Write);

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
                    subPath = Path.Combine(subPath, splitPath[subIndex]);
                }

                if (!string.IsNullOrEmpty(subPath))
                {
                    _iso.CreateDirectory(subPath);
                }
            }
        }

        private void DeleteDirectory(string path, IsolatedStorageFile iso)
        {
            if (iso.DirectoryExists(path))
            {
                var directory = Path.Combine(path, "*.*");
                var folders = iso.GetDirectoryNames(directory);

                foreach (string folder in folders)
                {
                    var folderPath = Path.Combine(path, folder);
                    DeleteDirectory(folderPath, iso);
                }

                foreach (string file in iso.GetFileNames(directory))
                {
                    iso.DeleteFile(Path.Combine(path, file));
                }

                if (!string.IsNullOrEmpty(path))
                {
                    iso.DeleteDirectory(path);
                }
            }
        }

        public void CopyDirectory(string sourcePath, string destinationPath, IsolatedStorageFile iso)
        {
            if (iso.DirectoryExists(sourcePath))
            {
                var directory = Path.Combine(sourcePath, "*.*");
                var folders = iso.GetDirectoryNames(directory);

                foreach (string folder in folders)
                {
                    var sourceFolderPath = Path.Combine(sourcePath, folder);
                    var destinationFolderPath = Path.Combine(destinationPath, folder);

                    iso.CreateDirectory(destinationFolderPath);
                    CopyDirectory(sourceFolderPath, destinationFolderPath, iso);
                }

                var sourceDirectory = "";
                var destinationDirectory = "";

                try
                {
                    foreach (string file in iso.GetFileNames(directory))
                    {
                        if (file.StartsWith("."))
                        {
                            continue;
                        }

                        sourceDirectory = Path.Combine(sourceDirectory, file);
                        destinationDirectory = Path.Combine(destinationDirectory, file);
                        iso.CopyFile(sourceDirectory, destinationDirectory);
                    }
                }
                catch (Exception)
                {
                    throw new Exception(string.Format("Cannot coppy {0} to {1}", sourceDirectory, destinationDirectory));
                }
            }
        }

        public void SetImageMaxThumbnailWidthHeight(int maxWidthHeight)
        {
            _imageThumbnailDefaultMaxWidthHeight = maxWidthHeight;
        }

        private static ExtendedImage CreateThumbnailImage(ExtendedImage image, int maxWidthHeight)
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