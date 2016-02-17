using Catrobat.Paint.WindowsPhone.Tool;
using System;
using Windows.Foundation;
using Windows.UI.Xaml.Media;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    class PipetteTool : ToolBase
    {
        public PipetteTool()
        {
            ToolType = ToolType.Pipette;
            ResetCanvas();
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
            
            PixelData.PixelData myPixelData = new PixelData.PixelData();
            PocketPaintApplication.GetInstance().PaintData.colorSelected = await myPixelData.GetPixel(PocketPaintApplication.GetInstance().Bitmap, (int)coordinates.X, (int)coordinates.Y);


        //PocketPaintApplication.GetInstance().PaintData.ColorSelected =
        //  new SolidColorBrush(PocketPaintApplication.GetInstance().Bitmap.);      
        }

        public override void HandleMove(object arg)
        {

        }

        public override void Draw(object o)
        {

        }

        public override void ResetDrawingSpace()
        {
            throw new NotImplementedException();
        }

        public override void ResetUsedElements()
        {
        }
    }
}
