using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Catrobat.Paint.WindowsPhone.Tool;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkID=390556 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class InputScopeControl
    {
        double heightMultiplicator = PocketPaintApplication.GetInstance().size_width_multiplication;
        double widthMultiplicator = PocketPaintApplication.GetInstance().size_width_multiplication;
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
            GrdLayoutRoot.Width *= widthMultiplicator;
            GrdLayoutRoot.Height *= heightMultiplicator;
            GrdThicknessKeyboard.Width *= widthMultiplicator;
            GrdThicknessKeyboard.Height *= heightMultiplicator;

            foreach (Object obj in GrdThicknessKeyboard.Children)
            {
                if (obj.GetType() == typeof(Button))
                {
                    Button button = ((Button)obj);
                    button.Height *= heightMultiplicator;
                    button.Width *= widthMultiplicator;

                    button.Margin = new Thickness(
                                            button.Margin.Left * widthMultiplicator,
                                            button.Margin.Top * heightMultiplicator,
                                            button.Margin.Right * widthMultiplicator,
                                            button.Margin.Bottom * heightMultiplicator);

                    button.FontSize *= heightMultiplicator;

                    var buttonContent = ((Button)obj).Content;
                    if (buttonContent != null && buttonContent.GetType() == typeof(Image))
                    {
                        Image contentImage = (Image)buttonContent;
                        contentImage.Height *= heightMultiplicator;
                        contentImage.Width *= widthMultiplicator;

                        contentImage.Margin = new Thickness(
                                                contentImage.Margin.Left * widthMultiplicator,
                                                contentImage.Margin.Top * heightMultiplicator,
                                                contentImage.Margin.Right * widthMultiplicator,
                                                contentImage.Margin.Bottom * heightMultiplicator);
                    }
                }
                else if (obj.GetType() == typeof(Slider))
                {
                    Slider slider = (Slider)obj;
                    slider.Height *= heightMultiplicator;
                    slider.Width *= widthMultiplicator;

                    slider.Margin = new Thickness(
                                            slider.Margin.Left * widthMultiplicator,
                                            slider.Margin.Top * heightMultiplicator,
                                            slider.Margin.Right * widthMultiplicator,
                                            slider.Margin.Bottom * heightMultiplicator);
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
                    
                    if (PocketPaintApplication.GetInstance().ToolCurrent is RectangleShapeBaseTool)
                    {
                        Debug.Assert(
                            ((RectangleShapeBaseTool) PocketPaintApplication.GetInstance().ToolCurrent)
                                .RectangleShapeBase != null);
                        ((RectangleShapeBaseTool) PocketPaintApplication.GetInstance().ToolCurrent)
                            .RectangleShapeBase.SetHeightOfControl(currentValue);
                    }
                    else
                    {
                        if (PocketPaintApplication.GetInstance().ImportImageSelectionControl != null)
                            PocketPaintApplication.GetInstance().ImportImageSelectionControl.changeHeightOfDrawingSelection(currentValue, false);
                    }
                }
                else if (currentButton.Name.Equals("btnWidthValue"))
                {
                    if (PocketPaintApplication.GetInstance().ToolCurrent is RectangleShapeBaseTool)
                    {
                        Debug.Assert(
                                     ((RectangleShapeBaseTool) PocketPaintApplication.GetInstance().ToolCurrent)
                                     .RectangleShapeBase != null);
                        ((RectangleShapeBaseTool)PocketPaintApplication.GetInstance().ToolCurrent)
                            .RectangleShapeBase.SetWidthOfControl(currentValue);
                    }
                    else
                    {
                        if (PocketPaintApplication.GetInstance().ImportImageSelectionControl != null)
                            PocketPaintApplication.GetInstance().ImportImageSelectionControl.changeWidthOfDrawingSelection(currentValue, false);
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
