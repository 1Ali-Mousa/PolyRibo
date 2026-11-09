using PolyRibo.Application.Interface.Infrastructure.Repository;
using PolyRibo.Domain.Entities;
using PolyRibo.Infrastructure.Persistence;

namespace PolyRibo.Infrastructure.Repository;

public class LanguageRepository : BaseRepository<Language> , ILanguageRepository
{
    public LanguageRepository(PolyRiboDbContext context) : base(context) { }
}
