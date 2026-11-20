namespace PolyRibo.Application.DTO.Auth.Register;

public record class RegisterResponse
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int CountryId { get; set; }
}
