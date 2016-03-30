using System;
using System.Diagnostics;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Catrobat.Paint.WindowsPhone.Tool;

// Die Elementvorlage "Benutzersteuerelement" ist unter http://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    public sealed partial class RectangleSelectionControl : UserControl
    {
        TransformGroup m_transformGridMain;
        bool _isModifiedRectangleMovement;
        double _rectangleForMovementSize = 200.0;
        double _rectangleToDrawSize = 160.0;

        public Rectangle rectangleToDraw { get; private set; }

        private enum Orientation
        {
            Top, Bottom, Left, Right    
        }

        public RectangleSelectionControl()
        {
            this.InitializeComponent();


            for (int i = 0; i < RectangleShapeBaseControl.AreaToDraw.Children.Count; i++)
            {
                if (! (RectangleShapeBaseControl.AreaToDraw.Children[i] is Rectangle))
                {
                    RectangleShapeBaseControl.AreaToDraw.Children.RemoveAt(i);
                    i--;
                }
                else
                {
                    rectangleToDraw = (Rectangle) RectangleShapeBaseControl.AreaToDraw.Children[i];
                    rectangleToDraw.Visibility = Visibility.Visible;
                }
            }
            
            Debug.Assert(rectangleToDraw != null);
            initRectangleToDraw();
        }

        private void initRectangleToDraw()
        {
            rectangleToDraw.Fill = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            rectangleToDraw.Stroke = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
            rectangleToDraw.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.strokeThickness;    
        }

        // TODO: adapt this


        public bool isModifiedRectangleMovement { get; set; }

        public PenLineJoin StrokeLineJoinOfRectangleToDraw
        {
            get { return rectangleToDraw.StrokeLineJoin; }
            set { rectangleToDraw.StrokeLineJoin = value; }
        }

        public double StrokeThicknessOfRectangleToDraw
        {
            get { return rectangleToDraw.StrokeThickness; }
            set { rectangleToDraw.StrokeThickness = value; }
        }

        public Brush FillOfRectangleToDraw
        {
            get { return rectangleToDraw.Fill; }
            set { rectangleToDraw.Fill = value; }
        }

        public Brush StrokeOfRectangleToDraw
        {
            get { return rectangleToDraw.Stroke; }
            set { rectangleToDraw.Stroke = value; }
        }

        //public void resetRectangleSelectionControl()
        //{
        //    PocketPaintApplication.GetInstance().PaintingAreaManipulationListener.lastPoint = new Point(0.0, 0.0);

        //    PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = _rectangleToDrawSize;
        //    PocketPaintApplication.GetInstance().BarRecEllShape.setBtnWidthValue = _rectangleToDrawSize;

        //    m_transformGridMain.Children.Clear();
        //    GridMainSelection.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);

        //    GridMainSelection.Width = _gridMainSize;
        //    GridMainSelection.Height = _gridMainSize;

        //    rectRectangleToDraw.Width = _rectangleToDrawSize;
        //    rectRectangleToDraw.Height = _rectangleToDrawSize;
        //    rectRectangleForMovement.Width = _rectangleForMovementSize;
        //    rectRectangleForMovement.Height = _rectangleForMovementSize;

        //    rectRectangleToDraw.Fill = PocketPaintApplication.GetInstance().PaintData.colorSelected;
        //    rectRectangleToDraw.Stroke = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
        //    rectRectangleToDraw.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.strokeThickness;
        //    // strokeLineJoinOfRectangleToDraw = PocketPaintApplication.GetInstance().PaintData.penLineJoinSelected;
        //    RotateTransform rotate = new RotateTransform();
        //    var angle = PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation;
        //    rotate.Angle = -angle;
        //    Point point = new Point(0.5, 0.5);
        //    PocketPaintApplication.GetInstance().RectangleSelectionControl.RenderTransformOrigin = point;
        //    PocketPaintApplication.GetInstance().RectangleSelectionControl.RenderTransform = rotate;

        //    isModifiedRectangleMovement = false;
        //    m_rotation = 0;
        //}



        internal void resetRectangleSelectionControl()
        {
            rectangleToDraw.Fill = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            rectangleToDraw.Stroke = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
            rectangleToDraw.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.strokeThickness;
            StrokeLineJoinOfRectangleToDraw = PocketPaintApplication.GetInstance().PaintData.penLineJoinSelected;   
    
            RectangleShapeBaseControl.resetRectangleSelectionControl();
        }

        internal RotateTransform getLastRotateTransformation()
        {
            throw new NotImplementedException();
        }

        internal void SetHeightOfControl(double currentValue)
        {
            throw new NotImplementedException();
        }

        internal void SetWidthOfControl(double currentValue)
        {
            throw new NotImplementedException();
        }
    }
}
