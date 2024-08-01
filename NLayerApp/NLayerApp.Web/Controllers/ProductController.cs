using Microsoft.AspNetCore.Mvc;
using NLayerApp.Core.Services;

namespace NLayerApp.Web.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            this._productService = productService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var customResponse = await _productService.GetProductsWithCategory();
            var data = customResponse.Data;
            return View(data);
        }
    }
}
