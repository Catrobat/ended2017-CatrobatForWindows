using System.ComponentModel;
using System.Runtime.CompilerServices;
using Catrobat.IDEWindowsPhone.Annotations;
using Microsoft.Xna.Framework.Media;

namespace Catrobat.IDEWindowsPhone.Utilities.Sounds
{
    public class SoundListItem : INotifyPropertyChanged
    {
        private Song _song;

        public Song Song
        {
            get { return _song; }
            set
            {
                if (_song != value)
                {
                    _song = value;
                    RaisePropertyChanged();
                }
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