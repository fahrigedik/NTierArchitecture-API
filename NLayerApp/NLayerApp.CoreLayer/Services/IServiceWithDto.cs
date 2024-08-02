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

        public Task<CustomResponseDto<Dto>> GetByIdAsync(int id);

        public Task<CustomResponseDto<IEnumerable<Dto>>> GetAllAsync();

        public Task<CustomResponseDto<IEnumerable<Dto>>>  Where(Expression<Func<TEntity, bool>> expression);
            
        public Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<TEntity, bool>> expression);

        public Task<CustomResponseDto<IEnumerable<Dto>>> AddRangeAsync(IEnumerable<Dto> dtos);

        public Task<CustomResponseDto<Dto>> AddAsync(Dto dto);

        public CustomResponseDto<NoContentDto> Uptade(Dto entity);

        public  CustomResponseDto<NoContentDto> Delete(int id);
         
        public CustomResponseDto<NoContentDto> RemoveRange(IEnumerable<int> ids);
    }
}
