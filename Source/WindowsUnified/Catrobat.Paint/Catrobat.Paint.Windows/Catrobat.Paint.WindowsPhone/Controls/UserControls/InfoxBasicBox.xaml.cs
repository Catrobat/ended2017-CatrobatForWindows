using System;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// Die Elementvorlage "Benutzersteuerelement" ist unter http://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    public sealed partial class InfoBasicBox : UserControl
    {
        private const int CONDITION_OF_USE = 0;
        private const int ABOUT = 1;
        public InfoBasicBox()
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

        public void initAndLoadControl(int opt)
        {
            if(CONDITION_OF_USE == opt)
            {
                tbTitle.Text = "Nutzungsbedinungen";
                tbText.Text = "Um Pocket Paint und andere Software, die vom Catrobat Projekt angeboten werden, nutzen" +
                               "zu dürfen, müssen Sie / musst Du mit unseren Nutzungsbedingungen einverstanden seind und sie während Ihrer / " +
                               "Deiner Benutzung genau einhalten. Die exakte englische Formulierung unserer Nutzungsbedingungen steht unter dem " +
                               "folgenden Terms of Use and Service Link zur Verfügung.";
                //rtbkLinks.

            }
            else if(ABOUT == opt)
            {
                tbTitle.Text = "Über Pocket Paint ...";
                tbText.Text = "Pocket Paint ist ein Bildeditor und Teil des Catrobat Projekts." +
                               "Catrobat ist eine visuelle Programmier-sprache und eine Sammlung von Krativitätswerkzeugen für" +
                               "Smartphones, Tablets und mobile Browser." +
                               "Der Source Code von Pocket Paint sthet im Wesentlichen unter der GNU Affero General Public" +
                               "Licence, v3." +
                               "Für genauere Details bitte dem Link folgen.";
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            PocketPaintApplication.GetInstance().InfoxBasicBox.Visibility = Visibility.Collapsed;
            PocketPaintApplication.GetInstance().InfoAboutAndConditionOfUseBox.Visibility = Visibility.Collapsed;
            PocketPaintApplication.GetInstance().PaintingAreaView.changeVisibilityOfAppBars(Visibility.Visible);
        }
    }
}
