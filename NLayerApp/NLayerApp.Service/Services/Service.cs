using NLayerApp.Core.Repositories;
using NLayerApp.Core.Services;
using NLayerApp.Core.UnitOfWorks;
using NLayerApp.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Service.Services
{
    public class Service<T> : IService<T> where T : class
    {

        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public Service(IUnitOfWork unitOfWork, IGenericRepository<T> repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<T> AddAsync(T entity)
        {
          await _repository.AddAsync(entity);
          await _unitOfWork.CommitAsync();
          return entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public void Delete(T entity)
        {
             _repository.Delete(entity);
             _unitOfWork.Commit();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return _repository.GetAll();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var hasEntity = await _repository.GetByIdAsync(id);

            if (hasEntity==null)
            {
                throw new ClientSideException($"bu id'ye sahip {typeof(T).Name} bulunamadı. ");
            }

            return await _repository.GetByIdAsync(id);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
           _repository.RemoveRange(entities);
            _unitOfWork.Commit();
        }

        public void Uptade(T entity)
        {
             _repository.Uptade(entity);
            _unitOfWork.Commit();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);   
        }
    }
}
