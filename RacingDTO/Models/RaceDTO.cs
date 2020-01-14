using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.Models
{
    public class RaceDTO
    {
        public int Id { get; set; }
        public List<RacingCarDTO> CarList { get; set; }
    }
}
