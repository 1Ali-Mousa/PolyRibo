namespace PolyRibo.Application.DTO.Auth.Register;

public record class RegisterRequest
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int CountryId { get; set; }
}
