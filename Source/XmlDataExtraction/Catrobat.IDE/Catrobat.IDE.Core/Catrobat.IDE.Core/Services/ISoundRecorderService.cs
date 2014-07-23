using System.IO;

namespace Catrobat.IDE.Core.Services
{
    public enum SoundRecorderState {NoAction, Recording, StoppingRecording, Playing}

    public interface ISoundRecorderService
    {
        SoundRecorderState State { get; }

        int SampleRate { get; }

        void InitializeSound();

        void StartRecording();

        void StopRecording();

        void PlayRecordedSound();

        void StopPlayingRecordedSound();

        MemoryStream GetSoundAsStream();
    }
}
