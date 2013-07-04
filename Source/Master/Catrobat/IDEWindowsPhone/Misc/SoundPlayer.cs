using System.Threading;
using System.Windows;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Sounds;
using Catrobat.Core.Storage;
using Microsoft.Xna.Framework.Audio;

namespace Catrobat.IDEWindowsPhone.Misc
{
    public enum SoundState
    {
        Playing,
        Paused,
        Stopped
    }

    public delegate void SoundStateChanged(SoundState oldState, SoundState newState);

    public delegate void SoundFinished();

    public class SoundPlayer
    {
        public SoundStateChanged SoundStateChanged;
        public SoundFinished SoundFinished;

        private SoundEffectInstance _soundEffect;
        private Thread _checkSoundThread;
        private SoundState _previousState;

        private bool _aborted = false;

        public void SetSound(Sound sound)
        {
            if (_soundEffect != null)
            {
                _soundEffect.Stop();
            }

            _previousState = SoundState.Stopped;

            var path = CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.SoundsPath + "/" +
                       sound.FileName;

            using (var storage = StorageSystem.GetStorage())
            {
                if (storage.FileExists(path))
                {
                    using (var stream = storage.OpenFile(path, StorageFileMode.Open, StorageFileAccess.Read))
                    {
                        var soundArray = new byte[stream.Length];
                        stream.Read(soundArray, 0, soundArray.Length);
                        stream.Close();
                        stream.Dispose();
                        _soundEffect = new SoundEffect(soundArray, Microphone.Default.SampleRate, AudioChannels.Mono).CreateInstance();
                    }
                }
            }
        }

        private void CheckIfSoundFinished()
        {
            var newState = GetSoundState(_soundEffect.State);
            var previousStateTemp = _previousState;
            _previousState = newState;

            do
            {
                _previousState = newState;
                newState = GetSoundState(_soundEffect.State);
                Thread.Sleep(50);
            } while (newState == _previousState);

            Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    if (SoundFinished != null && !_aborted)
                    {
                        SoundFinished.Invoke();
                    }

                    _aborted = false;

                    if (SoundStateChanged != null)
                    {
                        SoundStateChanged.Invoke(previousStateTemp, newState);
                    }
                });
        }

        private SoundState GetSoundState(Microsoft.Xna.Framework.Audio.SoundState state)
        {
            switch (state)
            {
                case Microsoft.Xna.Framework.Audio.SoundState.Paused:
                    return SoundState.Paused;

                case Microsoft.Xna.Framework.Audio.SoundState.Playing:
                    return SoundState.Playing;

                default:
                    return SoundState.Stopped;
            }
        }

        public void Play()
        {
            if (_soundEffect != null)
            {
                if (_checkSoundThread != null)
                {
                    _checkSoundThread.Abort();
                }
                _checkSoundThread = new Thread(CheckIfSoundFinished);
                _checkSoundThread.Start();

                _soundEffect.Play();
                var previousStateTemp = _previousState;
                _previousState = SoundState.Playing;
                if (SoundStateChanged != null)
                {
                    SoundStateChanged.Invoke(previousStateTemp, _previousState);
                }
            }
        }

        public void Pause()
        {
            if (_soundEffect != null)
            {
                _aborted = true;

                _soundEffect.Pause();
                var previousStateTemp = _previousState;
                _previousState = SoundState.Paused;
                if (SoundStateChanged != null)
                {
                    SoundStateChanged.Invoke(previousStateTemp, _previousState);
                }
            }
        }

        public void Stop()
        {
            if (_soundEffect != null)
            {
                _aborted = true;

                _soundEffect.Stop();
                var previousStateTemp = _previousState;
                _previousState = SoundState.Stopped;
                if (SoundStateChanged != null)
                {
                    SoundStateChanged.Invoke(previousStateTemp, _previousState);
                }
            }
        }

        public void Clear()
        {
            Stop();
            SoundStateChanged = null;
        }
    }
}