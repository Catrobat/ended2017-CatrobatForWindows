using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
// TODO: using System.Windows.Media.Imaging;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.Graphics.Display;
// TODO: using ImageTools;
// TODO: using ImageTools.IO.Png;
// TODO: using Microsoft.Xna.Framework.Media;

namespace Catrobat.Paint.WindowsPhone.Data
{
    public class StorageIo
    {
        private readonly StorageFolder _local = ApplicationData.Current.LocalFolder;
        private const String Foldername = "PocketPaint";
        private StorageFolder _pocketPaintFolder;

        public StorageIo()
        {
            CreateFolder();

        }

        private async void CreateFolder()
        {

            _pocketPaintFolder = await _local.CreateFolderAsync(Foldername,
                CreationCollisionOption.OpenIfExists);
        }

        public async Task<bool> WriteFileIsolaredStorage(byte[] bytes, String filenameWithEnding)
        {

            var file = await _pocketPaintFolder.CreateFileAsync(filenameWithEnding,
            CreationCollisionOption.ReplaceExisting);

            if (file == null)
            {
                return false;
            }

            //todo und im Fehlerfall?
            using (var s = await file.OpenStreamForWriteAsync())
            {
                s.Write(bytes, 0, bytes.Length);

                if (s.Length != bytes.Length)
                {
                    return false;
                }

                // TODO: s.Close();
            }

            return true;
        }

        public async Task<bool> WriteBitmapToPngFileIsolatedStorage(WriteableBitmap bitmap, String filenameWithEnding)
        {

            //var file = await _pocketPaintFolder.CreateFileAsync(filenameWithEnding,
            //CreationCollisionOption.ReplaceExisting);

            //if (file == null)
            //{
            //    return false;
            //}

            // var stream = await file.OpenAsync(FileAccessMode.ReadWrite);

            //await bitmap.SetSourceAsync(stream);

            RenderTargetBitmap retarbi = new RenderTargetBitmap();
            await retarbi.RenderAsync(PocketPaintApplication.GetInstance().PaintingAreaCanvas,
                                (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth,
                                (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualHeight);


            Windows.Storage.Streams.IBuffer buffer = await (retarbi.GetPixelsAsync());
            var pixels = WindowsRuntimeBufferExtensions.ToArray(buffer);

            StorageFile outputFile = await KnownFolders.PicturesLibrary.CreateFileAsync("myTestFile.png", CreationCollisionOption.ReplaceExisting);
            using (var writeStream = await outputFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, writeStream);
                encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, (uint)retarbi.PixelWidth, (uint)retarbi.PixelHeight, 96, 96, pixels);
                await encoder.FlushAsync();

            }
            // TODO: var img = bitmap.ToImage();
            // TODO: var encoder = new PngEncoder();
            // using (var s = await file.OpenStreamForWriteAsync())
            // {
            //
            //     // TODO: encoder.Encode(img, s);
            //    // TODO s.Close();
            // }



            return true;
        }

        public async Task<bool> WriteBitmapToPngMediaLibrary(string filename)
        {
            //Canvas tempCanvas = PocketPaintApplication.GetInstance().PaintingAreaCanvas;
            //Size canvasSize = tempCanvas.RenderSize;
            //Point defaultPoint = tempCanvas.RenderTransformOrigin;

            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Measure(PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderSize);
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Arrange(new Rect(new Point(0, 0), PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderSize));

            RenderTargetBitmap retarbi = new RenderTargetBitmap();
            await retarbi.RenderAsync(PocketPaintApplication.GetInstance().PaintingAreaCanvas);

            Windows.Storage.Streams.IBuffer buffer = await (retarbi.GetPixelsAsync());
            var pixels = await retarbi.GetPixelsAsync();
            StorageFile outputFile = await KnownFolders.PicturesLibrary.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            using (var writeStream = await outputFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, writeStream);
                encoder.SetPixelData(BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Straight,
                    (uint)retarbi.PixelWidth,
                    (uint)retarbi.PixelHeight,
                    DisplayInformation.GetForCurrentView().LogicalDpi,
                    DisplayInformation.GetForCurrentView().LogicalDpi,
                    pixels.ToArray());
                await encoder.FlushAsync();

            }
            return true;
        }

        public async Task<WriteableBitmap> ReadPngToBitmapIsolatedStorage(string filenameWithEnding)
        {

            using (var s = await _pocketPaintFolder.OpenStreamForReadAsync(filenameWithEnding))
            {
                // TODO: var decoder = new PngDecoder();
                // TODO: var img = new ExtendedImage();
                // TODO: decoder.Decode(img, s);
                // TODO: return new WriteableBitmap(img.ToBitmap());
                return null;
            }
        }
    }
}
