using System;
using System.Windows;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Catrobat.Paint.Phone
{
    public static class PaintLauncher
    {
        public static PaintLauncherTask Task { get; set; }

        public static void Launche(PaintLauncherTask task)
        {
            Task = task;
            
            // TODO: ((PhoneApplicationFrame)Application.Current.RootVisual).Navigate(new Uri("/Catrobat.Paint.Phone;component/View/PaintingAreaView.xaml", UriKind.RelativeOrAbsolute));
        }
    }

    public class PaintLauncherTask
    {
        public delegate void ImageChanged(PaintLauncherTask task);

        public ImageChanged OnImageChanged;

        public void RaiseImageChanged()
        {
            if (OnImageChanged != null)
                OnImageChanged.Invoke(this);
        }

        public BitmapSource CurrentImage { get; set; }
    }
}
