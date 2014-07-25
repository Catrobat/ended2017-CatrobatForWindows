using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.Tests.Services.Storage
{
    public class StorageTest : IStorage
    {
        private readonly List<Stream> _openedStreams = new List<Stream>();

        public string BasePath
        {
            get
            {
                string basePath = Assembly.GetExecutingAssembly().CodeBase;
                int end = basePath.IndexOf(("Catrobat/"), System.StringComparison.Ordinal) + 9;
                basePath = basePath.Substring(8, end - 8) + "TestStorage/";

                return basePath;
            }
        }

        #region Synchron

        public void CreateDirectory(string path)
        {
            if (path == null)
                throw new IOException("Path can not be null.");

            if (path.EndsWith("/") || path.EndsWith("\\")) // Remove '/' at the end
                path = Path.GetDirectoryName(path);

            if (path.StartsWith("/") || path.StartsWith("\\")) // Remove '/' at the end
                path = path.Substring(1, path.Length - 1);

            var directoryPaths = new List<string>();

            var tempPath = path;
            while (!string.IsNullOrEmpty(tempPath))
            {
                directoryPaths.Add(tempPath);
                tempPath = Path.GetDirectoryName(tempPath);
            }

            for (int i = directoryPaths.Count - 1; i >= 0; i--)
            {
                var parentPath = directoryPaths[i];
                if (!Directory.Exists(Path.Combine(BasePath, parentPath)))
                    Directory.CreateDirectory(Path.Combine(BasePath, parentPath));
            }
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

            foreach (string filePath in filePaths)
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

                if (!Directory.Exists(BasePath + GetPath(destinationPath)))
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

            FileStream fileStream = null;

            if (access == StorageFileAccess.Write || access == StorageFileAccess.ReadWrite)
                switch (mode)
                {
                    case StorageFileMode.Create:
                        {
                            CreateDirectory(GetPath(path));
                            fileStream = File.Create(BasePath + path);
                        }
                        break;
                    case StorageFileMode.CreateNew:
                        {
                            CreateDirectory(GetPath(path));
                            if (!File.Exists(BasePath + path))
                            {
                                fileStream = File.Create(BasePath + path);
                            }
                            else
                                throw new IOException("A file with the same name already exists");
                        }
                        break;
                    case StorageFileMode.OpenOrCreate:
                        {
                            CreateDirectory(GetPath(path));
                            if (!File.Exists(BasePath + path))
                            {
                                fileStream = File.Create(BasePath + path);
                            }
                        }
                        break;
                }


            if (fileStream == null)
                fileStream = File.Open(BasePath + path, fileMode, fileAccess);

            _openedStreams.Add(fileStream);

            return fileStream;
        }

        public void RenameDirectory(string directoryPath, string newDirectoryName)
        {
            if (Directory.Exists(BasePath + directoryPath))
                Directory.Move(BasePath + directoryPath, BasePath + GetPath(directoryPath) + newDirectoryName);
        }

        public PortableImage LoadImage(string pathToImage)
        {
            return new PortableImage();

            //if (File.Exists(BasePath + pathToImage))
            //  return File.ReadAllBytes(BasePath + pathToImage);
            //else
            //  return null;
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

        public async void SaveImage(string path, PortableImage image, bool deleteExisting, ImageFormat format)
        {
            if (deleteExisting && File.Exists(BasePath + path))
                File.Delete(BasePath + path);

            switch (format)
            {
                case ImageFormat.Jpg:
                    await image.WriteAsJpg(BasePath + path);
                    break;
                case ImageFormat.Png:
                    await image.WriteAsPng(BasePath + path);
                    break;
            }
        }

        public void DeleteImage(string pathToImage)
        {
            DeleteFile(pathToImage);
            DeleteFile(pathToImage + CatrobatContextBase.ImageThumbnailExtension);
        }

        public void MoveDirectory(string sourcePath, string destinationPath)
        {
            throw new NotImplementedException();
        }

        public void MoveFile(string sourcePath, string destinationPath)
        {
            throw new NotImplementedException();
        }

        public PortableImage LoadImageThumbnail(string pathToImage)
        {
            return null;
        }
        #endregion


        #region Async

        public PortableImage CreateThumbnail(PortableImage image)
        {
            return new PortableImage();
        }


        public async Task CreateDirectoryAsync(string path)
        {
            await Task.Run(() => CreateDirectory(path));
        }

        public async Task<bool> DirectoryExistsAsync(string path)
        {
            return await Task.Run(() => DirectoryExists(path));
        }

        public async Task<bool> FileExistsAsync(string path)
        {
            return await Task.Run(() => FileExists(path));
        }

        public async Task<string[]> GetDirectoryNamesAsync(string path)
        {
            return await Task.Run(() => GetDirectoryNames(path));
        }

        public async Task<string[]> GetFileNamesAsync(string path)
        {
            return await Task.Run(() => GetFileNames(path));
        }

        public async Task DeleteDirectoryAsync(string path)
        {
            await Task.Run(() => DeleteDirectory(path));
        }

        public async Task DeleteFileAsync(string path)
        {
            await Task.Run(() => DeleteFile(path));
        }

        public async Task CopyDirectoryAsync(string sourcePath, string destinationPath)
        {
            await Task.Run(() => CopyDirectory(sourcePath, destinationPath));
        }

        public async Task MoveDirectoryAsync(string sourcePath, string destinationPath)
        {
            await Task.Run(() => MoveDirectory(sourcePath, destinationPath));
        }

        public async Task CopyFileAsync(string sourcePath, string destinationPath)
        {
            await Task.Run(() => CopyFile(sourcePath, destinationPath));
        }

        public async Task MoveFileAsync(string sourcePath, string destinationPath)
        {
            await Task.Run(() => MoveFile(sourcePath, destinationPath));
        }

        public async Task<Stream> OpenFileAsync(string path, StorageFileMode mode, StorageFileAccess access)
        {
            return await Task.Run(() => OpenFile(path, mode, access));
        }

        public async Task RenameDirectoryAsync(string directoryPath, string newDirectoryName)
        {
            await Task.Run(() => RenameDirectory(directoryPath, newDirectoryName));
        }

        public async Task<PortableImage> LoadImageAsync(string pathToImage)
        {
            return await Task.Run(() => LoadImage(pathToImage));
        }

        public async Task<PortableImage> LoadImageThumbnailAsync(string pathToImage)
        {
            return await Task.Run(() => LoadImageThumbnail(pathToImage));
        }

        public async Task<PortableImage> CreateThumbnailAsync(PortableImage image)
        {
            return await Task.Run(() => CreateThumbnail(image));
        }

        public async Task DeleteImageAsync(string pathToImage)
        {
            await Task.Run(() => DeleteImage(pathToImage));
        }

        public async Task SaveImageAsync(string path, PortableImage image, bool deleteExisting, ImageFormat format)
        {
            await Task.Run(() => SaveImage(path, image, deleteExisting, format));
        }

        public async Task<string> ReadTextFileAsync(string path)
        {
            return await Task.Run(() => ReadTextFile(path));
        }

        public async Task WriteTextFileAsync(string path, string content)
        {
            await Task.Run(() => WriteTextFile(path, content));
        }

        public async Task<object> ReadSerializableObjectAsync(string path, Type type)
        {
            return await Task.Run(() => ReadSerializableObject(path, type));
        }

        public async Task WriteSerializableObjectAsync(string path, object serializableObject)
        {
            await Task.Run(() => WriteSerializableObject(path, serializableObject));
        }

        #endregion
    }
}
