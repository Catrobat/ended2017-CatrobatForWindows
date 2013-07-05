using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using Catrobat.IDEWindowsPhone.Annotations;

namespace Catrobat.IDEWindowsPhone.Themes
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
            var theme1 = new Theme("/Content/Images/Application/MainViewPanoramaBackgrounds/MainViewPanorama_strings",
                                   "/Content/Images/Application/MainViewPanoramaBackgrounds/CroppedImages/MainViewPanorama_strings_small.png")
            {
                AccentColor1 = new SolidColorBrush(Colors.Purple),
                AccentColor2 = new SolidColorBrush(Colors.Green),
                AccentColor3 = new SolidColorBrush(Colors.Orange),
                AppBarColor = new SolidColorBrush(Colors.Red)
            };


            var theme2 = new Theme("/Content/Images/Application/MainViewPanoramaBackgrounds/MainViewPanorama_cats_blue",
                                   "/Content/Images/Application/MainViewPanoramaBackgrounds/CroppedImages/MainViewPanorama_cats_blue_small.png")
            {
                AccentColor1 = new SolidColorBrush(Colors.Purple),
                AccentColor2 = new SolidColorBrush(Colors.Green),
                AccentColor3 = new SolidColorBrush(Colors.Orange),
                AppBarColor = new SolidColorBrush(Colors.Red)
            };

            var theme3 = new Theme("/Content/Images/Application/MainViewPanoramaBackgrounds/MainViewPanorama_cats_pink",
                                   "/Content/Images/Application/MainViewPanoramaBackgrounds/CroppedImages/MainViewPanorama_cats_pink_small.png")
            {
                AccentColor1 = new SolidColorBrush(Colors.Purple),
                AccentColor2 = new SolidColorBrush(Colors.Green),
                AccentColor3 = new SolidColorBrush(Colors.Orange),
                AppBarColor = new SolidColorBrush(Colors.Red)
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