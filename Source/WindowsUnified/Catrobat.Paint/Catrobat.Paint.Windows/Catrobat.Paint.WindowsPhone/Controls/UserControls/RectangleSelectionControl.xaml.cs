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
    public sealed partial class RectangleSelectionControl : UserControl
    {
        TransformGroup _transformGridMain;

        double _minGridMainHeight = 80.0;
        double _minGridMainWidth = 80.0;
        double _gridMainSize = 230.0;
        double _rectangleForMovementSize = 200.0;
        double _rectangleToDrawSize = 160.0;
        bool _isModifiedRectangleMovement;
        
        public RectangleSelectionControl()
        {
            this.InitializeComponent();

            GridMain.RenderTransform = _transformGridMain = new TransformGroup();
            rectRectangleToDraw.Fill = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            rectRectangleToDraw.Stroke = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
            rectRectangleToDraw.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.strokeThickness;

            PocketPaintApplication.GetInstance().RectangleSelectionControl = this;
            isModifiedRectangleMovement = false;
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

        private void changeSize_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Ellipse currentEllipse = ((Ellipse)sender);
            HorizontalAlignment horizonalAlignment = currentEllipse.HorizontalAlignment;
            VerticalAlignment verticalAlignment = currentEllipse.VerticalAlignment;
            
            Point offset = getOffsetFromLastRotateTransform();
            
            Point pointLeftTop = new Point(offset.X, offset.Y);
            Point pointRightTop = new Point(gridMain.Width - offset.Y, 0.0 + offset.X);
            Point pointRightBottom = new Point(gridMain.Width - offset.X, gridMain.Height - offset.Y);
            Point pointLeftBottom = new Point(0.0 + offset.Y, gridMain.Height - offset.X);

            Point pointCenterTop = new Point((pointLeftTop.X + pointRightTop.X) / 2.0,
                                             (pointLeftTop.Y + pointRightTop.Y) / 2.0);

            Point pointRightCenter = new Point((pointRightTop.X + pointRightBottom.X) / 2.0,
                                             (pointRightTop.Y + pointRightBottom.Y) / 2.0);

            Point pointCenterBottom = new Point((pointRightBottom.X + pointLeftBottom.X) / 2.0,
                                             (pointRightBottom.Y + pointLeftBottom.Y) / 2.0);

            Point pointLeftCenter = new Point((pointLeftBottom.X + pointLeftTop.X) / 2.0,
                                             (pointLeftBottom.Y + pointLeftTop.Y) / 2.0);

            double deltaTranslationX = e.Delta.Translation.X;
            double deltaTranslationY = e.Delta.Translation.Y;

            double resizeValue = Math.Sqrt((deltaTranslationX * deltaTranslationX) +
                                           (deltaTranslationY * deltaTranslationY));

            if (horizonalAlignment.ToString().ToLower().Contains("center"))
            {
                // pointCenterTop
                if (verticalAlignment.ToString().ToLower().Contains("top"))
                {
                    // default

                    // TODO check Position of Point
                    // change height

                    // TODO check if Translation is in bounds

                    // TODO use the correct Translation if in bounds


                    TranslateTransform moveX = createTranslateTransform(resizeValue, 0.0);
                    TranslateTransform moveY = createTranslateTransform(0.0, resizeValue);
                    
                    /*if (deltaTranslationX <= 0.0 && deltaTranslationY <= 0.0)
                    {

                    }
                    else if (deltaTranslationX <= 0.0)
                    {

                    }
                    else if (deltaTranslationY <= 0.0)
                    {

                    }
                    else
                    {

                    }

                    double deltaTranslationYold = Math.Round(e.Delta.Translation.Y * -1.0);
                    */
                    //if ((GridMain.Height - resizeValue) >= _minGridMainHeight)
                    //{
                    //    var moveY = createTranslateTransform(0.0, resizeValue);

                    //    changeHeightOfUiElements(moveY.Y);
                    //    changeMarginTopGridMain(moveY.Y);

                    //    setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
                    //}

                }
                // pointCenterBottom
                else if (verticalAlignment.ToString().ToLower().Contains("bottom"))
                {

                }
            }
            else if (horizonalAlignment.ToString().ToLower().Contains("left"))
            {
                // pointLeftTop
                if (verticalAlignment.ToString().ToLower().Contains("top"))
                {

                }
                // pointLeftCenter
                else if (verticalAlignment.ToString().ToLower().Contains("center"))
                {

                }
                // pointLeftBottom
                else if (verticalAlignment.ToString().ToLower().Contains("bottom"))
                {

                }
            }
            else if (horizonalAlignment.ToString().ToLower().Contains("right"))
            {
                // pointRightTop
                if (verticalAlignment.ToString().ToLower().Contains("top"))
                {

                }
                // pointRightCenter
                else if (verticalAlignment.ToString().ToLower().Contains("center"))
                {

                }
                // pointRightBottom
                else if (verticalAlignment.ToString().ToLower().Contains("bottom"))
                {

                }                
            }
        }

        public void performResize(Point currentPoint, double translateX, double translateY)
        {
            
        }

        private void ellCenterBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            // Fallunterscheidung:
            double deltaTranslationValue = 0.0;
            Point deltaTranslation = e.Delta.Translation;

            RotateTransform lastRotateTransform = getLastRotateTransformation();

            // Rotation = 0°
            if (lastRotateTransform == null)
            {
                deltaTranslationValue = e.Delta.Translation.Y;
            }
            // 0 < Rotation < 90 || -360 < Rotation < -270
            else if ((0.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 90.0) ||
                     (-360.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -270.0))
            {
                // - deltaTranslationX
                // + deltaTranslationY    
                if (deltaTranslation.X < 0.0 && deltaTranslation.Y >= 0.0)
                {
                    deltaTranslationValue = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X >= 0.0 && deltaTranslation.Y < 0.0)
                {
                    deltaTranslationValue = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    deltaTranslationValue = 0.0;
                }
            }
            // Rotation = 90°
            else if (lastRotateTransform.Angle == 90.0 || lastRotateTransform.Angle == -270.0)
            {
                deltaTranslationValue = (e.Delta.Translation.X * -1.0);
            }
            // 90 < Rotation < 180 || -270 < Rotation < -180
            if ((90.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 180.0) ||
                (-270.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -180.0))
            {
                // - deltaTranslationX
                // - deltaTranslationY 
                if (deltaTranslation.X < 0.0 && deltaTranslation.Y < 0.0)
                {
                    deltaTranslationValue = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X >= 0.0 && deltaTranslation.Y >= 0.0)
                {
                    deltaTranslationValue = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    deltaTranslationValue = 0.0;
                }
            }
            // Rotation = 180°
            else if (lastRotateTransform.Angle == 180.0 || lastRotateTransform.Angle == -180.0)
            {
                deltaTranslationValue = (e.Delta.Translation.Y * -1.0);
            }
            // 180 < Rotation < 270 || -180 < Rotation < -90
            if ((180.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 270.0) ||
                (-180.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -90.0))
            {
                // + deltaTranslationX
                // - deltaTranslationY 
                if (deltaTranslation.X >= 0.0 && deltaTranslation.Y < 0.0)
                {
                    deltaTranslationValue = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X < 0.0 && deltaTranslation.Y >= 0.0)
                {
                    deltaTranslationValue = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    deltaTranslationValue = 0.0;
                }
            }
            // Rotation = 270°
            else if (lastRotateTransform.Angle == 270.0 || lastRotateTransform.Angle == -90.0)
            {
                deltaTranslationValue = (e.Delta.Translation.X);
            }
            // 270 < Rotation < 360 || -90 < Rotation < 0
            if ((270.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 360.0) ||
                (-90.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 0.0))
            {
                // + deltaTranslationX
                // + deltaTranslationY
                if (deltaTranslation.X >= 0.0 && deltaTranslation.Y >= 0.0)
                {
                    deltaTranslationValue = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X < 0.0 && deltaTranslation.Y < 0.0)
                {
                    deltaTranslationValue = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    deltaTranslationValue = 0.0;
                }
            }

            if ((GridMain.Height + deltaTranslationValue) >= _minGridMainHeight)
            {
                changeHeightOfUiElements(deltaTranslationValue);
                changeMarginBottomGridMain(deltaTranslationValue);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        private void ellCenterTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double deltaTranslationY = Math.Round(e.Delta.Translation.Y * -1.0);

            if ((GridMain.Height + deltaTranslationY) >= _minGridMainHeight)
            {
                var moveY = createTranslateTransform(0.0, deltaTranslationY);

                changeHeightOfUiElements(moveY.Y);
                changeMarginTopGridMain(moveY.Y);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        private void ellLeftCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double deltaTranslationX = Math.Round(e.Delta.Translation.X * -1.0);

            if ((GridMain.Width + deltaTranslationX) >= _minGridMainHeight)
            {
                var moveX = createTranslateTransform(deltaTranslationX, 0.0);

                changeWidthOfUiElements(moveX.X);
                changeMarginLeftGridMain(moveX.X);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        private void ellRightCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            // Fallunterscheidung:
            double deltaTranslationValue = 0.0;
            Point deltaTranslation = e.Delta.Translation;

            RotateTransform lastRotateTransform = getLastRotateTransformation();

            // Rotation = 0°
            if (lastRotateTransform == null)
            {
                // + deltaTranslationX
                deltaTranslationValue = e.Delta.Translation.X;
            }
            // 0 < Rotation < 90 || -360 < Rotation < -270
            else if ((0.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 90.0) ||
                     (-360.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -270.0))
            {
                // + deltaTranslationX
                // + deltaTranslationY
                if (deltaTranslation.X >= 0.0 && deltaTranslation.Y >= 0.0)
                {
                    deltaTranslationValue = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X < 0.0 && deltaTranslation.Y < 0.0)
                {
                    deltaTranslationValue = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    deltaTranslationValue = 0.0;
                }
            }
            // Rotation = 90°
            else if (lastRotateTransform.Angle == 90.0 || lastRotateTransform.Angle == -270.0)
            {
                // + deltaTranslationY
                deltaTranslationValue = e.Delta.Translation.Y;
            }
            // 90 < Rotation < 180 || -270 < Rotation < -180
            if ((90.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 180.0) ||
                (-270.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -180.0))
            {
                // - deltaTranslationX
                // + deltaTranslationY    
                if (deltaTranslation.X < 0.0 && deltaTranslation.Y >= 0.0)
                {
                    deltaTranslationValue = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X >= 0.0 && deltaTranslation.Y < 0.0)
                {
                    deltaTranslationValue = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    deltaTranslationValue = 0.0;
                }
            }
            // Rotation = 180°
            else if (lastRotateTransform.Angle == 180.0 || lastRotateTransform.Angle == -180.0)
            {
                // - deltaTranslationX
                deltaTranslationValue = (e.Delta.Translation.X * -1.0);
            }
            // 180 < Rotation < 270 || -180 < Rotation < -90
            if ((180.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 270.0) ||
                (-180.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -90.0))
            {
                // - deltaTranslationX
                // - deltaTranslationY 
                if (deltaTranslation.X < 0.0 && deltaTranslation.Y < 0.0)
                {
                    deltaTranslationValue = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X >= 0.0 && deltaTranslation.Y >= 0.0)
                {
                    deltaTranslationValue = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    deltaTranslationValue = 0.0;
                }
            }
            // Rotation = 270°
            else if (lastRotateTransform.Angle == 270.0 || lastRotateTransform.Angle == -90.0)
            {
                // - deltaTranslationY
                deltaTranslationValue = (e.Delta.Translation.Y * -1.0);
            }
            // 270 < Rotation < 360 || -90 < Rotation < 0
            if ((270.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 360.0) ||
                (-90.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 0.0))
            {
                // + deltaTranslationX
                // - deltaTranslationY 
                if (deltaTranslation.X >= 0.0 && deltaTranslation.Y < 0.0)
                {
                    deltaTranslationValue = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X < 0.0 && deltaTranslation.Y >= 0.0)
                {
                    deltaTranslationValue = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    deltaTranslationValue = 0.0;
                }
            }


            if ((GridMain.Width + deltaTranslationValue) >= _minGridMainWidth)
            {
                changeWidthOfUiElements(deltaTranslationValue);
                changeMarginRightGridMain(deltaTranslationValue);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        private void ellLeftBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double deltaTranslationX = Math.Round(e.Delta.Translation.X * -1.0);
            double deltaTranslationY = Math.Round(e.Delta.Translation.Y);

            if ((GridMain.Width + deltaTranslationX) >= _minGridMainWidth &&
                (GridMain.Height + deltaTranslationY) >= _minGridMainHeight)
            {
                var moveX = createTranslateTransform(deltaTranslationX, 0.0);
                var moveY = createTranslateTransform(0.0, deltaTranslationY);

                changeWidthOfUiElements(moveX.X);
                changeHeightOfUiElements(moveY.Y);

                changeMarginLeftGridMain(moveX.X);
                changeMarginBottomGridMain(moveY.Y);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        private void ellRightBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double deltaTranslationX = Math.Round(e.Delta.Translation.X);
            double deltaTranslationY = Math.Round(e.Delta.Translation.Y);

            if ((GridMain.Width + deltaTranslationX) >= _minGridMainWidth &&
                (GridMain.Height + deltaTranslationY) >= _minGridMainHeight)
            {
                var moveX = createTranslateTransform(deltaTranslationX, 0.0);
                var moveY = createTranslateTransform(0.0, deltaTranslationY);

                changeWidthOfUiElements(moveX.X);
                changeHeightOfUiElements(moveY.Y);

                changeMarginRightGridMain(moveX.X);
                changeMarginBottomGridMain(moveY.Y);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        private void ellLeftTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double deltaTranslationX = Math.Round(e.Delta.Translation.X * -1.0);
            double deltaTranslationY = Math.Round(e.Delta.Translation.Y * -1.0);

            if ((GridMain.Width + deltaTranslationX) >= _minGridMainWidth &&
                (GridMain.Height + deltaTranslationY) >= _minGridMainHeight)
            {
                var moveX = createTranslateTransform(deltaTranslationX, 0.0);
                var moveY = createTranslateTransform(0.0, deltaTranslationY);

                changeWidthOfUiElements(moveX.X);
                changeHeightOfUiElements(moveY.Y);
                
                changeMarginLeftGridMain(moveX.X);
                changeMarginTopGridMain(moveY.Y);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        private void ellRightTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double deltaTranslationX = Math.Round(e.Delta.Translation.X);
            double deltaTranslationY = Math.Round(e.Delta.Translation.Y * -1.0);

            if ((GridMain.Width + deltaTranslationX) >= _minGridMainWidth &&
                (GridMain.Height + deltaTranslationY) >= _minGridMainHeight)
            {
                var moveX = createTranslateTransform(deltaTranslationX, 0.0);
                var moveY = createTranslateTransform(0.0, deltaTranslationY);

                changeWidthOfUiElements(moveX.X);
                changeHeightOfUiElements(moveY.Y);

                changeMarginRightGridMain(moveX.X);
                changeMarginTopGridMain(moveY.Y);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        public void changeHeightOfDrawingSelection(double newHeight, bool changeBtnValues)
        {
            double differenceHeight = newHeight - GridMain.Height;

            if ((GridMain.Height + differenceHeight) >= _minGridMainHeight)
            {
                var moveY = createTranslateTransform(0.0, differenceHeight);

                changeHeightOfUiElements(moveY.Y);
                changeMarginBottomGridMain(moveY.Y);

                if (changeBtnValues)
                {
                    PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = rectRectangleToDraw.Height;
                    PocketPaintApplication.GetInstance().BarRecEllShape.setBtnWidthValue = rectRectangleToDraw.Width;
                }
                resetAppBarButtonRectangleSelectionControl(true);
                isModifiedRectangleMovement = true;
            }

            PocketPaintApplication.GetInstance().BarRecEllShape.updateSldStrokeThickness(Convert.ToInt32(rectRectangleToDraw.Height), Convert.ToInt32(rectRectangleToDraw.Width));
        }

        public void changeWidthOfDrawingSelection(double newWidth, bool changeBtnValues)
        {
            double differenceWidth = newWidth - GridMain.Width;

            if ((GridMain.Width + differenceWidth) >= _minGridMainWidth)
            {
                var moveX = createTranslateTransform(differenceWidth, 0.0);

                changeWidthOfUiElements(moveX.X);
                changeMarginRightGridMain(moveX.X);

                if (changeBtnValues)
                {
                    PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = rectRectangleToDraw.Height;
                    PocketPaintApplication.GetInstance().BarRecEllShape.setBtnWidthValue = rectRectangleToDraw.Width;
                }
                resetAppBarButtonRectangleSelectionControl(true);
                isModifiedRectangleMovement = true;
            }

            PocketPaintApplication.GetInstance().BarRecEllShape.updateSldStrokeThickness(Convert.ToInt32(rectRectangleToDraw.Height), Convert.ToInt32(rectRectangleToDraw.Width));
        }

        private void changeHeightOfUiElements(double value)
        {
            GridMain.Height += value;
            rectRectangleForMovement.Height += value;
            rectRectangleToDraw.Height += value;

            resetAppBarButtonRectangleSelectionControl(true);
            isModifiedRectangleMovement = true;

            PocketPaintApplication.GetInstance().BarRecEllShape.updateSldStrokeThickness(Convert.ToInt32(rectRectangleToDraw.Height), Convert.ToInt32(rectRectangleToDraw.Width));
        }

        private void changeWidthOfUiElements(double value)
        {
            GridMain.Width += value;
            rectRectangleForMovement.Width += value;
            rectRectangleToDraw.Width += value;

            resetAppBarButtonRectangleSelectionControl(true);
            isModifiedRectangleMovement = true;

            PocketPaintApplication.GetInstance().BarRecEllShape.updateSldStrokeThickness(Convert.ToInt32(rectRectangleToDraw.Height), Convert.ToInt32(rectRectangleToDraw.Width));
        }

        private void changeMarginTopGridMain(double value)
        {
            GridMain.Margin = new Thickness(GridMain.Margin.Left, GridMain.Margin.Top - value, GridMain.Margin.Right, GridMain.Margin.Bottom);
        }

        private void changeMarginBottomGridMain(double value)
        {
            GridMain.Margin = new Thickness(GridMain.Margin.Left, GridMain.Margin.Top, GridMain.Margin.Right, GridMain.Margin.Bottom - value);
        }

        private void changeMarginLeftGridMain(double value)
        {
            GridMain.Margin = new Thickness(GridMain.Margin.Left - value, GridMain.Margin.Top, GridMain.Margin.Right, GridMain.Margin.Bottom);
        }

        private void changeMarginRightGridMain(double value)
        {
            GridMain.Margin = new Thickness(GridMain.Margin.Left, GridMain.Margin.Top, GridMain.Margin.Right - value, GridMain.Margin.Bottom);
        }

        public Brush fillOfRectangleToDraw
        {
            get
            {
                return rectRectangleToDraw.Fill;
            }
            set
            {
                rectRectangleToDraw.Fill = value;
            }
        }

        public Brush strokeOfRectangleToDraw
        {
            get
            {
                return rectRectangleToDraw.Stroke;
            }
            set
            {
                rectRectangleToDraw.Stroke = value;
            }
            
        }

        public double strokeThicknessOfRectangleToDraw
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

        public Rectangle rectangleToDraw
        {
            get
            {
                return rectRectangleToDraw;
            }
            set
            {
                rectRectangleToDraw = value;
            }
        }

        public bool isModifiedRectangleMovement
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

        public void resetAppBarButtonRectangleSelectionControl(bool isActivated)
        {
            AppBarButton appBarButtonReset = PocketPaintApplication.GetInstance().PaintingAreaView.getAppBarResetButton();
            if (appBarButtonReset != null)
            {
                appBarButtonReset.IsEnabled = isActivated;
            }
        }

        public Point getCenterCoordinateOfGridMain()
        {
            double halfScreenHeight = ((Window.Current.Bounds.Height - PocketPaintApplication.GetInstance().AppbarTop.Height
                                                                     - PocketPaintApplication.GetInstance().BarStandard.Height) / 2.0);
            double halfScreenWidth = Window.Current.Bounds.Width / 2.0;

            TranslateTransform lastTranslateTransform = getLastTranslateTransformation();
            RotateTransform lastRotateTransform = getLastRotateTransformation();

            double offsetX;
            double offsetY;
            if (lastTranslateTransform != null && lastRotateTransform == null)
            {
                offsetX = lastTranslateTransform.X;
                offsetY = lastTranslateTransform.Y;
            }
            else if (lastTranslateTransform == null && lastRotateTransform != null)
            {
                offsetX = 0.0;
                offsetY = 0.0;
            }
            else if (lastTranslateTransform != null && lastRotateTransform != null)
            {
                offsetX = lastTranslateTransform.X;
                offsetY = lastTranslateTransform.Y;
            }
            else
            {
                offsetX = 0.0;
                offsetY = 0.0;
            }

            double marginOffsetX = GridMain.Margin.Left - GridMain.Margin.Right;
            double marginOffsetY = GridMain.Margin.Top - GridMain.Margin.Bottom;

            double coordinateX = offsetX + halfScreenWidth + (marginOffsetX / 2.0);
            double coordinateY = offsetY + halfScreenHeight + (marginOffsetY / 2.0);

            return new Point(coordinateX, coordinateY);
        }
        
        public Grid gridMain
        {
            get
            {
                return GridMain;
            }
            set
            {
                GridMain = value;
            }
        }

        private void rectRectangleForMovement_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            TranslateTransform translateTransform = new TranslateTransform();

            TranslateTransform lastTranslateTransform = getLastTranslateTransformation();
            RotateTransform lastRotateTransform = getLastRotateTransformation();

            if (lastTranslateTransform != null && lastRotateTransform == null) 
            {
                translateTransform.X = e.Delta.Translation.X + lastTranslateTransform.X;
                translateTransform.Y = e.Delta.Translation.Y + lastTranslateTransform.Y;
            }
            else if (lastTranslateTransform == null && lastRotateTransform != null)
            {
                translateTransform.X = e.Delta.Translation.X;
                translateTransform.Y = e.Delta.Translation.Y;
            }
            else if (lastTranslateTransform != null && lastRotateTransform != null)
            {
                translateTransform.X = e.Delta.Translation.X + lastTranslateTransform.X;
                translateTransform.Y = e.Delta.Translation.Y + lastTranslateTransform.Y;
            }
            else
            {
                translateTransform.X = e.Delta.Translation.X;
                translateTransform.Y = e.Delta.Translation.Y;
            }
            addTransformation(translateTransform);
        }

        private void rectRectangleForMovement_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Point centerCoordinate = getCenterCoordinateOfGridMain();

            PocketPaintApplication.GetInstance().ToolCurrent.Draw(centerCoordinate);
        }

        public Point getOffsetFromLastRotateTransform()
        {
            Point offset;

            TranslateTransform lastTranslateTransform = getLastTranslateTransformation();
            RotateTransform lastRotateTransform = getLastRotateTransformation();

            if (lastTranslateTransform != null && lastRotateTransform == null)
            {
                offset = new Point(0.0, 0.0);
            }
            else if (lastTranslateTransform == null && lastRotateTransform != null)
            {
                offset = new Point(_transformGridMain.Value.OffsetX, _transformGridMain.Value.OffsetY);
            }
            else if (lastTranslateTransform != null && lastRotateTransform != null)
            {
                offset = new Point(_transformGridMain.Value.OffsetX - lastTranslateTransform.X,
                                   _transformGridMain.Value.OffsetY - lastTranslateTransform.Y);;
            }
            else
            {
                offset = new Point(0.0, 0.0);
            }

            return offset;
        }

        public TranslateTransform getLastTranslateTransformation()
        {
            for (int i = 0; i < _transformGridMain.Children.Count; i++)
            {
                if (_transformGridMain.Children[i].GetType() == typeof(TranslateTransform))
                {
                    TranslateTransform translateTransform = new TranslateTransform();
                    translateTransform.X = ((TranslateTransform)_transformGridMain.Children[i]).X;
                    translateTransform.Y = ((TranslateTransform)_transformGridMain.Children[i]).Y;

                    return translateTransform;
                }
            }

            return null;
        }

        public RotateTransform getLastRotateTransformation()
        {
            for (int i = 0; i < _transformGridMain.Children.Count; i++)
            {
                if (_transformGridMain.Children[i].GetType() == typeof(RotateTransform))
                {
                    RotateTransform rotateTransform = new RotateTransform();
                    rotateTransform.CenterX = ((RotateTransform)_transformGridMain.Children[i]).CenterX;
                    rotateTransform.CenterY = ((RotateTransform)_transformGridMain.Children[i]).CenterY;
                    rotateTransform.Angle = ((RotateTransform)_transformGridMain.Children[i]).Angle;

                    return rotateTransform;
                }
            }

            return null;
        }

        public void addTransformation(Transform currentTransform)
        {
            for (int i = 0; i < _transformGridMain.Children.Count; i++)
            {
                if (_transformGridMain.Children[i].GetType() == currentTransform.GetType())
                {
                    _transformGridMain.Children.RemoveAt(i);
                }
            }

            _transformGridMain.Children.Add(currentTransform);

            resetAppBarButtonRectangleSelectionControl(true);
            isModifiedRectangleMovement = true;
        }

        public void resetRectangleSelectionControl()
        {
            PocketPaintApplication.GetInstance().PaintingAreaManipulationListener.lastPoint = new Point(0.0, 0.0);

            PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = _rectangleToDrawSize;
            PocketPaintApplication.GetInstance().BarRecEllShape.setBtnWidthValue = _rectangleToDrawSize;
            
            _transformGridMain.Children.Clear();
            GridMain.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);

            GridMain.Width = _gridMainSize;
            GridMain.Height = _gridMainSize;

            rectRectangleToDraw.Width = _rectangleToDrawSize;
            rectRectangleToDraw.Height = _rectangleToDrawSize;
            rectRectangleForMovement.Width = _rectangleForMovementSize;
            rectRectangleForMovement.Height = _rectangleForMovementSize;

            fillOfRectangleToDraw = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            strokeOfRectangleToDraw = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
            strokeThicknessOfRectangleToDraw = PocketPaintApplication.GetInstance().PaintData.strokeThickness;
            strokeLineJoinOfRectangleToDraw = PocketPaintApplication.GetInstance().PaintData.penLineJoinSelected;
            isModifiedRectangleMovement = false;
        }
    }
}
