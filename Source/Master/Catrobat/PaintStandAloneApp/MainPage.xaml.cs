using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Catrobat.Paint;
using Microsoft.Phone.Controls;

namespace Catrobat.PaintStandAloneApp
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
            try
            {

                var u = new Uri("test.jpg", UriKind.Relative);
                var b = new BitmapImage(u);
                b.CreateOptions = BitmapCreateOptions.None;
                
                var w = new WriteableBitmap(b as BitmapSource);

            }
            catch (Exception e)
            {
                
                throw;
            }
            var task = new PaintLauncherTask { CurrentImage = null};
            task.OnImageChanged += OnPaintLauncherTaskImageChanged;
            PaintLauncher.Launche(task);
             
        }

        private void OnPaintLauncherTaskImageChanged(PaintLauncherTask task)
        {
            
        }
    }
}