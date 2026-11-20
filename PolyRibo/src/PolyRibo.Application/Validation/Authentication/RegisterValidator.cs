using FluentValidation;
using PolyRibo.Application.DTO.Auth.Register;

namespace PolyRibo.Application.Validation.Authentication;

public class RegisterValidator : AbstractValidator<RegisterRequest>
{
    public RegisterValidator()
    {
        RuleFor(request => request.UserName)
            .NotEmpty()
            .WithMessage("UserName is required.")
            .MaximumLength(100)
            .WithMessage("UserName must not exceed 100 characters.");

        RuleFor(request => request.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email is not valid.")
            .MaximumLength(255)
            .WithMessage("Email must not exceed 255 characters.");

        RuleFor(request => request.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters.");

        RuleFor(request => request.CountryId)
            .GreaterThan(0)
            .WithMessage("CountryId is required.");
    }
}
