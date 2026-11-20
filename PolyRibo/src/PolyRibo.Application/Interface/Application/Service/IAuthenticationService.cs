using PolyRibo.Application.DTO.Auth.Login;
using PolyRibo.Application.DTO.Auth.Register;

namespace PolyRibo.Application.Interface.Application.Service;

public interface IAuthenticationService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);

    Task<RegisterResponse> RegisterAsync(RegisterRequest request);
}
