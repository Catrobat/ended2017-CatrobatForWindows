using System;
using System.Diagnostics;
using Windows.Devices.Sensors;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities;


namespace Catrobat.IDE.WindowsShared.Services
{
    public class SensorServiceWindowsShared : ISensorService
    {
        private readonly Accelerometer _accelerometer = Accelerometer.GetDefault();
        private readonly Compass _compass = Compass.GetDefault();
        private readonly Inclinometer _inclinometer = Inclinometer.GetDefault();

        //private readonly Microphone2 _microphone = new Microphone2();
        private uint _reportIntervall = 200;

        public event SensorReadingChangedEventHandler SensorReadingChanged;
        
        public void Start()
        {
            //try { _microphone.Start(); }
            //catch (Exception) { Debugger.Break(); }

            if (_compass != null)
            {
                uint reportInterval = _reportIntervall;
                if (reportInterval < _compass.MinimumReportInterval)
                {
                    reportInterval = _compass.MinimumReportInterval; 
                }
                _compass.ReportInterval = reportInterval;
                _compass.ReadingChanged += _compass_ReadingChanged;
            }
            else
            {
                // sensor is not supported by the device
            }

            if (_accelerometer != null)
            {
                uint reportInterval = _reportIntervall;
                if (reportInterval < _accelerometer.MinimumReportInterval)
                {
                    reportInterval = _accelerometer.MinimumReportInterval;
                }
                _accelerometer.ReportInterval = reportInterval;
                _accelerometer.ReadingChanged += _accelerometer_ReadingChanged;
            }
            else
            {
                // sensor is not supported by the device
            }

            if (_inclinometer != null)
            {
                uint reportInterval = _reportIntervall;
                if (reportInterval < _inclinometer.MinimumReportInterval)
                {
                    reportInterval = _inclinometer.MinimumReportInterval;
                }
                _inclinometer.ReportInterval = reportInterval;
                _inclinometer.ReadingChanged += _inclinometer_ReadingChanged;
            }
            else
            {
                // sensor is not supported by the device
            }
        }

        public void Stop()
        {
            _compass.ReadingChanged -= _compass_ReadingChanged;
            _accelerometer.ReadingChanged -= _accelerometer_ReadingChanged;
            _inclinometer.ReadingChanged -= _inclinometer_ReadingChanged;
        }

        public double GetAccelerationX()
        {
            AccelerometerReading reading = _accelerometer.GetCurrentReading();
            if(reading != null)
            {
                return reading.AccelerationX;
            }
            else
            {
                return 0.0;
            }
        }

        public double GetAccelerationY()
        {
            AccelerometerReading reading = _accelerometer.GetCurrentReading();
            if (reading != null)
            {
                return reading.AccelerationY;
            }
            else
            {
                return 0.0;
            }
        }

        public double GetAccelerationZ()
        {
            AccelerometerReading reading = _accelerometer.GetCurrentReading();
            if (reading != null)
            {
                return reading.AccelerationZ;
            }
            else
            {
                return 0.0;
            }
        }

        public double GetCompass()
        {
            CompassReading reading = _compass.GetCurrentReading();
            if(reading != null)
            {
                //string magneticNorth = String.Format("{0,5:0.00}", reading.HeadingMagneticNorth);
                //if (reading.HeadingTrueNorth != null)
                //{
                //    string trueNorth = String.Format("{0,5:0.00}", reading.HeadingTrueNorth);
                //}
                return reading.HeadingMagneticNorth;
            }
            else
            {
                return 0.0;
            }
        }

        public double GetInclinationX()
        {
            InclinometerReading reading = _inclinometer.GetCurrentReading();
            if (reading != null)
            {
                return reading.PitchDegrees;
            }
            else
            {
                return 0.0;
            }
            //return _motion.IsDataValid ? -MathHelper.ToDegrees(_motion.CurrentValue.Attitude.Roll) : 0;
        }

        public double GetInclinationY()
        {
            InclinometerReading reading = _inclinometer.GetCurrentReading();
            if (reading != null)
            {
                return reading.RollDegrees;
            }
            else
            {
                return 0.0;
            }
            //return _motion.IsDataValid ? MathHelper.ToDegrees(_motion.CurrentValue.Attitude.Pitch) : 0;
        }

         public double GetInclinationZ()
        {
            InclinometerReading reading = _inclinometer.GetCurrentReading();
            if (reading != null)
            {
                return reading.YawDegrees;
            }
            else
            {
                return 0.0;
            }
        }

        public double GetLoudness()
        {
            return 0.0;
        }


        #region Events
        // the values in the sensorArgs are currently not used by the FormulaEditorViewModel,
        // because the FormulaEvaluator uses the Get-Functions to get the current readings
        void _compass_ReadingChanged(Compass sender, CompassReadingChangedEventArgs args)
        {
            CompassReading reading = args.Reading;
            if(reading != null)
            {
                SensorEventArgs sensorArgs = new SensorEventArgs(reading.HeadingMagneticNorth);
                SensorReadingChanged(sender, sensorArgs);
            }
        }

        void _accelerometer_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            AccelerometerReading reading = args.Reading;
            if (reading != null)
            {
                SensorEventArgs sensorArgs = new SensorEventArgs(reading.AccelerationX);
                SensorReadingChanged(sender, sensorArgs);
            }
        }

        void _inclinometer_ReadingChanged(Inclinometer sender, InclinometerReadingChangedEventArgs args)
        {
            InclinometerReading reading = args.Reading;
            if (reading != null)
            {
                SensorEventArgs sensorArgs = new SensorEventArgs(reading.RollDegrees);
                SensorReadingChanged(sender, sensorArgs);
            }
        }
        #endregion
    }
}
