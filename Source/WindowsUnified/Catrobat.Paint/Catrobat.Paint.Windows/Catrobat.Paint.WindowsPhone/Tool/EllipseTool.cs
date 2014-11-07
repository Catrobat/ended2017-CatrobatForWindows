using Catrobat.Paint.Phone;
using Catrobat.Paint.Phone.Tool;
using Catrobat.Paint.WindowsPhone.Controls.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    class EllipseTool : ToolBase
    {
        private TransformGroup _transforms;

        public EllipseTool(ToolType toolType = ToolType.Ellipse)
        {
            ToolType = toolType;

            if (PocketPaintApplication.GetInstance() != null && PocketPaintApplication.GetInstance().PaintingAreaView != null)
            {
                PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityOfGridEllipseSelectionControl = Visibility.Visible;

                if (PocketPaintApplication.GetInstance().GridEllipseSelectionControl.RenderTransform != null)
                {
                    _transforms = PocketPaintApplication.GetInstance().GridEllipseSelectionControl.RenderTransform as TransformGroup;
                }
                if (_transforms == null)
                {
                    PocketPaintApplication.GetInstance().GridEllipseSelectionControl.RenderTransform = _transforms = new TransformGroup();
                }
            }
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
            var strokeThickness = PocketPaintApplication.GetInstance().PaintData.strokeThicknessRecEll;

            var coordinate = (Point)o;
            //coordinate.X += strokeThickness;
            //coordinate.Y += strokeThickness;

            double height = PocketPaintApplication.GetInstance().BarRecEllShape.getHeight();
            double width = PocketPaintApplication.GetInstance().BarRecEllShape.getWidth();
            height -= strokeThickness;
            width -= strokeThickness;

            EllipseGeometry myEllipseGeometry = new EllipseGeometry();
            myEllipseGeometry.Center = new Point(coordinate.X, coordinate.Y);
            myEllipseGeometry.RadiusX = width / 2.0;
            myEllipseGeometry.RadiusY = height / 2.0;

            Path _path = new Path();
            _path.Fill = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            _path.Stroke = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
            _path.StrokeThickness = strokeThickness;
            _path.StrokeLineJoin = PocketPaintApplication.GetInstance().EllipseSelectionControl.strokeLineJoinOfEllipseToDraw;

            _path.Data = myEllipseGeometry;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(_path);
        }

        public override void ResetDrawingSpace()
        {
            PocketPaintApplication.GetInstance().GridEllipseSelectionControl.Children.Clear();
            PocketPaintApplication.GetInstance().GridEllipseSelectionControl.Children.Add(new EllipseSelectionControl());
            PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = 160.0;
            PocketPaintApplication.GetInstance().BarRecEllShape.setBtnWidthValue = 160.0;
            PocketPaintApplication.GetInstance().EllipseSelectionControl.setIsModifiedRectangleMovement = false;
        }
    }
}
