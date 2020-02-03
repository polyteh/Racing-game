using Ninject;
using RacingDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// generic repository for the CRUD function for Engine, Brake and Suspention
namespace RacingDAL
{
    public class GenericRacingRepository<TEntity> : IGeneralDBRepository<TEntity> where TEntity : class, IEntity, IName
    {
        protected DbContext _context;
        protected DbSet<TEntity> _dbSet;
        [Inject]
        public GenericRacingRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public virtual async Task CreateAsync(TEntity item)
        {
            _dbSet.Add(item);
            await _context.SaveChangesAsync();
        }
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public virtual async Task<TEntity> FindByIdAsync(int? id)
        {
            var itemById = await _dbSet.SingleOrDefaultAsync<TEntity>(e => e.Id == id);
            return itemById;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public IEnumerable<TEntity> GetAllAsync(Func<TEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> RemoveAsync(int? id)
        {

            var itemBuId = _dbSet.SingleOrDefault(e => e.Id == id);
            _dbSet.Remove(itemBuId);
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task UpdateAsync(TEntity item)
        {
            var itemBuId = _dbSet.SingleOrDefault(e => e.Id == item.Id);
            if (itemBuId != null)
            {
                _context.Entry<TEntity>(itemBuId).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual TEntity FindById(int id)
        {
            var itemById = _dbSet.SingleOrDefault<TEntity>(e => e.Id == id);
            return itemById;
        }

        public async Task< TEntity> FindByModelAsync(string model)
        {
            var itemByModel = await _dbSet.SingleOrDefaultAsync<TEntity>(e => e.Name==model);
            return itemByModel;
        }
        public TEntity FindByModel(string model)
        {
            var itemByModel =  _dbSet.SingleOrDefault<TEntity>(e => e.Name == model);
            return itemByModel;
        }
    }
}
