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

// Die Elementvorlage "Benutzersteuerelement" ist unter http://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // It is very to implement the logic if the app is suspended. Otherwise there could be some side effects.
    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public sealed partial class PhotoControl : UserControl
    {
        Windows.Media.Capture.MediaCapture _photoManager = null;
        MediaCaptureInitializationSettings _mediaCaptureSettings = null;
        int activeCameraValue = 1;
        bool isPreview = false;
        DeviceInformationCollection _mobileCameras = null;
        public PhotoControl()
        {
            this.InitializeComponent();
            initDeviceInformationCollection();
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
            if(_photoManager != null)
            {
                _photoManager.Dispose();
                _photoManager = null;
            }
            _photoManager = new MediaCapture();
            await _photoManager.InitializeAsync(getMediaCaptureInitializationSettings());
            // Element is in xaml (is needed to show the camera preview).
            cptElementShowPreview.Source = _photoManager;
            await _photoManager.StartPreviewAsync();

            DisplayInformation displayInfo = DisplayInformation.GetForCurrentView();
            displayInfo.OrientationChanged += DisplayInfo_OrientationChanged;

            DisplayInfo_OrientationChanged(displayInfo, null);
        }

        private void DisplayInfo_OrientationChanged(DisplayInformation sender, object args)
        {
            if (_photoManager != null)
            {
                if(activeCamera == 1)
                {
                    _photoManager.SetPreviewRotation(VideoRotationLookup(sender.CurrentOrientation, true));
                }
                else
                {
                    _photoManager.SetPreviewRotation(VideoRotationLookup(sender.CurrentOrientation, false));
                }
                var rotation = VideoRotationLookup(sender.CurrentOrientation, false);
                _photoManager.SetRecordRotation(rotation);
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
            _mediaCaptureSettings.VideoDeviceId = _mobileCameras[activeCamera].Id;
            _mediaCaptureSettings.AudioDeviceId = "";
            _mediaCaptureSettings.StreamingCaptureMode = Windows.Media.Capture.StreamingCaptureMode.AudioAndVideo;
            _mediaCaptureSettings.PhotoCaptureSource = Windows.Media.Capture.PhotoCaptureSource.VideoPreview;
        }


        async private void btnTakePhoto_Click(object sender, RoutedEventArgs e)
        {
            ImageEncodingProperties imgFormat = ImageEncodingProperties.CreateJpeg();
            // a file for photo saving
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                    "photo.jpg", CreationCollisionOption.ReplaceExisting);
            await _photoManager.CapturePhotoToStorageFileAsync(imgFormat, file);
            ImageBrush fillBrush = new ImageBrush();

            BitmapImage image = new BitmapImage();
            image.UriSource = new Uri(file.Path, UriKind.RelativeOrAbsolute);
            fillBrush.ImageSource = image;
            if (PocketPaintApplication.GetInstance().isLoadPictureClicked)
            {
                RectangleGeometry myRectangleGeometry = new RectangleGeometry();
                myRectangleGeometry.Rect = new Rect(new Point(0, 0), new Point(Window.Current.Bounds.Width, Window.Current.Bounds.Height));

                Path _path = new Path();
                _path.Fill = fillBrush;
                _path.Stroke = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;

                _path.Data = myRectangleGeometry;
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(_path);
            }
            else
            {
                PocketPaintApplication.GetInstance().ImportImageSelectionControl.imageSourceOfRectangleToDraw = fillBrush;
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

            //await _photoManager.StopPreviewAsync();
            _photoManager.Dispose();
            _photoManager = null;
        }

        public int activeCamera
        {
            get
            {
                return activeCameraValue;
            }
            set
            {
                activeCameraValue = value;
            }
        }

        async private void btnChangeCamera_Click(object sender, RoutedEventArgs e)
        {
            if (isPreview)
            {
                await _photoManager.StopPreviewAsync();
            }
            _photoManager.Dispose();
            changeCamera();
            initPhotoControl();
        }

        private void changeCamera()
        {
            if (activeCameraValue == 0)
            {
                activeCameraValue = 1;
            }
            else
            {
                activeCameraValue = 0;
            }
        }
    }
}
