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
            CropImage((int)leftTopRectangleCropSelection.X, (int)leftTopRectangleCropSelection.Y, width, height);
        }

        public override void Draw(object o)
        {
            
        }

        public override void ResetDrawingSpace()
        {
            PocketPaintApplication.GetInstance().CropControl.setCropSelection();
            PocketPaintApplication.GetInstance().CropControl.setIsModifiedRectangleMovement = false;
        }

        async public static void CropImage(int xOffset, int yOffset,
                                            int width, int height)
        {
            if (PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count != 0)
            {
                string filename = ("karlidavidtest") + ".png";
                await PocketPaintApplication.GetInstance().StorageIo.WriteBitmapToPngMediaLibrary(filename);
                StorageFile storageFile = await KnownFolders.PicturesLibrary.GetFileAsync(filename);
                InMemoryRandomAccessStream mrAccessStream = new InMemoryRandomAccessStream();

                using (Stream stream = await storageFile.OpenStreamForReadAsync())
                {
                    using (var memStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memStream);
                        memStream.Position = 0;

                        BitmapDecoder decoder = await BitmapDecoder.CreateAsync(memStream.AsRandomAccessStream());
                        BitmapEncoder encoder = await BitmapEncoder.CreateForTranscodingAsync(mrAccessStream, decoder);

                        encoder.BitmapTransform.ScaledHeight = (uint)PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderSize.Height;
                        encoder.BitmapTransform.ScaledWidth = (uint)PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderSize.Width;

                        uint canvasHeight = (uint)PocketPaintApplication.GetInstance().PaintingAreaCanvas.Height;
                        uint canvasWidth = (uint)PocketPaintApplication.GetInstance().PaintingAreaCanvas.Width;
                        BitmapBounds bounds = new BitmapBounds();
                        bounds.Height = (uint)height;
                        bounds.Width = (uint)width;
                        uint uwidth = (uint)width;
                        bounds.X = ((uint)width + (uint)xOffset) > canvasWidth ? canvasWidth - uwidth : (uint)xOffset;
                        uint uheight = (uint)height;
                        bounds.Y = ((uint)height + (uint)yOffset) > canvasHeight ? canvasHeight - uheight : (uint)yOffset;
                        encoder.BitmapTransform.Bounds = bounds;


                        // write out to the stream
                        try
                        {
                            await encoder.FlushAsync();
                        }
                        catch (Exception ex)
                        {
                            CropImage(xOffset, yOffset, width, height);
                        }
                    }
                    //render the stream to the screen
                    WriteableBitmap wbCroppedBitmap = new WriteableBitmap(width, height);
                    wbCroppedBitmap.SetSource(mrAccessStream);

                    Image image = new Image();
                    image.Source = wbCroppedBitmap;
                    image.Height = wbCroppedBitmap.PixelHeight;
                    image.Width = wbCroppedBitmap.PixelWidth;

                    PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();
                    PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(image);
                    PocketPaintApplication.GetInstance().PaintingAreaView.setSizeOfPaintingAreaViewCheckered(wbCroppedBitmap.PixelHeight, wbCroppedBitmap.PixelWidth);
                    PocketPaintApplication.GetInstance().PaintingAreaCanvas.Height = wbCroppedBitmap.PixelHeight;
                    PocketPaintApplication.GetInstance().PaintingAreaCanvas.Width = wbCroppedBitmap.PixelWidth;
                    PocketPaintApplication.GetInstance().CropControl.setCropSelection();

                    PocketPaintApplication.GetInstance().StampControl.setSourceImageStamp(wbCroppedBitmap);
                }
            }
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
    }
}
