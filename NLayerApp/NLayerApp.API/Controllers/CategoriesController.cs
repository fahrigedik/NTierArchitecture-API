using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerApp.Core.DTOs;
using NLayerApp.Core.Services;

namespace NLayerApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetCategoryWithProducts(int id)
        {
            var categoriesWithProducts = await _categoryService.GetSingleCategoryByIdWithProductAsync(id);

            return CreateActionResult(await _categoryService.GetSingleCategoryByIdWithProductAsync(id));

        }

    }
}
