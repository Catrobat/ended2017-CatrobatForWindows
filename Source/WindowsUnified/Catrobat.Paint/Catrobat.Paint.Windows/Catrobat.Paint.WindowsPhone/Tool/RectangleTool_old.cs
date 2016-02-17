using Catrobat.Paint.WindowsPhone.Command;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    // Dient nur als Nachlagewerk
    class RectangleToolOld : ToolBase
    {
        // private Path _path;

        public RectangleToolOld()
        {
            this.ToolType = ToolType.Rect;
            // _path = null;
            if (PocketPaintApplication.GetInstance() != null && PocketPaintApplication.GetInstance().PaintingAreaView != null)
            {
                PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityOfUcRectangleSelectionControl = Visibility.Visible;
            }
        }

        public override void HandleDown(object arg)
        {

        }

        public override void HandleMove(object arg)
        {
            //RotateTransform rotateTransform = new RotateTransform();
            //rotateTransform = (RotateTransform)arg;

            //TranslateTransform lastTranslateTransform = PocketPaintApplication.GetInstance().RectangleSelectionControl.getLastTranslateTransformation();
            //RotateTransform lastRotateTransform = PocketPaintApplication.GetInstance().RectangleSelectionControl.getLastRotateTransformation();

            //if (lastTranslateTransform != null && lastRotateTransform == null)
            //{
            //    TranslateTransform originTranslateTransform = new TranslateTransform();
            //    originTranslateTransform.X = (lastTranslateTransform.X * -1.0);
            //    originTranslateTransform.Y = (lastTranslateTransform.Y * -1.0);

            //    PocketPaintApplication.GetInstance().RectangleSelectionControl.addTransformation(originTranslateTransform);
            //    PocketPaintApplication.GetInstance().RectangleSelectionControl.addTransformation(rotateTransform);
            //    PocketPaintApplication.GetInstance().RectangleSelectionControl.addTransformation(lastTranslateTransform);
            //}
            //else if (lastTranslateTransform == null && lastRotateTransform != null)
            //{
            //    rotateTransform.Angle += lastRotateTransform.Angle;
            //    PocketPaintApplication.GetInstance().RectangleSelectionControl.addTransformation(rotateTransform);
            //}
            //else if (lastTranslateTransform != null && lastRotateTransform != null)
            //{
            //    TranslateTransform originTranslateTransform = new TranslateTransform();
            //    originTranslateTransform.X = (lastTranslateTransform.X * -1.0);
            //    originTranslateTransform.Y = (lastTranslateTransform.Y * -1.0);

            //    rotateTransform.Angle += lastRotateTransform.Angle;

            //    PocketPaintApplication.GetInstance().RectangleSelectionControl.addTransformation(originTranslateTransform);
            //    PocketPaintApplication.GetInstance().RectangleSelectionControl.addTransformation(rotateTransform);
            //    PocketPaintApplication.GetInstance().RectangleSelectionControl.addTransformation(lastTranslateTransform);
            //}
            //else
            //{
            //    PocketPaintApplication.GetInstance().RectangleSelectionControl.addTransformation(rotateTransform);
            //}          
        }

        public override void HandleUp(object arg)
        {
        }

        public override void Draw(object o)
        {
            //var strokeThickness = PocketPaintApplication.GetInstance().PaintData.strokeThickness;

            //Rectangle rectangleToDraw = PocketPaintApplication.GetInstance().RectangleSelectionControl.rectangleToDraw;

            //var coordinate = (Point)o;
            //coordinate.X += strokeThickness / 2.0;
            //coordinate.Y += strokeThickness / 2.0;
            //coordinate.X -= (rectangleToDraw.Width / 2.0);
            //coordinate.Y -= (rectangleToDraw.Height / 2.0);

            //double width = rectangleToDraw.Width;
            //double height = rectangleToDraw.Height;
            //width -= strokeThickness;
            //height -= strokeThickness;

            //RectangleGeometry myRectangleGeometry = new RectangleGeometry();
            //myRectangleGeometry.Rect = new Rect(coordinate, new Point(coordinate.X + width, coordinate.Y + height));

            //RotateTransform lastRotateTransform = PocketPaintApplication.GetInstance().RectangleSelectionControl.getLastRotateTransformation();
            //if (lastRotateTransform != null)
            //{
            //    RotateTransform rotateTransform = new RotateTransform();
            //    rotateTransform.CenterX = coordinate.X + myRectangleGeometry.Rect.Width / 2.0;
            //    rotateTransform.CenterY = coordinate.Y + myRectangleGeometry.Rect.Height / 2.0;
            //    rotateTransform.Angle = lastRotateTransform.Angle;

            //    myRectangleGeometry.Transform = rotateTransform;
            //}

            //_path = new Path();
            //_path.Fill = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            //_path.Stroke = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
            //_path.StrokeThickness = strokeThickness;
            //_path.StrokeLineJoin = PocketPaintApplication.GetInstance().RectangleSelectionControl.strokeLineJoinOfRectangleToDraw;
            //_path.Data = myRectangleGeometry;
            //PocketPaintApplication.GetInstance().PaintingAreaView.addElementToPaintingAreCanvas(_path);

            //var rectangleGeometry = new RectangleGeometry
            //{
            //    Rect = new Rect(0, 0, PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth,
            //    PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualHeight)
            //};
            //_path.Clip = rectangleGeometry;
            //_path.InvalidateArrange();
            //_path.InvalidateMeasure();

            //CommandManager.GetInstance().CommitCommand(new RectangleCommand(_path)); 
        }

        public override void ResetDrawingSpace()
        {
            //PocketPaintApplication.GetInstance().RectangleSelectionControl.resetRectangleSelectionControl();
        }

        public override void ResetUsedElements()
        {
        }
    }
}
