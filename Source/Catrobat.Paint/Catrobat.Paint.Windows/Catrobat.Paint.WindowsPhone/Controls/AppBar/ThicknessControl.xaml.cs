using System;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkID=390556 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.AppBar
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class ThicknessControl
    {
        Int32 slider_thickness_textbox_last_value = 1;
        double width_multiplicator = PocketPaintApplication.GetInstance().size_width_multiplication;
        double height_multiplicator = PocketPaintApplication.GetInstance().size_width_multiplication;

        public ThicknessControl()
        {
            this.InitializeComponent();
            setLayout();
        }

        private void setLayout()
        {
            GrdLayoutRoot.Width *= width_multiplicator;
            GrdLayoutRoot.Height *= height_multiplicator;
            GrdThicknessKeyboard.Width *= width_multiplicator;
            GrdThicknessKeyboard.Height *= height_multiplicator;

            GrdSliderThickness.Width *= width_multiplicator;
            GrdSliderThickness.Height *= height_multiplicator;
            GrdSliderThickness.Margin = new Thickness(
                                            GrdSliderThickness.Margin.Left * width_multiplicator,
                                            GrdSliderThickness.Margin.Top * height_multiplicator,
                                            GrdSliderThickness.Margin.Right * width_multiplicator,
                                            GrdSliderThickness.Margin.Bottom * height_multiplicator);

            foreach (Object obj in GrdLayoutRoot.Children.Concat(GrdBrushType.Children.Concat(GrdSlider.Children.Concat(GrdThicknessKeyboard.Children))))
            {
                if (obj.GetType() == typeof(Button))
                {
                    Button button = ((Button)obj);
                    button.Height *= height_multiplicator;
                    button.Width *= width_multiplicator;

                    button.Margin = new Thickness(
                                            button.Margin.Left * width_multiplicator,
                                            button.Margin.Top * height_multiplicator,
                                            button.Margin.Right * width_multiplicator,
                                            button.Margin.Bottom * height_multiplicator);

                    button.FontSize *= height_multiplicator;

                    var buttonContent = ((Button)obj).Content;
                    if (buttonContent != null && buttonContent.GetType() == typeof(Image))
                    {
                        Image contentImage = (Image)buttonContent;
                        contentImage.Height *= height_multiplicator;
                        contentImage.Width *= width_multiplicator;

                        contentImage.Margin = new Thickness(
                                                contentImage.Margin.Left * width_multiplicator,
                                                contentImage.Margin.Top * height_multiplicator,
                                                contentImage.Margin.Right * width_multiplicator,
                                                contentImage.Margin.Bottom * height_multiplicator);
                    }
                }
                else if (obj.GetType() == typeof(Slider))
                {
                    Slider slider = (Slider)obj;
                    slider.Height *= height_multiplicator;
                    slider.Width *= width_multiplicator;

                    slider.Margin = new Thickness(
                                            slider.Margin.Left * width_multiplicator,
                                            slider.Margin.Top * height_multiplicator,
                                            slider.Margin.Right * width_multiplicator,
                                            slider.Margin.Bottom * height_multiplicator);
                }
            }

            SliderThickness.Value = PocketPaintApplication.GetInstance().PaintData.thicknessSelected;
        }

        public void checkAndSetPenLineCap(PenLineCap penLineCap)
        {
            SolidColorBrush brushGray = new SolidColorBrush(Colors.Gray);
            SolidColorBrush brushWhite = new SolidColorBrush(Colors.Black);
            if (penLineCap == PenLineCap.Round)
            {
                BtnRoundImage.BorderBrush = brushWhite;
                BtnSquareImage.BorderBrush = brushGray;
                BtnTriangleImage.BorderBrush = brushGray;
            }
            else if (penLineCap == PenLineCap.Square)
            {
                BtnRoundImage.BorderBrush = brushGray;
                BtnSquareImage.BorderBrush = brushWhite;
                BtnTriangleImage.BorderBrush = brushGray;
            }
            else
            {
                BtnRoundImage.BorderBrush = brushGray;
                BtnSquareImage.BorderBrush = brushGray;
                BtnTriangleImage.BorderBrush = brushWhite;
            }
        }

        public void RoundButton_OnClick(object sender, RoutedEventArgs e)
        {
            var penLineCap = PenLineCap.Round;
            PocketPaintApplication.GetInstance().PaintData.penLineCapSelected = penLineCap;
            PocketPaintApplication.GetInstance().cursorControl.changeCursorType(penLineCap);
            checkAndSetPenLineCap(penLineCap);
        }

        public void SquareButton_OnClick(object sender, RoutedEventArgs e)
        {
            var penLineCap = PenLineCap.Square;
            PocketPaintApplication.GetInstance().PaintData.penLineCapSelected = penLineCap;
            PocketPaintApplication.GetInstance().cursorControl.changeCursorType(penLineCap);
            checkAndSetPenLineCap(penLineCap);
        }

        public void setValueBtnBrushThickness(int value)
        {
            BtnBrushThickness.Content = value.ToString();
        }

        public void setValueSliderThickness(double value)
        {
            SliderThickness.Value = value;
        }

        public void TriangleButton_OnClick(object sender, RoutedEventArgs e)
        {
            var penLineCap = PenLineCap.Triangle;
            PocketPaintApplication.GetInstance().PaintData.penLineCapSelected = penLineCap;
            PocketPaintApplication.GetInstance().cursorControl.changeCursorType(penLineCap);
            checkAndSetPenLineCap(penLineCap);
        }

        private void SliderThickness_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (SliderThickness != null)
            {
                BtnBrushThickness.Content = Convert.ToInt32(SliderThickness.Value).ToString();
                slider_thickness_textbox_last_value = Convert.ToInt32(SliderThickness.Value);
                PocketPaintApplication.GetInstance().PaintData.thicknessSelected = Convert.ToInt32(SliderThickness.Value);
                if (PocketPaintApplication.GetInstance().cursorControl != null)
                {
                    PocketPaintApplication.GetInstance().cursorControl.changeCursorsize();
                }
            }
        }

        private void ButtonNumbers_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                string get_clicked_button_number = button.Name.Substring(8);
                if (BtnBrushThickness.Content == null || BtnBrushThickness.Content.ToString().Length < 2)
                {
                    BtnBrushThickness.Content += get_clicked_button_number;
                }
                else if (BtnBrushThickness.Content.ToString().Length == 2)
                {
                    BtnBrushThickness.Content = "";
                    BtnBrushThickness.Content += get_clicked_button_number;
                }
                checkIfValueIsInRange(false);
            }
        }

        private void checkIfValueIsInRange(bool pressed_accept)
        {
            if (BtnBrushThickness.Content == null)
            {
                btnValue0.IsEnabled = true;
                btnValue1.IsEnabled = true;
                btnValue2.IsEnabled = true;
                btnValue3.IsEnabled = true;
                btnValue4.IsEnabled = true;
                btnValue5.IsEnabled = true;
                btnValue6.IsEnabled = true;
                btnValue7.IsEnabled = true;
                btnValue8.IsEnabled = true;
                btnValue9.IsEnabled = true;
                btnValue0.IsEnabled = false;
            }
            else
            {
                Int32 input = Convert.ToInt32(BtnBrushThickness.Content.ToString());
                if (input > 5 && input < 10)
                {
                    btnValue0.IsEnabled = false;
                    btnValue1.IsEnabled = false;
                    btnValue2.IsEnabled = false;
                    btnValue3.IsEnabled = false;
                    btnValue4.IsEnabled = false;
                    btnValue5.IsEnabled = false;
                    btnValue6.IsEnabled = false;
                    btnValue7.IsEnabled = false;
                    btnValue8.IsEnabled = false;
                    btnValue9.IsEnabled = false;
                }
                else if (input == 5)
                {
                    btnValue0.IsEnabled = true;
                    btnValue1.IsEnabled = false;
                    btnValue2.IsEnabled = false;
                    btnValue3.IsEnabled = false;
                    btnValue4.IsEnabled = false;
                    btnValue5.IsEnabled = false;
                    btnValue6.IsEnabled = false;
                    btnValue7.IsEnabled = false;
                    btnValue8.IsEnabled = false;
                    btnValue9.IsEnabled = false;
                }
                else if (input < 5)
                {
                    btnValue0.IsEnabled = true;
                    btnValue1.IsEnabled = true;
                    btnValue2.IsEnabled = true;
                    btnValue3.IsEnabled = true;
                    btnValue4.IsEnabled = true;
                    btnValue5.IsEnabled = true;
                    btnValue6.IsEnabled = true;
                    btnValue7.IsEnabled = true;
                    btnValue8.IsEnabled = true;
                    btnValue9.IsEnabled = true;
                }
                else
                {
                    btnValue0.IsEnabled = false;
                    btnValue1.IsEnabled = true;
                    btnValue2.IsEnabled = true;
                    btnValue3.IsEnabled = true;
                    btnValue4.IsEnabled = true;
                    btnValue5.IsEnabled = true;
                    btnValue6.IsEnabled = true;
                    btnValue7.IsEnabled = true;
                    btnValue8.IsEnabled = true;
                    btnValue9.IsEnabled = true;
                }

                SliderThickness.Value = Convert.ToDouble(input);
            }
        }

        private void btnDeleteNumbers_Click(object sender, RoutedEventArgs e)
        {
            if (BtnBrushThickness.Content != null && BtnBrushThickness.Content.ToString().Length > 0)
            {
                BtnBrushThickness.Content = BtnBrushThickness.Content.ToString().Remove(BtnBrushThickness.Content.ToString().Length - 1);
            }

            checkIfValueIsInRange(false);
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            checkIfThicknessWasEntered();
            checkIfValueIsInRange(true);

            GrdThicknessKeyboard.Visibility = Visibility.Collapsed;
            GrdSliderThickness.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
        }

        public void checkIfThicknessWasEntered()
        {
            string slider_thickness_text_box_value = string.Empty;
            if (BtnBrushThickness.Content != null)
            {
                slider_thickness_text_box_value = BtnBrushThickness.Content.ToString();
            }
            Int32 slider_thickness_text_box_int_value;

            if (!slider_thickness_text_box_value.Equals(""))
            {
                slider_thickness_text_box_int_value = Convert.ToInt32(slider_thickness_text_box_value);

                if (!(slider_thickness_text_box_int_value >= 1 && slider_thickness_text_box_int_value <= 50))
                {
                    BtnBrushThickness.Content = slider_thickness_textbox_last_value.ToString();
                }
                else
                {
                    slider_thickness_textbox_last_value = slider_thickness_text_box_int_value;
                    SliderThickness.Value = slider_thickness_text_box_int_value;
                }
            }
            else
            {
                BtnBrushThickness.Content = slider_thickness_textbox_last_value.ToString();
            }

            BtnBrushThickness.Foreground = new SolidColorBrush(Colors.White);
        }

        private void BtnBrushThickness_Click(object sender, RoutedEventArgs e)
        {
            checkIfThicknessWasEntered();
            if (GrdThicknessKeyboard.Visibility == Visibility.Collapsed)
            {
                GrdThicknessKeyboard.Visibility = Visibility.Visible;
                GrdSliderThickness.Margin = new Thickness(0.0, 0.0, 0.0, (180.0 * height_multiplicator));
            }
            else
            {
                GrdThicknessKeyboard.Visibility = Visibility.Collapsed;
                GrdSliderThickness.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
            }
        }
        
    }
}
