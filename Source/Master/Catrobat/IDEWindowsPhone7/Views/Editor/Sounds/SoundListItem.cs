using System.ComponentModel;
using Catrobat.IDEWindowsPhone7.Controls.Buttons;
using Microsoft.Xna.Framework.Media;

namespace Catrobat.IDEWindowsPhone7.Views.Editor.Sounds
{
  public class SoundListItem : INotifyPropertyChanged
  {
    private Song song;
    public Song Song
    {
      get
      {
        return song;
      }
      set
      {
        if (this.song == value)
          return;

        this.song = value;
        this.OnPropertyChanged(new PropertyChangedEventArgs("Song"));
      }
    }

    private PlayButtonState state;
    public PlayButtonState State
    {
      get
      {
        return state;
      }
      set
      {
        if (this.state == value)
          return;

        this.state = value;
        this.OnPropertyChanged(new PropertyChangedEventArgs("State"));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      if (this.PropertyChanged != null)
        this.PropertyChanged(this, e);
    }
  }
}
