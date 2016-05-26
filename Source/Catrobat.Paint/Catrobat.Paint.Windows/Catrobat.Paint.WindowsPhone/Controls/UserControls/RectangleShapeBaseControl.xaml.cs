using System;
using System.Diagnostics;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// Die Elementvorlage "Benutzersteuerelement" ist unter http://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    public sealed partial class RectangleShapeBaseControl
    {
        private const double m_DefaultGridMainSize = 290.0;
        private const double m_DefaultRectangleForMovementSize = 200.0;
        private const double m_DefaultAreaToDrawSize = 160.0;
        private const double m_MinWidthRectangleToDraw = 20;
        private const double m_MinHeightRectangleToDraw = 20;
        private Point m_CenterPointRotation;
        private readonly TransformGroup m_TransformGridMain;

        private float m_RotationAngle;
        private Point m_RotationStartingPoint;

        public Grid AreaToDraw { get; private set; }

    private enum Orientation
        {
            Top, Bottom, Left, Right    
        }

        public RectangleShapeBaseControl()
        {
            InitializeComponent();

            AreaToDraw = AreaToDrawGrid;

            GridMainSelection.RenderTransform = m_TransformGridMain = new TransformGroup();
            m_RotationAngle = 0;

            m_CenterPointRotation = new Point
            {
                X = GridMainSelection.Width / 2,
                Y = GridMainSelection.Height / 2
            };

            IsModifiedRectangleForMovement = false;
        }

        public bool IsModifiedRectangleForMovement { get; set; }

        public bool ShouldHeightOfSelectionChanged(double heightOfSelection)
        {
            var result = heightOfSelection >= 1.0;
            return result;
        }
        public bool ShouldWidthOfSelectionChanged(double widthOfSelection)
        {
            var result = widthOfSelection >= 1.0;
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
            var translateTransform = new TranslateTransform();

            // TODO: Fix bug of translation when rotated in certain positions. remove then the comments
            //RotateTransform lastRotateTransform = GetLastRotateTransformation();

            //var deltaX = e.Delta.Translation.X;
            //var deltaY = e.Delta.Translation.Y;
            //var rotationRadian = PocketPaintApplication.DegreeToRadian(m_RotationAngle);
            //var deltaXCorrected = Math.Cos(-rotationRadian) * (deltaX)
            //        - Math.Sin(-rotationRadian) * (deltaY);
            //var deltaYCorrected = Math.Sin(-rotationRadian) * (deltaX)
            //        + Math.Cos(-rotationRadian) * (deltaY);
            //var xVal = deltaXCorrected;
            //var yVal = deltaYCorrected;

            var xVal = e.Delta.Translation.X;
            var yVal = e.Delta.Translation.Y;

            m_CenterPointRotation.X += xVal;
            m_CenterPointRotation.Y += yVal;

            var lastTranslateTransform = GetLastTranslateTransformation();
            if (lastTranslateTransform != null)
            {
                xVal += lastTranslateTransform.X;
                yVal += lastTranslateTransform.Y;
            }

            translateTransform.X = xVal;
            translateTransform.Y = yVal;
            addTransformation(translateTransform);
        }

        private void rectEllipseForMovement_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Debug.Assert(AreaToDraw.Children.Count == 1);
            UIElement elementToDraw = AreaToDraw.Children[0];

            var coord = e.GetPosition(PocketPaintApplication.GetInstance().GridWorkingSpace);
            //var coord2 = e.GetPosition(elementToDraw);
            var coord2 = e.GetPosition(AreaToDraw);

            var angle = PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation;

            switch (angle)
            {
                case 0:
                    coord.X -= coord2.X;
                    coord.Y -= coord2.Y;
                    coord.X += (AreaToDraw.Width / 2);
                    coord.Y += (AreaToDraw.Height / 2);
                    break;
                case 90:
                    coord.X -= coord2.Y;
                    coord.Y -= (AreaToDraw.Width - coord2.X);
                    coord.X += (AreaToDraw.Width / 2);
                    coord.Y += (AreaToDraw.Height / 2);
                    break;
                case 180:
                    coord.X += coord2.X;
                    coord.Y += coord2.Y;
                    coord.X -= (AreaToDraw.Width / 2);
                    coord.Y -= (AreaToDraw.Height / 2);
                    break;
                case 270:
                    coord.X -= (AreaToDraw.Height - coord2.Y);
                    coord.Y -= coord2.X;
                    coord.X += (AreaToDraw.Width / 2);
                    coord.Y += (AreaToDraw.Height / 2);
                    break;
            }



            PocketPaintApplication.GetInstance().ToolCurrent.Draw(coord);
        }

        private void resizeWidth(double deltaX, double deltaY, Orientation orientation)
        {
            Debug.Assert(orientation == Orientation.Left || orientation == Orientation.Right);

            float rotation = m_RotationAngle;
            while (rotation < 0)
            {
                rotation += 360;
            }

            double rotationRadian = PocketPaintApplication.DegreeToRadian(rotation);
            double deltaXCorrected = Math.Cos(-rotationRadian) * (deltaX)
                    - Math.Sin(-rotationRadian) * (deltaY);
            //double deltaYCorrected = Math.Sin(-rotationRadian) * (deltaX)
            //        + Math.Cos(-rotationRadian) * (deltaY);

            if (orientation == Orientation.Left)
            {
                deltaXCorrected = deltaXCorrected * -1;
            }



            double newWidthRectangleToDraw = AreaToDrawGrid.Width + deltaXCorrected;
            if (newWidthRectangleToDraw < m_MinWidthRectangleToDraw)
            {
                newWidthRectangleToDraw = m_MinWidthRectangleToDraw;
                deltaXCorrected = m_MinWidthRectangleToDraw - AreaToDrawGrid.Width;
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

            float rotation = m_RotationAngle;
            while (rotation < 0)
            {
                rotation += 360;
            }

            double rotationRadian = PocketPaintApplication.DegreeToRadian(rotation);
            //double deltaXCorrected = Math.Cos(-rotationRadian) * (deltaX)
            //        - Math.Sin(-rotationRadian) * (deltaY);
            double deltaYCorrected = Math.Sin(-rotationRadian) * (deltaX)
                    + Math.Cos(-rotationRadian) * (deltaY);

            if (orientation == Orientation.Top)
            {
                deltaYCorrected = deltaYCorrected * -1;
            }

            double newHeightRectangleToDraw = AreaToDrawGrid.Height + deltaYCorrected;
            if (newHeightRectangleToDraw < m_MinHeightRectangleToDraw)
            {
                newHeightRectangleToDraw = m_MinHeightRectangleToDraw;
                deltaYCorrected = m_MinHeightRectangleToDraw - AreaToDrawGrid.Height;
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

        public Point GetCenterCoordinateOfGridMain()
        {
            double halfScreenHeight = Window.Current.Bounds.Height / 2.0;
            double halfScreenWidth = Window.Current.Bounds.Width / 2.0;

            TranslateTransform lastTranslateTransform = GetLastTranslateTransformation();
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

        public Point GetCenterCoordinateOfFrameworkElement(FrameworkElement e)
        {
            double halfScreenHeight = Window.Current.Bounds.Height / 2.0;
            double halfScreenWidth = Window.Current.Bounds.Width / 2.0;

            TranslateTransform lastTranslateTransform = GetLastTranslateTransformation();
            RotateTransform lastRotateTransform = GetLastRotateTransformation();

            double offsetX;
            double offsetY;
            if (lastTranslateTransform != null)
            {
                offsetX = lastTranslateTransform.X;
                offsetY = lastTranslateTransform.Y;
            }
            else
            {
                offsetX = 0.0;
                offsetY = 0.0;
            }

            double marginOffsetX = e.Margin.Left - e.Margin.Right;
            double marginOffsetY = e.Margin.Top - e.Margin.Bottom;

            double coordinateX = offsetX + halfScreenWidth + (marginOffsetX / 2.0);
            double coordinateY = offsetY + halfScreenHeight + (marginOffsetY / 2.0);

            return new Point(coordinateX, coordinateY); 
        }

        public void addTransformation(Transform currentTransform)
        {
            for (int i = 0; i < m_TransformGridMain.Children.Count; i++)
            {
                if (m_TransformGridMain.Children[i].GetType() == currentTransform.GetType())
                {
                    m_TransformGridMain.Children.RemoveAt(i);
                }
            }

            m_TransformGridMain.Children.Add(currentTransform);

            ResetAppBarButtonRectangleSelectionControl(true);
            IsModifiedRectangleForMovement = true;
        }

        public void ResetAppBarButtonRectangleSelectionControl(bool isActivated)
        {
            var paintingAreaView = PocketPaintApplication.GetInstance().PaintingAreaView;
            if (paintingAreaView != null)
            {
                var appBarButtonReset = paintingAreaView.getAppBarResetButton();
                if (appBarButtonReset != null)
                {
                    appBarButtonReset.IsEnabled = isActivated;
                }
            }
        }

        public TranslateTransform GetLastTranslateTransformation()
            {
            return m_TransformGridMain.Children.OfType<TranslateTransform>().Select(t => new TranslateTransform
                {
                X = t.X, 
                Y = t.Y
            }).FirstOrDefault();
        }

        public RotateTransform GetLastRotateTransformation()
        {
            return m_TransformGridMain.Children.OfType<RotateTransform>().Select(t => new RotateTransform
                    {
                CenterX = t.CenterX, 
                CenterY = t.CenterY, 
                Angle = t.Angle
            }).FirstOrDefault();
        }

        public void SetHeightOfControl(double newHeightRectangleToDraw)
        {
            //TODO 1 is maybe too small?
            if (newHeightRectangleToDraw >= 1)
            {
                GridMainSelection.Height = newHeightRectangleToDraw + (GridMainSelection.Height - AreaToDrawGrid.Height);
                MovementRectangle.Height = newHeightRectangleToDraw + (MovementRectangle.Height - AreaToDrawGrid.Height);
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
                MovementRectangle.Width = newWidthRectangleToDraw + (MovementRectangle.Width - AreaToDrawGrid.Width);
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
            GridMainSelection.Width = m_DefaultGridMainSize;
            GridMainSelection.Height = m_DefaultGridMainSize;
            GridMainSelection.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);

            MovementRectangle.Width = m_DefaultRectangleForMovementSize;
            MovementRectangle.Height = m_DefaultRectangleForMovementSize;

            AreaToDrawGrid.Width = m_DefaultAreaToDrawSize;
            AreaToDrawGrid.Height = m_DefaultAreaToDrawSize;
            PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = m_DefaultAreaToDrawSize;
            PocketPaintApplication.GetInstance().BarRecEllShape.setBtnWidthValue = m_DefaultAreaToDrawSize;

            m_TransformGridMain.Children.Clear();
            m_RotationAngle = 0;

            m_CenterPointRotation.X = GridMainSelection.Width / 2;
            m_CenterPointRotation.Y = GridMainSelection.Height / 2;

            ResetAppBarButtonRectangleSelectionControl(false);
            IsModifiedRectangleForMovement = false;

            // TODO: evaluate if the outcommented code is needed
            //PocketPaintApplication.GetInstance().PaintingAreaManipulationListener.lastPoint = new Point(0.0, 0.0);
            //RotateTransform rotate = new RotateTransform();
            //var angle = PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation;
            //rotate.Angle = -angle;
            //Point point = new Point(0.5, 0.5);
            //PocketPaintApplication.GetInstance().RectangleSelectionControl.RenderTransformOrigin = point;
            //PocketPaintApplication.GetInstance().RectangleSelectionControl.RenderTransform = rotate;

            //PocketPaintApplication.GetInstance().EllipseSelectionControl.RenderTransformOrigin = point;
            //PocketPaintApplication.GetInstance().EllipseSelectionControl.RenderTransform = rotate;
        }

        private void TopLeftGrid_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            resizeHeight(e.Delta.Translation.X, e.Delta.Translation.Y, Orientation.Top);
            resizeWidth(e.Delta.Translation.X, e.Delta.Translation.Y, Orientation.Left);
        }

        private void RotationTopRight_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("RotationArrowTopRight_OnManipulationDelta");
            //System.Diagnostics.Debug.WriteLine("Position: " + e.Position.X + ", " + e.Position.Y);
            Rotate(e.Position, e.Delta.Translation.X, e.Delta.Translation.Y, Orientation.Right);
           
        }

        private void RotationTopLeft_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Rotate(e.Position, e.Delta.Translation.X, e.Delta.Translation.Y, Orientation.Left);
            //System.Diagnostics.Debug.WriteLine("Position: " + e.Position.X + ", " + e.Position.Y);
        }

        private void Rotate(Point position, double deltaX, double deltaY, Orientation orientation)
        {

            Point topRightGrid = GetCenterCoordinateOfFrameworkElement(TopRightRotationGrid);


            topRightGrid.Y -= TopRightRotationGrid.Width / 2;
            topRightGrid.X -= TopRightRotationGrid.Height / 2;

            //Point marginMax = new Point(TopRightRotationGrid.Width + topRightGrid.X, TopRightRotationGrid.Height + topRightGrid.Y);
            //Point marginMin = topRightGrid;

            m_RotationStartingPoint.X = topRightGrid.X + position.X;
            m_RotationStartingPoint.Y = topRightGrid.Y + position.Y;

            System.Diagnostics.Debug.WriteLine("--------\nDeltaX: " + deltaX + ", DeltaY: " + deltaY);
            System.Diagnostics.Debug.WriteLine("e.Position.X: " + position.X + ", e.Position.Y: " + position.Y);
            System.Diagnostics.Debug.WriteLine("Position: " + m_RotationStartingPoint.X + ", " + m_RotationStartingPoint.Y);
            //System.Diagnostics.Debug.WriteLine("--------\nEnd Point: " + (m_RotationStartingPoint.X + deltaX) + ", " + (m_RotationStartingPoint.Y + deltaY));

            // TODO: check if translation of X & Y should be addeds

            Point centerPoint = GetCenterCoordinateOfGridMain();
            System.Diagnostics.Debug.WriteLine("--------\ncenterX: " + centerPoint.X + ", centerY: " + centerPoint.Y);

            double previousXLength = m_RotationStartingPoint.X - centerPoint.X;
            double previousYLength = centerPoint.Y - m_RotationStartingPoint.Y;

            System.Diagnostics.Debug.WriteLine("--------\npreviousXLength: " + previousXLength + ", previousYLength: " + previousYLength);

            double currentXLength = m_RotationStartingPoint.X + deltaX  - centerPoint.X;
            double currentYLength = centerPoint.Y - m_RotationStartingPoint.Y + deltaY;

            System.Diagnostics.Debug.WriteLine("--------\ncurrentXLength: " + currentXLength + ", currentYLength: " + currentYLength);

            double rotationAnglePrevious = Math.Atan2(previousYLength, previousXLength);
            System.Diagnostics.Debug.WriteLine("--------\nrotationAnglePrevious: " + rotationAnglePrevious );

            double rotationAngleCurrent = Math.Atan2(currentYLength, currentXLength);
            System.Diagnostics.Debug.WriteLine("--------\nrotationAngleCurrent: " + rotationAngleCurrent);

            double deltaAngle = -(rotationAnglePrevious - rotationAngleCurrent);

                
            System.Diagnostics.Debug.WriteLine("--------\nangle: " + (float)PocketPaintApplication.RadianToDegree(deltaAngle));

            //m_RotationAngle += (float) PocketPaintApplication.RadianToDegree(deltaAngle);
            m_RotationAngle += (float)PocketPaintApplication.RadianToDegree(deltaAngle) + 360;
            m_RotationAngle %= 360;

            if (m_RotationAngle > 180)
                m_RotationAngle = m_RotationAngle - 360;

            System.Diagnostics.Debug.WriteLine("--------\ncurrX: " + m_RotationAngle);
            var rt = new RotateTransform
            {
                Angle = m_RotationAngle,
                CenterX = m_CenterPointRotation.X,
                CenterY = m_CenterPointRotation.Y
            };
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
        }
    }
}
