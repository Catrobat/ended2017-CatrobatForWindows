using System;
using System.Diagnostics;
using Windows.Devices.Sensors;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Media.MediaProperties;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class SensorServiceWindowsShared : ISensorService
    {
        private readonly Accelerometer _accelerometer = Accelerometer.GetDefault();
        private readonly Compass _compass = Compass.GetDefault();
        private readonly Inclinometer _inclinometer = Inclinometer.GetDefault();
        private readonly MediaCapture _mediacapture = new MediaCapture();
        //private readonly Microphone _microphone = Microphone.Default; 
        private StorageFile _storageFile;

        private bool _accelerometerEnabled;
        private bool _compassEnabled;
        private bool _inclinometerEnabled;
        private bool _microphoneEnabled;

        private bool _accelerometerChecked;
        private bool _compassChecked;
        private bool _inclinometerChecked;
        private bool _microphoneChecked;

        private double _loudnessValue;

        private uint _reportIntervall = 200;

        public event SensorReadingChangedEventHandler SensorReadingChanged;

        public SensorServiceWindowsShared()
        {
            _accelerometerEnabled = true;
            _compassEnabled = true;
            _inclinometerEnabled = true;
            _microphoneEnabled = true;
            _loudnessValue = 0;

            this.InitializeAudioRecording();
        }

        public async void InitializeAudioRecording()
        {
            var settings = new MediaCaptureInitializationSettings();
            settings.StreamingCaptureMode = StreamingCaptureMode.Audio;
            settings.MediaCategory = MediaCategory.Other;
            settings.AudioProcessing = Windows.Media.AudioProcessing.Default;

            await _mediacapture.InitializeAsync(settings);

            //_mediacapture.RecordLimitationExceeded += new RecordLimitationExceededEventHandler(RecordLimitationExceeded);
        }

        public bool CheckSensors()
        {
            bool allSensorsWorking = true;

            _accelerometerChecked = false;
            _compassChecked = false;
            _inclinometerChecked = false;
            _microphoneChecked = false;

            uint reportIntervall = _reportIntervall;
            _reportIntervall = 1;

            CheckAccelaration();
            CheckCompass();
            CheckInclinometer();
            //CheckMicrophone();

            _reportIntervall = reportIntervall;

            return _accelerometerEnabled && _compassEnabled && _inclinometerEnabled && _microphoneEnabled;
        }

        private void CheckMicrophone()
        {
            //_microphoneChecked = false;

            //if(_microphone != null)
            //{
            //    try
            //    {
            //        _microphone.Start();
            //        _microphone.Stop();
            //    }
            //    catch(Exception)
            //    {
            //        _microphoneEnabled = false;
            //    }
            //}
            //else
            //{
            //    _microphoneEnabled = false;
            //}
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

                while (!_compassChecked) ;

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

        public bool IsMicrophoneEnabled()
        {
            return _microphoneEnabled;
        }

        public async void Start()
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

            if (_mediacapture != null)
            {
                _storageFile = await KnownFolders.VideosLibrary.CreateFileAsync("test", CreationCollisionOption.GenerateUniqueName);
                MediaEncodingProfile profile = MediaEncodingProfile.CreateM4a(AudioEncodingQuality.Auto);

                await _mediacapture.StartRecordToStorageFileAsync(profile, _storageFile);
            }

            //if(_microphone != null && _microphoneEnabled)
            //{
            //    _microphone.Start();
            //}
        }

        public async void Stop()
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

            if (_mediacapture != null)
            {
                try
                {
                    await _mediacapture.StopRecordAsync();
                    var properties = _storageFile.Properties;

                }
                catch (Exception ex)
                {
                    // TODO: handle exception
                }
            }

            //if(_microphone != null)
            //{
            //    _microphone.Stop();
            //}
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
            return _loudnessValue;
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
            catch (Exception)
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
            catch (Exception ex)
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
            catch (Exception)
            {
                _inclinometerEnabled = false;
            }

            _inclinometerChecked = true;
        }

        #endregion
    }
}
