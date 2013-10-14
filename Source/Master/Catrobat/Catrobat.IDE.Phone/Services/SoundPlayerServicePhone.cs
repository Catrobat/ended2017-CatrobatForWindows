using System.Threading;
using System.Windows;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Sounds;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;
using Microsoft.Xna.Framework.Audio;

namespace Catrobat.IDE.Phone.Services
{
    public class SoundPlayerServicePhone : ISoundPlayerService
    {
        private string _currentProjectBasePath;
        private SoundEffectInstance _soundEffect;
        private Thread _checkSoundThread;
        private SoundPlayerState _previousState;
        private bool _aborted = false;

        public event SoundStateChanged SoundStateChanged;
        public event SoundFinished SoundFinished;

        public void SetSound(Sound sound, Project currentProject)
        {
            _currentProjectBasePath = currentProject.BasePath;

            if (_soundEffect != null)
            {
                _soundEffect.Stop();
            }

            _previousState = SoundPlayerState.Stopped;

            var path = _currentProjectBasePath + "/" + Project.SoundsPath + "/" +
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
                _previousState = SoundPlayerState.Playing;
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
                _previousState = SoundPlayerState.Paused;
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
                _previousState = SoundPlayerState.Stopped;
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
            } while (newState == _previousState && newState != SoundPlayerState.Stopped);

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                //if (SoundFinished != null && !_aborted)
                //{
                    SoundFinished.Invoke();
                //}

                _aborted = false;

                if (SoundStateChanged != null)
                {
                    SoundStateChanged.Invoke(previousStateTemp, newState);
                }
            });
        }

        private SoundPlayerState GetSoundState(Microsoft.Xna.Framework.Audio.SoundState state)
        {
            switch (state)
            {
                case Microsoft.Xna.Framework.Audio.SoundState.Paused:
                    return SoundPlayerState.Paused;

                case Microsoft.Xna.Framework.Audio.SoundState.Playing:
                    return SoundPlayerState.Playing;

                default:
                    return SoundPlayerState.Stopped;
            }
        }
    }
}