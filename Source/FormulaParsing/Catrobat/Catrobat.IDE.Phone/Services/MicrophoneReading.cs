using System;

namespace Catrobat.IDE.Phone.Services
{
    /// <see cref="Microphone"/>
    struct MicrophoneReading
    {
        public byte[] Raw;

        public DateTime Timestamp;

        public double Loudness
        {
            get
            {
                // see http://developer.nokia.com/community/wiki/How_to_access_and_manage_the_Microphone_raw_data_in_WP
                throw new NotImplementedException();
            }
        }
    }
}
