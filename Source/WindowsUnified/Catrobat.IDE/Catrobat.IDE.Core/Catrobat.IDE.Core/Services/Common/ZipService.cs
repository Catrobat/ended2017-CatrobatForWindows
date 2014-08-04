//using SharpCompress.Archive.Zip;
//using SharpCompress.Common;
//using SharpCompress.Reader;

using System.Collections.Generic;
using Catrobat.IDE.Core.Services.Storage;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Catrobat.IDE.Core.Services.Common
{
    public class ZipService : IZipService
    {
        private static List<String> SkipedEndings
        {
            get
            {
                return new List<string>
                {
                    ".nomedia", "_thumb#"
                };
            }
        }

        public async Task UnzipCatrobatPackageIntoIsolatedStorage(Stream zipStream, string localStoragePath)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Read))
                {
                    foreach (var entry in archive.Entries)
                    {
                        using (var stream = entry.Open())
                        {
                            var skipFile = false;
                            foreach (var ending in SkipedEndings)
                                if (entry.Name.EndsWith(ending))
                                    skipFile = true;

                            if (skipFile)
                                continue;

                            var filePath = Path.Combine(localStoragePath, entry.FullName);

                            if (Path.GetFileName(filePath) == "") continue;

                            var outStream = await storage.OpenFileAsync(filePath,
                                StorageFileMode.CreateNew, StorageFileAccess.Write);

                            await stream.CopyToAsync(outStream);
                            outStream.Dispose();
                            stream.Dispose();
                        }
                    }
                }
            }
        }

        public async Task ZipCatrobatPackage(Stream zipStream, string localStoragePath)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var archive = new ZipArchive(zipStream, ZipArchiveMode.Create);
                await WriteFilesRecursiveToZip(archive, storage, localStoragePath, "");
                await zipStream.FlushAsync();
            }
        }

        private async Task WriteFilesRecursiveToZip(ZipArchive archive, IStorage storage,
            string sourceBasePath, string destinationBasePath)
        {
            var searchPattern = sourceBasePath;
            var fileNames = await storage.GetFileNamesAsync(searchPattern);

            foreach (string fileName in fileNames)
            {
                if (fileName.EndsWith(StorageConstants.ImageThumbnailExtension))
                    continue;

                var tempPath = Path.Combine(sourceBasePath, fileName);
                using (var fileStream = await storage.OpenFileAsync(tempPath,
                    StorageFileMode.Open, StorageFileAccess.Read))
                {
                    var destinationPath = Path.Combine(destinationBasePath, fileName);
                    var newEntry = archive.CreateEntry(destinationPath);
                    using (var stream = newEntry.Open())
                    {
                        await fileStream.CopyToAsync(stream);
                    }
                }
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