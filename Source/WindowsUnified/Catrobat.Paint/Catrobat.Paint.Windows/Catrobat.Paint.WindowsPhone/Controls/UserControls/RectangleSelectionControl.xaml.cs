using System;
using System.Linq;
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
        TransformGroup m_transformGridMain;
        bool _isModifiedRectangleMovement;
        double _rectangleForMovementSize = 200.0;
        double _rectangleToDrawSize = 160.0;
        double _gridMainSize = 290.0;
        public Grid MainGrid = null;

        private float m_rotation;
        private readonly double m_minWidthRectangleToDraw = 20;
        private readonly double m_minHeightRectangleToDraw = 20;

        private readonly double m_defaultWidthGridMainSelection;
        private readonly double m_defaultHeightGridMainSelection;

        public RectangleSelectionControl()
        {
            this.InitializeComponent();

            GridMainSelection.RenderTransform = m_transformGridMain = new TransformGroup();
            MainGrid = GridMainSelection;

            rectRectangleToDraw.Fill = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            rectRectangleToDraw.Stroke = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
            rectRectangleToDraw.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.strokeThickness;

            isModifiedRectangleMovement = false;

            m_defaultWidthGridMainSelection = GridMainSelection.Width;
            m_defaultHeightGridMainSelection = GridMainSelection.Height;
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

        public Rectangle rectangleForMovement
        {
            get
            {
                return rectRectangleForMovement;
            }
            set
            {
                rectRectangleForMovement = value;
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


        public bool shouldHeightOfSelectionChanged(double heightOfSelection)
        {
            bool result = heightOfSelection >= 1.0 ? true : false;
            return result;
        }
        public bool shouldWidthOfSelectionChanged(double widthOfSelection)
        {
            bool result = widthOfSelection >= 1.0 ? true : false;
            return result;
        }

        private void ellLeftTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Point deltaTranslation = e.Delta.Translation;
            double deltaTranslationX = deltaTranslation.X * -1.0;
            double deltaTranslationY = deltaTranslation.Y * -1.0;
            double newHeightOfRectangleToDraw = rectangleToDraw.Height + deltaTranslationY;

            if (shouldHeightOfSelectionChanged(newHeightOfRectangleToDraw))
            {
                SetHeightOfControl(newHeightOfRectangleToDraw);
                ChangeMarginOfGridMainSelection(GridMainSelection.Margin.Left,
                    GridMainSelection.Margin.Top - deltaTranslationY,
                    GridMainSelection.Margin.Right,
                    GridMainSelection.Margin.Bottom);
                PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = newHeightOfRectangleToDraw;
            }

            double newWidthOfRectangleToDraw = rectangleToDraw.Width + deltaTranslationX;
            if (shouldWidthOfSelectionChanged(newWidthOfRectangleToDraw))
            {
                SetWidthOfControl(newWidthOfRectangleToDraw);
                ChangeMarginOfGridMainSelection(GridMainSelection.Margin.Left - deltaTranslationX,
                                    GridMainSelection.Margin.Top,
                                    GridMainSelection.Margin.Right,
                                    GridMainSelection.Margin.Bottom);
                PocketPaintApplication.GetInstance().BarRecEllShape.setBtnWidthValue = newWidthOfRectangleToDraw;
            }
        }

        private void TopCenterGrid_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Point deltaTranslation = e.Delta.Translation;
            double deltaTranslationY = (deltaTranslation.Y) * -1.0;
            double newHeightOfRectangleToDraw = heightOfRectangleToDraw + deltaTranslationY;
            if (shouldHeightOfSelectionChanged(newHeightOfRectangleToDraw))
            {
                SetHeightOfControl(newHeightOfRectangleToDraw);
                ChangeMarginOfGridMainSelection(GridMainSelection.Margin.Left,
                                    GridMainSelection.Margin.Top - deltaTranslationY,
                                    GridMainSelection.Margin.Right,
                                    GridMainSelection.Margin.Bottom);
                PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = newHeightOfRectangleToDraw;
            }
        }

        private void TopRightGrid_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
           resizeWidth(e.Delta.Translation.X, e.Delta.Translation.Y);

            //double deltaHeight;

            //if (m_rotation > 0)
            //{
            //    deltaHeight = Math.Sqrt(Math.Pow(deltaTranslationX, 2) + Math.Pow(deltaTranslationY, 2));
            //    if (deltaTranslationY < 0)
            //    {
            //        deltaHeight *= -1;
            //    }
            //}
            //else
            //{
            //    deltaHeight = deltaTranslationY;
            //}

            //double newHeightOfRectangleToDraw = rectangleToDraw.Height + deltaHeight;

            //var transform = rectRectangleToDraw.TransformToVisual(GridMainSelection);
            //Point absolutePosition = transform.TransformPoint(new Point(0, 0));

            //absolutePosition.X += rectRectangleToDraw.Width / 2;
            //absolutePosition.Y += rectRectangleToDraw.Height / 2;

            //RotateTransform rt;

            //if (shouldHeightOfSelectionChanged(newHeightOfRectangleToDraw))
            //{
            //    //RotateTransform rt = new RotateTransform { Angle = 0, CenterX = absolutePosition.X, CenterY = absolutePosition.Y };
            //    //addTransformation(rt);

            //    if (m_rotation > 0)
            //    {
            //        SetHeightOfControl(newHeightOfRectangleToDraw);



            //        transform = rectRectangleToDraw.TransformToVisual(GridMainSelection);
            //        absolutePosition = transform.TransformPoint(new Point(0, 0));

            //        absolutePosition.X += rectRectangleToDraw.Width / 2;
            //        absolutePosition.Y += rectRectangleToDraw.Height / 2;

            //        rt = new RotateTransform { Angle = m_rotation, CenterX = absolutePosition.X, CenterY = absolutePosition.Y };
            //        addTransformation(rt);

            //        ChangeMarginOfGridMainSelection(
            //            GridMainSelection.Margin.Left,
            //            GridMainSelection.Margin.Top - deltaHeight / 2,
            //            GridMainSelection.Margin.Right - deltaHeight / 2,
            //            GridMainSelection.Margin.Bottom
            //        );
            //    }
            //    else
            //    {
            //        SetHeightOfControl(newHeightOfRectangleToDraw);
            //        ChangeMarginOfGridMainSelection(
            //            GridMainSelection.Margin.Left,
            //            GridMainSelection.Margin.Top - deltaTranslationY,
            //            GridMainSelection.Margin.Right,
            //            GridMainSelection.Margin.Bottom);
            //    }

            //    PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = newHeightOfRectangleToDraw;
            //}
        }

        private void CenterRightGrid_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {

            resizeWidth(e.Delta.Translation.X, e.Delta.Translation.Y);

            return;
            Point deltaTranslation = e.Delta.Translation;
            double deltaTranslationX = deltaTranslation.X;
            double deltaTranslationY = deltaTranslation.Y;

            double rotationRadian = DegreeToRadian(m_rotation);
            double deltaXCorrected = Math.Cos(-rotationRadian) * (deltaTranslationX)
                    - Math.Sin(-rotationRadian) * (deltaTranslationY);
            double deltaYCorrected = Math.Sin(-rotationRadian) * (deltaTranslationX)
                    + Math.Cos(-rotationRadian) * (deltaTranslationY);

            double deltaWidth = deltaXCorrected;
            //if (m_rotation == 90)
            //{
            //    deltaWidth = deltaTranslationY;
            //}
            //else if (m_rotation > 90 && m_rotation < 270)
            //{
            //    deltaWidth = deltaTranslationX * -1;
            //}
            //else if (m_rotation == 270)
            //{
            //    deltaWidth = deltaTranslationY * -1;
            //}

            double newWidthRectangleToDraw = rectangleToDraw.Width + deltaWidth;
            if (newWidthRectangleToDraw < m_minWidthRectangleToDraw)
            {
                newWidthRectangleToDraw = m_minWidthRectangleToDraw;
            }

            SetWidthOfControl(newWidthRectangleToDraw);
            // TODO: adapt ChangeMarginOfGridMainSelection when newWidthRectangleToDraw is set to minimum width
            // idea: setting of original height/width and calculating it based on this
            ChangeMarginOfGridMainSelection(GridMainSelection.Margin.Left,
                                            GridMainSelection.Margin.Top,
                                            GridMainSelection.Margin.Right - deltaWidth,
                                            GridMainSelection.Margin.Bottom);


            PocketPaintApplication.GetInstance().BarRecEllShape.setBtnWidthValue = newWidthRectangleToDraw;
        }

        private void BottomRightGrid_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            resizeWidth(e.Delta.Translation.X, e.Delta.Translation.Y);

            //Point deltaTranslation = e.Delta.Translation;
            //double deltaTranslationX = deltaTranslation.X;
            //double deltaTranslationY = deltaTranslation.Y;
            //double newHeightOfRectangleToDraw = heightOfRectangleToDraw + deltaTranslationY;

            //if (shouldHeightOfSelectionChanged(newHeightOfRectangleToDraw))
            //{
            //    ChangeMarginOfGridMainSelection(GridMainSelection.Margin.Left,
            //                    GridMainSelection.Margin.Top,
            //                    GridMainSelection.Margin.Right,
            //                    GridMainSelection.Margin.Bottom - deltaTranslation.Y);
            //    SetHeightOfControl(newHeightOfRectangleToDraw);
            //    PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = newHeightOfRectangleToDraw;
            //}
        }

        private void BottomCenterGrid_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Point deltaTranslation = e.Delta.Translation;
            double deltaTranslationY = deltaTranslation.Y;
            double newHeightOfRectangleToDraw = heightOfRectangleToDraw + deltaTranslationY;
            if (shouldHeightOfSelectionChanged(newHeightOfRectangleToDraw))
            {
                SetHeightOfControl(newHeightOfRectangleToDraw);
                ChangeMarginOfGridMainSelection(GridMainSelection.Margin.Left,
                                    GridMainSelection.Margin.Top,
                                    GridMainSelection.Margin.Right,
                                    GridMainSelection.Margin.Bottom - deltaTranslationY);
                PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = newHeightOfRectangleToDraw;
            }
        }

        private void BottomLeftGrid_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Point deltaTranslation = e.Delta.Translation;
            double deltaTranslationX = (deltaTranslation.X) * -1.0;
            double deltaTranslationY = deltaTranslation.Y;
            double newHeightOfRectangleToDraw = heightOfRectangleToDraw + deltaTranslationY;

            if (shouldHeightOfSelectionChanged(newHeightOfRectangleToDraw))
            {
                SetHeightOfControl(newHeightOfRectangleToDraw);
                ChangeMarginOfGridMainSelection(GridMainSelection.Margin.Left,
                                GridMainSelection.Margin.Top,
                                GridMainSelection.Margin.Right,
                                GridMainSelection.Margin.Bottom - deltaTranslationY);
                PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = newHeightOfRectangleToDraw;
            }

            double newWidthOfRectangleToDraw = rectangleToDraw.Width + deltaTranslationX;
            if (shouldWidthOfSelectionChanged(newWidthOfRectangleToDraw))
            {
                SetWidthOfControl(rectangleToDraw.Width + deltaTranslationX);
                ChangeMarginOfGridMainSelection(GridMainSelection.Margin.Left - deltaTranslationX,
                                    GridMainSelection.Margin.Top,
                                    GridMainSelection.Margin.Right,
                                    GridMainSelection.Margin.Bottom);
                PocketPaintApplication.GetInstance().BarRecEllShape.setBtnWidthValue = newWidthOfRectangleToDraw;
            }
        }

        private void CenterLeftGrid_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Point deltaTranslation = e.Delta.Translation;
            double deltaTranslationX = (deltaTranslation.X) * -1.0;
            double newWidthOfRectangleToDraw = rectangleToDraw.Width + deltaTranslationX;
            if (shouldWidthOfSelectionChanged(newWidthOfRectangleToDraw))
            {
                SetWidthOfControl(rectangleToDraw.Width + deltaTranslationX);
                ChangeMarginOfGridMainSelection(GridMainSelection.Margin.Left - deltaTranslationX,
                                    GridMainSelection.Margin.Top,
                                    GridMainSelection.Margin.Right,
                                    GridMainSelection.Margin.Bottom);
                PocketPaintApplication.GetInstance().BarRecEllShape.setBtnWidthValue = newWidthOfRectangleToDraw;
            }
        }

        private void rectEllipseForMovement_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            TranslateTransform translateTransform = new TranslateTransform();

            TranslateTransform lastTranslateTransform = getLastTranslateTransformation();
            RotateTransform lastRotateTransform = getLastRotateTransformation();

            double xVal = e.Delta.Translation.X;
            double yVal = e.Delta.Translation.Y;           

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
            var coord = e.GetPosition(PocketPaintApplication.GetInstance().GridWorkingSpace);
            var coord2 = e.GetPosition(rectangleToDraw);

            var angle = PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation;

            switch(angle)
            {
                case 0:
                    coord.X -= coord2.X;
                    coord.Y -= coord2.Y;
                    coord.X += (rectangleToDraw.Width / 2);
                    coord.Y += (rectangleToDraw.Height / 2);
                    break;
                case 90:
                    coord.X -= coord2.Y;
                    coord.Y -= (rectangleToDraw.Width - coord2.X);
                    coord.X += (rectangleToDraw.Width / 2);
                    coord.Y += (rectangleToDraw.Height / 2);
                    break;
                case 180:
                    coord.X += coord2.X;
                    coord.Y += coord2.Y;
                    coord.X -= (rectangleToDraw.Width / 2);
                    coord.Y -= (rectangleToDraw.Height / 2);
                    break;
                case 270:
                    coord.X -= (rectangleToDraw.Height - coord2.Y);
                    coord.Y -= coord2.X;
                    coord.X += (rectangleToDraw.Width / 2);
                    coord.Y += (rectangleToDraw.Height / 2);
                    break;
            }
            

            
            PocketPaintApplication.GetInstance().ToolCurrent.Draw(coord);
        }

        private void resizeWidth(double deltaX, double deltaY)
        {
            double rotationRadian = DegreeToRadian(m_rotation);
            double deltaXCorrected = Math.Cos(-rotationRadian) * (deltaX)
                    - Math.Sin(-rotationRadian) * (deltaY);
            //double deltaYCorrected = Math.Sin(-rotationRadian) * (deltaTranslationX)
            //        + Math.Cos(-rotationRadian) * (deltaTranslationY);

            double newWidthRectangleToDraw = rectangleToDraw.Width + deltaXCorrected;
            if (newWidthRectangleToDraw < m_minWidthRectangleToDraw)
            {
                newWidthRectangleToDraw = m_minWidthRectangleToDraw;
                deltaXCorrected = m_minWidthRectangleToDraw - rectangleToDraw.Width;
            }

            // TODO: maximum height

            SetWidthOfControl(newWidthRectangleToDraw);
            ChangeMarginOfGridMainSelection(GridMainSelection.Margin.Left,
                                            GridMainSelection.Margin.Top,
                                            GridMainSelection.Margin.Right - deltaXCorrected,
                                            GridMainSelection.Margin.Bottom);

            PocketPaintApplication.GetInstance().BarRecEllShape.setBtnWidthValue = newWidthRectangleToDraw;
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
            for (int i = 0; i < m_transformGridMain.Children.Count; i++)
            {
                if (m_transformGridMain.Children[i].GetType() == currentTransform.GetType())
                {
                    m_transformGridMain.Children.RemoveAt(i);
                }
            }

            m_transformGridMain.Children.Add(currentTransform);

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
            for (int i = 0; i < m_transformGridMain.Children.Count; i++)
            {
                if (m_transformGridMain.Children[i].GetType() == typeof(TranslateTransform))
                {
                    TranslateTransform translateTransform = new TranslateTransform();
                    translateTransform.X = ((TranslateTransform)m_transformGridMain.Children[i]).X;
                    translateTransform.Y = ((TranslateTransform)m_transformGridMain.Children[i]).Y;

                    return translateTransform;
                }
            }

            return null;
        }

        public RotateTransform getLastRotateTransformation()
        {
            for (int i = 0; i < m_transformGridMain.Children.Count; i++)
            {
                if (m_transformGridMain.Children[i].GetType() == typeof(RotateTransform))
                {
                    RotateTransform rotateTransform = new RotateTransform();
                    rotateTransform.CenterX = ((RotateTransform)m_transformGridMain.Children[i]).CenterX;
                    rotateTransform.CenterY = ((RotateTransform)m_transformGridMain.Children[i]).CenterY;
                    rotateTransform.Angle = ((RotateTransform)m_transformGridMain.Children[i]).Angle;

                    return rotateTransform;
                }
            }

            return null;
        }

        public void SetHeightOfControl(double newHeightRectangleToDraw)
        {
            //TODO 1 is maybe too small?
            if (newHeightRectangleToDraw >= 1)
            {
                GridMainSelection.Height = newHeightRectangleToDraw + (GridMainSelection.Height - rectangleToDraw.Height);
                rectRectangleForMovement.Height = newHeightRectangleToDraw + (rectRectangleForMovement.Height - rectangleToDraw.Height);
                rectRectangleToDraw.Height = newHeightRectangleToDraw;
            }
        }

        public double heightOfRectangleToDraw
        {
            get
            {
                return rectangleToDraw.Height;
            }
            set
            {
                rectangleToDraw.Height = value;
            }
            
        }

        public void SetWidthOfControl(double newWidthRectangleToDraw)
        {
            //TODO 1 is maybe too small?
            if (newWidthRectangleToDraw >= 1)
            {
                GridMainSelection.Width = newWidthRectangleToDraw + (GridMainSelection.Width - rectangleToDraw.Width);
                rectRectangleForMovement.Width = newWidthRectangleToDraw + (rectRectangleForMovement.Width - rectangleToDraw.Width);
                rectRectangleToDraw.Width = newWidthRectangleToDraw;
            }
        }
        public double widthOfRectangleToDraw
        {
            get
            {
                return rectangleToDraw.Width;
            }
            set
            {
                rectangleToDraw.Width = value;
            }
        }

        public Boolean HitRotationArrow(Point point)
        {

            System.Collections.Generic.IEnumerable<UIElement> hitElements =
                VisualTreeHelper.FindElementsInHostCoordinates(point, null);

            var hitUiElements = hitElements as UIElement[] ?? hitElements.ToArray();
            if (hitUiElements.Contains(rotationArrowTopRight) || 
                hitUiElements.Contains(rotationArrowTopLeft) || 
                hitUiElements.Contains(rotationArrowBottomLeft) || 
                hitUiElements.Contains(rotationArrowBottomRight))
            {
                return true;
            }

            return false;
        }

        private void ChangeMarginOfGridMainSelection(double leftMargin, double topMargin, double rightMargin, double bottomMargin)
        {        
            GridMainSelection.Margin = new Thickness(leftMargin, topMargin, rightMargin, bottomMargin);
        }

        public void resetRectangleSelectionControl()
        {
            PocketPaintApplication.GetInstance().PaintingAreaManipulationListener.lastPoint = new Point(0.0, 0.0);

            PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = _rectangleToDrawSize;
            PocketPaintApplication.GetInstance().BarRecEllShape.setBtnWidthValue = _rectangleToDrawSize;

            m_transformGridMain.Children.Clear();
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
            RotateTransform rotate = new RotateTransform();
            var angle = PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation;
            rotate.Angle = -angle;
            Point point = new Point(0.5, 0.5);
            PocketPaintApplication.GetInstance().RectangleSelectionControl.RenderTransformOrigin = point;
            PocketPaintApplication.GetInstance().RectangleSelectionControl.RenderTransform = rotate;

            isModifiedRectangleMovement = false;
            m_rotation = 0;
        }

        public Brush fillOfRectangleToDraw
        {
            get
            {
                return rectangleToDraw.Fill;
            }
            set
            {
                rectangleToDraw.Fill = value;
            }
        }

        public Brush strokeOfRectangleToDraw
        {
            get
            {
                return rectangleToDraw.Stroke;
            }
            set
            {
                rectangleToDraw.Stroke = value;
            }
        }

        public double strokeThicknessOfRectangleToDraw
        {
            get
            {
                return rectangleToDraw.StrokeThickness;
            }
            set
            {
                rectangleToDraw.StrokeThickness = value;
            }
        }

        private void TopLeftGrid_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Point deltaTranslation = e.Delta.Translation;
            double deltaTranslationX = deltaTranslation.X * -1.0;
            double deltaTranslationY = deltaTranslation.Y * -1.0;
            double newHeightOfRectangleToDraw = rectangleToDraw.Height + deltaTranslationY;

            if (shouldHeightOfSelectionChanged(newHeightOfRectangleToDraw))
            {
                SetHeightOfControl(newHeightOfRectangleToDraw);
                ChangeMarginOfGridMainSelection(GridMainSelection.Margin.Left,
                    GridMainSelection.Margin.Top - deltaTranslationY,
                    GridMainSelection.Margin.Right,
                    GridMainSelection.Margin.Bottom);
                PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = newHeightOfRectangleToDraw;
            }

            double newWidthOfRectangleToDraw = rectangleToDraw.Width + deltaTranslationX;
            if (shouldWidthOfSelectionChanged(newWidthOfRectangleToDraw))
            {
                SetWidthOfControl(newWidthOfRectangleToDraw);
                ChangeMarginOfGridMainSelection(GridMainSelection.Margin.Left - deltaTranslationX,
                                    GridMainSelection.Margin.Top,
                                    GridMainSelection.Margin.Right,
                                    GridMainSelection.Margin.Bottom);
                PocketPaintApplication.GetInstance().BarRecEllShape.setBtnWidthValue = newWidthOfRectangleToDraw;
            }
        }

        private void RotationArrow_OnManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("RotationArrowTopRight_OnManipulationStarted");
            return;
            var transform = rectRectangleToDraw.TransformToVisual(GridMainSelection);
            Point absolutePosition = transform.TransformPoint(new Point(0, 0));

            absolutePosition.X += rectRectangleToDraw.Width / 2;
            absolutePosition.Y += rectRectangleToDraw.Height / 2;
            m_rotation = (m_rotation + 45)%360;

            RotateTransform rt = new RotateTransform { Angle = m_rotation, CenterX = absolutePosition.X, CenterY = absolutePosition.Y };
            addTransformation(rt);

        }

        private void RotationArrow_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("RotationArrowTopRight_OnManipulationDelta");

            Point delta = new Point(e.Delta.Translation.X, e.Delta.Translation.Y);

            //var transform = rectRectangleToDraw.TransformToVisual(DrawingGrid);
            //Point absolutePosition = transform.TransformPoint(new Point(0, 0));
            var transform = rectRectangleToDraw.TransformToVisual(GridMainSelection);
            Point absolutePosition = transform.TransformPoint(new Point(0, 0));

            absolutePosition.X += rectRectangleToDraw.Width / 2;
            absolutePosition.Y += rectRectangleToDraw.Height / 2;

            Point currentPoint = e.Position;

            double previousXLength = currentPoint.X - e.Delta.Translation.X;
            double previousYLength = currentPoint.Y - e.Delta.Translation.Y;
            double currentXLength = currentPoint.X;
            double currentYLength = currentPoint.Y;

            double rotationAnglePrevious = Math.Atan2(previousYLength, previousXLength);
            double rotationAngleCurrent = Math.Atan2(currentYLength, currentXLength);
            double deltaAngle = -(rotationAnglePrevious - rotationAngleCurrent);

            m_rotation += (float) RadianToDegree(deltaAngle) ;
            m_rotation = m_rotation % 360;

            //if (m_rotation > 180)
            //    m_rotation = -180 + (m_rotation - 180);

            while (m_rotation < 0)
            {
                m_rotation += 360;
            }

            System.Diagnostics.Debug.WriteLine(e.Position);

            //private void rotate(float deltaX, float deltaY) {
            //    if (mDrawingBitmap == null) {
            //        return;
            //    }

            //    PointF currentPoint = new PointF(mPreviousEventCoordinate.x, mPreviousEventCoordinate.y);

            //    double previousXLength = mPreviousEventCoordinate.x - deltaX - mToolPosition.x;
            //    double previousYLength = mPreviousEventCoordinate.y - deltaY - mToolPosition.y;
            //    double currentXLength = currentPoint.x - mToolPosition.x;
            //    double currentYLength = currentPoint.y - mToolPosition.y;

            //    double rotationAnglePrevious = Math.atan2(previousYLength, previousXLength);
            //    double rotationAngleCurrent = Math.atan2(currentYLength, currentXLength);
            //    double deltaAngle = -(rotationAnglePrevious - rotationAngleCurrent);

            //    mBoxRotation += (float) Math.toDegrees(deltaAngle) + 360;
            //    mBoxRotation = mBoxRotation % 360;
            //    if (mBoxRotation > 180)
            //        mBoxRotation = -180 + (mBoxRotation - 180);
            //} 

            //foreach (Transform t in m_transformGridMain.Children)
            //{
            //    if (t is RotateTransform)
            //    {
            //        var angle = (t as RotateTransform).Angle;
            //        (t as RotateTransform).Angle = (angle + 90) % 360;
            //        return;
            //    }
            //}




            RotateTransform rt = new RotateTransform { Angle = m_rotation, CenterX = absolutePosition.X, CenterY = absolutePosition.Y };
            addTransformation(rt);
        }

        private void RotationArrow_OnManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("RotationArrowTopRight_OnManipulationCompleted");


        }

        private double RadianToDegree(double angle)
        {
            return angle * 180.0 / Math.PI;
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
}
