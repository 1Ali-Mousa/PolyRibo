using Microsoft.AspNetCore.Mvc;
using PolyRibo.API.Response;

namespace PolyRibo.API.Controllers;

[Route("api/Base")]
[ApiController]
public class BaseController : ControllerBase
{
    protected ActionResult<APIResponse<T>> CreateResponse<T>(T data, int code = 200, string? message = null)
    {
        return StatusCode(code, new APIResponse<T>
        {
            statusCode = code,
            message = message ?? (code == 200 ? "Success" : "Error"),
            data = data
        });
    }
}

