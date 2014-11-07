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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    class ImportTool : ToolBase
    {
        private TransformGroup _transforms;

        public ImportTool()
        {
            this.ToolType = ToolType.ImportPng;

            if (PocketPaintApplication.GetInstance() != null && PocketPaintApplication.GetInstance().PaintingAreaView != null)
            {

                if (PocketPaintApplication.GetInstance().GridImportImageSelectionControl.RenderTransform != null)
                {
                    _transforms = PocketPaintApplication.GetInstance().GridImportImageSelectionControl.RenderTransform as TransformGroup;
                }
                if (_transforms == null)
                {
                    PocketPaintApplication.GetInstance().GridImportImageSelectionControl.RenderTransform = _transforms = new TransformGroup();
                }
            }
        }

        public override void HandleDown(object arg)
        {

        }

        public override void HandleMove(object arg)
        {
            // TODO:
            //RotateTransform rotateTransform = new RotateTransform();
            //rotateTransform = (RotateTransform)arg;
            //_transforms.Children.Add(rotateTransform);

            // _transforms = ((RotateTransform)arg);

        }

        public override void HandleUp(object arg)
        {

        }

        public override void Draw(object o)
        {
            var strokeThickness = PocketPaintApplication.GetInstance().PaintData.strokeThicknessRecEll;

            var coordinate = (Point)o;
            coordinate.X += strokeThickness / 2.0;
            coordinate.Y += strokeThickness / 2.0;

            double height = PocketPaintApplication.GetInstance().BarRecEllShape.getHeight();
            double width = PocketPaintApplication.GetInstance().BarRecEllShape.getWidth();
            height -= strokeThickness;
            width -= strokeThickness;

            RectangleGeometry myRectangleGeometry = new RectangleGeometry();
            myRectangleGeometry.Rect = new Rect(coordinate, new Point(coordinate.X + width, coordinate.Y + height));


            Path _path = new Path();
            ImageBrush berriesBrush = new ImageBrush();
            berriesBrush.ImageSource =
                new BitmapImage(
                    new Uri("ms-resource:/Files/Assets/test.jpg", UriKind.Absolute)
                );
            _path.Fill = berriesBrush;
            _path.Stroke = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
            _path.StrokeThickness = strokeThickness;
            _path.StrokeLineJoin = PocketPaintApplication.GetInstance().ImportImageSelectionControl.strokeLineJoinOfRectangleToDraw;

            _path.Data = myRectangleGeometry;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(_path);
        }

        public override void ResetDrawingSpace()
        {
            PocketPaintApplication.GetInstance().GridImportImageSelectionControl.Children.Clear();
            PocketPaintApplication.GetInstance().GridImportImageSelectionControl.Children.Add(new ImportImageSelectionControl());
            PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = 160.0;
            PocketPaintApplication.GetInstance().BarRecEllShape.setBtnWidthValue = 160.0;
            PocketPaintApplication.GetInstance().RectangleSelectionControl.isModifiedRectangleMovement = false;
        }
    }
}
