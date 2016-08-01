using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using System.IO;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI;

namespace Catrobat.Paint.WindowsPhone.PixelData
{
    public class PixelData
    {
        private WriteableBitmap Bitmap;
        public String ReturnString;
        private int X;
        private int Y;
        public byte[] pixelsCanvas;
        public byte[] pixelsCanvasEraser;
        public int pixelHeightCanvas;
        public int pixelWidthCanvas;

        #region Contructor
        public PixelData()
        {
            pixelsCanvas = null;
            pixelHeightCanvas = 0;
            pixelWidthCanvas = 0;
            ReturnString = string.Empty;
        }
        #endregion

        #region PipetteTool
        public async Task<SolidColorBrush> GetPixel(WriteableBitmap bitmap, int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Bitmap = bitmap;
            return await GetPixelColor();
        }

        private async Task<SolidColorBrush> GetPixelColor()
        {
            RenderTargetBitmap retarbi = new RenderTargetBitmap();
            var paintingCanvas = PocketPaintApplication.GetInstance().PaintingAreaCanvas;

            await retarbi.RenderAsync(paintingCanvas,
                                (int)paintingCanvas.ActualWidth,
                                (int)paintingCanvas.ActualHeight);

            Windows.Storage.Streams.IBuffer buffer = await (retarbi.GetPixelsAsync());
            var pixels = WindowsRuntimeBufferExtensions.ToArray(buffer);

            var width = retarbi.PixelWidth;
            var height = retarbi.PixelHeight;

            var bitmap = PocketPaintApplication.GetInstance().Bitmap;
            if (bitmap == null)
                return null;
            double NormfactorX = (double)width / (double)bitmap.PixelWidth;
            double NormfactorY = (double)height / (double)bitmap.PixelHeight;

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

            return new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, B));
        }
        #endregion

        #region FillTool
        public byte[] ConvertArray(byte[] pixles, int width, int height)
        {
            try
            {
                if (pixles != null)
                {
                    int pixelWidth = PocketPaintApplication.GetInstance().Bitmap.PixelWidth;
                    int pixelHeight = PocketPaintApplication.GetInstance().Bitmap.PixelHeight;

                    byte[] PixelsBitmap = new byte[pixelWidth * pixelHeight * 4];

                    double NormfactorX = pixelWidth / (double)width;
                    double NormfactorY = pixelHeight / (double)height;

                        for (int i = 0; i < height; i++)
                        {
                            for (int j = 0; j < width; j++)
                            {

                                double doubleY = i * NormfactorY;
                                double doubleX = j * NormfactorX;

                                int intX = (int)Math.Round(doubleX, 0);
                                int intY = (int)Math.Round(doubleY, 0);

                                int intTemp = intY * pixelWidth;
                                int intXTemp = intTemp + intX;
                                int intValue = intXTemp * 4;
                                if (intValue >= PixelsBitmap.Length)
                                    break;

                                int intTempCanvas = i * width;
                                int intXTempCanvas = intTempCanvas + j;
                                int intValueCanvas = intXTempCanvas * 4;

                                PixelsBitmap[intValue] = pixles[intValueCanvas];
                                PixelsBitmap[intValue + 1] = pixles[intValueCanvas + 1];
                                PixelsBitmap[intValue + 2] = pixles[intValueCanvas + 2];
                                PixelsBitmap[intValue + 3] = pixles[intValueCanvas + 3];

                            }
                        }
                    

                    return PixelsBitmap;
                }
                else
                    return null;

            }
            catch (Exception)
            {
                throw;
            }
        }

        async public Task<int> preparePaintingAreaCanvasPixel()
        {
            RenderTargetBitmap retarbi = new RenderTargetBitmap();
            Canvas canvas = PocketPaintApplication.GetInstance().PaintingAreaCanvas;
            await retarbi.RenderAsync(canvas);

            Windows.Storage.Streams.IBuffer buffer = await (retarbi.GetPixelsAsync());
            pixelsCanvas = WindowsRuntimeBufferExtensions.ToArray(buffer);

            this.pixelHeightCanvas = retarbi.PixelHeight;
            this.pixelWidthCanvas = retarbi.PixelWidth;
            return 0;
        }

        public bool FloodFill4(Point coordinate, SolidColorBrush color)
        {
            try
            {
                if (color.Color.A == 0)
                {
                    color = new SolidColorBrush(Color.FromArgb(0x00, 0x00, 0x00, 0x00));
                }

                if (PocketPaintApplication.GetInstance().Bitmap == null)
                {
                    PocketPaintApplication.GetInstance().SaveAsWriteableBitmapToRam();
                }

                string newColor = ColorToString(color);
                coordinate = ConvertCoordinates(coordinate);

                string oldColor = getPixelFromCanvas(coordinate);
                if (oldColor == newColor)
                    return false;

                Stack<Point> stackWithCoordinates = new Stack<Point>();
                stackWithCoordinates.Push(coordinate);
                while (stackWithCoordinates.Count > 0)
                {
                    var currentCoordinate = stackWithCoordinates.Pop();
                    SetPixel(currentCoordinate, newColor);

                    if (currentCoordinate.X > 1)
                    {
                        if (ComparePixelsColorForFloddFill(new Point(currentCoordinate.X - 1, currentCoordinate.Y), oldColor))
                            stackWithCoordinates.Push(new Point(currentCoordinate.X - 1, currentCoordinate.Y));
                    }

                    if (currentCoordinate.X < pixelWidthCanvas)
                    {
                        if (ComparePixelsColorForFloddFill(new Point(currentCoordinate.X + 1, currentCoordinate.Y), oldColor))
                            stackWithCoordinates.Push(new Point(currentCoordinate.X + 1, currentCoordinate.Y));
                    }

                    if (currentCoordinate.Y > 1)
                    {
                        if (ComparePixelsColorForFloddFill(new Point(currentCoordinate.X, currentCoordinate.Y - 1), oldColor))
                            stackWithCoordinates.Push(new Point(currentCoordinate.X, currentCoordinate.Y - 1));
                    }

                    if (currentCoordinate.Y < pixelHeightCanvas)
                    {
                        if (ComparePixelsColorForFloddFill(new Point(currentCoordinate.X, currentCoordinate.Y + 1), oldColor))
                            stackWithCoordinates.Push(new Point(currentCoordinate.X, currentCoordinate.Y + 1));
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool FloodFill8(Point p, SolidColorBrush color)
        {
            try
            {
                string newColor = ColorToString(color);
                if (PocketPaintApplication.GetInstance().Bitmap == null)
                {
                    PocketPaintApplication.GetInstance().SaveAsWriteableBitmapToRam();
                }
                p = ConvertCoordinates(p);
                string oldColor = getPixelFromCanvas(p);
                if (oldColor == newColor)
                    return false;

                Stack<Point> list = new Stack<Point>();
                list.Push(p);
                while (list.Count > 0)
                {
                    var p2 = list.Pop();
                    SetPixel(p2, newColor);
                    //left
                    if (p2.X > 1)
                    {
                        if (getPixelFromCanvas(new Point(p2.X - 1, p2.Y)) == oldColor)
                            list.Push(new Point(p2.X - 1, p2.Y));
                    }
                    //right
                    if (p2.X < pixelWidthCanvas)
                    {
                        if (getPixelFromCanvas(new Point(p2.X + 1, p2.Y)) == oldColor)
                            list.Push(new Point(p2.X + 1, p2.Y));
                    }
                    //above
                    if (p2.Y > 1)
                    {
                        if (getPixelFromCanvas(new Point(p2.X, p2.Y - 1)) == oldColor)
                            list.Push(new Point(p2.X, p2.Y - 1));
                    }
                    //below
                    if (p2.Y < pixelHeightCanvas)
                    {
                        if (getPixelFromCanvas(new Point(p2.X, p2.Y + 1)) == oldColor)
                            list.Push(new Point(p2.X, p2.Y + 1));
                    }
                    //left-above
                    if (p2.X > 1 && p2.Y > 1)
                    {
                        if (getPixelFromCanvas(new Point(p2.X - 1, p2.Y - 1)) == oldColor)
                            list.Push(new Point(p2.X - 1, p2.Y - 1));
                    }
                    //right-below
                    if (p2.X > 1 && p2.Y < pixelHeightCanvas)
                    {
                        if (getPixelFromCanvas(new Point(p2.X - 1, p2.Y + 1)) == oldColor)
                            list.Push(new Point(p2.X - 1, p2.Y + 1));
                    }
                    //right-above
                    if (p2.Y > 1 && p2.X < pixelWidthCanvas)
                    {
                        if (getPixelFromCanvas(new Point(p2.X + 1, p2.Y - 1)) == oldColor)
                            list.Push(new Point(p2.X + 1, p2.Y - 1));
                    }
                    //right-below
                    if (p2.X < pixelWidthCanvas && p2.Y < pixelHeightCanvas)
                    {
                        if (getPixelFromCanvas(new Point(p2.X + 1, p2.Y + 1)) == oldColor)
                            list.Push(new Point(p2.X + 1, p2.Y + 1));
                    }

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SetPixel(Point p, string color)
        {
            int intTemp;
            int intXTemp;
            int intValue;
            try
            {
                intTemp = ((int)p.Y - 1) * pixelWidthCanvas;
                intXTemp = intTemp + ((int)p.X - 1);
                intValue = intXTemp * 4;

                var argb = color.Split('_');

                pixelsCanvas[intValue + 3] = Convert.ToByte(argb[0]);
                pixelsCanvas[intValue + 2] = Convert.ToByte(argb[1]);
                pixelsCanvas[intValue + 1] = Convert.ToByte(argb[2]);
                pixelsCanvas[intValue] = Convert.ToByte(argb[3]);
            }
            catch (Exception)
            {
                return;
            }
        }

        public List<Point> GetWhitePixels()
        {
            byte ff = Convert.ToByte(0xff);
            List<Point> results = new List<Point>();

            //TODO: An Philipp: Wofür wird hier die canvas-Variable benötigt?
            if (pixelsCanvasEraser != null && pixelsCanvasEraser.Length > 0)
            {
                for (int x = 0; x < pixelWidthCanvas; x++)
                {
                    for (int y = 0; y < pixelHeightCanvas; y++)
                    {
                        int Temp = y * pixelWidthCanvas;
                        int XTemp = Temp + x;
                        int Value = XTemp * 4;

                        if (pixelsCanvasEraser[Value] == ff &&
                        pixelsCanvasEraser[Value + 1] == ff &&
                        pixelsCanvasEraser[Value + 2] == ff &&
                        pixelsCanvasEraser[Value + 3] == ff)
                        {
                            results.Add(new Point(x, y));
                        }
                    }
                }
            }
            return results;
        }

        public Dictionary<Point, byte[]> GetDataPointsWithColor()
        {
            byte ff = Convert.ToByte(0xff);
            List<Point> results = new List<Point>();
            Dictionary<Point, byte[]> pointToColorByteArray = new Dictionary<Point, byte[]>();
            for (int x = 0; x < pixelWidthCanvas; x++)
            {
                for (int y = 0; y < pixelHeightCanvas; y++)
                {
                    int Temp = y * pixelWidthCanvas;
                    int XTemp = Temp + x;
                    int Value = XTemp * 4;

                    if (pixelsCanvasEraser[Value] == ff &&
                    pixelsCanvasEraser[Value + 1] == ff &&
                    pixelsCanvasEraser[Value + 2] == ff &&
                    pixelsCanvasEraser[Value + 3] == ff)
                    {
                        byte[] colorByteArray = new byte[4];
                        colorByteArray[0] = pixelsCanvasEraser[Value];
                        colorByteArray[1] = pixelsCanvasEraser[Value + 1];
                        colorByteArray[2] = pixelsCanvasEraser[Value + 2];
                        colorByteArray[3] = pixelsCanvasEraser[Value + 3];
                    }
                }
            }
            return pointToColorByteArray;
        }

        public async Task<Image> BufferToImage()
        {
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap();
            WriteableBitmap wb = new WriteableBitmap(pixelWidthCanvas, pixelHeightCanvas);

            await wb.PixelBuffer.AsStream().WriteAsync(pixelsCanvas, 0, pixelsCanvas.Length);

            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();
            Image image = new Image();
            image.Stretch = Stretch.Uniform;
            image.Source = wb;
            image.Height = PocketPaintApplication.GetInstance().PaintingAreaCanvas.Height;
            image.Width = PocketPaintApplication.GetInstance().PaintingAreaCanvas.Width;
            return image;
        }

        public async Task<bool> PixelBufferToBitmap()
        {
            try
            {
                var bitmapPixels = ConvertArray(pixelsCanvas, pixelWidthCanvas, pixelHeightCanvas);
                var wbCroppedBitmap = new WriteableBitmap(PocketPaintApplication.GetInstance().Bitmap.PixelWidth, PocketPaintApplication.GetInstance().Bitmap.PixelHeight);
                await wbCroppedBitmap.PixelBuffer.AsStream().WriteAsync(bitmapPixels, 0, bitmapPixels.Length);

                Image image = new Image();
                image.Source = wbCroppedBitmap;
                image.Height = wbCroppedBitmap.PixelHeight;
                image.Width = wbCroppedBitmap.PixelWidth;
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(image);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private Point ConvertCoordinates(Point oldPoint)
        {
            double NormfactorX = (double)pixelWidthCanvas / PocketPaintApplication.GetInstance().Bitmap.PixelWidth;
            double NormfactorY = (double)pixelHeightCanvas / PocketPaintApplication.GetInstance().Bitmap.PixelHeight;
            double doubleY = (oldPoint.Y) * NormfactorY;
            double doubleX = (oldPoint.X) * NormfactorX;
            return new Point(Math.Round(doubleX, 0), Math.Round(doubleY, 0));
        }

        public string getPixelFromCanvas(uint x, uint y)
        {
            string result = "";
            if (pixelsCanvas != null)
            {
                uint temp = Convert.ToUInt32(this.pixelWidthCanvas * y);
                uint indexOfSearchedPixel = (temp + x) * 4;
                var a = pixelsCanvas[indexOfSearchedPixel + 3];
                var r = pixelsCanvas[indexOfSearchedPixel + 2];
                var g = pixelsCanvas[indexOfSearchedPixel + 1];
                var b = pixelsCanvas[indexOfSearchedPixel];
                result = RGBToString(a, r, g, b);
            }
            return result;
        }

        public string getPixelFromCanvas(Point p)
        {
            int intTemp;
            int intXTemp;
            int intValue;
            try
            {
                if (pixelsCanvas != null)
                {
                    intTemp = ((int)p.Y - 1) * pixelWidthCanvas;
                    intXTemp = intTemp + ((int)p.X - 1);
                    intValue = intXTemp * 4;

                    var a = pixelsCanvas[intValue + 3];
                    var r = pixelsCanvas[intValue + 2];
                    var g = pixelsCanvas[intValue + 1];
                    var b = pixelsCanvas[intValue];

                    return RGBToString(a, r, g, b);
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ComparePixelsColorForFloddFill(Point p, string oldColor)
        {
            int intTemp;
            int intXTemp;
            int intValue;
            try
            {
                if (pixelsCanvas != null)
                {
                    intTemp = ((int)p.Y - 1) * pixelWidthCanvas;
                    intXTemp = intTemp + ((int)p.X - 1);
                    intValue = intXTemp * 4;

                    oldColor.Split('_');
                    var aO = oldColor.Split('_')[0];
                    var rO = oldColor.Split('_')[1];
                    var gO = oldColor.Split('_')[2];
                    var bO = oldColor.Split('_')[3];
                    var a = pixelsCanvas[intValue + 3];
                    var r = pixelsCanvas[intValue + 2];
                    var g = pixelsCanvas[intValue + 1];
                    var b = pixelsCanvas[intValue];
                    if (Math.Abs(Convert.ToInt16(aO) - (int)a) > 70)
                        return false;
                    if (Math.Abs(Convert.ToInt16(rO) - (int)r) > 70)
                        return false;
                    if (Math.Abs(Convert.ToInt16(gO) - (int)g) > 70)
                        return false;
                    if (Math.Abs(Convert.ToInt16(bO) - (int)b) > 70)
                        return false;

                    return true;
                }
                else
                    return false;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public string ColorToString(SolidColorBrush color)
        {
            return color.Color.A + "_" + color.Color.R + "_" + color.Color.G + "_" + color.Color.B;
        }

        public string RGBToString(int a, int r, int g, int b)
        {
            return a + "_" + r + "_" + g + "_" + b;
        }

        #endregion

        async public Task<int> preparePaintingAreaCanvasForEraser()
        {
            RenderTargetBitmap retarbi = new RenderTargetBitmap();
            Canvas eraserCanvas = PocketPaintApplication.GetInstance().EraserCanvas;
            try
            {
                if (eraserCanvas.Visibility == Visibility.Collapsed)
                    eraserCanvas.Visibility = Visibility.Visible;
                await retarbi.RenderAsync(eraserCanvas);

                Windows.Storage.Streams.IBuffer buffer = await (retarbi.GetPixelsAsync());
                pixelsCanvasEraser = WindowsRuntimeBufferExtensions.ToArray(buffer);

                this.pixelHeightCanvas = retarbi.PixelHeight;
                this.pixelWidthCanvas = retarbi.PixelWidth;
            }
            catch 
            { 
                return 1; 
            }
            return 0;
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

        public void SetPixel(List<Point> points, string c)
        {
            foreach (var point in points)
            {
                SetPixel(point, c);
            }
        }
    }
}
