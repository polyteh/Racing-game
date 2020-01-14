using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.RaceWorkerEngine.Models
{
    //TODO: implement as behaviour of the racer (as a part of RacingCar classes)
    public static class CarBehaviourWorker
    {
        private static readonly int _accelerationInfl = 2;
        private static readonly int _holdOnSpeedInfl = 3;
        private static readonly int _brakingInfl = 1;
        public static int Acceleration => _accelerationInfl;
        public static int HoldOnSpeed => _holdOnSpeedInfl;
        public static int Braking => _brakingInfl;
        public static int TotalActionWeight => _accelerationInfl + _holdOnSpeedInfl + _brakingInfl;

    }
}
