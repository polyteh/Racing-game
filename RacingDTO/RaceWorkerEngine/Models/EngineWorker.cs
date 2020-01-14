using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.RaceWorkerEngine.Models
{
    public class EngineWorker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public double NormilizedHP { get; set; }
        public bool Turbine { get; set; }
        ICollection<RacingCarWorker> RacingCar { get; set; }
    }
}
