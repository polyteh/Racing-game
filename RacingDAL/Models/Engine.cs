using RacingDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDAL.Models
{
    public class Engine : IEntity, IName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public bool Turbine { get; set; }
        public string Manufacurer { get; set; }
        public decimal Price { get; set; }
        public ICollection<RacingCar> RacingCar { get; set; }
    }
}
