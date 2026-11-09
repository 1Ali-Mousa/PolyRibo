using PolyRibo.Application.Interface.Infrastructure.Repository;
using PolyRibo.Domain.Entities;
using PolyRibo.Infrastructure.Persistence;

namespace PolyRibo.Infrastructure.Repository;

public class DatabaseTypeRepository : BaseRepository<DatabaseType> , IDatabaseTypeRepository
{
    public DatabaseTypeRepository(PolyRiboDbContext context) : base(context) { }
}
