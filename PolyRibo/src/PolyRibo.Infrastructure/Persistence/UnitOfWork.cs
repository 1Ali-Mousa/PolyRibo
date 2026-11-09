using PolyRibo.Application.Interface.Infrastructure;

namespace PolyRibo.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly PolyRiboDbContext _context;

    public UnitOfWork(PolyRiboDbContext context)
    {
        _context = context;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
