using Catrobat.Paint.WindowsPhone.Command;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    class EllipseTool : RectangleShapeBaseTool
    {
        private Path m_path;

        public EllipseTool()
        {
            m_path = null;

            this.ToolType = ToolType.Ellipse;
            this.RectangleShapeBase = PocketPaintApplication.GetInstance().RectangleSelectionControl
                .RectangleShapeBase;

            if (PocketPaintApplication.GetInstance() != null 
                && PocketPaintApplication.GetInstance().PaintingAreaView != null)
            {
                PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityOfUcEllipseSelectionControl 
                    = Visibility.Visible;
            }
        }

        public override void Draw(object o)
        {
            var strokeThickness = PocketPaintApplication.GetInstance().PaintData.strokeThickness;

            Ellipse ellipseToDraw = PocketPaintApplication.GetInstance().EllipseSelectionControl.EllipseToDraw;

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

            RotateTransform lastRotateTransform = this.RectangleShapeBase.GetLastRotateTransformation();
            if (lastRotateTransform != null)
            {
                RotateTransform rotateTransform = new RotateTransform();
                rotateTransform.CenterX = coordinate.X;
                rotateTransform.CenterY = coordinate.Y;
                rotateTransform.Angle = lastRotateTransform.Angle;

                myEllipseGeometry.Transform = rotateTransform;
            }

            m_path = new Path();
            m_path.Fill = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            m_path.Stroke = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
            m_path.StrokeThickness = strokeThickness;
            m_path.StrokeLineJoin = PocketPaintApplication.GetInstance().EllipseSelectionControl
                .StrokeLineJoinOfEllipseToDraw;
            m_path.Data = myEllipseGeometry;
            PocketPaintApplication.GetInstance().PaintingAreaView.addElementToPaintingAreCanvas(m_path);

            var rectangleGeometry = new RectangleGeometry
            {
                Rect = new Rect(0, 0, PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth,
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualHeight)
            };
            m_path.Clip = rectangleGeometry;
            m_path.InvalidateArrange();
            m_path.InvalidateMeasure();

            CommandManager.GetInstance().CommitCommand(new EllipseCommand(m_path)); 
        }

        public override void ResetDrawingSpace()
        {
            PocketPaintApplication.GetInstance().EllipseSelectionControl.resetEllipseSelectionControl();
        }

    }
}
