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
    public sealed partial class CutControl : UserControl
    {
        TransformGroup _transformGridEllipsCenterBottom;
        TransformGroup _transfomrGridEllipseCenterTop;
        TransformGroup _transformGridEllipseLeftBottom;
        TransformGroup _transformGridEllipseLeftCenter;
        TransformGroup _transformGridEllipseLeftTop;
        TransformGroup _transformGridEllipseRightBottom;
        TransformGroup _transformGridEllipseRightCenter;
        TransformGroup _transformGridEllipseRightTop;
        TransformGroup _transformGridMain;

        const double MIN_RECTANGLE_MOVE_HEIGHT = 60.0;
        const double MIN_RECTANGLE_MOVE_WIDTH = 60.0;
        bool _isModifiedRectangleMovement;

        public CutControl()
        {
            this.InitializeComponent();

            GridRectCenterBottom.RenderTransform = _transformGridEllipsCenterBottom = new TransformGroup();
            GridRectCenterTop.RenderTransform = _transfomrGridEllipseCenterTop = new TransformGroup();
            GridRectRightCenter.RenderTransform = _transformGridEllipseRightCenter = new TransformGroup();
            GridRectLeftBottom.RenderTransform = _transformGridEllipseLeftBottom = new TransformGroup();
            GridRectLeftCenter.RenderTransform = _transformGridEllipseLeftCenter = new TransformGroup();
            GridRectLeftTop.RenderTransform = _transformGridEllipseLeftTop = new TransformGroup();
            GridRectRightBottom.RenderTransform = _transformGridEllipseRightBottom = new TransformGroup();
            GridRectRightTop.RenderTransform = _transformGridEllipseRightTop = new TransformGroup();
            GridMain.RenderTransform = _transformGridMain = new TransformGroup();
            rectRectangleToDraw.Fill = PocketPaintApplication.GetInstance().PaintData.ColorSelected;
            rectRectangleToDraw.Stroke = PocketPaintApplication.GetInstance().PaintData.BorderColorSelected;
            rectRectangleToDraw.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.BorderThicknessRecEll;
            PocketPaintApplication.GetInstance().CutControl = this;
            setIsModifiedRectangleMovement = false;

            if(!hasElementsPaintingAreaViews())
            {
                rectRectangleForMovement.Stroke = new SolidColorBrush(Colors.Transparent);
            }
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
                                                 TranslateTransform leftCenter, TranslateTransform leftBottom, TranslateTransform leftTop,
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

            PocketPaintApplication.GetInstance().BarRecEllShape.setContentHeightValue = height;

            PocketPaintApplication.GetInstance().BarRecEllShape.setTbWidthValue = width;
        }

        private void rectCenterBottom_ManipulationDelta_1(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Height + e.Delta.Translation.Y) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveY = createTranslateTransform(0.0, Math.Round(e.Delta.Translation.Y));
                var moveY2 = createTranslateTransform(0.0, moveY.Y / 2.0);

                setGridTransformsOfEllipses(moveY, null, moveY2, moveY2, 
                                            moveY, null, moveY, null);

                changeHeightOfUiElements(moveY.Y);
                changeMarginBottomOfUiElements(moveY.Y);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        private void rectCenterTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Height + (e.Delta.Translation.Y * -1)) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveY = createTranslateTransform(0.0, Math.Round(e.Delta.Translation.Y));
                var moveY2 = createTranslateTransform(0.0, moveY.Y / 2.0);

                setGridTransformsOfEllipses(null, moveY, moveY2, moveY2, 
                                            null, moveY, null, moveY);

                changeHeightOfUiElements(moveY.Y * -1.0);
                changeMarginTopOfUiElements(moveY.Y * -1.0);
                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        private void rectLeftBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Width + (e.Delta.Translation.X * -1)) >= MIN_RECTANGLE_MOVE_WIDTH &&
               (rectRectangleForMovement.Height + e.Delta.Translation.Y) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveX = createTranslateTransform(Math.Round(e.Delta.Translation.X), 0.0);
                var moveY = createTranslateTransform(0.0, Math.Round(e.Delta.Translation.Y));
                var moveXY = createTranslateTransform(moveX.X, moveY.Y);
                var moveX2 = createTranslateTransform(moveX.X / 2.0, 0.0);
                var moveY2 = createTranslateTransform(0.0, moveY.Y / 2.0);
                var moveX2Y = createTranslateTransform(moveX2.X, moveY.Y);
                var moveXY2 = createTranslateTransform(moveX.X, moveY2.Y);
                setGridTransformsOfEllipses(moveX2Y, moveX2, moveY2, moveXY2, 
                                            moveXY, moveX, moveY, null);

                changeWidthOfUiElements(moveX.X * -1);
                changeMarginLeftOfUiElements(moveX.X * -1);

                changeHeightOfUiElements(moveY.Y);
                changeMarginBottomOfUiElements(moveY.Y);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        private void rectLeftCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Width + (e.Delta.Translation.X * -1)) >= MIN_RECTANGLE_MOVE_WIDTH)
            {
                var moveX = createTranslateTransform(Math.Round(e.Delta.Translation.X), 0.0);
                var moveX2 = createTranslateTransform(moveX.X / 2.0, 0.0);

                setGridTransformsOfEllipses(moveX2, moveX2, null, moveX, 
                                            moveX, moveX, null, null);

                changeWidthOfUiElements(moveX.X * -1.0);
                changeMarginLeftOfUiElements(moveX.X * -1.0);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }
        
        private void rectLeftTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Width + (e.Delta.Translation.X * -1)) >= MIN_RECTANGLE_MOVE_WIDTH &&
                (rectRectangleForMovement.Height + (e.Delta.Translation.Y * -1)) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveX = createTranslateTransform(Math.Round(e.Delta.Translation.X), 0.0);
                var moveY = createTranslateTransform(0.0, Math.Round(e.Delta.Translation.Y));
                var moveXY = createTranslateTransform(moveX.X, moveY.Y);
                var moveX2 = createTranslateTransform(moveX.X / 2.0, 0.0);
                var moveY2 = createTranslateTransform(0.0, moveY.Y / 2.0);
                var moveX2Y = createTranslateTransform(moveX2.X, moveY.Y);
                var moveXY2 = createTranslateTransform(moveX.X, moveY2.Y);
                setGridTransformsOfEllipses(moveX2, moveX2Y, moveY2, moveXY2, 
                                            moveX, moveXY, null, moveY);

                changeWidthOfUiElements(moveX.X * -1);
                changeMarginLeftOfUiElements(moveX.X * -1);

                changeHeightOfUiElements(moveY.Y * -1.0);
                changeMarginTopOfUiElements(moveY.Y * -1.0);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        private void rectRightBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Width + e.Delta.Translation.X) >= MIN_RECTANGLE_MOVE_WIDTH &&
                (rectRectangleForMovement.Height + e.Delta.Translation.Y) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveX = createTranslateTransform(Math.Round(e.Delta.Translation.X), 0.0);
                var moveY = createTranslateTransform(0.0, Math.Round(e.Delta.Translation.Y));
                var moveXY = createTranslateTransform(moveX.X, moveY.Y);
                var moveX2 = createTranslateTransform(moveX.X / 2.0, 0.0);
                var moveY2 = createTranslateTransform(0.0, moveY.Y / 2.0);
                var moveX2Y = createTranslateTransform(moveX2.X, moveY.Y);
                var moveXY2 = createTranslateTransform(moveX.X, moveY2.Y);
                setGridTransformsOfEllipses(moveX2Y, moveX2, moveXY2, moveY2, 
                                            moveY, null, moveXY, moveX);

                changeMarginRightOfUiElements(moveX.X);
                changeWidthOfUiElements(moveX.X);

                changeHeightOfUiElements(moveY.Y);
                changeMarginBottomOfUiElements(moveY.Y);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }

        }

        private void rectRightCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Width + e.Delta.Translation.X) >= MIN_RECTANGLE_MOVE_WIDTH)
            {
                var moveX = createTranslateTransform(Math.Round(e.Delta.Translation.X), 0.0);
                var moveX2 = createTranslateTransform(moveX.X / 2.0, 0.0);

                setGridTransformsOfEllipses(moveX2, moveX2, moveX, null, 
                                            null, null, moveX, moveX);

                changeWidthOfUiElements(moveX.X);
                changeMarginRightOfUiElements(moveX.X);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        private void rectRightTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Width + e.Delta.Translation.X) >= MIN_RECTANGLE_MOVE_WIDTH &&
               (rectRectangleForMovement.Height + (e.Delta.Translation.Y * -1)) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveX = createTranslateTransform(Math.Round(e.Delta.Translation.X), 0.0);
                var moveY = createTranslateTransform(0.0, Math.Round(e.Delta.Translation.Y));
                var moveXY = createTranslateTransform(moveX.X, moveY.Y);
                var moveX2 = createTranslateTransform(moveX.X / 2.0, 0.0);
                var moveY2 = createTranslateTransform(0.0, moveY.Y / 2.0);
                var moveX2Y = createTranslateTransform(moveX2.X, moveY.Y);
                var moveXY2 = createTranslateTransform(moveX.X, moveY2.Y);
                setGridTransformsOfEllipses(moveX2, moveX2Y, moveXY2, moveY2,
                                            null, moveY, moveX, moveXY);

                changeMarginRightOfUiElements(moveX.X);
                changeWidthOfUiElements(moveX.X);

                changeHeightOfUiElements(moveY.Y * -1.0);
                changeMarginTopOfUiElements(moveY.Y * -1.0);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
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

            resetAppBarButtonRectangleSelectionControl(true);
            setIsModifiedRectangleMovement = true;

            PocketPaintApplication.GetInstance().BarRecEllShape.updateSldStrokeThickness(Convert.ToInt32(rectRectangleToDraw.Height), Convert.ToInt32(rectRectangleToDraw.Width));
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

            resetAppBarButtonRectangleSelectionControl(true);
            setIsModifiedRectangleMovement = true;

            PocketPaintApplication.GetInstance().BarRecEllShape.updateSldStrokeThickness(Convert.ToInt32(rectRectangleToDraw.Height), Convert.ToInt32(rectRectangleToDraw.Width));
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
            var move = new TranslateTransform();
            ((TranslateTransform)move).X = e.Delta.Translation.X;
            ((TranslateTransform)move).Y = e.Delta.Translation.Y;

            _transformGridMain.Children.Add(move);

            move.X = _transformGridMain.Value.OffsetX;
            move.Y = _transformGridMain.Value.OffsetY;
            _transformGridMain.Children.Clear();
            _transformGridMain.Children.Add(move);

            resetAppBarButtonRectangleSelectionControl(true);
            setIsModifiedRectangleMovement = true;

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

        public void changeHeightOfDrawingSelection(double newHeight, bool changeTbValues)
        {
            double moveValue = newHeight - rectRectangleToDraw.Height;

            if ((rectRectangleForMovement.Height + moveValue) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveY = createTranslateTransform(0.0, moveValue);
                var moveY2 = createTranslateTransform(0.0, moveValue / 2.0);

                changeHeightOfUiElements(moveY.Y);
                changeMarginBottomOfUiElements(moveValue);

                setGridTransformsOfEllipses(moveY, null, moveY2, moveY2, 
                                            moveY, null, moveY, null);
                if (changeTbValues) {
                    PocketPaintApplication.GetInstance().BarRecEllShape.setContentHeightValue = rectRectangleToDraw.Height;
                    PocketPaintApplication.GetInstance().BarRecEllShape.setTbWidthValue = rectRectangleToDraw.Width;
                }
                resetAppBarButtonRectangleSelectionControl(true);
                setIsModifiedRectangleMovement = true;
            }

            PocketPaintApplication.GetInstance().BarRecEllShape.updateSldStrokeThickness(Convert.ToInt32(rectRectangleToDraw.Height), Convert.ToInt32(rectRectangleToDraw.Width));
        }

        public void changeWidthOfDrawingSelection(double newWidth, bool changeTbValues)
        {
            double moveValue = newWidth - rectRectangleToDraw.Width;
            if ((rectRectangleForMovement.Width + moveValue) >= MIN_RECTANGLE_MOVE_WIDTH)
            {
                var moveX = createTranslateTransform(moveValue, 0.0);
                var moveX2 = createTranslateTransform(moveValue / 2.0, 0.0);

                changeWidthOfUiElements(moveX.X);
                changeMarginRightOfUiElements(moveValue);

                setGridTransformsOfEllipses(moveX2, moveX2, moveX, null, 
                                            null, null, moveX, moveX);
                if (changeTbValues)
                {
                    PocketPaintApplication.GetInstance().BarRecEllShape.setContentHeightValue = rectRectangleToDraw.Height;
                    PocketPaintApplication.GetInstance().BarRecEllShape.setTbWidthValue = rectRectangleToDraw.Width;
                }
                resetAppBarButtonRectangleSelectionControl(true);
                setIsModifiedRectangleMovement = true;
            }

            PocketPaintApplication.GetInstance().BarRecEllShape.updateSldStrokeThickness(Convert.ToInt32(rectRectangleToDraw.Height), Convert.ToInt32(rectRectangleToDraw.Width));
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
            double valueBottom = -20.0 + rectRectangleToDraw.Margin.Bottom;
            double valueLeft = -20.0 + rectRectangleToDraw.Margin.Left;
            double valueRight = -20.0 + rectRectangleToDraw.Margin.Right;
            double valueTop = -20.0 + rectRectangleToDraw.Margin.Top;

            positionX = (rectRectangleToDraw.Width - valueLeft + valueRight) / 2.0;
            positionY = (rectRectangleToDraw.Height + valueBottom - valueTop + 144.0) / 2.0;
            coordinateX = offsetX + halfScreenWidth - positionX;
            coordianteY = offsetY + halfScreenHeight - positionY;
            PocketPaintApplication.GetInstance().ToolCurrent.Draw(new Point(coordinateX, coordianteY));
        }
        public PenLineJoin strokeLineJoinOfRectangleToDraw
        {
            get
            {
                return rectRectangleToDraw.StrokeLineJoin;
            }
            set
            {
                rectRectangleToDraw.StrokeLineJoin = value;
            }
        }

        public void resetAppBarButtonRectangleSelectionControl(bool activated)
        {
            AppBarButton appBarButtonReset = PocketPaintApplication.GetInstance().PaintingAreaView.getAppBarResetButton();
            if (appBarButtonReset != null)
            {
                appBarButtonReset.IsEnabled = activated;
            }
        }

        public bool setIsModifiedRectangleMovement
        {
            get
            {
                return _isModifiedRectangleMovement;
            }
            set
            {
                _isModifiedRectangleMovement = value;
            }
        }

        public Point getCenterPointOfSelectionControl()
        {
            double coordinateX = 0.0;
            double coordianteY = 0.0;
            double halfScreenHeight = Window.Current.Bounds.Height / 2.0;
            double halfScreenWidth = Window.Current.Bounds.Width / 2.0;
            double offsetX = _transformGridMain.Value.OffsetX;
            double offsetY = _transformGridMain.Value.OffsetY;
            double valueBottom = rectRectangleToDraw.Margin.Bottom;
            double valueLeft = rectRectangleToDraw.Margin.Left;
            double valueRight = rectRectangleToDraw.Margin.Right;
            double valueTop = rectRectangleToDraw.Margin.Top;

            coordinateX = offsetX + halfScreenWidth + (valueLeft - valueRight) / 2.0;
            coordianteY = offsetY + halfScreenHeight - 72.0 + (valueTop - valueBottom) / 2.0;

            return new Point(rectRectangleForMovement.Width/2.0, rectRectangleForMovement.Height/2.0);
        }

        public bool hasElementsPaintingAreaViews()
        {
            bool result = false;
            if (PocketPaintApplication.GetInstance().PaintingAreaCanvas != null)
            {
                result = PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count > 0 ? true : false;
            }
            return false;
        }
    }
}
