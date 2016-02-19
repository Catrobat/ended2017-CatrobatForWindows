using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Devices.Enumeration;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using Windows.Storage.Streams;
using Windows.Graphics.Display;
using Catrobat.Paint.WindowsPhone.Command;

// Die Elementvorlage "Benutzersteuerelement" ist unter http://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // It is very to implement the logic if the app is suspended. Otherwise there could be some side effects.
    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public sealed partial class PhotoControl : UserControl
    {
        Windows.Media.Capture.MediaCapture _captureManager = null;
        MediaCaptureInitializationSettings _mediaCaptureSettings = null;
        Dictionary<bool, int> boolToIntValue;
        bool isBackCameraActive;
        bool isPreview = false;
        DeviceInformationCollection _mobileCameras = null;
        public PhotoControl()
        {
            this.InitializeComponent();
            initDeviceInformationCollection();
            isBackCameraActive = true;
            boolToIntValue = new Dictionary<bool,int>(){
                {false, 0},
                {true,  1}};
        }

        async public void initDeviceInformationCollection()
        {
            _mobileCameras = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(
            Windows.Devices.Enumeration.DeviceClass.VideoCapture);
        }
        async public void initPhotoControl()
        {
            setMediaCaptureInitializationSettings();
            // reset all properties if photoManager contains an object.
            if (_captureManager != null)
            {
                _captureManager.Dispose();
                _captureManager = null;
            }
            _captureManager = new MediaCapture();
            await _captureManager.InitializeAsync(getMediaCaptureInitializationSettings());
            cptElementShowPreview.Source = _captureManager;

            await _captureManager.StartPreviewAsync();

            DisplayInformation displayInfo = DisplayInformation.GetForCurrentView();
            displayInfo.OrientationChanged += DisplayInfo_OrientationChanged;

            DisplayInfo_OrientationChanged(displayInfo, null);
        }

        private void DisplayInfo_OrientationChanged(DisplayInformation sender, object args)
        {
            if (_captureManager != null)
            {
                _captureManager.SetPreviewRotation(VideoRotationLookup(sender.CurrentOrientation, !isBackCameraActive));
            }
        }

        private VideoRotation VideoRotationLookup(DisplayOrientations displayOrientation, bool counterclockwise)
        {
            switch (displayOrientation)
            {
                case DisplayOrientations.Landscape:
                    return VideoRotation.None;

                case DisplayOrientations.Portrait:
                    return (counterclockwise) ? VideoRotation.Clockwise270Degrees : VideoRotation.Clockwise90Degrees;

                case DisplayOrientations.LandscapeFlipped:
                    return VideoRotation.Clockwise180Degrees;

                case DisplayOrientations.PortraitFlipped:
                    return (counterclockwise) ? VideoRotation.Clockwise90Degrees :
                    VideoRotation.Clockwise270Degrees;

                default:
                    return VideoRotation.None;
            }
        }

        public MediaCaptureInitializationSettings getMediaCaptureInitializationSettings()
        {
            return _mediaCaptureSettings;
        }

        public void setMediaCaptureInitializationSettings()
        {
            _mediaCaptureSettings = new MediaCaptureInitializationSettings();
            _mediaCaptureSettings.VideoDeviceId = _mobileCameras[boolToIntValue[isBackCameraActive]].Id;
            _mediaCaptureSettings.AudioDeviceId = "";
            _mediaCaptureSettings.StreamingCaptureMode = Windows.Media.Capture.StreamingCaptureMode.AudioAndVideo;
            _mediaCaptureSettings.PhotoCaptureSource = Windows.Media.Capture.PhotoCaptureSource.VideoPreview;
        }


        async private void btnTakePhoto_Click(object sender, RoutedEventArgs e)
        {
            ImageEncodingProperties imgFormat = ImageEncodingProperties.CreateJpeg();

            // TODO: If option replace existing is selected then the replacement
            // doesnt work
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                    "photo.jpg", CreationCollisionOption.GenerateUniqueName);
            await _captureManager.CapturePhotoToStorageFileAsync(imgFormat, file);

            ImageBrush imageBrush = new ImageBrush();

            BitmapImage biCapturedPhoto = new BitmapImage();
            biCapturedPhoto.UriSource = new Uri(file.Path, UriKind.RelativeOrAbsolute);

            imageBrush.ImageSource = biCapturedPhoto;
            if (PocketPaintApplication.GetInstance().isLoadPictureClicked)
            {
                RectangleGeometry myRectangleGeometry = new RectangleGeometry();
                myRectangleGeometry.Rect = new Rect(new Point(0, 0), new Point(Window.Current.Bounds.Height, Window.Current.Bounds.Width));

                RotateTransform rotate = new RotateTransform();
                TransformGroup transformGroup = new TransformGroup();
                Path _path = new Path();

                // HACK: Is needed to align the captured photo.
                if (isBackCameraActive)
                {                    
                    rotate.Angle = 90;
                    transformGroup.Children.Add(rotate);
                    Canvas.SetLeft(_path, PocketPaintApplication.GetInstance().PaintingAreaCanvas.Width);
                }
                else
                {
                    rotate.Angle = -90;
                    Canvas.SetTop(_path, PocketPaintApplication.GetInstance().PaintingAreaCanvas.Height);
                
                    transformGroup.Children.Add(rotate);
                }

                _path.Fill = imageBrush;
                _path.Stroke = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
                _path.RenderTransform = transformGroup;
                _path.Data = myRectangleGeometry;

                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(_path);
                CommandManager.GetInstance().CommitCommand(new LoadPictureCommand(_path));
            }
            else
            {
                PocketPaintApplication.GetInstance().ImportImageSelectionControl.imageSourceOfRectangleToDraw = imageBrush;
                PocketPaintApplication.GetInstance().PaintingAreaView.changeBackgroundColorAndOpacityOfPaintingAreaCanvas(Colors.Black, 0.5);
            }
            closePhoneControl(sender, e);
        }
        public void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            closePhoneControl(sender, e);
        }

        public void closePhoneControl(object sender, RoutedEventArgs e)
        {
            PocketPaintApplication.GetInstance().PhoneControl.Visibility = Visibility.Collapsed;
            PocketPaintApplication.GetInstance().PaintingAreaView.changeBackgroundColorAndOpacityOfPaintingAreaCanvas(Colors.Transparent, 1.0);
            PocketPaintApplication.GetInstance().PaintingAreaView.BottomAppBar.Visibility = Visibility.Visible;
            PocketPaintApplication.GetInstance().AppbarTop.Visibility = Visibility.Visible;

            _captureManager.Dispose();
            _captureManager = null;
        }

        async private void btnChangeCamera_Click(object sender, RoutedEventArgs e)
        {
            AppBarButton button = sender as AppBarButton;
            if (button != null)
            {
                BitmapIcon icon = new BitmapIcon();
                icon.UriSource = isBackCameraActive ?
                    new Uri("ms-appx:///Assets/AppBar/BackCam.png"):
                    new Uri("ms-appx:///Assets/AppBar/FrontCam.png");
                button.Icon = icon;
            }

            if (isPreview)
            {
                await _captureManager.StopPreviewAsync();
            }
            _captureManager.Dispose();
            changeCamera();
            initPhotoControl();
        }

        private void changeCamera()
        {
            isBackCameraActive = isBackCameraActive ? !isBackCameraActive : true;
        }

        private void sldBrigthness_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            try
            {
                if (sldBrightness != null)
                {
                    sldBrightness.Value = sldBrightness.Value > 100 ? 100 : sldBrightness.Value;
                    bool succeeded = _captureManager.VideoDeviceController.Brightness.TrySetValue(sldBrightness.Value);
                    if (!succeeded)
                    {
                        //ShowStatusMessage("Set Brightness failed");
                    }
                    else
                    {
                        tbBrightnessValue.Text = sldBrightness.Value.ToString();
                    }
                }
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.StackTrace);
            }
        }

        private void sldContrast_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            try
            {
                bool succeeded = _captureManager.VideoDeviceController.Contrast.TrySetValue(sldContrast.Value);
                if (!succeeded)
                {
                    //ShowStatusMessage("Set Brightness failed");
                }
                else
                {
                    tbContrastValue.Text = sldContrast.Value.ToString();
                }
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.StackTrace);
            }
        }

        private void app_btnSettings_Click(object sender, RoutedEventArgs e)
        {
            GridSettings.Visibility = GridSettings.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            GridSettings.Visibility = GridSettings.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
