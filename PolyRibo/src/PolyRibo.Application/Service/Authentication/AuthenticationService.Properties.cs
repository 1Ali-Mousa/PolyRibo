using FluentValidation;
using PolyRibo.Application.DTO.Auth.Login;
using PolyRibo.Application.Interface.Infrastructure.Repository;

namespace PolyRibo.Application.Service.Authentication;

public partial class AuthenticationService
{
    private readonly IUserRepository _repository;
    private readonly IValidator<LoginRequest> _Loginvalidator;
    public AuthenticationService(IUserRepository repository, IValidator<LoginRequest> validator)
    {
        _repository = repository;
        _Loginvalidator = validator;
    }
}
