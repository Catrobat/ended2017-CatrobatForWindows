using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Catrobat.IDEWindowsPhone7.Themes
{
  public class ThemeChooser : INotifyPropertyChanged
  {
    private ObservableCollection<Theme> themes;
    public ObservableCollection<Theme> Themes
    {
      get
      {
        return themes;
      }
    }

    private Theme selectedTheme;
    public Theme SelectedTheme
    {
      get
      {
        return selectedTheme;
      }
      set
      {
        if (selectedTheme != value && value != null)
        {
          selectedTheme = value;
          
        }

        OnPropertyChanged("SelectedTheme");
      }
    }

    private int selectedThemeIndex;
    public int SelectedThemeIndex
    {
      get
      {
        int index = 0;

        foreach(Theme theme in themes)
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
      themes = new ObservableCollection<Theme>(){
        new Theme("/Content/Images/Application/MainViewPanoramaBackgrounds/MainViewPanorama_strings.png",
                  "/Content/Images/Application/MainViewPanoramaBackgrounds/CroppedImages/MainViewPanorama_strings_small.png"),
        new Theme("/Content/Images/Application/MainViewPanoramaBackgrounds/MainViewPanorama_cats_blue.png",
                  "/Content/Images/Application/MainViewPanoramaBackgrounds/CroppedImages/MainViewPanorama_cats_blue_small.png"),
        new Theme("/Content/Images/Application/MainViewPanoramaBackgrounds/MainViewPanorama_cats_pink.png",
                  "/Content/Images/Application/MainViewPanoramaBackgrounds/CroppedImages/MainViewPanorama_cats_pink_small.png")
      };

      selectedTheme = themes[0];
    }
  }
}
