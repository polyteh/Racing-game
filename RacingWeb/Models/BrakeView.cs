using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RacingWeb.Models
{
    public class BrakeView
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Model name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Model should be in the range 3..20 characters")]
        public string Name { get; set; }
        [Required]
        [Range(3, 10, ErrorMessage = "Efficiency coefficient is out of range. Should be 3..10")]
        public int EffecientKoef { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Manufacturer name should be in the range 3..20 characters")]
        public string Manufacurer { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        ICollection<RacingCarView> RacingCar { get; set; }
    }
}