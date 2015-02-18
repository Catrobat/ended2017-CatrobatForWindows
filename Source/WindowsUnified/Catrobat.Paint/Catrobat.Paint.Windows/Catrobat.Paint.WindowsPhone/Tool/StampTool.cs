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
            double heightStampControl = 200.0;
            double widthStampControl = 200.0;
            double xOffsetStampControl = 0.0;
            double yOffsetStampControl = 0.0;
            string filename = ("stamp") + ".png";
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
                    bounds.Height = (uint)heightStampControl;
                    bounds.Width = (uint)widthStampControl;
                    bounds.X = (uint)xOffsetStampControl;
                    bounds.Y = (uint)yOffsetStampControl;
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
            PocketPaintApplication.GetInstance().StampControl.setSourceImageStamp(new WriteableBitmap(200, 200));
        }

        public override void ResetDrawingSpace()
        {
            PocketPaintApplication.GetInstance().StampControl.setControlPosition();
        }
    }
}
