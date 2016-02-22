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
            SolidColorBrush selectedSolidColorBrush = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            fillSpace(coordinate, selectedSolidColorBrush);
            CommandManager.GetInstance().CommitCommand(new FillCommand(coordinate, selectedSolidColorBrush));
        }

        async public void fillSpace(Point coordinate, SolidColorBrush solidColorBrush)
        {
            try
            {
                PocketPaintApplication.GetInstance().ProgressRing.IsActive = true;
                PixelData.PixelData pixelData = new PixelData.PixelData();
                await pixelData.preparePaintingAreaCanvasPixel();
                Catrobat.Paint.WindowsPhone.Ui.Spinner.StartSpinning();
                if (pixelData.FloodFill4(coordinate, solidColorBrush) == false)
                {
                    return;
                }
                Catrobat.Paint.WindowsPhone.Ui.Spinner.StopSpinning();
                await pixelData.PixelBufferToBitmap();
            }
            catch(Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.StackTrace);
            }
            finally
            {
                Catrobat.Paint.WindowsPhone.Ui.Spinner.StopSpinning();
                PocketPaintApplication.GetInstance().ProgressRing.IsActive = false;
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
        }

        public override void ResetDrawingSpace()
        {

        }

        public override void ResetUsedElements()
        {
        }

    }
}
