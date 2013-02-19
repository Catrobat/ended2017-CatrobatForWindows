using System;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Media.Imaging;

namespace Catrobat.IDEWindowsPhone.Controls.SplashScreen
{
  public partial class AnimatedSplashScreen : UserControl
  {
    public static int MillisBetweenSwitch = 500;
    bool _imageActive = true;

    public AnimatedSplashScreen()
    {
      InitializeComponent();
      Thread thread = new Thread(SwitchImage);
      thread.Start();
    }

    private void SwitchImage()
    {
      while (true)
      {
        Dispatcher.BeginInvoke(() =>
        {
          if (_imageActive)
          {
            _imageActive = false;
            var image = new BitmapImage(new Uri("Resources/Application/down.png", UriKind.Relative));
            imageSplashCat.Source = image;
          }
          else
          {
            _imageActive = true;
            var image = new BitmapImage(new Uri("Resources/Application/up.png", UriKind.Relative));
            imageSplashCat.Source = image;
          }

        });
        Thread.Sleep(MillisBetweenSwitch);

      }
    }
  }
}
