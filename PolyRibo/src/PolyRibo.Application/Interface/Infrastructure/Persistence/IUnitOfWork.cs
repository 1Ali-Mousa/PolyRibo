namespace PolyRibo.Application.Interface.Infrastructure.Persistence;

public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync();
}
