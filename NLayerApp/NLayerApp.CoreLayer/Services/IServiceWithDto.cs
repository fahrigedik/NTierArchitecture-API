using NLayerApp.Core.DTOs;
using NLayerApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Services
{
    public interface IServiceWithDto<TEntity,Dto> where TEntity : BaseEntity where Dto : class
    {

        Task<CustomResponseDto<Dto>> GetByIdAsync(int id);

        Task<CustomResponseDto<IEnumerable<Dto>> GetAllAsync();

        Task<CustomResponseDto<IEnumerable<Dto>>  Where(Expression<Func<TEntity, bool>> expression);
            
        Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<TEntity, bool>> expression);

        Task AddRangeAsync(IEnumerable<Dto> dtos);

        Task<CustomResponseDto<Dto>> AddAsync(Dto dto);

        CustomResponseDto<NoContentDto> Uptade(Dto entity);

        CustomResponseDto<NoContentDto> Delete(int id);

        CustomResponseDto<NoContentDto> RemoveRange(IEnumerable<int> ids);
    }
}
