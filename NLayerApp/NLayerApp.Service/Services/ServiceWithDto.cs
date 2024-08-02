using AutoMapper;
using Microsoft.AspNetCore.Http;
using NLayerApp.Core.DTOs;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Repositories;
using NLayerApp.Core.Services;
using NLayerApp.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Service.Services
{
    public class ServiceWithDto<TEntity, Dto> : IServiceWithDto<TEntity, Dto> where TEntity : BaseEntity where Dto : class
    {

        private readonly IGenericRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ServiceWithDto(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }




        public async Task<CustomResponseDto<Dto>> AddAsync(Dto dto)
        {
            TEntity entity = _mapper.Map<TEntity>(dto);
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();

            var newDto = _mapper.Map<Dto>(entity);

            return CustomResponseDto<Dto>.Success(200, newDto); 

        }

        public async Task<CustomResponseDto<IEnumerable<Dto>>> AddRangeAsync(IEnumerable<Dto> dtos)
        {
            var Entities = _mapper.Map<IEnumerable<TEntity>>(dtos);

            await _repository.AddRangeAsync(Entities); ;
            await _unitOfWork.CommitAsync();

            var newEntities = _mapper.Map<IEnumerable<Dto>>(Entities);

            return  CustomResponseDto<IEnumerable<Dto>>.Success(200, newEntities);

        }

        public async Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            var hasResult = await _repository.AnyAsync(expression);

            return CustomResponseDto<bool>.Success(200, hasResult);

        }

        public CustomResponseDto<NoContentDto> Delete(int id)
        {
            var entity = _repository.GetByIdAsync(id).Result;
            _repository.Delete(entity);
            return CustomResponseDto<NoContentDto>.Success(200,new NoContentDto());
        }

        public async Task<CustomResponseDto<Dto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            var dto = _mapper.Map<Dto>(entity);
            return CustomResponseDto<Dto>.Success(StatusCodes.Status200OK, dto);

        }

        public  CustomResponseDto<NoContentDto> RemoveRange(IEnumerable<int> ids)
        {
            var entities = _repository.Where(x => ids.Contains(x.Id)).ToList();
            _repository.RemoveRange(entities);
            return CustomResponseDto<NoContentDto>.Success(200, new NoContentDto());
        }

        public CustomResponseDto<NoContentDto> Uptade(Dto entity)
        {
            var Entity = _mapper.Map<TEntity>(entity);
            _repository.Uptade(Entity);
            return CustomResponseDto<NoContentDto>.Success(200, new NoContentDto());

        }

        public async Task<CustomResponseDto<IEnumerable<Dto>>> GetAllAsync()
        {
            var entities =  _repository.GetAll();

            var dtos = _mapper.Map<IEnumerable<Dto>>(entities.ToList());
            return CustomResponseDto<IEnumerable<Dto>>.Success(200, dtos);

        }

        public async Task<CustomResponseDto<IEnumerable<Dto>>> Where(Expression<Func<TEntity, bool>> expression)
        {
            var entities = _repository.Where(expression).ToList();

            var dtos = _mapper.Map<IEnumerable<Dto>>(entities);

            return CustomResponseDto<IEnumerable<Dto>>.Success(200, dtos);
        }
    }
}
