using System;
using System.Diagnostics;
using Windows.Devices.Sensors;
using Catrobat.IDE.Core.Services;


namespace Catrobat.IDE.WindowsShared.Services
{
    public class SensorServiceWindowsShared : ISensorService
    {
        //private readonly Accelerometer _accelerometer = new Accelerometer();
        //private readonly Compass _compass = new Compass();
        //private readonly Motion _motion = new Motion();
        //private readonly Microphone2 _microphone = new Microphone2();

        public void Start()
        {
            //if (Accelerometer.IsSupported)
            //{
            //    try { _accelerometer.Start(); }
            //    catch (Exception) { Debugger.Break(); }
            //}
            //if (Compass.IsSupported)
            //{
            //    try { _compass.Start(); }
            //    catch (Exception) { Debugger.Break(); }
            //}
            //if (Motion.IsSupported)
            //{
            //    try { _motion.Start(); }
            //    catch (Exception) { Debugger.Break(); }
            //}
            //try { _microphone.Start(); }
            //catch (Exception) { Debugger.Break(); }
        }

        public void Stop()
        {
            //_accelerometer.Stop();
            //_compass.Stop();
            //_motion.Stop();
            //_microphone.Stop();
        }

        public double GetAccelerationX()
        {
            return 0.0;
            //return _accelerometer.IsDataValid ? _accelerometer.CurrentValue.Acceleration.X : 0;
        }

        public double GetAccelerationY()
        {
            return 0.0;
            //return _accelerometer.IsDataValid ? _accelerometer.CurrentValue.Acceleration.Y : 0;
        }

        public double GetAccelerationZ()
        {
            return 0.0;
            //return _accelerometer.IsDataValid ? _accelerometer.CurrentValue.Acceleration.Z : 0;
        }

        public double GetCompass()
        {
            return 0.0;
            //return _compass.IsDataValid ? _compass.CurrentValue.MagneticHeading : 0;
        }

        public double GetInclinationX()
        {
            return 0.0;
            //return _motion.IsDataValid ? -MathHelper.ToDegrees(_motion.CurrentValue.Attitude.Roll) : 0;
        }

        public double GetInclinationY()
        {
            return 0.0;
            //return _motion.IsDataValid ? MathHelper.ToDegrees(_motion.CurrentValue.Attitude.Pitch) : 0;
        }

        public double GetLoudness()
        {
            return 0.0;
            //return _microphone.IsDataValid ? _microphone.CurrentValue.Loudness : 0;
        }
    }
}
