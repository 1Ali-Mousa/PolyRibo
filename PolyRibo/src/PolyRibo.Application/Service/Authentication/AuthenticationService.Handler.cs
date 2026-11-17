using PolyRibo.Application.DTO.Auth.Login;
using PolyRibo.Application.Interface.Application.Service;
using PolyRibo.Domain.Entities;

namespace PolyRibo.Application.Service.Authentication;

public partial class AuthenticationService : IAuthenticationService
{
    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var validationResult = await _Loginvalidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new ArgumentException($"Invalid request: {string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))}");
        }

        User? user = await _repository.GetByUserNameAsync(request.UserName);
        if (user is null)
        {
            throw new UnauthorizedAccessException($"Invalid Email or Password.");
        }

        string hash = BCrypt.Net.BCrypt.HashPassword(request.PasswordHashed);
        
        if (!BCrypt.Net.BCrypt.Verify(user.PasswordHashed, hash))
        {
            throw new UnauthorizedAccessException("Invalid UserName or Password.");
        }

        return new LoginResponse
        {
            UserName = user.UserName,
            CountryId = user.CountryId,
            Email = user.Email
        };
    }
}
