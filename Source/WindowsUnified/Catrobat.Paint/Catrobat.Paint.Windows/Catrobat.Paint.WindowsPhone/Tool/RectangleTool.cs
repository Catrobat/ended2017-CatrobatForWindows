using Catrobat.Paint.Phone;
using Catrobat.Paint.Phone.Tool;
using Catrobat.Paint.WindowsPhone.Controls.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    class RectangleTool : ToolBase
    {
        private Path _path;
        private PathGeometry _pathGeometry;
        private PathFigureCollection _pathFigureCollection;
        private PathFigure _pathFigure;
        private PathSegmentCollection _pathSegmentCollection;
        private Point _lastPoint;
        private bool _lastPointSet;
        private LineSegment _lineSegment;

        public RectangleTool()
        {
            this.ToolType = ToolType.Rect;
            //PocketPaintApplication.GetInstance().CurrentShape = PocketPaintApplication.GetInstance().BarRecEllShape.RectangleForeground;
            PocketPaintApplication.GetInstance().BarRecEllShape.setBorderColor();
            PocketPaintApplication.GetInstance().BarRecEllShape.setFillColor();
            if (PocketPaintApplication.GetInstance().RecDrawingRectangle != null)
            {
                PocketPaintApplication.GetInstance().RecDrawingRectangle.Fill = PocketPaintApplication.GetInstance().PaintData.ColorSelected;
                PocketPaintApplication.GetInstance().RecDrawingRectangle.Stroke = PocketPaintApplication.GetInstance().PaintData.BorderColorSelected;
                PocketPaintApplication.GetInstance().RecDrawingRectangle.Visibility = Visibility.Visible;
            }


        }
        public override void HandleDown(object arg)
        {
            /* if (!(arg is Point))
            {
                return;
            }

            var coordinate = (Point)arg;

            int height = PocketPaintApplication.GetInstance().BarRecEllShape.getHeight();
            int width = PocketPaintApplication.GetInstance().BarRecEllShape.getWidth();
            
            RectangleGeometry myRectangleGeometry = new RectangleGeometry();
            myRectangleGeometry.Rect = new Rect(coordinate, new Point(coordinate.X+width, coordinate.Y+height));

            _path = new Path();
            _path.Fill = PocketPaintApplication.GetInstance().PaintData.FillColorSelected;
            _path.Stroke = PocketPaintApplication.GetInstance().PaintData.BorderColorSelected;
            _path.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.BorderThicknessRecEll;
            _path.StrokeEndLineCap = PenLineCap.Square;
            _path.StrokeStartLineCap = PenLineCap.Square;

            _path.Data = myRectangleGeometry;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(_path);

           /* _path = new Path();
            _pathGeometry = new PathGeometry();
            _path.StrokeLineJoin = PenLineJoin.Bevel;

            _path.Stroke = PocketPaintApplication.GetInstance().PaintData.ColorSelected;
            _path.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.ThicknessSelected;
            _path.StrokeEndLineCap = PocketPaintApplication.GetInstance().PaintData.CapSelected;
            _path.StrokeStartLineCap = PocketPaintApplication.GetInstance().PaintData.CapSelected;

            _path.Data = _pathGeometry;
            _pathFigureCollection = new PathFigureCollection();
            _pathGeometry.Figures = _pathFigureCollection;
            _pathFigure = new PathFigure();

            _pathFigureCollection.Add(_pathFigure);
            _lastPoint = coordinate;
            _pathFigure.StartPoint = coordinate;
            _pathSegmentCollection = new PathSegmentCollection();
            _pathFigure.Segments = _pathSegmentCollection;

            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(_path);

            //            var transform = PocketPaintApplication.GetInstance().PaintingAreaCanvas.TransformToVisual(PocketPaintApplication.GetInstance().PaintingAreaLayoutRoot);
            //            var absolutePosition = transform.Transform(new Point(0, 0));
            var r = new RectangleGeometry
            {
                Rect = new Rect(0, 0, PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth,
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualHeight)
            };
            _path.Clip = r;
            _path.InvalidateArrange();
            _path.InvalidateMeasure();
            _lineSegment = new LineSegment();*/
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
            if (!(o is Point))
            {
                return;
            }

            var coordinate = (Point)o;

            int height = PocketPaintApplication.GetInstance().BarRecEllShape.getHeight();
            int width = PocketPaintApplication.GetInstance().BarRecEllShape.getWidth();

            RectangleGeometry myRectangleGeometry = new RectangleGeometry();
            myRectangleGeometry.Rect = new Rect(coordinate, new Point(coordinate.X + width, coordinate.Y + height));

            _path = new Path();
            _path.Fill = PocketPaintApplication.GetInstance().PaintData.ColorSelected;
            _path.Stroke = PocketPaintApplication.GetInstance().PaintData.BorderColorSelected;
            _path.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.BorderThicknessRecEll;
            _path.StrokeEndLineCap = PenLineCap.Square;
            _path.StrokeStartLineCap = PenLineCap.Square;

            _path.Data = myRectangleGeometry;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(_path);
        }

        public override void ResetDrawingSpace()
        {
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();
        }
    }
}
