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
    public sealed partial class EllipseSelectionControl : UserControl
    {
        TransformGroup _transformGridMain;

        double _minGridMainHeight = 80.0;
        double _minGridMainWidth = 80.0;
        double _gridMainSize = 230.0;
        double _rectangleForMovementSize = 200.0;
        double _ellipseToDrawSize = 160.0;
        bool _isModifiedEllipseMovement;

        public EllipseSelectionControl()
        {
            this.InitializeComponent();

            GridMain.RenderTransform = _transformGridMain = new TransformGroup();
            ellEllipseToDraw.Fill = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            ellEllipseToDraw.Stroke = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
            ellEllipseToDraw.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.strokeThickness;

            PocketPaintApplication.GetInstance().EllipseSelectionControl = this;
            isModifiedEllipseMovement = false;
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

            if ((GridMain.Height + deltaTranslationY) >= _minGridMainHeight)
            {
                var moveY = createTranslateTransform(0.0, deltaTranslationY);

                changeHeightOfUiElements(moveY.Y);
                changeMarginBottomGridMain(moveY.Y);

                setSizeOfRecBar(ellEllipseToDraw.Height, ellEllipseToDraw.Width);
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

                setSizeOfRecBar(ellEllipseToDraw.Height, ellEllipseToDraw.Width);
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

                setSizeOfRecBar(ellEllipseToDraw.Height, ellEllipseToDraw.Width);
            }
        }

        private void ellRightCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double deltaTranslationX = Math.Round(e.Delta.Translation.X);

            if ((GridMain.Width + deltaTranslationX) >= _minGridMainWidth)
            {
                var moveX = createTranslateTransform(deltaTranslationX, 0.0);

                changeWidthOfUiElements(moveX.X);
                changeMarginRightGridMain(moveX.X);

                setSizeOfRecBar(ellEllipseToDraw.Height, ellEllipseToDraw.Width);
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

                setSizeOfRecBar(ellEllipseToDraw.Height, ellEllipseToDraw.Width);
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

                setSizeOfRecBar(ellEllipseToDraw.Height, ellEllipseToDraw.Width);
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

                setSizeOfRecBar(ellEllipseToDraw.Height, ellEllipseToDraw.Width);
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

                setSizeOfRecBar(ellEllipseToDraw.Height, ellEllipseToDraw.Width);
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
                    PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = ellEllipseToDraw.Height;
                    PocketPaintApplication.GetInstance().BarRecEllShape.setBtnWidthValue = ellEllipseToDraw.Width;
                }
                resetAppBarButtonEllipseSelectionControl(true);
                isModifiedEllipseMovement = true;
            }

            PocketPaintApplication.GetInstance().BarRecEllShape.updateSldStrokeThickness(Convert.ToInt32(ellEllipseToDraw.Height), Convert.ToInt32(ellEllipseToDraw.Width));
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
                    PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = ellEllipseToDraw.Height;
                    PocketPaintApplication.GetInstance().BarRecEllShape.setBtnWidthValue = ellEllipseToDraw.Width;
                }
                resetAppBarButtonEllipseSelectionControl(true);
                isModifiedEllipseMovement = true;
            }

            PocketPaintApplication.GetInstance().BarRecEllShape.updateSldStrokeThickness(Convert.ToInt32(ellEllipseToDraw.Height), Convert.ToInt32(ellEllipseToDraw.Width));
        }

        private void changeHeightOfUiElements(double value)
        {
            GridMain.Height += value;
            rectEllipseForMovement.Height += value;
            ellEllipseToDraw.Height += value;

            resetAppBarButtonEllipseSelectionControl(true);
            isModifiedEllipseMovement = true;

            PocketPaintApplication.GetInstance().BarRecEllShape.updateSldStrokeThickness(Convert.ToInt32(ellEllipseToDraw.Height), Convert.ToInt32(ellEllipseToDraw.Width));
        }

        private void changeWidthOfUiElements(double value)
        {
            GridMain.Width += value;
            rectEllipseForMovement.Width += value;
            ellEllipseToDraw.Width += value;

            resetAppBarButtonEllipseSelectionControl(true);
            isModifiedEllipseMovement = true;

            PocketPaintApplication.GetInstance().BarRecEllShape.updateSldStrokeThickness(Convert.ToInt32(ellEllipseToDraw.Height), Convert.ToInt32(ellEllipseToDraw.Width));
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

        public Brush fillOfEllipseToDraw
        {
            get
            {
                return ellEllipseToDraw.Fill;
            }
            set
            {
                ellEllipseToDraw.Fill = value;
            }
        }

        public Brush strokeOfEllipseToDraw
        {
            get
            {
                return ellEllipseToDraw.Stroke;
            }
            set
            {
                ellEllipseToDraw.Stroke = value;
            }

        }

        public double strokeThicknessOfEllipseToDraw
        {
            get
            {
                return ellEllipseToDraw.StrokeThickness;
            }
            set
            {
                ellEllipseToDraw.StrokeThickness = value;
            }
        }

        public PenLineJoin strokeLineJoinOfRectangleToDraw
        {
            get
            {
                return ellEllipseToDraw.StrokeLineJoin;
            }
            set
            {
                ellEllipseToDraw.StrokeLineJoin = value;
            }
        }

        public Ellipse ellipseToDraw
        {
            get
            {
                return ellEllipseToDraw;
            }
            set
            {
                ellEllipseToDraw = value;
            }
        }

        public bool isModifiedEllipseMovement
        {
            get
            {
                return _isModifiedEllipseMovement;
            }
            set
            {
                _isModifiedEllipseMovement = value;
            }
        }

        public void resetAppBarButtonEllipseSelectionControl(bool isActivated)
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

        private void rectEllipseForMovement_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
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

        private void rectEllipseForMovement_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
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

            resetAppBarButtonEllipseSelectionControl(true);
            isModifiedEllipseMovement = true;
        }

        public void resetEllipseSelectionControl()
        {
            PocketPaintApplication.GetInstance().PaintingAreaManipulationListener.lastPoint = new Point(0.0, 0.0);

            PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = _ellipseToDrawSize;
            PocketPaintApplication.GetInstance().BarRecEllShape.setBtnWidthValue = _ellipseToDrawSize;

            _transformGridMain.Children.Clear();
            GridMain.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);

            GridMain.Width = _gridMainSize;
            GridMain.Height = _gridMainSize;

            ellEllipseToDraw.Width = _ellipseToDrawSize;
            ellEllipseToDraw.Height = _ellipseToDrawSize;
            rectEllipseForMovement.Width = _rectangleForMovementSize;
            rectEllipseForMovement.Height = _rectangleForMovementSize;

            fillOfEllipseToDraw = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            strokeOfEllipseToDraw = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
            strokeThicknessOfEllipseToDraw = PocketPaintApplication.GetInstance().PaintData.strokeThickness;
            // strokeLineJoinOfRectangleToDraw = PocketPaintApplication.GetInstance().PaintData.penLineJoinSelected;
            isModifiedEllipseMovement = false;
        }
    }
}
