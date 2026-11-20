using System;
using Microsoft.Extensions.DependencyInjection;

namespace PolyRibo.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddService();
        services.AddValidation();
        return services;
    }
}
