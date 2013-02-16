using System;
using System.Windows.Media.Imaging;

namespace Catrobat.IDEWindowsPhone.Themes
{
  public class Theme
  {
    private string backgroundPath;
    private string croppedPath;

    private BitmapImage background;
    public BitmapImage Background
    {
      get
      {
        if (background == null)
        {
          background = new BitmapImage(new Uri(backgroundPath, UriKind.Relative));
        }
        return background;
      }
    }

    private BitmapImage croppedBackground;
    public BitmapImage CroppedBackground
    {
      get
      {
        if (croppedBackground == null)
          croppedBackground = new BitmapImage(new Uri(croppedPath, UriKind.Relative));
        return croppedBackground;
      }
    }

    public Theme(string backgroundPath, string croppedPath)
    {
      this.backgroundPath = backgroundPath;
      this.croppedPath = croppedPath;
    }
  }
}
