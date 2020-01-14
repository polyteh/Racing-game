﻿using RacingDTO.RaceWorkerEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.RaceWorkerEngine.Interfaces
{
    public interface IRaceWorker
    {
        void StartRace(RaceWorker newRace);
        void StopRace();
    }
}