using PolyRibo.Application.Interface.Infrastructure.Repository;
using PolyRibo.Domain.Entities;
using PolyRibo.Infrastructure.Persistence;

namespace PolyRibo.Infrastructure.Repository;

public class PackageRepository : BaseRepository<Package> , IPackageRepository
{
    public PackageRepository(PolyRiboDbContext context) : base(context) { }
}
