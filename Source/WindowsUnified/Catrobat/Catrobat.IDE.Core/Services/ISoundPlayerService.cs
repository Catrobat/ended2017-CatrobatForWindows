using Catrobat.IDE.Core.Models;

namespace Catrobat.IDE.Core.Services
{
    public enum SoundPlayerState
    {
        Playing,
        Paused,
        Stopped
    }

    public delegate void SoundStateChanged(SoundPlayerState oldState, SoundPlayerState newState);

    public delegate void SoundFinished();

    public interface ISoundPlayerService
    {
        event SoundStateChanged SoundStateChanged;
        event SoundFinished SoundFinished;

        void SetSound(Sound sound, Project currentProject);

        void Play();

        void Pause();

        void Stop();

        void Clear();
    }
}
