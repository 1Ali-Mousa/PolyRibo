using System;

namespace PolyRibo.Application.Interface.Infrastructure;

public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync();
}