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

        public void resetEllipseSelectionControl()
        {
            FillOfEllipseToDraw = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            StrokeOfEllipseToDraw = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
            StrokeThicknessOfEllipseToDraw = PocketPaintApplication.GetInstance().PaintData.strokeThickness;
            StrokeLineJoinOfEllipseToDraw = PocketPaintApplication.GetInstance().PaintData.penLineJoinSelected;

            RectangleShapeBaseControl.ResetRectangleShapeBaseControl();
        }
    }
}
