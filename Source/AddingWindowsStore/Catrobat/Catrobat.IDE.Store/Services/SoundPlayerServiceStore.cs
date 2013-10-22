using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Sounds;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Store.Services
{
    public class SoundPlayerServicePhone : ISoundPlayerService
    {
        public event SoundStateChanged SoundStateChanged;

        public event SoundFinished SoundFinished;

        public void SetSound(Sound sound, Project currentProject)
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