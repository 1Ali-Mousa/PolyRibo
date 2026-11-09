
using PolyRibo.Application.Interface.Infrastructure.Repository;
using PolyRibo.Domain.Entities;
using PolyRibo.Infrastructure.Persistence;

namespace PolyRibo.Infrastructure.Repository;

public class FrameworkTypeRepository : BaseRepository<FrameworkType> , IFrameworkTypeRepository
{
    public FrameworkTypeRepository(PolyRiboDbContext context) : base(context) { }
}
