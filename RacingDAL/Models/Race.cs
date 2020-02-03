using RacingDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDAL.Models
{
        public class Race : IEntity, IName
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public ICollection<CarStat> CarStat { get; set; }
            public Race()
            {
                CarStat = new List<CarStat>();
            }
        }
}
