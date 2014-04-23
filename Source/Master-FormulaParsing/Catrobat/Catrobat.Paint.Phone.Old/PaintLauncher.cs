using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;

namespace Catrobat.Paint.Phone.Old
{
    public static class PaintLauncher
    {
        public static PaintLauncherTask Task { get; set; }

        public static void Launche(PaintLauncherTask task)
        {
            Task = task;
            ((PhoneApplicationFrame)Application.Current.RootVisual).Navigate(new Uri("/Catrobat.Paint.Phone.Old;component/View/PaintingAreaView.xaml", UriKind.RelativeOrAbsolute));
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
