using Microsoft.AspNetCore.Mvc;
using PolyRibo.API.Response;
using PolyRibo.Application.DTO.Auth.Login;
using PolyRibo.Application.DTO.Auth.Register;
using PolyRibo.Application.Interface.Application.Service;

namespace PolyRibo.API.Controllers;

[Route("api/Authentication")]
[ApiController]
public class AuthenticationController : BaseController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("Login", Name = "LoginAsync")]
    public async Task<ActionResult<APIResponse<LoginResponse>>> LoginAsync([FromBody] LoginRequest request)
    {
        var response = await _authenticationService.LoginAsync(request);

        return CreateResponse(response, StatusCodes.Status200OK, "Login Successfully!");
    }

    [HttpPost("Register", Name = "RegisterAsync")]
    public async Task<ActionResult<APIResponse<RegisterResponse>>> RegisterAsync([FromBody] RegisterRequest request)
    {
        var response = await _authenticationService.RegisterAsync(request);

        return CreateResponse(response, StatusCodes.Status201Created, "Registered Successfully!");
    }
}
