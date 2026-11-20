using System.Security.Authentication;
using PolyRibo.Domain.Entities;
using PolyRibo.Application.DTO.Auth.Login;
using PolyRibo.Application.DTO.Auth.Register;
using PolyRibo.Application.Interface.Application.Service;

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

    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
    {
        var validationResult = await _registerValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
            throw new ArgumentException($"Invalid request: {errors}");
        }

        var userName = request.UserName.Trim();
        var email = request.Email.Trim();

        if (await _userRepository.GetByUserNameAsync(userName) is not null)
        {
            throw new InvalidOperationException("Username is already taken.");
        }

        if (await _userRepository.GetByEmailAsync(email) is not null)
        {
            throw new InvalidOperationException("Email is already registered.");
        }

        if (await _countryRepository.GetByIdAsync(request.CountryId) is null)
        {
            throw new ArgumentException("Country does not exist.");
        }

        var user = new User
        {
            UserName = userName,
            Email = email,
            PasswordHashed = BCrypt.Net.BCrypt.HashPassword(request.Password),
            CountryId = request.CountryId
        };

        await _userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return new RegisterResponse
        {
            UserName = user.UserName,
            Email = user.Email,
            CountryId = user.CountryId
        };
    }
}
