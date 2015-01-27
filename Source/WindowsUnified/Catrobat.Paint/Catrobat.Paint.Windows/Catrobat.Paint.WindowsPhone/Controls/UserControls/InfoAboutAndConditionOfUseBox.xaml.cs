using Catrobat.Paint.WindowsPhone.Tool;
using System;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;


// Die Elementvorlage "Benutzersteuerelement" ist unter http://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    public sealed partial class InfoAboutAndConditionOfUseBox : UserControl
    {
        public InfoAboutAndConditionOfUseBox()
        {
            this.InitializeComponent();
        }

        private void setCursorControlLayout()
        {            
            double heightMultiplicator = PocketPaintApplication.GetInstance().size_width_multiplication;
            double widthMultiplicator = PocketPaintApplication.GetInstance().size_width_multiplication;

            GridMain.Width *= widthMultiplicator;
            GridMain.Height *= heightMultiplicator;

            foreach (Object obj in GridMain.Children)
            {
                if (obj.GetType() == typeof(Button))
                {
                    Button currentButton = (Button)obj;
                    currentButton.Height *= heightMultiplicator;
                    currentButton.Width *= widthMultiplicator;
                    Thickness currentButtonThickness = currentButton.BorderThickness;
                    currentButtonThickness.Bottom *= heightMultiplicator;
                    currentButtonThickness.Left *= heightMultiplicator;
                    currentButtonThickness.Right *= heightMultiplicator;
                    currentButtonThickness.Top *= heightMultiplicator;
                    currentButton.BorderThickness = currentButtonThickness;
                }
                else if (obj.GetType() == typeof(TextBox))
                {
                    TextBox currentTextbox = (TextBox)obj;
                    currentTextbox.Height *= heightMultiplicator;
                    currentTextbox.Width *= widthMultiplicator;
                }
            }
        }

        private void btnConditionOfUse_Click(object sender, RoutedEventArgs e)
        {
            PocketPaintApplication.GetInstance().InfoxBasicBox.Visibility = Visibility.Visible;
            PocketPaintApplication.GetInstance().InfoxBasicBox.initAndLoadControl(0);
            PocketPaintApplication.GetInstance().InfoAboutAndConditionOfUseBox.Visibility = Visibility.Collapsed;
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            PocketPaintApplication.GetInstance().InfoxBasicBox.Visibility = Visibility.Visible;
            PocketPaintApplication.GetInstance().InfoxBasicBox.initAndLoadControl(1);
            PocketPaintApplication.GetInstance().InfoAboutAndConditionOfUseBox.Visibility = Visibility.Collapsed;
        }
    }
}
