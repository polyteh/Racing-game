using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RacingWeb.Models
{
    public class RacingCarView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrakeId { get; set; }
        public BrakeView Brake { get; set; }
        public int EngineId { get; set; }
        public EngineView Engine { get; set; }
        public int SuspentionId { get; set; }
        public SuspentionView Suspention { get; set; }
    }
}