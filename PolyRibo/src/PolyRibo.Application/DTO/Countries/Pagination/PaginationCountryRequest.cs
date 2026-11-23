namespace PolyRibo.Application.DTO.Countries.Pagination;

public record class PaginationCountryRequest
{
    public int Page {get; set;} = 1;
    public int PageSize {get; set;} = 10;
}
