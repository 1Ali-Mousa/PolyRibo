using PolyRibo.Application.Interface.Infrastructure.Repository;
using PolyRibo.Domain.Entities;
using PolyRibo.Infrastructure.Persistence;

namespace PolyRibo.Infrastructure.Repository;

public class ProjectRepository : BaseRepository<Project> , IProjectRepository
{
    public ProjectRepository(PolyRiboDbContext context) : base(context) { }
}
