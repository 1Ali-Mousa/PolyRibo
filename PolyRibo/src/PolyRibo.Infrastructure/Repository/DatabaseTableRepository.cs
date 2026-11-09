using PolyRibo.Application.Interface.Infrastructure.Repository;
using PolyRibo.Domain.Entities;
using PolyRibo.Infrastructure.Persistence;

namespace PolyRibo.Infrastructure.Repository;

public class DatabaseTableRepository : BaseRepository<DatabaseTable> , IDatabaseTableRepository
{
    public DatabaseTableRepository(PolyRiboDbContext context) : base(context) { }
}
