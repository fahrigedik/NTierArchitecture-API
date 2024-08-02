using AutoMapper;
using NLayerApp.Core.DTOs;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Repositories;
using NLayerApp.Core.Services;
using NLayerApp.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Service.Services
{
    public class ProductServiceWithDto : ServiceWithDto<Product, ProductDto>, IProductServiceWithDto
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductServiceWithDto(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository) : base(repository, unitOfWork, mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto productCreateDto)
        {
            var entity = _mapper.Map<Product>(productCreateDto);

            await _productRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();

            var newDto = _mapper.Map<ProductDto>(entity);

            return CustomResponseDto<ProductDto>.Success(200,newDto);


        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var products = await _productRepository.GetProductsWithCategory();
            var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(products);

            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var entity = _mapper.Map<Product>(productUpdateDto);
            _productRepository.Uptade(entity);

            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(200,new NoContentDto());
            
        }
    }
}
