using PolyRibo.Domain.Entities;

namespace PolyRibo.Application.Interface.Infrastructure.Repository;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByUserNameAsync(string userName);

    Task<User?> GetByEmailAsync(string email);
}
