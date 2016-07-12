using Catrobat.Paint.WindowsPhone.Command;
using Windows.Foundation;
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
            if (PocketPaintApplication.GetInstance().GridWorkingSpace.RenderTransform.GetType() == typeof(TransformGroup))
            {
                _rotationTransformGroup = PocketPaintApplication.GetInstance().GridWorkingSpace.RenderTransform as TransformGroup;
            }
            if (_rotationTransformGroup == null)
            {
                PocketPaintApplication.GetInstance().GridWorkingSpace.RenderTransform = _rotationTransformGroup = new TransformGroup();
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
            PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation = 0;

            rotateTransform.Angle = PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation;
            rotateTransform.CenterX = (PocketPaintApplication.GetInstance().GridWorkingSpace.Width) / 2;
            rotateTransform.CenterY = ((PocketPaintApplication.GetInstance().GridWorkingSpace.Height) / 2);
            PocketPaintApplication.GetInstance().PaintingAreaView.alignPositionOfGridWorkingSpace(null);
            PocketPaintApplication.GetInstance().PaintingAreaView.enableResetButtonRotate(PocketPaintApplication.GetInstance().PaintingAreaView.getRotationCounter() * (-1));
        }

        public void proofBoundariesOfAngle(int angleValue)
        {
            PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation += angleValue;

            if (PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation == 360)
            {
                PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation = 0;
            }
            else if (PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation == -90)
            {
                PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation = 270;
            }
        }

        public void RotateLeft()
        {
            int rotationAngle = -90;
            proofBoundariesOfAngle(rotationAngle);
            RotateTransform rtRotationGridWorkingSpace = PocketPaintApplication.GetInstance().PaintingAreaView.CreateRotateTransform(PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation,
                new Point(PocketPaintApplication.GetInstance().GridWorkingSpace.Width / 2.0, PocketPaintApplication.GetInstance().GridWorkingSpace.Height / 2.0));
            PocketPaintApplication.GetInstance().PaintingAreaView.alignPositionOfGridWorkingSpace(rtRotationGridWorkingSpace);
            addRotationToCommandManager(rtRotationGridWorkingSpace, -1);
        }

        public void RotateRight()
        {
            int rotationAngle = 90;
            proofBoundariesOfAngle(rotationAngle);
            RotateTransform rtRotationGridWorkingSpace = PocketPaintApplication.GetInstance().PaintingAreaView.CreateRotateTransform(PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation,
                new Point(PocketPaintApplication.GetInstance().GridWorkingSpace.Width / 2.0, PocketPaintApplication.GetInstance().GridWorkingSpace.Height / 2.0));
            PocketPaintApplication.GetInstance().PaintingAreaView.alignPositionOfGridWorkingSpace(rtRotationGridWorkingSpace);
            addRotationToCommandManager(rtRotationGridWorkingSpace, 1);
        }

        private void addRotationToCommandManager(RotateTransform rotateTransform, int rotationDirection)
        {
            TransformGroup rotationTransformGroupForCommand = new TransformGroup();
            rotationTransformGroupForCommand.Children.Add(rotateTransform);
            CommandManager.GetInstance().CommitCommand(new RotateCommand(rotationTransformGroupForCommand, PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation, rotationDirection));
        }
        public override void ResetUsedElements()
        {
        }
    }
}
