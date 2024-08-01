using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayerApp.Core.DTOs;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Services;
using NLayerApp.Web.Filter;

namespace NLayerApp.Web.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;


        public ProductController(IProductService productService, ICategoryService categoryService, IMapper mapper)
        {
            this._productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var customResponse = await _productService.GetProductsWithCategory();
            var data = customResponse.Data;
            return View(data);
        }


        [HttpGet]
        public async Task<IActionResult> Save()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoriesDto, "Id", "categoryName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var categories = await _categoryService.GetAllAsync();
            categories = categories.ToList();

            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
           

            ViewBag.categories = new SelectList(categoriesDto, "Id", "categoryName");

            if (ModelState.IsValid)
            {
                await _productService.AddAsync(_mapper.Map<Product>(productDto));
            }

            return RedirectToAction("Index");
        }


        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            var categories = await _categoryService.GetAllAsync();
            categories = categories.ToList();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);


            ViewBag.categories = new SelectList(categoriesDto, "Id", "categoryName",product.categoryId);


            return View(_mapper.Map<ProductDto>(product));
        }


        
        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {

            if (ModelState.IsValid)
            {
                _productService.Uptade(_mapper.Map<Product>(productDto));
                return RedirectToAction("Index");
            }

            var categories = await _categoryService.GetAllAsync();
            categories = categories.ToList();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);


            ViewBag.categories = new SelectList(categoriesDto, "Id", "categoryName", productDto.categoryId);

            return View(productDto.Id);


        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            _productService.Delete(product);
            return RedirectToAction("Index");
              
        }
    }
}
