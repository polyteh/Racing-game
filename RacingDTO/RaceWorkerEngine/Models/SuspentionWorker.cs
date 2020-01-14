using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.RaceWorkerEngine.Models
{
    public class SuspentionWorker
    {
        public int Id { get; set; }

        public int RigidityKoef { get; set; }
        public double NormilizedRigidityKoef { get; set; }

        ICollection<RacingCarWorker> RacingCar { get; set; }
    }
}
