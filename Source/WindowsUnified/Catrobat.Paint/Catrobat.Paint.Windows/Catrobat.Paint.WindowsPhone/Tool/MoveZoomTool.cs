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

            if (PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform != null)
            {
                _transforms = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            }
            if (_transforms == null)
            {
                PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform = _transforms = new TransformGroup();
            }

            DISPLAY_WIDTH_HALF = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.ActualWidth / 2.0;
            DISPLAY_HEIGHT_HALF = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.ActualHeight / 2.0;
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
                var toScaleValue = (ScaleTransform)arg;
                bool isScaleAllowed = false;

                if ((toScaleValue.ScaleX > 1.0) && (_transforms.Value.M11 < 28.0))
                {
                    isScaleAllowed = true;
                }
                else if ((toScaleValue.ScaleX < 1.0) && (_transforms.Value.M11 > 0.5))
                {
                    isScaleAllowed = true;
                }

                if (isScaleAllowed)
                {
                    var fixedaspection = 0.0;
                    fixedaspection = toScaleValue.ScaleX > toScaleValue.ScaleY ? toScaleValue.ScaleX : toScaleValue.ScaleY;

                    toScaleValue.ScaleX = Math.Round(0.0 + fixedaspection, 1);
                    toScaleValue.ScaleY = Math.Round(0.0 + fixedaspection, 1);
                    toScaleValue.CenterX = DISPLAY_WIDTH_HALF;
                    toScaleValue.CenterY = DISPLAY_HEIGHT_HALF;

                    ScaleTransform actualSaleValue = getLastScaleTransformation();
                    if (actualSaleValue != null)
                    {
                        toScaleValue.ScaleX *= actualSaleValue.ScaleX;
                        toScaleValue.ScaleY *= actualSaleValue.ScaleY;
                        if (PocketPaintApplication.GetInstance().isZoomButtonClicked)
                        {
                            toScaleValue.CenterX = DISPLAY_WIDTH_HALF;
                            toScaleValue.CenterY = DISPLAY_HEIGHT_HALF;
                        }
                        else
                        {
                            toScaleValue.CenterX += actualSaleValue.CenterX;
                            toScaleValue.CenterY += actualSaleValue.CenterY;
                        }
                    }

                    addTransformation(toScaleValue);
                }
            }
            else if (arg is TranslateTransform)
            {
                var move = (TranslateTransform)arg;

                TranslateTransform currentTranslate = getLastTranslateTransformation();
                if (currentTranslate != null)
                {
                    move.X += currentTranslate.X;
                    move.Y += currentTranslate.Y;
                }

                addTransformation(move);
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

        public TranslateTransform getLastTranslateTransformation()
        {
            for (int i = 0; i < _transforms.Children.Count; i++)
            {
                if (_transforms.Children[i].GetType() == typeof(TranslateTransform))
                {
                    TranslateTransform translateTransform = new TranslateTransform();
                    translateTransform.X = ((TranslateTransform)_transforms.Children[i]).X;
                    translateTransform.Y = ((TranslateTransform)_transforms.Children[i]).Y;

                    return translateTransform;
                }
            }

            return null;
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
    }
}
