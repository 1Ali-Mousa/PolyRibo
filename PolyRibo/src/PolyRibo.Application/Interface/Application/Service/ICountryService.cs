using PolyRibo.Application.DTO.Countries;
using PolyRibo.Application.DTO.Countries.Create;
using PolyRibo.Application.DTO.Countries.Pagination;
using PolyRibo.Application.DTO.Countries.Update;

namespace PolyRibo.Application.Interface.Application.Service;

public interface ICountryService
{
    Task<CreateCountryResponse> CreateAsync(CreateCountryRequest request);

    Task<CountryResponse> GetByIdAsync(int id);

    Task<IEnumerable<CountryResponse>> GetAllAsync(PaginationCountryRequest request);

    Task<UpdateCountryResponse> UpdateAsync(int id, UpdateCountryRequest request);

    Task<bool> DeleteAsync(int id);
}
