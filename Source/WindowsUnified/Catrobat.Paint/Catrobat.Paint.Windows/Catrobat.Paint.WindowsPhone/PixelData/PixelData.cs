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
using System.IO;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;

namespace Catrobat.Paint.WindowsPhone.PixelData
{
    class PixelData
    {
        private WriteableBitmap Bitmap;
        //private string filename;
        //private bool IsBitmapStored;
        //private BitmapDecoder Decoder;
        //private SolidColorBrush ColorBrush;
        private int X;
        private int Y;
        private byte[] pixelsCanvas;
        private byte[] pixelsCanvasEraser;
        private int pixelHeightCanvas;
        private int pixelWidthCanvas;

        public PixelData()
        {
            pixelsCanvas = null;
            pixelHeightCanvas = 0;
            pixelWidthCanvas = 0;
        }

        public async Task<SolidColorBrush> GetPixel(WriteableBitmap bitmap, int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Bitmap = bitmap;
          //  string filename = string.Empty;
          //  if ((filename = await SaveAsPng()) != null)
          //  {
          //      BitmapDecoder decoder = await ConvertFileToDecoder(filename);
          //      return await GetPixelData(decoder);
          //  }
          //  //    ConvertFileToDecoder();
          //  //    GetPixelData();
          return await GetPixelColor();
        }

        private async Task<SolidColorBrush> GetPixelColor()
        {       
            RenderTargetBitmap retarbi = new RenderTargetBitmap();
            await retarbi.RenderAsync(PocketPaintApplication.GetInstance().PaintingAreaCanvas,
                                (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth,
                                (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualHeight);

            Windows.Storage.Streams.IBuffer buffer = await (retarbi.GetPixelsAsync());
            var pixels = WindowsRuntimeBufferExtensions.ToArray(buffer);
            
            var width = retarbi.PixelWidth;
            var height = retarbi.PixelHeight;

            double NormfactorX = (double)width / (double)PocketPaintApplication.GetInstance().Bitmap.PixelWidth;
            double NormfactorY = (double)height / (double)PocketPaintApplication.GetInstance().Bitmap.PixelHeight;

            double doubleY = ((double)Y) * NormfactorY;
            double doubleX = ((double)X) * NormfactorX;

            int intX = (int)Math.Round(doubleX, 0);
            int intY = (int)Math.Round(doubleY, 0);

            int intTemp = intY * width;
            int intXTemp = intTemp + intX;
            int intValue = intXTemp * 4;
       
            var a = pixels[intValue + 3];
            var r = pixels[intValue + 2];
            var g = pixels[intValue + 1];
            var B = pixels[intValue];

            return new SolidColorBrush(Windows.UI.Color.FromArgb(a, r,g,B));
        }

        async public Task<int> preparePaintingAreaCanvasPixel()
        {
            RenderTargetBitmap retarbi = new RenderTargetBitmap();
            await retarbi.RenderAsync(PocketPaintApplication.GetInstance().PaintingAreaCanvas);

            Windows.Storage.Streams.IBuffer buffer = await(retarbi.GetPixelsAsync());
            pixelsCanvas = WindowsRuntimeBufferExtensions.ToArray(buffer);
            pixelHeightCanvas = retarbi.PixelHeight;
            pixelWidthCanvas = retarbi.PixelWidth;
            return 0;
        }

        async public Task<int> preparePaintingAreaCanvasForEraser()
        {
            RenderTargetBitmap retarbi = new RenderTargetBitmap();
            await retarbi.RenderAsync(PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying,
                (int)PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying.Width,
                (int)PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying.Height);

            Windows.Storage.Streams.IBuffer buffer = await (retarbi.GetPixelsAsync());
            pixelsCanvasEraser = WindowsRuntimeBufferExtensions.ToArray(buffer);
            pixelHeightCanvas = retarbi.PixelHeight;
            pixelWidthCanvas = retarbi.PixelWidth;
            return 0;
        }

        public SolidColorBrush getPixelFromCanvas(int x, int y)
        {
            double NormfactorX = (double)pixelWidthCanvas / (double)PocketPaintApplication.GetInstance().Bitmap.PixelWidth;
            double NormfactorY = (double)pixelHeightCanvas / (double)PocketPaintApplication.GetInstance().Bitmap.PixelHeight;

            double doubleY = ((double)y) * NormfactorY;
            double doubleX = ((double)x) * NormfactorX;

            int intX = (int)Math.Round(doubleX, 0);
            int intY = (int)Math.Round(doubleY, 0);

            int intTemp = intY * pixelWidthCanvas;
            int intXTemp = intTemp + intX;
            int intValue = intXTemp * 4;

            var a = pixelsCanvas[intValue + 3];
            var r = pixelsCanvas[intValue + 2];
            var g = pixelsCanvas[intValue + 1];
            var B = pixelsCanvas[intValue];

            return new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, B));
      
        }

        public Byte getPixelAlphaFromCanvas(int x, int y)
        {
            double NormfactorX = (double)pixelWidthCanvas / (double)PocketPaintApplication.GetInstance().PaintingAreaCanvas.Width;
            double NormfactorY = (double)pixelHeightCanvas / (double)PocketPaintApplication.GetInstance().PaintingAreaCanvas.Height;

            double doubleY = ((double)y) * NormfactorY;
            double doubleX = ((double)x) * NormfactorX;

            int intX = (int)Math.Round(doubleX, 0);
            int intY = (int)Math.Round(doubleY, 0);

            int intTemp = intY * pixelWidthCanvas;
            int intXTemp = intTemp + intX;
            int intValue = intXTemp * 4;

            return pixelsCanvas[intValue + 3];
        }

