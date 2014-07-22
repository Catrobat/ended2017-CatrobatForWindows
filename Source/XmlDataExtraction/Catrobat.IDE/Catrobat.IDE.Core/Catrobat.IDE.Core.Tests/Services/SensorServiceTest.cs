using System;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Core.Tests.Services
{
    public class SensorServiceTest : ISensorService
    {
        private bool _isRunning = false;
        private readonly Random _random = new Random();

        public void Start()
        {
            _isRunning = true;
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public double GetAccelerationX()
        {
            return _isRunning ? _random.NextDouble(-1, 1) : 0;
        }

        public double GetAccelerationY()
        {
            return _isRunning ? _random.NextDouble(-1, 1) : 0;
        }

        public double GetAccelerationZ()
        {
            return _isRunning ? _random.NextDouble(-1, 1) : 0;
        }

        public double GetCompass()
        {
            return _isRunning ? _random.NextDouble(0, 360) : 0;
        }

        public double GetInclinationX()
        {
            if (_isRunning) throw new NotImplementedException();
            return _isRunning ? 0 : 0;
        }

        public double GetInclinationY()
        {
            if (_isRunning) throw new NotImplementedException();
            return _isRunning ? 0 : 0;
        }

        public double GetLoudness()
        {
            if (_isRunning) throw new NotImplementedException();
            return _isRunning ? 0 : 0;
        }
    }
}
