using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Catrobat.IDE.Core.Annotations;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.UI
{
    public class ThemeChooser : INotifyPropertyChanged
    {
        private readonly ObservableCollection<Theme> _themes;

        public ObservableCollection<Theme> Themes
        {
            get { return _themes; }
        }

        private Theme _selectedTheme;

        public Theme SelectedTheme
        {
            get { return _selectedTheme; }
            set
            {
                if (_selectedTheme != value && value != null)
                {
                    _selectedTheme = value;
                }

                RaisePropertyChanged();
            }
        }

        private readonly StaticTheme _staticTheme = new StaticTheme();
        public StaticTheme StaticTheme
        {
            get { return _staticTheme; }
        }

        public int SelectedThemeIndex
        {
            get
            {
                var index = 0;

                foreach (Theme theme in _themes)
                {
                    if (theme == SelectedTheme)
                    {
                        return index;
                    }

                    index++;
                }

                return -1;
            }
            set { SelectedTheme = _themes[value]; }
        }

        public ThemeChooser()
        {
            var theme1 = new Theme("/Content/Images/Application/MainViewPanoramaBackgrounds/empty_background.png",
                                   "/Content/Images/Application/MainViewPanoramaBackgrounds/CroppedImages/empty_background.png")
            {
                AccentColor1 = new PortableSolidColorBrush(255, 128, 0, 128),
                AccentColor2 = new PortableSolidColorBrush(255, 0, 128, 0),
                AccentColor3 = new PortableSolidColorBrush(255, 155, 165, 0),
                AppBarColor = new PortableSolidColorBrush(255, 50, 50, 50)
            };


            var theme2 = new Theme("/Content/Images/Application/MainViewPanoramaBackgrounds/MainViewPanorama_cats_blue.png",
                                   "/Content/Images/Application/MainViewPanoramaBackgrounds/CroppedImages/MainViewPanorama_cats_blue_small.png")
            {
                AccentColor1 = new PortableSolidColorBrush(255, 128, 0, 128),
                AccentColor2 = new PortableSolidColorBrush(255, 0, 128, 0),
                AccentColor3 = new PortableSolidColorBrush(255, 155, 165, 0),
                AppBarColor = new PortableSolidColorBrush(255, 50, 50, 50)
            };

            var theme3 = new Theme("/Content/Images/Application/MainViewPanoramaBackgrounds/MainViewPanorama_cats_pink.png",
                                   "/Content/Images/Application/MainViewPanoramaBackgrounds/CroppedImages/MainViewPanorama_cats_pink_small.png")
            {
                AccentColor1 = new PortableSolidColorBrush(255, 128, 0, 128),
                AccentColor2 = new PortableSolidColorBrush(255, 0, 128, 0),
                AccentColor3 = new PortableSolidColorBrush(255, 155, 165, 0),
                AppBarColor = new PortableSolidColorBrush(255, 50, 50, 50)
            };


            _themes = new ObservableCollection<Theme>() {theme1, theme2, theme3};

            _selectedTheme = _themes[0];
        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}