using PolyRibo.Application.Interface.Infrastructure.Repository;
using PolyRibo.Domain.Entities;
using PolyRibo.Infrastructure.Persistence;

namespace PolyRibo.Infrastructure.Repository;

public class ArchitectureTypeRepository : BaseRepository<ArchitectureType> , IArchitectureTypeRepository
{
    public ArchitectureTypeRepository(PolyRiboDbContext context) : base(context) { }
}
