using Catrobat.Paint.WindowsPhone.Command;
using System;
using System.IO;
using System.Threading.Tasks;
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

        double _heightOfRectangle = 0.0;
        double _widthOfRectangle = 0.0;

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
            _heightOfRectangle = rectRectangleCropSelection.Height;
            _widthOfRectangle = rectRectangleCropSelection.Width;
        }

        public void SetControlSize(double height, double width)
        {
            GridMain.Height = height;
            GridMain.Width = width;

            double calculatedGridHeight = (height * 0.3648);
            GridRectLeftCenter.Height = calculatedGridHeight > MaxGridHeight ? MaxGridHeight : calculatedGridHeight;
            GridRectRightCenter.Height = calculatedGridHeight > MaxGridHeight ? MaxGridHeight : calculatedGridHeight;

            GridRectCenterBottom.Width = width * 0.3648;
            GridRectCenterTop.Width = width * 0.3648;

            double calculatedCenterRectangleHeight = (height * 0.2);
            double calculatedCornerRectangleHeight = (height * 0.0781);

            SetHeightOfVerticalCornerRectangles(calculatedCornerRectangleHeight);
            SetHeightOfVerticalCenterRectangles(calculatedCenterRectangleHeight);

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

        public void SetHeightOfVerticalCenterRectangles(double newValue)
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

        public void SetWidthOfHorizontalCenterRectangles(double newValue)
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
            _heightOfRectangle = height;
            _widthOfRectangle = width;
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

            for (int indexWidth = 0; indexWidth < (int)paintingAreaCanvasWidth; indexWidth++)
                for (int indexHeight = 0; indexHeight < (int)paintingAreaCanvasHeight; indexHeight++)
                {
                    if (_pixelData.getPixelAlphaFromCanvas(indexWidth, indexHeight) != 0x00)
                    {
                        extremePoint.X = indexWidth;
                        foundLeftPixel = true;
                        indexWidth = (int)paintingAreaCanvasWidth;
                        indexHeight = (int)paintingAreaCanvasHeight;
                    }
                }
            if (foundLeftPixel)
                for (int indexHeight = 0; indexHeight < (int)paintingAreaCanvasHeight; indexHeight++)
                    for (int indexWidth = (int)paintingAreaCanvasWidth - 1; indexWidth >= (int)extremePoint.X; indexWidth--)
                    {
                        if (_pixelData.getPixelAlphaFromCanvas(indexWidth, indexHeight) != 0x00)
                        {
                            extremePoint.Y = indexHeight;
                            xCoordinateOfExtremeTop = indexWidth;

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
                int yCoordinateOfExtremeRight = 0;
                for (int indexWidth = (int)paintingAreaCanvasWidth - 1; indexWidth >= xCoordinateOfExtremeTop; indexWidth--)
                    for (int indexHeight = (int)paintingAreaCanvasHeight - 1; indexHeight >= extremeLeftAndTopCoordinate.Y; indexHeight--)
                    {
                        if (_pixelData.getPixelAlphaFromCanvas(indexWidth, indexHeight) != 0x00)
                        {
                            extremePoint.X = indexWidth;
                            yCoordinateOfExtremeRight = indexHeight;

                            indexWidth = 0;
                            indexHeight = 0;
                        }
                    }
                for (int indexHeight = (int)paintingAreaCanvasHeight - 1; indexHeight >= yCoordinateOfExtremeRight; indexHeight--)
                    for (int indexWidth = (int)extremePoint.X; indexWidth >= (int)extremeLeftAndTopCoordinate.X; indexWidth--)
                    {
                        if (_pixelData.getPixelAlphaFromCanvas(indexWidth, indexHeight) != 0x00)
                        {
                            extremePoint.Y = indexHeight;
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
            TransformGroup tgPaintingAreaCheckeredGrid = currentPaintApplication.GridWorkingSpace.RenderTransform as TransformGroup;
            if (currentPaintApplication == null || tgPaintingAreaCheckeredGrid == null)
            {
                return;
            }

            // is needed to move the blue selection to the right position
            TranslateTransform ttfMoveCropControl = new TranslateTransform();
            double heightOfpaintingAreaCheckeredGrid = currentPaintApplication.GridWorkingSpace.Height;
            double widthOfPaintingAreaCheckeredGrid = currentPaintApplication.GridWorkingSpace.Width;
            // Calculate the position from crop selection in connection with the working space respectively with the drawing
            // in the working space. In other words the crop selection should be adapted on the drawing in the working space.
            Point extremeLeftAndTopCoordinate = new Point(0.0, 0.0);
            Point extremeRightAndBottomCoordinate = new Point(widthOfPaintingAreaCheckeredGrid - 1.0, heightOfpaintingAreaCheckeredGrid - 1.0);

            bool isThereSomethingDrawn = currentPaintApplication.PaintingAreaCanvas.Children.Count != 0;

            if (isThereSomethingDrawn)
            {
                bool isFoundLeftMostDrawnPixel = false;
                int extremeCoordinateOfTop = 0;
                extremeLeftAndTopCoordinate = GetExtremeLeftAndTopCoordinate(extremeLeftAndTopCoordinate.X, extremeLeftAndTopCoordinate.Y,
                                                                             ref isFoundLeftMostDrawnPixel, ref extremeCoordinateOfTop);
                extremeRightAndBottomCoordinate = GetExtremeRightAndBottomCoordinate(extremeRightAndBottomCoordinate.X, extremeRightAndBottomCoordinate.Y,
                                                                                     extremeLeftAndTopCoordinate, isFoundLeftMostDrawnPixel, extremeCoordinateOfTop);

                // index starts with zero, so we have to add the value one.
                _heightCropControl = (extremeRightAndBottomCoordinate.Y - extremeLeftAndTopCoordinate.Y + 1.0) * scaleValueWorkingSpace + doubleBorderWidthValue;
                _widthCropControl = (extremeRightAndBottomCoordinate.X - extremeLeftAndTopCoordinate.X + 1.0) * scaleValueWorkingSpace + doubleBorderWidthValue;

                if (isWorkingSpaceFlippedHorizontally)
                {
                    ttfMoveCropControl.X = tgPaintingAreaCheckeredGrid.Value.OffsetX + ((widthOfPaintingAreaCheckeredGrid - extremeRightAndBottomCoordinate.X) * scaleValueWorkingSpace);
                }
                else
                {
                    ttfMoveCropControl.X = tgPaintingAreaCheckeredGrid.Value.OffsetX + (extremeLeftAndTopCoordinate.X * scaleValueWorkingSpace);
                }

                if (isWorkingSpaceFlippedVertically)
                {
                    ttfMoveCropControl.Y = tgPaintingAreaCheckeredGrid.Value.OffsetY + ((heightOfpaintingAreaCheckeredGrid - extremeRightAndBottomCoordinate.Y) * scaleValueWorkingSpace);
                }
                else
                {
                    ttfMoveCropControl.Y = tgPaintingAreaCheckeredGrid.Value.OffsetY + (extremeLeftAndTopCoordinate.Y * scaleValueWorkingSpace);
                }
            }
            else
            {
                _heightCropControl = scaleValueWorkingSpace * heightOfpaintingAreaCheckeredGrid + _offsetMargin * 2;
                _widthCropControl = scaleValueWorkingSpace * widthOfPaintingAreaCheckeredGrid + _offsetMargin * 2;

                ttfMoveCropControl.X = tgPaintingAreaCheckeredGrid.Value.OffsetX;
                ttfMoveCropControl.Y = tgPaintingAreaCheckeredGrid.Value.OffsetY;
            }
            _SetLimitsForMovableControlBorder(0, currentPaintApplication, tgPaintingAreaCheckeredGrid);
            SetCropControlPosition(_heightCropControl, _widthCropControl, ttfMoveCropControl);
        }

        private void _calculateAndSetCropControlPositionWith90DegreeRotation(double doubleBorderWidthValue, double scaleValueWorkingSpace, bool isWorkingSpaceFlippedHorizontally, bool isWorkingSpaceFlippedVertically)
        {
            PocketPaintApplication currentPaintApplication = PocketPaintApplication.GetInstance();
            TransformGroup tgPaintingAreaCheckeredGrid = currentPaintApplication.GridWorkingSpace.RenderTransform as TransformGroup;
            if (currentPaintApplication == null || tgPaintingAreaCheckeredGrid == null)
            {
                return;
            }
            TranslateTransform ttfMoveCropControl = new TranslateTransform();
            double heightPaintingAreaCheckeredGrid = currentPaintApplication.GridWorkingSpace.Height;
            double widthPaintingAreaCheckeredGrid = currentPaintApplication.GridWorkingSpace.Width;
            Point extremeLeftAndTopCoordinate = new Point(widthPaintingAreaCheckeredGrid - 1.0, heightPaintingAreaCheckeredGrid - 1.0);
            Point extremeRightAndBottomCoordinate = new Point(0.0, 0.0);

            bool isThereSomethingDrawn = currentPaintApplication.PaintingAreaCanvas.Children.Count != 0;

            if (isThereSomethingDrawn)
            {
                bool isFoundLeftMostDrawnPixel = false;
                int xCoordinateOfExtremeTop = 0;
                extremeLeftAndTopCoordinate = GetExtremeLeftAndTopCoordinate(extremeLeftAndTopCoordinate.X, extremeLeftAndTopCoordinate.Y,
                                                                             ref isFoundLeftMostDrawnPixel, ref xCoordinateOfExtremeTop);
                extremeRightAndBottomCoordinate = GetExtremeRightAndBottomCoordinate(extremeRightAndBottomCoordinate.X, extremeRightAndBottomCoordinate.Y,
                                                                                     extremeLeftAndTopCoordinate, isFoundLeftMostDrawnPixel, xCoordinateOfExtremeTop);

                _heightCropControl = (extremeRightAndBottomCoordinate.X - extremeLeftAndTopCoordinate.X + 1.0) * scaleValueWorkingSpace + doubleBorderWidthValue;
                _widthCropControl = (extremeRightAndBottomCoordinate.Y - extremeLeftAndTopCoordinate.Y + 1.0) * scaleValueWorkingSpace + doubleBorderWidthValue;

                double workingSpaceWidth = scaleValueWorkingSpace * heightPaintingAreaCheckeredGrid;
                double positionXRightTopCornerWorkingSpace = tgPaintingAreaCheckeredGrid.Value.OffsetX;
                double positionXLeftTopCornerWorkingSpace = positionXRightTopCornerWorkingSpace - workingSpaceWidth;

                if (isWorkingSpaceFlippedHorizontally)
                {
                    ttfMoveCropControl.X = positionXLeftTopCornerWorkingSpace + (extremeLeftAndTopCoordinate.Y * scaleValueWorkingSpace);
                }
                else
                {
                    ttfMoveCropControl.X = positionXLeftTopCornerWorkingSpace + ((heightPaintingAreaCheckeredGrid - (extremeRightAndBottomCoordinate.Y + 1.0)) * scaleValueWorkingSpace);
                }

                if (isWorkingSpaceFlippedVertically)
                {
                    ttfMoveCropControl.Y = tgPaintingAreaCheckeredGrid.Value.OffsetY + ((widthPaintingAreaCheckeredGrid - extremeRightAndBottomCoordinate.X) * scaleValueWorkingSpace);
                }
                else
                {
                    ttfMoveCropControl.Y = tgPaintingAreaCheckeredGrid.Value.OffsetY + (extremeLeftAndTopCoordinate.X * scaleValueWorkingSpace);
                }
            }
            else
            {
                _heightCropControl = scaleValueWorkingSpace * widthPaintingAreaCheckeredGrid + doubleBorderWidthValue;
                _widthCropControl = scaleValueWorkingSpace * heightPaintingAreaCheckeredGrid + doubleBorderWidthValue;
                double widthOfWorkingSpace = scaleValueWorkingSpace * heightPaintingAreaCheckeredGrid;

                double positionXRightTopCornerWorkingSpace = tgPaintingAreaCheckeredGrid.Value.OffsetX;
                double positionXLeftTopCornerWorkingSpace = positionXRightTopCornerWorkingSpace - widthOfWorkingSpace;

                ttfMoveCropControl.X = positionXLeftTopCornerWorkingSpace;
                ttfMoveCropControl.Y = tgPaintingAreaCheckeredGrid.Value.OffsetY;
            }
            _SetLimitsForMovableControlBorder(90, currentPaintApplication, tgPaintingAreaCheckeredGrid);
            SetCropControlPosition(_heightCropControl, _widthCropControl, ttfMoveCropControl);
        }

        private void _calculateAndSetCropControlPositionWith180DegreeRotation(double doubleBorderWidthValue, double scaleValueWorkingSpace, bool isWorkingSpaceFlippedHorizontally, bool isWorkingSpaceFlippedVertically)
        {
            PocketPaintApplication currentPaintApplication = PocketPaintApplication.GetInstance();
            TransformGroup tgPaintingAreaCheckeredGrid = currentPaintApplication.GridWorkingSpace.RenderTransform as TransformGroup;
            if (currentPaintApplication == null || tgPaintingAreaCheckeredGrid == null)
            {
                return;
            }
            TranslateTransform ttfMoveCropControl = new TranslateTransform();
            double heightPaintingAreaCheckeredGrid = currentPaintApplication.GridWorkingSpace.Height;
            double widthPaintingAreaCheckeredGrid = currentPaintApplication.GridWorkingSpace.Width;

            Point extremeLeftAndTopCoordinate = new Point(widthPaintingAreaCheckeredGrid - 1.0, 0.0);
            Point extremeRightAndBottomCoordinate = new Point(0.0, heightPaintingAreaCheckeredGrid - 1.0);

            bool isThereSomethingDrawn = currentPaintApplication.PaintingAreaCanvas.Children.Count != 0;

            if (isThereSomethingDrawn)
            {
                bool isFoundLeftMostDrawnPixel = false;
                int xCoordinateOfExtremeTop = 0;
                extremeLeftAndTopCoordinate = GetExtremeLeftAndTopCoordinate(extremeLeftAndTopCoordinate.X, extremeLeftAndTopCoordinate.Y,
                                                                             ref isFoundLeftMostDrawnPixel, ref xCoordinateOfExtremeTop);
                extremeRightAndBottomCoordinate = GetExtremeRightAndBottomCoordinate(extremeRightAndBottomCoordinate.X, extremeRightAndBottomCoordinate.Y,
                                                                                     extremeLeftAndTopCoordinate, isFoundLeftMostDrawnPixel, xCoordinateOfExtremeTop);

                _heightCropControl = (extremeRightAndBottomCoordinate.Y - extremeLeftAndTopCoordinate.Y + 1.0) * scaleValueWorkingSpace + doubleBorderWidthValue;
                _widthCropControl = (extremeRightAndBottomCoordinate.X - extremeLeftAndTopCoordinate.X + 1.0) * scaleValueWorkingSpace + doubleBorderWidthValue;

                double heightOfWorkingSpace = scaleValueWorkingSpace * heightPaintingAreaCheckeredGrid;
                double widthOfWoringSpace = scaleValueWorkingSpace * widthPaintingAreaCheckeredGrid;

                double positionXRightBottomCornerWorkingSpace = tgPaintingAreaCheckeredGrid.Value.OffsetX;
                double positionXLeftBottomCornerWorkingSpace = positionXRightBottomCornerWorkingSpace - widthOfWoringSpace;
                double positionYRigthBottomCornerWorkingSpace = tgPaintingAreaCheckeredGrid.Value.OffsetY;
                double positionYRightTopCornerWorkingSpace = positionYRigthBottomCornerWorkingSpace - heightOfWorkingSpace;

                if (isWorkingSpaceFlippedHorizontally)
                {
                    ttfMoveCropControl.X = positionXLeftBottomCornerWorkingSpace + (extremeLeftAndTopCoordinate.X * scaleValueWorkingSpace);
                }
                else
                {
                    ttfMoveCropControl.X = positionXLeftBottomCornerWorkingSpace + ((widthPaintingAreaCheckeredGrid - extremeRightAndBottomCoordinate.X) * scaleValueWorkingSpace);
                }

                if (isWorkingSpaceFlippedVertically)
                {
                    ttfMoveCropControl.Y = positionYRightTopCornerWorkingSpace + (extremeLeftAndTopCoordinate.Y * scaleValueWorkingSpace);
                }
                else
                {
                    ttfMoveCropControl.Y = positionYRightTopCornerWorkingSpace + (((int)heightPaintingAreaCheckeredGrid - (extremeRightAndBottomCoordinate.Y + 1.0)) * scaleValueWorkingSpace);
                }
            }
            else
            {
                _heightCropControl = scaleValueWorkingSpace * heightPaintingAreaCheckeredGrid + doubleBorderWidthValue;
                _widthCropControl = scaleValueWorkingSpace * widthPaintingAreaCheckeredGrid + doubleBorderWidthValue;
                double heightOfWorkingSpace = scaleValueWorkingSpace * heightPaintingAreaCheckeredGrid;
                double widthOfWorkingSpace = scaleValueWorkingSpace * widthPaintingAreaCheckeredGrid;

                double positionXRightBottomCornerWorkingSpace = tgPaintingAreaCheckeredGrid.Value.OffsetX;
                double positionXLeftBottomCornerWorkingSpace = positionXRightBottomCornerWorkingSpace - widthOfWorkingSpace;
                double positionYRigthBottomCornerWorkingSpace = tgPaintingAreaCheckeredGrid.Value.OffsetY;
                double positionYRightTopCornerWorkingSpace = positionYRigthBottomCornerWorkingSpace - heightOfWorkingSpace;
                ttfMoveCropControl.X = positionXLeftBottomCornerWorkingSpace;
                ttfMoveCropControl.Y = positionYRightTopCornerWorkingSpace;
            }
            _SetLimitsForMovableControlBorder(180, currentPaintApplication, tgPaintingAreaCheckeredGrid);
            SetCropControlPosition(_heightCropControl, _widthCropControl, ttfMoveCropControl);
        }

        private void _calculateAndSetCropControlPositionWith270DegreeRotation(double doubleBorderWidthValue, double scaleValueWorkingSpace, bool isWorkingSpaceFlippedHorizontally, bool isWorkingSpaceFlippedVertically)
        {
            PocketPaintApplication currentPaintApplication = PocketPaintApplication.GetInstance();
            TransformGroup tgPaintingAreaCheckeredGrid = currentPaintApplication.GridWorkingSpace.RenderTransform as TransformGroup;
            if (currentPaintApplication == null || tgPaintingAreaCheckeredGrid == null)
            {
                return;
            }
            TranslateTransform ttfMoveCropControl = new TranslateTransform();
            double heightOfPaintingAreaCheckeredGrid = currentPaintApplication.GridWorkingSpace.Height;
            double widthOfPaintingAreaCheckeredGrid = currentPaintApplication.GridWorkingSpace.Width;

            Point extremeLeftAndTopCoordinate = new Point(widthOfPaintingAreaCheckeredGrid - 1.0, heightOfPaintingAreaCheckeredGrid - 1.0);
            Point extremeRightAndBottomCoordinate = new Point(0.0, 0.0);

            bool isThereSomethingDrawn = currentPaintApplication.PaintingAreaCanvas.Children.Count != 0;

            if (isThereSomethingDrawn)
            {
                bool isFoundLeftMostDrawnPixel = false;
                int xCoordinateOfExtremeTop = 0;
                extremeLeftAndTopCoordinate = GetExtremeLeftAndTopCoordinate(extremeLeftAndTopCoordinate.X, extremeLeftAndTopCoordinate.Y,
                                                                             ref isFoundLeftMostDrawnPixel, ref xCoordinateOfExtremeTop);
                extremeRightAndBottomCoordinate = GetExtremeRightAndBottomCoordinate(extremeRightAndBottomCoordinate.X, extremeRightAndBottomCoordinate.Y,
                                                                                     extremeLeftAndTopCoordinate, isFoundLeftMostDrawnPixel, xCoordinateOfExtremeTop);

                _heightCropControl = (extremeRightAndBottomCoordinate.X - extremeLeftAndTopCoordinate.X + 1.0) * scaleValueWorkingSpace + doubleBorderWidthValue;
                _widthCropControl = (extremeRightAndBottomCoordinate.Y - extremeLeftAndTopCoordinate.Y + 1.0) * scaleValueWorkingSpace + doubleBorderWidthValue;
                double workingSpaceHeight = scaleValueWorkingSpace * widthOfPaintingAreaCheckeredGrid;
                double positionYLeftBottomCornerWorkingSpace = tgPaintingAreaCheckeredGrid.Value.OffsetY;
                double positionYLeftTopCornerWorkingSpace = positionYLeftBottomCornerWorkingSpace - workingSpaceHeight;

                if (isWorkingSpaceFlippedHorizontally)
                {
                    ttfMoveCropControl.X = tgPaintingAreaCheckeredGrid.Value.OffsetX + ((heightOfPaintingAreaCheckeredGrid - extremeRightAndBottomCoordinate.Y) * scaleValueWorkingSpace);
                }
                else
                {
                    ttfMoveCropControl.X = tgPaintingAreaCheckeredGrid.Value.OffsetX + (extremeLeftAndTopCoordinate.Y * scaleValueWorkingSpace);
                }

                if (isWorkingSpaceFlippedVertically)
                {
                    ttfMoveCropControl.Y = positionYLeftTopCornerWorkingSpace + (extremeLeftAndTopCoordinate.X * scaleValueWorkingSpace);
                }
                else
                {
                    ttfMoveCropControl.Y = positionYLeftTopCornerWorkingSpace + ((widthOfPaintingAreaCheckeredGrid - (extremeRightAndBottomCoordinate.X + 1.0)) * scaleValueWorkingSpace);
                }
            }
            else
            {
                _heightCropControl = tgPaintingAreaCheckeredGrid.Value.M21 * widthOfPaintingAreaCheckeredGrid + doubleBorderWidthValue;
                _widthCropControl = tgPaintingAreaCheckeredGrid.Value.M21 * heightOfPaintingAreaCheckeredGrid + doubleBorderWidthValue;
                double workingSpaceHeight = tgPaintingAreaCheckeredGrid.Value.M21 * widthOfPaintingAreaCheckeredGrid;
                double positionYLeftBottomCornerWorkingSpace = tgPaintingAreaCheckeredGrid.Value.OffsetY;
                double positionYLeftTopCornerWorkingSpace = positionYLeftBottomCornerWorkingSpace - workingSpaceHeight;

                ttfMoveCropControl.X = tgPaintingAreaCheckeredGrid.Value.OffsetX;
                ttfMoveCropControl.Y = positionYLeftTopCornerWorkingSpace;
            }
            _SetLimitsForMovableControlBorder(270, currentPaintApplication, tgPaintingAreaCheckeredGrid);
            SetCropControlPosition(_heightCropControl, _widthCropControl, ttfMoveCropControl);
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

            TransformGroup paintingAreaCheckeredGridTransformGroup = currentPaintApplication.GridWorkingSpace.RenderTransform as TransformGroup;
            // Das Selection-Control soll die Zeichnung einschließen und nicht darauf liegen. Daher wird
            // dieser Wert mit 10 verwendet. Anschließend wird dann die Margin Left und Top, um 5 verringert.
            double doubleBorderWidthValue = _offsetMargin * 2.0;

            _transformGridMain.Children.Clear();
            GridMain.Margin = new Thickness(-5.0, -5.0, 0.0, 0.0);

            bool isWorkingSpaceFlippedHorizontally = paintingAreaCheckeredGridTransformGroup != null && paintingAreaCheckeredGridTransformGroup.Value.M11 == -1.0;
            bool isWorkingSpaceFlippedVertically = paintingAreaCheckeredGridTransformGroup != null && paintingAreaCheckeredGridTransformGroup.Value.M22 == -1.0;

            if (currentPaintApplication.angularDegreeOfWorkingSpaceRotation == 0)
            {
                _scaleValueWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.M11;
                _calculateAndSetCropControlPositionWithoutRotating(doubleBorderWidthValue, _scaleValueWorkingSpace, isWorkingSpaceFlippedHorizontally, isWorkingSpaceFlippedVertically);
            }
            else if (currentPaintApplication.angularDegreeOfWorkingSpaceRotation == 90)
            {
                // Attention: Working space is rotated 90°
                _scaleValueWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.M12;
                _calculateAndSetCropControlPositionWith90DegreeRotation(doubleBorderWidthValue, _scaleValueWorkingSpace, isWorkingSpaceFlippedHorizontally, isWorkingSpaceFlippedVertically);
            }
            else if (currentPaintApplication.angularDegreeOfWorkingSpaceRotation == 180)
            {
                // Attention: Working space is rotated 180°
                _scaleValueWorkingSpace = Math.Abs(paintingAreaCheckeredGridTransformGroup.Value.M11);
                _calculateAndSetCropControlPositionWith180DegreeRotation(doubleBorderWidthValue, _scaleValueWorkingSpace, isWorkingSpaceFlippedHorizontally, isWorkingSpaceFlippedVertically);
            }
            else if (currentPaintApplication.angularDegreeOfWorkingSpaceRotation == 270)
            {
                _scaleValueWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.M21;
                // Attention: Working space is rotated 270°
                _calculateAndSetCropControlPositionWith270DegreeRotation(doubleBorderWidthValue, _scaleValueWorkingSpace, isWorkingSpaceFlippedHorizontally, isWorkingSpaceFlippedVertically);
            }
            currentPaintApplication.ProgressRing.IsActive = false;
        }

        private void _SetLimitsForMovableControlBorder(uint rotatedValue, PocketPaintApplication currentPaintApplication, TransformGroup tgPaintingAreaCheckeredGrid)
        {
            double paintingAreaCheckeredGridHeight = currentPaintApplication.GridWorkingSpace.Height;
            double paintingAreaCheckeredGridWidth = currentPaintApplication.GridWorkingSpace.Width;

            if (rotatedValue == 0)
            {
                _limitLeft = tgPaintingAreaCheckeredGrid.Value.OffsetX - _offsetMargin;
                _limitTop = tgPaintingAreaCheckeredGrid.Value.OffsetY - _offsetMargin;

                _limitBottom = _limitTop + (paintingAreaCheckeredGridHeight * _scaleValueWorkingSpace) + _offsetMargin * 2;
                _limitRight = _limitLeft + (paintingAreaCheckeredGridWidth * _scaleValueWorkingSpace) + _offsetMargin * 2;
            }
            else if (rotatedValue == 90)
            {
                _limitTop = tgPaintingAreaCheckeredGrid.Value.OffsetY - _offsetMargin;
                _limitBottom = _limitTop + (paintingAreaCheckeredGridWidth * _scaleValueWorkingSpace) + _offsetMargin * 2;
                _limitRight = tgPaintingAreaCheckeredGrid.Value.OffsetX + _offsetMargin;
                _limitLeft = _limitRight - (paintingAreaCheckeredGridHeight * _scaleValueWorkingSpace) - _offsetMargin * 2;
            }
            else if (rotatedValue == 180)
            {
                _limitRight = tgPaintingAreaCheckeredGrid.Value.OffsetX + _offsetMargin;
                _limitBottom = tgPaintingAreaCheckeredGrid.Value.OffsetY + _offsetMargin;
                _limitTop = _limitBottom - (paintingAreaCheckeredGridHeight * _scaleValueWorkingSpace) - _offsetMargin * 2;
                _limitLeft = _limitRight - (paintingAreaCheckeredGridWidth * _scaleValueWorkingSpace) - _offsetMargin * 2;
            }
            else if (rotatedValue == 270)
            {
                _limitBottom = tgPaintingAreaCheckeredGrid.Value.OffsetY + _offsetMargin;
                _limitTop = _limitBottom - (paintingAreaCheckeredGridWidth * _scaleValueWorkingSpace) - _offsetMargin * 2;
                _limitLeft = tgPaintingAreaCheckeredGrid.Value.OffsetX - _offsetMargin;
                _limitRight = _limitLeft + (paintingAreaCheckeredGridHeight * _scaleValueWorkingSpace) + _offsetMargin * 2;
            }
        }

        private TranslateTransform CreateTranslateTransform(double x, double y)
        {
            TranslateTransform move = new TranslateTransform { X = x, Y = y };
            return move;
        }

        private void rectCenterBottom_ManipulationDelta_1(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleCropSelection.Height + e.Delta.Translation.Y) >= MinRectangleMoveHeight)
            {
                var moveY = CreateTranslateTransform(0.0, e.Delta.Translation.Y);
                ChangeHeightOfUiElements(moveY.Y);
                ChangeMarginBottomOfUiElements(moveY.Y);
            }
        }

        private void rectCenterTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleCropSelection.Height + (e.Delta.Translation.Y * -1)) >= MinRectangleMoveHeight)
            {
                var moveY = CreateTranslateTransform(0.0, e.Delta.Translation.Y);
                moveY.Y *= -1.0;
                ChangeHeightOfUiElements(moveY.Y);
                ChangeMarginTopOfUiElements(moveY.Y);
            }
        }

        private void rectLeftBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleCropSelection.Width + (e.Delta.Translation.X * -1)) >= MinRectangleMoveWidth &&
               (rectRectangleCropSelection.Height + e.Delta.Translation.Y) >= MinRectangleMoveHeight)
            {
                var moveX = CreateTranslateTransform((e.Delta.Translation.X * -1.0), 0.0).X;
                ChangeWidthOfUiElements(moveX);
                ChangeMarginLeftOfUiElements(moveX);

                var moveY = CreateTranslateTransform(0.0, (e.Delta.Translation.Y)).Y;
                ChangeHeightOfUiElements(moveY);
                ChangeMarginBottomOfUiElements(moveY);
            }
        }

        private void rectLeftCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleCropSelection.Width + (e.Delta.Translation.X * -1)) >= MinRectangleMoveWidth)
            {
                var moveX = CreateTranslateTransform((e.Delta.Translation.X), 0.0).X * -1.0;
                ChangeWidthOfUiElements(moveX);
                ChangeMarginLeftOfUiElements(moveX);
            }
        }

        private void rectLeftTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleCropSelection.Width + (e.Delta.Translation.X * -1)) >= MinRectangleMoveWidth &&
                (rectRectangleCropSelection.Height + (e.Delta.Translation.Y * -1)) >= MinRectangleMoveHeight)
            {
                // left
                var moveX = CreateTranslateTransform((e.Delta.Translation.X), 0.0).X * -1.0;
                ChangeWidthOfUiElements(moveX);
                ChangeMarginLeftOfUiElements(moveX);

                // top
                var moveY = CreateTranslateTransform(0.0, (e.Delta.Translation.Y)).Y * -1;
                ChangeHeightOfUiElements(moveY);
                ChangeMarginTopOfUiElements(moveY);
            }
        }

        private void rectRightBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleCropSelection.Width + e.Delta.Translation.X) >= MinRectangleMoveWidth &&
                (rectRectangleCropSelection.Height + e.Delta.Translation.Y) >= MinRectangleMoveHeight)
            {
                var moveX = CreateTranslateTransform((e.Delta.Translation.X), 0.0).X;
                var moveY = CreateTranslateTransform(0.0, (e.Delta.Translation.Y)).Y;

                ChangeWidthOfUiElements(moveX);
                ChangeMarginRightOfUiElements(moveX);

                ChangeHeightOfUiElements(moveY);
                ChangeMarginBottomOfUiElements(moveY);
            }
        }

        private void rectRightCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleCropSelection.Width + e.Delta.Translation.X) >= MinRectangleMoveWidth)
            {
                var moveX = CreateTranslateTransform((e.Delta.Translation.X), 0.0).X;
                ChangeWidthOfUiElements(moveX);
                ChangeMarginRightOfUiElements(moveX);
            }
        }

        private void rectRightTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleCropSelection.Width + e.Delta.Translation.X) >= MinRectangleMoveWidth &&
               (rectRectangleCropSelection.Height + (e.Delta.Translation.Y * -1)) >= MinRectangleMoveHeight)
            {
                var moveX = CreateTranslateTransform((e.Delta.Translation.X), 0.0).X;          
                ChangeWidthOfUiElements(moveX);
                ChangeMarginRightOfUiElements(moveX);

                var moveY = CreateTranslateTransform(0.0, (e.Delta.Translation.Y)).Y * -1.0;
                ChangeHeightOfUiElements(moveY);
                ChangeMarginTopOfUiElements(moveY);
            }
        }

        private void ChangeHeightOfUiElements(double value)
        {
            GridMain.Height += value;
            _heightOfRectangle += value;
            rectRectangleCropSelection.Height += value;

            double addValueToTouchGrid = value / 4.0;

            ResetAppBarButtonRectangleSelectionControl(true);
            SetIsModifiedRectangleMovement = true;
        }

        private void ChangeWidthOfUiElements(double value)
        {
            GridMain.Width += value;
            _widthOfRectangle += value;
            rectRectangleCropSelection.Width += value;

            double addValueToTouchGrid = value / 4.0;
            if ((rectCenterTop.Width + addValueToTouchGrid) >= 5.0 || (rectCenterTop.Width + addValueToTouchGrid) <= 120.0)
            {
                GridRectCenterTop.Width += addValueToTouchGrid;
                rectCenterTop.Width += addValueToTouchGrid;

                GridRectCenterBottom.Width += addValueToTouchGrid;
                rectCenterBottom.Width += addValueToTouchGrid;
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
            var move = new TranslateTransform();
            move.X = e.Delta.Translation.X;
            move.Y = e.Delta.Translation.Y;

            _transformGridMain.Children.Add(move);
            ResetAppBarButtonRectangleSelectionControl(true);
            SetIsModifiedRectangleMovement = true;
        }

        private void rectRectangleCropSelection_Tapped(object sender, TappedRoutedEventArgs e)
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

        public bool HasPaintingAreaViewElements()
        {
            bool result = false;
            if (PocketPaintApplication.GetInstance().PaintingAreaCanvas != null)
            {
                result = PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count > 0;
            }
            return result;
        }

        public double GetHeightOfRectangleCropSelection()
        {
            return (_heightOfRectangle - 10.0) / _scaleValueWorkingSpace;
        }

        public double GetWidthOfRectangleCropSelection()
        {
            return (_widthOfRectangle - 10.0) / _scaleValueWorkingSpace;
        }

        public Point GetXYOffsetBetweenPaintingAreaAndCropControlSelection()
        {
            TransformGroup tgPaintingAreaCheckeredGrid = PocketPaintApplication.GetInstance().GridWorkingSpace.RenderTransform as TransformGroup;
            PocketPaintApplication currentPaintApplication = PocketPaintApplication.GetInstance();
            if (currentPaintApplication == null || tgPaintingAreaCheckeredGrid == null)
            {
                return new Point(0.0, 0.0);
            }

            double doubleMargin = 10.0;
            if (currentPaintApplication.angularDegreeOfWorkingSpaceRotation == 0)
            {
                double offsetX = ((_transformGridMain.Value.OffsetX + 5.0 + GridMain.Margin.Left) - tgPaintingAreaCheckeredGrid.Value.OffsetX) / 0.75;
                double offsetY = ((_transformGridMain.Value.OffsetY + 5.0 + GridMain.Margin.Top) - tgPaintingAreaCheckeredGrid.Value.OffsetY) / 0.75;
                return new Point(Math.Ceiling(offsetX), Math.Ceiling(offsetY));
            }
            else if (currentPaintApplication.angularDegreeOfWorkingSpaceRotation == 90)
            {
                double offsetX = (_transformGridMain.Value.OffsetY + 5.0 + GridMain.Margin.Top - tgPaintingAreaCheckeredGrid.Value.OffsetY) / _scaleValueWorkingSpace;
                double offsetY = (tgPaintingAreaCheckeredGrid.Value.OffsetX - (_transformGridMain.Value.OffsetX - doubleMargin + _widthCropControl) + GridMain.Margin.Right) / _scaleValueWorkingSpace;
                return new Point(offsetX, offsetY);
            }
            else if (currentPaintApplication.angularDegreeOfWorkingSpaceRotation == 180)
            {
                double offsetX = (tgPaintingAreaCheckeredGrid.Value.OffsetX - (_transformGridMain.Value.OffsetX + _widthCropControl - doubleMargin) + GridMain.Margin.Right) / 0.75;
                double offsetY = (tgPaintingAreaCheckeredGrid.Value.OffsetY - (_transformGridMain.Value.OffsetY + _heightCropControl - doubleMargin) + GridMain.Margin.Bottom) / _scaleValueWorkingSpace;
                return new Point(Math.Ceiling(offsetX), Math.Ceiling(offsetY));
            }
            else if (currentPaintApplication.angularDegreeOfWorkingSpaceRotation == 270)
            {
                double offsetX = (tgPaintingAreaCheckeredGrid.Value.OffsetY + GridMain.Margin.Bottom - (_transformGridMain.Value.OffsetY + _heightCropControl - doubleMargin)) / _scaleValueWorkingSpace;
                double offsetY = (_transformGridMain.Value.OffsetX - tgPaintingAreaCheckeredGrid.Value.OffsetX + 5.0 + GridMain.Margin.Left) / _scaleValueWorkingSpace;
                return new Point(offsetX, offsetY);
            }
            else
            {
                return new Point(0, 0);
            }
        }

        private void addImageToPaintingAreaCanvas(Image image)
        {
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(image);
        }

        private void setSizeOfPaintingAreaCanvas(double height, double width)
        {
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Height = height;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Width = width;
        }

        public void AddWriteableBitmapToCanvas(WriteableBitmap writeableBitmapToAdd, PocketPaintApplication currentPaintApplication)
        {
            TransformGroup tgPaintingAreaCheckeredGrid = PocketPaintApplication.GetInstance().GridWorkingSpace.RenderTransform as TransformGroup;
            int height = (int)Math.Ceiling(GetHeightOfRectangleCropSelection());
            int width = (int)Math.Ceiling(GetWidthOfRectangleCropSelection());
            Point leftTopRectangleCropSelection = GetXYOffsetBetweenPaintingAreaAndCropControlSelection();

            Image image = new Image();

            if (currentPaintApplication.angularDegreeOfWorkingSpaceRotation == 0)
            {
                int heightOfcroppedWorkingSpacePicture = writeableBitmapToAdd.PixelHeight;
                int widthOfcroppedWorkingSpacePicture = writeableBitmapToAdd.PixelWidth;
                image.Source = writeableBitmapToAdd;
                image.Height = heightOfcroppedWorkingSpacePicture;
                image.Width = widthOfcroppedWorkingSpacePicture;

                addImageToPaintingAreaCanvas(image);
                currentPaintApplication.PaintingAreaView.setSizeOfPaintingAreaViewCheckered(heightOfcroppedWorkingSpacePicture, widthOfcroppedWorkingSpacePicture);
                currentPaintApplication.PaintingAreaView.alignPositionOfGridWorkingSpace(null);
                setSizeOfPaintingAreaCanvas(heightOfcroppedWorkingSpacePicture, widthOfcroppedWorkingSpacePicture);
            }
            else if (currentPaintApplication.angularDegreeOfWorkingSpaceRotation == 90)
            {
                image.Source = writeableBitmapToAdd;
                image.Height = writeableBitmapToAdd.PixelHeight;
                image.Width = writeableBitmapToAdd.PixelWidth;

                addImageToPaintingAreaCanvas(image);
                currentPaintApplication.PaintingAreaView.setSizeOfPaintingAreaViewCheckered(writeableBitmapToAdd.PixelHeight, writeableBitmapToAdd.PixelWidth);
                currentPaintApplication.PaintingAreaView.alignPositionOfGridWorkingSpace(null);
                setSizeOfPaintingAreaCanvas(writeableBitmapToAdd.PixelHeight, writeableBitmapToAdd.PixelWidth);
            }
            else if (currentPaintApplication.angularDegreeOfWorkingSpaceRotation == 180)
            {
                int heigthOfcroppedWorkingSpacePicture = writeableBitmapToAdd.PixelHeight;
                int widthOfcroppedWorkingSpacePicture = writeableBitmapToAdd.PixelWidth;
                image.Source = writeableBitmapToAdd;
                image.Height = heigthOfcroppedWorkingSpacePicture;
                image.Width = widthOfcroppedWorkingSpacePicture;

                addImageToPaintingAreaCanvas(image);
                currentPaintApplication.PaintingAreaView.setSizeOfPaintingAreaViewCheckered(writeableBitmapToAdd.PixelHeight, writeableBitmapToAdd.PixelWidth);
                currentPaintApplication.PaintingAreaView.alignPositionOfGridWorkingSpace(null);
                setSizeOfPaintingAreaCanvas(heigthOfcroppedWorkingSpacePicture, widthOfcroppedWorkingSpacePicture);
            }
            else if (currentPaintApplication.angularDegreeOfWorkingSpaceRotation == 270)
            {
                image.Source = writeableBitmapToAdd;
                image.Height = writeableBitmapToAdd.PixelHeight;
                image.Width = writeableBitmapToAdd.PixelWidth;

                addImageToPaintingAreaCanvas(image);
                currentPaintApplication.PaintingAreaView.setSizeOfPaintingAreaViewCheckered(writeableBitmapToAdd.PixelHeight, writeableBitmapToAdd.PixelWidth);
                currentPaintApplication.PaintingAreaView.alignPositionOfGridWorkingSpace(null);
                setSizeOfPaintingAreaCanvas(writeableBitmapToAdd.PixelHeight, writeableBitmapToAdd.PixelWidth);
            }
            currentPaintApplication.CropControl.SetCropSelection();
        }

        private bool CheckIfCropSelctionIsChanged(PocketPaintApplication currentApplication, TransformGroup tgPaintingAreaCheckeredGrid)
        {
            bool result = false;
            Point currentLeftTopCoordinateOfCropSelection = GetXYOffsetBetweenPaintingAreaAndCropControlSelection();
            int heightOfCropSelection = 0;
            int widthOfCropSelection = 0;
            bool isWorkingSpaceNotRotated = tgPaintingAreaCheckeredGrid.Value.M11 > 0.0;
            bool isWorkingSpaceRotated90Degree = tgPaintingAreaCheckeredGrid.Value.M12 > 0.0;
            bool isWorkingSpaceRotated180Degree = tgPaintingAreaCheckeredGrid.Value.M11 < 0.0;
            bool isWorkingSpaceRotated270Degree = tgPaintingAreaCheckeredGrid.Value.M12 < 0.0;

            heightOfCropSelection = isWorkingSpaceNotRotated || isWorkingSpaceRotated180Degree ? (int)(currentApplication.CropControl.GetHeightOfRectangleCropSelection())
                                                             : (int)Math.Ceiling(currentApplication.CropControl.GetWidthOfRectangleCropSelection());

            widthOfCropSelection = isWorkingSpaceNotRotated || isWorkingSpaceRotated180Degree ? (int)(currentApplication.CropControl.GetWidthOfRectangleCropSelection())
                                                            : (int)(currentApplication.CropControl.GetHeightOfRectangleCropSelection());

            if ((heightOfCropSelection != (int)currentApplication.PaintingAreaCanvas.Height)
                    || (widthOfCropSelection != (int)currentApplication.PaintingAreaCanvas.Width))
            {
                result = true;
            }
            return result;
        }

        async public void CropImage()
        {
            // needs a little bit more storage but the performance is also little bit better.
            PocketPaintApplication currentApplication = PocketPaintApplication.GetInstance();
            TransformGroup tgPaintingAreaCheckeredGrid = PocketPaintApplication.GetInstance().GridWorkingSpace.RenderTransform as TransformGroup;

            if (currentApplication == null || tgPaintingAreaCheckeredGrid == null || !CheckIfCropSelctionIsChanged(currentApplication, tgPaintingAreaCheckeredGrid))
            {
                return;
            }

            Point currentLeftTopCoordinateOfCropSelection = GetXYOffsetBetweenPaintingAreaAndCropControlSelection();
            int heightOfCropSelection = (int)currentApplication.CropControl.GetHeightOfRectangleCropSelection();
            int widthOfCropSelection = (int)currentApplication.CropControl.GetWidthOfRectangleCropSelection();

            WriteableBitmap wbCroppedBitmap = null;

            string filename = ("karlidavidtest") + ".png";
            await currentApplication.StorageIo.WriteBitmapToPngMediaLibrary(filename);
            StorageFile sfSavedPictureFromWorkingSpace = await KnownFolders.PicturesLibrary.GetFileAsync(filename);
            InMemoryRandomAccessStream mrAccessStream = new InMemoryRandomAccessStream();

            using (Stream streamOfSavedPictureFromWorkingSpace = await sfSavedPictureFromWorkingSpace.OpenStreamForReadAsync())
            {
                using (var memStream = new MemoryStream())
                {
                    await streamOfSavedPictureFromWorkingSpace.CopyToAsync(memStream);
                    memStream.Position = 0;

                    BitmapDecoder bitmapDecoder = await BitmapDecoder.CreateAsync(memStream.AsRandomAccessStream());
                    BitmapEncoder bitmapEncoder = await BitmapEncoder.CreateForTranscodingAsync(mrAccessStream, bitmapDecoder);

                    bitmapEncoder.BitmapTransform.ScaledHeight = (uint)currentApplication.PaintingAreaCanvas.RenderSize.Height;
                    bitmapEncoder.BitmapTransform.ScaledWidth = (uint)currentApplication.PaintingAreaCanvas.RenderSize.Width;

                    uint canvasHeight = (uint)currentApplication.PaintingAreaCanvas.Height;
                    uint canvasWidth = (uint)currentApplication.PaintingAreaCanvas.Width;
                    BitmapBounds bitmapBoundsOfDrawing = new BitmapBounds();

                    if (currentApplication.angularDegreeOfWorkingSpaceRotation == 0)
                    {
                        // bitmapBounds starts with index zero. So we have to substract the height and width with value one.
                        bitmapBoundsOfDrawing.Height = (uint)heightOfCropSelection - 1;
                        bitmapBoundsOfDrawing.Width = (uint)widthOfCropSelection - 1;
                        uint uwidthOfCropSelection = (uint)widthOfCropSelection;
                        uint uheightOfCropSelection = (uint)heightOfCropSelection;
                        bitmapBoundsOfDrawing.X = (uwidthOfCropSelection + (uint)currentLeftTopCoordinateOfCropSelection.X) > canvasWidth ? canvasWidth - uwidthOfCropSelection : (uint)currentLeftTopCoordinateOfCropSelection.X;
                        bitmapBoundsOfDrawing.Y = ((uint)heightOfCropSelection + (uint)currentLeftTopCoordinateOfCropSelection.Y) > canvasHeight ? canvasHeight - uheightOfCropSelection : (uint)currentLeftTopCoordinateOfCropSelection.Y;
                        wbCroppedBitmap = new WriteableBitmap(widthOfCropSelection, heightOfCropSelection);
                    }
                    else if (currentApplication.angularDegreeOfWorkingSpaceRotation == 90)
                    {
                        bitmapBoundsOfDrawing.Height = (uint)widthOfCropSelection - 1;
                        bitmapBoundsOfDrawing.Width = (uint)heightOfCropSelection - 1;
                        uint uwidthOfCropSelection = (uint)widthOfCropSelection;
                        uint uheightOfCropSelection = (uint)heightOfCropSelection;
                        bitmapBoundsOfDrawing.X = ((uint)heightOfCropSelection + (uint)currentLeftTopCoordinateOfCropSelection.X) > canvasWidth ? canvasWidth - uheightOfCropSelection : (uint)currentLeftTopCoordinateOfCropSelection.X;
                        bitmapBoundsOfDrawing.Y = ((uint)widthOfCropSelection + (uint)currentLeftTopCoordinateOfCropSelection.Y) > canvasHeight ? canvasHeight - uwidthOfCropSelection : (uint)currentLeftTopCoordinateOfCropSelection.Y;
                        wbCroppedBitmap = new WriteableBitmap(widthOfCropSelection, heightOfCropSelection);
                    }
                    if (currentApplication.angularDegreeOfWorkingSpaceRotation == 180)
                    {
                        // bitmapBounds starts with index zero. So we have to substract the height and width with value one.
                        bitmapBoundsOfDrawing.Height = (uint)heightOfCropSelection - 1;
                        bitmapBoundsOfDrawing.Width = (uint)widthOfCropSelection - 1;
                        uint uwidthOfCropSelection = (uint)widthOfCropSelection;
                        uint uheightOfCropSelection = (uint)heightOfCropSelection;
                        bitmapBoundsOfDrawing.X = (uwidthOfCropSelection + (uint)currentLeftTopCoordinateOfCropSelection.X) > canvasWidth ? canvasWidth - uwidthOfCropSelection : (uint)currentLeftTopCoordinateOfCropSelection.X;
                        bitmapBoundsOfDrawing.Y = ((uint)heightOfCropSelection + (uint)currentLeftTopCoordinateOfCropSelection.Y) > canvasHeight ? canvasHeight - uheightOfCropSelection : (uint)currentLeftTopCoordinateOfCropSelection.Y;
                        wbCroppedBitmap = new WriteableBitmap(widthOfCropSelection, heightOfCropSelection);
                    }
                    else if (currentApplication.angularDegreeOfWorkingSpaceRotation == 270)
                    {
                        bitmapBoundsOfDrawing.Height = (uint)widthOfCropSelection - 1;
                        bitmapBoundsOfDrawing.Width = (uint)heightOfCropSelection - 1;
                        uint uwidthOfCropSelection = (uint)widthOfCropSelection;
                        uint uheightOfCropSelection = (uint)heightOfCropSelection;
                        bitmapBoundsOfDrawing.X = ((uint)heightOfCropSelection + (uint)currentLeftTopCoordinateOfCropSelection.X) > canvasWidth ? canvasWidth - uheightOfCropSelection : (uint)currentLeftTopCoordinateOfCropSelection.X;
                        bitmapBoundsOfDrawing.Y = ((uint)widthOfCropSelection + (uint)currentLeftTopCoordinateOfCropSelection.Y) > canvasHeight ? canvasHeight - uwidthOfCropSelection : (uint)currentLeftTopCoordinateOfCropSelection.Y;
                        wbCroppedBitmap = new WriteableBitmap(widthOfCropSelection, heightOfCropSelection);
                    }
                    CommandManager.GetInstance().CommitCommand(new CropCommand(bitmapBoundsOfDrawing.X, bitmapBoundsOfDrawing.Y, (uint)(heightOfCropSelection - 1), (uint)(widthOfCropSelection - 1)));
                    bitmapEncoder.BitmapTransform.Bounds = bitmapBoundsOfDrawing;

                    // write out to the stream
                    try
                    {
                        await bitmapEncoder.FlushAsync();
                    }
                    catch (Exception exception)
                    {
                        System.Diagnostics.Debug.WriteLine(exception.StackTrace);
                    }
                    if (wbCroppedBitmap != null)
                    {
                        wbCroppedBitmap.SetSource(mrAccessStream);
                        AddWriteableBitmapToCanvas(wbCroppedBitmap, currentApplication);
                    }
                }
            }
            PocketPaintApplication.GetInstance().PaintingAreaView.drawCheckeredBackgroundInCheckeredCanvas(9);
        }

        async public void CropImageForCropCommand(uint offsetX, uint offsetY, uint height, uint width)
        {
            // needs a little bit more storage but the performance is also little bit better.
            PocketPaintApplication currentApplication = PocketPaintApplication.GetInstance();
            TransformGroup tgPaintingAreaCheckeredGrid = PocketPaintApplication.GetInstance().GridWorkingSpace.RenderTransform as TransformGroup;

            WriteableBitmap wbCroppedBitmap = null;

            string filename = ("karlidavidtest") + ".png";
            await currentApplication.StorageIo.WriteBitmapToPngMediaLibrary(filename);
            StorageFile sfSavedPictureFromWorkingSpace = await KnownFolders.PicturesLibrary.GetFileAsync(filename);
            InMemoryRandomAccessStream mrAccessStream = new InMemoryRandomAccessStream();

            using (Stream streamOfSavedPictureFromWorkingSpace = await sfSavedPictureFromWorkingSpace.OpenStreamForReadAsync())
            {
                using (var memStream = new MemoryStream())
                {
                    await streamOfSavedPictureFromWorkingSpace.CopyToAsync(memStream);
                    memStream.Position = 0;

                    BitmapDecoder bitmapDecoder = await BitmapDecoder.CreateAsync(memStream.AsRandomAccessStream());
                    BitmapEncoder bitmapEncoder = await BitmapEncoder.CreateForTranscodingAsync(mrAccessStream, bitmapDecoder);

                    bitmapEncoder.BitmapTransform.ScaledHeight = (uint)currentApplication.PaintingAreaCanvas.RenderSize.Height;
                    bitmapEncoder.BitmapTransform.ScaledWidth = (uint)currentApplication.PaintingAreaCanvas.RenderSize.Width;

                    BitmapBounds bitmapBoundsOfDrawing = new BitmapBounds();
                    bitmapBoundsOfDrawing.Height = height;
                    bitmapBoundsOfDrawing.Width = width;
                    bitmapBoundsOfDrawing.X = offsetX;
                    bitmapBoundsOfDrawing.Y = offsetY;
                    wbCroppedBitmap = new WriteableBitmap((int)width, (int)height);
                    
                    bitmapEncoder.BitmapTransform.Bounds = bitmapBoundsOfDrawing;

                    // write out to the stream
                    try
                    {
                        await bitmapEncoder.FlushAsync();
                    }
                    catch (Exception exception)
                    {
                        System.Diagnostics.Debug.WriteLine(exception.StackTrace);
                    }
                    if (wbCroppedBitmap != null)
                    {
                        wbCroppedBitmap.SetSource(mrAccessStream);
                        AddWriteableBitmapToCanvas(wbCroppedBitmap, currentApplication);
                    }
                }
            }
        }
    }
}