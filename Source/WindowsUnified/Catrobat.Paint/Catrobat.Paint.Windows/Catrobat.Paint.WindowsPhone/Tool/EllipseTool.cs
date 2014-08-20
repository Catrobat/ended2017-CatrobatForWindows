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
        private Path _path;
        public EllipseTool(ToolType toolType = ToolType.Ellipse)
        {
            ToolType = toolType;
            PocketPaintApplication.GetInstance().CurrentShape = PocketPaintApplication.GetInstance().BarRecEllShape.EllipseForeground;
        }

        public override void HandleDown(object arg)
        {
            if (!(arg is Point))
            {
                return;
            }

            var coordinate = (Point)arg;

            int height = PocketPaintApplication.GetInstance().BarRecEllShape.getHeight();
            int width = PocketPaintApplication.GetInstance().BarRecEllShape.getWidth();

            EllipseGeometry myEllipseGeometry = new EllipseGeometry();
            myEllipseGeometry.Center = new Point(coordinate.X, coordinate.Y);
            myEllipseGeometry.RadiusX = width / 2;
            myEllipseGeometry.RadiusY = height / 2;

            _path = new Path();
            _path.Fill = PocketPaintApplication.GetInstance().PaintData.FillColorSelected;
            _path.Stroke = PocketPaintApplication.GetInstance().PaintData.BorderColorSelected;
            _path.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.BorderThicknessRecEll;
            _path.StrokeEndLineCap = PenLineCap.Square;
            _path.StrokeStartLineCap = PenLineCap.Square;

            _path.Data = myEllipseGeometry;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(_path);
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
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();
        }
    }
}
