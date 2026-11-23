using PolyRibo.Domain.Entities;

namespace PolyRibo.Application.Interface.Infrastructure.Repository;

public interface ICountryRepository : IBaseRepository<Country>
{
    Task<Country?> GetByCodeAsync(string code);

    Task<bool> HasUsersAsync(int countryId);
}
