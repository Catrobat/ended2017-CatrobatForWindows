using System;
using System.Diagnostics;
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
    public sealed partial class RectangleShapeBaseControl : UserControl
    {
        TransformGroup m_transformGridMain;
        double m_gridMainSize = 290.0;
        double m_rectangleForMovementSize = 200.0;
        double m_areaToDrawSize = 160.0;
        

        private float m_rotation;
        private readonly double m_minWidthRectangleToDraw = 20;
        private readonly double m_minHeightRectangleToDraw = 20;


        //public Grid MainGrid { get; private set; }
        public Grid AreaToDraw { get; private set; }

    private enum Orientation
        {
            Top, Bottom, Left, Right    
        }

        public RectangleShapeBaseControl()
        {
            this.InitializeComponent();

            GridMainSelection.RenderTransform = m_transformGridMain = new TransformGroup();
            //MainGrid = GridMainSelection;
            AreaToDraw = AreaToDrawGrid;

            IsModifiedRectangleForMovement = false;
        }

        public bool IsModifiedRectangleForMovement { get; set; }

        //public Rectangle rectangleForMovement
        //{
        //    get
        //    {
        //        return rectRectangleForMovement;
        //    }
        //    set
        //    {
        //        rectRectangleForMovement = value;
        //    }
        //}

        //public PenLineJoin strokeLineJoinOfRectangleToDraw
        //{
        //    get
        //    {
        //        return rectRectangleToDraw.StrokeLineJoin;
        //    }
        //    set
        //    {
        //        rectRectangleToDraw.StrokeLineJoin = value;
        //    }
        //}


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

        private void TopCenterGrid_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            resizeHeight(e.Delta.Translation.X, e.Delta.Translation.Y, Orientation.Top);
        }

        private void TopRightGrid_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
           resizeHeight(e.Delta.Translation.X, e.Delta.Translation.Y, Orientation.Top);
           resizeWidth(e.Delta.Translation.X, e.Delta.Translation.Y, Orientation.Right);
        }

        private void CenterRightGrid_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            resizeWidth(e.Delta.Translation.X, e.Delta.Translation.Y, Orientation.Right);
        }

        private void BottomRightGrid_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            resizeHeight(e.Delta.Translation.X, e.Delta.Translation.Y, Orientation.Bottom);
            resizeWidth(e.Delta.Translation.X, e.Delta.Translation.Y, Orientation.Right);
        }

        private void BottomCenterGrid_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            resizeHeight(e.Delta.Translation.X, e.Delta.Translation.Y, Orientation.Bottom);
        }

        private void BottomLeftGrid_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            resizeHeight(e.Delta.Translation.X, e.Delta.Translation.Y, Orientation.Bottom);
            resizeWidth(e.Delta.Translation.X, e.Delta.Translation.Y, Orientation.Left);
        }

        private void CenterLeftGrid_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            resizeWidth(e.Delta.Translation.X, e.Delta.Translation.Y, Orientation.Left);
        }

        private void rectEllipseForMovement_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            TranslateTransform translateTransform = new TranslateTransform();

            TranslateTransform lastTranslateTransform = getLastTranslateTransformation();
            RotateTransform lastRotateTransform = GetLastRotateTransformation();

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
            //var coord = e.GetPosition(PocketPaintApplication.GetInstance().GridWorkingSpace);
            //var coord2 = e.GetPosition(rectangleToDraw);

            //var angle = PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation;

            //switch(angle)
            //{
            //    case 0:
            //        coord.X -= coord2.X;
            //        coord.Y -= coord2.Y;
            //        coord.X += (rectangleToDraw.Width / 2);
            //        coord.Y += (rectangleToDraw.Height / 2);
            //        break;
            //    case 90:
            //        coord.X -= coord2.Y;
            //        coord.Y -= (rectangleToDraw.Width - coord2.X);
            //        coord.X += (rectangleToDraw.Width / 2);
            //        coord.Y += (rectangleToDraw.Height / 2);
            //        break;
            //    case 180:
            //        coord.X += coord2.X;
            //        coord.Y += coord2.Y;
            //        coord.X -= (rectangleToDraw.Width / 2);
            //        coord.Y -= (rectangleToDraw.Height / 2);
            //        break;
            //    case 270:
            //        coord.X -= (rectangleToDraw.Height - coord2.Y);
            //        coord.Y -= coord2.X;
            //        coord.X += (rectangleToDraw.Width / 2);
            //        coord.Y += (rectangleToDraw.Height / 2);
            //        break;
            //}
            

            
            //PocketPaintApplication.GetInstance().ToolCurrent.Draw(coord);
        }

        private void resizeWidth(double deltaX, double deltaY, Orientation orientation)
        {
            Debug.Assert(orientation == Orientation.Left || orientation == Orientation.Right);

            double rotationRadian = PocketPaintApplication.DegreeToRadian(m_rotation);
            double deltaXCorrected = Math.Cos(-rotationRadian) * (deltaX)
                    - Math.Sin(-rotationRadian) * (deltaY);
            //double deltaYCorrected = Math.Sin(-rotationRadian) * (deltaX)
            //        + Math.Cos(-rotationRadian) * (deltaY);

            if (orientation == Orientation.Left)
            {
                deltaXCorrected = deltaXCorrected * -1;
            }



            double newWidthRectangleToDraw = AreaToDrawGrid.Width + deltaXCorrected;
            if (newWidthRectangleToDraw < m_minWidthRectangleToDraw)
            {
                newWidthRectangleToDraw = m_minWidthRectangleToDraw;
                deltaXCorrected = m_minWidthRectangleToDraw - AreaToDrawGrid.Width;
            }

            // TODO: maximum width?

            SetWidthOfControl(newWidthRectangleToDraw);

            double deltaXLeft = 0;
            double deltaXRight = 0;
            if (orientation == Orientation.Left)
            {
                deltaXLeft = deltaXCorrected;
                //deltaXRight = deltaXCorrected * -1;
            }
            else if (orientation == Orientation.Right)
            {
                deltaXRight = deltaXCorrected;
            }
            ChangeMarginOfGridMainSelection(GridMainSelection.Margin.Left - deltaXLeft,
                                            GridMainSelection.Margin.Top,
                                            GridMainSelection.Margin.Right - deltaXRight,
                                            GridMainSelection.Margin.Bottom);

            //var transform = rectRectangleToDraw.TransformToVisual(GridMainSelection);
            //Point absolutePosition = transform.TransformPoint(new Point(0, 0));

            //absolutePosition.X += rectRectangleToDraw.Width / 2;
            //absolutePosition.Y += rectRectangleToDraw.Height / 2;

            //RotateTransform rt;

            //rt = new RotateTransform { Angle = m_rotation, CenterX = absolutePosition.X, CenterY = absolutePosition.Y };
            //addTransformation(rt);

            PocketPaintApplication.GetInstance().BarRecEllShape.setBtnWidthValue = newWidthRectangleToDraw;
        }

        private void resizeHeight(double deltaX, double deltaY, Orientation orientation)
        {
            Debug.Assert(orientation == Orientation.Top || orientation == Orientation.Bottom);

            double rotationRadian = PocketPaintApplication.DegreeToRadian(m_rotation);
            //double deltaXCorrected = Math.Cos(-rotationRadian) * (deltaX)
            //        - Math.Sin(-rotationRadian) * (deltaY);
            double deltaYCorrected = Math.Sin(-rotationRadian) * (deltaX)
                    + Math.Cos(-rotationRadian) * (deltaY);

            if (orientation == Orientation.Top)
            {
                deltaYCorrected = deltaYCorrected * -1;
            }

            double newHeightRectangleToDraw = AreaToDrawGrid.Height + deltaYCorrected;
            if (newHeightRectangleToDraw < m_minHeightRectangleToDraw)
            {
                newHeightRectangleToDraw = m_minHeightRectangleToDraw;
                deltaYCorrected = m_minHeightRectangleToDraw - AreaToDrawGrid.Height;
            }

            // TODO: maximum height?

            SetHeightOfControl(newHeightRectangleToDraw);

            double deltaYTop = 0;
            double deltaYBottom = 0;
            if (orientation == Orientation.Top)
            {
                deltaYTop = deltaYCorrected;
            }
            else if (orientation == Orientation.Bottom)
            {
                deltaYBottom = deltaYCorrected;
            }
            ChangeMarginOfGridMainSelection(GridMainSelection.Margin.Left,
                                            GridMainSelection.Margin.Top - deltaYTop,
                                            GridMainSelection.Margin.Right,
                                            GridMainSelection.Margin.Bottom - deltaYBottom);

            //var transform = rectRectangleToDraw.TransformToVisual(GridMainSelection);
            //Point absolutePosition = transform.TransformPoint(new Point(0, 0));

            //absolutePosition.X += rectRectangleToDraw.Width / 2;
            //absolutePosition.Y += rectRectangleToDraw.Height / 2;

            //RotateTransform rt;

            //rt = new RotateTransform { Angle = m_rotation, CenterX = absolutePosition.X, CenterY = absolutePosition.Y };
            //addTransformation(rt);

            PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = newHeightRectangleToDraw;
        }

        public Point getCenterCoordinateOfGridMain()
        {
            double halfScreenHeight = Window.Current.Bounds.Height / 2.0;
            double halfScreenWidth = Window.Current.Bounds.Width / 2.0;

            TranslateTransform lastTranslateTransform = getLastTranslateTransformation();
            RotateTransform lastRotateTransform = GetLastRotateTransformation();

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
            IsModifiedRectangleForMovement = true;
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

        public RotateTransform GetLastRotateTransformation()
        {
            foreach (Transform t in m_transformGridMain.Children)
            {
                if (t is RotateTransform)
                {
                    RotateTransform rotateTransform = new RotateTransform
                    {
                        CenterX = ((RotateTransform) t).CenterX,
                        CenterY = ((RotateTransform) t).CenterY,
                        Angle = ((RotateTransform) t).Angle
                    };

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
                GridMainSelection.Height = newHeightRectangleToDraw + (GridMainSelection.Height - AreaToDrawGrid.Height);
                rectRectangleForMovement.Height = newHeightRectangleToDraw + (rectRectangleForMovement.Height - AreaToDrawGrid.Height);
                AreaToDrawGrid.Height = newHeightRectangleToDraw;
            }
        }

        public double heightOfRectangleToDraw
        {
            get
            {
                return AreaToDrawGrid.Height;
            }
            set
            {
                AreaToDrawGrid.Height = value;
            }
            
        }

        public void SetWidthOfControl(double newWidthRectangleToDraw)
        {
            //TODO 1 is maybe too small?
            if (newWidthRectangleToDraw >= 1)
            {
                GridMainSelection.Width = newWidthRectangleToDraw + (GridMainSelection.Width - AreaToDrawGrid.Width);
                rectRectangleForMovement.Width = newWidthRectangleToDraw + (rectRectangleForMovement.Width - AreaToDrawGrid.Width);
                AreaToDrawGrid.Width = newWidthRectangleToDraw;
            }
        }
        public double widthOfRectangleToDraw
        {
            get
            {
                return AreaToDrawGrid.Width;
            }
            set
            {
                AreaToDrawGrid.Width = value;
            }
        }

        private void ChangeMarginOfGridMainSelection(double leftMargin, double topMargin, double rightMargin, double bottomMargin)
        {        
            GridMainSelection.Margin = new Thickness(leftMargin, topMargin, rightMargin, bottomMargin);
        }

        public void ResetRectangleShapeBaseControl()
        {
            PocketPaintApplication.GetInstance().PaintingAreaManipulationListener.lastPoint = new Point(0.0, 0.0);

            PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = m_areaToDrawSize;
            PocketPaintApplication.GetInstance().BarRecEllShape.setBtnWidthValue = m_areaToDrawSize;

            m_transformGridMain.Children.Clear();
            GridMainSelection.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);

            GridMainSelection.Width = m_gridMainSize;
            GridMainSelection.Height = m_gridMainSize;

            AreaToDrawGrid.Width = m_areaToDrawSize;
            AreaToDrawGrid.Height = m_areaToDrawSize;
            rectRectangleForMovement.Width = m_rectangleForMovementSize;
            rectRectangleForMovement.Height = m_rectangleForMovementSize;

            RotateTransform rotate = new RotateTransform();
            var angle = PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation;
            rotate.Angle = -angle;
            Point point = new Point(0.5, 0.5);
            PocketPaintApplication.GetInstance().RectangleSelectionControl.RenderTransformOrigin = point;
            PocketPaintApplication.GetInstance().RectangleSelectionControl.RenderTransform = rotate;

            IsModifiedRectangleForMovement = false;
            m_rotation = 0;
        }

        // TODO: adapt
        //public Brush fillOfRectangleToDraw
        //{
        //    get
        //    {
        //        return rectangleToDraw.Fill;
        //    }
        //    set
        //    {
        //        rectangleToDraw.Fill = value;
        //    }
        //}

        //public Brush strokeOfRectangleToDraw
        //{
        //    get
        //    {
        //        return rectangleToDraw.Stroke;
        //    }
        //    set
        //    {
        //        rectangleToDraw.Stroke = value;
        //    }
        //}

        //public double strokeThicknessOfRectangleToDraw
        //{
        //    get
        //    {
        //        return rectangleToDraw.StrokeThickness;
        //    }
        //    set
        //    {
        //        rectangleToDraw.StrokeThickness = value;
        //    }
        //}

        private void TopLeftGrid_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            resizeHeight(e.Delta.Translation.X, e.Delta.Translation.Y, Orientation.Top);
            resizeWidth(e.Delta.Translation.X, e.Delta.Translation.Y, Orientation.Left);
        }

        private void RotationTopRight_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("RotationArrowTopRight_OnManipulationDelta");
            Rotate(e.Position, e.Delta.Translation.X, e.Delta.Translation.Y, Orientation.Right);
        }

        private void RotationTopLeft_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Rotate(e.Position, e.Delta.Translation.X, e.Delta.Translation.Y, Orientation.Left);
        }

        private void Rotate(Point position, double deltaX, double deltaY, Orientation orientation)
        {

            //var transform = rectRectangleToDraw.TransformToVisual(DrawingGrid);
            //Point absolutePosition = transform.TransformPoint(new Point(0, 0));
            // 
            var transform = AreaToDrawGrid.TransformToVisual(GridMainSelection);
            Point absolutePosition = transform.TransformPoint(new Point(0, 0));

            absolutePosition.X += AreaToDrawGrid.Width / 2;
            absolutePosition.Y += AreaToDrawGrid.Height / 2;

            double previousXLength = position.X - deltaX;
            double previousYLength = position.Y - deltaY;
            double currentXLength = position.X;
            double currentYLength = position.Y;

            double rotationAnglePrevious = Math.Atan2(previousYLength, previousXLength);
            double rotationAngleCurrent = Math.Atan2(currentYLength, currentXLength);
            double deltaAngle = -(rotationAnglePrevious - rotationAngleCurrent);

            m_rotation += (float) PocketPaintApplication.RadianToDegree(deltaAngle);
            m_rotation = m_rotation % 360;

            if (orientation == Orientation.Left)
            {
                m_rotation = m_rotation * -1;
            }

            //if (m_rotation > 180)
            //    m_rotation = -180 + (m_rotation - 180);

            while (m_rotation < 0)
            {
                m_rotation += 360;
            }


            Point centerPoint = getCenterCoordinateOfGridMain();
            RotateTransform rt = new RotateTransform { Angle = m_rotation, CenterX = absolutePosition.X, CenterY = absolutePosition.Y };
            addTransformation(rt);

            // Paintroid code:
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

            // Previous Pocket Paint code:
        //    rotateCenterPoint.X = PocketPaintApplication.GetInstance().RectangleSelectionControl.MainGrid.Width / 2.0;
        //    rotateCenterPoint.Y = PocketPaintApplication.GetInstance().RectangleSelectionControl.MainGrid.Height / 2.0;

        //    rotate.CenterX = rotateCenterPoint.X;
        //    rotate.CenterY = rotateCenterPoint.Y;

        //    Point centerPoint = PocketPaintApplication.GetInstance().RectangleSelectionControl.getCenterCoordinateOfGridMain();

        //    if (!(lastPoint.X == 0.0 && lastPoint.Y == 0.0) &&
        //        (lastPoint.X != point.X || lastPoint.Y != point.Y))
        //    {
        //        double currentXLength = point.X - centerPoint.X;
        //        double currentYLength = point.Y - centerPoint.Y;
        //        double normalCurrentX = currentXLength / (Math.Sqrt(currentXLength * currentXLength + currentYLength * currentYLength));
        //        double normalCurrentY = currentYLength / (Math.Sqrt(currentXLength * currentXLength + currentYLength * currentYLength));

        //        double previousXLength = lastPoint.X - centerPoint.X;
        //        double previousYLength = lastPoint.Y - centerPoint.Y;
        //        double normalPreviousX = previousXLength / (Math.Sqrt(previousXLength * previousXLength + previousYLength * previousYLength));
        //        double normalPreviousY = previousYLength / (Math.Sqrt(previousXLength * previousXLength + previousYLength * previousYLength));

        //        double deltaAngle = (Math.Atan(normalPreviousX / normalPreviousY) - Math.Atan(normalCurrentX / normalCurrentY));
        //        double rotationAngle = deltaAngle * 360.0 / Math.PI;

        //        rotate.Angle = rotationAngle;
        //    }
        }
    }
}
