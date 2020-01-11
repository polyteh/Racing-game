using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDAL.Interfaces
{
    public interface IGeneralDBRepository<TEntity>:IDisposable 
        where TEntity : class, IEntity
    {
            Task CreateAsync(TEntity item);
            Task<TEntity> FindByIdAsync(int id);
            Task<IEnumerable<TEntity>> GetAllAsync();
            IEnumerable<TEntity> GetAllAsync(Func<TEntity, bool> predicate);
            void Remove(TEntity item);
            Task UpdateAsync(TEntity item);
    }
}
