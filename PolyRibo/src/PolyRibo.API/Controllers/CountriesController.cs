using Microsoft.AspNetCore.Mvc;
using PolyRibo.API.Response;
using PolyRibo.Application.DTO.Countries;
using PolyRibo.Application.DTO.Countries.Create;
using PolyRibo.Application.DTO.Countries.Pagination;
using PolyRibo.Application.DTO.Countries.Update;
using PolyRibo.Application.Interface.Application.Service;

namespace PolyRibo.API.Controllers;

[Route("api/Countries")]
[ApiController]
public class CountriesController : BaseController
{
    private readonly ICountryService _countryService;

    public CountriesController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpPost(Name = "CreateCountryAsync")]
    public async Task<ActionResult<APIResponse<CreateCountryResponse>>> CreateCountryAsync([FromBody] CreateCountryRequest request)
    {
        var response = await _countryService.CreateAsync(request);

        return CreateResponse(response, StatusCodes.Status201Created, "Created Successfully!");
    }

    [HttpGet("{id:int}", Name = "GetCountryAsync")]
    public async Task<ActionResult<APIResponse<CountryResponse>>> GetCountryAsync([FromRoute] int id)
    {
        var response = await _countryService.GetByIdAsync(id);

        return CreateResponse(response, StatusCodes.Status200OK);
    }

    [HttpGet(Name = "GetAllCountriesAsync")]
    public async Task<ActionResult<APIResponse<IEnumerable<CountryResponse>>>> GetAllCountriesAsync([FromQuery] PaginationCountryRequest pagination)
    {
        var response = await _countryService.GetAllAsync(pagination);

        return CreateResponse(response, StatusCodes.Status200OK);
    }

    [HttpPut("{id:int}", Name = "UpdateCountryAsync")]
    public async Task<ActionResult<APIResponse<UpdateCountryResponse>>> UpdateCountryAsync([FromRoute] int id, [FromBody] UpdateCountryRequest request)
    {
        var response = await _countryService.UpdateAsync(id, request);

        return CreateResponse(response, StatusCodes.Status200OK, "Updated Successfully!");
    }

    [HttpDelete("{id:int}", Name = "DeleteCountryAsync")]
    public async Task<ActionResult<APIResponse<bool>>> DeleteCountryAsync([FromRoute] int id)
    {
        var response = await _countryService.DeleteAsync(id);

        return CreateResponse(response, StatusCodes.Status200OK, "Deleted Successfully!");
    }
}
