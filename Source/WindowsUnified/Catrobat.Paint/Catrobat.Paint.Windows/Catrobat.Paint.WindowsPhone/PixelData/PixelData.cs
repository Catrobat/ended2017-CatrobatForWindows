using Catrobat.Paint.WindowsPhone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Catrobat.Paint.WindowsPhone.PixelData
{
    class PixelData
    {
        private WriteableBitmap Bitmap;
        private string filename;
        private bool IsBitmapStored;
        private BitmapDecoder Decoder;
        private SolidColorBrush ColorBrush;
        private int X;
        private int Y;

        public PixelData()
        {

        }

        public async Task<SolidColorBrush> GetPixel(WriteableBitmap bitmap, int x, int y)
        {
            IsBitmapStored = false;

            this.X = x;
            this.Y = y;
            this.Bitmap = bitmap;


            GetPixelColor();

            //SaveAsPng(this.Bitmap);
            //    ConvertFileToDecoder();
            //    GetPixelData();
            
               return this.ColorBrush;
        }

        private async void GetPixelColor()
        {
           
            RenderTargetBitmap retarbi = new RenderTargetBitmap();
            await retarbi.RenderAsync(PocketPaintApplication.GetInstance().PaintingAreaCanvas,
                                (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth,
                                (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualHeight);


            Windows.Storage.Streams.IBuffer buffer = await (retarbi.GetPixelsAsync());
            var pixels = WindowsRuntimeBufferExtensions.ToArray(buffer);


            foreach (byte b in pixels)
            {
                int i = 0;
                if (b != 0)
                    i = 0;


            }


            var width = retarbi.PixelWidth;
            var height = retarbi.PixelHeight;

            var temp = Y * width;
            var tempagain = temp + X;
            var value = tempagain * 4;
            this.ColorBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(pixels[value + 3], pixels[value], pixels[value + 1], pixels[value + 2]));
         
        }

        private async void SaveAsPng(WriteableBitmap bmp)
        {
            filename = DateTime.Now.ToString("d-M-yyyy_HH-mm-ss") + ".png";
            //IsBitmapStored = await PocketPaintApplication.GetInstance().StorageIo.WriteBitmapToPngMediaLibrary(bmp, filename);
            IsBitmapStored = await PocketPaintApplication.GetInstance().StorageIo.WriteBitmapToPngFileIsolatedStorage(bmp, filename);
        }

        private async void ConvertFileToDecoder()
        {
            StorageFile StorageFile = await StorageFile.GetFileFromPathAsync("myTestFile.png");
            var stream = await StorageFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
            this.Decoder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(stream);
        }

        private async void GetPixelData()
        {
            var pixelProvider = await this.Decoder.GetPixelDataAsync(
                      Windows.Graphics.Imaging.BitmapPixelFormat.Rgba8,
                      Windows.Graphics.Imaging.BitmapAlphaMode.Straight,
                      new Windows.Graphics.Imaging.BitmapTransform(),
                      Windows.Graphics.Imaging.ExifOrientationMode.RespectExifOrientation,
                      Windows.Graphics.Imaging.ColorManagementMode.ColorManageToSRgb
                      );

            var pixels = pixelProvider.DetachPixelData();

            var width = Decoder.OrientedPixelWidth;
            var height = Decoder.OrientedPixelHeight;

            var temp = Y * width;
            var tempagain = temp + X;
            var value = tempagain * 4;
            this.ColorBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(pixels[value + 3], pixels[value], pixels[value + 1], pixels[value + 2]));
        }

    }
}
