using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.Models
{
    //class for DB communication
    public class CarStatDTO
    {
        public int Id { get; set; }
        public int RacingCarId { get; set; }
        public RacingCarDTO RacingCar { get; set; }
        public int RaceId { get; set; }
        public RaceDBDTO RaceDBDTO { get; set; }
        public int? Place { get; set; }
    }
}
