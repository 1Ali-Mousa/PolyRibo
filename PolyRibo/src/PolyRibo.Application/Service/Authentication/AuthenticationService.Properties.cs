using System;
using FluentValidation;
using PolyRibo.Application.DTO.Auth.Login;
using PolyRibo.Application.Interface.Infrastructure.Repository;

namespace PolyRibo.Application.Service.Authentication;

public partial class AuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<LoginRequest> _loginValidator;

    public AuthenticationService(
        IUserRepository userRepository,
        IValidator<LoginRequest> loginValidator)
    {
        _userRepository = userRepository;
        _loginValidator = loginValidator;
    }
}
