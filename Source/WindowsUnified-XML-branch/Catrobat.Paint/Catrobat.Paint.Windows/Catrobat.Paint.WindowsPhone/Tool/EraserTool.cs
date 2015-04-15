using Catrobat.Paint.WindowsPhone.Command;
using System;
using Windows.Foundation;
using Windows.UI;
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

        public EraserTool()
        {
            ToolType = ToolType.Eraser;
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
            _pathFigureCollection = new PathFigureCollection();
            _pathFigure = new PathFigure();
            _pathSegmentCollection = new PathSegmentCollection();

            _path.StrokeLineJoin = PenLineJoin.Round;
            _path.Stroke = new SolidColorBrush(Color.FromArgb(1,255,255,255));
            _path.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.thicknessSelected;
            _path.StrokeStartLineCap = PocketPaintApplication.GetInstance().PaintData.penLineCapSelected;
            _path.StrokeEndLineCap = PocketPaintApplication.GetInstance().PaintData.penLineCapSelected;

            _pathFigure.StartPoint = coordinate;
            _pathFigure.Segments = _pathSegmentCollection;
            _pathFigureCollection.Add(_pathFigure);
            _pathGeometry.Figures = _pathFigureCollection;
            _lastPoint = coordinate;
            _path.Data = _pathGeometry;
            PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying.Children.Add(_path);

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
            System.Diagnostics.Debug.WriteLine("CropTool Coord: " + coordinate.X + " " + coordinate.Y);

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

                DeletePixels();
            }
        }

        public override void Draw(object o)
        {
        }

        public override void ResetDrawingSpace()
        {
            throw new NotImplementedException();
        }
        
        // performance critical... doing some optimizations
        async private void DeletePixels()
        {
            PixelData.PixelData pixelData = new PixelData.PixelData();
            await pixelData.preparePaintingAreaCanvasPixel();
            await pixelData.preparePaintingAreaCanvasForEraser();
          for(int colCounter = 0; colCounter < PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying.ActualHeight; colCounter++)
          {
              for(int rowCounter = 0; rowCounter < PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying.ActualWidth; rowCounter++)
              {
                  if(pixelData.getPixelAlphaFromCanvasEraser(rowCounter, colCounter) != 0)
                  {
                      pixelData.setPixelColor(rowCounter, colCounter);
                  }
              }
          }
          pixelData.changedPixelToCanvas();
        }

    }
}
