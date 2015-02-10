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

        double limitLeft = 0.0;
        double limitRight = 0.0;
        double limitBottom = 0.0;
        double limitTop = 0.0;

        double scaleValue = 0.0;

        PixelData.PixelData pixelData = new PixelData.PixelData();

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

        private Point getExtremeLeftAndTopCoordinate(double initLeft, double initTop, 
                                                     ref bool foundLeftPixel, ref int xCoordinateOfExtremeTop)
        {
            Point extremePoint = new Point(initLeft, initTop);
            foundLeftPixel = false;

            // left pixel
            for (int indexWidth = 0; indexWidth < (int)mobileDisplayWidth; indexWidth++)
                for (int indexHeight = 0; indexHeight < (int)mobileDisplayHeight; indexHeight++)
                {
                    SolidColorBrush brush = pixelData.getPixelFromCanvas(indexWidth, indexHeight);
                    if (brush.Color.A != 0x00)
                    {
                        extremePoint.X = indexWidth;
                        foundLeftPixel = true;

                        // found extreme point --> set break conditions
                        indexWidth = (int)mobileDisplayWidth;
                        indexHeight = (int)mobileDisplayHeight;
                    }
            }

            // top pixel
            if (foundLeftPixel == true)
                for (int indexHeight = 0; indexHeight < (int)mobileDisplayHeight; indexHeight++)
                    for (int indexWidth = (int)mobileDisplayWidth - 1; indexWidth >= (int)extremePoint.X; indexWidth--)
                    {
                        SolidColorBrush brush = pixelData.getPixelFromCanvas(indexWidth, indexHeight);
                        if (brush.Color.A != 0x00)
                        {
                            extremePoint.Y = indexHeight;
                            xCoordinateOfExtremeTop = indexWidth;

                            // found extreme point --> set break conditions
                            indexHeight = (int)mobileDisplayHeight;
                            indexWidth = 0;
                        }
                    }

            return extremePoint;
        }

        private Point getExtremeRightAndBottomCoordinate(double initRight, double initBottom, 
                                                         Point extremeLeftAndTopCoordinate, bool foundLeftPixel,
                                                         int xCoordinateOfExtremeTop)
        {
            Point extremePoint = new Point(initRight, initBottom);

            if (foundLeftPixel == true)
            {
                // right pixel
                int yCoordinateOfExtremeRight = 0;
                for (int indexWidth = (int)mobileDisplayWidth - 1; indexWidth >= xCoordinateOfExtremeTop; indexWidth--)
                    for (int indexHeight = (int)mobileDisplayHeight - 1; indexHeight >= extremeLeftAndTopCoordinate.Y ; indexHeight--)
                    {
                        SolidColorBrush brush = pixelData.getPixelFromCanvas(indexWidth, indexHeight);
                        if (brush.Color.A != 0x00)
                        {
                            extremePoint.X = indexWidth;
                            yCoordinateOfExtremeRight = indexHeight;

                            // found extreme point --> set break conditions
                            indexWidth = 0;
                            indexHeight = 0;
                        }
                }

                // bottom pixel
                for (int indexHeight = (int)mobileDisplayHeight - 1; indexHeight >= yCoordinateOfExtremeRight; indexHeight--)
                    for (int indexWidth = (int)extremePoint.X; indexWidth >= (int)extremeLeftAndTopCoordinate.X; indexWidth--)
                    {
                        SolidColorBrush brush = pixelData.getPixelFromCanvas(indexWidth, indexHeight);
                        if (brush.Color.A != 0x00)
                        {
                            extremePoint.Y = indexHeight;

                            // found extreme point --> set break conditions
                            indexHeight = 0;
                            indexWidth = 0;
                        }
                    }
            }

            return extremePoint;
        }

        // TODO: Refactor the setCropSelection function.

        async public void setCropSelection()
        {
            GridMain.Visibility = Visibility.Collapsed;
            TransformGroup transform = (PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform as TransformGroup);
            _transformGridMain.Children.Clear();
            GridMain.Margin = new Thickness(-5, -5, 0, 0);
            PocketPaintApplication.GetInstance().ProgressRing.IsActive = true;
            await pixelData.preparePaintingAreaCanvasPixel();
            TransformGroup paintingAreaCheckeredGridTransformGroup = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;

            rectRectangleForMovement.Stroke = PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count == 0 ? new SolidColorBrush(Colors.Transparent) : new SolidColorBrush(Colors.CornflowerBlue);
            // TODO: Besseren Namen finden. Das Selection-Control soll die Arbeitsfläche einschließen und nicht darauf liegen. Daher wird
            // dieser Wert mit 10 verwendet. Anschließend wird dann die Margin Left und Top, um 5 verringert.
            double offsetSize = 10.0;
            double offsetMargin = 5.0;
            double heightCropControl = 0.0;
            double widthCropControl = 0.0;
            scaleValue = 0.0;
            if(paintingAreaCheckeredGridTransformGroup.Value.M11 > 0.0)
            {
                // Calculate the position from crop selection in connection with the working space respectively with the drawing
                // in the working space. In other words the crop selection should be adapted on the drawing in the working space.
                Point extremeLeftAndTopCoordinate = new Point(0.0, 0.0);
                Point extremeRightAndBottomCoordinate = new Point(mobileDisplayWidth - 1.0, mobileDisplayHeight - 1.0);

                if (PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count != 0)
                {
                    bool foundLeftPixel = false;
                    int xCoordinateOfExtremeTop = 0;
                    extremeLeftAndTopCoordinate = getExtremeLeftAndTopCoordinate(extremeLeftAndTopCoordinate.X, extremeLeftAndTopCoordinate.Y, 
                                                                                 ref foundLeftPixel, ref xCoordinateOfExtremeTop);
                    extremeRightAndBottomCoordinate = getExtremeRightAndBottomCoordinate(extremeRightAndBottomCoordinate.X, extremeRightAndBottomCoordinate.Y,
                                                                                         extremeLeftAndTopCoordinate, foundLeftPixel, xCoordinateOfExtremeTop);

                    scaleValue = paintingAreaCheckeredGridTransformGroup.Value.M11;
                    heightCropControl = (extremeRightAndBottomCoordinate.Y - extremeLeftAndTopCoordinate.Y) * scaleValue + offsetSize;
                    widthCropControl = (extremeRightAndBottomCoordinate.X - extremeLeftAndTopCoordinate.X) * scaleValue + offsetSize;

                    TranslateTransform moveCropControl = new TranslateTransform();

                    // (M11 == -1.0) means that working space is flipped horizontally. 
                    if ((PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform as TransformGroup).Value.M11 == -1.0)
                    {
                        moveCropControl.X = paintingAreaCheckeredGridTransformGroup.Value.OffsetX + ((mobileDisplayWidth - extremeRightAndBottomCoordinate.X) * scaleValue);
                    }
                    else
                    {
                        moveCropControl.X = paintingAreaCheckeredGridTransformGroup.Value.OffsetX + (extremeLeftAndTopCoordinate.X * scaleValue);
                    }

                    // (M22 == -1.0) means that working space is flipped vertically. 
                    if ((PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform as TransformGroup).Value.M22 == -1.0)
                    {
                        moveCropControl.Y = paintingAreaCheckeredGridTransformGroup.Value.OffsetY + ((mobileDisplayHeight - extremeRightAndBottomCoordinate.Y) * scaleValue);
                    }
                    else
                    {
                        moveCropControl.Y = paintingAreaCheckeredGridTransformGroup.Value.OffsetY + (extremeLeftAndTopCoordinate.Y * scaleValue);
                    }

                    _transformGridMain.Children.Add(moveCropControl);
                }
                else
                {
                    heightCropControl = paintingAreaCheckeredGridTransformGroup.Value.M11 * mobileDisplayHeight + offsetSize;
                    widthCropControl = paintingAreaCheckeredGridTransformGroup.Value.M11 * mobileDisplayWidth + offsetSize;

                    TranslateTransform moveCropControl = new TranslateTransform();
                    moveCropControl.X = paintingAreaCheckeredGridTransformGroup.Value.OffsetX;
                    moveCropControl.Y = paintingAreaCheckeredGridTransformGroup.Value.OffsetY;
                    _transformGridMain.Children.Add(moveCropControl);
                }
                limitLeft = paintingAreaCheckeredGridTransformGroup.Value.OffsetX - offsetMargin;
                limitTop = paintingAreaCheckeredGridTransformGroup.Value.OffsetY - offsetMargin;
                // TODO: Explain the following line.
                limitBottom = limitTop + (mobileDisplayHeight * scaleValue) + offsetMargin * 2;
                limitRight = limitLeft + (mobileDisplayWidth * scaleValue) + offsetMargin * 2;
            }
            else if(paintingAreaCheckeredGridTransformGroup.Value.M11 < 0.0)
            {
                scaleValue = Math.Abs(paintingAreaCheckeredGridTransformGroup.Value.M11);

                // Attention: Working space is rotated 180°
                Point extremeLeftAndTopCoordinate = new Point(mobileDisplayWidth - 1.0, 0.0);
                Point extremeRightAndBottomCoordinate = new Point(0.0, mobileDisplayHeight - 1.0);

                if (PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count != 0)
                {
                    bool foundLeftPixel = false;
                    int xCoordinateOfExtremeTop = 0;
                    extremeLeftAndTopCoordinate = getExtremeLeftAndTopCoordinate(extremeLeftAndTopCoordinate.X, extremeLeftAndTopCoordinate.Y,
                                                                                 ref foundLeftPixel, ref xCoordinateOfExtremeTop);
                    extremeRightAndBottomCoordinate = getExtremeRightAndBottomCoordinate(extremeRightAndBottomCoordinate.X, extremeRightAndBottomCoordinate.Y,
                                                                                         extremeLeftAndTopCoordinate, foundLeftPixel, xCoordinateOfExtremeTop);

                    heightCropControl = (extremeRightAndBottomCoordinate.Y - extremeLeftAndTopCoordinate.Y) * scaleValue + offsetSize;
                    widthCropControl = (extremeRightAndBottomCoordinate.X - extremeLeftAndTopCoordinate.X) * scaleValue + offsetSize;

                    double workingSpaceHeight = scaleValue * mobileDisplayHeight;
                    double workingSpaceWidth = scaleValue * mobileDisplayWidth;
                    double positionXRightBottomCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetX;
                    double positionXLeftBottomCornerWorkingSpace = positionXRightBottomCornerWorkingSpace - workingSpaceWidth;
                    double positionYRigthBottomCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetY;
                    double positionYRightTopCornerWorkingSpace = positionYRigthBottomCornerWorkingSpace - workingSpaceHeight;

                    TranslateTransform moveCropControl = new TranslateTransform();
                    // (M11 == -1.0) means that working space is mirrored horizontally. 
                    if ((PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform as TransformGroup).Value.M11 == -1.0)
                    {
                        moveCropControl.X = positionXLeftBottomCornerWorkingSpace + (extremeLeftAndTopCoordinate.X * scaleValue);
                    }
                    else
                    {
                        moveCropControl.X = positionXLeftBottomCornerWorkingSpace + ((mobileDisplayWidth - extremeRightAndBottomCoordinate.X) * scaleValue);
                    }

                    // (M22 == -1.0) means that working space is mirrored vertically. 
                    if ((PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform as TransformGroup).Value.M22 == -1.0)
                    {
                        moveCropControl.Y = positionYRightTopCornerWorkingSpace + (extremeLeftAndTopCoordinate.Y * scaleValue);
                    }
                    else
                    {
                        moveCropControl.Y = positionYRightTopCornerWorkingSpace + ((mobileDisplayHeight - extremeRightAndBottomCoordinate.Y) * scaleValue);
                    }

                    _transformGridMain.Children.Add(moveCropControl);
                }
                else
                {


                    heightCropControl = scaleValue * mobileDisplayHeight + offsetSize;
                    widthCropControl = scaleValue * mobileDisplayWidth + offsetSize;
                    double workingSpaceHeight = scaleValue * mobileDisplayHeight;
                    double workingSpaceWidth = scaleValue * mobileDisplayWidth;
                    double positionXRightBottomCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetX;
                    double positionXLeftBottomCornerWorkingSpace = positionXRightBottomCornerWorkingSpace - workingSpaceWidth;
                    double positionYRigthBottomCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetY;
                    double positionYRightTopCornerWorkingSpace = positionYRigthBottomCornerWorkingSpace - workingSpaceHeight;

                    TranslateTransform moveCropControl = new TranslateTransform();
                    moveCropControl.X = positionXLeftBottomCornerWorkingSpace;
                    moveCropControl.Y = positionYRightTopCornerWorkingSpace;
                    _transformGridMain.Children.Add(moveCropControl);
                }
                limitRight = paintingAreaCheckeredGridTransformGroup.Value.OffsetX + offsetMargin;
                limitBottom = paintingAreaCheckeredGridTransformGroup.Value.OffsetY + offsetMargin;
                // TODO: Explain the following line.
                limitTop = limitBottom - (mobileDisplayHeight * scaleValue) - offsetMargin * 2;
                limitLeft = limitRight - (mobileDisplayWidth * scaleValue) - offsetMargin * 2;

            }
            else if(paintingAreaCheckeredGridTransformGroup.Value.M12 > 0.0)
            {
                scaleValue = paintingAreaCheckeredGridTransformGroup.Value.M12;

                // Attention: Working space is rotated 90°
                Point extremeLeftAndTopCoordinate = new Point(mobileDisplayWidth - 1.0, mobileDisplayHeight - 1.0);
                Point extremeRightAndBottomCoordinate = new Point(0.0, 0.0);
                if (PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count != 0)
                {
                    bool foundLeftPixel = false;
                    int xCoordinateOfExtremeTop = 0;
                    extremeLeftAndTopCoordinate = getExtremeLeftAndTopCoordinate(extremeLeftAndTopCoordinate.X, extremeLeftAndTopCoordinate.Y,
                                                                                 ref foundLeftPixel, ref xCoordinateOfExtremeTop);
                    extremeRightAndBottomCoordinate = getExtremeRightAndBottomCoordinate(extremeRightAndBottomCoordinate.X, extremeRightAndBottomCoordinate.Y,
                                                                                         extremeLeftAndTopCoordinate, foundLeftPixel, xCoordinateOfExtremeTop);

                    heightCropControl = (extremeRightAndBottomCoordinate.X - extremeLeftAndTopCoordinate.X) * scaleValue + offsetSize;
                    widthCropControl = (extremeRightAndBottomCoordinate.Y - extremeLeftAndTopCoordinate.Y) * scaleValue + offsetSize;

                    double workingSpaceHeight = scaleValue * mobileDisplayWidth;
                    double workingSpaceWidth = scaleValue * mobileDisplayHeight;
                    double positionXRightTopCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetX;
                    double positionXLeftTopCornerWorkingSpace = positionXRightTopCornerWorkingSpace - workingSpaceWidth;

                    TranslateTransform moveCropControl = new TranslateTransform();
                    // (M11 == -1.0) means that working space is flipped horizontally. 
                    if ((PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform as TransformGroup).Value.M22 == -1.0)
                    {
                        moveCropControl.X = positionXLeftTopCornerWorkingSpace + (extremeLeftAndTopCoordinate.Y * scaleValue);
                    }
                    else
                    {
                        moveCropControl.X = positionXLeftTopCornerWorkingSpace + ((mobileDisplayHeight - extremeRightAndBottomCoordinate.Y) * scaleValue);
                    }

                    // (M22 == -1.0) means that working space is flipped vertically. 
                    if ((PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform as TransformGroup).Value.M11 == -1.0)
                    {
                        moveCropControl.Y = paintingAreaCheckeredGridTransformGroup.Value.OffsetY + ((mobileDisplayWidth - extremeRightAndBottomCoordinate.X) * scaleValue);
                    }
                    else
                    {
                        moveCropControl.Y = paintingAreaCheckeredGridTransformGroup.Value.OffsetY + (extremeLeftAndTopCoordinate.X * scaleValue);
                    }

                    _transformGridMain.Children.Add(moveCropControl);

                }
                else
                {
                    heightCropControl = scaleValue * mobileDisplayWidth + offsetSize;
                    widthCropControl = scaleValue * mobileDisplayHeight + offsetSize;
                    double workingSpaceHeight = scaleValue * mobileDisplayWidth;
                    double workingSpaceWidth = scaleValue * mobileDisplayHeight;
                    double positionXRightTopCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetX;                   
                    double positionXLeftTopCornerWorkingSpace = positionXRightTopCornerWorkingSpace - workingSpaceWidth;

                    TranslateTransform moveCropControl = new TranslateTransform();
                    moveCropControl.X = positionXLeftTopCornerWorkingSpace;
                    moveCropControl.Y = paintingAreaCheckeredGridTransformGroup.Value.OffsetY;
                    _transformGridMain.Children.Add(moveCropControl);
                }
                //limitLeft = paintingAreaCheckeredGridTransformGroup.Value.OffsetX - (mobileDisplayHeight * scaleValue) - offsetMargin;
                limitTop = paintingAreaCheckeredGridTransformGroup.Value.OffsetY - offsetMargin;
                limitBottom = limitTop + (mobileDisplayWidth * scaleValue) + offsetMargin * 2;
                limitRight = paintingAreaCheckeredGridTransformGroup.Value.OffsetX + offsetMargin;
                limitLeft = limitRight - (mobileDisplayHeight * scaleValue) - offsetMargin * 2;
            }
            else if(paintingAreaCheckeredGridTransformGroup.Value.M12 < 0.0)
            {
                scaleValue = paintingAreaCheckeredGridTransformGroup.Value.M21;

                // Attention: Working space is rotated 270°
                Point extremeLeftAndTopCoordinate = new Point(mobileDisplayWidth - 1.0, mobileDisplayHeight - 1.0);
                Point extremeRightAndBottomCoordinate = new Point(0.0, 0.0);

                if (PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count != 0)
                {
                    bool foundLeftPixel = false;
                    int xCoordinateOfExtremeTop = 0;
                    extremeLeftAndTopCoordinate = getExtremeLeftAndTopCoordinate(extremeLeftAndTopCoordinate.X, extremeLeftAndTopCoordinate.Y,
                                                                                 ref foundLeftPixel, ref xCoordinateOfExtremeTop);
                    extremeRightAndBottomCoordinate = getExtremeRightAndBottomCoordinate(extremeRightAndBottomCoordinate.X, extremeRightAndBottomCoordinate.Y,
                                                                                         extremeLeftAndTopCoordinate, foundLeftPixel, xCoordinateOfExtremeTop);

                    heightCropControl = (extremeRightAndBottomCoordinate.X - extremeLeftAndTopCoordinate.X) * scaleValue + offsetSize;
                    widthCropControl = (extremeRightAndBottomCoordinate.Y - extremeLeftAndTopCoordinate.Y) * scaleValue + offsetSize;
                    double workingSpaceHeight = scaleValue * mobileDisplayWidth;
                    double workingSpaceWidth = scaleValue * mobileDisplayHeight;
                    double positionYLeftBottomCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetY;
                    double positionYLeftTopCornerWorkingSpace = positionYLeftBottomCornerWorkingSpace - workingSpaceHeight;

                    TranslateTransform moveCropControl = new TranslateTransform();
                    // (M11 == -1.0) means that working space is flipped horizontally. 
                    if ((PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform as TransformGroup).Value.M22 == -1.0)
                    {
                        moveCropControl.X = paintingAreaCheckeredGridTransformGroup.Value.OffsetX + ((mobileDisplayHeight - extremeRightAndBottomCoordinate.Y) * scaleValue);
                    }
                    else
                    {
                        moveCropControl.X = paintingAreaCheckeredGridTransformGroup.Value.OffsetX + (extremeLeftAndTopCoordinate.Y * scaleValue);
                    }

                    // (M22 == -1.0) means that working space is flipped vertically. 
                    if ((PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform as TransformGroup).Value.M11 == -1.0)
                    {
                        moveCropControl.Y = positionYLeftTopCornerWorkingSpace + (extremeLeftAndTopCoordinate.X * scaleValue);
                    }
                    else
                    {
                        moveCropControl.Y = positionYLeftTopCornerWorkingSpace + ((mobileDisplayWidth - extremeRightAndBottomCoordinate.X) * scaleValue);
                    }

                    _transformGridMain.Children.Add(moveCropControl);
                }
                else
                {
                    heightCropControl = paintingAreaCheckeredGridTransformGroup.Value.M21 * mobileDisplayWidth + offsetSize;
                    widthCropControl = paintingAreaCheckeredGridTransformGroup.Value.M21 * mobileDisplayHeight + offsetSize;
                    double workingSpaceHeight = paintingAreaCheckeredGridTransformGroup.Value.M21 * mobileDisplayWidth;
                    double workingSpaceWidth = paintingAreaCheckeredGridTransformGroup.Value.M21 * mobileDisplayHeight;
                    double positionYLeftBottomCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetY;
                    double positionYLeftTopCornerWorkingSpace = positionYLeftBottomCornerWorkingSpace - workingSpaceHeight;

                    TranslateTransform moveCropControl = new TranslateTransform();
                    moveCropControl.X = paintingAreaCheckeredGridTransformGroup.Value.OffsetX;
                    moveCropControl.Y = positionYLeftTopCornerWorkingSpace;
                    _transformGridMain.Children.Add(moveCropControl);
                }

                limitBottom = paintingAreaCheckeredGridTransformGroup.Value.OffsetY + offsetMargin;
                limitTop = limitBottom - (mobileDisplayWidth * scaleValue) - offsetMargin * 2;
                limitLeft = paintingAreaCheckeredGridTransformGroup.Value.OffsetX - offsetMargin;
                limitRight = limitLeft + (mobileDisplayHeight * scaleValue) + offsetMargin * 2;
            }
            PocketPaintApplication.GetInstance().ProgressRing.IsActive = false;
            setMainGridSize(heightCropControl, widthCropControl);
            setRectangleForMovementSize(heightCropControl, widthCropControl);

            GridMain.Visibility = Visibility.Visible;
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

                double sizeValueToAdd = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY + rectRectangleForMovement.Height + moveY.Y) > limitBottom ? 0.0 : moveY.Y;
                changeHeightOfUiElements(sizeValueToAdd);
                changeMarginBottomOfUiElements(sizeValueToAdd);
            }
        }

        private void rectCenterTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Height + (e.Delta.Translation.Y * -1)) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveY = createTranslateTransform(0.0, e.Delta.Translation.Y);
                moveY.Y *= -1.0;
                double sizeValueToAdd = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY - moveY.Y) < limitTop ? 0.0 : moveY.Y;
                changeHeightOfUiElements(sizeValueToAdd);
                changeMarginTopOfUiElements(sizeValueToAdd);
            }
        }

        private void rectLeftBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Width + (e.Delta.Translation.X * -1)) >= MIN_RECTANGLE_MOVE_WIDTH &&
               (rectRectangleForMovement.Height + e.Delta.Translation.Y) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveX = createTranslateTransform((e.Delta.Translation.X *-1.0), 0.0);
                var moveY = createTranslateTransform(0.0, (e.Delta.Translation.Y));

                // left
                double sizeValueToAddLeft = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX - moveX.X) < limitLeft ? 0.0 : moveX.X;
                changeWidthOfUiElements(sizeValueToAddLeft);
                changeMarginLeftOfUiElements(sizeValueToAddLeft);

                // bottom
                double sizeValueToAddBottom = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY + rectRectangleForMovement.Height + moveY.Y) > limitBottom ? 0.0 : moveY.Y;
                changeHeightOfUiElements(sizeValueToAddBottom);
                changeMarginBottomOfUiElements(sizeValueToAddBottom);
            }
        }

        private void rectLeftCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Width + (e.Delta.Translation.X * -1)) >= MIN_RECTANGLE_MOVE_WIDTH)
            {
                var moveX = createTranslateTransform((e.Delta.Translation.X), 0.0);
                moveX.X *= -1.0;
                double sizeValueToAdd = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX - moveX.X) < limitLeft ? 0.0 : moveX.X;
                changeWidthOfUiElements(sizeValueToAdd); 
                changeMarginLeftOfUiElements(sizeValueToAdd);
            }
        }
        
        private void rectLeftTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Width + (e.Delta.Translation.X * -1)) >= MIN_RECTANGLE_MOVE_WIDTH &&
                (rectRectangleForMovement.Height + (e.Delta.Translation.Y * -1)) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                // left
                var moveX = createTranslateTransform((e.Delta.Translation.X), 0.0);
                moveX.X *= -1.0;
                double sizeValueToAddLeft = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX - moveX.X) < limitLeft ? 0.0 : moveX.X;
                changeWidthOfUiElements(sizeValueToAddLeft);
                changeMarginLeftOfUiElements(sizeValueToAddLeft);

                // top
                var moveY = createTranslateTransform(0.0, (e.Delta.Translation.Y));
                moveY.Y *= -1;
                double sizeValueToAddTop = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY - moveY.Y) < limitTop ? 0.0 : moveY.Y;
                changeHeightOfUiElements(sizeValueToAddTop);
                changeMarginTopOfUiElements(sizeValueToAddTop);
            }
        }

        private void rectRightBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Width + e.Delta.Translation.X) >= MIN_RECTANGLE_MOVE_WIDTH &&
                (rectRectangleForMovement.Height + e.Delta.Translation.Y) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveX = createTranslateTransform((e.Delta.Translation.X), 0.0);
                var moveY = createTranslateTransform(0.0, (e.Delta.Translation.Y));

                // right
                double sizeValueToAddRight = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX + rectRectangleForMovement.Width + moveX.X) > limitRight ? 0.0 : moveX.X;
                changeWidthOfUiElements(sizeValueToAddRight);
                changeMarginRightOfUiElements(sizeValueToAddRight);

                // bottom
                double sizeValueToAddBottom = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY + rectRectangleForMovement.Height + moveY.Y) > limitBottom ? 0.0 : moveY.Y;
                changeHeightOfUiElements(sizeValueToAddBottom);
                changeMarginBottomOfUiElements(sizeValueToAddBottom);
            }

        }

        private void rectRightCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Width + e.Delta.Translation.X) >= MIN_RECTANGLE_MOVE_WIDTH)
            {
                var moveX = createTranslateTransform((e.Delta.Translation.X), 0.0);

                double sizeValueToAdd = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX + rectRectangleForMovement.Width + moveX.X) > limitRight ? 0.0 : moveX.X;
                changeWidthOfUiElements(sizeValueToAdd);
                changeMarginRightOfUiElements(sizeValueToAdd);
            }
        }

        private void rectRightTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleForMovement.Width + e.Delta.Translation.X) >= MIN_RECTANGLE_MOVE_WIDTH &&
               (rectRectangleForMovement.Height + (e.Delta.Translation.Y * -1)) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveX = createTranslateTransform((e.Delta.Translation.X), 0.0);
                var moveY = createTranslateTransform(0.0, (e.Delta.Translation.Y));

                // right
                double sizeValueToAddRight = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX + rectRectangleForMovement.Width + moveX.X) > limitRight ? 0.0 : moveX.X;
                changeWidthOfUiElements(sizeValueToAddRight);
                changeMarginRightOfUiElements(sizeValueToAddRight);

                // top
                moveY.Y *= -1.0;
                double sizeValueToAddTop = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY - moveY.Y) < limitTop ? 0.0 : moveY.Y;
                changeHeightOfUiElements(sizeValueToAddTop);
                changeMarginTopOfUiElements(sizeValueToAddTop);
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


                //((TranslateTransform)move).X = e.Delta.Translation.X;
                ((TranslateTransform)move).X = e.Delta.Translation.X;
                ((TranslateTransform)move).Y = e.Delta.Translation.Y;
                if(move.X < 0)
                {
                    move.X = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX + move.X ) < limitLeft ? 0.0 : move.X;
                }
                else
                {
                    move.X = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX + rectRectangleForMovement.Width + move.X) > limitRight ? 0.0 : move.X;
                }

                if (move.Y < 0)
                {
                    move.Y = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY + move.Y) < limitTop ? 0.0 : move.Y;
                }
                else
                {
                    move.Y = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY + rectRectangleForMovement.Height + move.Y) > limitBottom ? 0.0 : move.Y;
                }
                
                

                
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
