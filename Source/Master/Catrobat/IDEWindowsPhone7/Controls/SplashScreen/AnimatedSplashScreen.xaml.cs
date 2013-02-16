using System;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Media.Imaging;

namespace Catrobat.IDEWindowsPhone.Controls.SplashScreen
{
  public partial class AnimatedSplashScreen : UserControl
  {
    public static int MillisBetweenSwitch = 500;
    bool imageActive = true;

    public AnimatedSplashScreen()
    {
      InitializeComponent();
      Thread thread = new Thread(switchImage);
      thread.Start();
    }

    private void switchImage()
    {
      while (true)
      {
        Dispatcher.BeginInvoke(() =>
        {
          if (imageActive)
          {
            imageActive = false;
            BitmapImage image = new BitmapImage(new Uri("/IDEWindowsPhone;component/Resources/Application/down.png", UriKind.Relative));
            imageSplashCat.Source = image;
          }
          else
          {
            imageActive = true;
            BitmapImage image = new BitmapImage(new Uri("/IDEWindowsPhone;component/Resources/Application/up.png", UriKind.Relative));
            imageSplashCat.Source = image;
          }

        });
        Thread.Sleep(MillisBetweenSwitch);

      }
    }
  }
}
