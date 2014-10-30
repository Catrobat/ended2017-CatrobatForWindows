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
using Windows.UI.Xaml.Shapes;

// Die Elementvorlage "Benutzersteuerelement" ist unter http://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    public sealed partial class EllipseSelectionControl : UserControl
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

        // 0 = currently no shape is selected.
        // 1 = Rectangle-Shape is selected.
        // 2 = Ellips-Shape is selected.
        uint _currentSelectedShape;

        const double MIN_RECTANGLE_MOVE_HEIGHT = 50.0;
        const double MIN_RECTANGLE_MOVE_WIDTH = 50.0;
        const uint RECTANGLGE_SHAPE = 1;
        const uint ELLIPSE_SHAPE = 2;

        public EllipseSelectionControl()
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

            _currentSelectedShape = 0;
            PocketPaintApplication.GetInstance().EllipseSelectionControl = this;
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

        public void setSizeOfRecBar(double height, double width)
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

                setSizeOfRecBar(ellEllipseToDraw.Height, ellEllipseToDraw.Width);
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
                setSizeOfRecBar(ellEllipseToDraw.Height, ellEllipseToDraw.Width);
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

                setSizeOfRecBar(ellEllipseToDraw.Height, ellEllipseToDraw.Width);
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

                setSizeOfRecBar(ellEllipseToDraw.Height, ellEllipseToDraw.Width);
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

                setSizeOfRecBar(ellEllipseToDraw.Height, ellEllipseToDraw.Width);
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

                setSizeOfRecBar(ellEllipseToDraw.Height, ellEllipseToDraw.Width);
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

                setSizeOfRecBar(ellEllipseToDraw.Height, ellEllipseToDraw.Width);
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

                setSizeOfRecBar(ellEllipseToDraw.Height, ellEllipseToDraw.Width);
            }

        }

        private void changeHeightOfUiElements(double value)
        {

            rectRectangleForMovement.Height += value;

            if (ellEllipseToDraw.Height + value >= 10.0)
            {
                ellEllipseToDraw.Height += value;
            }
            else
            {
                ellEllipseToDraw.Height = 10.0;
            }
        }

        private void changeWidthOfUiElements(double value)
        {
            GridMain.Width += value;
            rectRectangleForMovement.Width += value;

            if (ellEllipseToDraw.Width + value >= 10.0)
            {
                ellEllipseToDraw.Width += value;
            }
            else
            {
                ellEllipseToDraw.Width = 10.0;
            }
        }

        private void changeMarginBottomOfUiElements(double value)
        {
            rectRectangleForMovement.Margin = new Thickness(rectRectangleForMovement.Margin.Left, rectRectangleForMovement.Margin.Top, rectRectangleForMovement.Margin.Right, rectRectangleForMovement.Margin.Bottom - value);
            ellEllipseToDraw.Margin = new Thickness(ellEllipseToDraw.Margin.Left, ellEllipseToDraw.Margin.Top, ellEllipseToDraw.Margin.Right, ellEllipseToDraw.Margin.Bottom - value);
        }

        private void changeMarginLeftOfUiElements(double value)
        {
            rectRectangleForMovement.Margin = new Thickness(rectRectangleForMovement.Margin.Left - value, rectRectangleForMovement.Margin.Top, rectRectangleForMovement.Margin.Right, rectRectangleForMovement.Margin.Bottom);
            ellEllipseToDraw.Margin = new Thickness(ellEllipseToDraw.Margin.Left - value, ellEllipseToDraw.Margin.Top, ellEllipseToDraw.Margin.Right, ellEllipseToDraw.Margin.Bottom);
        }

        private void changeMarginRightOfUiElements(double value)
        {
            rectRectangleForMovement.Margin = new Thickness(rectRectangleForMovement.Margin.Left, rectRectangleForMovement.Margin.Top, rectRectangleForMovement.Margin.Right - value, rectRectangleForMovement.Margin.Bottom);
            ellEllipseToDraw.Margin = new Thickness(ellEllipseToDraw.Margin.Left, ellEllipseToDraw.Margin.Top, ellEllipseToDraw.Margin.Right - value, ellEllipseToDraw.Margin.Bottom);
        }

        private void changeMarginTopOfUiElements(double value)
        {
            rectRectangleForMovement.Margin = new Thickness(rectRectangleForMovement.Margin.Left, rectRectangleForMovement.Margin.Top - value, rectRectangleForMovement.Margin.Right, rectRectangleForMovement.Margin.Bottom);
            ellEllipseToDraw.Margin = new Thickness(ellEllipseToDraw.Margin.Left, ellEllipseToDraw.Margin.Top - value, ellEllipseToDraw.Margin.Right, ellEllipseToDraw.Margin.Bottom);
        }

        private void rectRectangleForMovement_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var movezoom = new TranslateTransform();
            ((TranslateTransform)movezoom).X = e.Delta.Translation.X;
            ((TranslateTransform)movezoom).Y = e.Delta.Translation.Y;

            _transformGridMain.Children.Add(movezoom);
            
            movezoom.X = _transformGridMain.Value.OffsetX;
            movezoom.Y = _transformGridMain.Value.OffsetY;
            _transformGridMain.Children.Clear();
            _transformGridMain.Children.Add(movezoom);

        }

        public void changeColorOfDrawingShape(Color color)
        {
            ellEllipseToDraw.Fill = new SolidColorBrush(color);
        }

        public void changeStrokeOfDrawingShape(Color color)
        {
            ellEllipseToDraw.Stroke = new SolidColorBrush(color);
        }

        public double setStrokeThicknessOfDrawingShape
        {
            get
            {
                return ellEllipseToDraw.StrokeThickness;
            }
            set
            {
                ellEllipseToDraw.StrokeThickness = value;
            }
        }

        public void changeHeightOfDrawingSelection(double newHeight)
        {
            double moveValue = newHeight - ellEllipseToDraw.Height;
            var moveY = createTranslateTransform(0.0, moveValue);


            if ((rectRectangleForMovement.Height + moveValue) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                setGridTransformsOfEllipses(moveY, null, moveY, moveY, moveY, null, moveY, null);

                PocketPaintApplication.GetInstance().BarRecEllShape.setTbHeightValue = ellEllipseToDraw.Height;
                PocketPaintApplication.GetInstance().BarRecEllShape.setTbWidthValue = ellEllipseToDraw.Width;
            }
        }

        public void changeWidthOfDrawingSelection(double newWidth)
        {
            double moveValue = newWidth - ellEllipseToDraw.Width;
            if ((rectRectangleForMovement.Width + moveValue) >= MIN_RECTANGLE_MOVE_WIDTH)
            {
                var moveX = createTranslateTransform(moveValue, 0.0);
                var moveX2 = createTranslateTransform(moveValue / 2, 0.0);

                changeWidthOfUiElements(moveX.X);
                changeMarginRightOfUiElements(moveValue);

                setGridTransformsOfEllipses(moveX2, moveX2, moveX, null, null, null, moveX, moveX);
                PocketPaintApplication.GetInstance().BarRecEllShape.setTbHeightValue = ellEllipseToDraw.Height;
                PocketPaintApplication.GetInstance().BarRecEllShape.setTbWidthValue = ellEllipseToDraw.Width;
            }
        }

        private void rectRectangleForMovement_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            double coordinateX = 0.0;
            double coordianteY = 0.0;
            double halfScreenHeight = Window.Current.Bounds.Height / 2.0;
            double halfScreenWidth = Window.Current.Bounds.Width / 2.0;
            double offsetX = _transformGridMain.Value.OffsetX;
            double offsetY = _transformGridMain.Value.OffsetY;
            double positionX = 0.0;
            double positionY = 0.0;
            double valueBottom = -20.0 + ellEllipseToDraw.Margin.Bottom;
            double valueLeft = -20.0 + ellEllipseToDraw.Margin.Left;
            double valueRight = -20.0 + ellEllipseToDraw.Margin.Right;
            double valueTop = -20.0 + ellEllipseToDraw.Margin.Top;

            positionX = (ellEllipseToDraw.Width - valueLeft + valueRight) / 2.0;
            positionY = (ellEllipseToDraw.Height + valueBottom - valueTop + 144.0) / 2.0;
            coordinateX = offsetX + halfScreenWidth - positionX;
            coordianteY = offsetY + halfScreenHeight - positionY;
            PocketPaintApplication.GetInstance().ToolCurrent.Draw(new Point(coordinateX, coordianteY));
        }
        public PenLineJoin strokeLineJoinOfRectangleToDraw
        {
            get
            {
                return ellEllipseToDraw.StrokeLineJoin;
            }
            set
            {
                ellEllipseToDraw.StrokeLineJoin = value;
            }
        }

        public void resetRectangleSelectionControl()
        {
            startPosition = new Point(0, 0);
            GridEllCenterBottom.RenderTransform = _transformGridEllipsCenterBottom = new TransformGroup();
            GridEllCenterTop.RenderTransform = _transfomrGridEllipseCenterTop = new TransformGroup();
            GridEllRightCenter.RenderTransform = _transformGridEllipseRightCenter = new TransformGroup();
            GridEllLeftBottom.RenderTransform = _transformGridEllipseLeftBottom = new TransformGroup();
            GridEllLeftCenter.RenderTransform = _transformGridEllipseLeftCenter = new TransformGroup();
            GridEllLeftTop.RenderTransform = _transformGridEllipseLeftTop = new TransformGroup();
            GridEllRightBottom.RenderTransform = _transformGridEllipseRightBottom = new TransformGroup();
            GridEllRightTop.RenderTransform = _transformGridEllipseRightTop = new TransformGroup();
            GridMain.RenderTransform = _transformGridMain = new TransformGroup();

            rectRectangleForMovement.Height = 200.0;
            rectRectangleForMovement.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
            rectRectangleForMovement.Width = 200.0;

            ellEllipseToDraw.Height = 200.0;
            ellEllipseToDraw.Margin = new Thickness(20.0, 20.0, 20.0, 20.0);
            ellEllipseToDraw.Width = 200.0;

            setSizeOfRecBar(200.0, 200.0);

            GridMain.Height = 230.0;
            GridMain.Width = 230.0;
        }

        public void setCurrentSelectedShape(uint shapeValue)
        {
            if(shapeValue > 0 &&shapeValue < 3)
            {
                _currentSelectedShape = shapeValue;
            }
        }
    }
}
