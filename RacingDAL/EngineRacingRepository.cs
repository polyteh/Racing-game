using RacingDAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDAL
{
    public class EngineRacingRepository : GenericRacingRepository<Engine>
    {
        public EngineRacingRepository(DbContext context) : base(context)
        {

        }
    }
}
