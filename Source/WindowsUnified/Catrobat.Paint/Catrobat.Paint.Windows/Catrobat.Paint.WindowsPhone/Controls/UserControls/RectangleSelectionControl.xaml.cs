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
    public sealed partial class RectangleSelectionControl : UserControl
    {
        TransformGroup _transformGridMain;
        bool _isModifiedRectangleMovement;
        double _rectangleForMovementSize = 200.0;
        double _rectangleToDrawSize = 160.0;
        double _gridMainSize = 230.0;

        public RectangleSelectionControl()
        {
            this.InitializeComponent();

            GridMainSelection.RenderTransform = _transformGridMain = new TransformGroup();

            rectRectangleToDraw.Fill = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            rectRectangleToDraw.Stroke = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
            rectRectangleToDraw.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.strokeThickness;

            isModifiedRectangleMovement = false;
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

        private void ellLeftTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Point deltaTranslation = e.Delta.Translation;
            double deltaTranslationX = deltaTranslation.X * -1.0;
            double deltaTranslationY = deltaTranslation.Y * -1.0;
            changeHeightOfSelection(deltaTranslationY);
            changeWidthOfSelection(deltaTranslationX);
            changeMarginOfGridMainSelection(GridMainSelection.Margin.Left - deltaTranslationX,
                                GridMainSelection.Margin.Top - deltaTranslationY,
                                GridMainSelection.Margin.Right,
                                GridMainSelection.Margin.Bottom);
        }

        private void ellCenterTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Point deltaTranslation = e.Delta.Translation;
            double deltaTranslationY = (deltaTranslation.Y) * -1.0;
            changeHeightOfSelection(deltaTranslationY);
            changeMarginOfGridMainSelection(GridMainSelection.Margin.Left,
                                GridMainSelection.Margin.Top - deltaTranslationY,
                                GridMainSelection.Margin.Right,
                                GridMainSelection.Margin.Bottom);
        }

        private void ellRightTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Point deltaTranslation = e.Delta.Translation;
            double deltaTranslationX = deltaTranslation.X;
            double deltaTranslationY = deltaTranslation.Y * -1.0;
            changeHeightOfSelection(deltaTranslationY);
            changeWidthOfSelection(deltaTranslationX);
            changeMarginOfGridMainSelection(GridMainSelection.Margin.Left,
                                GridMainSelection.Margin.Top - deltaTranslationY,
                                GridMainSelection.Margin.Right - deltaTranslationX,
                                GridMainSelection.Margin.Bottom);
        }

        private void ellRightCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Point deltaTranslation = e.Delta.Translation;
            changeWidthOfSelection(deltaTranslation.X);
            changeMarginOfGridMainSelection(GridMainSelection.Margin.Left,
                                            GridMainSelection.Margin.Top,
                                            GridMainSelection.Margin.Right - deltaTranslation.X,
                                            GridMainSelection.Margin.Bottom);
        }

        private void ellRightBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Point deltaTranslation = e.Delta.Translation;
            double deltaTranslationX = deltaTranslation.X;
            changeHeightOfSelection(deltaTranslation.Y);
            changeWidthOfSelection(deltaTranslationX);
            changeMarginOfGridMainSelection(GridMainSelection.Margin.Left,
                                GridMainSelection.Margin.Top,
                                GridMainSelection.Margin.Right - deltaTranslationX,
                                GridMainSelection.Margin.Bottom - deltaTranslation.Y);
        }

        private void ellCenterBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Point deltaTranslation = e.Delta.Translation;
            changeHeightOfSelection(deltaTranslation.Y);
            changeMarginOfGridMainSelection(GridMainSelection.Margin.Left,
                                GridMainSelection.Margin.Top,
                                GridMainSelection.Margin.Right,
                                GridMainSelection.Margin.Bottom - deltaTranslation.Y);
        }

        private void ellLeftBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Point deltaTranslation = e.Delta.Translation;
            double deltaTranslationX = (deltaTranslation.X) * -1.0;
            changeHeightOfSelection(deltaTranslation.Y);
            changeWidthOfSelection(deltaTranslationX);
            changeMarginOfGridMainSelection(GridMainSelection.Margin.Left - deltaTranslationX,
                                GridMainSelection.Margin.Top,
                                GridMainSelection.Margin.Right,
                                GridMainSelection.Margin.Bottom - deltaTranslation.Y);
        }

        private void ellLeftCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Point deltaTranslation = e.Delta.Translation;
            double deltaTranslationX = (deltaTranslation.X) * -1.0;
            changeWidthOfSelection(deltaTranslationX);
            changeMarginOfGridMainSelection(GridMainSelection.Margin.Left - deltaTranslationX,
                                GridMainSelection.Margin.Top,
                                GridMainSelection.Margin.Right,
                                GridMainSelection.Margin.Bottom);
        }

        private void rectEllipseForMovement_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            TranslateTransform translateTransform = new TranslateTransform();

            TranslateTransform lastTranslateTransform = getLastTranslateTransformation();
            RotateTransform lastRotateTransform = getLastRotateTransformation();

            double xVal = 0.0;
            double yVal = 0.0;

            switch (PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation)
            {
                case 0:
                    xVal = e.Delta.Translation.X;
                    yVal = e.Delta.Translation.Y;
                    break;
                case 90:
                    xVal = e.Delta.Translation.Y;
                    yVal = -e.Delta.Translation.X;
                    break;
                case 180:
                    xVal = -e.Delta.Translation.X;
                    yVal = -e.Delta.Translation.Y;
                    break;
                case 270:
                    xVal = -e.Delta.Translation.Y;
                    yVal = e.Delta.Translation.X;
                    break;
            }

            if (lastTranslateTransform != null && lastRotateTransform == null)
            {
                translateTransform.X = xVal + lastTranslateTransform.X;
                translateTransform.Y = yVal + lastTranslateTransform.Y;
            }
            else if (lastTranslateTransform == null && lastRotateTransform != null)
            {
                translateTransform.X = xVal;
                translateTransform.Y = yVal;
            }
            else if (lastTranslateTransform != null && lastRotateTransform != null)
            {
                translateTransform.X = xVal + lastTranslateTransform.X;
                translateTransform.Y = yVal + lastTranslateTransform.Y;
            }
            else
            {
                translateTransform.X = xVal;
                translateTransform.Y = yVal;
            }
            addTransformation(translateTransform);
        }

        private void rectEllipseForMovement_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Point centerCoordinate = getCenterCoordinateOfGridMain();

            PocketPaintApplication.GetInstance().ToolCurrent.Draw(centerCoordinate);
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

            double marginOffsetX = GridMainSelection.Margin.Left - GridMainSelection.Margin.Right;
            double marginOffsetY = GridMainSelection.Margin.Top - GridMainSelection.Margin.Bottom;

            double coordinateX = offsetX + halfScreenWidth + (marginOffsetX / 2.0);
            double coordinateY = offsetY + halfScreenHeight + (marginOffsetY / 2.0);

            return new Point(coordinateX, coordinateY);
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

        public void resetAppBarButtonRectangleSelectionControl(bool isActivated)
        {
            AppBarButton appBarButtonReset = PocketPaintApplication.GetInstance().PaintingAreaView.getAppBarResetButton();
            if (appBarButtonReset != null)
            {
                appBarButtonReset.IsEnabled = isActivated;
            }
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

        private void changeHeightOfSelection(double valueToAdd)
        {
            GridMainSelection.Height += valueToAdd;
            rectRectangleForMovement.Height += valueToAdd;
            rectRectangleToDraw.Height += valueToAdd;
        }

        private void changeWidthOfSelection(double valueToAdd)
        {
            GridMainSelection.Width += valueToAdd;
            rectRectangleForMovement.Width += valueToAdd;
            rectRectangleToDraw.Width += valueToAdd;
        }

        private void changeMarginOfGridMainSelection(double leftMargin, double topMargin, double rightMargin, double bottomMargin)
        {
            GridMainSelection.Margin = new Thickness(leftMargin, topMargin, rightMargin, bottomMargin);
        }

        public void resetRectangleSelectionControl()
        {
            PocketPaintApplication.GetInstance().PaintingAreaManipulationListener.lastPoint = new Point(0.0, 0.0);

            PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = _rectangleToDrawSize;
            PocketPaintApplication.GetInstance().BarRecEllShape.setBtnWidthValue = _rectangleToDrawSize;

            _transformGridMain.Children.Clear();
            GridMainSelection.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);

            GridMainSelection.Width = _gridMainSize;
            GridMainSelection.Height = _gridMainSize;

            rectRectangleToDraw.Width = _rectangleToDrawSize;
            rectRectangleToDraw.Height = _rectangleToDrawSize;
            rectRectangleForMovement.Width = _rectangleForMovementSize;
            rectRectangleForMovement.Height = _rectangleForMovementSize;

            rectRectangleToDraw.Fill = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            rectRectangleToDraw.Stroke = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
            rectRectangleToDraw.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.strokeThickness;
            // strokeLineJoinOfRectangleToDraw = PocketPaintApplication.GetInstance().PaintData.penLineJoinSelected;
            isModifiedRectangleMovement = false;
        }
    }
}
