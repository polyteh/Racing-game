using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RacingWeb.Models
{
    //class to read/write DB
    public class CarStatView
    {
        public int Id { get; set; }
        public int RacingCarId { get; set; }
        public RacingCarView RacingCar { get; set; }
        public int RaceId { get; set; }
        public RaceDBView RaceDBDView{ get; set; }
        public int Place { get; set; }
    }
}