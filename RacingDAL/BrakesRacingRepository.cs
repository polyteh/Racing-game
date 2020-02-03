using RacingDAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDAL
{
    public class BrakesRacingRepository: GenericRacingRepository<Brake>
    {
        public BrakesRacingRepository(DbContext context) : base(context)
        {

        }
        public override async Task<bool> RemoveAsync(int? id)
        {
            var itemBuId = _dbSet.Include(x => x.RacingCar).SingleOrDefault(e => e.Id == id);
            if (itemBuId.RacingCar.Count() == 0)
            {
                _dbSet.Remove(itemBuId);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
