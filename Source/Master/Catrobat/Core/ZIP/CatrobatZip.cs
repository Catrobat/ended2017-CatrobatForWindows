using System.IO;
using Catrobat.Core.Storage;
using SharpCompress.Archive.Zip;
using SharpCompress.Common;
using SharpCompress.Reader;

namespace Catrobat.Core.ZIP
{
    public class CatrobatZip
    {
        public static void UnzipCatrobatPackageIntoIsolatedStorage(Stream zipStream, string localStoragePath)
        {
            IReader reader = ReaderFactory.Open(zipStream);

            using (IStorage storage = StorageSystem.GetStorage())
            {
                while (reader.MoveToNextEntry())
                {
                    string absolutPath = localStoragePath + "/" + reader.Entry.FilePath;

                    if (!reader.Entry.IsDirectory)
                    {
                        if (storage.FileExists(absolutPath))
                            storage.DeleteFile(absolutPath);

                        var writer = new StringWriter();

                        Stream fileStream = storage.OpenFile(absolutPath, StorageFileMode.Create,
                                                             StorageFileAccess.Write);
                        reader.WriteEntryTo(fileStream);
                        fileStream.Dispose();
                        //fileStream.Close(); // TODO: check if this is causing an error
                    }
                }
            }

            reader.Dispose();
        }


        public static void ZipCatrobatPackage(Stream zipStream, string localStoragePath)
        {
            using (IStorage storage = StorageSystem.GetStorage())
            {
                using (ZipArchive archive = ZipArchive.Create())
                {
                    WriteFilesRecursiveToZip(archive, storage, localStoragePath);
                    archive.SaveTo(zipStream, CompressionType.None);
                }
            }
        }

        private static void WriteFilesRecursiveToZip(ZipArchive archive, IStorage storage, string basePath)
        {
            string searchPattern = basePath;
            string[] fileNames = storage.GetFileNames(searchPattern);

            foreach (string fileName in fileNames)
            {
                Stream fileStream = storage.OpenFile(basePath + "/" + fileName, StorageFileMode.Open,
                                                     StorageFileAccess.Read);
                archive.AddEntry(fileName, fileStream);
            }

            string[] directrryNames = storage.GetDirectoryNames(searchPattern);
            foreach (string directoryName in directrryNames)
            {
                WriteFilesRecursiveToZip(archive, storage, basePath + "/" + directoryName);
            }
        }
    }
}