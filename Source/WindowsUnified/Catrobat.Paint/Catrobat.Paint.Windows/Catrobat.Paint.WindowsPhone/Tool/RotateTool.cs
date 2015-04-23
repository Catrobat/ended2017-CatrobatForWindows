using Catrobat.Paint.WindowsPhone.Command;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    class RotateTool : ToolBase
    {
        TransformGroup _rotationTransformGroup = null;

        public RotateTool()
        {
            this.ToolType = ToolType.Rotate;
            if (PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform.GetType() == typeof(TransformGroup))
            {
                _rotationTransformGroup = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            }
            if (_rotationTransformGroup == null)
            {
                PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform = _rotationTransformGroup = new TransformGroup();
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
            
        }

        public override void ResetDrawingSpace()
        {
            var rotateTransform = new RotateTransform();
            PocketPaintApplication.GetInstance().angularDegreeOfWorkingsSpaceRotation = 0;

            rotateTransform.Angle = PocketPaintApplication.GetInstance().angularDegreeOfWorkingsSpaceRotation;
            rotateTransform.CenterX = (PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.Width) / 2;
            rotateTransform.CenterY = ((PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.Height) / 2);
            addTransformsToRotationTransformGroup(rotateTransform, 0, PocketPaintApplication.GetInstance().PaintingAreaView.getRotationCounter()*(-1));
            PocketPaintApplication.GetInstance().PaintingAreaView.enableResetButtonRotate(PocketPaintApplication.GetInstance().PaintingAreaView.getRotationCounter() * (-1));
        }

        public void proofBoundariesOfAngle(int angleValue)
        {
            PocketPaintApplication.GetInstance().angularDegreeOfWorkingsSpaceRotation += angleValue;

            if (PocketPaintApplication.GetInstance().angularDegreeOfWorkingsSpaceRotation == 360)
            {
                PocketPaintApplication.GetInstance().angularDegreeOfWorkingsSpaceRotation = 0;
            }
            else if (PocketPaintApplication.GetInstance().angularDegreeOfWorkingsSpaceRotation == -90)
            {
                PocketPaintApplication.GetInstance().angularDegreeOfWorkingsSpaceRotation = 270;
            }
        }

        public void createRotationTransformAndAddedItToTransformGroup(int angleRotation, int rotationDirection)
        {
            var rotateTransform = new RotateTransform();
            rotateTransform.Angle = PocketPaintApplication.GetInstance().angularDegreeOfWorkingsSpaceRotation;
            rotateTransform.CenterX = (PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.ActualWidth) / 2;
            rotateTransform.CenterY = ((PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.ActualHeight) / 2);

            addTransformsToRotationTransformGroup(rotateTransform, angleRotation, rotationDirection);
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.InvalidateMeasure();
        }

        public void RotateLeft()
        {
            int angleToRotate = -90;
            proofBoundariesOfAngle(angleToRotate);
            createRotationTransformAndAddedItToTransformGroup(angleToRotate, -1);
        }

        public void RotateRight()
        {
            int angleToRotate = 90;
            proofBoundariesOfAngle(angleToRotate);
            createRotationTransformAndAddedItToTransformGroup(angleToRotate, 1);
        }
        public void RotateRight(int angleToRotate)
        {
            proofBoundariesOfAngle(angleToRotate);
            createRotationTransformAndAddedItToTransformGroup(angleToRotate, 1);
        }

        private void addTransformsToRotationTransformGroup(RotateTransform rotateTransform, int angle, int rotationDirection)
        {
            double DISPLAY_WIDTH_HALF = Window.Current.Bounds.Width / 2.0;
            double DISPLAY_HEIGHT_HALF = Window.Current.Bounds.Height / 2.0;

            TransformGroup rotationTransformGroupForCommand = new TransformGroup();
            _rotationTransformGroup.Children.Clear();
            _rotationTransformGroup.Children.Add(rotateTransform);
            rotationTransformGroupForCommand.Children.Add(rotateTransform);

            ScaleTransform scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX = 0.75;
            scaleTransform.ScaleY = 0.75;
            scaleTransform.CenterX = DISPLAY_WIDTH_HALF;
            scaleTransform.CenterY = DISPLAY_HEIGHT_HALF;
            _rotationTransformGroup.Children.Add(scaleTransform);
            rotationTransformGroupForCommand.Children.Add(scaleTransform);

            if (PocketPaintApplication.GetInstance().angularDegreeOfWorkingsSpaceRotation == 90 || PocketPaintApplication.GetInstance().angularDegreeOfWorkingsSpaceRotation == 270)
            {
                scaleTransform = new ScaleTransform();
                scaleTransform.ScaleX = 0.75;
                scaleTransform.ScaleY = 0.75;
                scaleTransform.CenterX = DISPLAY_WIDTH_HALF;
                scaleTransform.CenterY = DISPLAY_HEIGHT_HALF;
                _rotationTransformGroup.Children.Add(scaleTransform);
                rotationTransformGroupForCommand.Children.Add(scaleTransform);
            }
            else
            {
                var toTranslateValue = new TranslateTransform();
                toTranslateValue.X = 0;
                toTranslateValue.Y -= 11.0;
                _rotationTransformGroup.Children.Add(toTranslateValue);
                rotationTransformGroupForCommand.Children.Add(toTranslateValue);
            }
            CommandManager.GetInstance().CommitCommand(new RotateCommand(rotationTransformGroupForCommand, PocketPaintApplication.GetInstance().angularDegreeOfWorkingsSpaceRotation, rotationDirection));
        }
    }
}
