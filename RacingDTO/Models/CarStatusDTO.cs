using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.Models
{
    public class CarStatusDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double DistanceCovered { get; set; }
        public List<string> StatusMessage { get; set; }
        public TimeSpan? TimeInTheRace { get; set; }
        public bool IsFinished { get; set; }
        public int? Place { get; set; }
    }
}
