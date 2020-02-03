using RacingDAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDAL
{
    public class RaceRepository : GenericRacingRepository<Race>
    {
        public RaceRepository(DbContext context) : base(context)
        {

        }
        public async override Task<IEnumerable<Race>> GetAllAsync()
        {
            return await _dbSet.Include(x => x.CarStat).ToListAsync();
        }

    }
}
