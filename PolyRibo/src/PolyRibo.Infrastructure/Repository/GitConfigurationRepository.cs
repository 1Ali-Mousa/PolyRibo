using PolyRibo.Application.Interface.Infrastructure.Repository;
using PolyRibo.Domain.Entities;
using PolyRibo.Infrastructure.Persistence;

namespace PolyRibo.Infrastructure.Repository;

public class GitConfigurationRepository : BaseRepository<GitConfiguration> , IGitConfigurationRepository
{
    public GitConfigurationRepository(PolyRiboDbContext context) : base(context) { }
}
