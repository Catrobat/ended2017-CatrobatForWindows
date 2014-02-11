using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Catrobat.Paint;
using Microsoft.Phone.Controls;

namespace Catrobat.Paint.Standalone
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
//            PaintLauncher.Task.CurrentImage = new WriteableBitmap(new BitmapImage(new Uri("/Content/TestImage.png", UriKind.Relative)));

                var u = new Uri("/Content/TestImage.png", UriKind.Relative);
                var b = new BitmapImage(u) {CreateOptions = BitmapCreateOptions.None};

                var w = new WriteableBitmap(b);

                var task = new PaintLauncherTask { CurrentImage = w };
            task.OnImageChanged += OnPaintLauncherTaskImageChanged;
            PaintLauncher.Launche(task);
             
        }

        private void OnPaintLauncherTaskImageChanged(PaintLauncherTask task)
        {
            
        }
    }
}