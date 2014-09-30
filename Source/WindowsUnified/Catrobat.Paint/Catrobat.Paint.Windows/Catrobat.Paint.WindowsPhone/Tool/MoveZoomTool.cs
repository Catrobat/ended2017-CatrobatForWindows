using Catrobat.Paint.Phone;
using Catrobat.Paint.Phone.Tool;
using System;
using System.Windows;
using Windows.UI.Popups;
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
            if (PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform != null)
            {
                _transforms = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            }
            if (_transforms == null)
            {
                PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform = _transforms = new TransformGroup();
            }

            DISPLAY_HEIGHT_HALF = (Window.Current.Bounds.Height - 150.0) / 2.0;
            DISPLAY_WIDTH_HALF = Window.Current.Bounds.Width / 2.0;
        }
        public override void HandleDown(object arg)
        {

        }

        public override void HandleMove(object arg)
        {
            if (NeedToResetCanvas)
            {
                ResetCanvas();
            }

            if (arg is ScaleTransform)
            {
                var resize = (ScaleTransform)arg;
                bool is_scale_allowed = false;

                if ((resize.ScaleX > 1.0) && (_transforms.Value.M11 < 28.0))
                {
                    is_scale_allowed = true;
                }
                else if ((resize.ScaleX < 1.0) && (_transforms.Value.M11 > 0.5))
                {
                    is_scale_allowed = true;
                }

                if (is_scale_allowed)
                {
                    var fixedaspection = 0.0;
                    fixedaspection = resize.ScaleX > resize.ScaleY ? resize.ScaleX : resize.ScaleY;

                    resize.ScaleX = Math.Round(0.0 + fixedaspection, 1);
                    resize.ScaleY = Math.Round(0.0 + fixedaspection, 1);
                    resize.CenterX = DISPLAY_WIDTH_HALF;
                    resize.CenterY = DISPLAY_HEIGHT_HALF;
                    _transforms.Children.Add(resize);
                }
            }
            else if (arg is TranslateTransform)
            {
                var move = (TranslateTransform)arg;
                _transforms.Children.Add(move);

                AppBarButton appBarButtonReset = PocketPaintApplication.GetInstance().PaintingAreaView.getAppBarResetButton();
                if(appBarButtonReset != null)
                {
                    appBarButtonReset.IsEnabled = true;
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("MoveZoomTool Should Not Reach this!");
                return;
            }
        }

        public override void HandleUp(object arg)
        {

        }

        public override void Draw(object o)
        {
            throw new NotImplementedException();
        }

        public override void ResetDrawingSpace()
        {
            _transforms.Children.Clear();
        }
    }
}
