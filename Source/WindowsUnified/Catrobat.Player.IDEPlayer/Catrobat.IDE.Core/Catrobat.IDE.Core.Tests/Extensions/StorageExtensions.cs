using System;
using System.Collections.Generic;
using System.IO;
using Catrobat.IDE.Core.Services.Storage;
using System.Runtime.Serialization;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.Tests.Extensions
{
    public static class StorageExtensions
    {
        private static readonly List<Stream> _openedStreams = new List<Stream>();

        #region Synchron

        #region File manipulation

        public static string[] GetFileNames(this IStorage storage, string path)
        {
            path = FormatPath(path);

            var filePaths = Directory.GetFiles(storage.BasePath + path);

            var fileNames = new List<string>();

            foreach (string filePath in filePaths)
                fileNames.Add(GetName(filePath));

            return fileNames.ToArray();
        }

        public static bool FileExists(this IStorage storage, string path)
        {
            if (File.Exists(storage.BasePath + path))
                return true;
            else
                return false;
        }

        //Stream IStorage.OpenFile(this IStorage storage, string path, StorageFileMode mode, StorageFileAccess access)
        //{
        //    return OpenFile(storage, path, mode, access);
        //}

        public static Stream OpenFile(this IStorage storage, string path, StorageFileMode mode, StorageFileAccess access)
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
                            CreateDirectory(storage, GetPath(path));
                            fileStream = File.Create(storage.BasePath + path);
                        }
                        break;
                    case StorageFileMode.CreateNew:
                        {
                            CreateDirectory(storage, GetPath(path));
                            if (!File.Exists(storage.BasePath + path))
                            {
                                fileStream = File.Create(storage.BasePath + path);
                            }
                            else
                                throw new IOException("A file with the same name already exists");
                        }
                        break;
                    case StorageFileMode.OpenOrCreate:
                        {
                            CreateDirectory(storage, GetPath(path));
                            if (!File.Exists(storage.BasePath + path))
                            {
                                fileStream = File.Create(storage.BasePath + path);
                            }
                        }
                        break;
                }


            if (fileStream == null)
                fileStream = File.Open(storage.BasePath + path, fileMode, fileAccess);

            _openedStreams.Add(fileStream);

            return fileStream;
        }

        public static void DeleteFile(this IStorage storage, string path)
        {
            if (File.Exists(storage.BasePath + path))
                File.Delete(storage.BasePath + path);
        }

        public static void MoveFile(this IStorage storage, string sourcePath, string destinationPath)
        {
            throw new NotImplementedException();
        }

        public static void CopyFile(this IStorage storage, string sourcePath, string destinationPath)
        {
            if (File.Exists(storage.BasePath + sourcePath))
            {
                string content = File.ReadAllText(storage.BasePath + sourcePath, System.Text.Encoding.UTF8);

                if (!Directory.Exists(storage.BasePath + GetPath(destinationPath)))
                    Directory.CreateDirectory(storage.BasePath + GetPath(destinationPath));

                File.WriteAllText(storage.BasePath + destinationPath, content, System.Text.Encoding.UTF8);
            }
        }

        #endregion

        #region Directory manipulation

        public static string[] GetDirectoryNames(this IStorage storage, string path)
        {
            path = FormatPath(path);

            var directoryPaths = Directory.GetDirectories(storage.BasePath + path);

            var directoryNames = new List<string>();

            foreach (string directoryPath in directoryPaths)
                directoryNames.Add(GetName(directoryPath));

            return directoryNames.ToArray();
        }

        public static bool DirectoryExists(this IStorage storage, string path)
        {
            if (Directory.Exists(storage.BasePath + path))
                return true;
            else
                return false;
        }

        public static void CreateDirectory(this IStorage storage, string path)
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
                if (!Directory.Exists(Path.Combine(storage.BasePath, parentPath)))
                    Directory.CreateDirectory(Path.Combine(storage.BasePath, parentPath));
            }
        }

        public static void DeleteDirectory(this IStorage storage, string path)
        {
            if (Directory.Exists(storage.BasePath + path))
                Directory.Delete(storage.BasePath + path, true);
        }

        public static void MoveDirectory(this IStorage storage, string sourcePath, string destinationPath)
        {
            throw new NotImplementedException();
        }

        public static void CopyDirectory(this IStorage storage, string sourcePath, string destinationPath)
        {
            if (Directory.Exists(storage.BasePath + sourcePath))
            {
                Directory.CreateDirectory(storage.BasePath + destinationPath);

                foreach (string directory in Directory.GetDirectories(storage.BasePath + sourcePath))
                {
                    string directoryName = GetName(directory);
                    CopyDirectory(storage, sourcePath + "/" + directoryName, destinationPath + "/" + directoryName);
                }

                foreach (string file in Directory.GetFiles(storage.BasePath + sourcePath))
                {
                    string fileName = GetName(file);
                    CopyFile(storage, sourcePath + "/" + fileName, destinationPath + "/" + fileName);
                }
            }
        }

        public static void RenameDirectory(this IStorage storage,string directoryPath, string newDirectoryName)
        {
            if (Directory.Exists(storage.BasePath + directoryPath))
                Directory.Move(storage.BasePath + directoryPath, storage.BasePath + GetPath(directoryPath) + newDirectoryName);
        }

        #endregion

        #region Specialized reading and writing

        public static object ReadSerializableObject(this IStorage storage, string path, Type type)
        {
            using (Stream fileStream = OpenFile(storage, path, StorageFileMode.Open, StorageFileAccess.Read))
            {
                DataContractSerializer serializer = new DataContractSerializer(type);
                object serialireableObject = serializer.ReadObject(fileStream); // TODO: does not work any more
                fileStream.Close();
                return serialireableObject;
            }
        }

        public static void WriteSerializableObject(this IStorage storage, string path, object serializableObject)
        {
            using (Stream fileStream = OpenFile(storage, path, StorageFileMode.Create, StorageFileAccess.Write))
            {
                DataContractSerializer serializer = new DataContractSerializer(serializableObject.GetType());
                serializer.WriteObject(fileStream, serializableObject);
                fileStream.Close();
            }
        }

        public static string ReadTextFile(this IStorage storage, string path)
        {
            if (File.Exists(storage.BasePath + path))
                return File.ReadAllText(storage.BasePath + path, System.Text.Encoding.UTF8);
            else
                return null;
        }

        public static void WriteTextFile(this IStorage storage, string path, string content)
        {
            if (!Directory.Exists(storage.BasePath + GetPath(path)))
                Directory.CreateDirectory(storage.BasePath + GetPath(path));

            File.WriteAllText(storage.BasePath + path, content, System.Text.Encoding.UTF8);
        }

        #endregion

        #region Image reading and writing

        public static PortableImage LoadImage(this IStorage storage, string pathToImage)
        {
            return new PortableImage();

            //if (File.Exists(storage.BasePath + pathToImage))
            //  return File.ReadAllBytes(storage.BasePath + pathToImage);
            //else
            //  return null;
        }

        public static async void SaveImage(this IStorage storage, string path, PortableImage image, bool deleteExisting, ImageFormat format)
        {
            if (deleteExisting && File.Exists(storage.BasePath + path))
                File.Delete(storage.BasePath + path);

            switch (format)
            {
                case ImageFormat.Jpg:
                    await image.WriteAsJpg(storage.BasePath + path);
                    break;
                case ImageFormat.Png:
                    await image.WriteAsPng(storage.BasePath + path);
                    break;
            }
        }

        public static void DeleteImage(this IStorage storage, string pathToImage)
        {
            DeleteFile(storage, pathToImage);
            DeleteFile(storage, pathToImage + StorageConstants.ImageThumbnailExtension);
        }

        public static PortableImage LoadImageThumbnail(this IStorage storage, string pathToImage)
        {
            throw new NotImplementedException();
        }

        public static void TryCreateThumbnail(this IStorage storage, string file)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Syncron Helpers

        private static string GetName(string path)
        {
            int startSlash = path.LastIndexOf("/", System.StringComparison.Ordinal) + 1;
            int startBackSlash = path.LastIndexOf("\\", System.StringComparison.Ordinal) + 1;

            if (startSlash > startBackSlash)
                return path.Substring(startSlash);
            else
                return path.Substring(startBackSlash);
        }

        private static string GetPath(string path)
        {
            return path.Substring(0, path.Length - GetName(path).Length);
        }

        private static string FormatPath(string pathToFormat)
        {
            return pathToFormat.Replace('\\', '/');
        }

        #endregion

        #endregion
    }
}
