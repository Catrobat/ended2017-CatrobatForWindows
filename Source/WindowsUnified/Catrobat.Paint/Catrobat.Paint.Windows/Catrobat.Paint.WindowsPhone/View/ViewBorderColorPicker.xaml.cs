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
// TODO: using Coding4Fun.Toolkit;
using Windows.UI;
using Windows.UI.Xaml.Shapes;
using Catrobat.Paint.Phone;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Popups;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkID=390556 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.View
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class ViewBorderColorPicker : Page
    {
        public ViewBorderColorPicker()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Wird aufgerufen, wenn diese Seite in einem Frame angezeigt werden soll.
        /// </summary>
        /// <param name="e">Ereignisdaten, die beschreiben, wie diese Seite erreicht wurde.
        /// Dieser Parameter wird normalerweise zum Konfigurieren der Seite verwendet.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SolidColorBrush fill_color = PocketPaintApplication.GetInstance().PaintData.BorderColorSelected;
            double fill_color_opacity = Convert.ToDouble(fill_color.Color.A);

            if (PocketPaintApplication.GetInstance().PaintData.ColorSelected != null)
            {
                Color selected_color = ((SolidColorBrush)PocketPaintApplication.GetInstance().PaintData.BorderColorSelected).Color;

                changeValuesOfColourSliders(selected_color.R, selected_color.G, selected_color.B, (byte)fill_color_opacity);
                changeColorOfBtnSelectedColor(selected_color);
            }
        }

        private void changeColorOfBtnSelectedColor(Color color)
        {
            Color selected_color = color;

            int reference_color = (selected_color.R + selected_color.G + selected_color.B) / 3;

            if (reference_color <= 128 && (selected_color.A > 5))
            {
                BtnSelectedColor.Foreground = new SolidColorBrush(Colors.White);
                BtnSelectedColorSlider.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                BtnSelectedColor.Foreground = new SolidColorBrush(Colors.Black);
                BtnSelectedColorSlider.Foreground = new SolidColorBrush(Colors.Black);
            }

            if (selected_color == Colors.Transparent)
            {
                BtnSelectedColor.Background = new SolidColorBrush(Colors.Transparent);
                BtnSelectedColorSlider.Background = new SolidColorBrush(Colors.Transparent);

                BtnSelectedColor.BorderBrush = new SolidColorBrush(Colors.White);
                BtnSelectedColorSlider.BorderBrush = new SolidColorBrush(Colors.White);
            }
            else
            {
                BtnSelectedColor.Background = new SolidColorBrush(selected_color);
                BtnSelectedColorSlider.Background = new SolidColorBrush(selected_color);
            }
        }

        private void ColorChangedPredefined(object sender, RoutedEventArgs routedEventArgs)
        {
            var colorBrush = new SolidColorBrush(Colors.Black);
            if ((((Rectangle)sender).Fill) is ImageBrush)
            {
                colorBrush.Color = Colors.Transparent;
            }
            else
            {
                colorBrush = (SolidColorBrush)((Rectangle)sender).Fill;
                Color color = colorBrush.Color;
                color.A = 255;
                colorBrush = new SolidColorBrush(color);
            }

            changeValuesOfColourTextboxes(colorBrush.Color.R, colorBrush.Color.G, colorBrush.Color.B, colorBrush.Color.A);

            changeValuesOfColourSliders(colorBrush.Color.R, colorBrush.Color.G, colorBrush.Color.B, colorBrush.Color.A);
        }

        private void ColorChangedCustom(object sender, Color color)
        {
            var colorBrush = new SolidColorBrush(color);

            // TODO: changeValuesOfColourTextboxes(Coding4FunColorPicker.Color.R, Coding4FunColorPicker.Color.G,
            //    Coding4FunColorPicker.Color.B, (byte)sldAlpha.Value);

            // TODO: changeValuesOfColourSliders(Coding4FunColorPicker.Color.R, Coding4FunColorPicker.Color.G,
            //    Coding4FunColorPicker.Color.B, (byte)sldAlpha.Value);
        }

        private void BtnSelectedColor_OnClick(object sender, RoutedEventArgs e)
        {
            Color current_color = new Color();
            current_color.R = ((SolidColorBrush)BtnSelectedColor.Background).Color.R;
            current_color.G = ((SolidColorBrush)BtnSelectedColor.Background).Color.G;
            current_color.B = ((SolidColorBrush)BtnSelectedColor.Background).Color.B;
            current_color.A = (byte)(255 * (Convert.ToDouble(tbAlphaValue.Text) / 100));

            PocketPaintApplication.GetInstance().PaintData.BorderColorSelected = new SolidColorBrush(current_color);
            
            //this.Frame.Navigate(typeof(PaintingAreaView));
            this.Frame.GoBack();
        }

        private void sldSlidersChanged_ValueChanged(object sender, RoutedEventArgs e)
        {
            Color color = new Color();
            color.A = (byte)sldAlpha.Value;
            color.B = (byte)sldBlue.Value;
            color.G = (byte)sldGreen.Value;
            color.R = (byte)sldRed.Value;

            changeValuesOfColourTextboxes((byte)sldRed.Value, (byte)sldGreen.Value,
                (byte)sldBlue.Value, (byte)sldAlpha.Value);
            changeColorOfBtnSelectedColor(color);
            changeColorOfCoding4FunColorPicker((byte)sldRed.Value, (byte)sldGreen.Value,
                (byte)sldBlue.Value, (byte)sldAlpha.Value);
        }

        private void changeColorOfCoding4FunColorPicker(byte red, byte green, byte blue, byte alpha)
        {
            Color new_color = new Color();
            new_color.R = red;
            new_color.G = green;
            new_color.B = blue;

            new_color.A = alpha;
            // TODO: Coding4FunColorPicker.Color = new_color;
        }

        private void changeValuesOfColourTextboxes(byte red, byte green, byte blue, byte alpha)
        {
            tbRedValue.Text = red.ToString();
            tbBlueValue.Text = blue.ToString();
            tbGreenValue.Text = green.ToString();
            tbAlphaValue.Text = ((Int32)((Double)(alpha) / 2.55)).ToString();
        }

        private void changeValuesOfColourSliders(byte red, byte green, byte blue, byte alpha)
        {
            sldRed.Value = ((double)red);
            sldGreen.Value = ((double)green);
            sldBlue.Value = ((double)blue);
            sldAlpha.Value = (double)alpha;
        }

        private void Rectangle_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            BitmapImage image = new BitmapImage(new Uri("ms-resource:/File/Assets/ColorPicker/color.jpg"));
            BitmapImage bitmapImage = (BitmapImage)image;
            
            
        }

        private void Rectangle_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            
        }

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {         

        }

    }
}
