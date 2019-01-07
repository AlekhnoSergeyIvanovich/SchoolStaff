using System;
using System.Linq;
using System.Threading.Tasks;
using Business.Repositories;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
        public ApplicationDbContext _db;
        protected DbSet<TEntity> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            this._db = context;
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public virtual async Task<TEntity> Get(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Create(TEntity order)
        {
            _dbSet.AddAsync(order);
            //await _db.SaveChangesAsync();
        }

        public async Task Update(TEntity order)
        {
            //_dbSet.Attach(order);
            //_dbSet.State = 
            await _db.UpdateChangesSaved(order);
            //await _db.SaveChangesAsync();
        }

        public void Delete(TEntity order)
        {
            _dbSet.Remove(order);
        }

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}