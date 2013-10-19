using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using Catrobat.IDE.Core.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Catrobat.IDEWindowsPhone.Services
{
    public class SoundRecorderServicePhone : ISoundRecorderService
    {
        private Microphone _microphone;
        private byte[] _audioBuffer;
        private int _sampleRate;
        private MemoryStream _recordingStream;
        private SoundEffectInstance _sound;

        public SoundRecorderState State { get; private set; }

        public int SampleRate
        {
            get { return _sampleRate; }
        }

        public void InitializeSound()
        {
            if (_sound == null)
            {
                _sound = new SoundEffect(_recordingStream.ToArray(), _sampleRate, AudioChannels.Mono).CreateInstance();
            }
        }

        public void StartRecording()
        {
            InitializeMicrophone();
            _microphone.Start();
            State = SoundRecorderState.Recording;
        }

        public void StopRecording()
        {
            State = SoundRecorderState.StoppingRecording;
        }

        public void PlayRecordedSound()
        {
            _sound.Play();

            State = SoundRecorderState.Playing;
        }

        public void StopPlayingRecordedSound()
        {
            if (_sound != null)
            {
                _sound.Stop();
            }

            State = SoundRecorderState.NoAction;
        }

        public MemoryStream GetSoundAsStream()
        {
            return _recordingStream;
        }




        public SoundRecorderServicePhone()
        {
            State = SoundRecorderState.NoAction;
            _sound = null;

            InitializeMicrophone();
        }


        private void currentMicrophone_BufferReady(object sender, EventArgs e)
        {
            _microphone.GetData(_audioBuffer);
            _recordingStream.Write(_audioBuffer, 0, _audioBuffer.Length);
            if (State != SoundRecorderState.StoppingRecording)
                return;

            DeleteMicrophone();
        }

        private void InitializeMicrophone()
        {
            if (_microphone == null)
            {
                _microphone = Microphone.Default;
                _microphone.BufferDuration = TimeSpan.FromMilliseconds(300);
                _microphone.BufferReady += currentMicrophone_BufferReady;
                _audioBuffer = new byte[_microphone.GetSampleSizeInBytes(_microphone.BufferDuration)];
                _sampleRate = _microphone.SampleRate;
            }

            _recordingStream = new MemoryStream(1048576);
            State = SoundRecorderState.NoAction;
        }

        private void DeleteMicrophone()
        {
            if (_microphone != null)
            {
                _microphone.Stop();
                _microphone.BufferReady -= currentMicrophone_BufferReady;
                _microphone = null;
            }
            State = SoundRecorderState.NoAction;
        }

        public class RecorderDispatcher : IApplicationService
        {
            private readonly DispatcherTimer _frameworkDispatcherTimer;

            public RecorderDispatcher(TimeSpan dispatchInterval)
            {
                FrameworkDispatcher.Update();
                _frameworkDispatcherTimer = new DispatcherTimer();
                _frameworkDispatcherTimer.Tick += new EventHandler(frameworkDispatcherTimer_Tick);
                _frameworkDispatcherTimer.Interval = dispatchInterval;
            }

            void IApplicationService.StartService(ApplicationServiceContext context)
            {
                _frameworkDispatcherTimer.Start();
            }

            void IApplicationService.StopService()
            {
                _frameworkDispatcherTimer.Stop();
            }

            private void frameworkDispatcherTimer_Tick(object sender, EventArgs e)
            {
                FrameworkDispatcher.Update();
            }
        }
    }
}