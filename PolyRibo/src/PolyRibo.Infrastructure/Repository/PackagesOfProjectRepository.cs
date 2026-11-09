using PolyRibo.Application.Interface.Infrastructure.Repository;
using PolyRibo.Domain.Entities;
using PolyRibo.Infrastructure.Persistence;

namespace PolyRibo.Infrastructure.Repository;

public class PackagesOfProjectRepository : BaseRepository<PackagesOfProject> , IPackagesOfProjectRepository
{
    public PackagesOfProjectRepository(PolyRiboDbContext context) : base(context) { }
}
