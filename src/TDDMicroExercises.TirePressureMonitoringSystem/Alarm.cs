using System;

namespace TDDMicroExercises.TirePressureMonitoringSystem
{
    public class Alarm
    {
        private const double DefaultLowPressureThreshold = 17;
        private const double DefaultHighPressureThreshold = 21;
        private readonly double _lowPressureThreshold;
        private readonly double _highPressureThreshold;
        private readonly ISensor _sensor;

        public Alarm() : this(new Sensor())
        { }

        public Alarm(
            ISensor sensor,
            double lowPressureThreshold = DefaultLowPressureThreshold,
            double highPressureThreshold = DefaultHighPressureThreshold)
        {
            if (lowPressureThreshold < 0 || highPressureThreshold < 0)
            {
                throw new ArgumentException($"Invalid parameters: {nameof(lowPressureThreshold)} and {nameof(highPressureThreshold)} must be non-negative");
            }

            if (lowPressureThreshold > highPressureThreshold)
            {
                throw new ArgumentException($"Invalid parameters: {nameof(lowPressureThreshold)} cannot be higher than {nameof(highPressureThreshold)}");
            }

            _sensor = sensor;
            _lowPressureThreshold = lowPressureThreshold;
            _highPressureThreshold = highPressureThreshold;
        }

        public void Check()
        {
            double psiPressureValue = _sensor.PopNextPressurePsiValue();

            if (psiPressureValue < _lowPressureThreshold || _highPressureThreshold < psiPressureValue)
            {
                AlarmOn = true;
            }
        }

        public void Reset()
        {
            AlarmOn = false;
        }

        public bool AlarmOn { get; private set; }

    }
}
