using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    class MoveZoomTool : ToolBase
    {
        private TransformGroup _transforms;
        private double DISPLAY_HEIGHT_HALF;
        private double DISPLAY_WIDTH_HALF;

        public MoveZoomTool()
        {
            ToolType = ToolType.Move;
            ResetCanvas();
            if (PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform.GetType() == typeof(TransformGroup))
            {
                _transforms = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            }
            if (_transforms == null)
            {
                PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform = _transforms = new TransformGroup();
            }

            //DISPLAY_HEIGHT_HALF = (Window.Current.Bounds.Height - PocketPaintApplication.GetInstance().AppbarTop.ActualHeight) / 2.0;
            //DISPLAY_WIDTH_HALF = Window.Current.Bounds.Width / 2.0;

            DISPLAY_WIDTH_HALF = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.ActualWidth / 2.0;
            DISPLAY_HEIGHT_HALF = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.ActualHeight / 2.0;
        }
        public override void HandleDown(object arg)
        {

        }

        public override void HandleMove(object arg)
        {
            if (arg is ScaleTransform)
            {
                var toScaleValue = (ScaleTransform)arg;

                bool isScaleAllowed = checkIfScalingAllowed(toScaleValue);
                if (isScaleAllowed)
                {
                    var fixedaspection = 0.0;
                    fixedaspection = toScaleValue.ScaleX > toScaleValue.ScaleY ? toScaleValue.ScaleX : toScaleValue.ScaleY;
                    toScaleValue.ScaleX = fixedaspection;
                    toScaleValue.ScaleY = fixedaspection;
                    toScaleValue.CenterX = DISPLAY_WIDTH_HALF;
                    toScaleValue.CenterY = DISPLAY_HEIGHT_HALF;
                    _transforms.Children.Add(toScaleValue);
            }
                }
            else if (arg is TranslateTransform)
            {
                var move = (TranslateTransform)arg;
                _transforms.Children.Add(move);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("MoveZoomTool Should Not Reach this!");
                return;
            }

            AppBarButton appBarButtonReset = PocketPaintApplication.GetInstance().PaintingAreaView.getAppBarResetButton();
            if (appBarButtonReset != null)
            {
                appBarButtonReset.IsEnabled = true;
            }
        }

        public override void HandleUp(object arg)
        {

        }

        public override void Draw(object o)
        {

        }

        public override void ResetDrawingSpace()
        {
            _transforms.Children.Clear();
            PocketPaintApplication.GetInstance().PaintingAreaView.setSizeOfPaintingAreaViewCheckered();
        }

        public bool checkIfScalingAllowed(ScaleTransform toScaleValue)
        {
            bool isScaleAllowed = false;

            if ((toScaleValue.ScaleX > 1.0) && (_transforms.Value.M11 < 28.0))
            {
                isScaleAllowed = true;
            }
            else if ((toScaleValue.ScaleX < 1.0) && (_transforms.Value.M11 > 0.5))
            {
                isScaleAllowed = true;
            }
            return isScaleAllowed;
        }

        public ScaleTransform getLastScaleTransformation()
        {
            for (int i = 0; i < _transforms.Children.Count; i++)
            {
                if (_transforms.Children[i].GetType() == typeof(ScaleTransform))
                {
                    ScaleTransform scaleTransform = new ScaleTransform();
                    scaleTransform.ScaleX = ((ScaleTransform)_transforms.Children[i]).ScaleX;
                    scaleTransform.ScaleY = ((ScaleTransform)_transforms.Children[i]).ScaleY;
                    scaleTransform.CenterX = ((ScaleTransform)_transforms.Children[i]).CenterX;
                    scaleTransform.CenterY = ((ScaleTransform)_transforms.Children[i]).CenterY;
                    return scaleTransform;
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
