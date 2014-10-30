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
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    class RectangleTool : ToolBase
    {
        private TransformGroup _transforms;

        public RectangleTool()
        {
            this.ToolType = ToolType.Rect;

            if (PocketPaintApplication.GetInstance() != null && PocketPaintApplication.GetInstance().PaintingAreaView != null)
            {
                PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityOfGridRectangleSelectionControl = Visibility.Visible;

                if (PocketPaintApplication.GetInstance().GridRectangleSelectionControl.RenderTransform != null)
                {
                    _transforms = PocketPaintApplication.GetInstance().GridRectangleSelectionControl.RenderTransform as TransformGroup;
                }
                if (_transforms == null)
                {
                    PocketPaintApplication.GetInstance().GridRectangleSelectionControl.RenderTransform = _transforms = new TransformGroup();
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
            var borderThickness = PocketPaintApplication.GetInstance().PaintData.BorderThicknessRecEll;

            var coordinate = (Point)o;
            coordinate.X += borderThickness / 2.0;
            coordinate.Y += borderThickness / 2.0;

            double height = PocketPaintApplication.GetInstance().BarRecEllShape.getHeight();
            double width = PocketPaintApplication.GetInstance().BarRecEllShape.getWidth();
            height -= borderThickness;
            width -= borderThickness;

            RectangleGeometry myRectangleGeometry = new RectangleGeometry();
            myRectangleGeometry.Rect = new Rect(coordinate, new Point(coordinate.X + width, coordinate.Y + height));
            
            
            Path _path = new Path();
            _path.Fill = PocketPaintApplication.GetInstance().PaintData.ColorSelected;
            _path.Stroke = PocketPaintApplication.GetInstance().PaintData.BorderColorSelected;
            _path.StrokeThickness = borderThickness;
            _path.StrokeLineJoin = PocketPaintApplication.GetInstance().RectangleSelectionControl.strokeLineJoinOfRectangleToDraw;

            _path.Data = myRectangleGeometry;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(_path);
        }

        public override void ResetDrawingSpace()
        {
            PocketPaintApplication.GetInstance().GridRectangleSelectionControl.Children.Clear();
            PocketPaintApplication.GetInstance().GridRectangleSelectionControl.Children.Add(new RectangleSelectionControl());
            PocketPaintApplication.GetInstance().BarRecEllShape.setContentHeightValue = 160.0;
            PocketPaintApplication.GetInstance().BarRecEllShape.setTbWidthValue = 160.0;
        }
    }
}
