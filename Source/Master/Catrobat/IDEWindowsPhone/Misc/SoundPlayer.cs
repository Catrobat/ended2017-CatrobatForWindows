using System.Windows;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Sounds;
using Catrobat.Core.Storage;
using IDEWindowsPhone;
using Microsoft.Xna.Framework.Audio;
using System.Threading;
using System.IO;

namespace Catrobat.IDEWindowsPhone.Misc
{
    public enum SoundState { Playing, Paused, Stopped }
    public delegate void SoundStateChanged(SoundState oldState, SoundState newState);
    public delegate void SoundFinished();

    public class SoundPlayer
    {
        public SoundStateChanged SoundStateChanged;
        public SoundFinished SoundFinished;

        private SoundEffectInstance soundEffect;
        private Thread checkSoundThread;
        private SoundState previousState;

        private bool _aborted = false;

        public void SetSound(Sound sound)
        {
            if (soundEffect != null)
                soundEffect.Stop();

            previousState = SoundState.Stopped;

            string path = CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.SoundsPath + "/" + sound.FileName;

            using (IStorage storage = StorageSystem.GetStorage())
            {
                if (storage.FileExists(path))
                    using (Stream stream = storage.OpenFile(path, StorageFileMode.Open, StorageFileAccess.Read))
                    {
                        byte[] soundArray = new byte[stream.Length];
                        stream.Read(soundArray, 0, soundArray.Length);
                        stream.Close();
                        stream.Dispose();
                        soundEffect = new SoundEffect(soundArray, Microphone.Default.SampleRate, AudioChannels.Mono).CreateInstance();
                    }
            }
        }

        private void CheckIfSoundFinished()
        {
            SoundState newState = GetSoundState(soundEffect.State);
            SoundState previousStateTemp = previousState;
            previousState = newState;

            do
            {
                previousState = newState;
                newState = GetSoundState(soundEffect.State);
                Thread.Sleep(50);
            } while (newState == previousState);

            Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    if (SoundFinished != null && !_aborted)
                        SoundFinished.Invoke();

                    _aborted = false;

                    if (SoundStateChanged != null)
                        SoundStateChanged.Invoke(previousStateTemp, newState);
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
            if (soundEffect != null)
            {
                if (checkSoundThread != null)
                    checkSoundThread.Abort();
                checkSoundThread = new Thread(CheckIfSoundFinished);
                checkSoundThread.Start();

                soundEffect.Play();
                SoundState previousStateTemp = previousState;
                previousState = SoundState.Playing;
                if (SoundStateChanged != null)
                    SoundStateChanged.Invoke(previousStateTemp, previousState);
            }
        }

        public void Pause()
        {
            if (soundEffect != null)
            {
                _aborted = true;

                soundEffect.Pause();
                SoundState previousStateTemp = previousState;
                previousState = SoundState.Paused;
                if (SoundStateChanged != null)
                    SoundStateChanged.Invoke(previousStateTemp, previousState);
            }
        }

        public void Stop()
        {
            if (soundEffect != null)
            {
                _aborted = true;

                soundEffect.Stop();
                SoundState previousStateTemp = previousState;
                previousState = SoundState.Stopped;
                if (SoundStateChanged != null)
                    SoundStateChanged.Invoke(previousStateTemp, previousState);
            }
        }

        public void Clear()
        {
            Stop();
            SoundStateChanged = null;
        }
    }
}
