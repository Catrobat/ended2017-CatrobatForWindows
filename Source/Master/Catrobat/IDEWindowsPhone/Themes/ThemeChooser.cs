using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace Catrobat.IDEWindowsPhone.Themes
{
  public class ThemeChooser : INotifyPropertyChanged
  {
    private readonly ObservableCollection<Theme> themes;
    public ObservableCollection<Theme> Themes
    {
      get
      {
        return themes;
      }
    }

    private Theme _selectedTheme;
    public Theme SelectedTheme
    {
      get
      {
        return _selectedTheme;
      }
      set
      {
        if (_selectedTheme != value && value != null)
        {
          _selectedTheme = value;

        }

        OnPropertyChanged("SelectedTheme");
      }
    }

    private int _selectedThemeIndex;
    public int SelectedThemeIndex
    {
      get
      {
        int index = 0;

        foreach (Theme theme in themes)
        {
          if (theme == SelectedTheme)
            return index;

          index++;
        }

        return -1;
      }
      set
      {
        try
        {
          SelectedTheme = themes[value];
        }
        catch { }
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string property)
    {
      if (this.PropertyChanged != null)
        this.PropertyChanged(this, new PropertyChangedEventArgs(property));
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


      themes = new ObservableCollection<Theme>() { theme1, theme2, theme3 };

      _selectedTheme = themes[0];
    }
  }
}
