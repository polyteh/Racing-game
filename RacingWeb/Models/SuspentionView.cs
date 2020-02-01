using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RacingWeb.Models
{
    public class SuspentionView
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Suspention model name")]
        public string Name { get; set; }
        public int RigidityKoef { get; set; }
        public string Manufacurer { get; set; }
        public decimal Price { get; set; }
        ICollection<RacingCarView> RacingCar { get; set; }
    }
}