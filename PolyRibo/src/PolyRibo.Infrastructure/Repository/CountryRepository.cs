using PolyRibo.Application.Interface.Infrastructure.Repository;
using PolyRibo.Domain.Entities;
using PolyRibo.Infrastructure.Persistence;

namespace PolyRibo.Infrastructure.Repository;

public class CountryRepository : BaseRepository<Country>, ICountryRepository
{
    public CountryRepository(PolyRiboDbContext context) : base(context) { }
}
