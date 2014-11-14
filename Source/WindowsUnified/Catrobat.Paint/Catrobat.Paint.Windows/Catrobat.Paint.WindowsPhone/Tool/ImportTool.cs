using Catrobat.Paint.Phone;
using Catrobat.Paint.Phone.Tool;
using Catrobat.Paint.WindowsPhone.Controls.UserControls;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
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
            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform = (RotateTransform)arg;

            RotateTransform lastRotateTransform = getLastRotateTransformation();
            //_transforms.Children.Add(rotateTransform);

            if (lastRotateTransform != null)
            {
                rotateTransform.Angle += lastRotateTransform.Angle;
            }
            addTransformation(rotateTransform);

            //_transforms = ((RotateTransform)arg);

        }

        public override void HandleUp(object arg)
        {

        }

        public override void Draw(object o)
        {
            var strokeThickness = PocketPaintApplication.GetInstance().PaintData.strokeThickness;

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
            berriesBrush.ImageSource = PocketPaintApplication.GetInstance().ImportImageSelectionControl.imageSourceOfRectangleToDraw.ImageSource;
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

        public RotateTransform getLastRotateTransformation()
        {
            for (int i = 0; i < _transforms.Children.Count; i++)
            {
                if (_transforms.Children[i].GetType() == typeof(RotateTransform))
                {
                    RotateTransform rotateTransform = new RotateTransform();
                    rotateTransform.CenterX = ((RotateTransform)_transforms.Children[i]).CenterX;
                    rotateTransform.CenterY = ((RotateTransform)_transforms.Children[i]).CenterY;
                    rotateTransform.Angle = ((RotateTransform)_transforms.Children[i]).Angle;

                    return rotateTransform;
                }
            }

            return null;
        }

        public void addTransformation(Transform currentTransform)
        {
            for (int i = 0; i < _transforms.Children.Count; i++)
            {
                if (_transforms.Children[i].GetType() == currentTransform.GetType())
                {
                    _transforms.Children.RemoveAt(i);
                }
            }
            _transforms.Children.Add(currentTransform);
        }
    }
}
