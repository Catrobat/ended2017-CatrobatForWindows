using Catrobat.Paint.WindowsPhone.Command;
using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    //TODO: performance critical... doing some optimizations

    class EraserTool : ToolBase
    {
        private Path _path;
        private PathGeometry _pathGeometry;
        private PathFigureCollection _pathFigureCollection;
        private PathFigure _pathFigure;
        private PathSegmentCollection _pathSegmentCollection;
        private Point _lastPoint;
        private bool _lastPointSet;
        private List<Point> points;
        private PixelData.PixelData pixelDataEraser;
        private PixelData.PixelData pixelData;

        public EraserTool()
        {
            ToolType = ToolType.Eraser;
            _lastPointSet = false;
            points = new List<Point>();
            pixelData = new PixelData.PixelData();
            pixelDataEraser = new PixelData.PixelData();
            initPathInstances();
            initPathStrokeSettings();
        }

        private void initPathInstances()
        {
            _path = new Path();
            _pathGeometry = new PathGeometry();
            _pathFigureCollection = new PathFigureCollection();
            _pathFigure = new PathFigure();
            _pathSegmentCollection = new PathSegmentCollection();
        }

        private void initPathStrokeSettings()
        {
            _path.StrokeLineJoin = PenLineJoin.Round;
            _path.Stroke = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0xff, 0xff));
            _path.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.thicknessSelected;
            _path.StrokeStartLineCap = PocketPaintApplication.GetInstance().PaintData.penLineCapSelected;
            _path.StrokeEndLineCap = PocketPaintApplication.GetInstance().PaintData.penLineCapSelected;
        }

        async public override void HandleDown(object arg)
        {
            if (!(arg is Point))
            {
                return;
            }
            _lastPointSet = false;
            points = new List<Point>();
            var coordinate = (Point)arg;
            await pixelData.preparePaintingAreaCanvasPixel();

            initPathInstances();
            initPathStrokeSettings();

            _pathFigure.StartPoint = coordinate;
            _pathFigure.Segments = _pathSegmentCollection;
            _pathFigureCollection.Add(_pathFigure);
            _pathGeometry.Figures = _pathFigureCollection;
            _lastPoint = coordinate;
            _path.Data = _pathGeometry;

            PocketPaintApplication.GetInstance().PaintingAreaView.addElementToEraserCanvas(_path);

            var rectangleGeometry = new RectangleGeometry
            {
                Rect = new Rect(0, 0, PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth,
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualHeight)
            };
            _path.Clip = rectangleGeometry;
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

        async public override void HandleUp(object arg)
        {
            if (!(arg is Point))
            {
                return;
            }

            var coordinate = (Point)arg;

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
           
            int returnValue = await pixelDataEraser.preparePaintingAreaCanvasForEraser();
            if (returnValue == 1)
                throw new Exception("Preparing pixeldataeraser failed!");
            points = pixelDataEraser.GetWhitePixels();
            pixelData.SetPixel(points, "0_0_0_0");
            PocketPaintApplication.GetInstance().EraserCanvas.Children.Clear();
            if(await pixelData.PixelBufferToBitmap())
                CommandManager.GetInstance().CommitCommand(new EraserCommand(points));
        }
        async public override void Draw(object obj)
        {
            await pixelData.preparePaintingAreaCanvasPixel();
            pixelDataEraser = new PixelData.PixelData();
            await pixelDataEraser.preparePaintingAreaCanvasForEraser();
            List<Point> pointsToSet = ((List<Point>)obj);
            pixelData.SetPixel(pointsToSet, "0_0_0_0");
            PocketPaintApplication.GetInstance().EraserCanvas.Children.Clear();
            var image = await pixelData.BufferToImage();
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(image);
        }

        public override void ResetDrawingSpace()
        {
            throw new NotImplementedException();
        }

        public override void ResetUsedElements()
        {
            PocketPaintApplication.GetInstance().EraserCanvas.Visibility = Visibility.Collapsed;
        }
    }
}
