using Catrobat.Paint.Phone;
using Windows.UI.Xaml.Media;

namespace Catrobat.Paint.WindowsPhone.Command
{
    class RotateCommand : CommandBase
    {
        private RotateTransform _rotateTransform;

        public RotateCommand(RotateTransform rotateTransform)
        {
            _rotateTransform = rotateTransform;
        }

        public override bool ReDo()
        {
            TransformGroup transformGroup = ((TransformGroup)PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform);
            for (int i = 0; i < transformGroup.Children.Count; i++)
            {
                if (transformGroup.Children[i].GetType() == typeof(RotateTransform))
                {
                    transformGroup.Children.RemoveAt(i);
                }
            }

            transformGroup.Children.Add(_rotateTransform);

            PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform = transformGroup;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.InvalidateMeasure();

            return true;
        }

        public override bool UnDo()
        {
            TransformGroup transformGroup = ((TransformGroup)PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform);
            for (int i = 0; i < transformGroup.Children.Count; i++)
            {
                if (transformGroup.Children[i].GetType() == typeof(RotateTransform))
                {
                    transformGroup.Children.RemoveAt(i);
                }
            }

            var defaultRotateTransform = new RotateTransform();
            defaultRotateTransform.Angle = 0.0;
            defaultRotateTransform.CenterX = _rotateTransform.CenterX;
            defaultRotateTransform.CenterY = _rotateTransform.CenterY;

            transformGroup.Children.Add(defaultRotateTransform);

            PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform = transformGroup;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.InvalidateMeasure();

            return true;
        }
    }
}
