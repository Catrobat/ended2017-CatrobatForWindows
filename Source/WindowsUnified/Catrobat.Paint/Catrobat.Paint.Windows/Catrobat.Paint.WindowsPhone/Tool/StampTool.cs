using Catrobat.Paint.WindowsPhone.Command;
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

                    BitmapBounds bounds = new BitmapBounds();
                    bounds.Height = (uint)heightStampControl - 1;
                    bounds.Width = (uint)widthStampControl - 1;
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

        public void stampPaste()
        {
            double heightStampControl = PocketPaintApplication.GetInstance().StampControl.GetHeightOfRectangleStampSelection();
            double widthStampControl = PocketPaintApplication.GetInstance().StampControl.GetWidthOfRectangleStampSelection();

            Point leftTopPointStampSelection = PocketPaintApplication.GetInstance().StampControl.GetLeftTopPointOfStampedSelection();
            double xCoordinateOnWorkingSpace = leftTopPointStampSelection.X + 5.0;
            double yCoordinateOnWorkingSpace = leftTopPointStampSelection.Y + 5.0;

            Image stampedImage = new Image();
            stampedImage.Source = PocketPaintApplication.GetInstance().StampControl.getImageSourceStampedImage();
            WriteableBitmap originalWb = (WriteableBitmap)stampedImage.Source;
            stampedImage.Height = heightStampControl -10.0;
            stampedImage.Width = widthStampControl - 10.0;
            stampedImage.Stretch = Stretch.Fill;

            PocketPaintApplication.GetInstance().PaintingAreaView.addElementToPaintingAreCanvas(stampedImage, (int)(xCoordinateOnWorkingSpace + 5.0), (int)(yCoordinateOnWorkingSpace + 5.0));
            CommandManager.GetInstance().CommitCommand(new StampCommand((uint)xCoordinateOnWorkingSpace, (uint)yCoordinateOnWorkingSpace, stampedImage));
        }

        public void stampPaste(uint xCoordinateOnWorkingSpace, uint yCoordinateOnWorkingSpace, Image stampedImage)
        {
            Canvas.SetLeft(stampedImage, xCoordinateOnWorkingSpace);
            Canvas.SetTop(stampedImage, yCoordinateOnWorkingSpace);
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(stampedImage);
        }

        public override void ResetDrawingSpace()
        {
            PocketPaintApplication.GetInstance().StampControl.SetStampSelection();
            PocketPaintApplication.GetInstance().StampControl.resetCurrentCopiedSelection();
            PocketPaintApplication.GetInstance().PaintingAreaView.app_btnStampClear_Click(new object(), new RoutedEventArgs());
        }

        public override void ResetUsedElements()
        {
        }
    }
}
