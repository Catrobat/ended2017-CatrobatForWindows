using Catrobat.Paint.Phone;
using Catrobat.Paint.Phone.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    class EllipseTool : ToolBase
    {
        public EllipseTool(ToolType toolType = ToolType.Ellipse)
        {
            ToolType = toolType;
        }

        public override void HandleDown(object arg)
        {
            if (!(arg is Point))
            {
                return;
            }

            var coordinate = (Point)arg;
            /*
            Ellipse ellipse = new Ellipse();
            ellipse.Margin = new Thickness(coordinate.X, coordinate.Y, 0, 0);
            ellipse.Height = 20;
            ellipse.Width = 20;
            ellipse.MinHeight = 20;
            ellipse.MinWidth = 20;
            ellipse.Stroke = PocketPaintApplication.GetInstance().PaintData.ColorSelected;
            ellipse.StrokeThickness = 3;
            ellipse.Fill = PocketPaintApplication.GetInstance().PaintData.ColorSelected;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(ellipse); */
        }

        public override void HandleMove(object arg)
        {
            throw new NotImplementedException();
        }

        public override void HandleUp(object arg)
        {
            
        }

        public override void Draw(object o)
        {
            throw new NotImplementedException();
        }

        public override void ResetDrawingSpace()
        {
            throw new NotImplementedException();
        }
    }
}
