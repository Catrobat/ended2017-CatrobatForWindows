using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Catrobat.IDEWindowsPhone.Misc;

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
          switch (ResolutionHelper.CurrentResolution)
          {
            case Resolutions.WVGA:
              _background = new BitmapImage(new Uri(_backgroundPath + "_800.png", UriKind.Relative));
              break;
            case Resolutions.WXGA:
              _background = new BitmapImage(new Uri(_backgroundPath + "_800.png", UriKind.Relative));
              break;
            case Resolutions.HD720p:
              _background = new BitmapImage(new Uri(_backgroundPath + "_800.png", UriKind.Relative));
              break;
            default:
              throw new ArgumentOutOfRangeException();
          }
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
