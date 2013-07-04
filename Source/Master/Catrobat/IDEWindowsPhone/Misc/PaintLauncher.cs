using System;
using System.Threading.Tasks;
using Catrobat.Core;
using Catrobat.Core.Storage;
using Windows.Storage;
using Windows.System;

namespace Catrobat.IDEWindowsPhone.Misc
{
    public class PaintLauncher
    {
        private static string _imageFolderPath;
        private static string _imageFileName;
        private const string DummyFileExtension = ".catrobatimage";

        public static async Task<bool> LaunchPaint(string pathToImage)
        {
            try
            {
                var splitPath = pathToImage.Split('/');
                _imageFileName = splitPath[splitPath.Length - 1];
                _imageFolderPath = "";

                for (var i = 0; i < splitPath.Length - 1; i++)
                {
                    if (_imageFolderPath != "")
                    {
                        _imageFolderPath += "/";
                    }

                    _imageFolderPath += splitPath[i];
                }

                using (var storage = StorageSystem.GetStorage())
                {
                    storage.DeleteDirectory(CatrobatContext.TempPaintImagePath);
                    storage.CopyFile(pathToImage, CatrobatContext.TempPaintImagePath + "/" + _imageFileName + DummyFileExtension);
                }

                var localFolder = ApplicationData.Current.LocalFolder;
                //var tempFolder = await localFolder.CreateFolderAsync(CatrobatContext.TempPaintImagePath.Split('/')[0], CreationCollisionOption.OpenIfExists);
                var tempImageFolder = await localFolder.CreateFolderAsync(CatrobatContext.TempPaintImagePath.Split('/')[1], CreationCollisionOption.ReplaceExisting);
                var imageFile = await tempImageFolder.CreateFileAsync(_imageFileName + DummyFileExtension, CreationCollisionOption.ReplaceExisting);
                await Launcher.LaunchFileAsync(imageFile);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool RecieveImageFromPaint()
        {
            try
            {
                var tempFilePath = CatrobatContext.TempPaintImagePath + "/" + _imageFileName + DummyFileExtension;
                var originalFilePath = _imageFolderPath + "/" + _imageFileName;

                using (var storage = StorageSystem.GetStorage())
                {
                    if (storage.FileExists(tempFilePath))
                    {
                        storage.FileExists(originalFilePath);
                        storage.DeleteFile(originalFilePath);

                        storage.CopyFile(tempFilePath, originalFilePath);
                        storage.DeleteDirectory(CatrobatContext.TempPaintImagePath);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}