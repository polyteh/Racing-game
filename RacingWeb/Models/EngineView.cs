using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RacingWeb.Models
{
    public class EngineView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public bool Turbine { get; set; }
        public string Manufacurer { get; set; }
        public decimal Price { get; set; }
        ICollection<RacingCarView> RacingCar { get; set; }
    }
}