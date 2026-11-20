using Microsoft.AspNetCore.Mvc;
using PolyRibo.API.Response;

namespace PolyRibo.API.Controllers;

public abstract class BaseController : ControllerBase
{
    protected ActionResult<APIResponse<T>> CreateResponse<T>(T data, int code = StatusCodes.Status200OK, string? message = null)
    {
        return StatusCode(code, new APIResponse<T>
        {
            statusCode = code,
            message = message ?? (code == StatusCodes.Status200OK ? "Success" : "Error"),
            data = data
        });
    }
}
