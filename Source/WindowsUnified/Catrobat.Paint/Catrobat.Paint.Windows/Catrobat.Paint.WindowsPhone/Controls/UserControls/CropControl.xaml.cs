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

        const double MAX_VERTICAL_CORNER_RECTANGLE_HEIGHT = 30.0;
        const double MAX_HORIZONTAL_CORNER_RECTANGLE_WIDTH = 30.0;
        const double MAX_HORIZONTAL_CENTER_RECTANGLE_WIDTH = 120.0;
        const double MAX_VERTICAL_CENTER_RECTANGLE_HEIGHT = 120.0;
        const double MAX_GRID_HEIGHT = 140.0;
        const double MIN_CORNER_RECTANGLE_HEIGHT = 5.0;
        const double MIN_HORIZONTAL_CENTER_RECTANGLE_WIDTH = 120.0;
        const double MIN_HORIZONTAL_CORNER_RECTANGLE_WIDTH = 5.0;
        const double MIN_VERTICAL_CENTER_RECTANGLE_HEIGHT = 5.0;
        const double MIN_RECTANGLE_MOVE_HEIGHT = 60.0;
        const double MIN_RECTANGLE_MOVE_WIDTH = 60.0;
        const double MULTIPLICATION_FACTOR_GRID_SIZE = 0.3648;
        const double MULTiPLICATION_FACTOR_RECTANGLE_SIZE = 0.3125;
        bool _isModifiedRectangleMovement;
        double mobileDisplayHeight = 0.0;
        double mobileDisplayWidth = 0.0;

        double limitLeft = 0.0;
        double limitRight = 0.0;
        double limitBottom = 0.0;
        double limitTop = 0.0;

        double offsetMargin;
        double heightCropControl;
        double widthCropControl;

        double scaleValueWorkingSpace = 0.0;

        PixelData.PixelData pixelData = new PixelData.PixelData();

        Point leftTopNullPointCropSelection;

        public CropControl()
        {
            this.InitializeComponent();
            mobileDisplayHeight = Window.Current.Bounds.Height;
            mobileDisplayWidth = Window.Current.Bounds.Width;
            _transformGridMain = new TransformGroup();
            GridMain.RenderTransform = _transformGridMain;
            PocketPaintApplication.GetInstance().CropControl = this;
            setIsModifiedRectangleMovement = false;
            leftTopNullPointCropSelection = new Point(0.0, 0.0);

            offsetMargin = 5.0;
            heightCropControl = 0.0;
            widthCropControl = 0.0;
            scaleValueWorkingSpace = 0.0;
        }

        public void setControlSize(double height, double width)
        {
            GridMain.Height = height;
            GridMain.Width = width;

            // Grid-Height:
            double calculatedGridHeight = (height * 0.3648);
            // TODO: GridRectLeftBottom.Height = (height * 0.1302) > 50.0 ? 50.0 : (height * 0.1302);
            GridRectLeftCenter.Height = calculatedGridHeight > MAX_GRID_HEIGHT ? MAX_GRID_HEIGHT : calculatedGridHeight;
            // TODO: GridRectLeftTop.Height = (height * 0.1302) > 50.0 ? 50.0 : (height * 0.1302);
            // TODO: GridRectRightBottom.Height = (height * 0.1302) > 50.0 ? 50.0 : (height * 0.1302);
            // TODO: GridRectRightTop.Height = (height * 0.1302) > 50.0 ? 50.0 : (height * 0.1302);
            GridRectRightCenter.Height = calculatedGridHeight > MAX_GRID_HEIGHT ? MAX_GRID_HEIGHT : calculatedGridHeight;

            // Grid-Width
            GridRectCenterBottom.Width = width * 0.3648;
            GridRectCenterTop.Width = width * 0.3648;

            // Rectangle-Height
            // 0.3125
            double calculatedCenterRectangleHeight = (height * 0.2);
            double calculatedCornerRectangleHeight = (height * 0.0781);

            setHeightOfVerticalCornerRectangles(calculatedCornerRectangleHeight);
            setHeightOfVerticalCenterRectangles(calculatedCenterRectangleHeight);

            // Rectangle-Width
            double calcualtedHorizontalCenterRectangleWidth = (width * 0.2);
            double calcualtedCornerRectangleWidth = (Width * 0.0781);

            setWidthOfHorizontalCenterRectangles(calcualtedHorizontalCenterRectangleWidth);
            setWidthOfHorizontalCornerRectangles(calcualtedCornerRectangleWidth);
        }

        public void setHeightOfVerticalCornerRectangles(double newValue)
        {
            if (newValue > MAX_VERTICAL_CORNER_RECTANGLE_HEIGHT)
            {
                rectLeftBottomVert.Height = MAX_VERTICAL_CORNER_RECTANGLE_HEIGHT;
                rectLeftTopVert.Height = MAX_VERTICAL_CORNER_RECTANGLE_HEIGHT;
                rectRightBottomVert.Height = MAX_VERTICAL_CORNER_RECTANGLE_HEIGHT;
                rectRightTopVert.Height = MAX_VERTICAL_CORNER_RECTANGLE_HEIGHT;
            }
            else if (newValue < MIN_CORNER_RECTANGLE_HEIGHT)
            {
                rectLeftBottomVert.Height = MIN_CORNER_RECTANGLE_HEIGHT;
                rectLeftTopVert.Height = MIN_CORNER_RECTANGLE_HEIGHT;
                rectRightBottomVert.Height = MIN_CORNER_RECTANGLE_HEIGHT;
                rectRightTopVert.Height = MIN_CORNER_RECTANGLE_HEIGHT;
            }
            else
            {
                rectLeftBottomVert.Height = newValue;
                rectLeftTopVert.Height = newValue;
                rectRightBottomVert.Height = newValue;
                rectRightTopVert.Height = newValue;
            }
        }

        private void setHeightOfVerticalCenterRectangles(double newValue)
        {
            if (newValue > MAX_VERTICAL_CENTER_RECTANGLE_HEIGHT)
            {
                rectLeftCenter.Height = MAX_VERTICAL_CENTER_RECTANGLE_HEIGHT;
                rectRightCenter.Height = MAX_VERTICAL_CENTER_RECTANGLE_HEIGHT;
            }
            else if (newValue < MIN_VERTICAL_CENTER_RECTANGLE_HEIGHT)
            {
                rectLeftCenter.Height = MIN_VERTICAL_CENTER_RECTANGLE_HEIGHT;
                rectRightCenter.Height = MIN_VERTICAL_CENTER_RECTANGLE_HEIGHT;
            }
            else
            {
                rectLeftCenter.Height = newValue;
                rectRightCenter.Height = newValue;
            }
        }

        private void setWidthOfHorizontalCenterRectangles(double newValue)
        {
            if(newValue > MAX_HORIZONTAL_CENTER_RECTANGLE_WIDTH)
            {
                rectCenterTop.Width = MAX_HORIZONTAL_CENTER_RECTANGLE_WIDTH;
                rectCenterBottom.Width = MAX_HORIZONTAL_CENTER_RECTANGLE_WIDTH;
            }
            else if (newValue < MIN_HORIZONTAL_CENTER_RECTANGLE_WIDTH)
            {
                rectCenterTop.Width = MIN_HORIZONTAL_CENTER_RECTANGLE_WIDTH;
                rectCenterBottom.Width = MIN_HORIZONTAL_CENTER_RECTANGLE_WIDTH;
            }
            else
            {
                rectCenterTop.Width = newValue;
                rectCenterBottom.Width = newValue;
            }
        }

        public void setWidthOfHorizontalCornerRectangles(double newValue)
        {
            if (newValue > MAX_HORIZONTAL_CORNER_RECTANGLE_WIDTH)
            {
                rectLeftBottomHorz.Width = MAX_HORIZONTAL_CENTER_RECTANGLE_WIDTH;
                rectLeftTopHorz.Width = MAX_HORIZONTAL_CENTER_RECTANGLE_WIDTH;
                rectRightBottomHorz.Width = MAX_HORIZONTAL_CENTER_RECTANGLE_WIDTH;
                rectRightTopHorz.Width = MAX_HORIZONTAL_CENTER_RECTANGLE_WIDTH;
            }
            else if(newValue < MIN_HORIZONTAL_CORNER_RECTANGLE_WIDTH)
            {
                rectLeftBottomHorz.Width = MIN_HORIZONTAL_CENTER_RECTANGLE_WIDTH;
                rectLeftTopHorz.Width = MIN_HORIZONTAL_CENTER_RECTANGLE_WIDTH;
                rectRightBottomHorz.Width = MIN_HORIZONTAL_CENTER_RECTANGLE_WIDTH;
                rectRightTopHorz.Width = MIN_HORIZONTAL_CENTER_RECTANGLE_WIDTH;
            }
            else
            {
                rectLeftBottomHorz.Width = newValue;
                rectLeftTopHorz.Width = newValue;
                rectRightBottomHorz.Width = newValue;
                rectRightTopHorz.Width = newValue;
            }
        }

        public void setRectangleForMovementSize(double height, double width)
        {
            rectRectangleCropSelection.Height = height;
            rectRectangleCropSelection.Width = width;
        }

        private Point getExtremeLeftAndTopCoordinate(double initLeft, double initTop, 
                                                     ref bool foundLeftPixel, ref int xCoordinateOfExtremeTop)
        {
            Point extremePoint = new Point(initLeft, initTop);
            foundLeftPixel = false;

            double paintingAreaCanvasHeight = PocketPaintApplication.GetInstance().PaintingAreaCanvas.Height;
            double paintingAreaCanvasWidth = PocketPaintApplication.GetInstance().PaintingAreaCanvas.Width;

            // left pixel
            for (int indexWidth = 0; indexWidth < (int)paintingAreaCanvasWidth; indexWidth++)
                for (int indexHeight = 0; indexHeight < (int)paintingAreaCanvasHeight; indexHeight++)
                {
                    if (pixelData.getPixelAlphaFromCanvas(indexWidth, indexHeight) != 0x00)
                    {
                        extremePoint.X = indexWidth;
                        foundLeftPixel = true;

                        // found extreme point --> set break conditions
                        indexWidth = (int)paintingAreaCanvasWidth;
                        indexHeight = (int)paintingAreaCanvasHeight;
                    }
            }
            // top pixel
            if (foundLeftPixel == true)
                for (int indexHeight = 0; indexHeight < (int)paintingAreaCanvasHeight; indexHeight++)
                    for (int indexWidth = (int)paintingAreaCanvasWidth - 1; indexWidth >= (int)extremePoint.X; indexWidth--)
                    {
                        if (pixelData.getPixelAlphaFromCanvas(indexWidth, indexHeight) != 0x00)
                        {
                            extremePoint.Y = indexHeight;
                            xCoordinateOfExtremeTop = indexWidth;

                            // found extreme point --> set break conditions
                            indexHeight = (int)paintingAreaCanvasHeight;
                            indexWidth = 0;
                        }
                    }
            return extremePoint;
        }

        private Point getExtremeRightAndBottomCoordinate(double initRight, double initBottom, 
                                                         Point extremeLeftAndTopCoordinate, bool foundLeftPixel,
                                                         int xCoordinateOfExtremeTop)
        {
            double paintingAreaCanvasHeight = PocketPaintApplication.GetInstance().PaintingAreaCanvas.Height;
            double paintingAreaCanvasWidth = PocketPaintApplication.GetInstance().PaintingAreaCanvas.Width;

            Point extremePoint = new Point(initRight, initBottom);

            if (foundLeftPixel == true)
            {
                // right pixel
                int yCoordinateOfExtremeRight = 0;
                for (int indexWidth = (int)paintingAreaCanvasWidth - 1; indexWidth >= xCoordinateOfExtremeTop; indexWidth--)
                    for (int indexHeight = (int)paintingAreaCanvasHeight - 1; indexHeight >= extremeLeftAndTopCoordinate.Y; indexHeight--)
                    {
                        if (pixelData.getPixelAlphaFromCanvas(indexWidth, indexHeight) != 0x00)
                        {
                            extremePoint.X = indexWidth;
                            yCoordinateOfExtremeRight = indexHeight;

                            // found extreme point --> set break conditions
                            indexWidth = 0;
                            indexHeight = 0;
                        }
                }
                // bottom pixel
                for (int indexHeight = (int)paintingAreaCanvasHeight - 1; indexHeight >= yCoordinateOfExtremeRight; indexHeight--)
                    for (int indexWidth = (int)extremePoint.X; indexWidth >= (int)extremeLeftAndTopCoordinate.X; indexWidth--)
                    {
                        if (pixelData.getPixelAlphaFromCanvas(indexWidth, indexHeight) != 0x00)
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

        private void _calculateAndSetCropControlPositionWithoutRotating(double doubleBorderWidthValue, double scaleValueWorkingSpace, bool isWorkingSpaceFlippedHorizontally, bool isWorkingSpaceFlippedVertically)
        {
            PocketPaintApplication currentPaintApplication = PocketPaintApplication.GetInstance();
            if (currentPaintApplication == null)
            {
                return;
            }
            TranslateTransform moveCropControl = new TranslateTransform();
            TransformGroup transform = (currentPaintApplication.PaintingAreaCanvas.RenderTransform as TransformGroup);
            TransformGroup paintingAreaCheckeredGridTransformGroup = currentPaintApplication.PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            double paintingAreaCheckeredGridHeight = currentPaintApplication.PaintingAreaCheckeredGrid.Height;
            double paintingAreaCheckeredGridWidth = currentPaintApplication.PaintingAreaCheckeredGrid.Width;
            // Calculate the position from crop selection in connection with the working space respectively with the drawing
            // in the working space. In other words the crop selection should be adapted on the drawing in the working space.
            Point extremeLeftAndTopCoordinate = new Point(0.0, 0.0);
            Point extremeRightAndBottomCoordinate = new Point(paintingAreaCheckeredGridWidth - 1.0, paintingAreaCheckeredGridHeight - 1.0);

            if (currentPaintApplication.PaintingAreaCanvas.Children.Count != 0)
            {
                bool foundLeftPixel = false;
                int xCoordinateOfExtremeTop = 0;
                extremeLeftAndTopCoordinate = getExtremeLeftAndTopCoordinate(extremeLeftAndTopCoordinate.X, extremeLeftAndTopCoordinate.Y,
                                                                             ref foundLeftPixel, ref xCoordinateOfExtremeTop);
                extremeRightAndBottomCoordinate = getExtremeRightAndBottomCoordinate(extremeRightAndBottomCoordinate.X, extremeRightAndBottomCoordinate.Y,
                                                                                     extremeLeftAndTopCoordinate, foundLeftPixel, xCoordinateOfExtremeTop);

                heightCropControl = (extremeRightAndBottomCoordinate.Y - extremeLeftAndTopCoordinate.Y) * scaleValueWorkingSpace + doubleBorderWidthValue;
                widthCropControl = (extremeRightAndBottomCoordinate.X - extremeLeftAndTopCoordinate.X) * scaleValueWorkingSpace + doubleBorderWidthValue;

                if (isWorkingSpaceFlippedHorizontally)
                {
                    moveCropControl.X = paintingAreaCheckeredGridTransformGroup.Value.OffsetX + ((paintingAreaCheckeredGridWidth - extremeRightAndBottomCoordinate.X) * scaleValueWorkingSpace);
                }
                else
                {
                    moveCropControl.X = paintingAreaCheckeredGridTransformGroup.Value.OffsetX + (extremeLeftAndTopCoordinate.X * scaleValueWorkingSpace);
                }

                if (isWorkingSpaceFlippedVertically)
                {
                    moveCropControl.Y = paintingAreaCheckeredGridTransformGroup.Value.OffsetY + ((paintingAreaCheckeredGridHeight - extremeRightAndBottomCoordinate.Y) * scaleValueWorkingSpace);
                }
                else
                {
                    moveCropControl.Y = paintingAreaCheckeredGridTransformGroup.Value.OffsetY + (extremeLeftAndTopCoordinate.Y * scaleValueWorkingSpace);
                }
            }
            else
            {
                heightCropControl = scaleValueWorkingSpace * paintingAreaCheckeredGridHeight + doubleBorderWidthValue;
                widthCropControl = scaleValueWorkingSpace * paintingAreaCheckeredGridWidth + doubleBorderWidthValue;

                moveCropControl.X = paintingAreaCheckeredGridTransformGroup.Value.OffsetX;
                moveCropControl.Y = paintingAreaCheckeredGridTransformGroup.Value.OffsetY;
            }
            setLimitsForMovableControlBorder(0);
            setLeftTopNullPointCropSelection(paintingAreaCheckeredGridTransformGroup.Value.OffsetX,
                                         paintingAreaCheckeredGridTransformGroup.Value.OffsetY);
            setCropControlPosition(heightCropControl, widthCropControl, moveCropControl);
        }

        private void _calculateAndSetCropControlPositionWith90DegreeRotation(double doubleBorderWidthValue, double scaleValueWorkingSpace, bool isWorkingSpaceFlippedHorizontally, bool isWorkingSpaceFlippedVertically)
        {
            PocketPaintApplication currentPaintApplication = PocketPaintApplication.GetInstance();
            if (currentPaintApplication == null)
            {
                return;
            }
            TranslateTransform moveCropControl = new TranslateTransform();
            TransformGroup transform = (currentPaintApplication.PaintingAreaCanvas.RenderTransform as TransformGroup);
            TransformGroup paintingAreaCheckeredGridTransformGroup = currentPaintApplication.PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            double paintingAreaCheckeredGridHeight = currentPaintApplication.PaintingAreaCheckeredGrid.Height;
            double paintingAreaCheckeredGridWidth = currentPaintApplication.PaintingAreaCheckeredGrid.Width;
            Point extremeLeftAndTopCoordinate = new Point(paintingAreaCheckeredGridWidth - 1.0, paintingAreaCheckeredGridHeight - 1.0);
            Point extremeRightAndBottomCoordinate = new Point(0.0, 0.0);

            if (currentPaintApplication.PaintingAreaCanvas.Children.Count != 0)
            {
                bool foundLeftPixel = false;
                int xCoordinateOfExtremeTop = 0;
                extremeLeftAndTopCoordinate = getExtremeLeftAndTopCoordinate(extremeLeftAndTopCoordinate.X, extremeLeftAndTopCoordinate.Y,
                                                                             ref foundLeftPixel, ref xCoordinateOfExtremeTop);
                extremeRightAndBottomCoordinate = getExtremeRightAndBottomCoordinate(extremeRightAndBottomCoordinate.X, extremeRightAndBottomCoordinate.Y,
                                                                                     extremeLeftAndTopCoordinate, foundLeftPixel, xCoordinateOfExtremeTop);

                heightCropControl = (extremeRightAndBottomCoordinate.X - extremeLeftAndTopCoordinate.X) * scaleValueWorkingSpace + doubleBorderWidthValue;
                widthCropControl = (extremeRightAndBottomCoordinate.Y - extremeLeftAndTopCoordinate.Y) * scaleValueWorkingSpace + doubleBorderWidthValue;

                double workingSpaceHeight = scaleValueWorkingSpace * paintingAreaCheckeredGridWidth;
                double workingSpaceWidth = scaleValueWorkingSpace * paintingAreaCheckeredGridHeight;
                double positionXRightTopCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetX;
                double positionXLeftTopCornerWorkingSpace = positionXRightTopCornerWorkingSpace - workingSpaceWidth;

                if (isWorkingSpaceFlippedHorizontally)
                {
                    moveCropControl.X = positionXLeftTopCornerWorkingSpace + (extremeLeftAndTopCoordinate.Y * scaleValueWorkingSpace);
                }
                else
                {
                    moveCropControl.X = positionXLeftTopCornerWorkingSpace + ((paintingAreaCheckeredGridHeight - extremeRightAndBottomCoordinate.Y) * scaleValueWorkingSpace);
                }

                if (isWorkingSpaceFlippedVertically)
                {
                    moveCropControl.Y = paintingAreaCheckeredGridTransformGroup.Value.OffsetY + ((paintingAreaCheckeredGridWidth - extremeRightAndBottomCoordinate.X) * scaleValueWorkingSpace);
                }
                else
                {
                    moveCropControl.Y = paintingAreaCheckeredGridTransformGroup.Value.OffsetY + (extremeLeftAndTopCoordinate.X * scaleValueWorkingSpace);
                }
            }
            else
            {
                heightCropControl = scaleValueWorkingSpace * paintingAreaCheckeredGridWidth + doubleBorderWidthValue;
                widthCropControl = scaleValueWorkingSpace * paintingAreaCheckeredGridHeight + doubleBorderWidthValue;
                double workingSpaceHeight = scaleValueWorkingSpace * paintingAreaCheckeredGridWidth;
                double workingSpaceWidth = scaleValueWorkingSpace * paintingAreaCheckeredGridHeight;
                double positionXRightTopCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetX;
                double positionXLeftTopCornerWorkingSpace = positionXRightTopCornerWorkingSpace - workingSpaceWidth;

                moveCropControl.X = positionXLeftTopCornerWorkingSpace;
                moveCropControl.Y = paintingAreaCheckeredGridTransformGroup.Value.OffsetY;
            }
            setLimitsForMovableControlBorder(90);
            setCropControlPosition(heightCropControl, widthCropControl, moveCropControl);
        }

        private void _calculateAndSetCropControlPositionWith180DegreeRotation(double doubleBorderWidthValue, double scaleValueWorkingSpace, bool isWorkingSpaceFlippedHorizontally, bool isWorkingSpaceFlippedVertically)
        {
            PocketPaintApplication currentPaintApplication = PocketPaintApplication.GetInstance();
            if (currentPaintApplication == null)
            {
                return;
            }
            TranslateTransform moveCropControl = new TranslateTransform();
            TransformGroup transform = (currentPaintApplication.PaintingAreaCanvas.RenderTransform as TransformGroup);
            TransformGroup paintingAreaCheckeredGridTransformGroup = currentPaintApplication.PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            double paintingAreaCheckeredGridHeight = currentPaintApplication.PaintingAreaCheckeredGrid.Height;
            double paintingAreaCheckeredGridWidth = currentPaintApplication.PaintingAreaCheckeredGrid.Width;

            Point extremeLeftAndTopCoordinate = new Point(paintingAreaCheckeredGridWidth - 1.0, 0.0);
            Point extremeRightAndBottomCoordinate = new Point(0.0, paintingAreaCheckeredGridHeight - 1.0);

            if (currentPaintApplication.PaintingAreaCanvas.Children.Count != 0)
            {
                bool foundLeftPixel = false;
                int xCoordinateOfExtremeTop = 0;
                extremeLeftAndTopCoordinate = getExtremeLeftAndTopCoordinate(extremeLeftAndTopCoordinate.X, extremeLeftAndTopCoordinate.Y,
                                                                             ref foundLeftPixel, ref xCoordinateOfExtremeTop);
                extremeRightAndBottomCoordinate = getExtremeRightAndBottomCoordinate(extremeRightAndBottomCoordinate.X, extremeRightAndBottomCoordinate.Y,
                                                                                     extremeLeftAndTopCoordinate, foundLeftPixel, xCoordinateOfExtremeTop);

                heightCropControl = (extremeRightAndBottomCoordinate.Y - extremeLeftAndTopCoordinate.Y) * scaleValueWorkingSpace + doubleBorderWidthValue;
                widthCropControl = (extremeRightAndBottomCoordinate.X - extremeLeftAndTopCoordinate.X) * scaleValueWorkingSpace + doubleBorderWidthValue;

                double workingSpaceHeight = scaleValueWorkingSpace * paintingAreaCheckeredGridHeight;
                double workingSpaceWidth = scaleValueWorkingSpace * paintingAreaCheckeredGridWidth;
                double positionXRightBottomCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetX;
                double positionXLeftBottomCornerWorkingSpace = positionXRightBottomCornerWorkingSpace - workingSpaceWidth;
                double positionYRigthBottomCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetY;
                double positionYRightTopCornerWorkingSpace = positionYRigthBottomCornerWorkingSpace - workingSpaceHeight;

                if (isWorkingSpaceFlippedHorizontally)
                {
                    moveCropControl.X = positionXLeftBottomCornerWorkingSpace + (extremeLeftAndTopCoordinate.X * scaleValueWorkingSpace);
                }
                else
                {
                    moveCropControl.X = positionXLeftBottomCornerWorkingSpace + ((paintingAreaCheckeredGridWidth - extremeRightAndBottomCoordinate.X) * scaleValueWorkingSpace);
                }

                if (isWorkingSpaceFlippedVertically)
                {
                    moveCropControl.Y = positionYRightTopCornerWorkingSpace + (extremeLeftAndTopCoordinate.Y * scaleValueWorkingSpace);
                }
                else
                {
                    moveCropControl.Y = positionYRightTopCornerWorkingSpace + ((paintingAreaCheckeredGridHeight - extremeRightAndBottomCoordinate.Y) * scaleValueWorkingSpace);
                }
            }
            else
            {
                heightCropControl = scaleValueWorkingSpace * paintingAreaCheckeredGridHeight + doubleBorderWidthValue;
                widthCropControl = scaleValueWorkingSpace * paintingAreaCheckeredGridWidth + doubleBorderWidthValue;
                double workingSpaceHeight = scaleValueWorkingSpace * paintingAreaCheckeredGridHeight;
                double workingSpaceWidth = scaleValueWorkingSpace * paintingAreaCheckeredGridWidth;
                double positionXRightBottomCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetX;
                double positionXLeftBottomCornerWorkingSpace = positionXRightBottomCornerWorkingSpace - workingSpaceWidth;
                double positionYRigthBottomCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetY;
                double positionYRightTopCornerWorkingSpace = positionYRigthBottomCornerWorkingSpace - workingSpaceHeight;
                moveCropControl.X = positionXLeftBottomCornerWorkingSpace;
                moveCropControl.Y = positionYRightTopCornerWorkingSpace;
            }
            setLimitsForMovableControlBorder(180);
            setCropControlPosition(heightCropControl, widthCropControl, moveCropControl);
        }

        private void _calculateAndSetCropControlPositionWith270DegreeRotation(double doubleBorderWidthValue, double scaleValueWorkingSpace, bool isWorkingSpaceFlippedHorizontally, bool isWorkingSpaceFlippedVertically)
        {
            PocketPaintApplication currentPaintApplication = PocketPaintApplication.GetInstance();
            if (currentPaintApplication == null)
            {
                return;
            }
            TranslateTransform moveCropControl = new TranslateTransform();
            TransformGroup transform = (currentPaintApplication.PaintingAreaCanvas.RenderTransform as TransformGroup);
            TransformGroup paintingAreaCheckeredGridTransformGroup = currentPaintApplication.PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            double paintingAreaCheckeredGridHeight = currentPaintApplication.PaintingAreaCheckeredGrid.Height;
            double paintingAreaCheckeredGridWidth = currentPaintApplication.PaintingAreaCheckeredGrid.Width;

            Point extremeLeftAndTopCoordinate = new Point(paintingAreaCheckeredGridWidth - 1.0, paintingAreaCheckeredGridHeight - 1.0);
            Point extremeRightAndBottomCoordinate = new Point(0.0, 0.0);

            if (currentPaintApplication.PaintingAreaCanvas.Children.Count != 0)
            {
                bool foundLeftPixel = false;
                int xCoordinateOfExtremeTop = 0;
                extremeLeftAndTopCoordinate = getExtremeLeftAndTopCoordinate(extremeLeftAndTopCoordinate.X, extremeLeftAndTopCoordinate.Y,
                                                                             ref foundLeftPixel, ref xCoordinateOfExtremeTop);
                extremeRightAndBottomCoordinate = getExtremeRightAndBottomCoordinate(extremeRightAndBottomCoordinate.X, extremeRightAndBottomCoordinate.Y,
                                                                                     extremeLeftAndTopCoordinate, foundLeftPixel, xCoordinateOfExtremeTop);

                heightCropControl = (extremeRightAndBottomCoordinate.X - extremeLeftAndTopCoordinate.X) * scaleValueWorkingSpace + doubleBorderWidthValue;
                widthCropControl = (extremeRightAndBottomCoordinate.Y - extremeLeftAndTopCoordinate.Y) * scaleValueWorkingSpace + doubleBorderWidthValue;
                double workingSpaceHeight = scaleValueWorkingSpace * paintingAreaCheckeredGridWidth;
                double workingSpaceWidth = scaleValueWorkingSpace * paintingAreaCheckeredGridHeight;
                double positionYLeftBottomCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetY;
                double positionYLeftTopCornerWorkingSpace = positionYLeftBottomCornerWorkingSpace - workingSpaceHeight;

                if (isWorkingSpaceFlippedHorizontally)
                {
                    moveCropControl.X = paintingAreaCheckeredGridTransformGroup.Value.OffsetX + ((paintingAreaCheckeredGridHeight - extremeRightAndBottomCoordinate.Y) * scaleValueWorkingSpace);
                }
                else
                {
                    moveCropControl.X = paintingAreaCheckeredGridTransformGroup.Value.OffsetX + (extremeLeftAndTopCoordinate.Y * scaleValueWorkingSpace);
                }

                if (isWorkingSpaceFlippedVertically)
                {
                    moveCropControl.Y = positionYLeftTopCornerWorkingSpace + (extremeLeftAndTopCoordinate.X * scaleValueWorkingSpace);
                }
                else
                {
                    moveCropControl.Y = positionYLeftTopCornerWorkingSpace + ((paintingAreaCheckeredGridWidth - extremeRightAndBottomCoordinate.X) * scaleValueWorkingSpace);
                }
            }
            else
            {
                heightCropControl = paintingAreaCheckeredGridTransformGroup.Value.M21 * paintingAreaCheckeredGridWidth + doubleBorderWidthValue;
                widthCropControl = paintingAreaCheckeredGridTransformGroup.Value.M21 * paintingAreaCheckeredGridHeight + doubleBorderWidthValue;
                double workingSpaceHeight = paintingAreaCheckeredGridTransformGroup.Value.M21 * paintingAreaCheckeredGridWidth;
                double workingSpaceWidth = paintingAreaCheckeredGridTransformGroup.Value.M21 * paintingAreaCheckeredGridHeight;
                double positionYLeftBottomCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetY;
                double positionYLeftTopCornerWorkingSpace = positionYLeftBottomCornerWorkingSpace - workingSpaceHeight;

                moveCropControl.X = paintingAreaCheckeredGridTransformGroup.Value.OffsetX;
                moveCropControl.Y = positionYLeftTopCornerWorkingSpace;
            }
            setLimitsForMovableControlBorder(270);
            setCropControlPosition(heightCropControl, widthCropControl, moveCropControl);
        }

        public void setCropControlPosition(double cropControlHeight, double cropControlWidth, TranslateTransform moveValue)
        {
            _transformGridMain.Children.Add(moveValue);
            setControlSize(cropControlHeight, cropControlWidth);
            setRectangleForMovementSize(cropControlHeight, cropControlWidth);
        }

        // TODO: Refactor the setCropSelection function.
        async public void setCropSelection()
        {
            PocketPaintApplication currentPaintApplication = PocketPaintApplication.GetInstance();
            if(currentPaintApplication == null)
            {
                return;
            }

            currentPaintApplication.ProgressRing.IsActive = true;
            await pixelData.preparePaintingAreaCanvasPixel();

            TransformGroup paintingAreaCheckeredGridTransformGroup = currentPaintApplication.PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            // Das Selection-Control soll die Zeichnung einschließen und nicht darauf liegen. Daher wird
            // dieser Wert mit 10 verwendet. Anschließend wird dann die Margin Left und Top, um 5 verringert.
            double doubleBorderWidthValue = offsetMargin * 2.0;

            _transformGridMain.Children.Clear();
            GridMain.Margin = new Thickness(-5, -5, 0, 0);

            rectRectangleCropSelection.Stroke = currentPaintApplication.PaintingAreaCanvas.Children.Count == 0 ? new SolidColorBrush(Colors.Transparent) : new SolidColorBrush(Colors.CornflowerBlue);
            bool isWorkingSpaceNotRotated = paintingAreaCheckeredGridTransformGroup.Value.M11 > 0.0;
            bool isWorkingSpaceRotated90Degree = paintingAreaCheckeredGridTransformGroup.Value.M12 > 0.0;
            bool isWorkingSpaceRotated180Degree = paintingAreaCheckeredGridTransformGroup.Value.M11 < 0.0;
            bool isWorkingSpaceRotated270Degree = paintingAreaCheckeredGridTransformGroup.Value.M12 < 0.0;
            bool isWorkingSpaceFlippedHorizontally = paintingAreaCheckeredGridTransformGroup.Value.M11 == -1.0;
            bool isWorkingSpaceFlippedVertically = paintingAreaCheckeredGridTransformGroup.Value.M22 == -1.0;

            if(isWorkingSpaceNotRotated)
            {
                scaleValueWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.M11;
                _calculateAndSetCropControlPositionWithoutRotating(doubleBorderWidthValue, scaleValueWorkingSpace, isWorkingSpaceFlippedHorizontally, isWorkingSpaceFlippedVertically);
            }
            else if (isWorkingSpaceRotated90Degree)
            {
                // Attention: Working space is rotated 90°
                scaleValueWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.M12;
                _calculateAndSetCropControlPositionWith90DegreeRotation(doubleBorderWidthValue, scaleValueWorkingSpace, isWorkingSpaceFlippedHorizontally, isWorkingSpaceFlippedVertically);
            }
            else if(isWorkingSpaceRotated180Degree)
            {
                // Attention: Working space is rotated 180°
                scaleValueWorkingSpace = Math.Abs(paintingAreaCheckeredGridTransformGroup.Value.M11);
                _calculateAndSetCropControlPositionWith180DegreeRotation(doubleBorderWidthValue, scaleValueWorkingSpace, isWorkingSpaceFlippedHorizontally, isWorkingSpaceFlippedVertically);
            }
            else if(isWorkingSpaceRotated270Degree)
            {
                scaleValueWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.M21;
                // Attention: Working space is rotated 270°
                _calculateAndSetCropControlPositionWith270DegreeRotation(doubleBorderWidthValue, scaleValueWorkingSpace, isWorkingSpaceFlippedHorizontally, isWorkingSpaceFlippedVertically);
            }
            currentPaintApplication.ProgressRing.IsActive = false;
        }

        private void setLimitsForMovableControlBorder(uint rotatedValue)
        {
            PocketPaintApplication currentPaintApplication = PocketPaintApplication.GetInstance();
            if(currentPaintApplication == null)
            {
                return;
            }
            TransformGroup paintingAreaCheckeredGridTransformGroup = currentPaintApplication.PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            double paintingAreaCheckeredGridHeight = currentPaintApplication.PaintingAreaCheckeredGrid.Height;
            double paintingAreaCheckeredGridWidth = currentPaintApplication.PaintingAreaCheckeredGrid.Width;
            
            if(rotatedValue == 0)
            {
                limitLeft = paintingAreaCheckeredGridTransformGroup.Value.OffsetX - offsetMargin;
                limitTop = paintingAreaCheckeredGridTransformGroup.Value.OffsetY - offsetMargin;
                // TODO: Explain the following line.
                limitBottom = limitTop + (paintingAreaCheckeredGridHeight * scaleValueWorkingSpace) + offsetMargin * 2;
                limitRight = limitLeft + (paintingAreaCheckeredGridWidth * scaleValueWorkingSpace) + offsetMargin * 2;
            }
            else if(rotatedValue == 90)
            {
                limitTop = paintingAreaCheckeredGridTransformGroup.Value.OffsetY - offsetMargin;
                limitBottom = limitTop + (paintingAreaCheckeredGridWidth * scaleValueWorkingSpace) + offsetMargin * 2;
                limitRight = paintingAreaCheckeredGridTransformGroup.Value.OffsetX + offsetMargin;
                limitLeft = limitRight - (paintingAreaCheckeredGridHeight * scaleValueWorkingSpace) - offsetMargin * 2;
            }
            else if(rotatedValue == 180)
            {
                limitRight = paintingAreaCheckeredGridTransformGroup.Value.OffsetX + offsetMargin;
                limitBottom = paintingAreaCheckeredGridTransformGroup.Value.OffsetY + offsetMargin;
                limitTop = limitBottom - (paintingAreaCheckeredGridHeight * scaleValueWorkingSpace) - offsetMargin * 2;
                limitLeft = limitRight - (paintingAreaCheckeredGridWidth * scaleValueWorkingSpace) - offsetMargin * 2;
            }
            else if(rotatedValue == 270)
            {
                limitBottom = paintingAreaCheckeredGridTransformGroup.Value.OffsetY + offsetMargin;
                limitTop = limitBottom - (paintingAreaCheckeredGridWidth * scaleValueWorkingSpace) - offsetMargin * 2;
                limitLeft = paintingAreaCheckeredGridTransformGroup.Value.OffsetX - offsetMargin;
                limitRight = limitLeft + (paintingAreaCheckeredGridHeight * scaleValueWorkingSpace) + offsetMargin * 2;
            }
        }

        private TranslateTransform createTranslateTransform(double x, double y)
        {
            var move = new TranslateTransform();
            ((TranslateTransform)move).X = x;
            ((TranslateTransform)move).Y = y;

            return move;
        }

        private void rectCenterBottom_ManipulationDelta_1(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleCropSelection.Height + e.Delta.Translation.Y) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveY = createTranslateTransform(0.0, e.Delta.Translation.Y);

                double sizeValueToAdd = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY + rectRectangleCropSelection.Height + moveY.Y) > limitBottom ? 0.0 : moveY.Y;
                changeHeightOfUiElements(sizeValueToAdd);
                changeMarginBottomOfUiElements(sizeValueToAdd);
            }
        }

        private void rectCenterTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleCropSelection.Height + (e.Delta.Translation.Y * -1)) >= MIN_RECTANGLE_MOVE_HEIGHT)
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
            if (hasElementsPaintingAreaViews() && (rectRectangleCropSelection.Width + (e.Delta.Translation.X * -1)) >= MIN_RECTANGLE_MOVE_WIDTH &&
               (rectRectangleCropSelection.Height + e.Delta.Translation.Y) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveX = createTranslateTransform((e.Delta.Translation.X *-1.0), 0.0);
                var moveY = createTranslateTransform(0.0, (e.Delta.Translation.Y));

                // left
                double sizeValueToAddLeft = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX - moveX.X) < limitLeft ? 0.0 : moveX.X;
                changeWidthOfUiElements(sizeValueToAddLeft);
                changeMarginLeftOfUiElements(sizeValueToAddLeft);

                // bottom
                double sizeValueToAddBottom = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY + rectRectangleCropSelection.Height + moveY.Y) > limitBottom ? 0.0 : moveY.Y;
                changeHeightOfUiElements(sizeValueToAddBottom);
                changeMarginBottomOfUiElements(sizeValueToAddBottom);
            }
        }

        private void rectLeftCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleCropSelection.Width + (e.Delta.Translation.X * -1)) >= MIN_RECTANGLE_MOVE_WIDTH)
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
            if (hasElementsPaintingAreaViews() && (rectRectangleCropSelection.Width + (e.Delta.Translation.X * -1)) >= MIN_RECTANGLE_MOVE_WIDTH &&
                (rectRectangleCropSelection.Height + (e.Delta.Translation.Y * -1)) >= MIN_RECTANGLE_MOVE_HEIGHT)
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
            if (hasElementsPaintingAreaViews() && (rectRectangleCropSelection.Width + e.Delta.Translation.X) >= MIN_RECTANGLE_MOVE_WIDTH &&
                (rectRectangleCropSelection.Height + e.Delta.Translation.Y) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveX = createTranslateTransform((e.Delta.Translation.X), 0.0);
                var moveY = createTranslateTransform(0.0, (e.Delta.Translation.Y));

                // right
                double sizeValueToAddRight = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX + rectRectangleCropSelection.Width + moveX.X) > limitRight ? 0.0 : moveX.X;
                changeWidthOfUiElements(sizeValueToAddRight);
                changeMarginRightOfUiElements(sizeValueToAddRight);

                // bottom
                double sizeValueToAddBottom = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY + rectRectangleCropSelection.Height + moveY.Y) > limitBottom ? 0.0 : moveY.Y;
                changeHeightOfUiElements(sizeValueToAddBottom);
                changeMarginBottomOfUiElements(sizeValueToAddBottom);
            }

        }

        private void rectRightCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleCropSelection.Width + e.Delta.Translation.X) >= MIN_RECTANGLE_MOVE_WIDTH)
            {
                var moveX = createTranslateTransform((e.Delta.Translation.X), 0.0);

                double sizeValueToAdd = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX + rectRectangleCropSelection.Width + moveX.X) > limitRight ? 0.0 : moveX.X;
                changeWidthOfUiElements(sizeValueToAdd);
                changeMarginRightOfUiElements(sizeValueToAdd);
            }
        }

        private void rectRightTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (hasElementsPaintingAreaViews() && (rectRectangleCropSelection.Width + e.Delta.Translation.X) >= MIN_RECTANGLE_MOVE_WIDTH &&
               (rectRectangleCropSelection.Height + (e.Delta.Translation.Y * -1)) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var moveX = createTranslateTransform((e.Delta.Translation.X), 0.0);
                var moveY = createTranslateTransform(0.0, (e.Delta.Translation.Y));

                // right
                double sizeValueToAddRight = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX + rectRectangleCropSelection.Width + moveX.X) > limitRight ? 0.0 : moveX.X;
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
            rectRectangleCropSelection.Height += value;

            double addValueToTouchGrid = value / 4.0;
            if ((rectLeftCenter.Height + addValueToTouchGrid) >= 5.0 || (rectLeftCenter.Height + addValueToTouchGrid) <= 120.0)
            {
                // TODO:
                //GridRectLeftCenter.Height += addValueToTouchGrid;
                //rectLeftCenter.Height += addValueToTouchGrid;

                //GridRectRightCenter.Height += addValueToTouchGrid;
                //rectRightCenter.Height += addValueToTouchGrid;
            }

            if((rectLeftBottomVert.Height + addValueToTouchGrid) >= 5.0 || (rectLeftBottomVert.Height + addValueToTouchGrid) <= 30.0)
            {
                // TODO:
                //GridRectLeftBottom.Height += addValueToTouchGrid;
                //GridRectLeftTop.Height += addValueToTouchGrid;
                //GridRectRightBottom.Height += addValueToTouchGrid;
                //GridRectRightTop.Height += addValueToTouchGrid;
                //rectLeftBottomVert.Height += addValueToTouchGrid;
                //rectRightBottomVert.Height += addValueToTouchGrid;
                //rectRightTopVert.Height += addValueToTouchGrid;
                //rectLeftTopVert.Height += addValueToTouchGrid;
            }

            resetAppBarButtonRectangleSelectionControl(true);
            setIsModifiedRectangleMovement = true;
        }

        private void changeWidthOfUiElements(double value)
        {
            GridMain.Width += value;
            rectRectangleCropSelection.Width += value;

            double addValueToTouchGrid = value / 4.0;
            if ((rectCenterTop.Width + addValueToTouchGrid) >= 5.0 || (rectCenterTop.Width + addValueToTouchGrid) <= 120.0)
            {
                GridRectCenterTop.Width += addValueToTouchGrid;
                rectCenterTop.Width += addValueToTouchGrid;

                GridRectCenterBottom.Width += addValueToTouchGrid;
                rectCenterBottom.Width += addValueToTouchGrid;
            }

            if ((rectRightBottomHorz.Width + addValueToTouchGrid) >= 5.0 || (rectRightBottomHorz.Width + addValueToTouchGrid) <= 30.0)
            {
                // TODO:
                //GridRectLeftBottom.Width += addValueToTouchGrid;
                //GridRectLeftTop.Width += addValueToTouchGrid;
                //GridRectRightBottom.Width += addValueToTouchGrid;
                //GridRectRightTop.Width += addValueToTouchGrid;

                // TODO:
                //rectLeftBottomHorz.Width += addValueToTouchGrid;
                //rectRightBottomHorz.Width += addValueToTouchGrid;
                //rectRightTopHorz.Width += addValueToTouchGrid;
                //rectLeftTopHorz.Width += addValueToTouchGrid;
            }

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

        private void rectRectangleCropSelection_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
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
                    move.X = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX + rectRectangleCropSelection.Width + move.X) > limitRight ? 0.0 : move.X;
                }

                if (move.Y < 0)
                {
                    move.Y = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY + move.Y) < limitTop ? 0.0 : move.Y;
                }
                else
                {
                    move.Y = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY + rectRectangleCropSelection.Height + move.Y) > limitBottom ? 0.0 : move.Y;
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

        private void rectRectangleCropSelection_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
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

        public int getRectangleCropSelectionHeight()
        {
            return (int)(Math.Ceiling((Math.Ceiling(rectRectangleCropSelection.Height) - 10.0) / scaleValueWorkingSpace));
        }

        public int getRectangleCropSelectionWidth()
        {
            return (int)Math.Ceiling(((Math.Ceiling(rectRectangleCropSelection.Width) - 10.0) / scaleValueWorkingSpace));
        }

        public Point getLeftTopCoordinateRectangleCropSelection()
        {
            return new Point((Math.Ceiling(_transformGridMain.Value.OffsetX + 5.0 + GridMain.Margin.Left - leftTopNullPointCropSelection.X) / 0.75), Math.Ceiling((_transformGridMain.Value.OffsetY + 5.0 + GridMain.Margin.Top - leftTopNullPointCropSelection.Y) / 0.75));
        }

        public void setLeftTopNullPointCropSelection(double x, double y)
        {
            leftTopNullPointCropSelection = new Point(x, y);
        }

        public Point getLeftTopNullPointCropSelection()
        {
            return leftTopNullPointCropSelection;
        }
    }
}
