using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.Models
{
    public class RaceDBDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CarStatDTO> CarStat { get; set; }
        public RaceDBDTO()
        {
            CarStat = new List<CarStatDTO>();
        }
    }
}
