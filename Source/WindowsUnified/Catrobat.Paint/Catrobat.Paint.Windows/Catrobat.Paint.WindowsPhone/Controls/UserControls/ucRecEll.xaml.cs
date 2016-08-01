using Catrobat.Paint.WindowsPhone.Tool;
using Catrobat.Paint.WindowsPhone.View;
using System;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
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

            tbStrokeThicknessValue.Text = PocketPaintApplication.GetInstance().PaintData.strokeThickness.ToString();
            sldStrokeThickness.Value = PocketPaintApplication.GetInstance().PaintData.strokeThickness;

            _last_valid_height = Convert.ToInt32(btnHeightValue.Content.ToString());
            _last_valid_width = Convert.ToInt32(btnWidthValue.Content.ToString());

            PocketPaintApplication.GetInstance().PaintData.strokeColorChanged += ColorStrokeChanged;
            PocketPaintApplication.GetInstance().PaintData.colorChanged += ColorFillChanged;
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

            btnSelectedBorderColor.Background = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
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
                
                if(PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.ImportPng)
                {
                    PocketPaintApplication.GetInstance().PaintingAreaView.visibilityGridEllRecControl = Visibility.Collapsed;
                    PocketPaintApplication.GetInstance().InfoBoxActionControl.Visibility = Visibility.Visible;
                    PocketPaintApplication.GetInstance().PaintingAreaView.changeVisibilityOfAppBars(Visibility.Collapsed);
                }
                else
                {
                    rootFrame.Navigate(typeof(ViewColorPicker));
                }
            }
        }

        public void ColorFillChanged(SolidColorBrush color)
        {
            SolidColorBrush selected_color = new SolidColorBrush();
            selected_color.Color = color != null && color.Color != Colors.Transparent ? color.Color : Colors.Transparent;
            rectFillColor.Fill = selected_color;
        }

        private void ColorStrokeChanged(SolidColorBrush color)
        {
            SolidColorBrush selected_color = new SolidColorBrush();
            selected_color.Color = color.Color != Colors.Transparent ? color.Color : Colors.Transparent;
            rectBorderColor.Fill = selected_color;
        }


        public double getHeight()
        {
            int return_value = 0;
            return return_value = btnHeightValue.Content.ToString() != string.Empty ? Convert.ToInt32(btnHeightValue.Content.ToString()) : _last_valid_height;
        }

        public double getWidth()
        {
            int return_value = 0;
            return return_value = btnWidthValue.Content.ToString() != string.Empty ? Convert.ToInt32(btnWidthValue.Content.ToString()) : _last_valid_width;
        }

        private void sldSlidersChanged_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            int strokeThickness = (int)sldStrokeThickness.Value;
            tbStrokeThicknessValue.Text = strokeThickness.ToString();
            PocketPaintApplication.GetInstance().PaintData.strokeThickness = strokeThickness;

            if (PocketPaintApplication.GetInstance().BarRecEllShape != null)
            {
                if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == Tool.ToolType.Rect)
                {
                    PocketPaintApplication.GetInstance().RectangleSelectionControl.StrokeThicknessOfRectangleToDraw = strokeThickness;
                }
                else if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == Tool.ToolType.Ellipse)
                {
                    PocketPaintApplication.GetInstance().EllipseSelectionControl.StrokeThicknessOfEllipseToDraw = strokeThickness;
                }
                else
                {
                    PocketPaintApplication.GetInstance().ImportImageSelectionControl.setStrokeThicknessOfDrawingShape = strokeThickness;
                }
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

        public double setBtnHeightValue
        {
            get
            {
                return Convert.ToInt32(btnHeightValue.Content.ToString());
            }
            set
            {
                btnHeightValue.Content = Math.Round(value).ToString();
            }
        }

        public double setBtnWidthValue
        {
            get
            {
                return Convert.ToInt32(btnWidthValue.Content.ToString());
            }
            set
            {
                btnWidthValue.Content = Math.Round(value).ToString();
            }
        }

        private void BtnRound_Click(object sender, RoutedEventArgs e)
        {
            setRectangleEdgeType(Colors.Gray, Colors.Gray, Colors.White);
            PocketPaintApplication.GetInstance().PaintData.penLineJoinSelected = PenLineJoin.Round;
            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == Tool.ToolType.Rect)
            {
                PocketPaintApplication.GetInstance().RectangleSelectionControl.StrokeLineJoinOfRectangleToDraw = PenLineJoin.Round; 
            }
            else
            {
                PocketPaintApplication.GetInstance().ImportImageSelectionControl.strokeLineJoinOfRectangleToDraw = PenLineJoin.Round;
            }
        }

        private void setRectangleEdgeType(Color bevel, Color miter, Color round)
        {
            BtnBevel.BorderBrush = new SolidColorBrush(bevel);
            BtnMiter.BorderBrush = new SolidColorBrush(miter);
            BtnRound.BorderBrush = new SolidColorBrush(round);
        }

        private void BtnMiter_Click(object sender, RoutedEventArgs e)
        {
            setRectangleEdgeType(Colors.Gray, Colors.White, Colors.Gray);
            PocketPaintApplication.GetInstance().PaintData.penLineJoinSelected = PenLineJoin.Miter;
            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == Tool.ToolType.Rect)
            {
                PocketPaintApplication.GetInstance().RectangleSelectionControl.StrokeLineJoinOfRectangleToDraw = PenLineJoin.Miter;
            }
            else
            {
                PocketPaintApplication.GetInstance().ImportImageSelectionControl.strokeLineJoinOfRectangleToDraw = PenLineJoin.Miter;
            }
        }

        private void BtnBevel_Click(object sender, RoutedEventArgs e)
        {
            setRectangleEdgeType(Colors.White, Colors.Gray, Colors.Gray);
            PocketPaintApplication.GetInstance().PaintData.penLineJoinSelected = PenLineJoin.Bevel;
            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == Tool.ToolType.Rect)
            {
                PocketPaintApplication.GetInstance().RectangleSelectionControl.StrokeLineJoinOfRectangleToDraw = PenLineJoin.Bevel;
            }
            else
            {
                PocketPaintApplication.GetInstance().ImportImageSelectionControl.strokeLineJoinOfRectangleToDraw = PenLineJoin.Bevel;
            }
        }

        public void setIsEnabledOfEdgeType(bool bevel, bool miter, bool round)
        {
            BtnBevel.IsEnabled = bevel;
            BtnMiter.IsEnabled = miter;
            BtnRound.IsEnabled = round;
        }

        public void setForgroundOfLabelEdgeType(Color value)
        {
            tbEdgeType.Foreground = new SolidColorBrush(value);
        }

        private void btnHeightValue_Click(object sender, RoutedEventArgs e)
        {
            PocketPaintApplication.GetInstance().GridInputScopeControl.Visibility = Visibility.Visible;
            PocketPaintApplication.GetInstance().InputScopeControl.setTextOfInputValue(btnHeightValue.Content.ToString());
            PocketPaintApplication.GetInstance().InputScopeControl.setCurrentButton = btnHeightValue;
        }

        private void btnWidthValue_Click(object sender, RoutedEventArgs e)
        {
            PocketPaintApplication.GetInstance().GridInputScopeControl.Visibility = Visibility.Visible;
            PocketPaintApplication.GetInstance().InputScopeControl.setTextOfInputValue(btnWidthValue.Content.ToString());
            PocketPaintApplication.GetInstance().InputScopeControl.setCurrentButton = btnWidthValue;
        }

        public void updateSldStrokeThickness(int currentWidth, int currentHeight)
        {
            int minValue = currentWidth > currentHeight ? currentHeight : currentWidth;
            int currentStrokeThickness = (int) sldStrokeThickness.Value;

            if (minValue % 2 > 0)
            {
                sldStrokeThickness.Maximum = (minValue - 1) / 2;
            }
            else
            {
                sldStrokeThickness.Maximum = (minValue - 2) / 2;
            }
            
            if ((2 * currentStrokeThickness) >= minValue)
            {
                if (minValue % 2 > 0)
                {
                    currentStrokeThickness = (minValue - 1) / 2;
                }
                else
                {
                    currentStrokeThickness = (minValue - 2) / 2;
                }
            }
        }

        public void setSizeOfRecBar(double height, double width)
        {
            setBtnHeightValue =  height;
            setBtnWidthValue = width;
        }
    }
}
