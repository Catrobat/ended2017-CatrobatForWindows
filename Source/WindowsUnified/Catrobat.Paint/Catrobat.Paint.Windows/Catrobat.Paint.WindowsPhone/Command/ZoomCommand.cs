using Windows.UI.Xaml.Media;

namespace Catrobat.Paint.WindowsPhone.Command
{
    class ZoomCommand : CommandBase
    {
        private TransformGroup _transformGroup;

        public ZoomCommand(TransformGroup transformGroup)
        {
            _transformGroup = transformGroup;
        }

        public override bool ReDo()
        {
            TransformGroup transformGroup = ((TransformGroup)PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform);
            for (int i = 0; i < _transformGroup.Children.Count; i++)
            {
                transformGroup.Children.Add(_transformGroup.Children[i]);
            }

            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform = transformGroup;
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.InvalidateMeasure();

            return true;
        }

        public override bool UnDo()
        {
            TransformGroup transformGroup = ((TransformGroup)PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform);

            transformGroup.Children.Clear();

            double scaleX = transformGroup.Value.M11;
            double scaleY = transformGroup.Value.M22;

            double DISPLAY_WIDTH_HALF = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.ActualWidth / 2.0;
            double DISPLAY_HEIGHT_HALF = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.ActualHeight / 2.0;

            PocketPaintApplication.GetInstance().PaintingAreaView.setSizeOfPaintingAreaViewCheckered();

            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform = transformGroup;
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.InvalidateMeasure();

            return true;
        }
    }
}
