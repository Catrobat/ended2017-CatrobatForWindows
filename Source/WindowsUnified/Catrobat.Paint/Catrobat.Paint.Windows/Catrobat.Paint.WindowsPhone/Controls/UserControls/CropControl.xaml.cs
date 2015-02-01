using System;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// Die Elementvorlage "Benutzersteuerelement" ist unter http://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    public sealed partial class CropControl : UserControl
    {
        TransformGroup _transformGridMain;

        const double MIN_RECTANGLE_MOVE_HEIGHT = 60.0;
        const double MIN_RECTANGLE_MOVE_WIDTH = 60.0;
        bool _isModifiedRectangleMovement;
        double mobileDisplayHeight = 0.0;
        double mobileDisplayWidth = 0.0;

        public CropControl()
        {
            this.InitializeComponent();
            mobileDisplayHeight = Window.Current.Bounds.Height;
            mobileDisplayWidth = Window.Current.Bounds.Width;
            _transformGridMain = new TransformGroup();
            GridMain.RenderTransform = _transformGridMain;
            PocketPaintApplication.GetInstance().CropControl = this;
            setIsModifiedRectangleMovement = false;

            //if(!hasElementsPaintingAreaViews())
            //{
            //    rectRectangleForMovement.Stroke = new SolidColorBrush(Colors.Transparent);
            //}
        }

        public void setMainGridSize(double height, double width)
        {
            GridMain.Height = height;
            GridMain.Width = width;
        }

        public void setRectangleForMovementSize(double height, double width)
        {
            rectRectangleForMovement.Height = height;
            rectRectangleForMovement.Width = width;
        }

        async public void setCropSelection()
        {
            PocketPaintApplication.GetInstance().ProgressRing.IsActive = true;
            PixelData.PixelData pixelData = new PixelData.PixelData();
            await pixelData.preparePaintingAreaCanvasPixel();
            TransformGroup paintingAreaCheckeredGridTransformGroup = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            // TODO: Besseren Namen finden. Das Selection-Control soll die Arbeitsfläche einschließen und nicht darauf liegen. Daher wird
            // dieser Wert mit 10 verwendet. Anschließend wird dann die Margin Left und Top, um 5 verringert.
            double offsetSize = 10.0;
            double offsetMargin = 5.0;
            double heightCropControl = 0.0;
            double widthCropControl = 0.0;
            double scaleValue = 0.0;
            if(paintingAreaCheckeredGridTransformGroup.Value.M11 > 0.0)
            {
                // Calculate the position from crop selection in connection with the working space respectively with the drawing
                // in the working space. In other words the crop selection should be adapted on the drawing in the working space.
                double leftX = PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth;
                double rightX = 0;
                double bottomY = 0;
                double topY = PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualHeight;
                if (PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count != 0)
                {
                    for (int indexHeight = 0; indexHeight < mobileDisplayHeight; indexHeight++)
                    {
                        for (int indexWidth = 0; indexWidth < mobileDisplayWidth; indexWidth++)
                        {
                            SolidColorBrush brush = pixelData.getPixelFromCanvas(indexWidth, indexHeight);
                            if (brush.Color.A != 0x00)
                            {
                                leftX = indexWidth < leftX ? indexWidth : leftX;
                                topY = indexHeight < topY ? indexHeight : topY;
                                rightX = indexWidth > rightX ? indexWidth : rightX;
                                bottomY = indexHeight > bottomY ? indexHeight : bottomY;
                            }
                        }
                    }
                    scaleValue = paintingAreaCheckeredGridTransformGroup.Value.M11;
                    heightCropControl = (bottomY - topY) * scaleValue + offsetSize;
                    widthCropControl = (rightX - leftX) * scaleValue + offsetSize;
                    GridMain.Margin = new Thickness(paintingAreaCheckeredGridTransformGroup.Value.OffsetX - offsetMargin + (leftX * scaleValue),
                                               paintingAreaCheckeredGridTransformGroup.Value.OffsetY - offsetMargin + (topY * scaleValue), 0, 0);
                }
                else
                {
                    heightCropControl = paintingAreaCheckeredGridTransformGroup.Value.M11 * mobileDisplayHeight + offsetSize;
                    widthCropControl = paintingAreaCheckeredGridTransformGroup.Value.M11 * mobileDisplayWidth + offsetSize;
                    GridMain.Margin = new Thickness(paintingAreaCheckeredGridTransformGroup.Value.OffsetX - offsetMargin,
                                                paintingAreaCheckeredGridTransformGroup.Value.OffsetY - offsetMargin, 0, 0);
                }
            }
            else if(paintingAreaCheckeredGridTransformGroup.Value.M11 < 0.0)
            {
                heightCropControl = Math.Abs(paintingAreaCheckeredGridTransformGroup.Value.M11) * mobileDisplayHeight + offsetSize;
                widthCropControl = Math.Abs(paintingAreaCheckeredGridTransformGroup.Value.M11) * mobileDisplayWidth + offsetSize;
                double workingSpaceHeight = Math.Abs(paintingAreaCheckeredGridTransformGroup.Value.M11) * mobileDisplayHeight;
                double workingSpaceWidth = Math.Abs(paintingAreaCheckeredGridTransformGroup.Value.M11) * mobileDisplayWidth;
                double positionXRightBottomCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetX;
                double positionXLeftBottomCornerWorkingSpace = positionXRightBottomCornerWorkingSpace - workingSpaceWidth;
                double positionYRigthBottomCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetY;
                double positionYRightTopCornerWorkingSpace = positionYRigthBottomCornerWorkingSpace - workingSpaceHeight;
                GridMain.Margin = new Thickness(positionXLeftBottomCornerWorkingSpace - offsetMargin,
                                                positionYRightTopCornerWorkingSpace - offsetMargin, 0, 0);

            }
            else if(paintingAreaCheckeredGridTransformGroup.Value.M12 > 0.0)
            {
                heightCropControl = paintingAreaCheckeredGridTransformGroup.Value.M12 * mobileDisplayWidth + offsetSize;
                widthCropControl = paintingAreaCheckeredGridTransformGroup.Value.M12 * mobileDisplayHeight + offsetSize;
                double workingSpaceHeight = paintingAreaCheckeredGridTransformGroup.Value.M12 * mobileDisplayWidth;
                double workingSpaceWidth = paintingAreaCheckeredGridTransformGroup.Value.M12 * mobileDisplayHeight;
                double positionXRightTopCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetX;
                double positionXLeftTopCornerWorkingSpace = positionXRightTopCornerWorkingSpace - workingSpaceWidth;
                GridMain.Margin = new Thickness(positionXLeftTopCornerWorkingSpace - offsetMargin,
                                                paintingAreaCheckeredGridTransformGroup.Value.OffsetY - offsetMargin, 0, 0);

            }
            else if(paintingAreaCheckeredGridTransformGroup.Value.M12 < 0.0)
            {
                heightCropControl = paintingAreaCheckeredGridTransformGroup.Value.M21 * mobileDisplayWidth + offsetSize;
                widthCropControl = paintingAreaCheckeredGridTransformGroup.Value.M21 * mobileDisplayHeight + offsetSize;
                double workingSpaceHeight = paintingAreaCheckeredGridTransformGroup.Value.M21 * mobileDisplayWidth;
                double workingSpaceWidth = paintingAreaCheckeredGridTransformGroup.Value.M21 * mobileDisplayHeight;
                double positionYLeftBottomCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetY;
                double positionYLeftTopCornerWorkingSpace = positionYLeftBottomCornerWorkingSpace - workingSpaceHeight;
                GridMain.Margin = new Thickness(paintingAreaCheckeredGridTransformGroup.Value.OffsetX - offsetMargin,
                                                positionYLeftTopCornerWorkingSpace - offsetMargin, 0, 0);
            }
            PocketPaintApplication.GetInstance().ProgressRing.IsActive = false;
            setMainGridSize(heightCropControl, widthCropControl);
            setRectangleForMovementSize(heightCropControl, widthCropControl);
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

            PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = height;

            PocketPaintApplication.GetInstance().BarRecEllShape.setBtnWidthValue = width;
        }

        private void rectCenterBottom_ManipulationDelta_1(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Height + e.Delta.Translation.Y) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveY = createTranslateTransform(0.0, e.Delta.Translation.Y);

                changeHeightOfUiElements(moveY.Y);
                changeMarginBottomOfUiElements(moveY.Y);
            }
        }

        private void rectCenterTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Height + (e.Delta.Translation.Y * -1)) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveY = createTranslateTransform(0.0, e.Delta.Translation.Y);
                var moveY2 = createTranslateTransform(0.0, moveY.Y / 2.0);

                changeHeightOfUiElements(moveY.Y * -1.0);
                changeMarginTopOfUiElements(moveY.Y * -1.0);
            }
        }

        private void rectLeftBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Width + (e.Delta.Translation.X * -1)) >= MIN_RECTANGLE_MOVE_WIDTH &&
               (rectRectangleForMovement.Height + e.Delta.Translation.Y) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveX = createTranslateTransform((e.Delta.Translation.X), 0.0);
                var moveY = createTranslateTransform(0.0, (e.Delta.Translation.Y));
                var moveXY = createTranslateTransform(moveX.X, moveY.Y);
                var moveX2 = createTranslateTransform(moveX.X / 2.0, 0.0);
                var moveY2 = createTranslateTransform(0.0, moveY.Y / 2.0);
                var moveX2Y = createTranslateTransform(moveX2.X, moveY.Y);
                var moveXY2 = createTranslateTransform(moveX.X, moveY2.Y);

                changeWidthOfUiElements(moveX.X * -1);
                changeMarginLeftOfUiElements(moveX.X * -1);

                changeHeightOfUiElements(moveY.Y);
                changeMarginBottomOfUiElements(moveY.Y);
            }
        }

        private void rectLeftCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Width + (e.Delta.Translation.X * -1)) >= MIN_RECTANGLE_MOVE_WIDTH)
            {
                var moveX = createTranslateTransform((e.Delta.Translation.X), 0.0);
                var moveX2 = createTranslateTransform(moveX.X / 2.0, 0.0);

                changeWidthOfUiElements(moveX.X * -1.0);
                changeMarginLeftOfUiElements(moveX.X * -1.0);
            }
        }
        
        private void rectLeftTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Width + (e.Delta.Translation.X * -1)) >= MIN_RECTANGLE_MOVE_WIDTH &&
                (rectRectangleForMovement.Height + (e.Delta.Translation.Y * -1)) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveX = createTranslateTransform((e.Delta.Translation.X), 0.0);
                var moveY = createTranslateTransform(0.0, (e.Delta.Translation.Y));
                var moveXY = createTranslateTransform(moveX.X, moveY.Y);
                var moveX2 = createTranslateTransform(moveX.X / 2.0, 0.0);
                var moveY2 = createTranslateTransform(0.0, moveY.Y / 2.0);
                var moveX2Y = createTranslateTransform(moveX2.X, moveY.Y);
                var moveXY2 = createTranslateTransform(moveX.X, moveY2.Y);

                changeWidthOfUiElements(moveX.X * -1);
                changeMarginLeftOfUiElements(moveX.X * -1);

                changeHeightOfUiElements(moveY.Y * -1.0);
                changeMarginTopOfUiElements(moveY.Y * -1.0);
            }
        }

        private void rectRightBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Width + e.Delta.Translation.X) >= MIN_RECTANGLE_MOVE_WIDTH &&
                (rectRectangleForMovement.Height + e.Delta.Translation.Y) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveX = createTranslateTransform((e.Delta.Translation.X), 0.0);
                var moveY = createTranslateTransform(0.0, (e.Delta.Translation.Y));
                var moveXY = createTranslateTransform(moveX.X, moveY.Y);
                var moveX2 = createTranslateTransform(moveX.X / 2.0, 0.0);
                var moveY2 = createTranslateTransform(0.0, moveY.Y / 2.0);
                var moveX2Y = createTranslateTransform(moveX2.X, moveY.Y);
                var moveXY2 = createTranslateTransform(moveX.X, moveY2.Y);

                changeMarginRightOfUiElements(moveX.X);
                changeWidthOfUiElements(moveX.X);

                changeHeightOfUiElements(moveY.Y);
                changeMarginBottomOfUiElements(moveY.Y);
            }

        }

        private void rectRightCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Width + e.Delta.Translation.X) >= MIN_RECTANGLE_MOVE_WIDTH)
            {
                var moveX = createTranslateTransform((e.Delta.Translation.X), 0.0);
                var moveX2 = createTranslateTransform(moveX.X / 2.0, 0.0);

                changeWidthOfUiElements(moveX.X);
                changeMarginRightOfUiElements(moveX.X);
            }
        }

        private void rectRightTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Width + e.Delta.Translation.X) >= MIN_RECTANGLE_MOVE_WIDTH &&
               (rectRectangleForMovement.Height + (e.Delta.Translation.Y * -1)) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveX = createTranslateTransform((e.Delta.Translation.X), 0.0);
                var moveY = createTranslateTransform(0.0, (e.Delta.Translation.Y));
                var moveXY = createTranslateTransform(moveX.X, moveY.Y);
                var moveX2 = createTranslateTransform(moveX.X / 2.0, 0.0);
                var moveY2 = createTranslateTransform(0.0, moveY.Y / 2.0);
                var moveX2Y = createTranslateTransform(moveX2.X, moveY.Y);
                var moveXY2 = createTranslateTransform(moveX.X, moveY2.Y);

                changeMarginRightOfUiElements(moveX.X);
                changeWidthOfUiElements(moveX.X);

                changeHeightOfUiElements(moveY.Y * -1.0);
                changeMarginTopOfUiElements(moveY.Y * -1.0);
            }

        }

        private void changeHeightOfUiElements(double value)
        {
            GridMain.Height += value;
            rectRectangleForMovement.Height += value;
            resetAppBarButtonRectangleSelectionControl(true);
            setIsModifiedRectangleMovement = true;
        }

        private void changeWidthOfUiElements(double value)
        {
            GridMain.Width += value;
            rectRectangleForMovement.Width += value;

            resetAppBarButtonRectangleSelectionControl(true);
            setIsModifiedRectangleMovement = true;
        }

        private void changeMarginBottomOfUiElements(double value)
        {
            GridMain.Margin = new Thickness(GridMain.Margin.Left, GridMain.Margin.Top, 
                GridMain.Margin.Right, GridMain.Margin.Bottom - value);
        }

        private void changeMarginLeftOfUiElements(double value)
        {
            GridMain.Margin = new Thickness(GridMain.Margin.Left - value, GridMain.Margin.Top, 
                GridMain.Margin.Right, GridMain.Margin.Bottom);
        }

        private void changeMarginRightOfUiElements(double value)
        {
            GridMain.Margin = new Thickness(GridMain.Margin.Left, GridMain.Margin.Top, 
                GridMain.Margin.Right - value, GridMain.Margin.Bottom);
        }

        private void changeMarginTopOfUiElements(double value)
        {
            GridMain.Margin = new Thickness(GridMain.Margin.Left, GridMain.Margin.Top - value,
                GridMain.Margin.Right, GridMain.Margin.Bottom);
        }

        private void rectRectangleForMovement_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews())
            {
                var move = new TranslateTransform();
                ((TranslateTransform)move).X = e.Delta.Translation.X;
                ((TranslateTransform)move).Y = e.Delta.Translation.Y;

                _transformGridMain.Children.Add(move);

                //move.X = _transformGridMain.Value.OffsetX;
                //move.Y = _transformGridMain.Value.OffsetY;
                //_transformGridMain.Children.Clear();
                //_transformGridMain.Children.Add(move);

                resetAppBarButtonRectangleSelectionControl(true);
                setIsModifiedRectangleMovement = true;
            }
        }

        private void rectRectangleForMovement_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            PocketPaintApplication.GetInstance().ToolCurrent.HandleUp(new Point());
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

        public bool hasElementsPaintingAreaViews()
        {
            bool result = false;
            if (PocketPaintApplication.GetInstance().PaintingAreaCanvas != null)
            {
                result = PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count > 0 ? true : false;
            }
            return result;
        }
    }
}
