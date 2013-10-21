using System;
using System.IO;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Store.Services
{
    public class SoundRecorderServicePhone : ISoundRecorderService
    {

        public SoundRecorderState State
        {
            get { throw new NotImplementedException(); }
        }

        public int SampleRate
        {
            get { throw new NotImplementedException(); }
        }

        public void InitializeSound()
        {
            throw new NotImplementedException();
        }

        public void StartRecording()
        {
            throw new NotImplementedException();
        }

        public void StopRecording()
        {
            throw new NotImplementedException();
        }

        public void PlayRecordedSound()
        {
            throw new NotImplementedException();
        }

        public void StopPlayingRecordedSound()
        {
            throw new NotImplementedException();
        }

        public MemoryStream GetSoundAsStream()
        {
            throw new NotImplementedException();
        }
    }
}