using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Catrobat.IDE.Phone.Themes
{
    public class Theme
    {
        private readonly string _backgroundPath;
        private readonly string _croppedPath;

        private BitmapImage _background;

        public BitmapImage Background
        {
            get {
                return _background ?? 
                    (_background = new BitmapImage(new Uri(_backgroundPath + "_800.png", UriKind.Relative)));
            }
        }

        private BitmapImage _croppedBackground;

        public BitmapImage CroppedBackground
        {
            get
            {
                if (_croppedBackground == null)
                {
                    _croppedBackground = new BitmapImage(new Uri(_croppedPath, UriKind.Relative));
                }
                return _croppedBackground;
            }
        }

        public SolidColorBrush AccentColor1 { get; set; }
        public SolidColorBrush AccentColor2 { get; set; }
        public SolidColorBrush AccentColor3 { get; set; }
        public SolidColorBrush AppBarColor { get; set; }

        public Theme(string backgroundPath, string croppedPath)
        {
            _backgroundPath = backgroundPath;
            _croppedPath = croppedPath;
        }
    }
}