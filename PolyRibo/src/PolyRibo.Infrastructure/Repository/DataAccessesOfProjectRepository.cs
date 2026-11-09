using PolyRibo.Application.Interface.Infrastructure.Repository;
using PolyRibo.Domain.Entities;
using PolyRibo.Infrastructure.Persistence;

namespace PolyRibo.Infrastructure.Repository;

public class DataAccessesOfProjectRepository : BaseRepository<DataAccessesOfProject> , IDataAccessesOfProjectRepository
{
    public DataAccessesOfProjectRepository(PolyRiboDbContext context) : base(context) { }
}
