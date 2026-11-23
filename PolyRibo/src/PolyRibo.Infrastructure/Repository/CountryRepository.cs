using Microsoft.EntityFrameworkCore;
using PolyRibo.Application.Interface.Infrastructure.Repository;
using PolyRibo.Domain.Entities;
using PolyRibo.Infrastructure.Persistence;

namespace PolyRibo.Infrastructure.Repository;

public class CountryRepository : BaseRepository<Country>, ICountryRepository
{
    public CountryRepository(PolyRiboDbContext context) : base(context) { }

    public async Task<Country?> GetByCodeAsync(string code)
    {
        return await _context.Countries
            .AsNoTracking()
            .FirstOrDefaultAsync(country => country.Code == code);
    }

    public async Task<bool> HasUsersAsync(int countryId)
    {
        return await _context.Users
            .AsNoTracking()
            .AnyAsync(user => user.CountryId == countryId);
    }
}
