using Windows.UI.Xaml.Media;

namespace Catrobat.Paint.WindowsPhone.Command
{
    class RotateCommand : CommandBase
    {
        private TransformGroup _rotateTransformGroup;
        private int _angle = 0;
        private int _rotationDirection = 0;

        public RotateCommand(TransformGroup rotateTransformGroup, int angle, int rotationDirection)
        {
            _rotateTransformGroup = rotateTransformGroup;
            _angle = angle;
            _rotationDirection = rotationDirection;
        }

        public override bool ReDo()
        {
            TransformGroup transformGroup = ((TransformGroup)PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform);
            transformGroup.Children.Clear();
            for (int i = 0; i < _rotateTransformGroup.Children.Count; i++)
            {
                transformGroup.Children.Add(_rotateTransformGroup.Children[i]);
            }

            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform = transformGroup;
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.InvalidateMeasure();
            PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation = _angle;
            PocketPaintApplication.GetInstance().PaintingAreaView.enableResetButtonRotate(_rotationDirection);
            return true;
        }

        public override bool UnDo()
        {
            TransformGroup paintingAreaCheckeredTransformGroup = ((TransformGroup)PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform);

            paintingAreaCheckeredTransformGroup.Children.Clear();

            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform = paintingAreaCheckeredTransformGroup;
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.InvalidateMeasure();

            PocketPaintApplication.GetInstance().PaintingAreaView.setSizeOfPaintingAreaViewCheckered();
            PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation -= _angle;
            PocketPaintApplication.GetInstance().PaintingAreaView.enableResetButtonRotate(PocketPaintApplication.GetInstance().PaintingAreaView.getRotationCounter() * (-1));
            return true;
        }
    }
}
