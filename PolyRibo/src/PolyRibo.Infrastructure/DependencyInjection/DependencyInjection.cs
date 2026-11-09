using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PolyRibo.Application.Interface.Infrastructure.Repository;
using PolyRibo.Application.Interface.Infrastructure.Presistence;
using PolyRibo.Infrastructure.Persistence;
using PolyRibo.Infrastructure.Repository;

namespace PolyRibo.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PolyRiboDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddRepository();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}