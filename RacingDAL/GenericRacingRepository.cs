﻿using Ninject;
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
    public class GenericRacingRepository<TEntity> : IGeneralDBRepository<TEntity> where TEntity : class, IEntity
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

        public virtual async Task<TEntity> FindByIdAsync(int id)
        {
            var itemBuId = await _dbSet.SingleOrDefaultAsync<TEntity>(e=>e.Id==id);
            return itemBuId;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public IEnumerable<TEntity> GetAllAsync(Func<TEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Remove(TEntity item)
        {
            throw new NotImplementedException();
        }

        public virtual async Task UpdateAsync(TEntity item)
        {
            var itemBuId = _dbSet.SingleOrDefault(e => e.Id == item.Id);
            if (itemBuId!=null)
            {
                _context.Entry<TEntity>(itemBuId).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
