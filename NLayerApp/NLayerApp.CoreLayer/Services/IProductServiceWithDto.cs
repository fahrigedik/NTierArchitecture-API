using NLayerApp.Core.DTOs;
using NLayerApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Services
{
    public interface IProductServiceWithDto : IServiceWithDto<Product,ProductDto>
    {
        public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory();


        public Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto productUpdateDto);

        public Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto productCreateDto);



    }
}
