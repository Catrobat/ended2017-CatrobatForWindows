using Windows.UI.Xaml.Media;

namespace Catrobat.Paint.WindowsPhone.Command
{
    class ZoomCommand : CommandBase
    {
        private ScaleTransform _scaleTransform;

        public ZoomCommand(ScaleTransform scaleTransform)
        {
            _scaleTransform = scaleTransform;
        }

        public override bool ReDo()
        {
            TransformGroup transformGroup = ((TransformGroup)PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform);
            //for (int i = 0; i < transformGroup.Children.Count; i++)
            //{
            //    if (transformGroup.Children[i].GetType() == typeof(ScaleTransform))
            //    {
            //        transformGroup.Children.RemoveAt(i);
            //    }
            //}

            transformGroup.Children.Add(_scaleTransform);

            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform = transformGroup;
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.InvalidateMeasure();

            return true;
        }

        public override bool UnDo()
        {
            TransformGroup transformGroup = ((TransformGroup)PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform);

            for (int i = 0; i < transformGroup.Children.Count; i++)
            {
                if (transformGroup.Children[i].GetType() == typeof(ScaleTransform))
                {
                    transformGroup.Children.RemoveAt(i);
                }
            }

            double scaleX = transformGroup.Value.M11;
            double scaleY = transformGroup.Value.M22;

            double DISPLAY_WIDTH_HALF = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.ActualWidth / 2.0;
            double DISPLAY_HEIGHT_HALF = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.ActualHeight / 2.0;

            var defaultScaleTransform = new ScaleTransform();
            defaultScaleTransform.ScaleX = 1.0 / (scaleX / 0.75);
            defaultScaleTransform.ScaleY = 1.0 / (scaleY / 0.75);
            defaultScaleTransform.CenterX = DISPLAY_WIDTH_HALF;
            defaultScaleTransform.CenterY = DISPLAY_HEIGHT_HALF;

            transformGroup.Children.Add(defaultScaleTransform);

            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform = transformGroup;
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.InvalidateMeasure();

            return true;
        }
    }
}
