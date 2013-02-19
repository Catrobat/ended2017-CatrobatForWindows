using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Catrobat.IDEWindowsPhone.Themes
{
  public class Theme
  {
    private readonly string _backgroundPath;
    private readonly string _croppedPath;

    private BitmapImage _background;
    public BitmapImage Background
    {
      get
      {
        if (_background == null)
        {
          _background = new BitmapImage(new Uri(_backgroundPath, UriKind.Relative));
        }
        return _background;
      }
    }

    private BitmapImage _croppedBackground;
    public BitmapImage CroppedBackground
    {
      get
      {
        if (_croppedBackground == null)
          _croppedBackground = new BitmapImage(new Uri(_croppedPath, UriKind.Relative));
        return _croppedBackground;
      }
    }

    public SolidColorBrush AccentColor1 { get; set; }
    public SolidColorBrush AccentColor2 { get; set; }
    public SolidColorBrush AccentColor3 { get; set; }

    public Theme(string backgroundPath, string croppedPath)
    {
      this._backgroundPath = backgroundPath;
      this._croppedPath = croppedPath;
    }
  }
}
