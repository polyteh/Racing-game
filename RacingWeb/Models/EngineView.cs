using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RacingWeb.Models
{
    public class EngineView
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Engine model name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Model name should be in the range 3..20 characters")]
        [Remote("CheckModelName", "Engine", ErrorMessage = "Model name already exists")]
        public string Name { get; set; }
        [Required]
        [Range(140, 300, ErrorMessage = "HP is out of range. Should be 140..300")]
        public int HP { get; set; }
        public bool Turbine { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Manufacturer name should be in the range 3..20 characters")]
        public string Manufacurer { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        ICollection<RacingCarView> RacingCar { get; set; }
    }
}