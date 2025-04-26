using System.Linq.Expressions;

namespace progresssoft_task.Server.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, T>>? selector);
        Task<IEnumerable<T>> GetFilteredListAsync(List<Expression<Func<T, bool>>>? filters, Expression<Func<T, T>>? selector);
        Task<T> AddAsync(T entity);
        void Remove(T entity);
        Task<int> SaveChangesAsync();
    }
}
