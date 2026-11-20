using Microsoft.EntityFrameworkCore;
using PolyRibo.Application.Interface.Infrastructure.Repository;
using PolyRibo.Domain.Entities;
using PolyRibo.Infrastructure.Persistence;

namespace PolyRibo.Infrastructure.Repository;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(PolyRiboDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.UserName == userName);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email == email);
    }
}
