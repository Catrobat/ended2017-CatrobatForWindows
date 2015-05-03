using Catrobat.Paint.WindowsPhone.Controls.UserControls;
using Catrobat.Paint.WindowsPhone.Data;
using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    class StampTool : ToolBase
    {
        public StampTool()
        {
            this.ToolType = ToolType.Stamp;
             
        }

        public override void HandleDown(object arg)
        {
            
        }

        public override void HandleMove(object arg)
        {
            
        }

        public override void HandleUp(object arg)
        
        {
        }

        public override void Draw(object o)
        {
            
        }

        async public void stampCopy()
        {
            double heightStampControl = PocketPaintApplication.GetInstance().StampControl.GetHeightOfRectangleStampSelection();
            double widthStampControl = PocketPaintApplication.GetInstance().StampControl.GetWidthOfRectangleStampSelection();

            PocketPaintApplication.GetInstance().StampControl.setOriginalSizeOfStampedImage(heightStampControl, widthStampControl);

            Point leftTopPointStampSelection = PocketPaintApplication.GetInstance().StampControl.GetLeftTopPointOfStampedSelection();
            double xOffsetStampControl = leftTopPointStampSelection.X;
            double yOffsetStampControl = leftTopPointStampSelection.Y;

            string filename = "stamp" + ".png";
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

                    encoder.BitmapTransform.ScaledHeight = (uint)PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderSize.Height;
                    encoder.BitmapTransform.ScaledWidth = (uint)PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderSize.Width;

                    BitmapBounds bounds = new BitmapBounds();
                    bounds.Height = (uint)heightStampControl;
                    bounds.Width = (uint)widthStampControl;
                    bounds.X = (uint)(xOffsetStampControl);
                    bounds.Y = (uint)(yOffsetStampControl);
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
                }
                //render the stream to the screen
                WriteableBitmap wbCroppedBitmap = new WriteableBitmap((int)widthStampControl, (int)heightStampControl);
                wbCroppedBitmap.SetSource(mrAccessStream);

                PocketPaintApplication.GetInstance().StampControl.setSourceImageStamp(wbCroppedBitmap);
            }
        }

        public void stampClear()
        {
            PocketPaintApplication.GetInstance().StampControl.setSourceImageStamp(null);
        }

        public async void stampPaste()
        {

            //Image img = new Image();
            //img.Source = PocketPaintApplication.GetInstance().StampControl.getImageSourceStampedImage();
            //RenderTargetBitmap retarbi = new RenderTargetBitmap();
            //await retarbi.RenderAsync(img);

            //Windows.Storage.Streams.IBuffer buffer = await(retarbi.GetPixelsAsync());
            //var pixels = await retarbi.GetPixelsAsync();
            //StorageFile outputFile = await KnownFolders.PicturesLibrary.CreateFileAsync("karlikoch.jpg", CreationCollisionOption.ReplaceExisting);
            //using (var writeStream = await outputFile.OpenAsync(FileAccessMode.ReadWrite))
            //{
            //    var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, writeStream);
            //    encoder.SetPixelData(BitmapPixelFormat.Bgra8,
            //        BitmapAlphaMode.Straight,
            //        (uint)retarbi.PixelWidth,
            //        (uint)retarbi.PixelHeight,
            //        DisplayInformation.GetForCurrentView().LogicalDpi,
            //        DisplayInformation.GetForCurrentView().LogicalDpi,
            //        pixels.ToArray());
            //    await encoder.FlushAsync();

            //}

            double heightStampControl = PocketPaintApplication.GetInstance().StampControl.GetHeightOfRectangleStampSelection();
            double widthStampControl = PocketPaintApplication.GetInstance().StampControl.GetWidthOfRectangleStampSelection();

            Point leftTopPointStampSelection = PocketPaintApplication.GetInstance().StampControl.GetLeftTopPointOfStampedSelection();
            double xOffsetStampControl = leftTopPointStampSelection.X + 5.0;
            double yOffsetStampControl = leftTopPointStampSelection.Y + 5.0;

            Image img = new Image();
            img.Source = PocketPaintApplication.GetInstance().StampControl.getImageSourceStampedImage();
            WriteableBitmap originalWb = (WriteableBitmap)img.Source;
            img.Height = heightStampControl -10.0;
            img.Width = widthStampControl - 10.0;
            img.Stretch = Stretch.Fill;
            //Task<WriteableBitmap> wb = ResizeImage(originalWb, (uint)PocketPaintApplication.GetInstance().StampControl.getOriginalWidthOfStampedImage(),
            //    (uint)PocketPaintApplication.GetInstance().StampControl.getOriginalHeightOfStampedImage());
            Canvas.SetTop(img, yOffsetStampControl);
            Canvas.SetLeft(img, xOffsetStampControl);
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(img);
        }

        public override void ResetDrawingSpace()
        {
            PocketPaintApplication.GetInstance().StampControl.SetStampSelection();
        }
    }
}
