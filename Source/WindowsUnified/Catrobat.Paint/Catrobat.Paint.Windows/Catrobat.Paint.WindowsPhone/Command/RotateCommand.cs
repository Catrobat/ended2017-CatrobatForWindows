using Windows.UI.Xaml.Media;

namespace Catrobat.Paint.WindowsPhone.Command
{
    class RotateCommand : CommandBase
    {
        private TransformGroup _rotateTransformGroup;
        private int _angle = 0;

        public RotateCommand(TransformGroup rotateTransformGroup, int angle)
        {
            _rotateTransformGroup = rotateTransformGroup;
            _angle = angle;
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
            PocketPaintApplication.GetInstance().angleForRotation = _angle;

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
            PocketPaintApplication.GetInstance().angleForRotation -= _angle;
            return true;
        }
    }
}
