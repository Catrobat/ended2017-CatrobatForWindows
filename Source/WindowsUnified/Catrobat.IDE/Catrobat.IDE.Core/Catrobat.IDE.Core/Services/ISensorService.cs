using Catrobat.IDE.Core.Utilities;

namespace Catrobat.IDE.Core.Services
{
    public delegate void CompassReadingChangedEventHandler(object sender, SensorEventArgs e);

    public interface ISensorService
    {
        event CompassReadingChangedEventHandler CompassReadingChanged;
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
