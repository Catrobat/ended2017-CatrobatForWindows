using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Catrobat.Paint.Phone.Tool
{
    class PipetteTool : ToolBase
    {
        public PipetteTool(ToolType toolType = ToolType.Pipette)
        {
            ToolType = toolType;
            ResetCanvas();
        }


        public override void HandleDown(object arg)
        {
            if (NeedToResetCanvas)
            {
                ResetCanvas();
            }

        }

        public override void HandleUp(object arg)
        {
            if (!(arg is Point))
            {
                return;
            }

            var coordinate = (Point)arg;

            PocketPaintApplication.GetInstance().PaintData.ColorSelected = new SolidColorBrush(PocketPaintApplication.GetInstance().Bitmap.GetPixel((int)coordinate.X, (int)coordinate.Y)); 
        }

        public override void HandleMove(object arg)
        {

        }

        public override void Draw(object o)
        {

        }
    }
}
