using Catrobat.IDE.Core.Utilities;

namespace Catrobat.IDE.Core.Services
{
    public delegate void SensorReadingChangedEventHandler(object sender, SensorEventArgs e);

    public interface ISensorService
    {
        event SensorReadingChangedEventHandler SensorReadingChanged;

        bool CheckSensors();

        void Start();
        void Stop();

        bool IsAccelarationEnabled();
        bool IsCompassEnabled();
        bool IsInclinationEnabled();

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
