using System;
using Audio = Microsoft.Xna.Framework.Audio;

namespace Catrobat.IDE.Phone.Services
{
    /// <summary>
    /// Adaption of <see cref="Microsoft.Xna.Framework.Audio.Microphone"/> sensor to behave like the ones in <see cref="Microsoft.Devices.Sensors"/>. 
    /// </summary>
    class Microphone2
    {
        private readonly Audio.Microphone _microphone = Audio.Microphone.Default;
        private MicrophoneReading _value;

        public static bool IsSupported
        {
            get { return Audio.Microphone.Default != null; }
        }

        public Microphone2()
        {
            _value.Raw = new byte[_microphone.GetSampleSizeInBytes(_microphone.BufferDuration)];
            _microphone.BufferReady += (object sender, EventArgs args) =>
            {
                _microphone.GetData(_value.Raw);
                _value.Timestamp = DateTime.Now;
            };
        }

        public void Start()
        {
            _microphone.Start();
        }

        public void Stop()
        {
            _microphone.Stop();
        }

        public MicrophoneReading CurrentValue
        {
            get
            {
                return _value;
            }
        }

        public bool IsDataValid
        {
            get
            {
                // TODO: inspect timestamp
                return _value.Raw != null;
            }
        }
    }
}
