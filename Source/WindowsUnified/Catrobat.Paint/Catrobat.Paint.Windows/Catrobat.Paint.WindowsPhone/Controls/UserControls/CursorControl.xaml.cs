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
using Windows.UI.Xaml.Shapes;

// Die Elementvorlage "Benutzersteuerelement" ist unter http://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    public sealed partial class CursorControl : UserControl
    {
        private static bool isDrawing;
        public CursorControl()
        {
            this.InitializeComponent();
            isDrawing = false;
            if(PocketPaintApplication.GetInstance().cursorControl == null)
            {
                PocketPaintApplication.GetInstance().cursorControl = this;
            }
        }

        public void changeCursorsize()
        {
            int standardDrawingPoint = 8;
            int standardSizeInner = 13;
            int standardSizeOuter = 20;
            int currentThickness = PocketPaintApplication.GetInstance().PaintData.ThicknessSelected;
            int newCurrentThicness = currentThickness - 8;
            int newSizeInnerEllipse = standardSizeInner + newCurrentThicness;
            int newSizeOuterEllipse = standardSizeOuter + newCurrentThicness;
            ellDrawingPoint.Height = standardDrawingPoint + newCurrentThicness;
            ellDrawingPoint.Width = standardDrawingPoint + newCurrentThicness;
            ellInner.Height = newSizeInnerEllipse;
            ellInner.Width = newSizeInnerEllipse;
            ellOuter.Height = newSizeOuterEllipse;
            ellOuter.Width = newSizeOuterEllipse;

            rectBottom0.Margin = new Thickness(rectBottom0.Margin.Left, rectBottom0.Margin.Top, rectBottom0.Margin.Right, 0 - ((double)newCurrentThicness) / 2);
            rectBottom1.Margin = new Thickness(rectBottom1.Margin.Left, rectBottom1.Margin.Top, rectBottom1.Margin.Right, 7 - ((double)newCurrentThicness) / 2);
            rectBottom2.Margin = new Thickness(rectBottom2.Margin.Left, rectBottom2.Margin.Top, rectBottom2.Margin.Right, 15 - ((double)newCurrentThicness) / 2);
            rectBottom3.Margin = new Thickness(rectBottom3.Margin.Left, rectBottom3.Margin.Top, rectBottom3.Margin.Right, 23 - ((double)newCurrentThicness) / 2);

            rectLeft0.Margin = new Thickness(0 - ((double)newCurrentThicness) / 2, 0, 0, 0);
            rectLeft1.Margin = new Thickness(7 - ((double)newCurrentThicness) / 2, 0, 0, 0);
            rectLeft2.Margin = new Thickness(15 - ((double)newCurrentThicness) / 2, 0 ,0 ,0);
            rectLeft3.Margin = new Thickness(23 - ((double)newCurrentThicness) / 2, 0, 0, 0);

            rectRight0.Margin = new Thickness(0, 0, 0 - ((double)newCurrentThicness) / 2, 0);
            rectRight1.Margin = new Thickness(0, 0, 7 - ((double)newCurrentThicness) / 2, 0);
            rectRight2.Margin = new Thickness(0, 0, 15 - ((double)newCurrentThicness) / 2, 0);
            rectRight3.Margin = new Thickness(0, 0, 23 - ((double)newCurrentThicness) / 2, 0);

            rectTop0.Margin = new Thickness(rectTop0.Margin.Left, 0 - ((double)newCurrentThicness) / 2, rectTop0.Margin.Right, rectTop0.Margin.Bottom);
            rectTop1.Margin = new Thickness(rectTop1.Margin.Left, 7 - ((double)newCurrentThicness) / 2, rectTop1.Margin.Right, rectTop0.Margin.Bottom);
            rectTop2.Margin = new Thickness(rectTop2.Margin.Left, 15 - ((double)newCurrentThicness) / 2, rectTop2.Margin.Right, rectTop0.Margin.Bottom);
            rectTop3.Margin = new Thickness(rectTop3.Margin.Left, 23 - ((double)newCurrentThicness) / 2, rectTop3.Margin.Right, rectTop0.Margin.Bottom);
            GridMain.Height += newCurrentThicness;
            GridMain.Width += newCurrentThicness;
        }

        public void setCursorLook()
        {
            isDrawing = !isDrawing;

            if(isDrawing)
            {
                Color color = PocketPaintApplication.GetInstance().PaintData.ColorSelected.Color;
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
            if (isDrawing)
            {
                rectColorEven.Color = color;
                setVisibilityOfDrawingPoint = Visibility.Visible;
            }
        }

        public void setDrawingPointColor(Color color)
        {
            if(isDrawing)
            {
                ellDrawingPoint.Fill = new SolidColorBrush(color);
            }
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
            }
        }
    }
}
