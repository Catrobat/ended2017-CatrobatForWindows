using Catrobat.Paint.WindowsPhone.Command;
using System;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    class LineTool : ToolBase
    {
        private Path _path;
        private PathGeometry _pathGeometry;
        private PathFigureCollection _pathFigureCollection;
        private PathFigure _pathFigure;
        private PathSegmentCollection _pathSegmentCollection;
        private Point _lastPoint;
        private LineSegment _lineSegment;

        public LineTool()
        {
            this.ToolType = ToolType.Line;

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
            _path.StrokeLineJoin = PenLineJoin.Bevel;

            _path.Stroke = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            _path.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.thicknessSelected;
            _path.StrokeEndLineCap = PocketPaintApplication.GetInstance().PaintData.penLineCapSelected;
            _path.StrokeStartLineCap = PocketPaintApplication.GetInstance().PaintData.penLineCapSelected;

            _path.Data = _pathGeometry;
            _pathFigureCollection = new PathFigureCollection();
            _pathGeometry.Figures = _pathFigureCollection;
            _pathFigure = new PathFigure();

            _pathFigureCollection.Add(_pathFigure);
            _lastPoint = coordinate;
            _pathFigure.StartPoint = coordinate;
            _pathSegmentCollection = new PathSegmentCollection();
            _pathFigure.Segments = _pathSegmentCollection;

            PocketPaintApplication.GetInstance().PaintingAreaView.addElementToPaintingAreCanvas(_path);

            var rectangleGeometry = new RectangleGeometry
            {
                Rect = new Rect(0, 0, PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth,
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualHeight)
            };
            _path.Clip = rectangleGeometry;
            _path.InvalidateArrange();
            _path.InvalidateMeasure();
            _lineSegment = new LineSegment();
        }

        public override void HandleMove(object arg)
        {
            if (!(arg is Point))
            {
                return;
            }

            var coordinate = (Point)arg;
            _pathSegmentCollection.Remove(_lineSegment);
            _lineSegment.Point = coordinate;
            _pathSegmentCollection.Add(_lineSegment);

            PocketPaintApplication.GetInstance().PaintingAreaLayoutRoot.InvalidateMeasure();
            _path.InvalidateArrange();
        }

        public override void HandleUp(object arg)
        {
            CommandManager.GetInstance().CommitCommand(new LineCommand(_path));            
        }

        public override void Draw(object o)
        {
            
        }

        public override void ResetDrawingSpace()
        {
            throw new NotImplementedException();
        }
        public override void ResetUsedElements()
        {
        }
    }
}
