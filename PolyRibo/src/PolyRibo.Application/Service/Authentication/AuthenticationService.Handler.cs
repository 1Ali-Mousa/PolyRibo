using System.Security.Authentication;
using FluentValidation;
using PolyRibo.Application.DTO.Auth.Login;
using PolyRibo.Application.Interface.Application.Service;
using PolyRibo.Application.Interface.Infrastructure.Repository;

namespace PolyRibo.Application.Service.Authentication;

public partial class AuthenticationService : IAuthenticationService
{
    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var validationResult = await _loginValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
            throw new ArgumentException($"Invalid request: {errors}");
        }

        var user = await _userRepository.GetByUserNameAsync(request.UserName.Trim());

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHashed))
        {
            throw new AuthenticationException("Invalid username or password.");
        }

        return new LoginResponse
        {
            UserName = user.UserName,
            Email = user.Email,
            CountryId = user.CountryId
        };
    }
}
