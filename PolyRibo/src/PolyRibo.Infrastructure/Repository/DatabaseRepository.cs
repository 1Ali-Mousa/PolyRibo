using PolyRibo.Application.Interface.Infrastructure.Repository;
using PolyRibo.Domain.Entities;
using PolyRibo.Infrastructure.Persistence;

namespace PolyRibo.Infrastructure.Repository;

public class DatabaseRepository : BaseRepository<Database> , IDatabaseRepository
{
    public DatabaseRepository(PolyRiboDbContext context) : base(context) { }
}
