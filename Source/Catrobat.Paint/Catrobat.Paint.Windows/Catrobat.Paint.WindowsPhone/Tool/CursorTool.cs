using Catrobat.Paint.WindowsPhone.Command;
using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
// TODO: using Catrobat.Paint.Phone.Command;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone.Tool
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
        private double height;
        private double width;

        public CursorTool()
        {
            ToolType = ToolType.Cursor;

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
                setHeightWidth();

                var coordinate = new Point(width + _transforms.Value.OffsetX, height + _transforms.Value.OffsetY);

                _path = new Path();
                _pathGeometry = new PathGeometry();
                _pathFigureCollection = new PathFigureCollection();
                _pathFigure = new PathFigure();
                _pathSegmentCollection = new PathSegmentCollection();

                _path.StrokeLineJoin = PenLineJoin.Round;
                _path.Stroke = PocketPaintApplication.GetInstance().PaintData.colorSelected;
                _path.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.thicknessSelected;
                _path.StrokeStartLineCap = PocketPaintApplication.GetInstance().PaintData.penLineCapSelected;
                _path.StrokeEndLineCap = PocketPaintApplication.GetInstance().PaintData.penLineCapSelected;

                _pathFigure.StartPoint = coordinate;
                _pathFigure.Segments = _pathSegmentCollection;
                _pathFigureCollection.Add(_pathFigure);
                _pathGeometry.Figures = _pathFigureCollection;
                _lastPoint = coordinate;
                _path.Data = _pathGeometry;

                PocketPaintApplication.GetInstance().PaintingAreaView.addElementToPaintingAreCanvas(_path);

                var rectangleGeometry = new RectangleGeometry
                {
                    Rect = new Rect(0, 0, PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth,
                    PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualHeight)
                };
                _path.Clip = rectangleGeometry;
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
                    setHeightWidth();

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
                _transforms.Children.Add(move);
            }

            if (PocketPaintApplication.GetInstance() != null)
            {
                AppBarButton appBarButtonReset = PocketPaintApplication.GetInstance().PaintingAreaView.getAppBarResetButton();
                if (appBarButtonReset != null)
                {
                    if (!appBarButtonReset.IsEnabled)
                    {
                        appBarButtonReset.IsEnabled = true;
                    }
                }
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
                setHeightWidth();

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
                CommandManager.GetInstance().CommitCommand(new CursorCommand(_path));
            }
        }

        public void app_btnResetCursor_Click(object sender, RoutedEventArgs e)
        {
            ((AppBarButton)sender).IsEnabled = false;
            PocketPaintApplication.GetInstance().GridCursor.RenderTransform = _transforms = new TransformGroup();
        }

        public override void Draw(object o)
        {
            throw new NotImplementedException();
        }

        public override void ResetDrawingSpace()
        {
            throw new NotImplementedException();
        }

        public void setHeightWidth()
        {
            height = PocketPaintApplication.GetInstance().GridWorkingSpace.ActualHeight / 2.0;
            width = PocketPaintApplication.GetInstance().GridWorkingSpace.ActualWidth / 2.0;
        }

        public override void ResetUsedElements()
        {
            PocketPaintApplication.GetInstance().GridCursor.RenderTransform = _transforms = new TransformGroup();
            PocketPaintApplication.GetInstance().cursorControl.setCursorLook(false);
        }
    }
}
