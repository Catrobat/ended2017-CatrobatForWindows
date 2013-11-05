using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;
using ToolStackPNGWriterLib;

namespace Catrobat.IDE.Store.Services.Storage
{
    public class StorageStore : IStorage
    {
        private static int _imageThumbnailDefaultMaxWidthHeight = 400;
        private readonly List<Stream> _openedStreams = new List<Stream>();

        #region Synchron

        public void CreateDirectory(string path)
        {
            CreateDirectoryAsync(path).Wait();
        }

        public bool DirectoryExists(string path)
        {
            var task = DirectoryExistsAsync(path);
            task.Wait();
            return task.Result;
        }

        public bool FileExists(string path)
        {
            var task = FileExistsAsync(path);
            task.Wait();
            return task.Result;
        }

        public string[] GetDirectoryNames(string path)
        {
            var task = GetDirectoryNamesAsync(path);
            task.Wait();
            return task.Result;
        }

        public string[] GetFileNames(string path)
        {
            var task = GetFileNamesAsync(path);
            task.Wait();
            return task.Result;
        }

        public void DeleteDirectory(string path)
        {
            DeleteDirectoryAsync(path).Wait();
        }

        public void DeleteFile(string path)
        {
            DeleteFileAsync(path).Wait();
        }

        public void CopyDirectory(string sourcePath, string destinationPath)
        {
            CopyDirectoryAsync(sourcePath, destinationPath).Wait();
        }

        public void MoveDirectory(string sourcePath, string destinationPath)
        {
            MoveDirectoryAsync(sourcePath, destinationPath).Wait();
        }

        public void CopyFile(string sourcePath, string destinationPath)
        {
            CopyFileAsync(sourcePath, destinationPath).Wait();
        }

        public void MoveFile(string sourcePath, string destinationPath)
        {
            MoveFileAsync(sourcePath, destinationPath).Wait();
        }

        public Stream OpenFile(string path, StorageFileMode mode, StorageFileAccess access)
        {
            var task = OpenFileAsync(path, mode, access);
            task.Wait();
            return task.Result;
        }

        public void RenameDirectory(string directoryPath, string newDirectoryName)
        {
            RenameDirectoryAsync(directoryPath, newDirectoryName).Wait();
        }

        public PortableImage LoadImage(string pathToImage)
        {
            var task = LoadImageAsync(pathToImage);
            task.Wait();
            return task.Result;
        }

        public PortableImage LoadImageThumbnail(string pathToImage)
        {
            var task = LoadImageThumbnailAsync(pathToImage);
            task.Wait();
            return task.Result;
        }

        public PortableImage CreateThumbnail(PortableImage image)
        {
            var task = CreateThumbnailAsync(image);
            task.Wait();
            return task.Result;
        }

        public void DeleteImage(string pathToImage)
        {
            DeleteImageAsync(pathToImage).Wait();
        }

        public void SaveImage(string path, PortableImage image, bool deleteExisting, ImageFormat format)
        {
            SaveImageAsync(path, image, deleteExisting, format).Wait();
        }

        public string ReadTextFile(string path)
        {
            var task = ReadTextFileAsync(path);
            task.Wait();
            return task.Result;
        }

        public void WriteTextFile(string path, string content)
        {
            WriteTextFileAsync(path, content).Wait();
        }

        public object ReadSerializableObject(string path, Type type)
        {
            var task = ReadSerializableObjectAsync(path, type);
            task.Wait();
            return task.Result;
        }

        public void WriteSerializableObject(string path, object serializableObject)
        {
            WriteSerializableObjectAsync(path, serializableObject).Wait();
        }

        public string BasePath
        {
            get { return ""; }
        }

        public void Dispose()
        {
            foreach (var stream in _openedStreams)
            {
                stream.Dispose();
            }
        }

        internal void SetImageMaxThumbnailWidthHeight(int maxWidthHeight)
        {
            _imageThumbnailDefaultMaxWidthHeight = maxWidthHeight;
        }

        #endregion

        #region Async

        public async Task CreateDirectoryAsync(string path)
        {
            await CreateFolderPath(path);
        }

