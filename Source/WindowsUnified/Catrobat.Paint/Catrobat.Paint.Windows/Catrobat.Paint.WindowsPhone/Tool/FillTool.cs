using Catrobat.Paint.WindowsPhone.Command;
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

        public override void HandleUp(object arg)
        {
            if (!(arg is Point))
            {
                return;
            }
            Point coordinate = (Point)arg;
            fillSpace(coordinate);
            CommandManager.GetInstance().CommitCommand(new FillCommand(coordinate));
        }

        async public void fillSpace(Point coordinate)
        {
            try
            {
                PocketPaintApplication.GetInstance().ProgressRing.IsActive = true;
                PixelData.PixelData pixelData = new PixelData.PixelData();
                await pixelData.preparePaintingAreaCanvasPixel();
                Catrobat.Paint.WindowsPhone.Ui.Spinner.StartSpinning();
                if (pixelData.FloodFill4(coordinate, PocketPaintApplication.GetInstance().PaintData.colorSelected) == false)
                {
                    Catrobat.Paint.WindowsPhone.Ui.Spinner.StopSpinning();
                    return;
                }
                Catrobat.Paint.WindowsPhone.Ui.Spinner.StopSpinning();
                await pixelData.PixelBufferToBitmap();
                PocketPaintApplication.GetInstance().ProgressRing.IsActive = false;
            }
            catch(Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.StackTrace);
            }
        }

        public override void HandleMove(object arg)
        {
            
        }

        public override void Draw(object obj)
        {
            if (!(obj is Point))
            {
                return;
            }
            Point coordinate = (Point)obj;
            fillSpace(coordinate);
        }

        public override void ResetDrawingSpace()
        {

        }

    }
}
