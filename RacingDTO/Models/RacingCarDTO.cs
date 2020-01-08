using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.Models
{
    public class RacingCarDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrakeId { get; set; }
        public BrakeDTO Brake { get; set; }
        public int EngineId { get; set; }
        public EngineDTO Engine { get; set; }
        public int SuspentionId { get; set; }
        public SuspentionDTO Suspention { get; set; }
    }
}