        public async Task<bool> DirectoryExistsAsync(string path)
        {
            var folder = await GetFolderAsync(path);
            return folder != null;
        }

        public async Task<bool> FileExistsAsync(string path)
        {
            try
            {
                var folder = await GetFileAsync(path, false);
                return folder != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string[]> GetDirectoryNamesAsync(string path)
        {
            var rootDirectory = await GetFolderAsync(path);
            var directories = await rootDirectory.GetFoldersAsync();

            return directories.Select(directory => directory.Name).ToArray();
        }

        public async Task<string[]> GetFileNamesAsync(string path)
        {
            var rootDirectory = await GetFolderAsync(path);
            var files = await rootDirectory.GetFilesAsync();

            return files.Select(file => file.Name).ToArray();
        }

        public async Task DeleteDirectoryAsync(string path)
        {
            var directory = await GetFolderAsync(path);

            var folders = await directory.GetFoldersAsync();

            foreach (var folder in folders)
            {
                var folderPath = Path.Combine(path, folder.Name);
                await DeleteDirectoryAsync(folderPath);
            }

            foreach (var file in await directory.GetFilesAsync())
            {
                await file.DeleteAsync();
            }

            if(path != "")
                await directory.DeleteAsync();
        }

        public async Task DeleteFileAsync(string path)
        {
            var file = await GetFileAsync(path);

            if (file != null)
                await file.DeleteAsync();
        }

        public async Task CopyDirectoryAsync(string sourcePath, string destinationPath)
        {
            var directory = await GetFolderAsync(sourcePath);

            foreach (var folder in await directory.GetFoldersAsync())
            {
                var sourceFolderPath = Path.Combine(sourcePath, folder.Name);
                var destinationFolderPath = Path.Combine(destinationPath, folder.Name);

                await CreateDirectoryAsync(destinationFolderPath);
                await CopyDirectoryAsync(sourceFolderPath, destinationFolderPath);
            }

            var sourceDirectory = "";
            var destinationDirectory = "";

            try
            {
                foreach (var file in await directory.GetFilesAsync())
                {
                    if (file.Name.StartsWith("."))
                        continue;

                    sourceDirectory = Path.Combine(sourcePath, file.Name);
                    destinationDirectory = Path.Combine(destinationPath, file.Name);
                    await CopyFileAsync(sourceDirectory, destinationDirectory);
                }
            }
            catch (Exception)
            {
                throw new Exception(string.Format("Cannot coppy {0} to {1}", sourceDirectory, destinationDirectory));
            }
        }

        public async Task MoveDirectoryAsync(string sourcePath, string destinationPath)
        {
            await CopyDirectoryAsync(sourcePath, destinationPath);

            var directory = await GetFolderAsync(sourcePath);
            await directory.DeleteAsync();
        }

        public async Task CopyFileAsync(string sourcePath, string destinationPath)
        {
            var file = await GetFileAsync(sourcePath);
            var destinationFolderPath = Path.GetDirectoryName(destinationPath);
            var destinationFolder = await GetFolderAsync(destinationFolderPath);

            await file.CopyAsync(destinationFolder, file.Name, NameCollisionOption.ReplaceExisting);
        }

        public async Task MoveFileAsync(string sourcePath, string destinationPath)
        {
            var file = await GetFileAsync(sourcePath);
            var destinationFolderPath = Path.GetPathRoot(destinationPath);
            var destinationFolder = await GetFolderAsync(destinationFolderPath);

            await file.MoveAsync(destinationFolder);
        }

        public async Task<Stream> OpenFileAsync(string path, StorageFileMode mode, StorageFileAccess access)
        {
            var folderPath = Path.GetDirectoryName(path);
            var fileName = Path.GetFileName(path);

            var accessMode = FileAccessMode.ReadWrite;

            switch (access)
            {
                case StorageFileAccess.Read:
                    accessMode = FileAccessMode.Read;
                    break;
                case StorageFileAccess.ReadWrite:
                    accessMode = FileAccessMode.ReadWrite;
                    break;
                case StorageFileAccess.Write:
                    accessMode = FileAccessMode.ReadWrite;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("access");
            }


            StorageFile file;

            switch (mode)
            {
                case StorageFileMode.Append:
                case StorageFileMode.Open:
                case StorageFileMode.OpenOrCreate:
                case StorageFileMode.Truncate:
                    file = await GetFileAsync(path);
                    break;
                case StorageFileMode.Create:
                case StorageFileMode.CreateNew:
                    {
                        var folder =  await CreateFolderPath(Path.GetDirectoryName(path));
                        //var folder = await GetFolderAsync(folderPath);
                        file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }

            if (file == null)
                return null;

            var stream = await file.OpenAsync(accessMode);
            _openedStreams.Add(stream.AsStream());
            return stream.AsStream();
        }

        public async Task RenameDirectoryAsync(string directoryPath, string newDirectoryName)
        {
            var directory = await GetFolderAsync(directoryPath);
            await directory.RenameAsync(newDirectoryName);
        }

        public async Task<PortableImage> LoadImageAsync(string pathToImage)
        {
            if (await FileExistsAsync(pathToImage))
            {
                try
                {
                    var bitmapImage = new BitmapImage();

                    var file = await GetFileAsync(pathToImage);
                    var stream = await file.OpenAsync(FileAccessMode.Read);
                    bitmapImage.SetSource(stream);

                    var writeableBitmap = new WriteableBitmap(bitmapImage.PixelWidth, bitmapImage.PixelHeight);
                    stream.Seek(0);
                    writeableBitmap.SetSource(stream);
                    var portableImage = new PortableImage(writeableBitmap);
                    // TODO: Dispose Stream?
                    return portableImage;

                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public async Task<PortableImage> LoadImageThumbnailAsync(string pathToImage)
        {
            pathToImage = pathToImage.Replace("\\", "/");

            PortableImage retVal = null;
            var withoutExtension = Path.GetFileNameWithoutExtension(pathToImage);
            var imageBasePath = Path.GetDirectoryName(pathToImage);

            if (imageBasePath != null)
            {
                var thumbnailPath = Path.Combine(imageBasePath, string.Format("{0}{1}",
                    withoutExtension, CatrobatContextBase.ImageThumbnailExtension));

                if (await FileExistsAsync(thumbnailPath))
                {
                    retVal = await LoadImageAsync(thumbnailPath);
                }
                else
                {
                    var fullSizePortableImage = await LoadImageAsync(pathToImage);

                    if (fullSizePortableImage != null)
                    {
                        var thumbnailImage = ServiceLocator.ImageResizeService.ResizeImage(fullSizePortableImage,
                            _imageThumbnailDefaultMaxWidthHeight);
                        retVal = thumbnailImage;

                        try
                        {
                            var bitmap = ((WriteableBitmap) thumbnailImage.ImageSource);
                            var resolution = 100;

                            var fileStream = await OpenFileAsync(thumbnailPath, StorageFileMode.Create, StorageFileAccess.Write);

                            var encoderId = BitmapEncoder.PngEncoderId;
                            var encoder = await  BitmapEncoder.CreateAsync(encoderId, fileStream.AsRandomAccessStream());
                            encoder.SetPixelData(BitmapPixelFormat.Bgra8,
                                                                 BitmapAlphaMode.Straight,
                                                                 (uint)bitmap.PixelWidth,
                                                                 (uint)bitmap.PixelHeight,
                                                                 resolution,
                                                                 resolution,
                                                                 bitmap.ToByteArray());
                            await encoder.FlushAsync();
                        }

                        catch
                        {
                            retVal = null;
                        }
                    }
                }
            }

            return retVal;
        }

        public async Task<PortableImage> CreateThumbnailAsync(PortableImage image)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteImageAsync(string pathToImage)
        {
            await DeleteFileAsync(pathToImage);
            await DeleteFileAsync(pathToImage + CatrobatContextBase.ImageThumbnailExtension);
        }

        public async Task SaveImageAsync(string path, PortableImage image, bool deleteExisting, ImageFormat format)
        {
            var withoutExtension = Path.GetFileNameWithoutExtension(path);
            var thumbnailPath = string.Format("{0}{1}", withoutExtension, CatrobatContextBase.ImageThumbnailExtension);

            if (deleteExisting)
            {
                if (await FileExistsAsync(path))
                    await DeleteFileAsync(path);

                if (await FileExistsAsync(thumbnailPath))
                    await DeleteFileAsync(thumbnailPath);
            }

            Stream stream = null;

            try
            {
                stream = await OpenFileAsync(path, StorageFileMode.CreateNew, StorageFileAccess.Write);

                switch (format)
                {
                    case ImageFormat.Png:
                        PNGWriter.WritePNG((WriteableBitmap)image.ImageSource, stream, 95);
                        break;
                    case ImageFormat.Jpg:
                        throw new NotImplementedException();
                        //((WriteableBitmap)image.ImageSource).SaveJpeg(stream, image.Width, image.Height, 0, 95);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("format");
                }
            }
            finally
            {
                if (stream != null)
                {
                    stream.Flush();
                    stream.Dispose();
                }
            }
        }

        public async Task<string> ReadTextFileAsync(string path)
        {
            var file = await GetFileAsync(path, false);

            if (file == null)
                return null;

            return await FileIO.ReadTextAsync(file);
        }

        public async Task WriteTextFileAsync(string path, string content)
        {
            var file = await GetFileAsync(path, true);
            await FileIO.WriteTextAsync(file, content);
        }

        public async Task<object> ReadSerializableObjectAsync(string path, Type type)
        {
            using (var fileStream = await OpenFileAsync(path, StorageFileMode.Open, StorageFileAccess.Read))
            {
                var serializer = new DataContractSerializer(type);

                if (fileStream == null)
                    return null;

                var serializeableObject = serializer.ReadObject(fileStream);
                fileStream.Dispose();
                return serializeableObject;
            }
        }

        public async Task WriteSerializableObjectAsync(string path, object serializableObject)
        {
            using (var fileStream = await OpenFileAsync(path, StorageFileMode.Create, StorageFileAccess.Write))
            {
                var serializer = new DataContractSerializer(serializableObject.GetType());
                serializer.WriteObject(fileStream, serializableObject);
                fileStream.Dispose();
            }
        }


        #endregion

        #region Helpers

        public async Task<StorageFolder> CreateFolderPath(string path)
        {
            if (path == "")
                return ApplicationData.Current.LocalFolder;

            var subPath = Path.GetDirectoryName(path);

            var parentFolder = await CreateFolderPath(subPath);

            if (parentFolder == null)
                return null;

            var folderName = Path.GetFileName(path);

            try
            {
                var folder = await parentFolder.GetFolderAsync(folderName);
                return folder;
            }
            catch { }


            var folders = await parentFolder.GetFoldersAsync();

            foreach (var folder in folders)
            {
                
            }

            await parentFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            var newFolder = await parentFolder.GetFolderAsync(folderName);
            return newFolder;

            //var subPath = Path.GetDirectoryName(path);

            //if (subPath == "")
            //    return;

            //await CreateFolderPath(subPath);

            //var folder = await GetFolderAsync(subPath);
            //var fileName = Path.GetFileName(path);
            //if (folder != null)
            //{
            //    folder.CreateFolderAsync(fileName);
            //}

            //path = subPath;
        }

        public async Task<StorageFolder> GetFolderAsync(string path)
        {
            if (path == "")
                return ApplicationData.Current.LocalFolder;

            var subPath = Path.GetDirectoryName(path);

            var parentFolder = await GetFolderAsync(subPath);

            if (parentFolder == null)
                return null;

            var folderName = Path.GetFileName(path);

            try
            {
                var folder = await parentFolder.GetFolderAsync(folderName);
                return folder;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<StorageFile> GetFileAsync(string path, bool createIfNotExists = true)
        {
            var fileName = Path.GetFileName(path);
            var directoryName = Path.GetDirectoryName(path);

            if (createIfNotExists)
                await CreateFolderPath(directoryName);

            var folder = await GetFolderAsync(directoryName);

            if (folder == null)
                    return null;

            if (createIfNotExists)
                return await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);

            return await folder.GetFileAsync(fileName);
        }


        #endregion
    }
}