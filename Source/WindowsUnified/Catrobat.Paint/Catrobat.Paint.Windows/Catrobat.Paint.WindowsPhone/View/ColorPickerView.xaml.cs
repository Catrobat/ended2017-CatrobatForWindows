using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Catrobat.Paint.Phone.View
{
    public partial class ColorPickerView : PhoneApplicationPage
    {
        public ColorPickerView()
        {

            InitializeComponent();
            SolidColorBrush fill_color = PocketPaintApplication.GetInstance().PaintData.ColorSelected;
            double fill_color_opacity = fill_color.Color.A;


            if (PocketPaintApplication.GetInstance().PaintData.ColorSelected != null)
            {
                Color selected_color = ((SolidColorBrush)PocketPaintApplication.GetInstance().PaintData.ColorSelected).Color;

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

            changeValuesOfColourTextboxes(Coding4FunColorPicker.Color.R, Coding4FunColorPicker.Color.G,
                Coding4FunColorPicker.Color.B, (byte)sldAlpha.Value);

            changeValuesOfColourSliders(Coding4FunColorPicker.Color.R, Coding4FunColorPicker.Color.G,
                Coding4FunColorPicker.Color.B, (byte)sldAlpha.Value);
        }


        private void BtnSelectedColor_OnClick(object sender, RoutedEventArgs e)
        {
            Color current_color = new Color();
            current_color.R = ((SolidColorBrush)BtnSelectedColor.Background).Color.R;
            current_color.G = ((SolidColorBrush)BtnSelectedColor.Background).Color.G;
            current_color.B = ((SolidColorBrush)BtnSelectedColor.Background).Color.B;
            current_color.A = (byte)(255 * (Convert.ToDouble(tbAlphaValue.Text) / 100));

            PocketPaintApplication.GetInstance().PaintData.ColorSelected = new SolidColorBrush(current_color);
            NavigationService.GoBack();
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
            Coding4FunColorPicker.Color = new_color;
        }

        private void changeValuesOfColourTextboxes(byte red, byte green, byte blue, byte alpha)
        {
            tbRedValue.Text = red.ToString();
            tbBlueValue.Text = green.ToString();
            tbGreenValue.Text = blue.ToString();
            tbAlphaValue.Text = ((Int32)((Double)(alpha) / 2.55)).ToString();
        }

        private void changeValuesOfColourSliders(byte red, byte green, byte blue, byte alpha)
        {
            sldRed.Value = ((double)red);
            sldGreen.Value = ((double)green);
            sldBlue.Value = ((double)blue);
            sldAlpha.Value = (double)alpha;
        }
    }
}