namespace PolyRibo.Application.DTO.Countries.Create;

public record class CreateCountryRequest
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
}
