using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Sounds
{
  class Recorder
  {
    private Microphone _microphone;
    private byte[] _audioBuffer;
    private int _sampleRate;
    private MemoryStream _recordingStream;
    
    public bool StopRequested { get; set; }
    public int SampleRate { get { return _sampleRate; }}

    private SoundEffectInstance _sound;
    public SoundEffectInstance Sound
    {
      get { return _sound; }
      set { _sound = value; }
    }
    public void InitializeSound()
    {
      if (_sound == null)
        _sound = new SoundEffect(_recordingStream.ToArray(), _sampleRate, AudioChannels.Mono).CreateInstance();
    }

    public Recorder()
    {
      StopRequested = false;
      _sound = null;

      InitializeMicrophone();
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
      StopRequested = false;
    }

    private void DeleteMicrophone()
    {
      if (_microphone != null)
      {
        _microphone.Stop();
        _microphone.BufferReady -= currentMicrophone_BufferReady;
        _microphone = null;
      }
      StopRequested = false;
    }

    public void StartRecording()
    {
      InitializeMicrophone();
      _microphone.Start();
    }

    public void StopRecording()
    {
      StopRequested = true;
    }

    private void currentMicrophone_BufferReady(object sender, EventArgs e)
    {
        _microphone.GetData(_audioBuffer);
        _recordingStream.Write(_audioBuffer, 0, _audioBuffer.Length);
        if (!StopRequested)
          return;

        DeleteMicrophone();
    }

    public void PlaySound()
    {
      Sound.Play();
    }

    public void StopSound()
    {
      if (_sound != null)
      {
        _sound.Stop();
      }
    }

    public MemoryStream GetSoundAsStream()
    {
      return _recordingStream;
    }

  }

  public class RecorderDispatcher : IApplicationService
  {
    private DispatcherTimer _frameworkDispatcherTimer;
    public RecorderDispatcher(TimeSpan dispatchInterval)
    {
      FrameworkDispatcher.Update();
      this._frameworkDispatcherTimer = new DispatcherTimer();
      this._frameworkDispatcherTimer.Tick += new EventHandler(frameworkDispatcherTimer_Tick);
      this._frameworkDispatcherTimer.Interval = dispatchInterval;
    }
    void IApplicationService.StartService(ApplicationServiceContext context)
    {
      this._frameworkDispatcherTimer.Start();
    }

    void IApplicationService.StopService()
    {
      this._frameworkDispatcherTimer.Stop();
    }

    void frameworkDispatcherTimer_Tick(object sender, EventArgs e)
    {
      FrameworkDispatcher.Update();
    }
  }

}
