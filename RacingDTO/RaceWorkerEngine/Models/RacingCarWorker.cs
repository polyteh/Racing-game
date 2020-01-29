using RacingDTO.RaceWorkerEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RacingDTO.RaceWorkerEngine.Models
{
    public class RacingCarWorker : IRacingCarWorker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrakeId { get; set; }
        public BrakeWorker Brake { get; set; }
        public int EngineId { get; set; }
        public EngineWorker Engine { get; set; }
        public int SuspentionId { get; set; }
        public SuspentionWorker Suspention { get; set; }
        private RaceConfiguration _curRaceConfiguration;
        public bool IsFinished { get; set; }
        IRacingCarEngine _racingCarEngine;
        public void Move(RaceConfiguration raceConfiguration)
        {
            _curRaceConfiguration = raceConfiguration;
            _racingCarEngine = new RacingCarEngine(this, _curRaceConfiguration);
            _racingCarEngine.Move();
        }
        public CarStatusWorker GetStatus()
        {
            return _racingCarEngine.GetStatus();
        }

        public void Pause()
        {
            _racingCarEngine.Pause();
        }
        public void Resume()
        {
            _racingCarEngine.Resume();
        }

        public bool IsInTheRace()
        {
            return _racingCarEngine.IsInTheRace(); 
        }
    }
}
