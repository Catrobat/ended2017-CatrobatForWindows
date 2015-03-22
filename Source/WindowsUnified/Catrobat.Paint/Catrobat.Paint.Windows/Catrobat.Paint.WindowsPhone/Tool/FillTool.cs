using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    class FillTool : ToolBase
    {
        public FillTool()
        {
            ToolType = ToolType.Fill;        
        }
        public override void HandleDown(object arg)
        {

        }

        public async override void HandleUp(object arg)
        {
            if (!(arg is Point))
            {
                return;
            }
            Point coordinates = (Point)arg;
            PixelData.PixelData PixelData = new PixelData.PixelData();
            await PixelData.preparePaintingAreaCanvasPixel();
            PixelData.FloodFill(coordinates, PocketPaintApplication.GetInstance().PaintData.colorSelected);
            await PixelData.PixelBufferToBitmap();
        }

        public override void HandleMove(object arg)
        {
            
        }

        public override void Draw(object o)
        {
            
        }

        public override void ResetDrawingSpace()
        {

        }

    }
}
