using System;

namespace Catrobat.IDE.Core.Utilities
{
    public class SensorEventArgs : EventArgs
    {
        private double _value;

        public double Value { get { return _value; } }

        public SensorEventArgs(double sensorReading)
        {
            _value = sensorReading;
        }
    }
}