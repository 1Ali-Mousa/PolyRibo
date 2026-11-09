using Microsoft.Extensions.DependencyInjection;
using PolyRibo.Application.Interface.Infrastructure.Repository;
using PolyRibo.Infrastructure.Repository;

namespace PolyRibo.Infrastructure.DependencyInjection;

internal static class RepositoryInjection
{
    internal static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IArchitectureTypeRepository, ArchitectureTypeRepository>();
        services.AddScoped<ICountryRepository , CountryRepository>();
        services.AddScoped<IDataAccessesOfProjectRepository, DataAccessesOfProjectRepository>();
        services.AddScoped<IDataAccessTechnologyRepository, DataAccessTechnologyRepository>();
        services.AddScoped<IDatabaseRepository, DatabaseRepository>();
        services.AddScoped<IDatabaseTableRepository, DatabaseTableRepository>();
        services.AddScoped<IDatabaseTypeRepository, DatabaseTypeRepository>();
        services.AddScoped<IDockerConfigurationRepository, DockerConfigurationRepository>();
        services.AddScoped<IFrameworkTypeRepository, FrameworkTypeRepository>();
        services.AddScoped<IGitConfigurationRepository, GitConfigurationRepository>();
        services.AddScoped<ILanguageRepository, LanguageRepository>();
        services.AddScoped<IPackageRepository, PackageRepository>();
        services.AddScoped<IPackagesOfProjectRepository, PackagesOfProjectRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
