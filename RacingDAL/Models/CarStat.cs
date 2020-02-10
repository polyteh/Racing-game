using RacingDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDAL.Models
{
        public class CarStat : IEntity
    {
            public int Id { get; set; }
            public int RacingCarId { get; set; }
            public RacingCar RacingCar { get; set; }
            public int RaceId { get; set; }
            public Race Race { get; set; }
            public int? Place { get; set; }
        }

}
