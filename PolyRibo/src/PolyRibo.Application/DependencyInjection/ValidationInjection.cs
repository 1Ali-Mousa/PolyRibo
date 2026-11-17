
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace PolyRibo.Application.DependencyInjection;

public static class ValidationInjection
{
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ValidationInjection).Assembly);
        return services;
    }
}
