using PolyRibo.Application.DTO.Countries;
using PolyRibo.Application.DTO.Countries.Create;
using PolyRibo.Application.DTO.Countries.Pagination;
using PolyRibo.Application.DTO.Countries.Update;
using PolyRibo.Application.Interface.Application.Service;
using PolyRibo.Domain.Entities;

namespace PolyRibo.Application.Service.Countries;

public partial class CountryService : ICountryService
{
    public async Task<CreateCountryResponse> CreateAsync(CreateCountryRequest request)
    {
        request.Code = NormalizeCode(request.Code);

        var validationResult = await _createCountryValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
            throw new ArgumentException($"Invalid request: {errors}");
        }

        var name = request.Name.Trim();
        var code = request.Code;

        if (await _countryRepository.GetByCodeAsync(code) is not null)
        {
            throw new InvalidOperationException("Country code is already registered.");
        }

        var country = new Country
        {
            Name = name,
            Code = code
        };

        await _countryRepository.AddAsync(country);

        if (!await _unitOfWork.SaveChangesAsync())
        {
            throw new InvalidOperationException("Country could not be created.");
        }

        return new CreateCountryResponse
        {
            Id = country.Id,
            Name = country.Name,
            Code = country.Code
        };
    }

    public async Task<CountryResponse> GetByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Id must be greater than 0.");
        }

        var country = await _countryRepository.GetByIdAsync(id);

        if (country is null)
        {
            throw new KeyNotFoundException("Country does not exist.");
        }

        return new CountryResponse
        {
            Id = country.Id,
            Name = country.Name,
            Code = country.Code
        };
    }

    public async Task<IEnumerable<CountryResponse>> GetAllAsync(PaginationCountryRequest request)
    {
        var validationResult = await _paginationCountryValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
            throw new ArgumentException($"Invalid request: {errors}");
        }

        var countries = await _countryRepository.GetAllAsync(request.Page, request.PageSize);

        return countries.Select(country => new CountryResponse
        {
            Id = country.Id,
            Name = country.Name,
            Code = country.Code
        }).ToList();
    }

    public async Task<UpdateCountryResponse> UpdateAsync(int id, UpdateCountryRequest request)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Id must be greater than 0.");
        }

        request.Code = NormalizeCode(request.Code);

        var validationResult = await _updateCountryValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
            throw new ArgumentException($"Invalid request: {errors}");
        }

        if (await _countryRepository.GetByIdAsync(id) is null)
        {
            throw new KeyNotFoundException("Country does not exist.");
        }

        var name = request.Name.Trim();
        var code = request.Code;

        var countryWithCode = await _countryRepository.GetByCodeAsync(code);

        if (countryWithCode is not null && countryWithCode.Id != id)
        {
            throw new InvalidOperationException("Country code is already registered.");
        }

        var country = await _countryRepository.UpdateAsync(new Country
        {
            Id = id,
            Name = name,
            Code = code
        });

        if (country is null)
        {
            throw new KeyNotFoundException("Country does not exist.");
        }

        if (!await _unitOfWork.SaveChangesAsync())
        {
            throw new InvalidOperationException("Country could not be updated.");
        }

        return new UpdateCountryResponse
        {
            Id = country.Id,
            Name = country.Name,
            Code = country.Code
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Id must be greater than 0.");
        }

        var country = await _countryRepository.GetByIdAsync(id);

        if (country is null)
        {
            throw new KeyNotFoundException("Country does not exist.");
        }

        if (await _countryRepository.HasUsersAsync(id))
        {
            throw new InvalidOperationException("Country cannot be deleted because it has registered users.");
        }

        await _countryRepository.DeleteAsync(country);

        if (!await _unitOfWork.SaveChangesAsync())
        {
            throw new InvalidOperationException("Country could not be deleted.");
        }

        return true;
    }

    private static string NormalizeCode(string code)
    {
        return code?.Trim() ?? string.Empty;
    }
}
