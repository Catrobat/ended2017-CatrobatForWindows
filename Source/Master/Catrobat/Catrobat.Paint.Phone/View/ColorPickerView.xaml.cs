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
            Brush fill_color = PocketPaintApplication.GetInstance().PaintData.ColorSelected;
            double fill_color_opacity = fill_color.Opacity * 255.0;

            PocketPaintApplication.GetInstance().PaintData.ColorChanged += PaintDataOnColorChanged;

            if (PocketPaintApplication.GetInstance().PaintData.ColorSelected != null)
            {
                Color selected_color = ((SolidColorBrush)PocketPaintApplication.GetInstance().PaintData.ColorSelected).Color;
                //MessageBox.Show("Alpha-Value: " + selected_color.A);
                int reference_color = (selected_color.R + selected_color.G + selected_color.B) / 3;
                if (reference_color <= 128 && (selected_color.A > 5 || fill_color_opacity > 5.0))
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

                changeColorOfCoding4FunColorPicker(selected_color.R, selected_color.G, selected_color.B, (byte)fill_color_opacity);
                changeValuesOfColourTextboxes(selected_color.R, selected_color.G, selected_color.B, (byte)fill_color_opacity);
                changeValuesOfColourSliders(selected_color.R, selected_color.G, selected_color.B, (byte)fill_color_opacity);

                changeOpacityOfColorButtons((byte)fill_color_opacity);
            }
        }

        private void PaintDataOnColorChanged(SolidColorBrush color)
        {
            changeColorOfFinishButton(color);

            changeColorOfCoding4FunColorPicker(color.Color.R, color.Color.G, color.Color.B, (byte)sldAlpha.Value);
            changeValuesOfColourTextboxes(color.Color.R, color.Color.G, color.Color.B, (byte)sldAlpha.Value);
            changeValuesOfColourSliders(color.Color.R, color.Color.G, color.Color.B, (byte)sldAlpha.Value);
        }


        private void ColorChangedN(object sender, RoutedEventArgs routedEventArgs)
        {
            var colorBrush = new SolidColorBrush(Colors.Black);
            if ((((Rectangle)sender).Fill) is ImageBrush)
            {
                colorBrush.Color = Colors.Transparent;
            }
            else
            {
                colorBrush = (SolidColorBrush)((Rectangle)sender).Fill;
            }
            var colorBrush_Opacity = (colorBrush.Color.A / 255.0);

            PocketPaintApplication.GetInstance().PaintData.ColorSelected = colorBrush;
            PocketPaintApplication.GetInstance().PaintData.ColorSelected.Opacity = colorBrush_Opacity;
        }

        private void ColorChanged(object sender, Color color)
        {
            var colorBrush = new SolidColorBrush(color);
            PocketPaintApplication.GetInstance().PaintData.ColorSelected = colorBrush;

            changeValuesOfColourTextboxes(Coding4FunColorPicker.Color.R, Coding4FunColorPicker.Color.G,
                Coding4FunColorPicker.Color.B, (byte)sldAlpha.Value);

            changeValuesOfColourSliders(Coding4FunColorPicker.Color.R, Coding4FunColorPicker.Color.G,
                Coding4FunColorPicker.Color.B, (byte)sldAlpha.Value);

            changeOpacityOfColorButtons(Coding4FunColorPicker.Color.A);
        }

        private void changeOpacityOfColorButtons(byte alpha)
        {
            BtnSelectedColor.Background.Opacity = ((Double)(alpha)) / 255.0;
            BtnSelectedColorSlider.Background.Opacity = ((Double)(alpha)) / 255.0;
        }

        private void BtnSelectedColor_OnClick(object sender, RoutedEventArgs e)
        {
            Color current_color = ((SolidColorBrush)PocketPaintApplication.GetInstance().PaintData.ColorSelected).Color;
            if (current_color == Colors.Transparent)
            {
                PocketPaintApplication.GetInstance().SwitchTool(Catrobat.Paint.Phone.Tool.ToolType.Eraser);
            }
            else
            {
                if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == Catrobat.Paint.Phone.Tool.ToolType.Eraser)
                {
                    PocketPaintApplication.GetInstance().SwitchTool(Catrobat.Paint.Phone.Tool.ToolType.Brush);
                }
            }

            ColorChanged(sender, Coding4FunColorPicker.Color);
            NavigationService.GoBack();
        }

        private void sldRed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbRedValue.Text = ((Int32)sldRed.Value).ToString();
            Color color = new Color();
            color.A = (byte)sldAlpha.Value;
            color.B = (byte)sldBlue.Value;
            color.G = (byte)sldGreen.Value;
            color.R = (byte)sldRed.Value;
            changeColorOfFinishButton(new SolidColorBrush(color));
            changeColorOfCoding4FunColorPicker((byte)sldRed.Value, (byte)sldGreen.Value,
                (byte)sldBlue.Value, (byte)sldAlpha.Value);
        }

        private void sldGreen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbGreenValue.Text = ((Int32)sldGreen.Value).ToString();
            Color color = new Color();
            color.A = (byte)sldAlpha.Value;
            color.B = (byte)sldBlue.Value;
            color.G = (byte)sldGreen.Value;
            color.R = (byte)sldRed.Value;
            changeColorOfFinishButton(new SolidColorBrush(color));
            changeColorOfCoding4FunColorPicker((byte)sldRed.Value, (byte)sldGreen.Value,
                (byte)sldBlue.Value, (byte)sldAlpha.Value);
        }

        private void sldBlue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbBlueValue.Text = ((Int32)sldBlue.Value).ToString();
            Color color = new Color();
            color.A = (byte)sldAlpha.Value;
            color.B = (byte)sldBlue.Value;
            color.G = (byte)sldGreen.Value;
            color.R = (byte)sldRed.Value;

            changeColorOfFinishButton(new SolidColorBrush(color));
            changeColorOfCoding4FunColorPicker((byte)sldRed.Value, (byte)sldGreen.Value,
                (byte)sldBlue.Value, (byte)sldAlpha.Value);
        }

        private void sldAlpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbAlphaValue.Text = ((Int32)((sldAlpha.Value / 255) * 100)).ToString();
            Color color = new Color();
            color.A = (byte)sldAlpha.Value;
            color.B = (byte)sldBlue.Value;
            color.G = (byte)sldGreen.Value;
            color.R = (byte)sldRed.Value;

            changeColorOfFinishButton(new SolidColorBrush(color));
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

            //changeColorOfFinishButton(new SolidColorBrush(new_color));
            //changeOpacityOfColorButtons(alpha);
            //PocketPaintApplication.GetInstance().PaintData.ColorSelected = new SolidColorBrush(new_color);
            //PocketPaintApplication.GetInstance().PaintData.ColorSelected.Opacity = ((Double)(alpha)) / 255.0;
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

        private void changeColorOfFinishButton(SolidColorBrush color)
        {
            Color current_color = color.Color;
            int reference_color = (current_color.R + current_color.G + current_color.B) / 3;
            if (reference_color <= 128 && current_color.A > 5)
            {
                BtnSelectedColor.Foreground = new SolidColorBrush(Colors.White);
                BtnSelectedColorSlider.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                BtnSelectedColor.Foreground = new SolidColorBrush(Colors.Black);
                BtnSelectedColorSlider.Foreground = new SolidColorBrush(Colors.Black);
            }

            if (color.Color == Colors.Transparent)
            {
                BtnSelectedColor.Background = new SolidColorBrush(Colors.Transparent);
                BtnSelectedColorSlider.Background = new SolidColorBrush(Colors.Transparent);

                BtnSelectedColor.BorderBrush = new SolidColorBrush(Colors.White);
                BtnSelectedColorSlider.BorderBrush = new SolidColorBrush(Colors.White);
            }
            else
            {
                BtnSelectedColor.Background = color;
                BtnSelectedColorSlider.Background = color;
            }
        }
    }
}