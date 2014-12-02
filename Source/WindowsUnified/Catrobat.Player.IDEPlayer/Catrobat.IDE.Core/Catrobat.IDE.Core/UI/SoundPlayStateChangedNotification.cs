using Catrobat.IDE.Core.Models;

namespace Catrobat.IDE.Core.UI
{
    public enum SoundPlayState { Loading, Playing, Paused, Stopped }

    public class SoundPlayStateChangedNotification
    {
        public Sound OldSound { get; set; }
        public SoundPlayState OldState { get; set; }
        
        public Sound NewSound { get; set; }
        public SoundPlayState NewState { get; set; }
    }
}
