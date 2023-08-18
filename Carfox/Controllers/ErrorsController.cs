using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Carfox.Errors;

namespace Carfox.Controllers
{
    [Route("erros/code")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public ActionResult Error(int code)
        {
            return NotFound(new ApiResponse(code));
        }
    }
}
