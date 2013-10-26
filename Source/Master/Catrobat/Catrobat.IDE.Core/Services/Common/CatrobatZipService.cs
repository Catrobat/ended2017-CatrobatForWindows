using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Services.Storage;
using SharpCompress.Archive.Zip;
using SharpCompress.Common;
using SharpCompress.Reader;

namespace Catrobat.IDE.Core.Services.Common
{
    public static class CatrobatZipService
    {
        public static async Task UnzipCatrobatPackageIntoIsolatedStorage(Stream zipStream, string localStoragePath)
        {
            if (zipStream != null)
            {
                var reader = ReaderFactory.Open(zipStream);

                using (var storage = StorageSystem.GetStorage())
                {
                    while (reader.MoveToNextEntry())
                    {
                        var absolutPath = Path.Combine(localStoragePath, reader.Entry.FilePath);
                        absolutPath = absolutPath.Replace("\\", "/");

                        if (!reader.Entry.IsDirectory)
                        {
                            if (await storage.FileExistsAsync(absolutPath))
                            {
                                await storage.DeleteFileAsync(absolutPath);
                            }

                            var fileStream = await storage.OpenFileAsync(absolutPath,
                                                              StorageFileMode.Create,
                                                              StorageFileAccess.Write);
                            reader.WriteEntryTo(fileStream);
                            fileStream.Dispose();
                        }
                    }
                }

                reader.Dispose();
            }
        }

        public static async Task ZipCatrobatPackage(Stream zipStream, string localStoragePath)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                using (var archive = ZipArchive.Create())
                {
                    await WriteFilesRecursiveToZip(archive, storage, localStoragePath, "");
                    archive.SaveTo(zipStream, CompressionType.None);
                }
            }
        }

        private static async Task WriteFilesRecursiveToZip(ZipArchive archive, IStorage storage, 
            string sourceBasePath, string destinationBasePath)
        {
            var searchPattern = sourceBasePath;
            var fileNames = await storage.GetFileNamesAsync(searchPattern);

            foreach (string fileName in fileNames)
            {
                if (fileName.EndsWith(CatrobatContextBase.ImageThumbnailExtension))
                    continue;

                var tempPath = Path.Combine(sourceBasePath, fileName);
                var fileStream = await storage.OpenFileAsync(tempPath, StorageFileMode.Open, StorageFileAccess.Read);
                var destinationPath = Path.Combine(destinationBasePath, fileName);
                archive.AddEntry(destinationPath, fileStream);
            }

            var directrryNames = await storage.GetDirectoryNamesAsync(searchPattern);
            foreach (string directoryName in directrryNames)
            {
                var tempZipPath = Path.Combine(sourceBasePath, directoryName);
                var nextDestinationBasePath = Path.Combine(destinationBasePath, directoryName);
                await WriteFilesRecursiveToZip(archive, storage, tempZipPath, nextDestinationBasePath);
            }
        }
    }
}