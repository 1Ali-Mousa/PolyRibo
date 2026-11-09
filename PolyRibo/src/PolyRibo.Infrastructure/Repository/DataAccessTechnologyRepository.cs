using PolyRibo.Application.Interface.Infrastructure.Repository;
using PolyRibo.Domain.Entities;
using PolyRibo.Infrastructure.Persistence;

namespace PolyRibo.Infrastructure.Repository;

public class DataAccessTechnologyRepository : BaseRepository<DataAccessTechnology> , IDataAccessTechnologyRepository
{
    public DataAccessTechnologyRepository(PolyRiboDbContext context) : base(context) { }
}
