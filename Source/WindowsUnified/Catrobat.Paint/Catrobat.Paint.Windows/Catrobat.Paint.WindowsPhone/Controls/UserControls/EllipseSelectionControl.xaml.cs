using System;
using System.Diagnostics;
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
        public RectangleShapeBaseControl RectangleShapeBase { get; private set; }
        public Ellipse EllipseToDraw { get; private set; }

        public PenLineJoin StrokeLineJoinOfEllipseToDraw
        {
            get { return EllipseToDraw.StrokeLineJoin; }
            set { EllipseToDraw.StrokeLineJoin = value; }
        }

        public double StrokeThicknessOfEllipseToDraw
        {
            get { return EllipseToDraw.StrokeThickness; }
            set { EllipseToDraw.StrokeThickness = value; }
        }

        public Brush FillOfEllipseToDraw
        {
            get { return EllipseToDraw.Fill; }
            set { EllipseToDraw.Fill = value; }
        }

        public Brush StrokeOfEllipseToDraw
        {
            get { return EllipseToDraw.Stroke; }
            set { EllipseToDraw.Stroke = value; }
        }

        public EllipseSelectionControl()
        {
            this.InitializeComponent();

            RectangleShapeBase = RectangleShapeBaseControl;

            //GridMain.RenderTransform = _transformGridMain = new TransformGroup();
            //ellEllipseToDraw.Fill = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            //ellEllipseToDraw.Stroke = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
            //ellEllipseToDraw.StrokeThickness = PocketPaintApplication.GetInstance().PaintData.strokeThickness;
            
            //PocketPaintApplication.GetInstance().EllipseSelectionControl = this;
            //isModifiedEllipseMovement = false;

            for (int i = 0; i < RectangleShapeBaseControl.AreaToDraw.Children.Count; i++)
            {
                if (!(RectangleShapeBaseControl.AreaToDraw.Children[i] is Ellipse))
                {
                    RectangleShapeBaseControl.AreaToDraw.Children.RemoveAt(i);
                    i--;
                }
                else
                {
                    EllipseToDraw = (Ellipse)RectangleShapeBaseControl.AreaToDraw.Children[i];
                    EllipseToDraw.Visibility = Visibility.Visible;
                }
            }

            Debug.Assert(EllipseToDraw != null);
        }

        public Point getCenterCoordinateOfGridMain()
        {
            //double halfScreenHeight = Window.Current.Bounds.Height / 2.0;
            //double halfScreenWidth = Window.Current.Bounds.Width / 2.0;

            //TranslateTransform lastTranslateTransform = getLastTranslateTransformation();
            //RotateTransform lastRotateTransform = getLastRotateTransformation();

            //double offsetX;
            //double offsetY;
            //if (lastTranslateTransform != null && lastRotateTransform == null)
            //{
            //    offsetX = lastTranslateTransform.X;
            //    offsetY = lastTranslateTransform.Y;
            //}
            //else if (lastTranslateTransform == null && lastRotateTransform != null)
            //{
            //    offsetX = 0.0;
            //    offsetY = 0.0;
            //}
            //else if (lastTranslateTransform != null && lastRotateTransform != null)
            //{
            //    offsetX = lastTranslateTransform.X;
            //    offsetY = lastTranslateTransform.Y;
            //}
            //else
            //{
            //    offsetX = 0.0;
            //    offsetY = 0.0;
            //}

            //double marginOffsetX = GridMain.Margin.Left - GridMain.Margin.Right;
            //double marginOffsetY = GridMain.Margin.Top - GridMain.Margin.Bottom;

            //double coordinateX = offsetX + halfScreenWidth + (marginOffsetX / 2.0);
            //double coordinateY = offsetY + halfScreenHeight + (marginOffsetY / 2.0);

            //return new Point(coordinateX, coordinateY);
            return new Point(0,0);
        }


        private void rectEllipseForMovement_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Point centerCoordinate = getCenterCoordinateOfGridMain();

            PocketPaintApplication.GetInstance().ToolCurrent.Draw(centerCoordinate);
        }

        public TranslateTransform getLastTranslateTransformation()
        {
            //for (int i = 0; i < _transformGridMain.Children.Count; i++)
            //{
            //    if (_transformGridMain.Children[i].GetType() == typeof(TranslateTransform))
            //    {
            //        TranslateTransform translateTransform = new TranslateTransform();
            //        translateTransform.X = ((TranslateTransform)_transformGridMain.Children[i]).X;
            //        translateTransform.Y = ((TranslateTransform)_transformGridMain.Children[i]).Y;

            //        return translateTransform;
            //    }
            //}

            return null;
        }

        public RotateTransform getLastRotateTransformation()
        {
            //for (int i = 0; i < _transformGridMain.Children.Count; i++)
            //{
            //    if (_transformGridMain.Children[i].GetType() == typeof(RotateTransform))
            //    {
            //        RotateTransform rotateTransform = new RotateTransform();
            //        rotateTransform.CenterX = ((RotateTransform)_transformGridMain.Children[i]).CenterX;
            //        rotateTransform.CenterY = ((RotateTransform)_transformGridMain.Children[i]).CenterY;
            //        rotateTransform.Angle = ((RotateTransform)_transformGridMain.Children[i]).Angle;

            //        return rotateTransform;
            //    }
            //}

            return null;
        }


        public void resetEllipseSelectionControl()
        {
            //PocketPaintApplication.GetInstance().PaintingAreaManipulationListener.lastPoint = new Point(0.0, 0.0);

            //PocketPaintApplication.GetInstance().BarRecEllShape.setBtnHeightValue = _ellipseToDrawSize;
            //PocketPaintApplication.GetInstance().BarRecEllShape.setBtnWidthValue = _ellipseToDrawSize;

            //_transformGridMain.Children.Clear();
            //GridMain.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);

            //GridMain.Width = _gridMainSize;
            //GridMain.Height = _gridMainSize;

            //ellEllipseToDraw.Width = _ellipseToDrawSize;
            //ellEllipseToDraw.Height = _ellipseToDrawSize;
            //rectEllipseForMovement.Width = _rectangleForMovementSize;
            //rectEllipseForMovement.Height = _rectangleForMovementSize;

            //FillOfEllipseToDraw = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            //StrokeOfEllipseToDraw = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
            //StrokeThicknessOfEllipseToDraw = PocketPaintApplication.GetInstance().PaintData.strokeThickness;
            //// strokeLineJoinOfRectangleToDraw = PocketPaintApplication.GetInstance().PaintData.penLineJoinSelected;

            //RotateTransform rotate = new RotateTransform();
            //var angle = PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation;
            //rotate.Angle = -angle;
            //Point point = new Point(0.5, 0.5);
            //PocketPaintApplication.GetInstance().EllipseSelectionControl.RenderTransformOrigin = point;
            //PocketPaintApplication.GetInstance().EllipseSelectionControl.RenderTransform = rotate;

            //isModifiedEllipseMovement = false;
        }
    }
}
