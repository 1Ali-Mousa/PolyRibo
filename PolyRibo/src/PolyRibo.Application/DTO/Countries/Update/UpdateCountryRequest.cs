namespace PolyRibo.Application.DTO.Countries.Update;

public record class UpdateCountryRequest
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
}
