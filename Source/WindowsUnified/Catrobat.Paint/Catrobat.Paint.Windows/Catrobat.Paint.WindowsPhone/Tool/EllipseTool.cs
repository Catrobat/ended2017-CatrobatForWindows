using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    class EllipseTool : ToolBase
    {
        private TransformGroup _transforms;

        public EllipseTool()
        {
            this.ToolType = ToolType.Ellipse;

            if (PocketPaintApplication.GetInstance() != null && PocketPaintApplication.GetInstance().PaintingAreaView != null)
            {
                PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityOfUcEllipseSelectionControl = Visibility.Visible;
            }
        }

        public override void HandleDown(object arg)
        {

        }

        public override void HandleMove(object arg)
        {
            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform = (RotateTransform)arg;

            TranslateTransform lastTranslateTransform = PocketPaintApplication.GetInstance().EllipseSelectionControl.getLastTranslateTransformation();
            RotateTransform lastRotateTransform = PocketPaintApplication.GetInstance().EllipseSelectionControl.getLastRotateTransformation();

            if (lastTranslateTransform != null && lastRotateTransform == null)
            {
                TranslateTransform originTranslateTransform = new TranslateTransform();
                originTranslateTransform.X = (lastTranslateTransform.X * -1.0);
                originTranslateTransform.Y = (lastTranslateTransform.Y * -1.0);

                PocketPaintApplication.GetInstance().EllipseSelectionControl.addTransformation(originTranslateTransform);
                PocketPaintApplication.GetInstance().EllipseSelectionControl.addTransformation(rotateTransform);
                PocketPaintApplication.GetInstance().EllipseSelectionControl.addTransformation(lastTranslateTransform);
            }
            else if (lastTranslateTransform == null && lastRotateTransform != null)
            {
                rotateTransform.Angle += lastRotateTransform.Angle;
                PocketPaintApplication.GetInstance().EllipseSelectionControl.addTransformation(rotateTransform);
            }
            else if (lastTranslateTransform != null && lastRotateTransform != null)
            {
                TranslateTransform originTranslateTransform = new TranslateTransform();
                originTranslateTransform.X = (lastTranslateTransform.X * -1.0);
                originTranslateTransform.Y = (lastTranslateTransform.Y * -1.0);

                rotateTransform.Angle += lastRotateTransform.Angle;

                PocketPaintApplication.GetInstance().EllipseSelectionControl.addTransformation(originTranslateTransform);
                PocketPaintApplication.GetInstance().EllipseSelectionControl.addTransformation(rotateTransform);
                PocketPaintApplication.GetInstance().EllipseSelectionControl.addTransformation(lastTranslateTransform);
            }
            else
            {
                PocketPaintApplication.GetInstance().EllipseSelectionControl.addTransformation(rotateTransform);
            }          
        }

        public override void HandleUp(object arg)
        {

        }

        public override void Draw(object o)
        {
            var strokeThickness = PocketPaintApplication.GetInstance().PaintData.strokeThickness;

            Ellipse ellipseToDraw = PocketPaintApplication.GetInstance().EllipseSelectionControl.ellipseToDraw;

            var coordinate = (Point)o;
            //coordinate.X += strokeThickness / 2.0;
            //coordinate.Y += strokeThickness / 2.0;
            //coordinate.X -= (ellipseToDraw.Width / 2.0);
            //coordinate.Y -= (ellipseToDraw.Height / 2.0);

            double width = ellipseToDraw.Width;
            double height = ellipseToDraw.Height;
            width -= strokeThickness;
            height -= strokeThickness;

            EllipseGeometry myEllipseGeometry = new EllipseGeometry();
            myEllipseGeometry.Center = new Point(coordinate.X, coordinate.Y);
            myEllipseGeometry.RadiusX = width / 2.0;
            myEllipseGeometry.RadiusY = height / 2.0;

            RotateTransform lastRotateTransform = PocketPaintApplication.GetInstance().EllipseSelectionControl.getLastRotateTransformation();
            if (lastRotateTransform != null)
            {
                RotateTransform rotateTransform = new RotateTransform();
                rotateTransform.CenterX = coordinate.X;
                rotateTransform.CenterY = coordinate.Y;
                rotateTransform.Angle = lastRotateTransform.Angle;

                myEllipseGeometry.Transform = rotateTransform;
            }

            Path _path = new Path();
            _path.Fill = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            _path.Stroke = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
            _path.StrokeThickness = strokeThickness;
            _path.StrokeLineJoin = PocketPaintApplication.GetInstance().EllipseSelectionControl.strokeLineJoinOfRectangleToDraw;
            _path.Data = myEllipseGeometry;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(_path);
        }

        public override void ResetDrawingSpace()
        {
            PocketPaintApplication.GetInstance().EllipseSelectionControl.resetEllipseSelectionControl();
        }
    }
}
