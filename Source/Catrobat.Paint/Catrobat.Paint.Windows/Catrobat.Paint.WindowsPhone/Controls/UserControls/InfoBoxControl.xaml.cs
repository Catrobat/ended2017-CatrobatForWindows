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
    public sealed partial class InfoBoxControl : UserControl
    {
        public InfoBoxControl()
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

        private void btnNewDrawingSpace_Click(object sender, RoutedEventArgs e)
        {
            if (PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count > 0)
            {
                PocketPaintApplication.GetInstance().PaintingAreaView.messageBoxNewDrawingSpace_Click("Neues Bild", false);
            }
            else
            {
                PocketPaintApplication.GetInstance().PaintingAreaView.resetTools();
            }

            //PocketPaintApplication.GetInstance().SwitchTool(ToolType.Brush);
            //PocketPaintApplication.GetInstance().PaintingAreaView.changeBackgroundColorAndOpacityOfPaintingAreaCanvas(Colors.Transparent, 1.0);
            //PocketPaintApplication.GetInstance().PaintingAreaView.resetControls(Visibility.Collapsed);
        }

        private void btnNewPictureFromCamera_Click(object sender, RoutedEventArgs e)
        {
            //if (PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count > 0)
            //{
            //    PocketPaintApplication.GetInstance().PaintingAreaView.messageBoxNewDrawingSpace_Click("Neues Bild von Kamera",false);
            //}
            PocketPaintApplication.GetInstance().InfoBoxControl.Visibility = Visibility.Collapsed;
            PocketPaintApplication.GetInstance().PaintingAreaView.BottomAppBar.Visibility = Visibility.Collapsed;
            PocketPaintApplication.GetInstance().AppbarTop.Visibility = Visibility.Collapsed;
            PocketPaintApplication.GetInstance().PhoneControl.Visibility = Visibility.Visible;
            //PocketPaintApplication.GetInstance().PhoneControl.InitCameraBtn_Click(sender, e);
            // PocketPaintApplication.GetInstance().PhoneControl.initAndStartPhotoManager(0);
            //PocketPaintApplication.GetInstance().PhoneControl.InitCameraBtn_Click(sender, e);
            //PocketPaintApplication.GetInstance().PhoneControl.StartPreviewBtn_Click(sender, e);
        }

        private void InitCamera_Click(object sender, RoutedEventArgs e)
        {
            //captureManager = new MediaCapture();
            //await captureManager.InitializeAsync();
        }

        // TODO: Implement the following method
        /*async private void StartCapturePreview_Click(object sender, RoutedEventArgs e)
        {
            //capturePreview.Source = captureManager;
            //await captureManager.StartPreviewAsync();
        }*/

        // TODO: Implement the following method
        /*async private void StopCapturePreview_Click(object sender, RoutedEventArgs e)
        {
            // await captureManager.StopPreviewAsync();
        }*/

        // TODO: Implement the following method
        /*async private void CapturePhoto_Click(object sender, RoutedEventArgs e)
        {
            //ImageEncodingProperties imgFormat = ImageEncodingProperties.CreateJpeg();

            //// create storage file in local app storage
            //StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
            //    "TestPhoto.jpg",
            //    CreationCollisionOption.GenerateUniqueName);

            //// take photo
            //await captureManager.CapturePhotoToStorageFileAsync(imgFormat, file);

            //// Get photo as a BitmapImage
            //BitmapImage bmpImage = new BitmapImage(new Uri(file.Path));

            //// imagePreivew is a <Image> object defined in XAML
            //PocketPaintApplication.GetInstance().GridImportImageSelectionControl.Visibility = Visibility.Visible;
            ////PocketPaintApplication.GetInstance().ImportImageSelectionControl;
            ////imagePreivew.Source = bmpImage;
        }*/
    }
}
