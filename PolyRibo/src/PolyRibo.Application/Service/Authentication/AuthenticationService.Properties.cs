using System;
using FluentValidation;
using PolyRibo.Application.DTO.Auth.Login;
using PolyRibo.Application.DTO.Auth.Register;
using PolyRibo.Application.Interface.Infrastructure.Persistence;
using PolyRibo.Application.Interface.Infrastructure.Repository;

namespace PolyRibo.Application.Service.Authentication;

public partial class AuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<LoginRequest> _loginValidator;
    private readonly IValidator<RegisterRequest> _registerValidator;

    public AuthenticationService(
        IUserRepository userRepository,
        ICountryRepository countryRepository,
        IUnitOfWork unitOfWork,
        IValidator<LoginRequest> loginValidator,
        IValidator<RegisterRequest> registerValidator)
    {
        _userRepository = userRepository;
        _countryRepository = countryRepository;
        _unitOfWork = unitOfWork;
        _loginValidator = loginValidator;
        _registerValidator = registerValidator;
    }
}
