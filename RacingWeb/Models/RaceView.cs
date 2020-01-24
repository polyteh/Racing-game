using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RacingWeb.Models
{
    public class RaceView
    {
        public int Id { get; set; }
        public List<CarStatusView> CarList { get; set; }
        public bool isStarted { get; set; }

    }
}