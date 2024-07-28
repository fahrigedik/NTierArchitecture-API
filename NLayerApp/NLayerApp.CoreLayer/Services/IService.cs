using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Services
{
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);

        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        Task AddRangeAsync(IEnumerable<T> entities);

        Task AddAsync(T entity);

        Task UptadeAsync(T entity);

        Task DeleteAsync(T entity);

        Task RemoveRangeAsync(IEnumerable<T> entities);

    }
}
