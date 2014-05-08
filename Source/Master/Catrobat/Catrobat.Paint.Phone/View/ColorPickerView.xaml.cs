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
            //SelectedColorRectangle.Fill = fill_color;
            //SelectedColorRectangleSlider.Fill = fill_color;
            PocketPaintApplication.GetInstance().PaintData.ColorChanged += PaintDataOnColorChanged;

            if (PocketPaintApplication.GetInstance().PaintData.ColorSelected != null)
            {
                Color selected_color = ((SolidColorBrush)PocketPaintApplication.GetInstance().PaintData.ColorSelected).Color;
                MessageBox.Show("Alpha-Value: " + selected_color.A);
                int reference_color = (selected_color.R + selected_color.G + selected_color.B) / 3;
                if (reference_color <= 128 && selected_color.A > 5)
                {
                    BtnSelectedColor.Foreground = new SolidColorBrush(Colors.White);
                    BtnSelectedColorSlider.Foreground = new SolidColorBrush(Colors.White);
                    BtnSelectedColor.BorderBrush = new SolidColorBrush(Colors.White);
                    BtnSelectedColorSlider.BorderBrush = new SolidColorBrush(Colors.White);
                }
                else
                {
                    BtnSelectedColor.Foreground = new SolidColorBrush(Colors.Black);
                    BtnSelectedColorSlider.Foreground = new SolidColorBrush(Colors.Black);
                    BtnSelectedColor.BorderBrush = new SolidColorBrush(Colors.White);
                    BtnSelectedColorSlider.BorderBrush = new SolidColorBrush(Colors.White);
                }

                if (selected_color == Colors.Transparent)
                {
                    recTransparence.Visibility = Visibility.Visible;
                    // SelectedColorRectangle.Visibility = Visibility.Collapsed;
                    BtnSelectedColor.Background = new SolidColorBrush(Colors.Transparent);

                    recTransparenceSlider.Visibility = Visibility.Visible;
                    // SelectedColorRectangleSlider.Visibility = Visibility.Collapsed;
                    BtnSelectedColorSlider.Background = new SolidColorBrush(Colors.Transparent);
                    BtnSelectedColor.BorderBrush = new SolidColorBrush(Colors.White);
                    BtnSelectedColorSlider.BorderBrush = new SolidColorBrush(Colors.White);
                }
                else
                {
                    BtnSelectedColor.Background = new SolidColorBrush(selected_color);
                    BtnSelectedColorSlider.Background = new SolidColorBrush(selected_color);

                }

                changeColorOfCoding4FunColorPicker(selected_color.R, selected_color.G, selected_color.B, selected_color.A);
                changeValuesOfColourTextboxes(selected_color.R, selected_color.G, selected_color.B, selected_color.A);
                changeValuesOfColourSliders(selected_color.R, selected_color.G, selected_color.B, selected_color.A);
            }            
        }

        private void PaintDataOnColorChanged(SolidColorBrush color)
        {
            //SelectedColorRectangle.Fill = color;
            //SelectedColorRectangleSlider.Fill = color;

            Color current_color = ((SolidColorBrush)color).Color;
            int reference_color = (current_color.R + current_color.G + current_color.B) / 3;
            if (reference_color <= 128 && current_color.A > 5)
            {
                BtnSelectedColor.Foreground = new SolidColorBrush(Colors.White);
                BtnSelectedColorSlider.Foreground = new SolidColorBrush(Colors.White);
                BtnSelectedColor.BorderBrush = new SolidColorBrush(Colors.White);
                BtnSelectedColorSlider.BorderBrush = new SolidColorBrush(Colors.White);
            }
            else
            {
                BtnSelectedColor.Foreground = new SolidColorBrush(Colors.Black);
                BtnSelectedColorSlider.Foreground = new SolidColorBrush(Colors.Black);
                BtnSelectedColor.BorderBrush = new SolidColorBrush(Colors.White);
                BtnSelectedColorSlider.BorderBrush = new SolidColorBrush(Colors.White);
            }

            if (color.Color == Colors.Transparent)
            {
                //recTransparence.Visibility = Visibility.Visible;
                //SelectedColorRectangle.Visibility = Visibility.Collapsed;
                BtnSelectedColor.Background = new SolidColorBrush(Colors.Transparent);

                //recTransparenceSlider.Visibility = Visibility.Visible;
                //SelectedColorRectangleSlider.Visibility = Visibility.Collapsed;
                BtnSelectedColorSlider.Background = new SolidColorBrush(Colors.Transparent);
                BtnSelectedColor.BorderBrush = new SolidColorBrush(Colors.White);
                BtnSelectedColorSlider.BorderBrush = new SolidColorBrush(Colors.White);

            }
            else
            {
                BtnSelectedColor.Background = color;
                //recTransparence.Visibility = Visibility.Collapsed;
                //SelectedColorRectangle.Visibility = Visibility.Visible;

                BtnSelectedColorSlider.Background = color;
                //recTransparenceSlider.Visibility = Visibility.Collapsed;
                //SelectedColorRectangleSlider.Visibility = Visibility.Visible;
            }

            changeColorOfCoding4FunColorPicker(color.Color.R, color.Color.G, color.Color.B, (byte)sldAlpha.Value);
            changeValuesOfColourTextboxes(color.Color.R, color.Color.G, color.Color.B, (byte)sldAlpha.Value);
            changeValuesOfColourSliders(color.Color.R, color.Color.G, color.Color.B, (byte)sldAlpha.Value);

        }


        private void ColorChangedN(object sender, RoutedEventArgs routedEventArgs)
        {
            var colorBrush = new SolidColorBrush(Colors.Black);
            if ((((Rectangle) sender).Fill) is ImageBrush)
            {
                colorBrush.Color = Colors.Transparent;

            }
            else
            {
                colorBrush = (SolidColorBrush)((Rectangle)sender).Fill;

            }
            MessageBox.Show("Alpha_in_color_change: " + colorBrush.Color.A);
            MessageBox.Show("Alpha_in_color_change: " + colorBrush.Opacity.ToString());
            PocketPaintApplication.GetInstance().PaintData.ColorSelected = colorBrush;

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
            //recTransparence.Opacity = ((Double)(alpha)) / 255.0;
            //SelectedColorRectangle.Opacity = ((Double)(alpha)) / 255.0;
           // BtnSelectedColor.Background.Opacity = ((Double)(alpha)) / 255.0;

            //recTransparenceSlider.Opacity = ((Double)(alpha)) / 255.0;
            //SelectedColorRectangleSlider.Opacity = ((Double)(alpha)) / 255.0;
            //BtnSelectedColorSlider.Background.Opacity = ((Double)(alpha)) / 255.0;
        }

        private void BtnSelectedColor_OnClick(object sender, RoutedEventArgs e)
        {
            Color current_color = ((SolidColorBrush)PocketPaintApplication.GetInstance().PaintData.ColorSelected).Color;
            if( current_color == Colors.Transparent)
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
            
            NavigationService.GoBack();
        }

        private void HeaderTemplate_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void HeaderTemplate_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void sldRed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbRed.Text = ((Int32)sldRed.Value).ToString();
            changeColorOfCoding4FunColorPicker((byte)sldRed.Value, (byte)sldGreen.Value,
                (byte)sldBlue.Value, (byte)sldAlpha.Value);
        }

        private void sldGreen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbGreen.Text = ((Int32)sldGreen.Value).ToString();
            changeColorOfCoding4FunColorPicker((byte)sldRed.Value, (byte)sldGreen.Value,
                (byte)sldBlue.Value, (byte)sldAlpha.Value);
        }

        private void sldBlue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbBlue.Text = ((Int32)sldBlue.Value).ToString();
            changeColorOfCoding4FunColorPicker((byte)sldRed.Value, (byte)sldGreen.Value,
                (byte)sldBlue.Value, (byte)sldAlpha.Value);       
        }

        private void sldAlpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbAlpha.Text = ((Int32)((sldAlpha.Value / 255) * 100)).ToString();
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

            changeOpacityOfColorButtons(alpha);
        }
        private void changeValuesOfColourTextboxes(byte red, byte green, byte blue, byte alpha)
        {
            tbRed.Text = red.ToString();
            tbBlue.Text = green.ToString(); 
            tbGreen.Text = blue.ToString();
            tbAlpha.Text = ((Int32)((Double)(alpha) / 2.55)).ToString();
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