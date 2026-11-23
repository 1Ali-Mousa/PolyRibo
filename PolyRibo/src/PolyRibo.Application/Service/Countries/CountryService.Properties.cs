using FluentValidation;
using PolyRibo.Application.DTO.Countries.Create;
using PolyRibo.Application.DTO.Countries.Pagination;
using PolyRibo.Application.DTO.Countries.Update;
using PolyRibo.Application.Interface.Infrastructure.Persistence;
using PolyRibo.Application.Interface.Infrastructure.Repository;

namespace PolyRibo.Application.Service.Countries;

public partial class CountryService
{
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateCountryRequest> _createCountryValidator;
    private readonly IValidator<PaginationCountryRequest> _paginationCountryValidator;
    private readonly IValidator<UpdateCountryRequest> _updateCountryValidator;

    public CountryService(
        ICountryRepository countryRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateCountryRequest> createCountryValidator,
        IValidator<UpdateCountryRequest> updateCountryValidator,
        IValidator<PaginationCountryRequest> paginationCountryValidator)
    {
        _countryRepository = countryRepository;
        _unitOfWork = unitOfWork;
        _createCountryValidator = createCountryValidator;
        _updateCountryValidator = updateCountryValidator;
        _paginationCountryValidator = paginationCountryValidator;
    }
}
