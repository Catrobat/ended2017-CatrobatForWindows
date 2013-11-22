using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catrobat.Core.Services
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
