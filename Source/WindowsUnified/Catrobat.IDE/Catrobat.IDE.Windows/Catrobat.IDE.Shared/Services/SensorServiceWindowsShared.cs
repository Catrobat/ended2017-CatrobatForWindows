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

        private bool _accelerometerEnabled;
        private bool _compassEnabled;
        private bool _inclinometerEnabled;

        private bool _accelerometerChecked;
        private bool _compassChecked;
        private bool _inclinometerChecked;

        //private readonly Microphone2 _microphone = new Microphone2();
        private uint _reportIntervall = 200;

        public event SensorReadingChangedEventHandler SensorReadingChanged;

        public SensorServiceWindowsShared()
        {
            _accelerometerEnabled = true;
            _compassEnabled = true;
            _inclinometerEnabled = true;
        }

        public bool CheckSensors()
        {
            bool allSensorsWorking = true;
            string returnValue = "The following sensors are not working correct: ";

            _accelerometerChecked = false;
            _compassChecked = false;
            _inclinometerChecked = false;

            uint reportIntervall = _reportIntervall;
            _reportIntervall = 1;

            CheckAccelaration();
            CheckCompass();
            CheckInclinometer();

            _reportIntervall = reportIntervall;

            return _accelerometerEnabled && _compassEnabled && _inclinometerEnabled;
        }

        private void CheckAccelaration()
        {
            _accelerometerChecked = false;

            if (_accelerometer != null)
            {
                uint reportInterval = _reportIntervall;

                if (reportInterval < _accelerometer.MinimumReportInterval)
                {
                    reportInterval = _accelerometer.MinimumReportInterval;
                }

                _accelerometer.ReportInterval = reportInterval;
                _accelerometer.ReadingChanged += _accelerometer_ReadingChanged;

                while (!_accelerometerChecked) ;

                _accelerometer.ReadingChanged -= _accelerometer_ReadingChanged;
            }
            else
            {
                _accelerometerEnabled = false;
            }
        }

        private void CheckCompass()
        {
            _compassChecked = false;

            if (_compass != null)
            {
                uint reportInterval = _reportIntervall;

                if (reportInterval < _compass.MinimumReportInterval)
                {
                    reportInterval = _compass.MinimumReportInterval;
                }

                _compass.ReportInterval = reportInterval;
                _compass.ReadingChanged += _compass_ReadingChanged;

                while(!_compassChecked);

                _compass.ReadingChanged -= _compass_ReadingChanged;
            }
            else
            {
                _compassEnabled = false;
            }
        }

        private void CheckInclinometer()
        {
            _inclinometerChecked = false;

            if (_inclinometer != null)
            {
                uint reportInterval = _reportIntervall;

                if (reportInterval < _inclinometer.MinimumReportInterval)
                {
                    reportInterval = _inclinometer.MinimumReportInterval;
                }

                _inclinometer.ReportInterval = reportInterval;
                _inclinometer.ReadingChanged += _inclinometer_ReadingChanged;

                while (!_inclinometerChecked) ;

                _inclinometer.ReadingChanged -= _inclinometer_ReadingChanged;
            }
            else
            {
                _inclinometerEnabled = false;
            }
        }

        public bool IsAccelarationEnabled()
        {
            return _accelerometerEnabled;
        }

        public bool IsCompassEnabled()
        {
            return _compassEnabled;
        }

        public bool IsInclinationEnabled()
        {
            return _inclinometerEnabled;
        }

        public void Start()
        {
            if (_compass != null && _compassEnabled)
            {
                uint reportInterval = _reportIntervall;

                if (reportInterval < _compass.MinimumReportInterval)
                {
                    reportInterval = _compass.MinimumReportInterval;
                }

                _compass.ReportInterval = reportInterval;
                _compass.ReadingChanged += _compass_ReadingChanged;
            }

            if (_accelerometer != null && _accelerometerEnabled)
            {
                uint reportInterval = _reportIntervall;

                if (reportInterval < _accelerometer.MinimumReportInterval)
                {
                    reportInterval = _accelerometer.MinimumReportInterval;
                }

                _accelerometer.ReportInterval = reportInterval;
                _accelerometer.ReadingChanged += _accelerometer_ReadingChanged;
            }

            if (_inclinometer != null && _inclinometerEnabled)
            {
                uint reportInterval = _reportIntervall;

                if (reportInterval < _inclinometer.MinimumReportInterval)
                {
                    reportInterval = _inclinometer.MinimumReportInterval;
                }

                _inclinometer.ReportInterval = reportInterval;
                _inclinometer.ReadingChanged += _inclinometer_ReadingChanged;
            }
        }

        public void Stop()
        {
            if (_compass != null)
            {
                _compass.ReadingChanged -= _compass_ReadingChanged;
            }

            if (_accelerometer != null)
            {
                _accelerometer.ReadingChanged -= _accelerometer_ReadingChanged;
            }

            if (_inclinometer != null)
            {
                _inclinometer.ReadingChanged -= _inclinometer_ReadingChanged;
            }
        }

        public double GetAccelerationX()
        {
            if (_accelerometer != null)
            {
                AccelerometerReading reading = _accelerometer.GetCurrentReading();

                if (reading != null)
                {
                    return reading.AccelerationX;
                }
            }

            return 0.0;
        }

        public double GetAccelerationY()
        {
            if (_accelerometer != null)
            {
                AccelerometerReading reading = _accelerometer.GetCurrentReading();

                if (reading != null)
                {
                    return reading.AccelerationY;
                }
            }

            return 0.0;
        }

        public double GetAccelerationZ()
        {
            if (_accelerometer != null)
            {
                AccelerometerReading reading = _accelerometer.GetCurrentReading();

                if (reading != null)
                {
                    return reading.AccelerationZ;
                }
            }

            return 0.0;
        }

        public double GetCompass()
        {
            if (_compass != null)
            {
                CompassReading reading = _compass.GetCurrentReading();

                if (reading != null)
                {
                    //string magneticNorth = String.Format("{0,5:0.00}", reading.HeadingMagneticNorth);
                    //if (reading.HeadingTrueNorth != null)
                    //{
                    //    string trueNorth = String.Format("{0,5:0.00}", reading.HeadingTrueNorth);
                    //}
                    return reading.HeadingMagneticNorth;
                }
            }

            return 0.0;
        }

        public double GetInclinationX()
        {
            if (_inclinometer != null)
            {
                InclinometerReading reading = _inclinometer.GetCurrentReading();

                if (reading != null)
                {
                    return reading.PitchDegrees;
                }
            }

            return 0.0;
        }

        public double GetInclinationY()
        {
            if (_inclinometer != null)
            {
                InclinometerReading reading = _inclinometer.GetCurrentReading();

                if (reading != null)
                {
                    return reading.RollDegrees;
                }
            }

            return 0.0;
        }

        public double GetInclinationZ()
        {
            if (_inclinometer != null)
            {
                InclinometerReading reading = _inclinometer.GetCurrentReading();

                if (reading != null)
                {
                    return reading.YawDegrees;
                }
            }

            return 0.0;
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
            try
            {
                CompassReading reading = args.Reading;

                if (reading != null)
                {
                    SensorEventArgs sensorArgs = new SensorEventArgs(reading.HeadingMagneticNorth);

                    if (SensorReadingChanged != null)
                    {
                        SensorReadingChanged(sender, sensorArgs);
                    }
                }
            }
            catch(Exception)
            {
                _compassEnabled = false;
            }

            _compassChecked = true;
        }

        void _accelerometer_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            try
            {
                AccelerometerReading reading = args.Reading;

                if (reading != null)
                {
                    SensorEventArgs sensorArgs = new SensorEventArgs(reading.AccelerationX);

                    if (SensorReadingChanged != null)
                    {
                        SensorReadingChanged(sender, sensorArgs);
                    }
                }
            }
            catch(Exception ex)
            {
                _accelerometerEnabled = false;
            }

            _accelerometerChecked = true;
        }

        void _inclinometer_ReadingChanged(Inclinometer sender, InclinometerReadingChangedEventArgs args)
        {
            try
            {
                InclinometerReading reading = args.Reading;

                if (reading != null)
                {
                    SensorEventArgs sensorArgs = new SensorEventArgs(reading.RollDegrees);

                    if (SensorReadingChanged != null)
                    {
                        SensorReadingChanged(sender, sensorArgs);
                    }
                }
            }
            catch(Exception)
            {
                _inclinometerEnabled = false;
            }

            _inclinometerChecked = true;
        }

        #endregion
    }
}
