using FluentValidation;
using PolyRibo.Application.DTO.Auth.Login;

namespace PolyRibo.Application.Feature.Validation.Authentication;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("UserName is required.");


        RuleFor(x => x.PasswordHashed)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters.");
    }
}
