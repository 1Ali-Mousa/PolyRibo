using System;
using PolyRibo.Domain.Entities;

namespace PolyRibo.Application.Interface.Infrastructure;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(int id);

    Task<IEnumerable<TEntity>> GetAllAsync(int page, int rows);

    Task AddAsync(TEntity entity);

    Task<TEntity?> UpdateAsync(TEntity entity);

    Task DeleteAsync(TEntity entity);

    Task<bool> ExistsAsync(int id);
}