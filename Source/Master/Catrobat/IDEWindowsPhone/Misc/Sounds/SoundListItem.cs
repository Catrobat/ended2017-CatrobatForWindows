using System.ComponentModel;
using Catrobat.IDEWindowsPhone.Controls.Buttons;
using Microsoft.Xna.Framework.Media;

namespace Catrobat.IDEWindowsPhone.Misc.Sounds
{
  public class SoundListItem : INotifyPropertyChanged
  {
    private Song _song;
    public Song Song
    {
      get
      {
        return _song;
      }
      set
      {
        if (this._song == value)
          return;

        this._song = value;
        this.OnPropertyChanged(new PropertyChangedEventArgs("Song"));
      }
    }

    private PlayButtonState _state;
    public PlayButtonState State
    {
      get
      {
        return _state;
      }
      set
      {
        if (this._state == value)
          return;

        this._state = value;
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
