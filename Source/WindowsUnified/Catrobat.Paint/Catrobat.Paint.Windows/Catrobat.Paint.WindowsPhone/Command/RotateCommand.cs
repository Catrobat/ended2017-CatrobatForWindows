using Catrobat.Paint.WindowsPhone.Tool;
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
            ToolType = ToolType.Rotate;
            _rotateTransformGroup = rotateTransformGroup;
            _angle = angle;
            _rotationDirection = rotationDirection;
        }

        public override bool ReDo()
        {
            TransformGroup tgGridWorkingSpace = ((TransformGroup)PocketPaintApplication.GetInstance().GridWorkingSpace.RenderTransform);
            tgGridWorkingSpace.Children.Clear();
            for (int i = 0; i < _rotateTransformGroup.Children.Count; i++)
            {
                tgGridWorkingSpace.Children.Add(_rotateTransformGroup.Children[i]);
            }

            PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation = _angle;
            PocketPaintApplication.GetInstance().PaintingAreaView.alignPositionOfGridWorkingSpace(null);
            PocketPaintApplication.GetInstance().GridWorkingSpace.UpdateLayout();
            PocketPaintApplication.GetInstance().GridWorkingSpace.InvalidateArrange();
            PocketPaintApplication.GetInstance().GridWorkingSpace.InvalidateMeasure();
            PocketPaintApplication.GetInstance().PaintingAreaView.enableResetButtonRotate(_rotationDirection);
            return true;
        }

        public override bool UnDo()
        {
            TransformGroup paintingAreaCheckeredTransformGroup = ((TransformGroup)PocketPaintApplication.GetInstance().GridWorkingSpace.RenderTransform);

            paintingAreaCheckeredTransformGroup.Children.Clear();

            PocketPaintApplication.GetInstance().GridWorkingSpace.RenderTransform = paintingAreaCheckeredTransformGroup;
            PocketPaintApplication.GetInstance().GridWorkingSpace.UpdateLayout();
            PocketPaintApplication.GetInstance().GridWorkingSpace.InvalidateArrange();
            PocketPaintApplication.GetInstance().GridWorkingSpace.InvalidateMeasure();

            PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation -= _angle;
            PocketPaintApplication.GetInstance().PaintingAreaView.alignPositionOfGridWorkingSpace(null);
            PocketPaintApplication.GetInstance().PaintingAreaView.enableResetButtonRotate(PocketPaintApplication.GetInstance().PaintingAreaView.getRotationCounter() * (-1));
            return true;
        }
    }
}
