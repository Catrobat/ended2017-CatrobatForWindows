using Catrobat.Paint.WindowsPhone.Controls.UserControls;
using Catrobat.Paint.WindowsPhone.Data;
using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
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
            int height = PocketPaintApplication.GetInstance().CropControl.getRectangleCropSelectionHeight();
            int width = PocketPaintApplication.GetInstance().CropControl.getRectangleCropSelectionWidth();
            Point leftTopRectangleCropSelection = PocketPaintApplication.GetInstance().CropControl.getLeftTopCoordinateRectangleCropSelection();
            await CropImage(null, (int)leftTopRectangleCropSelection.X, (int)leftTopRectangleCropSelection.Y, width, height);
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

        //async private static Task<WriteableBitmap> CropImage(WriteableBitmap source,
        //                                                 int xOffset, int yOffset,
        //                                                 int width, int height)
        //{
        //    RenderTargetBitmap retarbi = new RenderTargetBitmap();
        //    await retarbi.RenderAsync(PocketPaintApplication.GetInstance().PaintingAreaCanvas,
        //                        (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth,
        //                        (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualHeight);



        //    RenderTargetBitmap destRetarbi = new RenderTargetBitmap();
        //    await destRetarbi.RenderAsync(PocketPaintApplication.GetInstance().PaintingAreaCanvas,
        //                        (int)width,
        //                        (int)height);
            
        //    Windows.Storage.Streams.IBuffer buffer = await(retarbi.GetPixelsAsync());
        //    Windows.Storage.Streams.IBuffer destBuffer = await(destRetarbi.GetPixelsAsync());
        //    var pixels = WindowsRuntimeBufferExtensions.ToArray(buffer);
        //    // Get the width of the source image
        //    var sourceWidth = retarbi.PixelWidth;

        //    // Get the resultant image as WriteableBitmap with specified size
        //    var result = new WriteableBitmap(width, height);
        //    byte[] destPixels = WindowsRuntimeBufferExtensions.ToArray(destBuffer);

        //    // Create the array of bytes
        //    for (var x = 0; x <= height - 1; x++)
        //    {
        //        var sourceIndex = xOffset + (yOffset + x) * sourceWidth;
        //        var destinationIndex = x * width;

        //        Array.Copy(pixels, sourceIndex, destPixels, destinationIndex, width);
        //    }

        //    StorageFile outputFile = await KnownFolders.PicturesLibrary.CreateFileAsync("david.png", CreationCollisionOption.ReplaceExisting);
        //    using (var writeStream = await outputFile.OpenAsync(FileAccessMode.ReadWrite))
        //    {
        //        var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, writeStream);
        //        encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, (uint)destRetarbi.PixelWidth, (uint)destRetarbi.PixelHeight, 96, 96, destPixels);
        //        await encoder.FlushAsync();

        //    }
        //    return result;
        //}

        //async private static Task<WriteableBitmap> CropImage(WriteableBitmap source,
        //                                            int xOffset, int yOffset,
        //                                            int width, int height)
        //{

        //    StorageFile StorageFile = await KnownFolders.CameraRoll.GetFileAsync("WP_20150208_009.jpg");
        //    var stream = await StorageFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
        //    BitmapDecoder bitmapDecoder =  await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(stream);

        //    BitmapTransform transform = new BitmapTransform();
        //    BitmapBounds bounds = new BitmapBounds();
        //    bounds.X = (uint)xOffset;
        //    bounds.Y = (uint)yOffset;
        //    bounds.Height = (uint)height;
        //    bounds.Width = (uint)width;
        //    transform.Bounds = bounds;

        //    // Get the cropped pixels within the bounds of transform. 
        //    PixelDataProvider pix = await bitmapDecoder.GetPixelDataAsync(
        //        BitmapPixelFormat.Bgra8,
        //        BitmapAlphaMode.Straight,
        //        transform,
        //        ExifOrientationMode.IgnoreExifOrientation,
        //        ColorManagementMode.ColorManageToSRgb);
        //    byte[] pixels = pix.DetachPixelData();

        //    // Stream the bytes into a WriteableBitmap 
        //    WriteableBitmap cropBmp = new WriteableBitmap((int)width, (int)height);
        //    Stream pixStream = cropBmp.PixelBuffer.AsStream();
        //    pixStream.Write(pixels, 0, (int)(width * height * 4));


        //    Windows.Storage.Streams.IBuffer buffer = cropBmp.PixelBuffer;
        //    var pixels_dest = WindowsRuntimeBufferExtensions.ToArray(buffer);

        //    StorageFile outputFile = await KnownFolders.CameraRoll.CreateFileAsync("karlimax.png", CreationCollisionOption.ReplaceExisting);
        //    using (var writeStream = await outputFile.OpenAsync(FileAccessMode.ReadWrite))
        //    {
        //        var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, writeStream);
        //        encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, (uint)cropBmp.PixelWidth, (uint)cropBmp.PixelHeight, 96, 96, pixels_dest);
        //        await encoder.FlushAsync();

        //    }


        //    PocketPaintApplication.GetInstance().ImageTest.Source = cropBmp;
        //    return cropBmp;
        //}
        async private static Task<WriteableBitmap> CropImage(WriteableBitmap source,
                                                    int xOffset, int yOffset,
                                                    int width, int height)
        {
            //StorageFile storageFile = await KnownFolders.CameraRoll.GetFileAsync("WP_20150208_009.jpg");


            string filename = ("karlidavidtest") + ".png";
            //IsBitmapStored = await PocketPaintApplication.GetInstance().StorageIo.WriteBitmapToPngMediaLibrary(bmp, filename);
            await PocketPaintApplication.GetInstance().StorageIo.WriteBitmapToPngMediaLibrary(filename);
            StorageFile storageFile = await KnownFolders.PicturesLibrary.GetFileAsync(filename);
            //StorageFile file = await Windows.Storage.StorageFile.GetFileFromPathAsync("C:\\Data\\Users\\Public\\Pictures\\Camera Roll\\WP_20150208_006.jpg");
            //Stream stream = await storageFile.OpenStreamForReadAsync();
            Stream stream = await storageFile.OpenStreamForReadAsync(); ;
            using (var memStream = new MemoryStream())
            {
                await stream.CopyToAsync(memStream);
                memStream.Position = 0;

                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(memStream.AsRandomAccessStream());

                // create a new stream and encoder for the new image
                InMemoryRandomAccessStream mrAccessStream = new InMemoryRandomAccessStream();
                BitmapEncoder encoder = await BitmapEncoder.CreateForTranscodingAsync(mrAccessStream, decoder);

                //// convert the bitmap to a 400px by 600px bitmap
                encoder.BitmapTransform.ScaledHeight = (uint)Window.Current.Bounds.Height;
                encoder.BitmapTransform.ScaledWidth = (uint)Window.Current.Bounds.Width;

                BitmapBounds bounds = new BitmapBounds();
                bounds.Height = (uint)height;
                bounds.Width = (uint)width;
                bounds.X = (uint)xOffset;
                bounds.Y = (uint)yOffset;
                encoder.BitmapTransform.Bounds = bounds;


                // write out to the stream
                try
                {
                    await encoder.FlushAsync();
                }
                catch (Exception ex)
                {
                    string s = ex.ToString();
                }

                // render the stream to the screen
                WriteableBitmap WB = new WriteableBitmap(width, height);
                WB.SetSource(mrAccessStream);

                Image image = new Image();
                image.Source = WB;
                image.Height = WB.PixelHeight;
                image.Width = WB.PixelWidth;
                //await WB.SetSourceAsync(await storageFile.OpenAsync(FileAccessMode.Read));

                //BitmapImage image = new BitmapImage();
                //image.SetSource(mrAccessStream);

                // Create file in Pictures library and write jpeg to it
                //var outputFile = await KnownFolders.CameraRoll.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                //using (var writeStream = await outputFile.OpenAsync(FileAccessMode.ReadWrite))
                //{
                //    await EncodeWriteableBitmap(WB, writeStream, BitmapEncoder.JpegEncoderId);
                //}
                // PocketPaintApplication.GetInstance().ImageTest.Source = WB;

                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(image);
                PocketPaintApplication.GetInstance().PaintingAreaView.setSizeOfPaintingAreaViewCheckered(WB.PixelHeight, WB.PixelWidth);
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Height = WB.PixelHeight;
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Width = WB.PixelWidth;
                PocketPaintApplication.GetInstance().CropControl.setCropSelection();
            }
            return null;
        }

        private static async Task EncodeWriteableBitmap(WriteableBitmap bmp, IRandomAccessStream writeStream, Guid encoderId)
        {
            // Copy buffer to pixels
            byte[] pixels;
            using (var stream = bmp.PixelBuffer.AsStream())
            {
                pixels = new byte[(uint)stream.Length];
                await stream.ReadAsync(pixels, 0, pixels.Length);
            }

            // Encode pixels into stream
            var encoder = await BitmapEncoder.CreateAsync(encoderId, writeStream);
            encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied,
               (uint)bmp.PixelWidth, (uint)bmp.PixelHeight,
               96, 96, pixels);
            await encoder.FlushAsync();
        }

        //async private static Task<WriteableBitmap> CropImage(WriteableBitmap source,
        //                                            int xOffset, int yOffset,
        //                                            int width, int height)
        //{
        //    double heightCanvas = PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderSize.Height;
        //    double widthCanvas = PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderSize.Width;

        //    RenderTargetBitmap sourceRetarbi = new RenderTargetBitmap();
        //    await sourceRetarbi.RenderAsync(PocketPaintApplication.GetInstance().PaintingAreaCanvas);
        //    var sourceBuffer = await sourceRetarbi.GetPixelsAsync();
        //    var sourceWidth = sourceRetarbi.PixelWidth;

        //    Windows.Storage.Streams.IBuffer buffer = await (sourceRetarbi.GetPixelsAsync());
        //    var pixelsSource = WindowsRuntimeBufferExtensions.ToArray(sourceBuffer);

        //    WriteableBitmap wb = new WriteableBitmap(width, height);
        //    byte[] pixelsDestination;
        //    using (var stream = wb.PixelBuffer.AsStream())
        //    {
        //        pixelsDestination = new byte[(uint)stream.Length];
        //    }
        //    //    // copy each row of pixels from the starting y location to (y + height) with the width of width
        //    for (var x = 0; x <= height - 1; x++)
        //    {
        //        var sourceIndex = xOffset + (yOffset + x) * sourceWidth;
        //        var destinationIndex = x * width;
        //        Array.Copy(pixelsSource, sourceIndex, pixelsDestination, destinationIndex, width);
        //    }
            
        //    // PocketPaintApplication.GetInstance().ImageTest.Source = retarbi;

        //    using (var memStream = new MemoryStream())
        //    {
        //        await pixelsDestination.AsBuffer().AsStream().CopyToAsync(memStream);
        //        memStream.Position = 0;

        //        BitmapDecoder decoder = await BitmapDecoder.CreateAsync(memStream.AsRandomAccessStream());

        //        // create a new stream and encoder for the new image
        //        InMemoryRandomAccessStream mrAccessStream = new InMemoryRandomAccessStream();
        //        BitmapEncoder encoder = await BitmapEncoder.CreateForTranscodingAsync(mrAccessStream, decoder);

        //        await encoder.FlushAsync();

        //        // render the stream to the screen
        //        WriteableBitmap WB = new WriteableBitmap(width, height);
        //        WB.SetSource(mrAccessStream);

        //        PocketPaintApplication.GetInstance().ImageTest.Source = WB;
        //    }
        //    return null;
        //}
    }
}
