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
using Windows.UI.Xaml.Shapes;


// Die Elementvorlage "Benutzersteuerelement" ist unter http://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    public sealed partial class InfoAboutAndConditionOfUseBox : UserControl
    {
        public InfoAboutAndConditionOfUseBox()
        {
            this.InitializeComponent();
            setLayout();
        }

        private void setLayout()
        {
            double heightMultiplicator = PocketPaintApplication.GetInstance().size_width_multiplication;
            double widthMultiplicator = PocketPaintApplication.GetInstance().size_width_multiplication;
            GridMain.Height *= heightMultiplicator;
            GridMain.Width *= widthMultiplicator;
            this.Height *= heightMultiplicator;
            this.Width *= widthMultiplicator;
            foreach (Object obj in GridMain.Children)
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
                    if (buttonContent != null && buttonContent.GetType() == typeof(TextBox))
                    {
                        TextBox contentTextbox = (TextBox)buttonContent;
                        contentTextbox.FontSize *= heightMultiplicator;
                    }
                }
                else if (obj.GetType() == typeof(Rectangle))
                {
                    Rectangle rectangle = (Rectangle)obj;
                    rectangle.Height *= heightMultiplicator;
                    rectangle.Width *= widthMultiplicator;
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
            PocketPaintApplication.GetInstance().InfoxBasicBoxControl.Visibility = Visibility.Visible;
            PocketPaintApplication.GetInstance().InfoxBasicBoxControl.initAndLoadControl(0);
            PocketPaintApplication.GetInstance().InfoAboutAndConditionOfUseBox.Visibility = Visibility.Collapsed;
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            PocketPaintApplication.GetInstance().InfoxBasicBoxControl.Visibility = Visibility.Visible;
            PocketPaintApplication.GetInstance().InfoxBasicBoxControl.initAndLoadControl(1);
            PocketPaintApplication.GetInstance().InfoAboutAndConditionOfUseBox.Visibility = Visibility.Collapsed;
        }
    }
}
