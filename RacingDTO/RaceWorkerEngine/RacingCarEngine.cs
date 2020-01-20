using RacingDTO.RaceWorkerEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.RaceWorkerEngine
{
    public class RacingCarEngine:IRacingCarWorker
    {
        private double _curDistanceCovered;
        private int _curEngineTemp = 30;
        private RaceConfiguration _curRaceConfiguration;
        private bool _hasOverheatingPenalty = false;

        public void GetRaceConfiguration(RaceConfiguration raceConfiguration)
        {
            throw new NotImplementedException();
        }

        public void Move()
        {
            throw new NotImplementedException();
        }
    }
}
