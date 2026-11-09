using System;
using PolyRibo.Application.Interface.Infrastructure.Repository;
using PolyRibo.Domain.Entities;
using PolyRibo.Infrastructure.Persistence;

namespace PolyRibo.Infrastructure.Repository;

public class DockerConfigurationRepository : BaseRepository<DockerConfiguration> , IDockerConfigurationRepository
{
    public DockerConfigurationRepository(PolyRiboDbContext context) : base(context) { }
}
