using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RacingWeb.Models
{
    public class RaceDBView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CarStatView> CarStat { get; set; }
        public RaceDBView()
        {
            CarStat = new List<CarStatView>();
        }
    }
}