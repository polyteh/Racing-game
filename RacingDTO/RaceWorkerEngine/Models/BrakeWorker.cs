using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.RaceWorkerEngine.Models
{
    public class BrakeWorker
    {
        public int Id { get; set; }
        public int EffecientKoef { get; set; }
        public double NormilizedEffecientKoef { get; set; }
        ICollection<RacingCarWorker> RacingCar { get; set; }

    }
}
