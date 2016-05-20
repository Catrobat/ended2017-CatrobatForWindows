using System;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

// Die Elementvorlage "Benutzersteuerelement" ist unter http://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    public sealed partial class CursorControl : UserControl
    {
        private static bool _isDrawing;
        private double standardDrawingPoint = 8.0;
        private double standardSizeInner = 13.0;
        private double standardSizeOuter = 20.0;

        public CursorControl()
        {
            this.InitializeComponent();
            _isDrawing = false;

            if(PocketPaintApplication.GetInstance().cursorControl == null)
            {
                PocketPaintApplication.GetInstance().cursorControl = this;
            }
        }

        private void setCursorControlLayout()
        {
            double width_multiplicator = PocketPaintApplication.GetInstance().size_width_multiplication;
            double height_multiplicator = PocketPaintApplication.GetInstance().size_width_multiplication;

            standardDrawingPoint *= width_multiplicator;
            standardSizeInner *= width_multiplicator;
            standardSizeOuter *= width_multiplicator;

            GridMain.Width *= width_multiplicator;
            GridMain.Height *= height_multiplicator;

            foreach (Object obj in GridRight.Children.Concat(
                                        GridLeft.Children.Concat(
                                            GridBottom.Children.Concat(
                                                GridTop.Children.Concat(
                                                    GridEllipse.Children.Concat(
                                                        GridRectangle.Children.Concat(
                                                            GridTriangle.Children)))))))
            {
                if (obj.GetType() == typeof(Ellipse))
                {
                    Ellipse currentEllipse = (Ellipse)obj;
                    currentEllipse.Height *= height_multiplicator;
                    currentEllipse.Width *= width_multiplicator;
                    currentEllipse.StrokeThickness *= height_multiplicator;

                    currentEllipse.Margin = new Thickness(
                                            currentEllipse.Margin.Left * width_multiplicator,
                                            currentEllipse.Margin.Top * height_multiplicator,
                                            currentEllipse.Margin.Right * width_multiplicator,
                                            currentEllipse.Margin.Bottom * height_multiplicator);
                }
                else if (obj.GetType() == typeof(Rectangle))
                {
                    Rectangle currentRectangle = (Rectangle)obj;
                    currentRectangle.Height *= height_multiplicator;
                    currentRectangle.Width *= width_multiplicator;
                    currentRectangle.StrokeThickness *= height_multiplicator;

                    currentRectangle.Margin = new Thickness(
                                            currentRectangle.Margin.Left * width_multiplicator,
                                            currentRectangle.Margin.Top * height_multiplicator,
                                            currentRectangle.Margin.Right * width_multiplicator,
                                            currentRectangle.Margin.Bottom * height_multiplicator);
                }
            }
        }

        public void changeCursorType(PenLineCap currentPenLineCap)
        {
            if (PocketPaintApplication.GetInstance() != null)
            {
                if(currentPenLineCap == PenLineCap.Round)
                {
                    GridEllipse.Visibility = Visibility.Visible;
                    GridRectangle.Visibility = Visibility.Collapsed;
                    GridTriangle.Visibility = Visibility.Collapsed;
                }
                else if(currentPenLineCap == PenLineCap.Square)
                {
                    GridEllipse.Visibility = Visibility.Collapsed;
                    GridRectangle.Visibility = Visibility.Visible;
                    GridTriangle.Visibility = Visibility.Collapsed;
                }
                else if(currentPenLineCap == PenLineCap.Triangle)
                {
                    GridEllipse.Visibility = Visibility.Collapsed;
                    GridRectangle.Visibility = Visibility.Collapsed;
                    GridTriangle.Visibility = Visibility.Visible;
                }
            }
        }

        public void changeCursorsize()
        {
            double currentThickness = PocketPaintApplication.GetInstance().PaintData.thicknessSelected;
            double newCurrentThickness = currentThickness - standardDrawingPoint;
            double newSizeInner = standardSizeInner + newCurrentThickness;
            double newSizeOuter = standardSizeOuter + newCurrentThickness;

            GridMain.Height += newCurrentThickness;
            GridMain.Width += newCurrentThickness;

            ellDrawingPoint.Height = currentThickness;
            ellDrawingPoint.Width = currentThickness;
            ellInner.Height = newSizeInner;
            ellInner.Width = newSizeInner;
            ellOuter.Height = newSizeOuter;
            ellOuter.Width = newSizeOuter;

            rectDrawingPoint.Height = currentThickness;
            rectDrawingPoint.Width = currentThickness;
            rectInner.Height = newSizeInner;
            rectInner.Width = newSizeInner;
            rectOuter.Height = newSizeOuter;
            rectOuter.Width = newSizeOuter;

            double drawingPointTri = currentThickness / Math.Sqrt(2.0);
            double sizeTriIn = newSizeInner / Math.Sqrt(2.0);
            double sizeTriOut = newSizeOuter / Math.Sqrt(2.0);

            triangelDrawingPoint.Height = drawingPointTri;
            triangelDrawingPoint.Width = drawingPointTri;
            triangelInner.Height = sizeTriIn;
            triangelInner.Width = sizeTriIn;
            triangelOuter.Height = sizeTriOut;
            triangelOuter.Width = sizeTriOut;

            rectBottom0.Margin = new Thickness(0.0, 0.0, 0.0, 0.0 - ((double)newCurrentThickness) / 2.0);
            rectBottom1.Margin = new Thickness(0.0, 0.0, 0.0, 8.0 - ((double)newCurrentThickness) / 2.0);
            rectBottom2.Margin = new Thickness(0.0, 0.0, 0.0, 16.0 - ((double)newCurrentThickness) / 2.0);
            rectBottom3.Margin = new Thickness(0.0, 0.0, 0.0, 24.0 - ((double)newCurrentThickness) / 2.0);

            rectLeft0.Margin = new Thickness(0.0 - ((double)newCurrentThickness) / 2.0, 0, 0, 0);
            rectLeft1.Margin = new Thickness(8.0 - ((double)newCurrentThickness) / 2.0, 0, 0, 0);
            rectLeft2.Margin = new Thickness(16.0 - ((double)newCurrentThickness) / 2.0, 0 ,0 ,0);
            rectLeft3.Margin = new Thickness(24.0 - ((double)newCurrentThickness) / 2.0, 0, 0, 0);

            rectRight0.Margin = new Thickness(0.0, 0.0, 0.0 - ((double)newCurrentThickness) / 2.0, 0.0);
            rectRight1.Margin = new Thickness(0.0, 0.0, 8.0 - ((double)newCurrentThickness) / 2.0, 0.0);
            rectRight2.Margin = new Thickness(0.0, 0.0, 16.0 - ((double)newCurrentThickness) / 2.0, 0.0);
            rectRight3.Margin = new Thickness(0.0, 0.0, 24.0 - ((double)newCurrentThickness) / 2.0, 0.0);

            rectTop0.Margin = new Thickness(0.0, 0.0 - ((double)newCurrentThickness) / 2.0, 0.0, 0.0);
            rectTop1.Margin = new Thickness(0.0, 8.0 - ((double)newCurrentThickness) / 2.0, 0.0, 0.0);
            rectTop2.Margin = new Thickness(0.0, 16.0 - ((double)newCurrentThickness) / 2.0, 0.0, 0.0);
            rectTop3.Margin = new Thickness(0.0, 24.0 - ((double)newCurrentThickness) / 2.0, 0.0, 0.0);
        }

        public bool isDrawingActivated()
        {
            return _isDrawing;
        }

        public void setCursorLook(bool isDrawing)
        {
            _isDrawing = isDrawing;

            if(_isDrawing)
            {
                Color color = PocketPaintApplication.GetInstance().PaintData.colorSelected.Color;
                rectColorEven.Color = color;
                setDrawingPointColor(color);
                setVisibilityOfDrawingPoint = Visibility.Visible;
            }
            else
            {
                rectColorEven.Color = Colors.DarkGray;
                setVisibilityOfDrawingPoint = Visibility.Collapsed;
            }
        }

        public void setCursorColor(Color color)
        {
            if (_isDrawing)
            {
                rectColorEven.Color = color;
                setVisibilityOfDrawingPoint = Visibility.Visible;
            }
        }

        public void setDrawingPointColor(Color color)
        {
            if(_isDrawing)
            {
                ellDrawingPoint.Fill = new SolidColorBrush(color);
                rectDrawingPoint.Fill = new SolidColorBrush(color);
                triangelDrawingPoint.Fill = new SolidColorBrush(color);
            }
        }

        public void setStrokeOfInnerShape(Color color)
        {
            ellInner.Stroke = new SolidColorBrush(color);
            rectInner.Stroke = new SolidColorBrush(color);
            triangelInner.Stroke = new SolidColorBrush(color);
        }

        public Visibility setVisibilityOfDrawingPoint
        {
            get
            {
                return ellDrawingPoint.Visibility;
            }
            set
            {
                ellDrawingPoint.Visibility = value;
                rectDrawingPoint.Visibility = value;
                triangelDrawingPoint.Visibility = value;
            }
        }
    }
}
