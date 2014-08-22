using System;
using System.Windows;
// TODO: using Catrobat.Paint.Phone.Command;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI;
using Catrobat.Paint.WindowsPhone.Tool;
using Catrobat.Paint.Phone.Command;

namespace Catrobat.Paint.Phone.Tool
{
    class EraserTool : ToolBase
    {
        private Path _path;
        private PathGeometry _pathGeometry;
        private PathFigureCollection _pathFigureCollection;
        private PathFigure _pathFigure;
        private PathSegmentCollection _pathSegmentCollection;
        private Point _lastPoint;
        private Point _startPoint;
        private bool _lastPointSet;
        private WriteableBitmap _bitmapTemp;
        private readonly Color _colorEmpty = new Color { A = 0x00, B = 0x00, G = 0x00, R = 0x00 };

        public EraserTool(ToolType toolType = ToolType.Eraser)
        {
            ToolType = toolType;
            ResetCanvas();
        }



        public override void HandleDown(object arg)
        {
            if (!(arg is Point))
            {
                return;
            }

            var coordinate = (Point)arg;

            if (NeedToResetCanvas)
            {
                ResetCanvas();
            }

            _path = new Path();
            _pathGeometry = new PathGeometry();
            _path.StrokeLineJoin = PenLineJoin.Bevel;

            _path.Stroke = new SolidColorBrush(Colors.Transparent);
            _path.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.ThicknessSelected;
            _path.StrokeEndLineCap = PocketPaintApplication.GetInstance().PaintData.CapSelected;
            _path.StrokeStartLineCap = PocketPaintApplication.GetInstance().PaintData.CapSelected;

            _path.Data = _pathGeometry;
            _pathFigureCollection = new PathFigureCollection();
            _pathGeometry.Figures = _pathFigureCollection;
            _pathFigure = new PathFigure();

            _pathFigureCollection.Add(_pathFigure);
            _lastPoint = coordinate;
            _startPoint = coordinate;
            _pathFigure.StartPoint = _lastPoint;
            _pathSegmentCollection = new PathSegmentCollection();
            _pathFigure.Segments = _pathSegmentCollection;

            PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying.Children.Add(_path);

            //            var transform = PocketPaintApplication.GetInstance().PaintingAreaCanvas.TransformToVisual(PocketPaintApplication.GetInstance().PaintingAreaLayoutRoot);
            //            var absolutePosition = transform.Transform(new Point(0, 0));
            var r = new RectangleGeometry
            {
                Rect = new Rect(0, 0, PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying.ActualWidth,
                PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying.ActualHeight)
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

            if (!_lastPointSet)
            {
                _lastPoint = coordinate;
                _lastPointSet = true;
            }
            if (_lastPointSet && !_lastPoint.Equals(coordinate))
            {
                var qbs = new QuadraticBezierSegment
                {
                    Point1 = _lastPoint,
                    Point2 = coordinate
                };

                _pathSegmentCollection.Add(qbs);

                DeletePixels(_startPoint, coordinate);
                _startPoint = coordinate;
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
            }


            DeletePixels(_startPoint, coordinate, true);

            CommandManager.GetInstance().CommitCommand(new EraserCommand(_path));
        }

        public override void Draw(object o)
        {
            var path = o as Path;
            if (path != null)
            {
                _path = path;
                PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying.Children.Add(_path);
                DeletePixels(new Point(), new Point(), true);
            }

            
        }

        public override void ResetDrawingSpace()
        {
            throw new NotImplementedException();
        }
        
        // performance critical... doing some optimizations
        private void DeletePixels(Point a, Point b, bool allPixels = false)
        {
            _path.Stroke = new SolidColorBrush(Colors.Black);
            var n = new TranslateTransform();
            Point grid_size = new Point();
            grid_size.X = PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.Width;
            grid_size.Y = PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.Height;

            _bitmapTemp = new WriteableBitmap(Convert.ToInt32(grid_size.X), Convert.ToInt32(grid_size.Y));
            
            _path.Stroke = new SolidColorBrush(Colors.Transparent);
            var c = _colorEmpty;

        
            // we do not need to walk over the whole bitmap 
            // we build a "clip rectangle" and we just walk over that pixels
            // 
            // xmin|ymin --------------------
            // |                            |
            // |                            |
            // |                            |
            // -------------------- xmax|ymax
            int xmin, xmax, ymin, ymax;

            if (allPixels)
            {
                xmin = 0;
                xmax = _bitmapTemp.PixelWidth;
                ymin = 0;
                ymax = _bitmapTemp.PixelHeight;
            }
            else
            {

                if (a.X < b.X)
                {
                    xmin = (int)(a.X - Convert.ToInt32(_path.StrokeThickness / 2) - 5);
                    if (xmin < 0)
                        xmin = 0;

                    xmax = (int)(b.X + Convert.ToInt32(_path.StrokeThickness / 2) + 5);
                    if (xmax > _bitmapTemp.PixelWidth)
                        xmax = _bitmapTemp.PixelWidth;
                }
                else
                {
                    xmin = (int)(b.X - Convert.ToInt32(_path.StrokeThickness / 2) - 5);
                    if (xmin < 0)
                        xmin = 0;

                    xmax = (int)(a.X + Convert.ToInt32(_path.StrokeThickness / 2) + 5);
                    if (xmax > _bitmapTemp.PixelWidth)
                        xmax = _bitmapTemp.PixelWidth;
                }

                if (a.Y < b.Y)
                {
                    ymin = (int)(a.Y - Convert.ToInt32(_path.StrokeThickness / 2) - 5);
                    if (ymin < 0)
                        ymin = 0;

                    ymax = (int)(b.Y + Convert.ToInt32(_path.StrokeThickness / 2) + 5);
                    if (ymax > _bitmapTemp.PixelHeight)
                        ymax = _bitmapTemp.PixelHeight;
                }
                else
                {
                    ymin = (int)(b.Y - Convert.ToInt32(_path.StrokeThickness / 2) - 5);
                    if (ymin < 0)
                        ymin = 0;

                    ymax = (int)(a.Y + Convert.ToInt32(_path.StrokeThickness / 2) + 5);
                    if (ymax > _bitmapTemp.PixelHeight)
                        ymax = _bitmapTemp.PixelHeight;
                }

            }
            
            // I read that GetBitmapContext gets ride of some overhead and has performance improvements 
            // (but I experienced them as rather low :) )
           /*s using (_bitmapTemp.GetBitmapContext())
            {
                using (PocketPaintApplication.GetInstance().Bitmap.GetBitmapContext())
                {
                    for (var x = xmin; x < xmax; x++)
                    {
                        for (var y = ymin; y < ymax; y++)
                        {
                            if (_bitmapTemp.GetPixel(x, y) == Colors.Black)
                            {
                                PocketPaintApplication.GetInstance().Bitmap.SetPixel(x, y, c);
                            }
                        }
                    }
                }
            }*/

            PocketPaintApplication.GetInstance().PaintingAreaLayoutRoot.InvalidateMeasure();
        }

    }
}
