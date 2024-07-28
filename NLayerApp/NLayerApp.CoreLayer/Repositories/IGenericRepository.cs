using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);

        IQueryable<T> Where(Expression<Func<T,bool>> expression);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        Task AddRangeAsync(IEnumerable<T> entities);

        Task AddAsync(T entity);

        void Uptade(T entity);

        void Delete(T entity);

        void RemoveRange(IEnumerable<T> entities);


    }
}
