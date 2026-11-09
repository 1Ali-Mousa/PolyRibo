using Microsoft.EntityFrameworkCore;
using PolyRibo.Application.Interface.Infrastructure.Repository;
using PolyRibo.Domain.Entities;
using PolyRibo.Infrastructure.Persistence;

namespace PolyRibo.Infrastructure.Repository;

public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly PolyRiboDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(PolyRiboDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(int page, int rows)
    {
        return await _dbSet
            .AsNoTracking()
            .OrderBy(e => e.Id).Skip((page - 1) * rows).Take(rows)
            .ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task<TEntity?> UpdateAsync(TEntity entity)
    {
        var existingEntity = await _dbSet.FindAsync(entity.Id);

        if (existingEntity is null)
            return null;

        _context.Entry(existingEntity)
            .CurrentValues.SetValues(entity);

        return existingEntity;
    }

    public Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _dbSet
            .AsNoTracking()
            .AnyAsync(e => e.Id == id);
    }
}