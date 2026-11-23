using System;
using FluentValidation;
using PolyRibo.Application.DTO.Countries.Pagination;

namespace PolyRibo.Application.Validation.Countries;

public class PaginationCountryValidator : AbstractValidator<PaginationCountryRequest>
{
    public PaginationCountryValidator()
    {
                RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Rows must be greater than 0.")
                .LessThanOrEqualTo(250)
                .WithMessage("Rows must not exceed 250.");
    }
}
