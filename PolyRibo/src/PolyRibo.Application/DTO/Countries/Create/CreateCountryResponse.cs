namespace PolyRibo.Application.DTO.Countries.Create;

public record class CreateCountryResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
}
