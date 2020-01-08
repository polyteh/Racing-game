using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RacingWeb.Models
{
    public class BrakeView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EffecientKoef { get; set; }
        public string Manufacurer { get; set; }
        public decimal Price { get; set; }
        ICollection<RacingCarView> RacingCar { get; set; }
    }
}