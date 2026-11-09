using System;

namespace PolyRibo.Application.Interface.Infrastructure.Presistence;

public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync();
}