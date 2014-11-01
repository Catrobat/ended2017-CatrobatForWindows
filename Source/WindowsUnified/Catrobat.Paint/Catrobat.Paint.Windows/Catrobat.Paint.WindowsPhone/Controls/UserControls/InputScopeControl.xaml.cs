using Catrobat.Paint.Phone;
using Catrobat.Paint.Phone.Tool;
using Catrobat.Paint.WindowsPhone.Tool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkID=390556 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class InputScopeControl
    {
        double width_multiplicator = PocketPaintApplication.GetInstance().size_width_multiplication;
        double height_multiplicator = PocketPaintApplication.GetInstance().size_width_multiplication;
        int lastValue;
        int minValue;
        private Button currentButton = null;
        public InputScopeControl()
        {
            this.InitializeComponent();
            PocketPaintApplication.GetInstance().InputScopeControl = this;
            lastValue = 0;
            minValue = 10;
            setLayout();
        }

        private void setLayout()
        {
            GrdLayoutRoot.Width *= width_multiplicator;
            GrdLayoutRoot.Height *= height_multiplicator;
            GrdThicknessKeyboard.Width *= width_multiplicator;
            GrdThicknessKeyboard.Height *= height_multiplicator;

            foreach (Object obj in GrdThicknessKeyboard.Children)
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

        }

        private void ButtonNumbers_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                string getTextOfClickedButton = button.Name.Substring(8);
                if(tbInputValue.Text.Length == 1 && tbInputValue.Text[0] == '0')
                {
                    
                    tbInputValue.Text = getTextOfClickedButton;
                    lastValue = Convert.ToInt32(tbInputValue.Text);
                }
                else if(tbInputValue.Text.Length < 3)
                {
                    tbInputValue.Text += getTextOfClickedButton;
                    lastValue = Convert.ToInt32(tbInputValue.Text);
                }
                
            }

            checkInputRange();
        }

        private void btnDeleteNumbers_Click(object sender, RoutedEventArgs e)
        {
            if(tbInputValue.Text.Length > 0)
            {
                tbInputValue.Text = tbInputValue.Text.Remove(tbInputValue.Text.Length - 1);
                lastValue = tbInputValue.Text.Length != 0 ? Convert.ToInt32(tbInputValue.Text) : lastValue;
            }

            checkInputRange();
        }

        public void setTextOfInputValue(string text)
        {
            tbInputValue.Text = text;
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            PocketPaintApplication.GetInstance().GridInputScopeControl.Visibility = Visibility.Collapsed;
            if (currentButton != null)
            {
                if(tbInputValue.Text.Length == 0)
                {
                    tbInputValue.Text = lastValue.ToString();
                }

                currentButton.Content = tbInputValue.Text;
                double currentValue = Convert.ToDouble(tbInputValue.Text);

                if (currentButton.Name.Equals("btnHeightValue"))
                {
                    if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == Tool.ToolType.Rect)
                    {
                        if (PocketPaintApplication.GetInstance().RectangleSelectionControl != null)
                            PocketPaintApplication.GetInstance().RectangleSelectionControl.changeHeightOfDrawingSelection(currentValue, false);
                    }
                    else
                    {
                        if (PocketPaintApplication.GetInstance().EllipseSelectionControl != null)
                            PocketPaintApplication.GetInstance().EllipseSelectionControl.changeHeightOfDrawingSelection(currentValue, false);
                    }
                }
                else if (currentButton.Name.Equals("btnWidthValue"))
                {
                    if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == Tool.ToolType.Rect)
                    {
                        if (PocketPaintApplication.GetInstance().RectangleSelectionControl != null)
                            PocketPaintApplication.GetInstance().RectangleSelectionControl.changeWidthOfDrawingSelection(currentValue, false);
                    }
                    else
                    {
                        if (PocketPaintApplication.GetInstance().EllipseSelectionControl != null)
                            PocketPaintApplication.GetInstance().EllipseSelectionControl.changeWidthOfDrawingSelection(currentValue, false);
                    }
                }
            }
        }

        public void checkInputRange()
        {
            if (tbInputValue.Text.Length == 0 || Convert.ToInt32(tbInputValue.Text) < minValue)
            {
                btnAccept.IsEnabled = false;
            }
            else
            {
                btnAccept.IsEnabled = true;
            }
        }

        public Button setCurrentButton
        {
            get
            {
                return currentButton;
            }
            set
            {
                currentButton = value;
            }
        }
    }
}
