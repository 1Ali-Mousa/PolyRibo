using Microsoft.Extensions.DependencyInjection;
using PolyRibo.Application.Interface.Infrastructure;
using PolyRibo.Infrastructure.Persistence;
using PolyRibo.Infrastructure.Repository;

namespace PolyRibo.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}