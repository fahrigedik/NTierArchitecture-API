using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerApp.Core.DTOs;

namespace NLayerApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {


        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.statusCode == 204)
            {
                return new ObjectResult(null) { StatusCode = response.statusCode };
            }

            return new ObjectResult(null) { StatusCode = response.statusCode };


        }
    }
}
