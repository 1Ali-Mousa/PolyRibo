using PolyRibo.Application.Interface.Infrastructure.Repository;
using PolyRibo.Domain.Entities;
using PolyRibo.Infrastructure.Persistence;

namespace PolyRibo.Infrastructure.Repository;

public class UserRepository : BaseRepository<User> , IUserRepository
{
    public UserRepository(PolyRiboDbContext context) : base(context) { }
}
