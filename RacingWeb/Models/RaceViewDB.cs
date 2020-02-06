using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RacingWeb.Models
{
    public class RaceViewDB
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CarStatView> CarStat { get; set; }
        public RaceViewDB()
        {
            CarStat = new List<CarStatView>();
        }
    }
}