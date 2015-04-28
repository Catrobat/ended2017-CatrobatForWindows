using System;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// Die Elementvorlage "Benutzersteuerelement" ist unter http://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    public sealed partial class InfoBoxActionControl : UserControl
    {
        public InfoBoxActionControl()
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

        private void btnOpenGallery_Click(object sender, RoutedEventArgs e)
        {
            PocketPaintApplication.GetInstance().InfoBoxActionControl.Visibility = Visibility.Collapsed;
            if (!PocketPaintApplication.GetInstance().isLoadPictureClicked)
            {
                PocketPaintApplication.GetInstance().GridImportImageSelectionControl.Visibility = Visibility.Visible;
            }
            PocketPaintApplication.GetInstance().PaintingAreaView.changeVisibilityOfAppBars(Visibility.Visible);
            PocketPaintApplication.GetInstance().PaintingAreaView.changeBackgroundColorAndOpacityOfPaintingAreaCanvas(Colors.Transparent, 1.0);
            PocketPaintApplication.GetInstance().PaintingAreaView.PickAFileButton_Click(sender, e);
            PocketPaintApplication.GetInstance().PaintingAreaView.setActivityOfToolsControls(true);
        }

        private void resetTools()
        {
            if (PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count > 0) 
            {
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();
            }

            PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform = new TransformGroup();
            PocketPaintApplication.GetInstance().GridWorkingSpace.RenderTransform = new TransformGroup();
            PocketPaintApplication.GetInstance().PaintingAreaView.disableToolbarsAndPaintingArea(false);
        }

        private async void messageBoxNewDrawingSpace_Click()
        { 
            // TODO: use dynamic text instead of static text

            var messageDialog = new MessageDialog("Änderungen speichern?", "Neues Bild");

            messageDialog.Commands.Add(new UICommand(
                "Speichern",
                new UICommandInvokedHandler(saveChanges)));
            messageDialog.Commands.Add(new UICommand(
                "Verwerfen",
                new UICommandInvokedHandler(deleteChanges)));

            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;

            await messageDialog.ShowAsync();
        }

        private void saveChanges(IUICommand command)
        {
            // TODO save current Image
            
            resetTools();
        }

        private void deleteChanges(IUICommand command)
        {
            resetTools();
        }

        private void tbGalerie_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnNewPictureFromCamera_Click(object sender, RoutedEventArgs e)
        {
            PocketPaintApplication.GetInstance().InfoBoxActionControl.Visibility = Visibility.Collapsed;
            PocketPaintApplication.GetInstance().PaintingAreaView.BottomAppBar.Visibility = Visibility.Collapsed;
            PocketPaintApplication.GetInstance().AppbarTop.Visibility = Visibility.Collapsed;
            // TODO: Before you activate this line, implement the logic if the app is suspended.
            PocketPaintApplication.GetInstance().PhoneControl.Visibility = Visibility.Visible;
            PocketPaintApplication.GetInstance().PhoneControl.initPhotoControl();
            PocketPaintApplication.GetInstance().PaintingAreaView.setActivityOfToolsControls(true);
        }
    }
}
