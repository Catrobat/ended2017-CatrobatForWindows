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
    class CursorTool : ToolBase
    {
        private Path _path;
        private PathGeometry _pathGeometry;
        private PathFigureCollection _pathFigureCollection;
        private PathFigure _pathFigure;
        private PathSegmentCollection _pathSegmentCollection;
        private Point _lastPoint;
        private bool _lastPointSet;
        private TransformGroup _transforms;


        public CursorTool(ToolType toolType = ToolType.Cursor)
        {
            ToolType = toolType;

            if (PocketPaintApplication.GetInstance().GridCursor.RenderTransform != null)
            {
                _transforms = PocketPaintApplication.GetInstance().GridCursor.RenderTransform as TransformGroup;
            }
            if (_transforms == null)
            {
                PocketPaintApplication.GetInstance().GridCursor.RenderTransform = _transforms = new TransformGroup();
            }
        }


 
        public override void HandleDown(object arg)
        {
            if (PocketPaintApplication.GetInstance().cursorControl.isDrawingActivated())
            {
                if (!(arg is Point))
                {
                    return;
                }
                double height = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.ActualHeight / 2;
                double width = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.ActualWidth / 2;
                var coordinate = new Point(width + _transforms.Value.OffsetX, height + _transforms.Value.OffsetY);
                var test = new Point(_transforms.Value.OffsetX, _transforms.Value.OffsetY);

                _path = new Path();
                _pathGeometry = new PathGeometry();
                _pathFigureCollection = new PathFigureCollection();
                _pathFigure = new PathFigure();
                _pathSegmentCollection = new PathSegmentCollection();

                _path.StrokeLineJoin = PenLineJoin.Round;
                _path.Stroke = PocketPaintApplication.GetInstance().PaintData.ColorSelected;
                _path.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.ThicknessSelected;
                _path.StrokeStartLineCap = PocketPaintApplication.GetInstance().PaintData.CapSelected;
                _path.StrokeEndLineCap = PocketPaintApplication.GetInstance().PaintData.CapSelected;

                _pathFigure.StartPoint = coordinate;
                _pathFigure.Segments = _pathSegmentCollection;
                _pathFigureCollection.Add(_pathFigure);
                _pathGeometry.Figures = _pathFigureCollection;
                _lastPoint = coordinate;
                _path.Data = _pathGeometry;
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
        }

        public override void HandleMove(object arg)
        {
            if (arg.GetType() == typeof(Point))
            {
                if (PocketPaintApplication.GetInstance().cursorControl.isDrawingActivated())
                {
                    double height = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.ActualHeight / 2;
                    double width = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.ActualWidth / 2;
                    var coordinate = new Point(width + _transforms.Value.OffsetX, height + _transforms.Value.OffsetY);
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
            }
            else if (arg.GetType() == typeof(TranslateTransform))
            {
                var move = (TranslateTransform)arg;
                TranslateTransform movezoom = new TranslateTransform();
                movezoom.X = move.X;
                movezoom.Y = move.Y;
                _transforms.Children.Add(movezoom);

                //PocketPaintApplication.GetInstance().PaintingAreaView.setCoordTextbox(PocketPaintApplication.GetInstance().GridCursor.ActualHeight, PocketPaintApplication.GetInstance().GridCursor.ActualWidth);
                // PocketPaintApplication.GetInstance().PaintingAreaView.setCoordTextbox(_transforms.Value.OffsetX, _transforms.Value.OffsetY);
            }
        }

        public override void HandleUp(object arg)
        {
            if (PocketPaintApplication.GetInstance().cursorControl.isDrawingActivated())
            {
                if (!(arg is Point))
                {
                    return;
                }

                double height = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.ActualHeight / 2;
                double width = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.ActualWidth / 2;
                var coordinate = new Point(width + _transforms.Value.OffsetX, height + _transforms.Value.OffsetY);

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
            }
            //CommandManager.GetInstance().CommitCommand(new BrushCommand(_path));
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
