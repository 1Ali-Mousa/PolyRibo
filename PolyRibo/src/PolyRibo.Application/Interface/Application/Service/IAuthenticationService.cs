using PolyRibo.Application.DTO.Auth.Login;

namespace PolyRibo.Application.Interface.Application.Service;

public interface IAuthenticationService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
}
