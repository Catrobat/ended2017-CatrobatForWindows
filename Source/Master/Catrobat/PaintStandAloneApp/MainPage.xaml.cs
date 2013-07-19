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
            PaintLauncher.CurrentImage = new BitmapImage(new Uri("/Content/TestImage.png", UriKind.Relative));
            PaintLauncher.Launche();
        }
    }
}