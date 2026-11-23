using Microsoft.Extensions.DependencyInjection;
using PolyRibo.Application.Interface.Application.Service;
using PolyRibo.Application.Service.Authentication;
using PolyRibo.Application.Service.Countries;

namespace PolyRibo.Application.DependencyInjection;

public static class ServiceInjection
{
    public static IServiceCollection AddService(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ICountryService, CountryService>();

        return services;
    }
}
