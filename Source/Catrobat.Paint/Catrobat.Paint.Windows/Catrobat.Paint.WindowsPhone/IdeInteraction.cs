using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace Catrobat.Paint.WindowsPhone
{
    public static class IdeInteraction
    {
        public const string _localFileName = "image.catrobat_ide_png";

        public static async Task GotPictureFromIde(IStorageFile file)
        {
            BitmapImage bitmapImage = new BitmapImage();

            using (IRandomAccessStream fileStream = await file.OpenReadAsync())
            {
                await bitmapImage.SetSourceAsync(fileStream);
            }

            WriteableBitmap writableBitmap = new WriteableBitmap(bitmapImage.PixelWidth, bitmapImage.PixelHeight);

            using (IRandomAccessStream fileStream = await file.OpenReadAsync())
            {
                await writableBitmap.SetSourceAsync(fileStream);
            }

            // TODO: edit "writableBitmap" and send it back to 
            // TODO: the IDE by using the following line
            await SendPictureToIde(writableBitmap);

            // TODO: Maybe call the following line to exit the 
            // TODO: Paint app after the file was sent back to the IDE
            // Application.Current.Exit();
        }

        public static async Task SendPictureToIde(WriteableBitmap image)
        {
            var imageWidth = image.PixelWidth;
            var imageHeight = image.PixelHeight;

            StorageFolder tempFolder = await ApplicationData.Current.TemporaryFolder.CreateFolderAsync("Temp", CreationCollisionOption.OpenIfExists);
            StorageFile localFile = await tempFolder.CreateFileAsync(_localFileName, CreationCollisionOption.ReplaceExisting);
            using (IRandomAccessStream localFileStream = await localFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, localFileStream);
                var pixels = image.PixelBuffer.ToArray();

                encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight,
                          (uint)imageWidth,
                          (uint)imageHeight,
                          96,
                          96,
                          pixels);
 
                await encoder.FlushAsync();
            }

            var options = new Windows.System.LauncherOptions { DisplayApplicationPicker = false };

            await Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.High, async () =>
            {
                bool success = await Windows.System.Launcher.LaunchFileAsync(localFile, options);
                if (success)
                {
                    // File launch succeded
                }
                else
                {
                    // File launch failed
                }
            });
        }
    }
}
