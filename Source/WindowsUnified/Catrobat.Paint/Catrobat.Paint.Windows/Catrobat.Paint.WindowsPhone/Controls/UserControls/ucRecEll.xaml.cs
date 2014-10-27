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
using Windows.UI.Xaml.Shapes;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkID=390556 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class ucRecEll
    {
        private int _last_valid_height;
        private int _last_valid_width;
        public ucRecEll()
        {
            this.InitializeComponent();

            tbStrokeThicknessValue.Text = PocketPaintApplication.GetInstance().PaintData.BorderThicknessRecEll.ToString();
            sldStrokeThickness.Value = PocketPaintApplication.GetInstance().PaintData.BorderThicknessRecEll;

            _last_valid_height = Convert.ToInt32(tbHeightValue.Text);
            _last_valid_width = Convert.ToInt32(tbWidthValue.Text);

            PocketPaintApplication.GetInstance().PaintData.BorderColorChanged += ColorStrokeChanged;
            PocketPaintApplication.GetInstance().PaintData.ColorChanged += ColorFillChanged;
            PocketPaintApplication.GetInstance().BarRecEllShape = this;
            setUcRecEllLayout();
        }

        private void setUcRecEllLayout()
        {
            var heightMultiplicator = PocketPaintApplication.GetInstance().size_height_multiplication;
            var widthMultiplicator = PocketPaintApplication.GetInstance().size_width_multiplication;

            gridMain.Height *= heightMultiplicator;
            gridMain.Width *= widthMultiplicator;

            foreach (Object obj in gridMain.Children.Concat(GrdSelecectedBorderColor.Children.Concat(GrdSelectedFillColor.Children)))
            {
                if (obj.GetType() == typeof(TextBox))
                {
                    TextBox currentTextBox = ((TextBox)obj);
                    currentTextBox.Height *= heightMultiplicator;
                    currentTextBox.Width *= widthMultiplicator;

                    currentTextBox.Margin = new Thickness(
                                            currentTextBox.Margin.Left * widthMultiplicator,
                                            currentTextBox.Margin.Top * heightMultiplicator,
                                            currentTextBox.Margin.Right * widthMultiplicator,
                                            currentTextBox.Margin.Bottom * heightMultiplicator);

                    currentTextBox.FontSize *= heightMultiplicator;
                }
                else if (obj.GetType() == typeof(Button))
                {
                    Button currentButton = ((Button)obj);
                    currentButton.Height *= heightMultiplicator;
                    currentButton.Width *= widthMultiplicator;

                    currentButton.Margin = new Thickness(
                                            currentButton.Margin.Left * widthMultiplicator,
                                            currentButton.Margin.Top * heightMultiplicator,
                                            currentButton.Margin.Right * widthMultiplicator,
                                            currentButton.Margin.Bottom * heightMultiplicator);

                    currentButton.FontSize *= heightMultiplicator;
                }
                else if (obj.GetType() == typeof(Rectangle))
                {
                    Rectangle currentRectangle = ((Rectangle)(obj));
                    currentRectangle.Height *= heightMultiplicator;
                    currentRectangle.Width *= widthMultiplicator;

                    currentRectangle.Margin = new Thickness(
                                            currentRectangle.Margin.Left * widthMultiplicator,
                                            currentRectangle.Margin.Top * heightMultiplicator,
                                            currentRectangle.Margin.Right * widthMultiplicator,
                                            currentRectangle.Margin.Bottom * heightMultiplicator);

                }
                else if(obj.GetType() == typeof(Slider))
                {
                    Slider currentSlider = ((Slider)(obj));
                    currentSlider.Height *= heightMultiplicator;
                    currentSlider.Width *= widthMultiplicator;

                    currentSlider.Margin = new Thickness(
                                            currentSlider.Margin.Left * widthMultiplicator,
                                            currentSlider.Margin.Top * heightMultiplicator,
                                            currentSlider.Margin.Right * widthMultiplicator,
                                            currentSlider.Margin.Bottom * heightMultiplicator);
                }
                else if(obj.GetType() == typeof(Image))
                {
                    Image currentImage = ((Image)obj);
                    currentImage.Height *= heightMultiplicator;
                    currentImage.Width *= widthMultiplicator;
                }
            }

            btnSelectedBorderColor.Background = PocketPaintApplication.GetInstance().PaintData.BorderColorSelected;
        }

        private void btnSelectedColor_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if(rootFrame != null)
            {
                PocketPaintApplication.GetInstance().is_border_color = true;
                rootFrame.Navigate(typeof(ViewColorPicker));
            }
        }

        private void btnSelectedFillColor_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame != null)
            {
                PocketPaintApplication.GetInstance().is_border_color = false;
                rootFrame.Navigate(typeof(ViewColorPicker));
            }
        }

        public void ColorFillChanged(SolidColorBrush color)
        {
            SolidColorBrush selected_color = new SolidColorBrush();
            selected_color.Color = color.Color != Colors.Transparent ? color.Color : Colors.Transparent;
            rectFillColor.Fill = selected_color;
        }

        private void ColorStrokeChanged(SolidColorBrush color)
        {
            SolidColorBrush selected_color = new SolidColorBrush();
            selected_color.Color = color.Color != Colors.Transparent ? color.Color : Colors.Transparent;
            rectBorderColor.Fill = selected_color;
        }

        public void setBorderColor()
        {
            rectBorderColor.Fill = PocketPaintApplication.GetInstance().PaintData.BorderColorSelected;
        }

        public void setFillColor()
        {
            rectFillColor.Fill = PocketPaintApplication.GetInstance().PaintData.ColorSelected;
        }

        public double getHeight()
        {
            int return_value = 0;
            return return_value = tbHeightValue.Text != string.Empty ? Convert.ToInt32(tbHeightValue.Text) : _last_valid_height;
        }

        public double getWidth()
        {
            int return_value = 0;
            return return_value = tbWidthValue.Text != string.Empty ? Convert.ToInt32(tbWidthValue.Text) : _last_valid_width;
        }

        private void sldSlidersChanged_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            int strokeThickness = (int)sldStrokeThickness.Value;
            tbStrokeThicknessValue.Text = strokeThickness.ToString();
            PocketPaintApplication.GetInstance().PaintData.BorderThicknessRecEll = strokeThickness;

            if (PocketPaintApplication.GetInstance().BarRecEllShape != null)
            {
                PocketPaintApplication.GetInstance().RectangleSelectionControl.setStrokeThicknessOfDrawingShape = strokeThickness;
            }

            if(strokeThickness > 0)
            {
                btnSelectedBorderColor.IsEnabled = true;
            }
            else
            {
                btnSelectedBorderColor.IsEnabled = false;
            }
        }

        private void tbHeightValue_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            tbHeightValue.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void tbHeightValue_LostFocus(object sender, RoutedEventArgs e)
        {
            tbHeightValue.Foreground = new SolidColorBrush(Colors.White);

            _last_valid_height = tbHeightValue.Text != string.Empty ? Convert.ToInt32(tbHeightValue.Text) : _last_valid_width;
            tbHeightValue.Text = _last_valid_height.ToString();
        }

        private void tbWidthValue_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            tbWidthValue.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void tbWidthValue_LostFocus(object sender, RoutedEventArgs e)
        {
            tbWidthValue.Foreground = new SolidColorBrush(Colors.White);

            _last_valid_width = tbWidthValue.Text != string.Empty ? Convert.ToInt32(tbWidthValue.Text) : _last_valid_width;
            tbWidthValue.Text = _last_valid_width.ToString();
        }

        private void tbHeightValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            char[] comma = new char[1];
            comma[0] = ',';
            tbHeightValue.Text =  tbHeightValue.Text.Trim(comma);

            if (tbHeightValue.Text != string.Empty)
            {
                double height_value = Convert.ToDouble(tbHeightValue.Text);
                if (PocketPaintApplication.GetInstance().RectangleSelectionControl != null)
                {
                    PocketPaintApplication.GetInstance().RectangleSelectionControl.changeHeightOfDrawingSelection(height_value);
                }
            }
        }

        private void tbWidthValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            char[] comma = new char[1];
            comma[0] = ',';
            tbWidthValue.Text = tbWidthValue.Text.Trim(comma);

            if (tbWidthValue.Text != string.Empty)
            {
                double width_value = Convert.ToDouble(tbWidthValue.Text);
                PocketPaintApplication.GetInstance().RectangleSelectionControl.changeWidthOfDrawingSelection(width_value);
            //    // PocketPaintApplication.GetInstance().CurrentShape.Width = Convert.ToDouble(tbWidthValue.Text);
            //    PocketPaintApplication.GetInstance().RecDrawingRectangle.Width = width_value;
            }
        }

        private void tbHeightValue_LostFocus_1(object sender, RoutedEventArgs e)
        {
            tbHeightValue.Foreground = new SolidColorBrush(Colors.White);

            _last_valid_width = tbHeightValue.Text != string.Empty ? Convert.ToInt32(tbHeightValue.Text) : _last_valid_width;
            tbHeightValue.Text = _last_valid_width.ToString();
        }

        public double setTbHeightValue
        {
            get
            {
                return Convert.ToInt32(tbHeightValue.Text);
            }
            set
            {
                tbHeightValue.Text = value.ToString();
            }
        }

        public double setTbWidthValue
        {
            get
            {
                return Convert.ToInt32(tbWidthValue.Text);
            }
            set
            {
                tbWidthValue.Text = value.ToString();
            }
        }

        private void tbHeightValue_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            tbHeightValue.Foreground = new SolidColorBrush(Colors.White);
        }

        private void tbWidthValue_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            tbWidthValue.Foreground = new SolidColorBrush(Colors.White);
        }

        private void tbWidthValue_GotFocus(object sender, RoutedEventArgs e)
        {
            tbWidthValue.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void tbHeightValue_GotFocus(object sender, RoutedEventArgs e)
        {
            tbHeightValue.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}