        public Byte getPixelAlphaFromCanvasEraser(int x, int y)
        {
            double NormfactorX = (double)pixelWidthCanvas / (double)PocketPaintApplication.GetInstance().Bitmap.PixelWidth;
            double NormfactorY = (double)pixelHeightCanvas / (double)PocketPaintApplication.GetInstance().Bitmap.PixelHeight;

            double doubleY = ((double)y) * NormfactorY;
            double doubleX = ((double)x) * NormfactorX;

            int intX = (int)Math.Round(doubleX, 0);
            int intY = (int)Math.Round(doubleY, 0);

            int intTemp = intY * pixelWidthCanvas;
            int intXTemp = intTemp + intX;
            int intValue = intXTemp * 4;

            return pixelsCanvasEraser[intValue + 3];
        }

        public void setPixelColor(int x, int y)
        {
            double NormfactorX = (double)pixelWidthCanvas / (double)PocketPaintApplication.GetInstance().Bitmap.PixelWidth;
            double NormfactorY = (double)pixelHeightCanvas / (double)PocketPaintApplication.GetInstance().Bitmap.PixelHeight;

            double doubleY = ((double)y) * NormfactorY;
            double doubleX = ((double)x) * NormfactorX;

            int intX = (int)Math.Round(doubleX, 0);
            int intY = (int)Math.Round(doubleY, 0);

            int intTemp = intY * pixelWidthCanvas;
            int intXTemp = intTemp + intX;
            int intValue = intXTemp * 4;

            pixelsCanvas[intValue + 3] = 0;
        }


        async public void changedPixelToCanvas()
        {
            StorageFile outputFile = await KnownFolders.PicturesLibrary.CreateFileAsync("myTestFile.png", CreationCollisionOption.ReplaceExisting);
            using (var writeStream = await outputFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, writeStream);
                encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, (uint)pixelWidthCanvas, (uint)pixelHeightCanvas, 96, 96, pixelsCanvas);
                await encoder.FlushAsync();
            }
                string filename = "myTestFile" + ".png";
                
                await PocketPaintApplication.GetInstance().StorageIo.WriteBitmapToPngMediaLibrary(filename);
                StorageFile storageFile = await KnownFolders.PicturesLibrary.GetFileAsync(filename);
                InMemoryRandomAccessStream mrAccessStream = new InMemoryRandomAccessStream();

                Stream stream = await storageFile.OpenStreamForReadAsync(); ;
                using (var memStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memStream);
                    memStream.Position = 0;

                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(memStream.AsRandomAccessStream());
                    BitmapEncoder encoder = await BitmapEncoder.CreateForTranscodingAsync(mrAccessStream, decoder);

                    // write out to the stream
                    try
                    {
                        await encoder.FlushAsync();
                    }
                    catch (Exception ex)
                    {
                        string s = ex.ToString();
                    }
                }
                //render the stream to the screen
                WriteableBitmap wbCroppedBitmap = new WriteableBitmap(
                    (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.Width,
                    (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.Height);
                wbCroppedBitmap.SetSource(mrAccessStream);

                Image image = new Image();
                image.Source = wbCroppedBitmap;
                image.Height = wbCroppedBitmap.PixelHeight;
                image.Width = wbCroppedBitmap.PixelWidth;
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(image);
        }

        //public Byte getPixelAlphaFromCanvas(int x, int y)
        //{
        //    double NormfactorX = (double)pixelWidthCanvas / (double)PocketPaintApplication.GetInstance().Bitmap.PixelWidth;
        //    double NormfactorY = (double)pixelHeightCanvas / (double)PocketPaintApplication.GetInstance().Bitmap.PixelHeight;

        //    double doubleY = ((double)y) * NormfactorY;
        //    double doubleX = ((double)x) * NormfactorX;

        //    int intX = (int)Math.Round(doubleX, 0);
        //    int intY = (int)Math.Round(doubleY, 0);

        //    int intTemp = intY * pixelWidthCanvas;
        //    int intXTemp = intTemp + intX;
        //    int intValue = intXTemp * 4;

        //    return pixelsCanvas[intValue + 3];
        //}


        private async Task<string> SaveAsPng()
        {
            string filename = DateTime.Now.ToString("d-M-yyyy_HH-mm-ss") + ".png";
            //IsBitmapStored = await PocketPaintApplication.GetInstance().StorageIo.WriteBitmapToPngMediaLibrary(bmp, filename);
            if (await PocketPaintApplication.GetInstance().StorageIo.WriteBitmapToPngMediaLibrary(filename))
                return filename;
            return null;
            
        }

        private async Task<BitmapDecoder> ConvertFileToDecoder(string filename)
        {
            try
            {
                StorageFile StorageFile =  await KnownFolders.PicturesLibrary.GetFileAsync(filename);
                var stream = await StorageFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
                return await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(stream);

            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        private async Task<SolidColorBrush> GetPixelData(BitmapDecoder decoder)
        {
            var pixelProvider = await decoder.GetPixelDataAsync(
                      Windows.Graphics.Imaging.BitmapPixelFormat.Rgba8,
                      Windows.Graphics.Imaging.BitmapAlphaMode.Straight,
                      new Windows.Graphics.Imaging.BitmapTransform(),
                      Windows.Graphics.Imaging.ExifOrientationMode.IgnoreExifOrientation,
                      Windows.Graphics.Imaging.ColorManagementMode.ColorManageToSRgb
                      );

            var pixels = pixelProvider.DetachPixelData();

            var width = decoder.OrientedPixelWidth;
            var height = decoder.OrientedPixelHeight;

            var temp = Y * width;
            var tempagain = temp + X;
            var value = tempagain * 4;
            return new SolidColorBrush(Windows.UI.Color.FromArgb(pixels[value + 3], pixels[value], pixels[value + 1], pixels[value + 2]));
        }

    }
}
