using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Benutzersteuerelement" ist unter http://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    public sealed partial class RectangleSelectionControl : UserControl
    {
        Point startPosition;
        public RectangleSelectionControl()
        {
            this.InitializeComponent();
            startPosition = new Point(0,0);
        }

        private void ellCenterBottom_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            ellCenterBottom.Margin = new Thickness(ellCenterBottom.Margin.Left, ellCenterBottom.Margin.Top, ellCenterBottom.Margin.Right, ellCenterBottom.Margin.Bottom - 1);
            rectRectangle.Height += 1;
            rectRectangle.Margin = new Thickness(rectRectangle.Margin.Left, rectRectangle.Margin.Top, rectRectangle.Margin.Right, rectRectangle.Margin.Bottom -1);
        }

        private void ellCenterBottom_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            //startPosition.X = e.GetCurrentPoint(ellCenterBottom).Position.X;
            //startPosition.Y = e.GetCurrentPoint(ellCenterBottom).Position.Y;
        }

        private void ellCenterBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double x = e.Delta.Translation.X;
            double y = e.Delta.Translation.Y;

            double diffX = startPosition.X - x;
            double diffY = startPosition.Y - y;

            rectRectangle.Height += diffX;
            startPosition.X = x;
        }
    }
}
