using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using progresssoft_task.Server.Data;
using progresssoft_task.Server.Repositories.Interfaces;

namespace progresssoft_task.Server.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DBContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DBContext dbContext, DbSet<T> dbSet) { 

            _dbContext = dbContext;
            _dbSet = dbSet;
        }


        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);

        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, T>>? selector)
        {
            IQueryable<T> query = selector != null ? _dbSet.Select(selector) : _dbSet;
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetFilteredListAsync(List<Expression<Func<T, bool>>>? filters, Expression<Func<T, T>>? selector)
        {
            IQueryable<T> query = _dbSet;
            foreach(var filter in filters!)
            {
                query = query.Where(filter);
            }
            query = selector != null ? query.Select(selector) : query;
            return await query.ToListAsync();
        }

        public async Task<T> AddAsync(T enitity)
        {
            await _dbSet.AddAsync(enitity);
            return enitity;
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

    }
}
