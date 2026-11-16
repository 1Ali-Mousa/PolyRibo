using System;
using PolyRibo.Domain.Entities;

namespace PolyRibo.Application.Interface.Infrastructure.Repository;

public interface IUserRepository : IBaseRepository<User>
{
    public Task<User?> GetByUserNameAsync(string UserName);
    public Task<User?> GetByEmailAsync(string Email);
}
