using System;

namespace Catrobat.IDE.Phone.Services
{
    /// <see cref="Microphone2"/>
    struct MicrophoneReading
    {
        public byte[] Raw;

        public DateTime Timestamp;

        public double Loudness
        {
            get
            {
                // RMS Method            
                // see http://developer.nokia.com/community/wiki/How_to_access_and_manage_the_Microphone_raw_data_in_WP

                double rms = 0;            
                ushort byte1 = 0;            
                ushort byte2 = 0;            
                short value = 0;            
                int volume = 0;            
                rms = (short)(byte1 | (byte2 << 8));             
                
                for (int i = 0; i < Raw.Length - 1; i += 2)            
                {                
                    byte1 = Raw[i];                
                    byte2 = Raw[i + 1];                 
                    value = (short)(byte1 | (byte2 << 8));                
                    rms += Math.Pow(value, 2);            
                }
            
                rms /= (double)(Raw.Length / 2);            
                volume = (int)Math.Floor(Math.Sqrt(rms));

                return volume;
            }
        }
    }
}
