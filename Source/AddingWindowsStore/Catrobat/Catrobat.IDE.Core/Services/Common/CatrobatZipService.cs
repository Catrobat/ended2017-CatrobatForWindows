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
            //TODO: Error message? Why is the stream every restart null?
        }

        public static void ZipCatrobatPackage(Stream zipStream, string localStoragePath)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                using (var archive = ZipArchive.Create())
                {
                    WriteFilesRecursiveToZip(archive, storage, localStoragePath, "");
                    archive.SaveTo(zipStream, CompressionType.None);
                }
            }
        }

        private static void WriteFilesRecursiveToZip(ZipArchive archive, IStorage storage, 
            string sourceBasePath, string destinationBasePath)
        {
            var searchPattern = sourceBasePath;
            var fileNames = storage.GetFileNames(searchPattern);

            foreach (string fileName in fileNames)
            {
                if (fileName.EndsWith(CatrobatContextBase.ImageThumbnailExtension))
                    continue;

                var tempPath = Path.Combine(sourceBasePath, fileName);
                var fileStream = storage.OpenFile(tempPath, StorageFileMode.Open, StorageFileAccess.Read);
                var destinationPath = Path.Combine(destinationBasePath, fileName);
                archive.AddEntry(destinationPath, fileStream);
            }

            var directrryNames = storage.GetDirectoryNames(searchPattern);
            foreach (string directoryName in directrryNames)
            {
                var tempZipPath = Path.Combine(sourceBasePath, directoryName);
                var nextDestinationBasePath = Path.Combine(destinationBasePath, directoryName);
                WriteFilesRecursiveToZip(archive, storage, tempZipPath, nextDestinationBasePath);
            }
        }
    }
}