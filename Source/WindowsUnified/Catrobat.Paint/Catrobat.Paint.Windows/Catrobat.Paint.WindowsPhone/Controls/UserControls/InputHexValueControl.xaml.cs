using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkID=390556 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class InputHexValueControl
    {
        public InputHexValueControl()
        {
            this.InitializeComponent();
        }

        private void ButtonNumbers_Click(object sender, RoutedEventArgs e)
        {
            if(tbInputValue.Text.Length <= 7)
            {
                Button button = sender as Button;
                if (button != null)
                {
                    string getTextOfClickedButton = button.Name.Substring(8);
                        tbInputValue.Text += getTextOfClickedButton;
                }
            }
            
            
            int lenOfInputValue = tbInputValue.Text.Length;
            if (lenOfInputValue == 6)
            {
                Color color = new Color();
                string redValueString = tbInputValue.Text.ToString().Substring(lenOfInputValue - 6, 2);
                string greenValueString = tbInputValue.Text.ToString().Substring(lenOfInputValue - 4, 2);
                string blueValueString = tbInputValue.Text.ToString().Substring(lenOfInputValue - 2, 2);
                color.R = Convert.ToByte(redValueString, 16);
                color.G = Convert.ToByte(greenValueString, 16);
                color.B = Convert.ToByte(blueValueString, 16);
                color.A = 0xFF;

                PocketPaintApplication.GetInstance().ViewColorPicker.changeColorOfCoding4FunColorPicker(color.R, color.G, color.B, color.A);
                PocketPaintApplication.GetInstance().ViewColorPicker.changeColorOfBtnSelectedColor(color);
                PocketPaintApplication.GetInstance().ViewColorPicker.changeValuesOfColourSliders(color.R, color.G, color.B, color.A);
                PocketPaintApplication.GetInstance().ViewColorPicker.changeValuesOfColourTextboxes(color.R, color.G, color.B, color.A);
            }
        }

        private void btnDeleteDigit_Click(object sender, RoutedEventArgs e)
        {
            tbInputValue.Text = tbInputValue.Text.Length > 0 ? tbInputValue.Text.Substring(0, tbInputValue.Text.Length - 1) : tbInputValue.Text;
        }

        private void tbInputValue_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        public void setInputHexValueTextbox(string text)
        {
            tbInputValue.Text = text;
        }
    }
}
