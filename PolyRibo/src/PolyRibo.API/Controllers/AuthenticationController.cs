using Microsoft.AspNetCore.Mvc;
using PolyRibo.API.Response;
using PolyRibo.Application.DTO.Auth.Login;
using PolyRibo.Application.Interface.Application.Service;

namespace PolyRibo.API.Controllers;

[Route("api/Authentication")]
[ApiController]
public class AuthenticationController : BaseController
{
    private readonly IAuthenticationService _service;
    public AuthenticationController(IAuthenticationService service)
    {
        _service = service;
    }

    public async Task<ActionResult<APIResponse<LoginResponse>>> LoginAsync(LoginRequest request)
    {
        var response = await _service.LoginAsync(request);
        return CreateResponse<LoginResponse>(response, StatusCodes.Status200OK ,"Login Successfully!");
    }

}
