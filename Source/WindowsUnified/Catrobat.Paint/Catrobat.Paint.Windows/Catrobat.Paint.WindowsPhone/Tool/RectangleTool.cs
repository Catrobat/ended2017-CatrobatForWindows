using Catrobat.Paint.WindowsPhone.Command;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    class RectangleTool : ToolBase
    {
        private Path _path;

        public RectangleTool()
        {
            this.ToolType = ToolType.Rect;
        }

        public override void HandleDown(object arg)
        {

        }

        public override void HandleMove(object arg)
        {
        }

        public override void HandleUp(object arg)
        {
        }

        public override void Draw(object o)
        {
            var strokeThickness = PocketPaintApplication.GetInstance().PaintData.strokeThickness;

            Rectangle rectangleToDraw = PocketPaintApplication.GetInstance().RectangleSelectionControl.rectangleToDraw;

            var coordinate = (Point)o;
            //coordinate.X += strokeThickness / 2.0;
            //coordinate.Y += strokeThickness / 2.0;
            //coordinate.X -= (ellipseToDraw.Width / 2.0);
            //coordinate.Y -= (ellipseToDraw.Height / 2.0);

            double width = rectangleToDraw.Width;
            double height = rectangleToDraw.Height;
            width -= strokeThickness;
            height -= strokeThickness;

            Rect rect = new Rect();

            var angle = PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation;

            switch (angle)
            {
                case 0:
                case 180:
                    rect.Width = width;
                    rect.Height = height;
                    break;
                case 90:
                case 270:
                    rect.Width = height;
                    rect.Height = width;
                    break;
            }

            
            rect.X = coordinate.X - width / 2.0;
            rect.Y = coordinate.Y - height / 2.0;

            RectangleGeometry myRectGeometry = new RectangleGeometry();
            myRectGeometry.Rect = rect;

            RotateTransform lastRotateTransform = PocketPaintApplication.GetInstance().RectangleSelectionControl.getLastRotateTransformation();
            if (lastRotateTransform != null)
            {
                RotateTransform rotateTransform = new RotateTransform();
                rotateTransform.CenterX = coordinate.X;
                rotateTransform.CenterY = coordinate.Y;
                rotateTransform.Angle = lastRotateTransform.Angle;

                myRectGeometry.Transform = rotateTransform;
            }

            _path = new Path();
            _path.Fill = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            _path.Stroke = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
            _path.StrokeThickness = strokeThickness;
            _path.StrokeLineJoin = PocketPaintApplication.GetInstance().RectangleSelectionControl.strokeLineJoinOfRectangleToDraw;
            _path.Data = myRectGeometry;
            PocketPaintApplication.GetInstance().PaintingAreaView.addElementToPaintingAreCanvas(_path);

            var rectangleGeometry = new RectangleGeometry
            {
                Rect = new Rect(0, 0, PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth,
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualHeight)
            };
            _path.Clip = rectangleGeometry;
            _path.InvalidateArrange();
            _path.InvalidateMeasure();

            CommandManager.GetInstance().CommitCommand(new RectangleCommand(_path));
        }

        public override void ResetDrawingSpace()
        {
            PocketPaintApplication.GetInstance().RectangleSelectionControl.resetRectangleSelectionControl();
        }
    }
}
