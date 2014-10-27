using Catrobat.Paint.Phone;
using Catrobat.Paint.WindowsPhone.Tool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Benutzersteuerelement" ist unter http://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    public sealed partial class RectangleSelectionControl : UserControl
    {
        Point startPosition;
        TransformGroup _transformGridEllipsCenterBottom;
        TransformGroup _transfomrGridEllipseCenterTop;
        TransformGroup _transformGridEllipseLeftBottom;
        TransformGroup _transformGridEllipseLeftCenter;
        TransformGroup _transformGridEllipseLeftTop;
        TransformGroup _transformGridEllipseRightBottom;
        TransformGroup _transformGridEllipseRightCenter;
        TransformGroup _transformGridEllipseRightTop;
        TransformGroup _transformGridMain;

        const double MIN_RECTANGLE_MOVE_HEIGHT = 50.0;
        const double MIN_RECTANGLE_MOVE_WIDTH = 50.0;
        
        public RectangleSelectionControl()
        {
            this.InitializeComponent();
            startPosition = new Point(0,0);
            GridEllCenterBottom.RenderTransform = _transformGridEllipsCenterBottom = new TransformGroup();
            GridEllCenterTop.RenderTransform = _transfomrGridEllipseCenterTop = new TransformGroup();
            GridEllRightCenter.RenderTransform = _transformGridEllipseRightCenter = new TransformGroup();
            GridEllLeftBottom.RenderTransform = _transformGridEllipseLeftBottom = new TransformGroup();
            GridEllLeftCenter.RenderTransform = _transformGridEllipseLeftCenter = new TransformGroup();
            GridEllLeftTop.RenderTransform = _transformGridEllipseLeftTop = new TransformGroup();
            GridEllRightBottom.RenderTransform = _transformGridEllipseRightBottom = new TransformGroup();
            GridEllRightTop.RenderTransform = _transformGridEllipseRightTop = new TransformGroup();
            GridMain.RenderTransform = _transformGridMain = new TransformGroup();

            PocketPaintApplication.GetInstance().RectangleSelectionControl = this;
        }
        private void _setGridTransformsOfEllipses(TransformGroup transformGroup, TranslateTransform translateTransform)
        {
            transformGroup.Children.Add(translateTransform);
            double offsetX = transformGroup.Value.OffsetX;
            double offsetY = transformGroup.Value.OffsetY;

            transformGroup.Children.Clear();

            var move = new TranslateTransform();
            move.X = offsetX;
            move.Y = offsetY;

            transformGroup.Children.Add(move);
        }
        private void setGridTransformsOfEllipses(TranslateTransform centerBottom, TranslateTransform centerTop, TranslateTransform rightCenter,
                                                 TranslateTransform leftBottom, TranslateTransform leftCenter, TranslateTransform leftTop,
                                                 TranslateTransform rightBottom, TranslateTransform rightTop)
        {
            if(centerBottom != null)
            {
                _setGridTransformsOfEllipses(_transformGridEllipsCenterBottom, centerBottom);
            }

            if(centerTop != null)
            {
                _setGridTransformsOfEllipses(_transfomrGridEllipseCenterTop, centerTop);
            }

            if(rightCenter != null)
            {
                _setGridTransformsOfEllipses(_transformGridEllipseRightCenter, rightCenter);
            }
            
            if(leftBottom != null)
            {
                _setGridTransformsOfEllipses(_transformGridEllipseLeftBottom, leftBottom);
            }

            if(leftCenter != null)
            {
                _setGridTransformsOfEllipses(_transformGridEllipseLeftCenter, leftCenter);
            }

            if(leftTop != null)
            {
                _setGridTransformsOfEllipses(_transformGridEllipseLeftTop, leftTop);
            }

            if(rightBottom != null)
            {
                _setGridTransformsOfEllipses(_transformGridEllipseRightBottom, rightBottom);
            }

            if(rightTop != null)
            {
                _setGridTransformsOfEllipses(_transformGridEllipseRightTop, rightTop);
            }
        }

        private TranslateTransform createTranslateTransform(double x, double y)
        {
            var move = new TranslateTransform();
            ((TranslateTransform)move).X = x;
            ((TranslateTransform)move).Y = y;

            return move;
        }

        public void setSizeOfRecBar(int height, int width)
        {

            PocketPaintApplication.GetInstance().BarRecEllShape.setTbHeightValue = height;

            PocketPaintApplication.GetInstance().BarRecEllShape.setTbWidthValue = width;
        }

        private void ellCenterBottom_ManipulationDelta_1(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleForMovement.Height + e.Delta.Translation.Y) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveY = createTranslateTransform(0.0, Math.Round(e.Delta.Translation.Y));
                var moveY2 = createTranslateTransform(0.0, moveY.Y / 2.0);

                setGridTransformsOfEllipses(moveY,null,moveY2,moveY,moveY2,null,moveY,null);

                changeHeightOfUiElements(moveY.Y);
                changeMarginBottomOfUiElements(moveY.Y);

                setSizeOfRecBar((int)rectRectangleToDraw.Height, (int)rectRectangleToDraw.Width);
            }
        }

        private void ellCenterTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleForMovement.Height + (e.Delta.Translation.Y * -1)) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveY = createTranslateTransform(0.0, Math.Round(e.Delta.Translation.Y));
                var moveY2 = createTranslateTransform(0.0, moveY.Y / 2.0);

                setGridTransformsOfEllipses(null, moveY, moveY2, null, moveY2, moveY, null, moveY);

                changeHeightOfUiElements(moveY.Y * -1.0);
                changeMarginTopOfUiElements(moveY.Y * -1.0);
                setSizeOfRecBar((int)rectRectangleToDraw.Height, (int)rectRectangleToDraw.Width);
            }
        }

        private void ellLeftBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleForMovement.Width + (e.Delta.Translation.X * -1)) >= MIN_RECTANGLE_MOVE_WIDTH &&
               (rectRectangleForMovement.Height + e.Delta.Translation.Y) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveX = createTranslateTransform(Math.Round(e.Delta.Translation.X), 0.0);
                var moveY = createTranslateTransform(0.0, Math.Round(e.Delta.Translation.Y));
                var moveXY = createTranslateTransform(moveX.X, moveY.Y);
                var moveX2 = createTranslateTransform(moveX.X / 2.0, 0.0);
                var moveY2 = createTranslateTransform(0.0, moveY.Y / 2.0);
                var moveX2Y = createTranslateTransform(moveX2.X, moveY.Y);
                var moveXY2 = createTranslateTransform(moveX.X, moveY2.Y);
                setGridTransformsOfEllipses(moveX2Y, moveX2, moveY2, moveXY, moveXY2, moveX, moveY, null);

                changeWidthOfUiElements(moveX.X * -1);
                changeMarginLeftOfUiElements(moveX.X * -1);

                changeHeightOfUiElements(moveY.Y);
                changeMarginBottomOfUiElements(moveY.Y);

                setSizeOfRecBar((int)rectRectangleToDraw.Height, (int)rectRectangleToDraw.Width);
            }
        }

        private void ellLeftCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleForMovement.Width + (e.Delta.Translation.X * -1)) >= MIN_RECTANGLE_MOVE_WIDTH)
            {
                var moveX = createTranslateTransform(Math.Round(e.Delta.Translation.X), 0.0);
                var moveX2 = createTranslateTransform(moveX.X / 2.0, 0.0);

                setGridTransformsOfEllipses(moveX2, moveX2, null, moveX, moveX, moveX, null, null);

                changeWidthOfUiElements(moveX.X * -1.0);
                changeMarginLeftOfUiElements(moveX.X * -1.0);

                setSizeOfRecBar((int)rectRectangleToDraw.Height, (int)rectRectangleToDraw.Width);
            }
        }
        
        private void ellLeftTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleForMovement.Width + (e.Delta.Translation.X * -1)) >= MIN_RECTANGLE_MOVE_WIDTH &&
                (rectRectangleForMovement.Height + (e.Delta.Translation.Y * -1)) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveX = createTranslateTransform(Math.Round(e.Delta.Translation.X), 0.0);
                var moveY = createTranslateTransform(0.0, Math.Round(e.Delta.Translation.Y));
                var moveXY = createTranslateTransform(moveX.X, moveY.Y);
                var moveX2 = createTranslateTransform(moveX.X / 2.0, 0.0);
                var moveY2 = createTranslateTransform(0.0, moveY.Y / 2.0);
                var moveX2Y = createTranslateTransform(moveX2.X, moveY.Y);
                var moveXY2 = createTranslateTransform(moveX.X, moveY2.Y);
                setGridTransformsOfEllipses(moveX2, moveX2Y, moveY2, moveX, moveXY2, moveXY, null, moveY);

                changeWidthOfUiElements(moveX.X * -1);
                changeMarginLeftOfUiElements(moveX.X * -1);

                changeHeightOfUiElements(moveY.Y * -1.0);
                changeMarginTopOfUiElements(moveY.Y * -1.0);

                setSizeOfRecBar((int)rectRectangleToDraw.Height, (int)rectRectangleToDraw.Width);
            }
        }

        private void ellRightBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleForMovement.Width + e.Delta.Translation.X) >= MIN_RECTANGLE_MOVE_WIDTH &&
                (rectRectangleForMovement.Height + e.Delta.Translation.Y) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveX = createTranslateTransform(Math.Round(e.Delta.Translation.X), 0.0);
                var moveY = createTranslateTransform(0.0, Math.Round(e.Delta.Translation.Y));
                var moveXY = createTranslateTransform(moveX.X, moveY.Y);
                var moveX2 = createTranslateTransform(moveX.X / 2.0, 0.0);
                var moveY2 = createTranslateTransform(0.0, moveY.Y / 2.0);
                var moveX2Y = createTranslateTransform(moveX2.X, moveY.Y);
                var moveXY2 = createTranslateTransform(moveX.X, moveY2.Y);
                setGridTransformsOfEllipses(moveX2Y, moveX2, moveXY2, moveY, moveY2, null, moveXY, moveX);

                changeMarginRightOfUiElements(moveX.X);
                changeWidthOfUiElements(moveX.X);

                changeHeightOfUiElements(moveY.Y);
                changeMarginBottomOfUiElements(moveY.Y);

                setSizeOfRecBar((int)rectRectangleToDraw.Height, (int)rectRectangleToDraw.Width);
            }

        }

        private void ellRightCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleForMovement.Width + e.Delta.Translation.X) >= MIN_RECTANGLE_MOVE_WIDTH)
            {
                var moveX = createTranslateTransform(Math.Round(e.Delta.Translation.X), 0.0);
                var moveX2 = createTranslateTransform(moveX.X / 2.0, 0.0);

                setGridTransformsOfEllipses(moveX2, moveX2, moveX, null, null, null, moveX, moveX);

                changeWidthOfUiElements(moveX.X);
                changeMarginRightOfUiElements(moveX.X);

                setSizeOfRecBar((int)rectRectangleToDraw.Height, (int)rectRectangleToDraw.Width);
            }
        }

        private void ellRightTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleForMovement.Width + e.Delta.Translation.X) >= MIN_RECTANGLE_MOVE_WIDTH &&
               (rectRectangleForMovement.Height + (e.Delta.Translation.Y * -1)) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveX = createTranslateTransform(Math.Round(e.Delta.Translation.X), 0.0);
                var moveY = createTranslateTransform(0.0, Math.Round(e.Delta.Translation.Y));
                var moveXY = createTranslateTransform(moveX.X, moveY.Y);
                var moveX2 = createTranslateTransform(moveX.X / 2.0, 0.0);
                var moveY2 = createTranslateTransform(0.0, moveY.Y / 2.0);
                var moveX2Y = createTranslateTransform(moveX2.X, moveY.Y);
                var moveXY2 = createTranslateTransform(moveX.X, moveY2.Y);
                setGridTransformsOfEllipses(moveX2, moveX2Y, moveXY2, null, moveY2, moveY, moveX, moveXY);

                changeMarginRightOfUiElements(moveX.X);
                changeWidthOfUiElements(moveX.X);

                changeHeightOfUiElements(moveY.Y * -1.0);
                changeMarginTopOfUiElements(moveY.Y * -1.0);

                setSizeOfRecBar((int)rectRectangleToDraw.Height, (int)rectRectangleToDraw.Width);
            }

        }

        private void changeHeightOfUiElements(double value)
        {

            rectRectangleForMovement.Height += value;

            if (rectRectangleToDraw.Height + value >= 10.0)
            {
                rectRectangleToDraw.Height += value;
            }
            else
            {
                rectRectangleToDraw.Height = 10.0;
            }
        }

        private void changeWidthOfUiElements(double value)
        {
            GridMain.Width += value;
            rectRectangleForMovement.Width += value;

            if (rectRectangleToDraw.Width + value >= 10.0)
            {
                rectRectangleToDraw.Width += value;
            }
            else
            {
                rectRectangleToDraw.Width = 10.0;
            }
        }

        private void changeMarginBottomOfUiElements(double value)
        {
            rectRectangleForMovement.Margin = new Thickness(rectRectangleForMovement.Margin.Left, rectRectangleForMovement.Margin.Top, rectRectangleForMovement.Margin.Right, rectRectangleForMovement.Margin.Bottom - value);
            rectRectangleToDraw.Margin = new Thickness(rectRectangleToDraw.Margin.Left, rectRectangleToDraw.Margin.Top, rectRectangleToDraw.Margin.Right, rectRectangleToDraw.Margin.Bottom - value);
        }

        private void changeMarginLeftOfUiElements(double value)
        {
            rectRectangleForMovement.Margin = new Thickness(rectRectangleForMovement.Margin.Left - value, rectRectangleForMovement.Margin.Top, rectRectangleForMovement.Margin.Right, rectRectangleForMovement.Margin.Bottom);
            rectRectangleToDraw.Margin = new Thickness(rectRectangleToDraw.Margin.Left - value, rectRectangleToDraw.Margin.Top, rectRectangleToDraw.Margin.Right, rectRectangleToDraw.Margin.Bottom);
        }

        private void changeMarginRightOfUiElements(double value)
        {
            rectRectangleForMovement.Margin = new Thickness(rectRectangleForMovement.Margin.Left, rectRectangleForMovement.Margin.Top, rectRectangleForMovement.Margin.Right - value, rectRectangleForMovement.Margin.Bottom);
            rectRectangleToDraw.Margin = new Thickness(rectRectangleToDraw.Margin.Left, rectRectangleToDraw.Margin.Top, rectRectangleToDraw.Margin.Right - value, rectRectangleToDraw.Margin.Bottom);
        }

        private void changeMarginTopOfUiElements(double value)
        {
            rectRectangleForMovement.Margin = new Thickness(rectRectangleForMovement.Margin.Left, rectRectangleForMovement.Margin.Top - value, rectRectangleForMovement.Margin.Right, rectRectangleForMovement.Margin.Bottom);
            rectRectangleToDraw.Margin = new Thickness(rectRectangleToDraw.Margin.Left, rectRectangleToDraw.Margin.Top - value, rectRectangleToDraw.Margin.Right, rectRectangleToDraw.Margin.Bottom);
        }

        private void rectRectangleForMovement_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var movezoom = new TranslateTransform();
            ((TranslateTransform)movezoom).X = Math.Round(e.Delta.Translation.X);
            ((TranslateTransform)movezoom).Y = Math.Round(e.Delta.Translation.Y);;

            _transformGridMain.Children.Add(movezoom);
            
            movezoom.X = _transformGridMain.Value.OffsetX;
            movezoom.Y = _transformGridMain.Value.OffsetY;
            _transformGridMain.Children.Clear();
            _transformGridMain.Children.Add(movezoom);

        }

        public void resetRectangleSelectionControl()
        {
            GridEllCenterBottom.RenderTransform = _transformGridEllipsCenterBottom = new TransformGroup();
            GridEllCenterTop.RenderTransform = _transfomrGridEllipseCenterTop = new TransformGroup();
            GridEllRightCenter.RenderTransform = _transformGridEllipseRightCenter = new TransformGroup();
            GridEllLeftBottom.RenderTransform = _transformGridEllipseLeftBottom = new TransformGroup();
            GridEllLeftCenter.RenderTransform = _transformGridEllipseLeftCenter = new TransformGroup();
            GridEllLeftTop.RenderTransform = _transformGridEllipseLeftTop = new TransformGroup();
            GridEllRightBottom.RenderTransform = _transformGridEllipseRightBottom = new TransformGroup();
            GridEllRightTop.RenderTransform = _transformGridEllipseRightTop = new TransformGroup();
            GridMain.RenderTransform = _transformGridMain = new TransformGroup();
        }

        public void changeColorOfDrawingShape(Color color)
        {
            rectRectangleToDraw.Fill = new SolidColorBrush(color);
        }

        public void changeStrokeOfDrawingShape(Color color)
        {
            rectRectangleToDraw.Stroke = new SolidColorBrush(color);
        }

        public double setStrokeThicknessOfDrawingShape
        {
            get
            {
                return rectRectangleToDraw.StrokeThickness;
            }
            set
            {
                rectRectangleToDraw.StrokeThickness = value;
            }
        }

        public void changeHeightOfDrawingSelection(double newHeight)
        {
            double moveValue = newHeight - rectRectangleToDraw.Height;
            var move = new TranslateTransform();
            ((TranslateTransform)move).X = 0.0;
            ((TranslateTransform)move).Y = moveValue;

            if ((rectRectangleForMovement.Height + moveValue) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {

                _transformGridEllipsCenterBottom.Children.Add(move);
                _transformGridEllipseLeftBottom.Children.Add(move);
                _transformGridEllipseRightBottom.Children.Add(move);

                var move2 = new TranslateTransform();
                ((TranslateTransform)move2).X = 0.0;
                ((TranslateTransform)move2).Y = moveValue;

                _transformGridEllipseLeftCenter.Children.Add(move2);
                _transformGridEllipseRightCenter.Children.Add(move2);

                changeHeightOfUiElements(move.Y);
                changeMarginBottomOfUiElements(move.Y);
                PocketPaintApplication.GetInstance().BarRecEllShape.setTbHeightValue = (int)rectRectangleToDraw.Height;
                PocketPaintApplication.GetInstance().BarRecEllShape.setTbWidthValue = (int)rectRectangleToDraw.Width;
            }
        }

        public void changeWidthOfDrawingSelection(double newWidth)
        {
            double moveValue = newWidth - rectRectangleToDraw.Width;
            if ((rectRectangleForMovement.Width + moveValue) >= MIN_RECTANGLE_MOVE_WIDTH)
            {
                var move = new TranslateTransform();
                ((TranslateTransform)move).X = moveValue;
                ((TranslateTransform)move).Y = 0.0;

                _transformGridEllipseRightTop.Children.Add(move);
                _transformGridEllipseRightCenter.Children.Add(move);
                _transformGridEllipseRightBottom.Children.Add(move);

                changeWidthOfUiElements(move.X);
                changeMarginRightOfUiElements(moveValue);

                var move2 = new TranslateTransform();
                ((TranslateTransform)move2).X = moveValue / 2;
                ((TranslateTransform)move2).Y = 0.0;

                _transfomrGridEllipseCenterTop.Children.Add(move2);
                _transformGridEllipsCenterBottom.Children.Add(move2);

                PocketPaintApplication.GetInstance().BarRecEllShape.setTbHeightValue = (int)rectRectangleToDraw.Height;
                PocketPaintApplication.GetInstance().BarRecEllShape.setTbWidthValue = (int)rectRectangleToDraw.Width;
            }
        }

        private void rectRectangleForMovement_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            double valueY = -20.0 + rectRectangleToDraw.Margin.Bottom;
            PocketPaintApplication.GetInstance().ToolCurrent.Draw(new Point(Math.Round(_transformGridMain.Value.OffsetX + Window.Current.Bounds.Width / 2.0 - rectRectangleToDraw.Width / 2.0), Math.Round(_transformGridMain.Value.OffsetY + (Window.Current.Bounds.Height - 150.0) / 2.0 - (rectRectangleToDraw.Height + valueY) / 2.0)));
            string test = "string";
        }
    }
}
