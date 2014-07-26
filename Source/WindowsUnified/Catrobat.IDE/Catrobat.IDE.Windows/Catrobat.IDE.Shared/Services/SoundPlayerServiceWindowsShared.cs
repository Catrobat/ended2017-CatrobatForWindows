using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class SoundPlayerServiceWindowsShared : ISoundPlayerService
    {
        public event SoundStateChanged SoundStateChanged;

        public event SoundFinished SoundFinished;

        public void SetSound(Sound sound, Program currentProject)
        {
            throw new System.NotImplementedException();
        }

        public void Play()
        {
            throw new System.NotImplementedException();
        }

        public void Pause()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }
    }
}