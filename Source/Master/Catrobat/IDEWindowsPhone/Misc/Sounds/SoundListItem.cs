using System.ComponentModel;
using System.Runtime.CompilerServices;
using Catrobat.IDEWindowsPhone.Annotations;
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
        this.RaisePropertyChanged();
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
        this.RaisePropertyChanged();
      }
    }

    #region PropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    [NotifyPropertyChangedInvocator]

    protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
  }
}
