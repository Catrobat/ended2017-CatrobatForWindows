using Catrobat.Paint.WindowsPhone.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
// TODO: using Catrobat.Paint.Phone.Command;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone.Tool
{
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
            points = new List<Point>();
            pixelData = new PixelData.PixelData();
            
        }

        async public override void HandleDown(object arg)
        {
            if (!(arg is Point))
            {
                return;
            }

            await pixelData.preparePaintingAreaCanvasPixel();
            pixelDataEraser = new PixelData.PixelData();


            var coordinate = (Point)arg;

            _path = new Path();
            _pathGeometry = new PathGeometry();
            _pathFigureCollection = new PathFigureCollection();
            _pathFigure = new PathFigure();
            _pathSegmentCollection = new PathSegmentCollection();

            _path.StrokeLineJoin = PenLineJoin.Round;
            _path.Stroke = new SolidColorBrush(Color.FromArgb(0xff,0xff,0xff,0xff));
            _path.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.thicknessSelected;
            _path.StrokeStartLineCap = PocketPaintApplication.GetInstance().PaintData.penLineCapSelected;
            _path.StrokeEndLineCap = PocketPaintApplication.GetInstance().PaintData.penLineCapSelected;

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

            //var rectangleGeometry = new RectangleGeometry
            //{
            //    Rect = new Rect(0, 0, PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth,
            //    PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualHeight)
            //};
            //_path.Clip = rectangleGeometry;

            //_path.InvalidateArrange();
            //_path.InvalidateMeasure();
        }

        async public override void HandleMove(object arg)
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


                /*var list = GetPointsOnLine((int)_lastPoint.X, (int)_lastPoint.Y, (int)coordinate.X, (int)coordinate.Y);

                List<Point> yolo = new List<Point>();
                Debug.WriteLine(coordinate.ToString() + " " + _lastPoint.ToString());
                foreach (var item in list)
                {
                    Debug.WriteLine(item.ToString());
                    yolo.Add(item);
                }
                

                List<Point> temp = null;
                temp = AddPointRange(yolo,20);

                foreach (var item in temp)
                {
                    yolo.Add(item);
                }

                data.SetPixel(yolo, "0_0_0_0");
                yolo.Clear();*/

                _pathSegmentCollection.Add(qbs);


                PocketPaintApplication.GetInstance().PaintingAreaLayoutRoot.InvalidateMeasure();
                _lastPointSet = false;
            }
            /*await pixelDataEraser.preparePaintingAreaCanvasForEraser();
            points = pixelDataEraser.GetWhitePixels();*/
            

/*
            PixelData.PixelData pixelData = new PixelData.PixelData();
            await pixelData.preparePaintingAreaCanvasPixel();


            var pixelCanvas = pixelData.pixelsCanvas;


            pixelData.SetPixel(coordinate, "");
            

            var image = await pixelData.BufferToImage();
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(image);*/

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

            //DeletePointFromPaintingAreaCanvas();
            await pixelDataEraser.preparePaintingAreaCanvasForEraser();
            points = pixelDataEraser.GetWhitePixels();
            pixelData.SetPixel(points, "0_0_0_0");
            PocketPaintApplication.GetInstance().EraserCanvas.Visibility = Visibility.Collapsed;
            PocketPaintApplication.GetInstance().EraserCanvas.Children.Clear();
            var image = await pixelData.BufferToImage();
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(image);

            CommandManager.GetInstance().CommitCommand(new EraserCommand(_path));
        }

        public override void Draw(object o)
        {
        }

        public override void ResetDrawingSpace()
        {
            throw new NotImplementedException();
        }
        
        // performance critical... doing some optimizations

    }
}
