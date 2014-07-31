using System;
using System.Windows;
using Windows.UI.Xaml.Shapes;
// TODO: using Catrobat.Paint.Phone.Command;
using Windows.UI.Xaml.Media;
using Windows.Foundation;
using Catrobat.Paint.WindowsPhone.Tool;
using Catrobat.Paint.Phone.Command;

namespace Catrobat.Paint.Phone.Tool
{
    class BrushTool : ToolBase
    {
        private Path _path;
        private PathGeometry _pathGeometry;
        private PathFigureCollection _pathFigureCollection;
        private PathFigure _pathFigure;
        private PathSegmentCollection _pathSegmentCollection;
        private Point _lastPoint;
        private bool _lastPointSet;



        public BrushTool(ToolType toolType = ToolType.Brush)
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

            _path = new Path();
            _pathGeometry = new PathGeometry();
            _path.StrokeLineJoin = PenLineJoin.Round;

            _path.Stroke = PocketPaintApplication.GetInstance().PaintData.ColorSelected;
            _path.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.ThicknessSelected;
            _path.StrokeStartLineCap = PocketPaintApplication.GetInstance().PaintData.CapSelected;
            _path.StrokeEndLineCap = PocketPaintApplication.GetInstance().PaintData.CapSelected;

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

            // var transform = PocketPaintApplication.GetInstance().PaintingAreaCanvas.TransformToVisual(PocketPaintApplication.GetInstance().PaintingAreaLayoutRoot);
            // var absolutePosition = transform.TransformPoint(new Point(0, 0));
            
            var r = new RectangleGeometry
            {
                Rect = new Rect(0, 0, PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth,
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualHeight)
            };
            _path.Clip = r;
            _path.InvalidateArrange();
            _path.InvalidateMeasure();


            
        }

        public override void HandleMove(object arg)
        {
            if (!(arg is Point))
            {
                return;
            }

            var coordinate = (Point)arg;
            System.Diagnostics.Debug.WriteLine("BrushTool Coord: " + coordinate.X + " " + coordinate.Y);

            if (!_lastPointSet)
            {
                _lastPoint = coordinate;
                _lastPointSet = true;
                return;
            }
            if (_lastPointSet && !_lastPoint.Equals(coordinate))
            {
                var qbs = new QuadraticBezierSegment
                {
                    Point1 = _lastPoint,
                    Point2 = coordinate
                };

                _pathSegmentCollection.Add(qbs);
                

                PocketPaintApplication.GetInstance().PaintingAreaLayoutRoot.InvalidateMeasure();
                _lastPointSet = false;
            }
            
        }

        public override void HandleUp(object arg)
        {
            if (!(arg is Point))
            {
                return;
            }

            var coordinate = (Point)arg;

            // only a point/dot is drawn, no movement of finger on screen
            if (_lastPoint.Equals(coordinate))
            {
                var qbs = new QuadraticBezierSegment
                {
                    Point1 = _lastPoint,
                    Point2 = coordinate 
                };

                _pathSegmentCollection.Add(qbs);
 
                PocketPaintApplication.GetInstance().PaintingAreaLayoutRoot.InvalidateMeasure();
                _path.InvalidateArrange();

            }

            
            CommandManager.GetInstance().CommitCommand(new BrushCommand(_path));
        }

        public override void Draw(object o)
        {
            throw new NotImplementedException();
        }
 
    }
}
