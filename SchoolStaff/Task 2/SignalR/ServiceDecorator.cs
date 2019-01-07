using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business;
using Business.Repositories;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace Presentation.SignalR
{
    public class ServiceDecorator<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly Repository<T> _context;
        private readonly IAppHub _hub;
        //private readonly DbSet<T> _dbSet;

        public string MethodUpdate { get; set; }
        public string MethodRemove { get; set; }
        public string MethodAdd { get; set; }

        public ServiceDecorator(
            Repository<T> context,
            ApplicationDbContext dbcontext,
            IAppHub hub)
        {
            _context = context;
            _dbcontext = dbcontext;
            _hub = hub;
            //_dbSet = _context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _context.GetAll(); //_dbSet;
        }

        public Task<T> Get(int id)
        {
            return _context.Get(id); //_dbSet.FindAsync(id);
        }

        public async void Create(T index)
        {
            _context.Create(index); //_dbSet.AddAsync(index);
            await _dbcontext.SaveChangesAsync();
            await NotifyEmployeeListModified(MethodAdd, index);
        }

        public async void Delete(T index)
        {
            await NotifyEmployeeListModified(MethodRemove, index);
            _context.Delete(index);
        }
        
        public async Task Update(T index)
        {
            await NotifyEmployeeListModified(MethodUpdate, index);
            _dbcontext.Entry(index).State = EntityState.Modified;
            //await _context.Update(index); //_dbSet.Update(index);
        }

        private async Task NotifyEmployeeListModified(string method, T index)
        {
            object[] mas = { index };
            await _hub.NotifyAll(method, mas, default(CancellationToken));
        }
    }
}