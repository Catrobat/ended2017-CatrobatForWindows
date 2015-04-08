using System;
using System.IO;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// Die Elementvorlage "Benutzersteuerelement" ist unter http://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    public sealed partial class CropControl : UserControl
    {
        TransformGroup _transformGridMain;

        const double MaxVerticalCornerRectangleHeight = 30.0;
        const double MaxHorizontalCornerRectangleWidth = 30.0;
        const double MaxHorizontalCenterRectangleWidth = 120.0;
        const double MaxVerticalCenterRectangleHeight = 120.0;
        const double MaxGridHeight = 140.0;
        const double MinCornerRectangleHeight = 5.0;
        const double MinHorizontalCenterRectangleWidth = 120.0;
        const double MinHorizontalCornerRectangleWidth = 5.0;
        const double MinVerticalCenterRectangleHeight = 5.0;
        const double MinRectangleMoveHeight = 60.0;
        const double MinRectangleMoveWidth = 60.0;
        bool _isModifiedRectangleMovement;

        double _limitLeft = 0.0;
        double _limitRight = 0.0;
        double _limitBottom = 0.0;
        double _limitTop = 0.0;

        double _offsetMargin;
        double _heightCropControl;
        double _widthCropControl;

        double _scaleValueWorkingSpace = 0.0;

        PixelData.PixelData _pixelData = new PixelData.PixelData();

        Point _leftTopNullPointCropSelection;

        public CropControl()
        {
            InitializeComponent();
            _transformGridMain = new TransformGroup();
            GridMain.RenderTransform = _transformGridMain;
            PocketPaintApplication.GetInstance().CropControl = this;
            SetIsModifiedRectangleMovement = false;
            _leftTopNullPointCropSelection = new Point(0.0, 0.0);

            _offsetMargin = 5.0;
            _heightCropControl = 0.0;
            _widthCropControl = 0.0;
            _scaleValueWorkingSpace = 0.0;
        }

        public void SetControlSize(double height, double width)
        {
            GridMain.Height = height;
            GridMain.Width = width;

            // Grid-Height:
            double calculatedGridHeight = (height * 0.3648);
            // TODO: GridRectLeftBottom.Height = (height * 0.1302) > 50.0 ? 50.0 : (height * 0.1302);
            GridRectLeftCenter.Height = calculatedGridHeight > MaxGridHeight ? MaxGridHeight : calculatedGridHeight;
            // TODO: GridRectLeftTop.Height = (height * 0.1302) > 50.0 ? 50.0 : (height * 0.1302);
            // TODO: GridRectRightBottom.Height = (height * 0.1302) > 50.0 ? 50.0 : (height * 0.1302);
            // TODO: GridRectRightTop.Height = (height * 0.1302) > 50.0 ? 50.0 : (height * 0.1302);
            GridRectRightCenter.Height = calculatedGridHeight > MaxGridHeight ? MaxGridHeight : calculatedGridHeight;

            // Grid-Width
            GridRectCenterBottom.Width = width * 0.3648;
            GridRectCenterTop.Width = width * 0.3648;

            // Rectangle-Height
            // 0.3125
            double calculatedCenterRectangleHeight = (height * 0.2);
            double calculatedCornerRectangleHeight = (height * 0.0781);

            SetHeightOfVerticalCornerRectangles(calculatedCornerRectangleHeight);
            SetHeightOfVerticalCenterRectangles(calculatedCenterRectangleHeight);

            // Rectangle-Width
            double calcualtedHorizontalCenterRectangleWidth = (width * 0.2);
            double calcualtedCornerRectangleWidth = (Width * 0.0781);

            SetWidthOfHorizontalCenterRectangles(calcualtedHorizontalCenterRectangleWidth);
            SetWidthOfHorizontalCornerRectangles(calcualtedCornerRectangleWidth);
        }

        public void SetHeightOfVerticalCornerRectangles(double newValue)
        {
            if (newValue > MaxVerticalCornerRectangleHeight)
            {
                rectLeftBottomVert.Height = MaxVerticalCornerRectangleHeight;
                rectLeftTopVert.Height = MaxVerticalCornerRectangleHeight;
                rectRightBottomVert.Height = MaxVerticalCornerRectangleHeight;
                rectRightTopVert.Height = MaxVerticalCornerRectangleHeight;
            }
            else if (newValue < MinCornerRectangleHeight)
            {
                rectLeftBottomVert.Height = MinCornerRectangleHeight;
                rectLeftTopVert.Height = MinCornerRectangleHeight;
                rectRightBottomVert.Height = MinCornerRectangleHeight;
                rectRightTopVert.Height = MinCornerRectangleHeight;
            }
            else
            {
                rectLeftBottomVert.Height = newValue;
                rectLeftTopVert.Height = newValue;
                rectRightBottomVert.Height = newValue;
                rectRightTopVert.Height = newValue;
            }
        }

        private void SetHeightOfVerticalCenterRectangles(double newValue)
        {
            if (newValue > MaxVerticalCenterRectangleHeight)
            {
                rectLeftCenter.Height = MaxVerticalCenterRectangleHeight;
                rectRightCenter.Height = MaxVerticalCenterRectangleHeight;
            }
            else if (newValue < MinVerticalCenterRectangleHeight)
            {
                rectLeftCenter.Height = MinVerticalCenterRectangleHeight;
                rectRightCenter.Height = MinVerticalCenterRectangleHeight;
            }
            else
            {
                rectLeftCenter.Height = newValue;
                rectRightCenter.Height = newValue;
            }
        }

        private void SetWidthOfHorizontalCenterRectangles(double newValue)
        {
            if (newValue > MaxHorizontalCenterRectangleWidth)
            {
                rectCenterTop.Width = MaxHorizontalCenterRectangleWidth;
                rectCenterBottom.Width = MaxHorizontalCenterRectangleWidth;
            }
            else if (newValue < MinHorizontalCenterRectangleWidth)
            {
                rectCenterTop.Width = MinHorizontalCenterRectangleWidth;
                rectCenterBottom.Width = MinHorizontalCenterRectangleWidth;
            }
            else
            {
                rectCenterTop.Width = newValue;
                rectCenterBottom.Width = newValue;
            }
        }

        public void SetWidthOfHorizontalCornerRectangles(double newValue)
        {
            if (newValue > MaxHorizontalCornerRectangleWidth)
            {
                rectLeftBottomHorz.Width = MaxHorizontalCenterRectangleWidth;
                rectLeftTopHorz.Width = MaxHorizontalCenterRectangleWidth;
                rectRightBottomHorz.Width = MaxHorizontalCenterRectangleWidth;
                rectRightTopHorz.Width = MaxHorizontalCenterRectangleWidth;
            }
            else if (newValue < MinHorizontalCornerRectangleWidth)
            {
                rectLeftBottomHorz.Width = MinHorizontalCenterRectangleWidth;
                rectLeftTopHorz.Width = MinHorizontalCenterRectangleWidth;
                rectRightBottomHorz.Width = MinHorizontalCenterRectangleWidth;
                rectRightTopHorz.Width = MinHorizontalCenterRectangleWidth;
            }
            else
            {
                rectLeftBottomHorz.Width = newValue;
                rectLeftTopHorz.Width = newValue;
                rectRightBottomHorz.Width = newValue;
                rectRightTopHorz.Width = newValue;
            }
        }

        public void SetRectangleForMovementSize(double height, double width)
        {
            rectRectangleCropSelection.Height = height;
            rectRectangleCropSelection.Width = width;
        }

        private Point GetExtremeLeftAndTopCoordinate(double initLeft, double initTop,
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
                    if (_pixelData.getPixelAlphaFromCanvas(indexWidth, indexHeight) != 0x00)
                    {
                        extremePoint.X = indexWidth;
                        foundLeftPixel = true;

                        // found extreme point --> set break conditions
                        indexWidth = (int)paintingAreaCanvasWidth;
                        indexHeight = (int)paintingAreaCanvasHeight;
                    }
                }
            // top pixel
            if (foundLeftPixel)
                for (int indexHeight = 0; indexHeight < (int)paintingAreaCanvasHeight; indexHeight++)
                    for (int indexWidth = (int)paintingAreaCanvasWidth - 1; indexWidth >= (int)extremePoint.X; indexWidth--)
                    {
                        if (_pixelData.getPixelAlphaFromCanvas(indexWidth, indexHeight) != 0x00)
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

        private Point GetExtremeRightAndBottomCoordinate(double initRight, double initBottom,
                                                         Point extremeLeftAndTopCoordinate, bool foundLeftPixel,
                                                         int xCoordinateOfExtremeTop)
        {
            double paintingAreaCanvasHeight = PocketPaintApplication.GetInstance().PaintingAreaCanvas.Height;
            double paintingAreaCanvasWidth = PocketPaintApplication.GetInstance().PaintingAreaCanvas.Width;

            Point extremePoint = new Point(initRight, initBottom);

            if (foundLeftPixel)
            {
                // right pixel
                int yCoordinateOfExtremeRight = 0;
                for (int indexWidth = (int)paintingAreaCanvasWidth - 1; indexWidth >= xCoordinateOfExtremeTop; indexWidth--)
                    for (int indexHeight = (int)paintingAreaCanvasHeight - 1; indexHeight >= extremeLeftAndTopCoordinate.Y; indexHeight--)
                    {
                        if (_pixelData.getPixelAlphaFromCanvas(indexWidth, indexHeight) != 0x00)
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
                        if (_pixelData.getPixelAlphaFromCanvas(indexWidth, indexHeight) != 0x00)
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
            TranslateTransform translateTransformMovableCropSelection = new TranslateTransform();
            TransformGroup transformGroupPaintingAreaCheckeredGrid = currentPaintApplication.PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
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
                extremeLeftAndTopCoordinate = GetExtremeLeftAndTopCoordinate(extremeLeftAndTopCoordinate.X, extremeLeftAndTopCoordinate.Y,
                                                                             ref foundLeftPixel, ref xCoordinateOfExtremeTop);
                extremeRightAndBottomCoordinate = GetExtremeRightAndBottomCoordinate(extremeRightAndBottomCoordinate.X, extremeRightAndBottomCoordinate.Y,
                                                                                     extremeLeftAndTopCoordinate, foundLeftPixel, xCoordinateOfExtremeTop);

                _heightCropControl = (extremeRightAndBottomCoordinate.Y - extremeLeftAndTopCoordinate.Y + 1.0) * scaleValueWorkingSpace + doubleBorderWidthValue;
                _widthCropControl = (extremeRightAndBottomCoordinate.X - extremeLeftAndTopCoordinate.X + 1.0) * scaleValueWorkingSpace + doubleBorderWidthValue;

                if (isWorkingSpaceFlippedHorizontally)
                {
                    translateTransformMovableCropSelection.X = transformGroupPaintingAreaCheckeredGrid.Value.OffsetX + ((paintingAreaCheckeredGridWidth - extremeRightAndBottomCoordinate.X) * scaleValueWorkingSpace);
                }
                else
                {
                    translateTransformMovableCropSelection.X = transformGroupPaintingAreaCheckeredGrid.Value.OffsetX + (extremeLeftAndTopCoordinate.X * scaleValueWorkingSpace);
                }

                if (isWorkingSpaceFlippedVertically)
                {
                    translateTransformMovableCropSelection.Y = transformGroupPaintingAreaCheckeredGrid.Value.OffsetY + ((paintingAreaCheckeredGridHeight - extremeRightAndBottomCoordinate.Y) * scaleValueWorkingSpace);
                }
                else
                {
                    translateTransformMovableCropSelection.Y = transformGroupPaintingAreaCheckeredGrid.Value.OffsetY + (extremeLeftAndTopCoordinate.Y * scaleValueWorkingSpace);
                }
            }
            else
            {
                _heightCropControl = scaleValueWorkingSpace * paintingAreaCheckeredGridHeight + _offsetMargin * 2;
                _widthCropControl = scaleValueWorkingSpace * paintingAreaCheckeredGridWidth + _offsetMargin * 2;

                translateTransformMovableCropSelection.X = transformGroupPaintingAreaCheckeredGrid.Value.OffsetX;
                translateTransformMovableCropSelection.Y = transformGroupPaintingAreaCheckeredGrid.Value.OffsetY;
            }
            SetLimitsForMovableControlBorder(0);
            SetLeftTopNullPointCropSelection(transformGroupPaintingAreaCheckeredGrid.Value.OffsetX,
                                         transformGroupPaintingAreaCheckeredGrid.Value.OffsetY);
            SetCropControlPosition(_heightCropControl, _widthCropControl, translateTransformMovableCropSelection);
        }

        private void _calculateAndSetCropControlPositionWith90DegreeRotation(double doubleBorderWidthValue, double scaleValueWorkingSpace, bool isWorkingSpaceFlippedHorizontally, bool isWorkingSpaceFlippedVertically)
        {
            PocketPaintApplication currentPaintApplication = PocketPaintApplication.GetInstance();
            if (currentPaintApplication == null)
            {
                return;
            }
            TranslateTransform moveCropControl = new TranslateTransform();
            TransformGroup paintingAreaCheckeredGridTransformGroup = currentPaintApplication.PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            double paintingAreaCheckeredGridHeight = currentPaintApplication.PaintingAreaCheckeredGrid.Height;
            double paintingAreaCheckeredGridWidth = currentPaintApplication.PaintingAreaCheckeredGrid.Width;
            Point extremeLeftAndTopCoordinate = new Point(paintingAreaCheckeredGridWidth - 1.0, paintingAreaCheckeredGridHeight - 1.0);
            Point extremeRightAndBottomCoordinate = new Point(0.0, 0.0);

            if (currentPaintApplication.PaintingAreaCanvas.Children.Count != 0)
            {
                bool foundLeftPixel = false;
                int xCoordinateOfExtremeTop = 0;
                extremeLeftAndTopCoordinate = GetExtremeLeftAndTopCoordinate(extremeLeftAndTopCoordinate.X, extremeLeftAndTopCoordinate.Y,
                                                                             ref foundLeftPixel, ref xCoordinateOfExtremeTop);
                extremeRightAndBottomCoordinate = GetExtremeRightAndBottomCoordinate(extremeRightAndBottomCoordinate.X, extremeRightAndBottomCoordinate.Y,
                                                                                     extremeLeftAndTopCoordinate, foundLeftPixel, xCoordinateOfExtremeTop);

                _heightCropControl = (extremeRightAndBottomCoordinate.X - extremeLeftAndTopCoordinate.X + 1.0) * scaleValueWorkingSpace + doubleBorderWidthValue;
                _widthCropControl = (extremeRightAndBottomCoordinate.Y - extremeLeftAndTopCoordinate.Y + 1.0) * scaleValueWorkingSpace + doubleBorderWidthValue;

                double workingSpaceWidth = scaleValueWorkingSpace * paintingAreaCheckeredGridHeight;
                double positionXRightTopCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetX;
                double positionXLeftTopCornerWorkingSpace = positionXRightTopCornerWorkingSpace - workingSpaceWidth;

                if (isWorkingSpaceFlippedHorizontally)
                {
                    moveCropControl.X = positionXLeftTopCornerWorkingSpace + (extremeLeftAndTopCoordinate.Y * scaleValueWorkingSpace);
                }
                else
                {
                    moveCropControl.X = positionXLeftTopCornerWorkingSpace + ((paintingAreaCheckeredGridHeight - (extremeRightAndBottomCoordinate.Y + 1.0)) * scaleValueWorkingSpace);
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
                _heightCropControl = scaleValueWorkingSpace * paintingAreaCheckeredGridWidth + doubleBorderWidthValue;
                _widthCropControl = scaleValueWorkingSpace * paintingAreaCheckeredGridHeight + doubleBorderWidthValue;
                double workingSpaceWidth = scaleValueWorkingSpace * paintingAreaCheckeredGridHeight;
                double positionXRightTopCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetX;
                double positionXLeftTopCornerWorkingSpace = positionXRightTopCornerWorkingSpace - workingSpaceWidth;

                moveCropControl.X = positionXLeftTopCornerWorkingSpace;
                moveCropControl.Y = paintingAreaCheckeredGridTransformGroup.Value.OffsetY;
            }
            SetLimitsForMovableControlBorder(90);
            SetLeftTopNullPointCropSelection(paintingAreaCheckeredGridTransformGroup.Value.OffsetX - PocketPaintApplication.GetInstance().PaintingAreaCanvas.Height * scaleValueWorkingSpace,
                             paintingAreaCheckeredGridTransformGroup.Value.OffsetY);
            SetCropControlPosition(_heightCropControl, _widthCropControl, moveCropControl);
        }

        private void _calculateAndSetCropControlPositionWith180DegreeRotation(double doubleBorderWidthValue, double scaleValueWorkingSpace, bool isWorkingSpaceFlippedHorizontally, bool isWorkingSpaceFlippedVertically)
        {
            PocketPaintApplication currentPaintApplication = PocketPaintApplication.GetInstance();
            if (currentPaintApplication == null)
            {
                return;
            }
            TranslateTransform moveCropControl = new TranslateTransform();
            TransformGroup paintingAreaCheckeredGridTransformGroup = currentPaintApplication.PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            double paintingAreaCheckeredGridHeight = currentPaintApplication.PaintingAreaCheckeredGrid.Height;
            double paintingAreaCheckeredGridWidth = currentPaintApplication.PaintingAreaCheckeredGrid.Width;

            Point extremeLeftAndTopCoordinate = new Point(paintingAreaCheckeredGridWidth - 1.0, 0.0);
            Point extremeRightAndBottomCoordinate = new Point(0.0, paintingAreaCheckeredGridHeight - 1.0);

            if (currentPaintApplication.PaintingAreaCanvas.Children.Count != 0)
            {
                bool foundLeftPixel = false;
                int xCoordinateOfExtremeTop = 0;
                extremeLeftAndTopCoordinate = GetExtremeLeftAndTopCoordinate(extremeLeftAndTopCoordinate.X, extremeLeftAndTopCoordinate.Y,
                                                                             ref foundLeftPixel, ref xCoordinateOfExtremeTop);
                extremeRightAndBottomCoordinate = GetExtremeRightAndBottomCoordinate(extremeRightAndBottomCoordinate.X, extremeRightAndBottomCoordinate.Y,
                                                                                     extremeLeftAndTopCoordinate, foundLeftPixel, xCoordinateOfExtremeTop);

                _heightCropControl = (extremeRightAndBottomCoordinate.Y - extremeLeftAndTopCoordinate.Y) * scaleValueWorkingSpace + doubleBorderWidthValue;
                _widthCropControl = (extremeRightAndBottomCoordinate.X - extremeLeftAndTopCoordinate.X) * scaleValueWorkingSpace + doubleBorderWidthValue;

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
                _heightCropControl = scaleValueWorkingSpace * paintingAreaCheckeredGridHeight + doubleBorderWidthValue;
                _widthCropControl = scaleValueWorkingSpace * paintingAreaCheckeredGridWidth + doubleBorderWidthValue;
                double workingSpaceHeight = scaleValueWorkingSpace * paintingAreaCheckeredGridHeight;
                double workingSpaceWidth = scaleValueWorkingSpace * paintingAreaCheckeredGridWidth;
                double positionXRightBottomCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetX;
                double positionXLeftBottomCornerWorkingSpace = positionXRightBottomCornerWorkingSpace - workingSpaceWidth;
                double positionYRigthBottomCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetY;
                double positionYRightTopCornerWorkingSpace = positionYRigthBottomCornerWorkingSpace - workingSpaceHeight;
                moveCropControl.X = positionXLeftBottomCornerWorkingSpace;
                moveCropControl.Y = positionYRightTopCornerWorkingSpace;
            }
            SetLimitsForMovableControlBorder(180);
            SetCropControlPosition(_heightCropControl, _widthCropControl, moveCropControl);
        }

        private void _calculateAndSetCropControlPositionWith270DegreeRotation(double doubleBorderWidthValue, double scaleValueWorkingSpace, bool isWorkingSpaceFlippedHorizontally, bool isWorkingSpaceFlippedVertically)
        {
            PocketPaintApplication currentPaintApplication = PocketPaintApplication.GetInstance();
            if (currentPaintApplication == null)
            {
                return;
            }
            TranslateTransform moveCropControl = new TranslateTransform();
            TransformGroup paintingAreaCheckeredGridTransformGroup = currentPaintApplication.PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            double paintingAreaCheckeredGridHeight = currentPaintApplication.PaintingAreaCheckeredGrid.Height;
            double paintingAreaCheckeredGridWidth = currentPaintApplication.PaintingAreaCheckeredGrid.Width;

            Point extremeLeftAndTopCoordinate = new Point(paintingAreaCheckeredGridWidth - 1.0, paintingAreaCheckeredGridHeight - 1.0);
            Point extremeRightAndBottomCoordinate = new Point(0.0, 0.0);

            if (currentPaintApplication.PaintingAreaCanvas.Children.Count != 0)
            {
                bool foundLeftPixel = false;
                int xCoordinateOfExtremeTop = 0;
                extremeLeftAndTopCoordinate = GetExtremeLeftAndTopCoordinate(extremeLeftAndTopCoordinate.X, extremeLeftAndTopCoordinate.Y,
                                                                             ref foundLeftPixel, ref xCoordinateOfExtremeTop);
                extremeRightAndBottomCoordinate = GetExtremeRightAndBottomCoordinate(extremeRightAndBottomCoordinate.X, extremeRightAndBottomCoordinate.Y,
                                                                                     extremeLeftAndTopCoordinate, foundLeftPixel, xCoordinateOfExtremeTop);

                _heightCropControl = (extremeRightAndBottomCoordinate.X - extremeLeftAndTopCoordinate.X) * scaleValueWorkingSpace + doubleBorderWidthValue;
                _widthCropControl = (extremeRightAndBottomCoordinate.Y - extremeLeftAndTopCoordinate.Y) * scaleValueWorkingSpace + doubleBorderWidthValue;
                double workingSpaceHeight = scaleValueWorkingSpace * paintingAreaCheckeredGridWidth;
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
                _heightCropControl = paintingAreaCheckeredGridTransformGroup.Value.M21 * paintingAreaCheckeredGridWidth + doubleBorderWidthValue;
                _widthCropControl = paintingAreaCheckeredGridTransformGroup.Value.M21 * paintingAreaCheckeredGridHeight + doubleBorderWidthValue;
                double workingSpaceHeight = paintingAreaCheckeredGridTransformGroup.Value.M21 * paintingAreaCheckeredGridWidth;
                double positionYLeftBottomCornerWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.OffsetY;
                double positionYLeftTopCornerWorkingSpace = positionYLeftBottomCornerWorkingSpace - workingSpaceHeight;

                moveCropControl.X = paintingAreaCheckeredGridTransformGroup.Value.OffsetX;
                moveCropControl.Y = positionYLeftTopCornerWorkingSpace;
            }
            SetLimitsForMovableControlBorder(270);
            SetCropControlPosition(_heightCropControl, _widthCropControl, moveCropControl);
        }

        public void SetCropControlPosition(double cropControlHeight, double cropControlWidth, TranslateTransform moveValue)
        {
            SetControlSize(cropControlHeight, cropControlWidth);
            SetRectangleForMovementSize(cropControlHeight, cropControlWidth);
            _transformGridMain.Children.Add(moveValue);
        }

        // TODO: Refactor the setCropSelection function.
        async public void SetCropSelection()
        {
            PocketPaintApplication currentPaintApplication = PocketPaintApplication.GetInstance();
            if (currentPaintApplication == null)
            {
                return;
            }

            currentPaintApplication.ProgressRing.IsActive = true;
            await _pixelData.preparePaintingAreaCanvasPixel();

            TransformGroup paintingAreaCheckeredGridTransformGroup = currentPaintApplication.PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            // Das Selection-Control soll die Zeichnung einschließen und nicht darauf liegen. Daher wird
            // dieser Wert mit 10 verwendet. Anschließend wird dann die Margin Left und Top, um 5 verringert.
            double doubleBorderWidthValue = _offsetMargin * 2.0;

            _transformGridMain.Children.Clear();
            GridMain.Margin = new Thickness(-5.0, -5.0, 0.0, 0.0);

            rectRectangleCropSelection.Stroke = currentPaintApplication.PaintingAreaCanvas.Children.Count == 0 ? new SolidColorBrush(Colors.Transparent) : new SolidColorBrush(Colors.CornflowerBlue);
            bool isWorkingSpaceNotRotated = paintingAreaCheckeredGridTransformGroup.Value.M11 > 0.0;
            bool isWorkingSpaceRotated90Degree = paintingAreaCheckeredGridTransformGroup.Value.M12 > 0.0;
            bool isWorkingSpaceRotated180Degree = paintingAreaCheckeredGridTransformGroup.Value.M11 < 0.0;
            bool isWorkingSpaceRotated270Degree = paintingAreaCheckeredGridTransformGroup.Value.M12 < 0.0;
            bool isWorkingSpaceFlippedHorizontally = paintingAreaCheckeredGridTransformGroup.Value.M11 == -1.0;
            bool isWorkingSpaceFlippedVertically = paintingAreaCheckeredGridTransformGroup.Value.M22 == -1.0;

            if (isWorkingSpaceNotRotated)
            {
                _scaleValueWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.M11;
                _calculateAndSetCropControlPositionWithoutRotating(doubleBorderWidthValue, _scaleValueWorkingSpace, isWorkingSpaceFlippedHorizontally, isWorkingSpaceFlippedVertically);
            }
            else if (isWorkingSpaceRotated90Degree)
            {
                // Attention: Working space is rotated 90°
                _scaleValueWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.M12;
                _calculateAndSetCropControlPositionWith90DegreeRotation(doubleBorderWidthValue, _scaleValueWorkingSpace, isWorkingSpaceFlippedHorizontally, isWorkingSpaceFlippedVertically);
            }
            else if (isWorkingSpaceRotated180Degree)
            {
                // Attention: Working space is rotated 180°
                _scaleValueWorkingSpace = Math.Abs(paintingAreaCheckeredGridTransformGroup.Value.M11);
                _calculateAndSetCropControlPositionWith180DegreeRotation(doubleBorderWidthValue, _scaleValueWorkingSpace, isWorkingSpaceFlippedHorizontally, isWorkingSpaceFlippedVertically);
            }
            else if (isWorkingSpaceRotated270Degree)
            {
                _scaleValueWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.M21;
                // Attention: Working space is rotated 270°
                _calculateAndSetCropControlPositionWith270DegreeRotation(doubleBorderWidthValue, _scaleValueWorkingSpace, isWorkingSpaceFlippedHorizontally, isWorkingSpaceFlippedVertically);
            }
            currentPaintApplication.ProgressRing.IsActive = false;
        }

        private void SetLimitsForMovableControlBorder(uint rotatedValue)
        {
            PocketPaintApplication currentPaintApplication = PocketPaintApplication.GetInstance();
            if (currentPaintApplication == null)
            {
                return;
            }
            TransformGroup paintingAreaCheckeredGridTransformGroup = currentPaintApplication.PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            double paintingAreaCheckeredGridHeight = currentPaintApplication.PaintingAreaCheckeredGrid.Height;
            double paintingAreaCheckeredGridWidth = currentPaintApplication.PaintingAreaCheckeredGrid.Width;

            if (rotatedValue == 0)
            {
                _limitLeft = paintingAreaCheckeredGridTransformGroup.Value.OffsetX - _offsetMargin;
                _limitTop = paintingAreaCheckeredGridTransformGroup.Value.OffsetY - _offsetMargin;
                // TODO: Explain the following line.
                _limitBottom = _limitTop + (paintingAreaCheckeredGridHeight * _scaleValueWorkingSpace) + _offsetMargin * 2;
                _limitRight = _limitLeft + (paintingAreaCheckeredGridWidth * _scaleValueWorkingSpace) + _offsetMargin * 2;
            }
            else if (rotatedValue == 90)
            {
                _limitTop = paintingAreaCheckeredGridTransformGroup.Value.OffsetY - _offsetMargin;
                _limitBottom = _limitTop + (paintingAreaCheckeredGridWidth * _scaleValueWorkingSpace) + _offsetMargin * 2;
                _limitRight = paintingAreaCheckeredGridTransformGroup.Value.OffsetX + _offsetMargin;
                _limitLeft = _limitRight - (paintingAreaCheckeredGridHeight * _scaleValueWorkingSpace) - _offsetMargin * 2;
            }
            else if (rotatedValue == 180)
            {
                _limitRight = paintingAreaCheckeredGridTransformGroup.Value.OffsetX + _offsetMargin;
                _limitBottom = paintingAreaCheckeredGridTransformGroup.Value.OffsetY + _offsetMargin;
                _limitTop = _limitBottom - (paintingAreaCheckeredGridHeight * _scaleValueWorkingSpace) - _offsetMargin * 2;
                _limitLeft = _limitRight - (paintingAreaCheckeredGridWidth * _scaleValueWorkingSpace) - _offsetMargin * 2;
            }
            else if (rotatedValue == 270)
            {
                _limitBottom = paintingAreaCheckeredGridTransformGroup.Value.OffsetY + _offsetMargin;
                _limitTop = _limitBottom - (paintingAreaCheckeredGridWidth * _scaleValueWorkingSpace) - _offsetMargin * 2;
                _limitLeft = paintingAreaCheckeredGridTransformGroup.Value.OffsetX - _offsetMargin;
                _limitRight = _limitLeft + (paintingAreaCheckeredGridHeight * _scaleValueWorkingSpace) + _offsetMargin * 2;
            }
        }

        private TranslateTransform CreateTranslateTransform(double x, double y)
        {
            var move = new TranslateTransform();
            ((TranslateTransform)move).X = x;
            ((TranslateTransform)move).Y = y;

            return move;
        }

        private void rectCenterBottom_ManipulationDelta_1(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (HasElementsPaintingAreaViews() && (rectRectangleCropSelection.Height + e.Delta.Translation.Y) >= MinRectangleMoveHeight)
            {
                var moveY = CreateTranslateTransform(0.0, e.Delta.Translation.Y);

                double sizeValueToAdd = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY + rectRectangleCropSelection.Height + moveY.Y) > _limitBottom ? 0.0 : moveY.Y;
                ChangeHeightOfUiElements(sizeValueToAdd);
                ChangeMarginBottomOfUiElements(sizeValueToAdd);
            }
        }

        private void rectCenterTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (HasElementsPaintingAreaViews() && (rectRectangleCropSelection.Height + (e.Delta.Translation.Y * -1)) >= MinRectangleMoveHeight)
            {
                var moveY = CreateTranslateTransform(0.0, e.Delta.Translation.Y);
                moveY.Y *= -1.0;
                double sizeValueToAdd = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY - moveY.Y) < _limitTop ? 0.0 : moveY.Y;
                ChangeHeightOfUiElements(sizeValueToAdd);
                ChangeMarginTopOfUiElements(sizeValueToAdd);
            }
        }

        private void rectLeftBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (HasElementsPaintingAreaViews() && (rectRectangleCropSelection.Width + (e.Delta.Translation.X * -1)) >= MinRectangleMoveWidth &&
               (rectRectangleCropSelection.Height + e.Delta.Translation.Y) >= MinRectangleMoveHeight)
            {
                var moveX = CreateTranslateTransform((e.Delta.Translation.X * -1.0), 0.0);
                var moveY = CreateTranslateTransform(0.0, (e.Delta.Translation.Y));

                // left
                double sizeValueToAddLeft = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX - moveX.X) < _limitLeft ? 0.0 : moveX.X;
                ChangeWidthOfUiElements(sizeValueToAddLeft);
                ChangeMarginLeftOfUiElements(sizeValueToAddLeft);

                // bottom
                double sizeValueToAddBottom = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY + rectRectangleCropSelection.Height + moveY.Y) > _limitBottom ? 0.0 : moveY.Y;
                ChangeHeightOfUiElements(sizeValueToAddBottom);
                ChangeMarginBottomOfUiElements(sizeValueToAddBottom);
            }
        }

        private void rectLeftCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (HasElementsPaintingAreaViews() && (rectRectangleCropSelection.Width + (e.Delta.Translation.X * -1)) >= MinRectangleMoveWidth)
            {
                var moveX = CreateTranslateTransform((e.Delta.Translation.X), 0.0);
                moveX.X *= -1.0;
                double sizeValueToAdd = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX - moveX.X) < _limitLeft ? 0.0 : moveX.X;
                ChangeWidthOfUiElements(sizeValueToAdd);
                ChangeMarginLeftOfUiElements(sizeValueToAdd);
            }
        }

        private void rectLeftTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (HasElementsPaintingAreaViews() && (rectRectangleCropSelection.Width + (e.Delta.Translation.X * -1)) >= MinRectangleMoveWidth &&
                (rectRectangleCropSelection.Height + (e.Delta.Translation.Y * -1)) >= MinRectangleMoveHeight)
            {
                // left
                var moveX = CreateTranslateTransform((e.Delta.Translation.X), 0.0);
                moveX.X *= -1.0;
                double sizeValueToAddLeft = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX - moveX.X) < _limitLeft ? 0.0 : moveX.X;
                ChangeWidthOfUiElements(sizeValueToAddLeft);
                ChangeMarginLeftOfUiElements(sizeValueToAddLeft);

                // top
                var moveY = CreateTranslateTransform(0.0, (e.Delta.Translation.Y));
                moveY.Y *= -1;
                double sizeValueToAddTop = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY - moveY.Y) < _limitTop ? 0.0 : moveY.Y;
                ChangeHeightOfUiElements(sizeValueToAddTop);
                ChangeMarginTopOfUiElements(sizeValueToAddTop);
            }
        }

        private void rectRightBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (HasElementsPaintingAreaViews() && (rectRectangleCropSelection.Width + e.Delta.Translation.X) >= MinRectangleMoveWidth &&
                (rectRectangleCropSelection.Height + e.Delta.Translation.Y) >= MinRectangleMoveHeight)
            {
                var moveX = CreateTranslateTransform((e.Delta.Translation.X), 0.0);
                var moveY = CreateTranslateTransform(0.0, (e.Delta.Translation.Y));

                // right
                double sizeValueToAddRight = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX + rectRectangleCropSelection.Width + moveX.X) > _limitRight ? 0.0 : moveX.X;
                ChangeWidthOfUiElements(sizeValueToAddRight);
                ChangeMarginRightOfUiElements(sizeValueToAddRight);

                // bottom
                double sizeValueToAddBottom = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY + rectRectangleCropSelection.Height + moveY.Y) > _limitBottom ? 0.0 : moveY.Y;
                ChangeHeightOfUiElements(sizeValueToAddBottom);
                ChangeMarginBottomOfUiElements(sizeValueToAddBottom);
            }

        }

        private void rectRightCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (HasElementsPaintingAreaViews() && (rectRectangleCropSelection.Width + e.Delta.Translation.X) >= MinRectangleMoveWidth)
            {
                var moveX = CreateTranslateTransform((e.Delta.Translation.X), 0.0);

                double sizeValueToAdd = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX + rectRectangleCropSelection.Width + moveX.X) > _limitRight ? 0.0 : moveX.X;
                ChangeWidthOfUiElements(sizeValueToAdd);
                ChangeMarginRightOfUiElements(sizeValueToAdd);
            }
        }

        private void rectRightTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (HasElementsPaintingAreaViews() && (rectRectangleCropSelection.Width + e.Delta.Translation.X) >= MinRectangleMoveWidth &&
               (rectRectangleCropSelection.Height + (e.Delta.Translation.Y * -1)) >= MinRectangleMoveHeight)
            {
                var moveX = CreateTranslateTransform((e.Delta.Translation.X), 0.0);
                var moveY = CreateTranslateTransform(0.0, (e.Delta.Translation.Y));

                // right
                double sizeValueToAddRight = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX + rectRectangleCropSelection.Width + moveX.X) > _limitRight ? 0.0 : moveX.X;
                ChangeWidthOfUiElements(sizeValueToAddRight);
                ChangeMarginRightOfUiElements(sizeValueToAddRight);

                // top
                moveY.Y *= -1.0;
                double sizeValueToAddTop = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY - moveY.Y) < _limitTop ? 0.0 : moveY.Y;
                ChangeHeightOfUiElements(sizeValueToAddTop);
                ChangeMarginTopOfUiElements(sizeValueToAddTop);
            }

        }

        private void ChangeHeightOfUiElements(double value)
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

            if ((rectLeftBottomVert.Height + addValueToTouchGrid) >= 5.0 || (rectLeftBottomVert.Height + addValueToTouchGrid) <= 30.0)
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

            ResetAppBarButtonRectangleSelectionControl(true);
            SetIsModifiedRectangleMovement = true;
        }

        private void ChangeWidthOfUiElements(double value)
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

            ResetAppBarButtonRectangleSelectionControl(true);
            SetIsModifiedRectangleMovement = true;
        }

        private void ChangeMarginBottomOfUiElements(double value)
        {
            GridMain.Margin = new Thickness(GridMain.Margin.Left, GridMain.Margin.Top,
                GridMain.Margin.Right, GridMain.Margin.Bottom - value);
        }

        private void ChangeMarginLeftOfUiElements(double value)
        {
            GridMain.Margin = new Thickness(GridMain.Margin.Left - value, GridMain.Margin.Top,
                GridMain.Margin.Right, GridMain.Margin.Bottom);
        }

        private void ChangeMarginRightOfUiElements(double value)
        {
            GridMain.Margin = new Thickness(GridMain.Margin.Left, GridMain.Margin.Top,
                GridMain.Margin.Right - value, GridMain.Margin.Bottom);
        }

        private void ChangeMarginTopOfUiElements(double value)
        {
            GridMain.Margin = new Thickness(GridMain.Margin.Left, GridMain.Margin.Top - value,
                GridMain.Margin.Right, GridMain.Margin.Bottom);
        }

        private void rectRectangleCropSelection_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (HasElementsPaintingAreaViews())
            {
                var move = new TranslateTransform();

                //((TranslateTransform)move).X = e.Delta.Translation.X;
                ((TranslateTransform)move).X = e.Delta.Translation.X;
                ((TranslateTransform)move).Y = e.Delta.Translation.Y;
                if (move.X < 0)
                {
                    move.X = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX + move.X) < _limitLeft ? 0.0 : move.X;
                }
                else
                {
                    move.X = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX + rectRectangleCropSelection.Width + move.X) > _limitRight ? 0.0 : move.X;
                }

                if (move.Y < 0)
                {
                    move.Y = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY + move.Y) < _limitTop ? 0.0 : move.Y;
                }
                else
                {
                    move.Y = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY + rectRectangleCropSelection.Height + move.Y) > _limitBottom ? 0.0 : move.Y;
                }
                _transformGridMain.Children.Add(move);

                //move.X = _transformGridMain.Value.OffsetX;
                //move.Y = _transformGridMain.Value.OffsetY;
                //_transformGridMain.Children.Clear();
                //_transformGridMain.Children.Add(move);

                ResetAppBarButtonRectangleSelectionControl(true);
                SetIsModifiedRectangleMovement = true;
            }
        }

        private void rectRectangleCropSelection_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            PocketPaintApplication.GetInstance().ToolCurrent.HandleUp(new Point());
        }

        public void ResetAppBarButtonRectangleSelectionControl(bool activated)
        {
            AppBarButton appBarButtonReset = PocketPaintApplication.GetInstance().PaintingAreaView.getAppBarResetButton();
            if (appBarButtonReset != null)
            {
                appBarButtonReset.IsEnabled = activated;
            }
        }

        public bool SetIsModifiedRectangleMovement
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

        public bool HasElementsPaintingAreaViews()
        {
            bool result = false;
            if (PocketPaintApplication.GetInstance().PaintingAreaCanvas != null)
            {
                result = PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count > 0 ? true : false;
            }
            return result;
        }

        public double GetRectangleCropSelectionHeight()
        {
            return (rectRectangleCropSelection.Height - 10.0) / _scaleValueWorkingSpace;
        }

        public double GetRectangleCropSelectionWidth()
        {
            return (rectRectangleCropSelection.Width - 10.0) / _scaleValueWorkingSpace;
        }

        public Point GetLeftTopCoordinateRectangleCropSelection()
        {
            TransformGroup paintingAreaCheckeredGridTransformGroup = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            bool isWorkingSpaceNotRotated = paintingAreaCheckeredGridTransformGroup.Value.M11 > 0.0;
            if (isWorkingSpaceNotRotated)
            {
                return new Point((Math.Ceiling(_transformGridMain.Value.OffsetX + 5.0 + GridMain.Margin.Left - _leftTopNullPointCropSelection.X) / 0.75), Math.Ceiling((_transformGridMain.Value.OffsetY + 5.0 + GridMain.Margin.Top - _leftTopNullPointCropSelection.Y) / 0.75));
            }
            else
            {
                double offsetY = GridMain.Margin.Right / _scaleValueWorkingSpace;
                return new Point((_transformGridMain.Value.OffsetY + 5.0 + GridMain.Margin.Top - _leftTopNullPointCropSelection.Y) / _scaleValueWorkingSpace, offsetY);
            }
        }

        public void SetLeftTopNullPointCropSelection(double x, double y)
        {
            _leftTopNullPointCropSelection = new Point(x, y);
        }

        public Point GetLeftTopNullPointCropSelection()
        {
            return _leftTopNullPointCropSelection;
        }

        public void AddWriteableBitmapToCanvas(WriteableBitmap writeableBitmapToAdd)
        {
            TransformGroup paintingAreaCheckeredGridTransformGroup = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            bool isWorkingSpaceNotRotated = paintingAreaCheckeredGridTransformGroup.Value.M11 > 0.0;
            bool isWorkingSpaceRotated90Degree = paintingAreaCheckeredGridTransformGroup.Value.M12 > 0.0;

            int height = (int)Math.Ceiling(GetRectangleCropSelectionHeight());
            int width = (int)Math.Ceiling(GetRectangleCropSelectionWidth());
            Point leftTopRectangleCropSelection = GetLeftTopCoordinateRectangleCropSelection();

            Image image = new Image();

            if (isWorkingSpaceNotRotated)
            {
                if ((height + leftTopRectangleCropSelection.Y != (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.Height)
                    || (width + leftTopRectangleCropSelection.X != (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.Width))
                {
                    image.Source = writeableBitmapToAdd;
                    image.Height = writeableBitmapToAdd.PixelHeight;
                    image.Width = writeableBitmapToAdd.PixelWidth;

                    PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();
                    PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(image);

                    PocketPaintApplication.GetInstance().PaintingAreaView.setSizeOfPaintingAreaViewCheckered(writeableBitmapToAdd.PixelHeight, writeableBitmapToAdd.PixelWidth);
                    PocketPaintApplication.GetInstance().PaintingAreaCanvas.Height = writeableBitmapToAdd.PixelHeight;
                    PocketPaintApplication.GetInstance().PaintingAreaCanvas.Width = writeableBitmapToAdd.PixelWidth;
                    PocketPaintApplication.GetInstance().CropControl.SetCropSelection();
                }
            }
            else if (isWorkingSpaceRotated90Degree)
            {
                image.Source = writeableBitmapToAdd;
                image.Height = writeableBitmapToAdd.PixelHeight;
                image.Width = writeableBitmapToAdd.PixelWidth;

                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(image);
                PocketPaintApplication.GetInstance().PaintingAreaView.setSizeOfPaintingAreaViewCheckered(writeableBitmapToAdd.PixelHeight, writeableBitmapToAdd.PixelWidth);
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Height = writeableBitmapToAdd.PixelHeight;
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Width = writeableBitmapToAdd.PixelWidth;
                PocketPaintApplication.GetInstance().CropControl.SetCropSelection();
            }
        }

        async public void CropImage()
        {
            Point leftTopRectangleCropSelection = GetLeftTopCoordinateRectangleCropSelection();
            double xOffset = leftTopRectangleCropSelection.X;
            double yOffset = leftTopRectangleCropSelection.Y;
            int height = (int)Math.Ceiling(PocketPaintApplication.GetInstance().CropControl.GetRectangleCropSelectionHeight());
            int width = (int)Math.Ceiling(PocketPaintApplication.GetInstance().CropControl.GetRectangleCropSelectionWidth());

            TransformGroup paintingAreaCheckeredGridTransformGroup = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            bool isWorkingSpaceNotRotated = paintingAreaCheckeredGridTransformGroup.Value.M11 > 0.0;
            bool isWorkingSpaceRotated90Degree = paintingAreaCheckeredGridTransformGroup.Value.M12 > 0.0;

            WriteableBitmap wbCroppedBitmap = null;
            if (PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count != 0)
            {
                string filename = ("karlidavidtest") + ".png";
                await PocketPaintApplication.GetInstance().StorageIo.WriteBitmapToPngMediaLibrary(filename);
                StorageFile storageFile = await KnownFolders.PicturesLibrary.GetFileAsync(filename);
                InMemoryRandomAccessStream mrAccessStream = new InMemoryRandomAccessStream();

                using (Stream stream = await storageFile.OpenStreamForReadAsync())
                {
                    using (var memStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memStream);
                        memStream.Position = 0;

                        BitmapDecoder bitmapDecoder = await BitmapDecoder.CreateAsync(memStream.AsRandomAccessStream());
                        BitmapEncoder bitmapEncoder = await BitmapEncoder.CreateForTranscodingAsync(mrAccessStream, bitmapDecoder);

                        bitmapEncoder.BitmapTransform.ScaledHeight = (uint)PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderSize.Height;
                        bitmapEncoder.BitmapTransform.ScaledWidth = (uint)PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderSize.Width;

                        uint canvasHeight = (uint)PocketPaintApplication.GetInstance().PaintingAreaCanvas.Height;
                        uint canvasWidth = (uint)PocketPaintApplication.GetInstance().PaintingAreaCanvas.Width;
                        BitmapBounds bitmapBoundsOfDrawing = new BitmapBounds();

                        if (isWorkingSpaceNotRotated)
                        {
                            // bitmapBounds starts with index zero. So we have to substract the height and width with one.
                            bitmapBoundsOfDrawing.Height = (uint)height - 1;
                            bitmapBoundsOfDrawing.Width = (uint)width - 1;
                            uint uwidth = (uint)width;
                            bitmapBoundsOfDrawing.X = ((uint)width + (uint)xOffset) > canvasWidth ? canvasWidth - uwidth : (uint)xOffset;
                            uint uheight = (uint)height;
                            bitmapBoundsOfDrawing.Y = ((uint)height + (uint)yOffset) > canvasHeight ? canvasHeight - uheight : (uint)yOffset;
                            wbCroppedBitmap = new WriteableBitmap(width, height);
                        }
                        else if (isWorkingSpaceRotated90Degree)
                        {
                            bitmapBoundsOfDrawing.Height = (uint)width;
                            bitmapBoundsOfDrawing.Width = (uint)height;
                            uint uwidth = (uint)width;
                            bitmapBoundsOfDrawing.X = ((uint)width + (uint)xOffset) > canvasHeight ? canvasHeight - uwidth : (uint)xOffset;
                            uint uheight = (uint)height;
                            bitmapBoundsOfDrawing.Y = ((uint)height + (uint)yOffset) > canvasWidth ? canvasWidth - uheight : (uint)yOffset;
                            wbCroppedBitmap = new WriteableBitmap(height, width);
                        }
                        bitmapEncoder.BitmapTransform.Bounds = bitmapBoundsOfDrawing;

                        // write out to the stream
                        try
                        {
                            await bitmapEncoder.FlushAsync();
                        }
                        catch (Exception ex)
                        {
                        }
                        wbCroppedBitmap.SetSource(mrAccessStream);
                        AddWriteableBitmapToCanvas(wbCroppedBitmap);
                    }
                    //render the stream to the screen
                }
            }
        }
    }
}
