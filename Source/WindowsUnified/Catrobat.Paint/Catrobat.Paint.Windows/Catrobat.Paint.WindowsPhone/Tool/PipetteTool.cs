using Catrobat.Paint.WindowsPhone.Tool;
using System;
using Windows.Foundation;

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
            //if (NeedToResetCanvas)
            //{
            //    ResetCanvas();
            //}

        }

        public override void HandleUp(object arg)
        {
            if (!(arg is Point))
            {
                return;
            }

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
    }
}
