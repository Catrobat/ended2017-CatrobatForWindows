using Catrobat.Paint.Phone;
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

// Die Elementvorlage "Benutzersteuerelement" ist unter http://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    public sealed partial class RectangleSelectionControl : UserControl
    {
        Point startPosition;
        TransformGroup _transformGridEllipsCenterBottom;
        TransformGroup _transfomrGridEllipseCenterTop;
        TransformGroup _transformGridEllipseLeftBottom;
        TransformGroup _transformGridEllipseLeftCenter;
        TransformGroup _transformGridEllipseLeftTop;
        TransformGroup _transformGridEllipseRightBottom;
        TransformGroup _transformGridEllipseRightCenter;
        TransformGroup _transformGridEllipseRightTop;
        TransformGroup _transformGridMain;

        const double MIN_RECTANGLE_MOVE_HEIGHT = 50.0;
        const double MIN_RECTANGLE_MOVE_WIDTH = 50.0;
        
        public RectangleSelectionControl()
        {
            this.InitializeComponent();
            startPosition = new Point(0,0);
            GridEllCenterBottom.RenderTransform = _transformGridEllipsCenterBottom = new TransformGroup();
            GridEllCenterTop.RenderTransform = _transfomrGridEllipseCenterTop = new TransformGroup();
            GridEllRightCenter.RenderTransform = _transformGridEllipseRightCenter = new TransformGroup();
            GridEllLeftBottom.RenderTransform = _transformGridEllipseLeftBottom = new TransformGroup();
            GridEllLeftCenter.RenderTransform = _transformGridEllipseLeftCenter = new TransformGroup();
            GridEllLeftTop.RenderTransform = _transformGridEllipseLeftTop = new TransformGroup();
            GridEllRightBottom.RenderTransform = _transformGridEllipseRightBottom = new TransformGroup();
            GridEllRightTop.RenderTransform = _transformGridEllipseRightTop = new TransformGroup();
            GridMain.RenderTransform = _transformGridMain = new TransformGroup();

            PocketPaintApplication.GetInstance().RectangleSelectionControl = this;
        }

        private void ellCenterBottom_ManipulationDelta_1(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var move = new TranslateTransform();
            ((TranslateTransform)move).X = 0.0;
            ((TranslateTransform)move).Y = Math.Round(e.Delta.Translation.Y);

            if ((rectRectangleForMovement.Height + e.Delta.Translation.Y) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {

                _transformGridEllipsCenterBottom.Children.Add(move);
                _transformGridEllipseLeftBottom.Children.Add(move);
                _transformGridEllipseRightBottom.Children.Add(move);

                var move2 = new TranslateTransform();
                ((TranslateTransform)move2).X = 0.0;
                ((TranslateTransform)move2).Y = Math.Round(e.Delta.Translation.Y / 2.0);

                _transformGridEllipseLeftCenter.Children.Add(move2);
                _transformGridEllipseRightCenter.Children.Add(move2);

                changeHeightOfUiElements(move.Y);
                changeMarginBottomOfUiElements(move.Y);
                PocketPaintApplication.GetInstance().BarRecEllShape.setTbHeightValue = (int)rectRectangleToDraw.Height;
                PocketPaintApplication.GetInstance().BarRecEllShape.setTbWidthValue = (int)rectRectangleToDraw.Width;
            }
        }

        private void ellCenterTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleForMovement.Height + (e.Delta.Translation.Y * -1)) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var move = new TranslateTransform();
                ((TranslateTransform)move).X = 0.0;
                ((TranslateTransform)move).Y = Math.Round(e.Delta.Translation.Y);

                _transfomrGridEllipseCenterTop.Children.Add(move);
                _transformGridEllipseLeftTop.Children.Add(move);
                _transformGridEllipseRightTop.Children.Add(move);

                var move2 = new TranslateTransform();
                ((TranslateTransform)move2).X = 0.0;
                ((TranslateTransform)move2).Y = Math.Round(e.Delta.Translation.Y / 2.0);

                _transformGridEllipseLeftCenter.Children.Add(move2);
                _transformGridEllipseRightCenter.Children.Add(move2);

                changeHeightOfUiElements((e.Delta.Translation.Y * -1.0));
                changeMarginTopOfUiElements((e.Delta.Translation.Y * -1.0));
                PocketPaintApplication.GetInstance().BarRecEllShape.setTbHeightValue = (int)rectRectangleToDraw.Height;
                PocketPaintApplication.GetInstance().BarRecEllShape.setTbWidthValue = (int)rectRectangleToDraw.Width;
            }
        }

        private void ellLeftBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleForMovement.Width + (e.Delta.Translation.X * -1)) >= MIN_RECTANGLE_MOVE_WIDTH &&
               (rectRectangleForMovement.Height + e.Delta.Translation.Y) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var move = new TranslateTransform();
                ((TranslateTransform)move).X = Math.Round(e.Delta.Translation.X);
                ((TranslateTransform)move).Y = 0.0;

                _transformGridEllipseLeftCenter.Children.Add(move);
                _transformGridEllipseLeftTop.Children.Add(move);

                changeWidthOfUiElements((e.Delta.Translation.X * -1));
                changeMarginLeftOfUiElements((e.Delta.Translation.X * -1));

                var move2 = new TranslateTransform();
                ((TranslateTransform)move2).X = 0.0;
                ((TranslateTransform)move2).Y = Math.Round(e.Delta.Translation.Y);;

                _transformGridEllipsCenterBottom.Children.Add(move2);
                _transformGridEllipseRightBottom.Children.Add(move2);

                changeHeightOfUiElements(move2.Y);
                changeMarginBottomOfUiElements(move2.Y);

                var move3 = new TranslateTransform();
                ((TranslateTransform)move3).X = Math.Round(e.Delta.Translation.X);
                ((TranslateTransform)move3).Y = Math.Round(e.Delta.Translation.Y);

                _transformGridEllipseLeftBottom.Children.Add(move3);

                var move4 = new TranslateTransform();
                ((TranslateTransform)move4).X = 0.0;
                ((TranslateTransform)move4).Y = Math.Round(e.Delta.Translation.Y / 2.0);

                _transformGridEllipseLeftCenter.Children.Add(move4);
                _transformGridEllipseRightCenter.Children.Add(move4);

                var move5 = new TranslateTransform();
                ((TranslateTransform)move5).X = e.Delta.Translation.X / 2;
                ((TranslateTransform)move5).Y = 0.0;

                _transfomrGridEllipseCenterTop.Children.Add(move5);
                _transformGridEllipsCenterBottom.Children.Add(move5);
                PocketPaintApplication.GetInstance().BarRecEllShape.setTbHeightValue = (int)rectRectangleToDraw.Height;
                PocketPaintApplication.GetInstance().BarRecEllShape.setTbWidthValue = (int)rectRectangleToDraw.Width;
            }
        }

        private void ellLeftCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleForMovement.Width + (e.Delta.Translation.X * -1)) >= MIN_RECTANGLE_MOVE_WIDTH)
            {
                var move = new TranslateTransform();
                ((TranslateTransform)move).X = (e.Delta.Translation.X);
                ((TranslateTransform)move).Y = 0.0;

                _transformGridEllipseLeftTop.Children.Add(move);
                _transformGridEllipseLeftCenter.Children.Add(move);
                _transformGridEllipseLeftBottom.Children.Add(move);

                changeWidthOfUiElements((e.Delta.Translation.X * -1));
                changeMarginLeftOfUiElements((e.Delta.Translation.X * -1));

                var move2 = new TranslateTransform();
                ((TranslateTransform)move2).X = e.Delta.Translation.X / 2;
                ((TranslateTransform)move2).Y = 0.0;

                _transfomrGridEllipseCenterTop.Children.Add(move2);
                _transformGridEllipsCenterBottom.Children.Add(move2);

                PocketPaintApplication.GetInstance().BarRecEllShape.setTbHeightValue = (int)rectRectangleToDraw.Height;
                PocketPaintApplication.GetInstance().BarRecEllShape.setTbWidthValue = (int)rectRectangleToDraw.Width;
            }
        }

        private void ellLeftTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleForMovement.Width + (e.Delta.Translation.X * -1)) >= MIN_RECTANGLE_MOVE_WIDTH &&
                (rectRectangleForMovement.Height + (e.Delta.Translation.Y * -1)) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var move = new TranslateTransform();
                ((TranslateTransform)move).X = Math.Round(e.Delta.Translation.X);
                ((TranslateTransform)move).Y = 0.0;

                _transformGridEllipseLeftBottom.Children.Add(move);
                _transformGridEllipseLeftCenter.Children.Add(move);

                changeWidthOfUiElements((e.Delta.Translation.X * -1));
                changeMarginLeftOfUiElements((e.Delta.Translation.X * -1));

                var move2 = new TranslateTransform();
                ((TranslateTransform)move2).X = 0.0;
                ((TranslateTransform)move2).Y = Math.Round(e.Delta.Translation.Y);;

                _transfomrGridEllipseCenterTop.Children.Add(move2);
                _transformGridEllipseRightTop.Children.Add(move2);

                changeHeightOfUiElements((e.Delta.Translation.Y * -1.0));
                changeMarginTopOfUiElements((e.Delta.Translation.Y * -1.0));

                var move3 = new TranslateTransform();
                ((TranslateTransform)move3).X = Math.Round(e.Delta.Translation.X);
                ((TranslateTransform)move3).Y = Math.Round(e.Delta.Translation.Y);

                _transformGridEllipseLeftTop.Children.Add(move3);

                var move4 = new TranslateTransform();
                ((TranslateTransform)move4).X = 0.0;
                ((TranslateTransform)move4).Y = Math.Round(e.Delta.Translation.Y / 2.0);

                _transformGridEllipseLeftCenter.Children.Add(move4);
                _transformGridEllipseRightCenter.Children.Add(move4);

                var move5 = new TranslateTransform();
                ((TranslateTransform)move5).X = e.Delta.Translation.X / 2;
                ((TranslateTransform)move5).Y = 0.0;

                _transfomrGridEllipseCenterTop.Children.Add(move5);
                _transformGridEllipsCenterBottom.Children.Add(move5);

                PocketPaintApplication.GetInstance().BarRecEllShape.setTbHeightValue = (int)rectRectangleToDraw.Height;
                PocketPaintApplication.GetInstance().BarRecEllShape.setTbWidthValue = (int)rectRectangleToDraw.Width;
            }
        }

        private void ellRightBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleForMovement.Width + e.Delta.Translation.X) >= MIN_RECTANGLE_MOVE_WIDTH &&
                (rectRectangleForMovement.Height + e.Delta.Translation.Y) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var move = new TranslateTransform();
                ((TranslateTransform)move).X = Math.Round(e.Delta.Translation.X);
                ((TranslateTransform)move).Y = 0.0;

                _transformGridEllipseRightCenter.Children.Add(move);
                _transformGridEllipseRightTop.Children.Add(move);

                changeMarginRightOfUiElements(e.Delta.Translation.X);
                changeWidthOfUiElements(e.Delta.Translation.X);

                var move2 = new TranslateTransform();
                ((TranslateTransform)move2).X = 0.0;
                ((TranslateTransform)move2).Y = Math.Round(e.Delta.Translation.Y);;

                _transformGridEllipsCenterBottom.Children.Add(move2);
                _transformGridEllipseLeftBottom.Children.Add(move2);

                changeHeightOfUiElements(move2.Y);
                changeMarginBottomOfUiElements(move2.Y);

                var move3 = new TranslateTransform();
                ((TranslateTransform)move3).X = Math.Round(e.Delta.Translation.X);
                ((TranslateTransform)move3).Y = Math.Round(e.Delta.Translation.Y);

                _transformGridEllipseRightBottom.Children.Add(move3);

                var move4 = new TranslateTransform();
                ((TranslateTransform)move4).X = 0.0;
                ((TranslateTransform)move4).Y = Math.Round(e.Delta.Translation.Y / 2.0);

                _transformGridEllipseLeftCenter.Children.Add(move4);
                _transformGridEllipseRightCenter.Children.Add(move4);

                var move5 = new TranslateTransform();
                ((TranslateTransform)move5).X = e.Delta.Translation.X / 2;
                ((TranslateTransform)move5).Y = 0.0;

                _transfomrGridEllipseCenterTop.Children.Add(move5);
                _transformGridEllipsCenterBottom.Children.Add(move5);

                PocketPaintApplication.GetInstance().BarRecEllShape.setTbHeightValue = (int)rectRectangleToDraw.Height;
                PocketPaintApplication.GetInstance().BarRecEllShape.setTbWidthValue = (int)rectRectangleToDraw.Width;
            }

        }

        private void ellRightCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleForMovement.Width + e.Delta.Translation.X) >= MIN_RECTANGLE_MOVE_WIDTH)
            {
                var move = new TranslateTransform();
                ((TranslateTransform)move).X = (e.Delta.Translation.X);
                ((TranslateTransform)move).Y = 0.0;

                _transformGridEllipseRightTop.Children.Add(move);
                _transformGridEllipseRightCenter.Children.Add(move);
                _transformGridEllipseRightBottom.Children.Add(move);

                changeWidthOfUiElements(move.X);
                changeMarginRightOfUiElements(e.Delta.Translation.X);

                var move2 = new TranslateTransform();
                ((TranslateTransform)move2).X = e.Delta.Translation.X / 2;
                ((TranslateTransform)move2).Y = 0.0;

                _transfomrGridEllipseCenterTop.Children.Add(move2);
                _transformGridEllipsCenterBottom.Children.Add(move2);

                PocketPaintApplication.GetInstance().BarRecEllShape.setTbHeightValue = (int)rectRectangleToDraw.Height;
                PocketPaintApplication.GetInstance().BarRecEllShape.setTbWidthValue = (int)rectRectangleToDraw.Width;
            }
        }

        private void ellRightTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if ((rectRectangleForMovement.Width + e.Delta.Translation.X) >= MIN_RECTANGLE_MOVE_WIDTH &&
               (rectRectangleForMovement.Height + (e.Delta.Translation.Y * -1)) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {
                var move = new TranslateTransform();
                ((TranslateTransform)move).X = Math.Round(e.Delta.Translation.X);
                ((TranslateTransform)move).Y = 0.0;

                _transformGridEllipseRightBottom.Children.Add(move);
                _transformGridEllipseRightCenter.Children.Add(move);

                changeMarginRightOfUiElements(e.Delta.Translation.X);
                changeWidthOfUiElements(e.Delta.Translation.X);

                var move2 = new TranslateTransform();
                ((TranslateTransform)move2).X = 0.0;
                ((TranslateTransform)move2).Y = Math.Round(e.Delta.Translation.Y);;

                _transfomrGridEllipseCenterTop.Children.Add(move2);
                _transformGridEllipseLeftTop.Children.Add(move2);

                changeHeightOfUiElements((e.Delta.Translation.Y * -1.0));
                changeMarginTopOfUiElements((e.Delta.Translation.Y * -1.0));

                var move3 = new TranslateTransform();
                ((TranslateTransform)move3).X = Math.Round(e.Delta.Translation.X);
                ((TranslateTransform)move3).Y = Math.Round(e.Delta.Translation.Y);

                _transformGridEllipseRightTop.Children.Add(move3);

                var move4 = new TranslateTransform();
                ((TranslateTransform)move4).X = 0.0;
                ((TranslateTransform)move4).Y = Math.Round(e.Delta.Translation.Y / 2.0);

                _transformGridEllipseLeftCenter.Children.Add(move4);
                _transformGridEllipseRightCenter.Children.Add(move4);

                var move5 = new TranslateTransform();
                ((TranslateTransform)move5).X = e.Delta.Translation.X / 2;
                ((TranslateTransform)move5).Y = 0.0;

                _transfomrGridEllipseCenterTop.Children.Add(move5);
                _transformGridEllipsCenterBottom.Children.Add(move5);

                PocketPaintApplication.GetInstance().BarRecEllShape.setTbHeightValue = (int)rectRectangleToDraw.Height;
                PocketPaintApplication.GetInstance().BarRecEllShape.setTbWidthValue = (int)rectRectangleToDraw.Width;
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
            var movezoom = new TranslateTransform();

            ((TranslateTransform)movezoom).X = Math.Round(e.Delta.Translation.X);
            ((TranslateTransform)movezoom).Y = Math.Round(e.Delta.Translation.Y);;

            _transformGridMain.Children.Add(movezoom);
        }

        public void resetRectangleSelectionControl()
        {
            GridEllCenterBottom.RenderTransform = _transformGridEllipsCenterBottom = new TransformGroup();
            GridEllCenterTop.RenderTransform = _transfomrGridEllipseCenterTop = new TransformGroup();
            GridEllRightCenter.RenderTransform = _transformGridEllipseRightCenter = new TransformGroup();
            GridEllLeftBottom.RenderTransform = _transformGridEllipseLeftBottom = new TransformGroup();
            GridEllLeftCenter.RenderTransform = _transformGridEllipseLeftCenter = new TransformGroup();
            GridEllLeftTop.RenderTransform = _transformGridEllipseLeftTop = new TransformGroup();
            GridEllRightBottom.RenderTransform = _transformGridEllipseRightBottom = new TransformGroup();
            GridEllRightTop.RenderTransform = _transformGridEllipseRightTop = new TransformGroup();
            GridMain.RenderTransform = _transformGridMain = new TransformGroup();
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

        public void changeHeightOfDrawingSelection(double newHeight)
        {
            double moveValue = newHeight - rectRectangleToDraw.Height;
            var move = new TranslateTransform();
            ((TranslateTransform)move).X = 0.0;
            ((TranslateTransform)move).Y = moveValue;

            if ((rectRectangleForMovement.Height + moveValue) >= MIN_RECTANGLE_MOVE_HEIGHT)
            {

                _transformGridEllipsCenterBottom.Children.Add(move);
                _transformGridEllipseLeftBottom.Children.Add(move);
                _transformGridEllipseRightBottom.Children.Add(move);

                var move2 = new TranslateTransform();
                ((TranslateTransform)move2).X = 0.0;
                ((TranslateTransform)move2).Y = moveValue;

                _transformGridEllipseLeftCenter.Children.Add(move2);
                _transformGridEllipseRightCenter.Children.Add(move2);

                changeHeightOfUiElements(move.Y);
                changeMarginBottomOfUiElements(move.Y);
                PocketPaintApplication.GetInstance().BarRecEllShape.setTbHeightValue = (int)rectRectangleToDraw.Height;
                PocketPaintApplication.GetInstance().BarRecEllShape.setTbWidthValue = (int)rectRectangleToDraw.Width;
            }
        }

        public void changeWidthOfDrawingSelection(double newWidth)
        {
            double moveValue = newWidth - rectRectangleToDraw.Width;
            if ((rectRectangleForMovement.Width + moveValue) >= MIN_RECTANGLE_MOVE_WIDTH)
            {
                var move = new TranslateTransform();
                ((TranslateTransform)move).X = moveValue;
                ((TranslateTransform)move).Y = 0.0;

                _transformGridEllipseRightTop.Children.Add(move);
                _transformGridEllipseRightCenter.Children.Add(move);
                _transformGridEllipseRightBottom.Children.Add(move);

                changeWidthOfUiElements(move.X);
                changeMarginRightOfUiElements(moveValue);

                var move2 = new TranslateTransform();
                ((TranslateTransform)move2).X = moveValue / 2;
                ((TranslateTransform)move2).Y = 0.0;

                _transfomrGridEllipseCenterTop.Children.Add(move2);
                _transformGridEllipsCenterBottom.Children.Add(move2);

                PocketPaintApplication.GetInstance().BarRecEllShape.setTbHeightValue = (int)rectRectangleToDraw.Height;
                PocketPaintApplication.GetInstance().BarRecEllShape.setTbWidthValue = (int)rectRectangleToDraw.Width;
            }
        }
    }
}
