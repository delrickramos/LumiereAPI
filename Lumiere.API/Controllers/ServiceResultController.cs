using Lumiere.API.Common;
using Microsoft.AspNetCore.Mvc;

namespace Lumiere.API.Controllers
{
    [ApiController]
    public abstract class ServiceResultController : ControllerBase
    {
        protected IActionResult HandleResult<T>(ServiceResult<T> result)
        {
            if (!result.Ok)
            {
                return result.StatusCode switch
                {
                    404 => NotFound(new { error = result.Error }),
                    400 => BadRequest(new { error = result.Error }),
                    409 => Conflict(new { error = result.Error }),
                    _ => StatusCode(result.StatusCode, new { error = result.Error })
                };
            }

            return result.StatusCode switch
            {
                201 => Created(string.Empty, result.Data),
                204 => NoContent(),
                _ => Ok(result.Data)
            };
        }
    }
}
