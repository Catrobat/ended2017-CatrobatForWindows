using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.UI
{
    public class Theme
    {
        private readonly string _backgroundPath;
        private readonly string _croppedPath;

        private PortableImage _background;

        public PortableImage Background
        {
            get {

                if (_background == null)
                {
                    _background = new PortableImage();
                    _background.LoadFromResources(ResourceScope.IdePhone, _backgroundPath);
                }

                return _background;
            }
        }

        private PortableImage _croppedBackground;

        public PortableImage CroppedBackground
        {
            get
            {
                if (_croppedBackground == null)
                {
                    _croppedBackground = new PortableImage();
                    _croppedBackground.LoadFromResources(ResourceScope.IdePhone, _croppedPath);
                }

                return _croppedBackground;
            }
        }

        public PortableSolidColorBrush AccentColor1 { get; set; }
        public PortableSolidColorBrush AccentColor2 { get; set; }
        public PortableSolidColorBrush AccentColor3 { get; set; }
        public PortableSolidColorBrush AppBarColor { get; set; }

        public Theme(string backgroundPath, string croppedPath)
        {
            _backgroundPath = backgroundPath;
            _croppedPath = croppedPath;
        }
    }
}