using RacingDTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.Interfaces
{
    public interface IRaceService
    {
        void InitRace(RaceDTO newRace);
        Task StartRace(RaceDTO newRace);

        List<CarStatusDTO> GetRaceStatus();
        bool IsRunning();
        void PauseRace();
        void ResumeRace();
    }
}
