using Catrobat.Paint.WindowsPhone.Tool;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;
using Catrobat.Paint.WindowsPhone.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using System.Runtime.InteropServices.WindowsRuntime;
using System.IO;
using Windows.Storage.Streams;
using Windows.Foundation;

namespace Catrobat.Paint.WindowsPhone.Command
{
    class FillCommand : CommandBase
    {
        private byte[] _oldCanvas { get; set; }
        private byte[] _newCanvas { get; set; }
        private int _width;
        private int _height;
        private PixelData.PixelData _pD;
        public FillCommand(byte[] oldCanvas, byte[] newCanvas, int width, int height)
        {
            ToolType = ToolType.Fill;
            _oldCanvas = oldCanvas;
            _newCanvas = newCanvas;
            _width = width;
            _height = height;
            _pD = new PixelData.PixelData();

        }

        public override bool ReDo()
        {
            try
            {
                PerformUndo();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public  override bool UnDo()
        {
            try
            {
                PerformUndo();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async void PerformUndo() 
        {
            try
            {
                _oldCanvas = _pD.ConvertArray(_oldCanvas, _width, _height);
                var wbCroppedBitmap = new WriteableBitmap(PocketPaintApplication.GetInstance().Bitmap.PixelWidth, PocketPaintApplication.GetInstance().Bitmap.PixelHeight);
                await wbCroppedBitmap.PixelBuffer.AsStream().WriteAsync(_oldCanvas, 0, _oldCanvas.Length);

                Image image = new Image();
                image.Source = wbCroppedBitmap;
                image.Height = wbCroppedBitmap.PixelHeight;
                image.Width = wbCroppedBitmap.PixelWidth;
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(image);
                return;
            }
            catch (Exception)
            {
                return;
            }
        }
        public async void PerformRedo()
        {
            try
            {
                _newCanvas = _pD.ConvertArray(_newCanvas, _width, _height);
                var wbCroppedBitmap = new WriteableBitmap(PocketPaintApplication.GetInstance().Bitmap.PixelWidth, PocketPaintApplication.GetInstance().Bitmap.PixelHeight);
                await wbCroppedBitmap.PixelBuffer.AsStream().WriteAsync(_newCanvas, 0, _newCanvas.Length);

                Image image = new Image();
                image.Source = wbCroppedBitmap;
                image.Height = wbCroppedBitmap.PixelHeight;
                image.Width = wbCroppedBitmap.PixelWidth;
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(image);
                return;
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
