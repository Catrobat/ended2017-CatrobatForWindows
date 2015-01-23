using Catrobat.Paint.WindowsPhone.Tool;
using System;
using System.Linq;
// TODO: using Coding4Fun.Toolkit;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkID=390556 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.View
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class ViewColorPicker : Page
    {
        public ViewColorPicker()
        {
            this.InitializeComponent();
            Color selected_color;
            double color_opacity;
            if (PocketPaintApplication.GetInstance().is_border_color && PocketPaintApplication.GetInstance().PaintData.strokeColorSelected != null)
            {
                selected_color = ((SolidColorBrush)PocketPaintApplication.GetInstance().PaintData.strokeColorSelected).Color;
                color_opacity = Convert.ToDouble(((SolidColorBrush)PocketPaintApplication.GetInstance().PaintData.strokeColorSelected).Color.A);

                changeValuesOfColourSliders(selected_color.R, selected_color.G, selected_color.B, (byte)color_opacity);
                changeColorOfBtnSelectedColor(selected_color);
            }
            else if (!PocketPaintApplication.GetInstance().is_border_color && PocketPaintApplication.GetInstance().PaintData.colorSelected != null)
            {
                selected_color = ((SolidColorBrush)PocketPaintApplication.GetInstance().PaintData.colorSelected).Color;
                color_opacity = Convert.ToDouble(((SolidColorBrush)PocketPaintApplication.GetInstance().PaintData.colorSelected).Color.A);

                changeValuesOfColourSliders(selected_color.R, selected_color.G, selected_color.B, (byte)color_opacity);
                changeColorOfBtnSelectedColor(selected_color);
            }

            HeaderTemplate.Height = Window.Current.Bounds.Height;
            piFirstPage.Height = Window.Current.Bounds.Height;

            setColorPickerLayout();
        }

        /// <summary>
        /// Wird aufgerufen, wenn diese Seite in einem Frame angezeigt werden soll.
        /// </summary>
        /// <param name="e">Ereignisdaten, die beschreiben, wie diese Seite erreicht wurde.
        /// Dieser Parameter wird normalerweise zum Konfigurieren der Seite verwendet.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SolidColorBrush fill_color;
            if (PocketPaintApplication.GetInstance().is_border_color)
            {
                fill_color = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
            }
            else
            {
                fill_color = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            }

            double fill_color_opacity = Convert.ToDouble(fill_color.Color.A);


            if (PocketPaintApplication.GetInstance().PaintData.colorSelected != null)
            {
                Color selected_color;
                if (PocketPaintApplication.GetInstance().is_border_color)
                {
                    selected_color = ((SolidColorBrush)PocketPaintApplication.GetInstance().PaintData.strokeColorSelected).Color;
                }
                else
                {
                    selected_color = ((SolidColorBrush)PocketPaintApplication.GetInstance().PaintData.colorSelected).Color;
                }

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
                TbFertig.Foreground = new SolidColorBrush(Colors.White);
                TbFertigSlider.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                TbFertig.Foreground = new SolidColorBrush(Colors.Black);
                TbFertigSlider.Foreground = new SolidColorBrush(Colors.Black);
            }

            if (selected_color == Colors.Transparent)
            {
                RecSelectedColor.Fill = new SolidColorBrush(Colors.Transparent);
                RecSelectedColorSlider.Fill = new SolidColorBrush(Colors.Transparent);

                BtnSelectedColor.BorderBrush = new SolidColorBrush(Colors.White);
                BtnSelectedColorSlider.BorderBrush = new SolidColorBrush(Colors.White);
            }
            else
            {
                RecSelectedColor.Fill = new SolidColorBrush(selected_color);
                RecSelectedColorSlider.Fill = new SolidColorBrush(selected_color);
            }
        }

        private void ColorChangedPredefined(object sender, RoutedEventArgs routedEventArgs)
        {
            var colorBrush = new SolidColorBrush(Colors.Black);
            //if ((((Rectangle)sender).Fill) is ImageBrush)
            //{
            //    colorBrush.Color = Colors.Transparent;
            //}
            //else
            //{
                colorBrush = (SolidColorBrush)((Rectangle)sender).Fill;
                Color color = colorBrush.Color;
                color.A = colorBrush.Color.A;
                colorBrush = new SolidColorBrush(color);
            //}

            changeValuesOfColourTextboxes(colorBrush.Color.R, colorBrush.Color.G, colorBrush.Color.B, colorBrush.Color.A);

            changeValuesOfColourSliders(colorBrush.Color.R, colorBrush.Color.G, colorBrush.Color.B, colorBrush.Color.A);
        }

        private void ColorChangedCustom(object sender, Color color)
        {
            var colorBrush = new SolidColorBrush(color);

            changeValuesOfColourTextboxes(colorPicker.Color.R, colorPicker.Color.G,
                colorPicker.Color.B, (byte)sldAlpha.Value);

            changeValuesOfColourSliders(colorPicker.Color.R, colorPicker.Color.G,
                colorPicker.Color.B, (byte)sldAlpha.Value);
        }

        private void BtnSelectedColor_OnClick(object sender, RoutedEventArgs e)
        {
            Color current_color = new Color();
            current_color.R = ((SolidColorBrush)RecSelectedColor.Fill).Color.R;
            current_color.G = ((SolidColorBrush)RecSelectedColor.Fill).Color.G;
            current_color.B = ((SolidColorBrush)RecSelectedColor.Fill).Color.B;
            current_color.A = (byte)(255 * (Convert.ToDouble(tbAlphaValue.Text) / 100));

            if ( PocketPaintApplication.GetInstance().is_border_color )
            {
                var current_solid_brush = new SolidColorBrush(current_color);
                PocketPaintApplication.GetInstance().PaintData.strokeColorSelected = current_solid_brush;
                PocketPaintApplication.GetInstance().RectangleSelectionControl.strokeOfRectangleToDraw = current_solid_brush;
                PocketPaintApplication.GetInstance().EllipseSelectionControl.strokeOfEllipseToDraw = current_solid_brush;
                PocketPaintApplication.GetInstance().ImportImageSelectionControl.changeStrokeOfDrawingShape(current_color);
            }
            else
            {
                var current_solid_brush = new SolidColorBrush(current_color);
                PocketPaintApplication.GetInstance().PaintData.colorSelected = current_solid_brush;

                PocketPaintApplication.GetInstance().RectangleSelectionControl.fillOfRectangleToDraw = current_solid_brush;
                PocketPaintApplication.GetInstance().EllipseSelectionControl.fillOfEllipseToDraw = current_solid_brush;
            }

            // TODO: If selected color is transparence then select the eraser-tool.
            if (PocketPaintApplication.GetInstance().isBrushTool)
            {
                if (current_color.A == 0)
                {
                    PocketPaintApplication.GetInstance().isBrushEraser = true;
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Eraser);
                    PocketPaintApplication.GetInstance().isToolPickerUsed = false;
                }
                else
                {
                    PocketPaintApplication.GetInstance().isBrushEraser = false;
                    if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Eraser)
                    {
                        PocketPaintApplication.GetInstance().SwitchTool(ToolType.Brush);
                    }
                }
            }
            if(PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Cursor)
            {
                PocketPaintApplication.GetInstance().cursorControl.setCursorColor(current_color);
                PocketPaintApplication.GetInstance().cursorControl.setDrawingPointColor(current_color);
            }
            
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
            colorPicker.Color = new_color;
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

        private void setColorPickerLayout()
        {
            var height = PocketPaintApplication.GetInstance().size_height_multiplication;
            var width = PocketPaintApplication.GetInstance().size_width_multiplication;
            
            foreach (Object obj in GrdColorButtons.Children.Concat(GrdColorSlider.Children))
            {
                if (obj.GetType() == typeof(Button))
                {
                    Button currentButton = ((Button)obj);
                    currentButton.Height *= height;
                    currentButton.Width *= width;

                    currentButton.Margin = new Thickness(
                                            currentButton.Margin.Left * width,
                                            currentButton.Margin.Top * height,
                                            currentButton.Margin.Right * width,
                                            currentButton.Margin.Bottom * height);

                    currentButton.FontSize *= height;

                    if (currentButton.Content != null && currentButton.Content.GetType() == typeof(Rectangle))
                    {
                        Rectangle currentRectangle = ((Rectangle)(currentButton.Content));
                        currentRectangle.Height *= height;
                        currentRectangle.Width *= width;

                        currentRectangle.Margin = new Thickness(
                                                currentRectangle.Margin.Left * width,
                                                currentRectangle.Margin.Top * height,
                                                currentRectangle.Margin.Right * width,
                                                currentRectangle.Margin.Bottom * height);
                    }
                }
                else if (obj.GetType() == typeof(Slider))
                {
                    Slider currentSlider = ((Slider)obj);
                    currentSlider.Width *= width;

                    currentSlider.Margin = new Thickness(
                                            currentSlider.Margin.Left * width,
                                            currentSlider.Margin.Top * height,
                                            currentSlider.Margin.Right * width,
                                            currentSlider.Margin.Bottom * height);
                }
                else if (obj.GetType() == typeof(TextBox))
                {
                    TextBox currentTextBox = ((TextBox)obj);
                    currentTextBox.Height *= height;
                    currentTextBox.Width *= width;

                    currentTextBox.Margin = new Thickness(
                                            currentTextBox.Margin.Left * width,
                                            currentTextBox.Margin.Top * height,
                                            currentTextBox.Margin.Right * width,
                                            currentTextBox.Margin.Bottom * height);
                    currentTextBox.FontSize *= height;
                }
            }
            GrdButtonSelectedColor.Height *= height;
            GrdButtonSelectedColor.Width *= width;
            GrdButtonSelectedColorSlider.Height *= height;
            GrdButtonSelectedColorSlider.Width *= width;

            ImgPredColorTransp.Height *= height;
            ImgPredColorTransp.Width *= width;

            ImgTransparence.Height *= height;
            ImgTransparence.Width *= width;
            ImgTransparence.Margin = new Thickness(
                                        ImgTransparence.Margin.Left * width,
                                        ImgTransparence.Margin.Top * height,
                                        ImgTransparence.Margin.Right * width,
                                        ImgTransparence.Margin.Bottom * height);

            ImgTransparenceSlider.Height *= height;
            ImgTransparenceSlider.Width *= width;
            ImgTransparenceSlider.Margin = new Thickness(
                                        ImgTransparenceSlider.Margin.Left * width,
                                        ImgTransparenceSlider.Margin.Top * height,
                                        ImgTransparenceSlider.Margin.Right * width,
                                        ImgTransparenceSlider.Margin.Bottom * height);

            RecSelectedColor.Height *= height;
            RecSelectedColor.Width *= width;
            RecSelectedColor.Margin = new Thickness(
                                        RecSelectedColor.Margin.Left * width,
                                        RecSelectedColor.Margin.Top * height,
                                        RecSelectedColor.Margin.Right * width,
                                        RecSelectedColor.Margin.Bottom * height);

            RecSelectedColorSlider.Height *= height;
            RecSelectedColorSlider.Width *= width;
            RecSelectedColorSlider.Margin = new Thickness(
                                        RecSelectedColorSlider.Margin.Left * width,
                                        RecSelectedColorSlider.Margin.Top * height,
                                        RecSelectedColorSlider.Margin.Right * width,
                                        RecSelectedColorSlider.Margin.Bottom * height);

            TbFertig.Height *= height;
            TbFertig.Width *= width;
            TbFertig.Margin = new Thickness(
                                        TbFertig.Margin.Left * width,
                                        TbFertig.Margin.Top * height,
                                        TbFertig.Margin.Right * width,
                                        TbFertig.Margin.Bottom * height);
            TbFertig.FontSize *= height;

            TbFertigSlider.Height *= height;
            TbFertigSlider.Width *= width;
            TbFertigSlider.Margin = new Thickness(
                                        TbFertigSlider.Margin.Left * width,
                                        TbFertigSlider.Margin.Top * height,
                                        TbFertigSlider.Margin.Right * width,
                                        TbFertigSlider.Margin.Bottom * height);
            TbFertigSlider.FontSize *= height;
        }
    }
}
