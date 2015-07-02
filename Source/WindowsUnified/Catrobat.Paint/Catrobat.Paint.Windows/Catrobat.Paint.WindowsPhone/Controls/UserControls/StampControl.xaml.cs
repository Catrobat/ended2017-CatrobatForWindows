using Catrobat.Paint.WindowsPhone.Command;
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
    public sealed partial class StampControl : UserControl
    {
        TransformGroup _transformGridMain;

        double originalHeightStampedImage = 0.0;
        double originalWidthStampedImage = 0.0;

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
        double _heightStampControl;
        double _widthStampControl;

        double _scaleValueWorkingSpace = 0.0;

        PixelData.PixelData _pixelData = new PixelData.PixelData();

        Point _leftTopNullPointStampSelection;

        double heightOfRectangle = 0.0;
        double widthOfRectangle = 0.0;

        public StampControl()
        {
            InitializeComponent();
            _transformGridMain = new TransformGroup();
            GridMain.RenderTransform = _transformGridMain;
            PocketPaintApplication.GetInstance().StampControl = this;
            SetIsModifiedRectangleMovement = false;
            _leftTopNullPointStampSelection = new Point(0.0, 0.0);

            _offsetMargin = 5.0;
            _heightStampControl = 0.0;
            _widthStampControl = 0.0;
            _scaleValueWorkingSpace = 0.0;
            heightOfRectangle = rectRectangleStampSelection.Height;
            widthOfRectangle = rectRectangleStampSelection.Width;
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
            heightOfRectangle = height;
            widthOfRectangle = width;
            rectRectangleStampSelection.Height = height;
            rectRectangleStampSelection.Width = width;
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

        private void _calculateAndSetStampControlPositionWithoutRotating(double doubleBorderWidthValue, double scaleValueWorkingSpace, bool isWorkingSpaceFlippedHorizontally, bool isWorkingSpaceFlippedVertically)
        {
            PocketPaintApplication currentPaintApplication = PocketPaintApplication.GetInstance();
            TransformGroup tgPaintingAreaCheckeredGrid = currentPaintApplication.GridWorkingSpace.RenderTransform as TransformGroup;
            if (currentPaintApplication == null || tgPaintingAreaCheckeredGrid == null)
            {
                return;
            }

            // is needed to move the blue selection to the right position
            TranslateTransform ttfMoveStampControl = new TranslateTransform();
            double heightOfpaintingAreaCheckeredGrid = currentPaintApplication.GridWorkingSpace.Height;
            double widthOfPaintingAreaCheckeredGrid = currentPaintApplication.GridWorkingSpace.Width;
            // Calculate the position from Stamp selection in connection with the working space respectively with the drawing
            // in the working space. In other words the Stamp selection should be adapted on the drawing in the working space.
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
                _heightStampControl = (extremeRightAndBottomCoordinate.Y - extremeLeftAndTopCoordinate.Y + 1.0) * scaleValueWorkingSpace + doubleBorderWidthValue;
                _widthStampControl = (extremeRightAndBottomCoordinate.X - extremeLeftAndTopCoordinate.X + 1.0) * scaleValueWorkingSpace + doubleBorderWidthValue;

                if (isWorkingSpaceFlippedHorizontally)
                {
                    ttfMoveStampControl.X = tgPaintingAreaCheckeredGrid.Value.OffsetX + ((widthOfPaintingAreaCheckeredGrid - extremeRightAndBottomCoordinate.X) * scaleValueWorkingSpace);
                }
                else
                {
                    ttfMoveStampControl.X = tgPaintingAreaCheckeredGrid.Value.OffsetX + (extremeLeftAndTopCoordinate.X * scaleValueWorkingSpace);
                }

                if (isWorkingSpaceFlippedVertically)
                {
                    ttfMoveStampControl.Y = tgPaintingAreaCheckeredGrid.Value.OffsetY + ((heightOfpaintingAreaCheckeredGrid - extremeRightAndBottomCoordinate.Y) * scaleValueWorkingSpace);
                }
                else
                {
                    ttfMoveStampControl.Y = tgPaintingAreaCheckeredGrid.Value.OffsetY + (extremeLeftAndTopCoordinate.Y * scaleValueWorkingSpace);
                }
            }
            else
            {
                _heightStampControl = scaleValueWorkingSpace * heightOfpaintingAreaCheckeredGrid + _offsetMargin * 2;
                _widthStampControl = scaleValueWorkingSpace * widthOfPaintingAreaCheckeredGrid + _offsetMargin * 2;

                ttfMoveStampControl.X = tgPaintingAreaCheckeredGrid.Value.OffsetX;
                ttfMoveStampControl.Y = tgPaintingAreaCheckeredGrid.Value.OffsetY;
            }
            _SetLimitsForMovableControlBorder(0, currentPaintApplication, tgPaintingAreaCheckeredGrid);
            SetStampControlPosition(_heightStampControl, _widthStampControl, ttfMoveStampControl);
        }

        private void _calculateAndSetStampControlPositionWith90DegreeRotation(double doubleBorderWidthValue, double scaleValueWorkingSpace, bool isWorkingSpaceFlippedHorizontally, bool isWorkingSpaceFlippedVertically)
        {
            PocketPaintApplication currentPaintApplication = PocketPaintApplication.GetInstance();
            TransformGroup tgPaintingAreaCheckeredGrid = currentPaintApplication.GridWorkingSpace.RenderTransform as TransformGroup;
            if (currentPaintApplication == null || tgPaintingAreaCheckeredGrid == null)
            {
                return;
            }
            TranslateTransform ttfMoveStampControl = new TranslateTransform();
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

                _heightStampControl = (extremeRightAndBottomCoordinate.X - extremeLeftAndTopCoordinate.X + 1.0) * scaleValueWorkingSpace + doubleBorderWidthValue;
                _widthStampControl = (extremeRightAndBottomCoordinate.Y - extremeLeftAndTopCoordinate.Y + 1.0) * scaleValueWorkingSpace + doubleBorderWidthValue;

                double workingSpaceWidth = scaleValueWorkingSpace * heightPaintingAreaCheckeredGrid;
                double positionXRightTopCornerWorkingSpace = tgPaintingAreaCheckeredGrid.Value.OffsetX;
                double positionXLeftTopCornerWorkingSpace = positionXRightTopCornerWorkingSpace - workingSpaceWidth;

                if (isWorkingSpaceFlippedHorizontally)
                {
                    ttfMoveStampControl.X = positionXLeftTopCornerWorkingSpace + (extremeLeftAndTopCoordinate.Y * scaleValueWorkingSpace);
                }
                else
                {
                    ttfMoveStampControl.X = positionXLeftTopCornerWorkingSpace + ((heightPaintingAreaCheckeredGrid - (extremeRightAndBottomCoordinate.Y + 1.0)) * scaleValueWorkingSpace);
                }

                if (isWorkingSpaceFlippedVertically)
                {
                    ttfMoveStampControl.Y = tgPaintingAreaCheckeredGrid.Value.OffsetY + ((widthPaintingAreaCheckeredGrid - extremeRightAndBottomCoordinate.X) * scaleValueWorkingSpace);
                }
                else
                {
                    ttfMoveStampControl.Y = tgPaintingAreaCheckeredGrid.Value.OffsetY + (extremeLeftAndTopCoordinate.X * scaleValueWorkingSpace);
                }
            }
            else
            {
                _heightStampControl = scaleValueWorkingSpace * widthPaintingAreaCheckeredGrid + doubleBorderWidthValue;
                _widthStampControl = scaleValueWorkingSpace * heightPaintingAreaCheckeredGrid + doubleBorderWidthValue;
                double widthOfWorkingSpace = scaleValueWorkingSpace * heightPaintingAreaCheckeredGrid;

                double positionXRightTopCornerWorkingSpace = tgPaintingAreaCheckeredGrid.Value.OffsetX;
                double positionXLeftTopCornerWorkingSpace = positionXRightTopCornerWorkingSpace - widthOfWorkingSpace;

                ttfMoveStampControl.X = positionXLeftTopCornerWorkingSpace;
                ttfMoveStampControl.Y = tgPaintingAreaCheckeredGrid.Value.OffsetY;
            }
            _SetLimitsForMovableControlBorder(90, currentPaintApplication, tgPaintingAreaCheckeredGrid);
            SetStampControlPosition(_heightStampControl, _widthStampControl, ttfMoveStampControl);
        }

        private void _calculateAndSetStampControlPositionWith180DegreeRotation(double doubleBorderWidthValue, double scaleValueWorkingSpace, bool isWorkingSpaceFlippedHorizontally, bool isWorkingSpaceFlippedVertically)
        {
            PocketPaintApplication currentPaintApplication = PocketPaintApplication.GetInstance();
            TransformGroup tgPaintingAreaCheckeredGrid = currentPaintApplication.GridWorkingSpace.RenderTransform as TransformGroup;
            if (currentPaintApplication == null || tgPaintingAreaCheckeredGrid == null)
            {
                return;
            }
            TranslateTransform ttfMoveStampControl = new TranslateTransform();
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

                _heightStampControl = (extremeRightAndBottomCoordinate.Y - extremeLeftAndTopCoordinate.Y + 1.0) * scaleValueWorkingSpace + doubleBorderWidthValue;
                _widthStampControl = (extremeRightAndBottomCoordinate.X - extremeLeftAndTopCoordinate.X + 1.0) * scaleValueWorkingSpace + doubleBorderWidthValue;

                double heightOfWorkingSpace = scaleValueWorkingSpace * heightPaintingAreaCheckeredGrid;
                double widthOfWoringSpace = scaleValueWorkingSpace * widthPaintingAreaCheckeredGrid;

                double positionXRightBottomCornerWorkingSpace = tgPaintingAreaCheckeredGrid.Value.OffsetX;
                double positionXLeftBottomCornerWorkingSpace = positionXRightBottomCornerWorkingSpace - widthOfWoringSpace;
                double positionYRigthBottomCornerWorkingSpace = tgPaintingAreaCheckeredGrid.Value.OffsetY;
                double positionYRightTopCornerWorkingSpace = positionYRigthBottomCornerWorkingSpace - heightOfWorkingSpace;

                if (isWorkingSpaceFlippedHorizontally)
                {
                    ttfMoveStampControl.X = positionXLeftBottomCornerWorkingSpace + (extremeLeftAndTopCoordinate.X * scaleValueWorkingSpace);
                }
                else
                {
                    ttfMoveStampControl.X = positionXLeftBottomCornerWorkingSpace + ((widthPaintingAreaCheckeredGrid - extremeRightAndBottomCoordinate.X) * scaleValueWorkingSpace);
                }

                if (isWorkingSpaceFlippedVertically)
                {
                    ttfMoveStampControl.Y = positionYRightTopCornerWorkingSpace + (extremeLeftAndTopCoordinate.Y * scaleValueWorkingSpace);
                }
                else
                {
                    ttfMoveStampControl.Y = positionYRightTopCornerWorkingSpace + (((int)heightPaintingAreaCheckeredGrid - (extremeRightAndBottomCoordinate.Y + 1.0)) * scaleValueWorkingSpace);
                }
            }
            else
            {
                _heightStampControl = scaleValueWorkingSpace * heightPaintingAreaCheckeredGrid + doubleBorderWidthValue;
                _widthStampControl = scaleValueWorkingSpace * widthPaintingAreaCheckeredGrid + doubleBorderWidthValue;
                double heightOfWorkingSpace = scaleValueWorkingSpace * heightPaintingAreaCheckeredGrid;
                double widthOfWorkingSpace = scaleValueWorkingSpace * widthPaintingAreaCheckeredGrid;

                double positionXRightBottomCornerWorkingSpace = tgPaintingAreaCheckeredGrid.Value.OffsetX;
                double positionXLeftBottomCornerWorkingSpace = positionXRightBottomCornerWorkingSpace - widthOfWorkingSpace;
                double positionYRigthBottomCornerWorkingSpace = tgPaintingAreaCheckeredGrid.Value.OffsetY;
                double positionYRightTopCornerWorkingSpace = positionYRigthBottomCornerWorkingSpace - heightOfWorkingSpace;
                ttfMoveStampControl.X = positionXLeftBottomCornerWorkingSpace;
                ttfMoveStampControl.Y = positionYRightTopCornerWorkingSpace;
            }
            _SetLimitsForMovableControlBorder(180, currentPaintApplication, tgPaintingAreaCheckeredGrid);
            SetStampControlPosition(_heightStampControl, _widthStampControl, ttfMoveStampControl);
        }

        private void _calculateAndSetStampControlPositionWith270DegreeRotation(double doubleBorderWidthValue, double scaleValueWorkingSpace, bool isWorkingSpaceFlippedHorizontally, bool isWorkingSpaceFlippedVertically)
        {
            PocketPaintApplication currentPaintApplication = PocketPaintApplication.GetInstance();
            TransformGroup tgPaintingAreaCheckeredGrid = currentPaintApplication.GridWorkingSpace.RenderTransform as TransformGroup;
            if (currentPaintApplication == null || tgPaintingAreaCheckeredGrid == null)
            {
                return;
            }
            TranslateTransform ttfMoveStampControl = new TranslateTransform();
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

                _heightStampControl = (extremeRightAndBottomCoordinate.X - extremeLeftAndTopCoordinate.X + 1.0) * scaleValueWorkingSpace + doubleBorderWidthValue;
                _widthStampControl = (extremeRightAndBottomCoordinate.Y - extremeLeftAndTopCoordinate.Y + 1.0) * scaleValueWorkingSpace + doubleBorderWidthValue;
                double workingSpaceHeight = scaleValueWorkingSpace * widthOfPaintingAreaCheckeredGrid;
                double positionYLeftBottomCornerWorkingSpace = tgPaintingAreaCheckeredGrid.Value.OffsetY;
                double positionYLeftTopCornerWorkingSpace = positionYLeftBottomCornerWorkingSpace - workingSpaceHeight;

                if (isWorkingSpaceFlippedHorizontally)
                {
                    ttfMoveStampControl.X = tgPaintingAreaCheckeredGrid.Value.OffsetX + ((heightOfPaintingAreaCheckeredGrid - extremeRightAndBottomCoordinate.Y) * scaleValueWorkingSpace);
                }
                else
                {
                    ttfMoveStampControl.X = tgPaintingAreaCheckeredGrid.Value.OffsetX + (extremeLeftAndTopCoordinate.Y * scaleValueWorkingSpace);
                }

                if (isWorkingSpaceFlippedVertically)
                {
                    ttfMoveStampControl.Y = positionYLeftTopCornerWorkingSpace + (extremeLeftAndTopCoordinate.X * scaleValueWorkingSpace);
                }
                else
                {
                    ttfMoveStampControl.Y = positionYLeftTopCornerWorkingSpace + ((widthOfPaintingAreaCheckeredGrid - (extremeRightAndBottomCoordinate.X + 1.0)) * scaleValueWorkingSpace);
                }
            }
            else
            {
                _heightStampControl = tgPaintingAreaCheckeredGrid.Value.M21 * widthOfPaintingAreaCheckeredGrid + doubleBorderWidthValue;
                _widthStampControl = tgPaintingAreaCheckeredGrid.Value.M21 * heightOfPaintingAreaCheckeredGrid + doubleBorderWidthValue;
                double workingSpaceHeight = tgPaintingAreaCheckeredGrid.Value.M21 * widthOfPaintingAreaCheckeredGrid;
                double positionYLeftBottomCornerWorkingSpace = tgPaintingAreaCheckeredGrid.Value.OffsetY;
                double positionYLeftTopCornerWorkingSpace = positionYLeftBottomCornerWorkingSpace - workingSpaceHeight;

                ttfMoveStampControl.X = tgPaintingAreaCheckeredGrid.Value.OffsetX;
                ttfMoveStampControl.Y = positionYLeftTopCornerWorkingSpace;
            }
            _SetLimitsForMovableControlBorder(270, currentPaintApplication, tgPaintingAreaCheckeredGrid);
            SetStampControlPosition(_heightStampControl, _widthStampControl, ttfMoveStampControl);
        }

        public void SetStampControlPosition(double stampControlHeight, double stampControlWidth, TranslateTransform moveValue)
        {
            SetControlSize(stampControlHeight, stampControlWidth);
            SetRectangleForMovementSize(stampControlHeight, stampControlWidth);
            _transformGridMain.Children.Add(moveValue);
        }

        public Point GetLeftTopPointOfStampedSelection()
        {
            uint canvasHeight = (uint)PocketPaintApplication.GetInstance().PaintingAreaCanvas.Height;
            uint canvasWidth = (uint)PocketPaintApplication.GetInstance().PaintingAreaCanvas.Width;
            int heightOfStampSelection = (int)PocketPaintApplication.GetInstance().StampControl.GetHeightOfRectangleStampSelection();
            int widthOfStampSelection = (int)PocketPaintApplication.GetInstance().StampControl.GetWidthOfRectangleStampSelection();
            uint uwidthOfStampSelection = (uint)widthOfStampSelection;
            uint uheightOfStampSelection = (uint)heightOfStampSelection;
            Point currentLeftTopCoordinateOfStampSelection = GetXYOffsetBetweenPaintingAreaAndStampControlSelection();
            uint offsetX = (uwidthOfStampSelection + (uint)currentLeftTopCoordinateOfStampSelection.X) > canvasWidth ? canvasWidth - uwidthOfStampSelection : (uint)currentLeftTopCoordinateOfStampSelection.X;
            uint offsetY = ((uint)heightOfStampSelection + (uint)currentLeftTopCoordinateOfStampSelection.Y) > canvasHeight ? canvasHeight - uheightOfStampSelection : (uint)currentLeftTopCoordinateOfStampSelection.Y;
            return new Point(Convert.ToDouble(offsetX), Convert.ToDouble(offsetY));
        
        }

        public void resetCurrentCopiedSelection()
        {
            imgStampedImage.Source = null;
        }
        // TODO: Refactor the setStampSelection function.
        async public void SetStampSelection()
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

            rectRectangleStampSelection.Stroke = currentPaintApplication.PaintingAreaCanvas.Children.Count == 0 ? new SolidColorBrush(Colors.Transparent) : new SolidColorBrush(Colors.CornflowerBlue);
            bool isWorkingSpaceFlippedHorizontally = paintingAreaCheckeredGridTransformGroup != null && paintingAreaCheckeredGridTransformGroup.Value.M11 == -1.0;
            bool isWorkingSpaceFlippedVertically = paintingAreaCheckeredGridTransformGroup != null && paintingAreaCheckeredGridTransformGroup.Value.M22 == -1.0;

            if (currentPaintApplication.angularDegreeOfWorkingSpaceRotation == 0)
            {
                _scaleValueWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.M11;
                _calculateAndSetStampControlPositionWithoutRotating(doubleBorderWidthValue, _scaleValueWorkingSpace, isWorkingSpaceFlippedHorizontally, isWorkingSpaceFlippedVertically);
            }
            else if (currentPaintApplication.angularDegreeOfWorkingSpaceRotation == 90)
            {
                // Attention: Working space is rotated 90°
                _scaleValueWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.M12;
                _calculateAndSetStampControlPositionWith90DegreeRotation(doubleBorderWidthValue, _scaleValueWorkingSpace, isWorkingSpaceFlippedHorizontally, isWorkingSpaceFlippedVertically);
            }
            else if (currentPaintApplication.angularDegreeOfWorkingSpaceRotation == 180)
            {
                // Attention: Working space is rotated 180°
                _scaleValueWorkingSpace = Math.Abs(paintingAreaCheckeredGridTransformGroup.Value.M11);
                _calculateAndSetStampControlPositionWith180DegreeRotation(doubleBorderWidthValue, _scaleValueWorkingSpace, isWorkingSpaceFlippedHorizontally, isWorkingSpaceFlippedVertically);
            }
            else if (currentPaintApplication.angularDegreeOfWorkingSpaceRotation == 270)
            {
                _scaleValueWorkingSpace = paintingAreaCheckeredGridTransformGroup.Value.M21;
                // Attention: Working space is rotated 270°
                _calculateAndSetStampControlPositionWith270DegreeRotation(doubleBorderWidthValue, _scaleValueWorkingSpace, isWorkingSpaceFlippedHorizontally, isWorkingSpaceFlippedVertically);
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
                // TODO: Explain the following line.
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
            if (HasElementsPaintingAreaViews() && (rectRectangleStampSelection.Height + e.Delta.Translation.Y) >= MinRectangleMoveHeight)
            {
                var moveY = CreateTranslateTransform(0.0, e.Delta.Translation.Y);

                double sizeValueToAdd = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY + rectRectangleStampSelection.Height + moveY.Y) > _limitBottom ? 0.0 : moveY.Y;
                ChangeHeightOfUiElements(sizeValueToAdd);
                ChangeMarginBottomOfUiElements(sizeValueToAdd);
            }
        }

        private void rectCenterTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (HasElementsPaintingAreaViews() && (rectRectangleStampSelection.Height + (e.Delta.Translation.Y * -1)) >= MinRectangleMoveHeight)
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
            if (HasElementsPaintingAreaViews() && (rectRectangleStampSelection.Width + (e.Delta.Translation.X * -1)) >= MinRectangleMoveWidth &&
               (rectRectangleStampSelection.Height + e.Delta.Translation.Y) >= MinRectangleMoveHeight)
            {
                var moveX = CreateTranslateTransform((e.Delta.Translation.X * -1.0), 0.0);
                var moveY = CreateTranslateTransform(0.0, (e.Delta.Translation.Y));

                // left
                double sizeValueToAddLeft = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX - moveX.X) < _limitLeft ? 0.0 : moveX.X;
                ChangeWidthOfUiElements(sizeValueToAddLeft);
                ChangeMarginLeftOfUiElements(sizeValueToAddLeft);

                // bottom
                double sizeValueToAddBottom = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY + rectRectangleStampSelection.Height + moveY.Y) > _limitBottom ? 0.0 : moveY.Y;
                ChangeHeightOfUiElements(sizeValueToAddBottom);
                ChangeMarginBottomOfUiElements(sizeValueToAddBottom);
            }
        }

        private void rectLeftCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (HasElementsPaintingAreaViews() && (rectRectangleStampSelection.Width + (e.Delta.Translation.X * -1)) >= MinRectangleMoveWidth)
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
            if (HasElementsPaintingAreaViews() && (rectRectangleStampSelection.Width + (e.Delta.Translation.X * -1)) >= MinRectangleMoveWidth &&
                (rectRectangleStampSelection.Height + (e.Delta.Translation.Y * -1)) >= MinRectangleMoveHeight)
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
            if (HasElementsPaintingAreaViews() && (rectRectangleStampSelection.Width + e.Delta.Translation.X) >= MinRectangleMoveWidth &&
                (rectRectangleStampSelection.Height + e.Delta.Translation.Y) >= MinRectangleMoveHeight)
            {
                var moveX = CreateTranslateTransform((e.Delta.Translation.X), 0.0);
                var moveY = CreateTranslateTransform(0.0, (e.Delta.Translation.Y));

                // right
                double sizeValueToAddRight = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX + rectRectangleStampSelection.Width + moveX.X) > _limitRight ? 0.0 : moveX.X;
                ChangeWidthOfUiElements(sizeValueToAddRight);
                ChangeMarginRightOfUiElements(sizeValueToAddRight);

                // bottom
                double sizeValueToAddBottom = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY + rectRectangleStampSelection.Height + moveY.Y) > _limitBottom ? 0.0 : moveY.Y;
                ChangeHeightOfUiElements(sizeValueToAddBottom);
                ChangeMarginBottomOfUiElements(sizeValueToAddBottom);
            }

        }

        private void rectRightCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (HasElementsPaintingAreaViews() && (rectRectangleStampSelection.Width + e.Delta.Translation.X) >= MinRectangleMoveWidth)
            {
                var moveX = CreateTranslateTransform((e.Delta.Translation.X), 0.0);

                double sizeValueToAdd = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX + rectRectangleStampSelection.Width + moveX.X) > _limitRight ? 0.0 : moveX.X;
                ChangeWidthOfUiElements(sizeValueToAdd);
                ChangeMarginRightOfUiElements(sizeValueToAdd);
            }
        }

        private void rectRightTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (HasElementsPaintingAreaViews() && (rectRectangleStampSelection.Width + e.Delta.Translation.X) >= MinRectangleMoveWidth &&
               (rectRectangleStampSelection.Height + (e.Delta.Translation.Y * -1)) >= MinRectangleMoveHeight)
            {
                var moveX = CreateTranslateTransform((e.Delta.Translation.X), 0.0);
                var moveY = CreateTranslateTransform(0.0, (e.Delta.Translation.Y));

                // right
                double sizeValueToAddRight = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX + rectRectangleStampSelection.Width + moveX.X) > _limitRight ? 0.0 : moveX.X;
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
            heightOfRectangle += value;
            rectRectangleStampSelection.Height += value;

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
            widthOfRectangle += value;
            rectRectangleStampSelection.Width += value;

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

        private void rectRectangleStampSelection_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (HasElementsPaintingAreaViews())
            {
                var move = new TranslateTransform();
                move.X = e.Delta.Translation.X;
                move.Y = e.Delta.Translation.Y;

                //((TranslateTransform)move).X = e.Delta.Translation.X;
                if (move.X < 0)
                {
                    move.X = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX + move.X) < _limitLeft ? 0.0 : move.X;
                }
                else
                {
                    move.X = (GridMain.Margin.Left + _transformGridMain.Value.OffsetX + rectRectangleStampSelection.Width + move.X) > _limitRight ? 0.0 : move.X;
                }

                if (move.Y < 0)
                {
                    move.Y = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY + move.Y) < _limitTop ? 0.0 : move.Y;
                }
                else
                {
                    move.Y = (GridMain.Margin.Top + _transformGridMain.Value.OffsetY + rectRectangleStampSelection.Height + move.Y) > _limitBottom ? 0.0 : move.Y;
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

        private void rectRectangleStampSelection_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
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
                result = PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count > 0;
            }
            return result;
        }

        public double GetHeightOfRectangleStampSelection()
        {
            return (heightOfRectangle - 10.0) / _scaleValueWorkingSpace;
        }

        public double GetWidthOfRectangleStampSelection()
        {
            return (widthOfRectangle - 10.0) / _scaleValueWorkingSpace;
        }

        public Point GetXYOffsetBetweenPaintingAreaAndStampControlSelection()
        {
            TransformGroup tgPaintingAreaCheckeredGrid = PocketPaintApplication.GetInstance().GridWorkingSpace.RenderTransform as TransformGroup;
            PocketPaintApplication currentPaintApplication = PocketPaintApplication.GetInstance();
            if (currentPaintApplication == null || tgPaintingAreaCheckeredGrid == null)
            {
                // TODO: raise Exception
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
                double offsetY = (tgPaintingAreaCheckeredGrid.Value.OffsetX - (_transformGridMain.Value.OffsetX - doubleMargin + _widthStampControl) + GridMain.Margin.Right) / _scaleValueWorkingSpace;
                return new Point(offsetX, offsetY);
            }
            else if (currentPaintApplication.angularDegreeOfWorkingSpaceRotation == 180)
            {
                double offsetX = (tgPaintingAreaCheckeredGrid.Value.OffsetX - (_transformGridMain.Value.OffsetX + _widthStampControl - doubleMargin) + GridMain.Margin.Right) / 0.75;
                double offsetY = (tgPaintingAreaCheckeredGrid.Value.OffsetY - (_transformGridMain.Value.OffsetY + _heightStampControl - doubleMargin) + GridMain.Margin.Bottom) / _scaleValueWorkingSpace;
                return new Point(Math.Ceiling(offsetX), Math.Ceiling(offsetY));
            }
            else if (currentPaintApplication.angularDegreeOfWorkingSpaceRotation == 270)
            {
                double offsetX = (tgPaintingAreaCheckeredGrid.Value.OffsetY + GridMain.Margin.Bottom - (_transformGridMain.Value.OffsetY + _heightStampControl - doubleMargin)) / _scaleValueWorkingSpace;
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
            int height = (int)Math.Ceiling(GetHeightOfRectangleStampSelection());
            int width = (int)Math.Ceiling(GetWidthOfRectangleStampSelection());
            Point leftTopRectangleStampSelection = GetXYOffsetBetweenPaintingAreaAndStampControlSelection();

            Image image = new Image();

            if (currentPaintApplication.angularDegreeOfWorkingSpaceRotation == 0)
            {
                int heightOfStamppedWorkingSpacePicture = writeableBitmapToAdd.PixelHeight;
                int widthOfStamppedWorkingSpacePicture = writeableBitmapToAdd.PixelWidth;
                image.Source = writeableBitmapToAdd;
                image.Height = heightOfStamppedWorkingSpacePicture;
                image.Width = widthOfStamppedWorkingSpacePicture;

                addImageToPaintingAreaCanvas(image);
                currentPaintApplication.PaintingAreaView.setSizeOfPaintingAreaViewCheckered(heightOfStamppedWorkingSpacePicture, widthOfStamppedWorkingSpacePicture);
                currentPaintApplication.PaintingAreaView.alignPositionOfGridWorkingSpace(null);
                setSizeOfPaintingAreaCanvas(heightOfStamppedWorkingSpacePicture, widthOfStamppedWorkingSpacePicture);
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
                int heigthOfStampedWorkingSpacePicture = writeableBitmapToAdd.PixelHeight;
                int widthOfStampedWorkingSpacePicture = writeableBitmapToAdd.PixelWidth;
                image.Source = writeableBitmapToAdd;
                image.Height = heigthOfStampedWorkingSpacePicture;
                image.Width = widthOfStampedWorkingSpacePicture;

                addImageToPaintingAreaCanvas(image);
                currentPaintApplication.PaintingAreaView.setSizeOfPaintingAreaViewCheckered(writeableBitmapToAdd.PixelHeight, writeableBitmapToAdd.PixelWidth);
                currentPaintApplication.PaintingAreaView.alignPositionOfGridWorkingSpace(null);
                setSizeOfPaintingAreaCanvas(heigthOfStampedWorkingSpacePicture, widthOfStampedWorkingSpacePicture);
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
            currentPaintApplication.StampControl.SetStampSelection();
        }

        private bool CheckIfStampSelctionIsChanged(PocketPaintApplication currentApplication, TransformGroup tgPaintingAreaCheckeredGrid)
        {
            bool result = false;
            Point currentLeftTopCoordinateOfStampSelection = GetXYOffsetBetweenPaintingAreaAndStampControlSelection();
            int heightOfStampSelection = 0;
            int widthOfStampSelection = 0;
            bool isWorkingSpaceNotRotated = tgPaintingAreaCheckeredGrid.Value.M11 > 0.0;
            bool isWorkingSpaceRotated90Degree = tgPaintingAreaCheckeredGrid.Value.M12 > 0.0;
            bool isWorkingSpaceRotated180Degree = tgPaintingAreaCheckeredGrid.Value.M11 < 0.0;
            bool isWorkingSpaceRotated270Degree = tgPaintingAreaCheckeredGrid.Value.M12 < 0.0;

            heightOfStampSelection = isWorkingSpaceNotRotated || isWorkingSpaceRotated180Degree ? (int)(currentApplication.StampControl.GetHeightOfRectangleStampSelection())
                                                             : (int)Math.Ceiling(currentApplication.StampControl.GetWidthOfRectangleStampSelection());

            widthOfStampSelection = isWorkingSpaceNotRotated || isWorkingSpaceRotated180Degree ? (int)(currentApplication.StampControl.GetWidthOfRectangleStampSelection())
                                                            : (int)(currentApplication.StampControl.GetHeightOfRectangleStampSelection());

            if ((heightOfStampSelection != (int)currentApplication.PaintingAreaCanvas.Height)
                    || (widthOfStampSelection != (int)currentApplication.PaintingAreaCanvas.Width))
            {
                result = true;
            }
            return result;
        }

        public ImageSource getImageSourceStampedImage()
        {
            return imgStampedImage.Source;
        }

        public void setOriginalSizeOfStampedImage(double height, double width)
        {
            originalHeightStampedImage = height;
            originalWidthStampedImage = width;
        }


        public void setSourceImageStamp(ImageSource imageSource)
        {
            imgStampedImage.Source = imageSource;
        }
    }
}