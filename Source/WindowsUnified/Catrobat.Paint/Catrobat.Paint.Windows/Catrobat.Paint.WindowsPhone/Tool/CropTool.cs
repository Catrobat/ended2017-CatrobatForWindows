using Catrobat.Paint.WindowsPhone.Controls.UserControls;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    class CropTool : ToolBase
    {
        public CropTool()
        {
            this.ToolType = ToolType.Crop;
             
        }

        public override void HandleDown(object arg)
        {
            
        }

        public override void HandleMove(object arg)
        {
            
        }

        async public override void HandleUp(object arg)
        {
            await CropImage(null, 0, 0, 200, 200);
        }

        public override void Draw(object o)
        {
            
        }

        public override void ResetDrawingSpace()
        {
            PocketPaintApplication.GetInstance().CropControl.setCropSelection();
            PocketPaintApplication.GetInstance().CropControl.setIsModifiedRectangleMovement = false;
        }

        //async private Task<WriteableBitmap> CropImage(int x, int y)
        //{

        //    RenderTargetBitmap retarbi = new RenderTargetBitmap();
        //    await retarbi.RenderAsync(PocketPaintApplication.GetInstance().PaintingAreaCanvas,
        //                        (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth,
        //                        (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualHeight);
            
        //    Windows.Storage.Streams.IBuffer buffer = await(retarbi.GetPixelsAsync());
        //    var pixels = WindowsRuntimeBufferExtensions.ToArray(buffer);
        //    WriteableBitmap dest = new WriteableBitmap((int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth, (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualHeight);
        //    //byte[] destPixels = new byte[dest.PixelBuffer.Length];
        //    byte[] destPixels = new byte[pixels.Length];
        //    // range check
        //    if (x < 0 || y < 0 || (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth <= 0 || (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualHeight <= 0)
        //        return null;

        //    // create the bitmap
        //    //WriteableBitmap dest = new WriteableBitmap((int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth, (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualHeight);

        //    // calculate the starting offset
        //    int offset = (y * retarbi.PixelWidth) + x;

        //    // copy each row of pixels from the starting y location to (y + height) with the width of width
        //    for (int i = 0; i < (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualHeight; i++)
        //    {
        //        Array.Copy(pixels, offset, destPixels, i * (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth, (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth);
        //        offset += retarbi.PixelWidth;
        //    }

        //    StorageFile outputFile = await KnownFolders.PicturesLibrary.CreateFileAsync("david.png", CreationCollisionOption.ReplaceExisting);
        //    using (var writeStream = await outputFile.OpenAsync(FileAccessMode.ReadWrite))
        //    {
        //        var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, writeStream);
        //        encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, (uint)retarbi.PixelWidth, (uint)retarbi.PixelHeight, 96, 96, destPixels);
        //        await encoder.FlushAsync();

        //    }
        //    // return the crop image
        //    return dest;
        //}

        async private static Task<WriteableBitmap> CropImage(WriteableBitmap source,
                                                         int xOffset, int yOffset,
                                                         int width, int height)
        {
            RenderTargetBitmap retarbi = new RenderTargetBitmap();
            await retarbi.RenderAsync(PocketPaintApplication.GetInstance().PaintingAreaCanvas,
                                (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth,
                                (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualHeight);

            RenderTargetBitmap destRetarbi = new RenderTargetBitmap();
            await destRetarbi.RenderAsync(PocketPaintApplication.GetInstance().PaintingAreaCanvas,
                                (int)width,
                                (int)height);
            
            Windows.Storage.Streams.IBuffer buffer = await(retarbi.GetPixelsAsync());
            Windows.Storage.Streams.IBuffer destBuffer = await(destRetarbi.GetPixelsAsync());
            var pixels = WindowsRuntimeBufferExtensions.ToArray(buffer);
            // Get the width of the source image
            var sourceWidth = retarbi.PixelWidth;

            // Get the resultant image as WriteableBitmap with specified size
            var result = new WriteableBitmap(width, height);
            byte[] destPixels = WindowsRuntimeBufferExtensions.ToArray(destBuffer);

            // Create the array of bytes
            for (var x = 0; x <= height - 1; x++)
            {
                var sourceIndex = xOffset + (yOffset + x) * sourceWidth;
                var destinationIndex = x * width;

                Array.Copy(pixels, sourceIndex, destPixels, destinationIndex, width);
            }

            StorageFile outputFile = await KnownFolders.PicturesLibrary.CreateFileAsync("david.png", CreationCollisionOption.ReplaceExisting);
            using (var writeStream = await outputFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, writeStream);
                encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, (uint)destRetarbi.PixelWidth, (uint)destRetarbi.PixelHeight, 96, 96, destPixels);
                await encoder.FlushAsync();

            }
            return result;
        }
    }
}
