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

        const double MIN_GRIDMAIN_HEIGHT = 80.0;
        const double MIN_GRIDMAIN_WIDTH = 80.0;
        bool _isModifiedRectangleMovement;
        
        public RectangleSelectionControl()
        {
            this.InitializeComponent();

            //if (GridMain.RenderTransform != null)
            //{
            //    _transformGridMain = GridMain.RenderTransform as TransformGroup;
            // }
            //if (_transformGridMain == null)
            //{
                GridMain.RenderTransform = _transformGridMain = new TransformGroup();
            //}
            rectRectangleToDraw.Fill = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            rectRectangleToDraw.Stroke = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
            rectRectangleToDraw.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.strokeThicknessRecEll;

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

        private void ellCenterBottom_ManipulationDelta_1(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double deltaTranslationY = Math.Round(e.Delta.Translation.Y);

            if ((GridMain.Height + deltaTranslationY) >= MIN_GRIDMAIN_HEIGHT)
            {
                var moveY = createTranslateTransform(0.0, deltaTranslationY);

                changeHeightOfUiElements(moveY.Y);
                changeMarginBottomGridMain(moveY.Y);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        private void ellCenterTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double deltaTranslationY = Math.Round(e.Delta.Translation.Y * -1.0);

            if ((GridMain.Height + deltaTranslationY) >= MIN_GRIDMAIN_HEIGHT)
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

            if ((GridMain.Width + deltaTranslationX) >= MIN_GRIDMAIN_HEIGHT)
            {
                var moveX = createTranslateTransform(deltaTranslationX, 0.0);

                changeWidthOfUiElements(moveX.X);
                changeMarginLeftGridMain(moveX.X);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        private void ellRightCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double deltaTranslationX = Math.Round(e.Delta.Translation.X);

            if ((GridMain.Width + deltaTranslationX) >= MIN_GRIDMAIN_WIDTH)
            {
                var moveX = createTranslateTransform(deltaTranslationX, 0.0);

                changeWidthOfUiElements(moveX.X);
                changeMarginRightGridMain(moveX.X);

                setSizeOfRecBar(rectRectangleToDraw.Height, rectRectangleToDraw.Width);
            }
        }

        private void ellLeftBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double deltaTranslationX = Math.Round(e.Delta.Translation.X * -1.0);
            double deltaTranslationY = Math.Round(e.Delta.Translation.Y);

            if ((GridMain.Width + deltaTranslationX) >= MIN_GRIDMAIN_WIDTH &&
                (GridMain.Height + deltaTranslationY) >= MIN_GRIDMAIN_HEIGHT)
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

            if ((GridMain.Width + deltaTranslationX) >= MIN_GRIDMAIN_WIDTH &&
                (GridMain.Height + deltaTranslationY) >= MIN_GRIDMAIN_HEIGHT)
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

            if ((GridMain.Width + deltaTranslationX) >= MIN_GRIDMAIN_WIDTH &&
                (GridMain.Height + deltaTranslationY) >= MIN_GRIDMAIN_HEIGHT)
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

            if ((GridMain.Width + deltaTranslationX) >= MIN_GRIDMAIN_WIDTH &&
                (GridMain.Height + deltaTranslationY) >= MIN_GRIDMAIN_HEIGHT)
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

            if ((GridMain.Height + differenceHeight) >= MIN_GRIDMAIN_HEIGHT)
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

            if ((GridMain.Width + differenceWidth) >= MIN_GRIDMAIN_WIDTH)
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

        private void rectRectangleForMovement_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Point centerCoordinate = getCenterCoordinateOfGridMain();

            double coordinateX = centerCoordinate.X - (rectRectangleToDraw.Width / 2.0);
            double coordinateY = centerCoordinate.Y - (rectRectangleToDraw.Height / 2.0);

            PocketPaintApplication.GetInstance().ToolCurrent.Draw(new Point(coordinateX, coordinateY));
        }

        public Point getCenterCoordinateOfGridMain()
        {
            double halfScreenHeight = ((Window.Current.Bounds.Height - PocketPaintApplication.GetInstance().AppbarTop.Height
                                                                     - PocketPaintApplication.GetInstance().BarStandard.Height) / 2.0);
            double halfScreenWidth = Window.Current.Bounds.Width / 2.0;

            double offsetX = _transformGridMain.Value.OffsetX;
            double offsetY = _transformGridMain.Value.OffsetY;

            double marginOffsetX = GridMain.Margin.Left - GridMain.Margin.Right;
            double marginOffsetY = GridMain.Margin.Top - GridMain.Margin.Bottom;

            double coordinateX = offsetX + halfScreenWidth + (marginOffsetX / 2.0);
            double coordinateY = offsetY + halfScreenHeight + (marginOffsetY / 2.0);

            return new Point(coordinateX, coordinateY);
        }

        public Point getCenterOfGridMain()
        {
            double coordinateX = PocketPaintApplication.GetInstance().RectangleSelectionControl.gridMain.Width / 2.0;
            double coordinateY = PocketPaintApplication.GetInstance().RectangleSelectionControl.gridMain.Height / 2.0;
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
            TranslateTransform move = new TranslateTransform();
            move.X = _transformGridMain.Value.OffsetX + e.Delta.Translation.X;
            move.Y = _transformGridMain.Value.OffsetY + e.Delta.Translation.Y;
            addTransformation(move);

            resetAppBarButtonRectangleSelectionControl(true);
            isModifiedRectangleMovement = true;
        }

        public void addTransformation(Transform currentTransformation)
        {
            for (int i = 0; i < _transformGridMain.Children.Count; i++)
            {
                if (_transformGridMain.Children[i].GetType() == currentTransformation.GetType())
                {
                    _transformGridMain.Children.RemoveAt(i);
                }
            }

            if (currentTransformation.GetType() == typeof(RotateTransform))
            {
                //System.Diagnostics.Debug.WriteLine("---------------\noverallRotationAngle = " + ((RotateTransform)currentTransformation).Angle);
                //System.Diagnostics.Debug.WriteLine("---------------\nlastRotationAngle = " + _lastRotationAngle + "\nnewRotationAngle = " + ((RotateTransform)currentTransformation).Angle);
                //_lastRotationAngle += ((RotateTransform)currentTransformation).Angle;
                //System.Diagnostics.Debug.WriteLine("overallRotationAngle = " + _lastRotationAngle);
                //((RotateTransform)currentTransformation).Angle = _lastRotationAngle % 360.0;
            }

            _transformGridMain.Children.Add(currentTransformation);
        }
    }
}
