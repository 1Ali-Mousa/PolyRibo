namespace PolyRibo.Application.DTO.Countries.Update;

public record class UpdateCountryResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
}
