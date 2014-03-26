using System;
using System.Diagnostics;
using Catrobat.IDE.Core.Services;
using Microsoft.Devices.Sensors;
using Catrobat.IDE.Core.ExtensionMethods;

namespace Catrobat.IDE.Phone.Services
{
    public class SensorServicePhone : ISensorService
    {
        private readonly Accelerometer _accelerometer = new Accelerometer();
        private readonly Compass _compass = new Compass();
        private readonly Motion _motion = new Motion();

        public void Start()
        {
            if (Accelerometer.IsSupported)
            {
                try { _accelerometer.Start(); }
                catch (Exception) { Debugger.Break(); }
            }
            if (Compass.IsSupported)
            {
                try { _compass.Start(); }
                catch (Exception) { Debugger.Break(); }
            }
            if (Motion.IsSupported)
            {
                try { _motion.Start(); }
                catch (Exception) { Debugger.Break(); }
            }
        }

        public void Stop()
        {
            _accelerometer.Stop();
            _compass.Stop();
            _motion.Stop();
        }

        public double GetAccelerationX()
        {
            return _accelerometer.IsDataValid ? _accelerometer.CurrentValue.Acceleration.X : 0;
        }

        public double GetAccelerationY()
        {
            return _accelerometer.IsDataValid ? _accelerometer.CurrentValue.Acceleration.Y : 0;
        }

        public double GetAccelerationZ()
        {
            return _accelerometer.IsDataValid ? _accelerometer.CurrentValue.Acceleration.Z : 0;
        }

        public double GetCompass()
        {
            return _compass.IsDataValid ? _compass.CurrentValue.MagneticHeading : 0;
        }

        public double GetInclinationX()
        {
            return _motion.IsDataValid ? _motion.CurrentValue.Attitude.Roll : 0;
        }

        public double GetInclinationY()
        {
            return _motion.IsDataValid ? _motion.CurrentValue.Attitude.Pitch : 0;
        }
    }
}
