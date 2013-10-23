using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Store.Services.Storage
{
    public class StorageStore : IStorage
    {
        private static int _imageThumbnailDefaultMaxWidthHeight = 400;
        private readonly List<Stream> _openedStreams = new List<Stream>();

        public async void CreateDirectory(string path)
        {
            await CreateFolderPath(path);
            var parent = Path.GetPathRoot(path);
            var folder = await StorageFolder.GetFolderFromPathAsync(parent);
            await folder.CreateFolderAsync(path);
        }

        public bool DirectoryExists(string path)
        {
            //var folder = await StorageFolder.GetFolderFromPathAsync(path);
            //await folder.CreateFolderAsync(path);
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

        public PortableImage LoadImage(string pathToImage)
        {
            throw new NotImplementedException();
        }

        public PortableImage LoadImageThumbnail(string pathToImage)
        {
            throw new NotImplementedException();
        }

        public PortableImage CreateThumbnail(PortableImage image)
        {
            throw new NotImplementedException();
        }

        public void DeleteImage(string pathToImage)
        {
            throw new NotImplementedException();
        }

        public void SaveImage(string path, PortableImage image, bool deleteExisting, ImageFormat format)
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

        internal void SetImageMaxThumbnailWidthHeight(int maxWidthHeight)
        {
            _imageThumbnailDefaultMaxWidthHeight = maxWidthHeight;
        }

        private async Task CreateFolderPath(string path)
        {
            while (true)
            {
                var subPath = Path.GetPathRoot(path);

                if (subPath == null)
                    return;

                await CreateFolderPath(subPath);

                if (!DirectoryExists(subPath))
                {
                    var f = await StorageFolder.GetFolderFromPathAsync(subPath);
                    if (f != null)
                    {
                        f.CreateFolderAsync(path);
                    }
                }
            }
        }
    }
}