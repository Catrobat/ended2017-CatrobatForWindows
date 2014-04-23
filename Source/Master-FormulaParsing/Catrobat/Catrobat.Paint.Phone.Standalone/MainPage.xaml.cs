using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Catrobat.Paint.Phone.Standalone.Resources;

namespace Catrobat.Paint.Phone.Standalone
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
            var b = new BitmapImage(u) { CreateOptions = BitmapCreateOptions.None };

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