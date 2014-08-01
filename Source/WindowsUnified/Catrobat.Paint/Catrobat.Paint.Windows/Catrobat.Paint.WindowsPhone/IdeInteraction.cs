using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.Paint.Phone;

namespace Catrobat.Paint.WindowsPhone
{
    public static class IdeInteraction
    {
        public const string LocalFileName = "image.catrobat_ide_png";

        public static async Task GotPictureFromIde(IStorageFile file)
        {
            var fileStream = await file.OpenReadAsync();

            //var localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(
            //    LocalFileName, CreationCollisionOption.ReplaceExisting);
            //var localFileStream = await localFile.OpenAsync(FileAccessMode.ReadWrite);

            //await fileStream.AsStream().CopyToAsync(localFileStream.AsStream());
            //await localFileStream.FlushAsync();

            var bitmapImage = new BitmapImage();
            await bitmapImage.SetSourceAsync(fileStream);

            fileStream.Seek(0);
            var writableBitmap = new WriteableBitmap(
                bitmapImage.PixelWidth, bitmapImage.PixelHeight);
            bitmapImage.SetSourceAsync(fileStream);

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

            var localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                LocalFileName, CreationCollisionOption.ReplaceExisting);
            var localFileStream = await localFile.OpenAsync(FileAccessMode.ReadWrite);

            var encoder = await BitmapEncoder.CreateAsync(
              BitmapEncoder.PngEncoderId, localFileStream);

            var pixels = image.PixelBuffer.ToArray();

            encoder.SetPixelData(BitmapPixelFormat.Rgba8, BitmapAlphaMode.Straight,
                (uint)imageWidth, (uint)imageHeight, 96, 96, pixels);
            await encoder.FlushAsync();
            localFileStream.Dispose();

            var options = new Windows.System.LauncherOptions { DisplayApplicationPicker = false };

            Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.High, async () =>
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
