using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.RaceWorkerEngine.Interfaces
{
    public interface IRacingCarWorker
    {
        void Move();
        void GetRaceConfiguration(RaceConfiguration raceConfiguration);

    }
}
