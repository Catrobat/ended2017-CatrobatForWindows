using System;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

// Die Elementvorlage "Benutzersteuerelement" ist unter http://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    public sealed partial class InfoBasicBoxControl : UserControl
    {
        private const int CONDITION_OF_USE = 0;
        private const int ABOUT = 1;
        public InfoBasicBoxControl()
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
                if (obj.GetType() == typeof(Rectangle))
                {
                    Rectangle rectangle = (Rectangle)obj;
                    rectangle.Height *= heightMultiplicator;
                    rectangle.Width *= widthMultiplicator;
                }
                else if (obj.GetType() == typeof(TextBox))
                {
                    TextBox currentTextbox = (TextBox)obj;
                    currentTextbox.FontSize *= heightMultiplicator;
                    currentTextbox.Height *= heightMultiplicator;
                    currentTextbox.Width *= widthMultiplicator;
                }
                else if(obj.GetType() == typeof(Image))
                {
                    Image image = (Image)obj;
                    image.Height *= heightMultiplicator;
                    image.Width *= widthMultiplicator;
                }
                else if(obj.GetType() == typeof(Grid))
                {
                    Grid grid = (Grid)obj;
                    grid.Height *= heightMultiplicator;
                    grid.Width *= widthMultiplicator;
                    grid.Margin = new Thickness(
                        grid.Margin.Left * widthMultiplicator,
                        grid.Margin.Top * heightMultiplicator,
                        grid.Margin.Right * widthMultiplicator,
                        grid.Margin.Bottom * heightMultiplicator);
                    foreach (Object gridObj in grid.Children)
                    {
                        if (gridObj.GetType() == typeof(TextBox))
                        {
                            TextBox currentTextbox = (TextBox)gridObj;
                            currentTextbox.FontSize *= heightMultiplicator;
                            currentTextbox.Height *= heightMultiplicator;
                            currentTextbox.Width *= widthMultiplicator;
                        }
                        else if (gridObj.GetType() == typeof(Image))
                        {
                            Image image = (Image)gridObj;
                            image.Height *= heightMultiplicator;
                            image.Width *= widthMultiplicator;
                        }
                        else if (gridObj.GetType() == typeof(HyperlinkButton))
                        {
                            HyperlinkButton hyperlinkButton = (HyperlinkButton)gridObj;
                            hyperlinkButton.Height *= heightMultiplicator;
                            hyperlinkButton.Width *= widthMultiplicator;
                            hyperlinkButton.Margin = new Thickness(
                                hyperlinkButton.Margin.Left * widthMultiplicator,
                                hyperlinkButton.Margin.Top * heightMultiplicator,
                                hyperlinkButton.Margin.Right * widthMultiplicator,
                                hyperlinkButton.Margin.Bottom * heightMultiplicator);

                            object hyperlinkButtonContent = hyperlinkButton.Content;
                            if (hyperlinkButtonContent != null && hyperlinkButtonContent.GetType() == typeof(Grid))
                            {
                                Grid grid2 = (Grid)hyperlinkButtonContent;
                                foreach (Object gridObj2 in grid2.Children)
                                {
                                    if (gridObj2.GetType() == typeof(TextBox))
                                    {
                                        TextBox textbox = (TextBox)gridObj2;
                                        textbox.FontSize *= heightMultiplicator;
                                        textbox.Margin = new Thickness(
                                                    textbox.Margin.Left * widthMultiplicator,
                                                    textbox.Margin.Top * heightMultiplicator,
                                                    textbox.Margin.Right * widthMultiplicator,
                                                    textbox.Margin.Bottom * heightMultiplicator);
                                    }
                                }
                            }
                        }
                        else if (obj.GetType() == typeof(Button))
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
                    }
                }
            }
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

        private string _createUnderlineForText(uint numberOfUnderlines)
        {
            string underlineString = "";
            for (int i = 0; i <= numberOfUnderlines; i++)
            {
                underlineString += "_";
            }
            return underlineString;
        }

        public void initAndLoadControl(int opt)
        {
            if(CONDITION_OF_USE == opt)
            {
                tbTitle.Text = "Nutzungsbedinungen";
                tbText.Text = "Um Pocket Paint und andere Software, die vom Catrobat Projekt angeboten werden, nutzen " +
                               "zu dürfen, müssen Sie / musst Du mit unseren Nutzungsbedingungen einverstanden sein und sie während Ihrer / " +
                               "Deiner Benutzung genau einhalten. Die exakte englische Formulierung unserer Nutzungsbedingungen steht unter den " +
                               "folgenden Terms of Use and Service Link zur Verfügung.";

                tbLink1.Text = "Nutzungsbedingungen und Service";
                tbLink1Underline.Text = _createUnderlineForText((uint)tbLink1.Text.Length + 2);
                HyperLink1.NavigateUri = new Uri("http://developer.catrobat.org/terms_of_use_and_service");
            }
            else if(ABOUT == opt)
            {
                tbTitle.Text = "Über Pocket Paint ...";
                tbText.Text = "Pocket Paint ist ein Bildeditor und Teil des Catrobat Projekts." +
                               "Catrobat ist eine visuelle Programmier-sprache und eine Sammlung von Kreativitätswerkzeugen für" +
                               "Smartphones, Tablets und mobile Browser." +
                               "Der Source Code von Pocket Paint steht im Wesentlichen unter der GNU Affero General Public" +
                               "Licence, v3." +
                               "Für genauere Details bitte dem Link folgen.";

                tbLink1.Text = "Pocket Paint Quellcode-Lizenz";
                tbLink1Underline.Text = _createUnderlineForText((uint)tbLink1.Text.Length-2);

                HyperLink1.NavigateUri = new Uri("http://developer.catrobat.org/terms_of_use_and_service");
            }
            tbLink2.Text = "Über Catrobat";
            tbLink2Underline.Text = _createUnderlineForText((uint)tbLink2.Text.Length);
            HyperLink2.NavigateUri = new Uri("http://www.catrobat.org/");
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            PocketPaintApplication.GetInstance().InfoxBasicBoxControl.Visibility = Visibility.Collapsed;
            PocketPaintApplication.GetInstance().InfoAboutAndConditionOfUseBox.Visibility = Visibility.Collapsed;
            PocketPaintApplication.GetInstance().PaintingAreaView.changeVisibilityOfAppBars(Visibility.Visible);
            PocketPaintApplication.GetInstance().PaintingAreaView.setActivityOfToolsControls(true);
        }
    }
}
