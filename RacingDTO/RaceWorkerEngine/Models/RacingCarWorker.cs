using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.RaceWorkerEngine.Models
{
    public class RacingCarWorker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrakeId { get; set; }
        public BrakeWorker Brake { get; set; }
        public int EngineId { get; set; }
        public EngineWorker Engine { get; set; }
        public int SuspentionId { get; set; }
        public SuspentionWorker Suspention { get; set; }

        private int _curDistanceCovered = 0;
        private int _curEngineTemp = 30;
        private readonly RaceConfiguration _curRaceConfiguration;
        private bool hasOverheatingPenalty = false;
        private int curBreakingFromOverheatingNumber=0;
        private static readonly int _tempTresholdLimit = 5;
        private static readonly int _tempIncreaseAccel = 3;
        private static readonly int _tempIncreaseHold = 1;
        private static readonly int _tempDecreaseBrake = -2;
        private static readonly int MaxBreakingFromOverheatingNumber = 5;


        public RacingCarWorker(RaceConfiguration raceConfiguration)
        {
            _curRaceConfiguration = raceConfiguration;
        }
        private void Accelerate()
        {
            this._curEngineTemp += _tempIncreaseAccel;
        }
        private void HoldSpeed()
        {
            this._curEngineTemp += _tempIncreaseHold;
        }
        private void Braking()
        {
            this._curEngineTemp += _tempDecreaseBrake;
        }
        private bool isFinished()
        {
            if (_curRaceConfiguration.TracDistance > _curDistanceCovered)
            {
                return false;
            }
            return true;
        }
        private bool isEngineNearOverheated()
        {
            return false;
        }
        public void Move()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            int actionNumber = random.Next(1, CarBehaviourWorker.TotalActionWeight+1);
            if (actionNumber<=CarBehaviourWorker.Acceleration)
            {
                Accelerate();
            }
            else if ((actionNumber > CarBehaviourWorker.Acceleration)&&(actionNumber<=(CarBehaviourWorker.TotalActionWeight-CarBehaviourWorker.Braking)))
            {
                HoldSpeed();
            }
            else
            {
                Braking();
            }
        }

    }
}
