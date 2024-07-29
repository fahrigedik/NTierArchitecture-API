using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerApp.Core.DTOs;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Services;

namespace NLayerApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController :  CustomBaseController 
    {


        private readonly IMapper _mapper;
        private readonly IService<Product> _genericService;
        private readonly IProductService _productService;
        public ProductsController(IMapper mapper, IService<Product> genericService, IProductService productService)
        {
            _mapper = mapper;
            _genericService = genericService ;
            _productService = productService ;
        }



        // GET api/products/GetProductsWithCategory
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            return CreateActionResult(await _productService.GetProductsWithCategory());
        }




        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _genericService.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());

            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));

            return CreateActionResult<List<ProductDto>>(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));   
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _genericService.GetByIdAsync(id);
            var productsDto = _mapper.Map<ProductDto>(product);

            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));

            return CreateActionResult<ProductDto>(CustomResponseDto<ProductDto>.Success(200, productsDto));
        }


        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _genericService.AddAsync(_mapper.Map<Product>(productDto));
            var productsDto = _mapper.Map<ProductDto>(product);
            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
            return CreateActionResult<ProductDto>(CustomResponseDto<ProductDto>.Success(200, productsDto));
        }


        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
             _genericService.Uptade(_mapper.Map<Product>(productDto));

            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _genericService.GetByIdAsync(id);

           /* if (product == null)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "bu id'ye sahip ürün bulunamadı."));
            }*/


              _genericService.Delete(product);

            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }


    }
}
