using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

// Die Elementvorlage "Benutzersteuerelement" ist unter http://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    public sealed partial class RectangleSelectionControlOld : UserControl
    {
        TransformGroup _transformGridMain;

        double _minGridMainHeight = 80.0;
        double _minGridMainWidth = 80.0;
        double _gridMainSize = 230.0;
        double _rectangleForMovementSize = 200.0;
        double _rectangleToDrawSize = 160.0;
        bool _isModifiedRectangleMovement;
        
        public RectangleSelectionControlOld()
        {
            this.InitializeComponent();

            GridMain.RenderTransform = _transformGridMain = new TransformGroup();
            rectRectangleToDraw.Fill = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            rectRectangleToDraw.Stroke = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
            rectRectangleToDraw.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.strokeThickness;

            // PocketPaintApplication.GetInstance().RectangleSelectionControl = this;
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

        private void ellCenterBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double addToHeight = 0.0;
            Point deltaTranslation = e.Delta.Translation;
            RotateTransform lastRotateTransform = getLastRotateTransformation();

            if (lastRotateTransform == null)
            {
                addToHeight = e.Delta.Translation.Y;
            }
            else if ((0.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 90.0) ||
                     (-360.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -270.0))
            { 
                if (deltaTranslation.X < 0.0 && deltaTranslation.Y >= 0.0)
                {
                    addToHeight = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X >= 0.0 && deltaTranslation.Y < 0.0)
                {
                    addToHeight = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    addToHeight = 0.0;
                }
            }
            else if (lastRotateTransform.Angle == 90.0 || lastRotateTransform.Angle == -270.0)
            {
                addToHeight = (e.Delta.Translation.X * -1.0);
            }
            else if ((90.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 180.0) ||
                     (-270.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -180.0))
            {
                if (deltaTranslation.X < 0.0 && deltaTranslation.Y < 0.0)
                {
                    addToHeight = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X >= 0.0 && deltaTranslation.Y >= 0.0)
                {
                    addToHeight = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    addToHeight = 0.0;
                }
            }
            else if (lastRotateTransform.Angle == 180.0 || lastRotateTransform.Angle == -180.0)
            {
                addToHeight = (e.Delta.Translation.Y * -1.0);
            }
            else if ((180.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 270.0) ||
                     (-180.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -90.0))
            {
                if (deltaTranslation.X >= 0.0 && deltaTranslation.Y < 0.0)
                {
                    addToHeight = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X < 0.0 && deltaTranslation.Y >= 0.0)
                {
                    addToHeight = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    addToHeight = 0.0;
                }
            }
            else if (lastRotateTransform.Angle == 270.0 || lastRotateTransform.Angle == -90.0)
            {
                addToHeight = (e.Delta.Translation.X);
            }
            else if ((270.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 360.0) ||
                     (-90.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 0.0))
            {
                if (deltaTranslation.X >= 0.0 && deltaTranslation.Y >= 0.0)
                {
                    addToHeight = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X < 0.0 && deltaTranslation.Y < 0.0)
                {
                    addToHeight = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    addToHeight = 0.0;
                }
            }

            if ((GridMain.Height + addToHeight) >= _minGridMainHeight)
            {
                changeHeightOfUiElements(addToHeight);
                changeMarginBottomGridMain(addToHeight);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        private void ellCenterTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double addToHeight = 0.0;
            Point deltaTranslation = e.Delta.Translation;
            RotateTransform lastRotateTransform = getLastRotateTransformation();

            if (lastRotateTransform == null)
            {
                addToHeight = (e.Delta.Translation.Y * -1.0);
            }
            else if ((0.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 90.0) ||
                     (-360.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -270.0))
            {
                if (deltaTranslation.X >= 0.0 && deltaTranslation.Y < 0.0)
                {
                    addToHeight = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X < 0.0 && deltaTranslation.Y >= 0.0)
                {
                    addToHeight = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    addToHeight = 0.0;
                }
            }
            else if (lastRotateTransform.Angle == 90.0 || lastRotateTransform.Angle == -270.0)
            {
                addToHeight = (e.Delta.Translation.X);
            }
            else if ((90.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 180.0) ||
                     (-270.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -180.0))
            {
                if (deltaTranslation.X >= 0.0 && deltaTranslation.Y >= 0.0)
                {
                    addToHeight = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X < 0.0 && deltaTranslation.Y < 0.0)
                {
                    addToHeight = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    addToHeight = 0.0;
                }
            }
            else if (lastRotateTransform.Angle == 180.0 || lastRotateTransform.Angle == -180.0)
            {
                addToHeight = e.Delta.Translation.Y;
            }
            else if ((180.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 270.0) ||
                     (-180.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -90.0))
            {
                if (deltaTranslation.X < 0.0 && deltaTranslation.Y >= 0.0)
                {
                    addToHeight = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X >= 0.0 && deltaTranslation.Y < 0.0)
                {
                    addToHeight = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    addToHeight = 0.0;
                }
            }
            else if (lastRotateTransform.Angle == 270.0 || lastRotateTransform.Angle == -90.0)
            {
                addToHeight = (e.Delta.Translation.X * -1.0);
            }
            else if ((270.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 360.0) ||
                     (-90.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 0.0))
            {
                if (deltaTranslation.X < 0.0 && deltaTranslation.Y < 0.0)
                {
                    addToHeight = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X >= 0.0 && deltaTranslation.Y >= 0.0)
                {
                    addToHeight = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    addToHeight = 0.0;
                }
            }

            if ((GridMain.Height + addToHeight) >= _minGridMainHeight)
            {
                changeHeightOfUiElements(addToHeight);
                changeMarginTopGridMain(addToHeight);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        private void ellLeftCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double addToWidth = 0.0;
            Point deltaTranslation = e.Delta.Translation;
            RotateTransform lastRotateTransform = getLastRotateTransformation();

            if (lastRotateTransform == null)
            {
                addToWidth = (e.Delta.Translation.X * -1.0);
            }
            else if ((0.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 90.0) ||
                     (-360.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -270.0))
            {
                if (deltaTranslation.X < 0.0 && deltaTranslation.Y < 0.0)
                {
                    addToWidth = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X >= 0.0 && deltaTranslation.Y >= 0.0)
                {
                    addToWidth = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    addToWidth = 0.0;
                }
            }
            else if (lastRotateTransform.Angle == 90.0 || lastRotateTransform.Angle == -270.0)
            {
                addToWidth = (e.Delta.Translation.Y * -1.0);
            }
            else if ((90.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 180.0) ||
                     (-270.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -180.0))
            {
                if (deltaTranslation.X >= 0.0 && deltaTranslation.Y < 0.0)
                {
                    addToWidth = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X < 0.0 && deltaTranslation.Y >= 0.0)
                {
                    addToWidth = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    addToWidth = 0.0;
                }
            }
            else if (lastRotateTransform.Angle == 180.0 || lastRotateTransform.Angle == -180.0)
            {
                addToWidth = (e.Delta.Translation.X);
            }
            else if ((180.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 270.0) ||
                     (-180.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -90.0))
            {
                if (deltaTranslation.X >= 0.0 && deltaTranslation.Y >= 0.0)
                {
                    addToWidth = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X < 0.0 && deltaTranslation.Y < 0.0)
                {
                    addToWidth = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    addToWidth = 0.0;
                }
            }
            else if (lastRotateTransform.Angle == 270.0 || lastRotateTransform.Angle == -90.0)
            {
                addToWidth = e.Delta.Translation.Y;
            }
            else if ((270.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 360.0) ||
                     (-90.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 0.0))
            {  
                if (deltaTranslation.X < 0.0 && deltaTranslation.Y >= 0.0)
                {
                    addToWidth = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X >= 0.0 && deltaTranslation.Y < 0.0)
                {
                    addToWidth = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    addToWidth = 0.0;
                }
            }

            if ((GridMain.Width + addToWidth) >= _minGridMainHeight)
            {
                changeWidthOfUiElements(addToWidth);
                changeMarginLeftGridMain(addToWidth);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        private void ellRightCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double addToWidth = 0.0;
            Point deltaTranslation = e.Delta.Translation;
            RotateTransform lastRotateTransform = getLastRotateTransformation();

            if (lastRotateTransform == null)
            {
                addToWidth = e.Delta.Translation.X;
            }
            else if ((0.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 90.0) ||
                     (-360.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -270.0))
            {
                if (deltaTranslation.X >= 0.0 && deltaTranslation.Y >= 0.0)
                {
                    addToWidth = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X < 0.0 && deltaTranslation.Y < 0.0)
                {
                    addToWidth = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    addToWidth = 0.0;
                }
            }
            else if (lastRotateTransform.Angle == 90.0 || lastRotateTransform.Angle == -270.0)
            {
                addToWidth = e.Delta.Translation.Y;
            }
            else if ((90.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 180.0) ||
                     (-270.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -180.0))
            {    
                if (deltaTranslation.X < 0.0 && deltaTranslation.Y >= 0.0)
                {
                    addToWidth = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X >= 0.0 && deltaTranslation.Y < 0.0)
                {
                    addToWidth = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    addToWidth = 0.0;
                }
            }
            else if (lastRotateTransform.Angle == 180.0 || lastRotateTransform.Angle == -180.0)
            {
                addToWidth = (e.Delta.Translation.X * -1.0);
            }
            else if ((180.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 270.0) ||
                     (-180.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -90.0))
            {
                if (deltaTranslation.X < 0.0 && deltaTranslation.Y < 0.0)
                {
                    addToWidth = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X >= 0.0 && deltaTranslation.Y >= 0.0)
                {
                    addToWidth = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    addToWidth = 0.0;
                }
            }
            else if (lastRotateTransform.Angle == 270.0 || lastRotateTransform.Angle == -90.0)
            {
                addToWidth = (e.Delta.Translation.Y * -1.0);
            }
            else if ((270.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 360.0) ||
                     (-90.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 0.0))
            {
                if (deltaTranslation.X >= 0.0 && deltaTranslation.Y < 0.0)
                {
                    addToWidth = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y);
                }
                else if (deltaTranslation.X < 0.0 && deltaTranslation.Y >= 0.0)
                {
                    addToWidth = Math.Sqrt(deltaTranslation.X * deltaTranslation.X + deltaTranslation.Y * deltaTranslation.Y) * -1.0;
                }
                else
                {
                    addToWidth = 0.0;
                }
            }

            if ((GridMain.Width + addToWidth) >= _minGridMainWidth)
            {
                changeWidthOfUiElements(addToWidth);
                changeMarginRightGridMain(addToWidth);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        private void ellLeftBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double addToWidth = 0.0;
            double addToHeight = 0.0;
            Point deltaTranslation = e.Delta.Translation;
            RotateTransform lastRotateTransform = getLastRotateTransformation();

            if (lastRotateTransform == null)
            {
                addToWidth = (deltaTranslation.X * -1.0);
                addToHeight = deltaTranslation.Y;
            }
            else if ((0.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 90.0) ||
                     (-360.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -270.0))
            {
                // TODO
            }
            else if (lastRotateTransform.Angle == 90.0 || lastRotateTransform.Angle == -270.0)
            {
                addToWidth = (deltaTranslation.X * -1.0);
                addToHeight = (deltaTranslation.Y * -1.0);
            }
            else if ((90.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 180.0) ||
                     (-270.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -180.0))
            {
                // TODO
            }
            else if (lastRotateTransform.Angle == 180.0 || lastRotateTransform.Angle == -180.0)
            {
                addToWidth = deltaTranslation.X;
                addToHeight = (deltaTranslation.Y * -1.0);
            }
            else if ((180.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 270.0) ||
                     (-180.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -90.0))
            {
                // TODO
            }
            else if (lastRotateTransform.Angle == 270.0 || lastRotateTransform.Angle == -90.0)
            {
                addToWidth = deltaTranslation.X;
                addToHeight = deltaTranslation.Y;
            }
            else if ((270.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 360.0) ||
                     (-90.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 0.0))
            {
                // TODO
            }

            if ((GridMain.Width + addToWidth) >= _minGridMainWidth &&
                (GridMain.Height + addToHeight) >= _minGridMainHeight)
            {
                changeWidthOfUiElements(addToWidth);
                changeHeightOfUiElements(addToHeight);

                changeMarginLeftGridMain(addToWidth);
                changeMarginBottomGridMain(addToHeight);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        private void ellRightBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double addToWidth = 0.0;
            double addToHeight = 0.0;
            Point deltaTranslation = e.Delta.Translation;
            RotateTransform lastRotateTransform = getLastRotateTransformation();

            if (lastRotateTransform == null)
            {
                addToWidth = deltaTranslation.X;
                addToHeight = deltaTranslation.Y;
            }
            else if ((0.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 90.0) ||
                     (-360.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -270.0))
            {
                // TODO
            }
            else if (lastRotateTransform.Angle == 90.0 || lastRotateTransform.Angle == -270.0)
            {
                addToWidth = (deltaTranslation.X * -1.0);
                addToHeight = deltaTranslation.Y;
            }
            else if ((90.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 180.0) ||
                     (-270.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -180.0))
            {
                // TODO
            }
            else if (lastRotateTransform.Angle == 180.0 || lastRotateTransform.Angle == -180.0)
            {
                addToWidth = (deltaTranslation.X * -1.0);
                addToHeight = (deltaTranslation.Y * -1.0);
            }
            else if ((180.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 270.0) ||
                     (-180.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -90.0))
            {
                // TODO
            }
            else if (lastRotateTransform.Angle == 270.0 || lastRotateTransform.Angle == -90.0)
            {
                addToWidth = deltaTranslation.X;
                addToHeight = (deltaTranslation.Y * -1.0);
            }
            else if ((270.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 360.0) ||
                     (-90.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 0.0))
            {
                // TODO
            }

            if ((GridMain.Width + addToWidth) >= _minGridMainWidth &&
                (GridMain.Height + addToHeight) >= _minGridMainHeight)
            {
                changeWidthOfUiElements(addToWidth);
                changeHeightOfUiElements(addToHeight);

                changeMarginRightGridMain(addToWidth);
                changeMarginBottomGridMain(addToHeight);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        private void ellLeftTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double addToWidth = 0.0;
            double addToHeight = 0.0;
            Point deltaTranslation = e.Delta.Translation;
            RotateTransform lastRotateTransform = getLastRotateTransformation();

            if (lastRotateTransform == null)
            {
                addToWidth = (deltaTranslation.X * -1.0);
                addToHeight = (deltaTranslation.Y * -1.0);
            }
            else if ((0.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 90.0) ||
                     (-360.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -270.0))
            {
                // TODO
            }
            else if (lastRotateTransform.Angle == 90.0 || lastRotateTransform.Angle == -270.0)
            {
                addToWidth = deltaTranslation.X;
                addToHeight = (deltaTranslation.Y * -1.0);
            }
            else if ((90.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 180.0) ||
                     (-270.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -180.0))
            {
                // TODO
            }
            else if (lastRotateTransform.Angle == 180.0 || lastRotateTransform.Angle == -180.0)
            {
                addToWidth = deltaTranslation.X;
                addToHeight = deltaTranslation.Y;
            }
            else if ((180.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 270.0) ||
                     (-180.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -90.0))
            {
                // TODO
            }
            else if (lastRotateTransform.Angle == 270.0 || lastRotateTransform.Angle == -90.0)
            {
                addToWidth = (deltaTranslation.X * -1.0);
                addToHeight = deltaTranslation.Y;
            }
            else if ((270.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 360.0) ||
                     (-90.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 0.0))
            {
                // TODO
            }

            if ((GridMain.Width + addToWidth) >= _minGridMainWidth &&
                (GridMain.Height + addToHeight) >= _minGridMainHeight)
            {
                changeWidthOfUiElements(addToWidth);
                changeHeightOfUiElements(addToHeight);

                changeMarginLeftGridMain(addToWidth);
                changeMarginTopGridMain(addToHeight);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        private void ellRightTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double addToWidth = 0.0;
            double addToHeight = 0.0;
            Point deltaTranslation = e.Delta.Translation;
            RotateTransform lastRotateTransform = getLastRotateTransformation();

            if (lastRotateTransform == null)
            {
                addToWidth = deltaTranslation.X;
                addToHeight = (deltaTranslation.Y * -1.0);
            }
            else if ((0.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 90.0) ||
                     (-360.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -270.0))
            {
                // TODO
            }
            else if (lastRotateTransform.Angle == 90.0 || lastRotateTransform.Angle == -270.0)
            {
                addToWidth = deltaTranslation.X;
                addToHeight = deltaTranslation.Y;
            }
            else if ((90.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 180.0) ||
                     (-270.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -180.0))
            {
                // TODO
            }
            else if (lastRotateTransform.Angle == 180.0 || lastRotateTransform.Angle == -180.0)
            {
                addToWidth = (deltaTranslation.X * -1.0);
                addToHeight = deltaTranslation.Y;
            }
            else if ((180.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 270.0) ||
                     (-180.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < -90.0))
            {
                // TODO
            }
            else if (lastRotateTransform.Angle == 270.0 || lastRotateTransform.Angle == -90.0)
            {
                addToWidth = (deltaTranslation.X * -1.0);
                addToHeight = (deltaTranslation.Y * -1.0);
            }
            else if ((270.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 360.0) ||
                     (-90.0 < lastRotateTransform.Angle && lastRotateTransform.Angle < 0.0))
            {
                // TODO
            }

            if ((GridMain.Width + addToWidth) >= _minGridMainWidth &&
                (GridMain.Height + addToHeight) >= _minGridMainHeight)
            {
                changeWidthOfUiElements(addToWidth);
                changeHeightOfUiElements(addToHeight);

                changeMarginRightGridMain(addToWidth);
                changeMarginTopGridMain(addToHeight);

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
            double halfScreenHeight = Window.Current.Bounds.Height / 2.0;
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
