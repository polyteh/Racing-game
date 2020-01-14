using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.RaceWorkerEngine.Models
{
    public class RaceWorker
    {
        public int Id { get; set; }
        public List<RacingCarWorker> CarList { get; set; }
    }
}
