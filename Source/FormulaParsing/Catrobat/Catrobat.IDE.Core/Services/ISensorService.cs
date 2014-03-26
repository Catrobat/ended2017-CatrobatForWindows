namespace Catrobat.IDE.Core.Services
{
    public interface ISensorService
    {
        void Start();
        void Stop();

        double GetAccelerationX();
        double GetAccelerationY();
        double GetAccelerationZ();
        double GetCompass();
        double GetInclinationX();
        double GetInclinationY();
    }
}
