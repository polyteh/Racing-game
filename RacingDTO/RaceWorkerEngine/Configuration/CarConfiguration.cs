using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.RaceWorkerEngine
{
    public static class CarConfiguration
    {
        //maximum engine temperature
        private static readonly int _maxEngineTemperature=80;
        //threshold limit for engine temperature check
        private static readonly int _tempTresholdLimit = 5;
        //temperature incriasing during acceleartion
        private static readonly int _tempIncreaseAccel = 10;
        private static readonly int _tempIncreaseAccelTurbo = 13;
        private static readonly int _tempIncreaseHold = 5;
        private static readonly int _tempDecreaseBrake = -3;
        private static readonly int _maxBrakingAfterOverheating = 5;
        private static readonly int _maxAccelerateAfterSpeedUp = 3;

        public static int MaxEngineTemperature => _maxEngineTemperature;
        public static int TemperatureTresholdLimit => _tempTresholdLimit;
        public static int TemperatureIncreaseAccel => _tempIncreaseAccel;
        public static int TemperatureIncreaseTurbo => _tempIncreaseAccelTurbo;
        public static int TemperatureIncreaseHoldSpeed => _tempIncreaseHold;
        public static int TemperatureIncreaseBrake => _tempDecreaseBrake;
        public static int MaxBrakingAfterOverheating => _maxBrakingAfterOverheating;
        public static int MaxAccelerateAfterSpeedUp => _maxAccelerateAfterSpeedUp;

    }
}
