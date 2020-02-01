using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RacingWeb.Models
{
    public class RacingCarView
    {
        public int Id { get; set; }
        [Display(Name = "Model name")]
        [Required]
        public string Name { get; set; }
        public int BrakeId { get; set; }
        [Display(Name = "Brake model name")]
        public BrakeView Brake { get; set; }
        public int EngineId { get; set; }
        [Display(Name = "Engine model name")]
        public EngineView Engine { get; set; }
        public int SuspentionId { get; set; }
        [Display(Name = "Suspention model name")]
        public SuspentionView Suspention { get; set; }
    }
}