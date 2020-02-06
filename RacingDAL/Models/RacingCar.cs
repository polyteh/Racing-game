using RacingDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDAL.Models
{
    public class RacingCar : IEntity, IName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrakeId { get; set; }
        public Brake Brake { get; set; }
        public int EngineId { get; set; }
        public Engine Engine { get; set; }
        public int SuspentionId { get; set; }
        public Suspention Suspention { get; set; }
        public ICollection<CarStat> CarStat { get; set; }
        public RacingCar()
        {
            CarStat = new List<CarStat>();
        }
    }
}
