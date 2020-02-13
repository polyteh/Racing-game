using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RacingWeb.Models
{
    public class SuspentionView
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Suspention model name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Model name should be in the range 3..20 characters")]
        [Remote("CheckModelName", "Suspention", ErrorMessage = "Model name already exists")]
        public string Name { get; set; }
        [Required]
        [Range(3, 10, ErrorMessage = "Rigidity coefficient is out of range. Should be 3..10")]
        public int RigidityKoef { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Manufacturer name should be in the range 3..20 characters")]
        public string Manufacurer { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Range(110, 200, ErrorMessage = "Price is out of range. Should be 110..200")]
        public decimal Price { get; set; }
        ICollection<RacingCarView> RacingCar { get; set; }
    }
}