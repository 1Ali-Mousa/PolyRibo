using PolyRibo.Application.Interface.Infrastructure.Repository;
using PolyRibo.Domain.Entities;
using PolyRibo.Infrastructure.Persistence;

namespace PolyRibo.Infrastructure.Repository;

public class UserRepository : BaseRepository<User> , IUserRepository
{

    public UserRepository(PolyRiboDbContext context) : base(context)
    { }

    public async Task<User?> GetByUserNameAsync(string UserName)
    {
        return await _context.Users.FindAsync(UserName);
    }
    
    public async Task<User?> GetByEmailAsync(string Email)
    {
        return await _context.Users.FindAsync(Email);
    }
}
