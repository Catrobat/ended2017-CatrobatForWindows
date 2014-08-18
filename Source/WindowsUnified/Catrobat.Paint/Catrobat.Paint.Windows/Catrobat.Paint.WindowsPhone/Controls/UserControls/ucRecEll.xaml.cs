using Catrobat.Paint.Phone;
using Catrobat.Paint.Phone.Data;
using Catrobat.Paint.WindowsPhone.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkID=390556 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class ucRecEll
    {
        public ucRecEll()
        {
            this.InitializeComponent();
            tbStrokeThicknessValue.Text = PocketPaintApplication.GetInstance().PaintData.BorderThicknessRecEll.ToString();
            sldStrokeThickness.Value = PocketPaintApplication.GetInstance().PaintData.BorderThicknessRecEll;
            PocketPaintApplication.GetInstance().PaintData.BorderColorChanged += ColorStrokeChanged;
            PocketPaintApplication.GetInstance().PaintData.FillColorChanged += ColorFillChanged;
            PocketPaintApplication.GetInstance().BarRecEllShape = this;
        }

        private void btnSelectedColor_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if(rootFrame != null)
            {
                rootFrame.Navigate(typeof(ViewBorderColorPicker));
            }
        }

        private void btnSelectedFillColor_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame != null)
            {
                rootFrame.Navigate(typeof(ViewFillColorPicker));
            }
        }

        private void ColorFillChanged(SolidColorBrush color)
        {
            SolidColorBrush selected_color = new SolidColorBrush();
            selected_color.Color = color.Color != Colors.Transparent ? color.Color : Colors.Transparent;
            btnSelectedFillColor.Background = selected_color;
            rectTransDisplayForeground.Fill = selected_color;
        }

        private void ColorStrokeChanged(SolidColorBrush color)
        {
            SolidColorBrush selected_color = new SolidColorBrush();
            selected_color.Color = color.Color != Colors.Transparent ? color.Color : Colors.Transparent;
            btnSelectedBorderColor.Background = selected_color;
            rectTransDisplayForeground.Stroke = selected_color;
        }

        private void sldStrokeThickness_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
           
        }

        private void sldSliderThickness_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
           //  sldSliderThickness.Background.ToString();
           //  sldSliderThickness.Value.ToString(); 
        }

        public int getHeight()
        {
            return Convert.ToInt32(tbHeightValue.Text);
        }

        public int getWidth()
        {
            return Convert.ToInt32(tbWidthValue.Text);
        }

        private void sldSlidersChanged_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            int strokeThickness = (int)sldStrokeThickness.Value;
            tbStrokeThicknessValue.Text = strokeThickness.ToString();
            PocketPaintApplication.GetInstance().PaintData.BorderThicknessRecEll = strokeThickness;
            rectTransDisplayForeground.StrokeThickness = strokeThickness;
        }
    }
}
