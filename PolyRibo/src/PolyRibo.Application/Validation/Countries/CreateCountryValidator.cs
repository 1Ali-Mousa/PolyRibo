using FluentValidation;
using PolyRibo.Application.DTO.Countries.Create;

namespace PolyRibo.Application.Validation.Countries;

public class CreateCountryValidator : AbstractValidator<CreateCountryRequest>
{
    public CreateCountryValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage("Name must not exceed 100 characters.");

        RuleFor(request => request.Code)
            .NotEmpty()
            .WithMessage("Code is required.")
            .MaximumLength(10)
            .WithMessage("Code must not exceed 10 characters.");
    }
}
