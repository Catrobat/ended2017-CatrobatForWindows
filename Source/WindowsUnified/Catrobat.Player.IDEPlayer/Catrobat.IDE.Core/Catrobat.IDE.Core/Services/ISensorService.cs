using Catrobat.IDE.Core.Utilities;

namespace Catrobat.IDE.Core.Services
{
    public delegate void SensorReadingChangedEventHandler(object sender, SensorEventArgs e);

    public interface ISensorService
    {
        event SensorReadingChangedEventHandler SensorReadingChanged;
        void Start();
        void Stop();

        double GetAccelerationX();
        double GetAccelerationY();
        double GetAccelerationZ();
        double GetCompass();
        double GetInclinationX();
        double GetInclinationY();
        double GetInclinationZ();
        double GetLoudness();
    }
}
