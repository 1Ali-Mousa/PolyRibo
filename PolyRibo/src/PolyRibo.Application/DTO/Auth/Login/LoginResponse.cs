namespace PolyRibo.Application.DTO.Auth.Login;

public record class LoginResponse
{
    public string UserName {get;set;} = null!;
    public string Email {get; set;} = null!;
    public int CountryId {get; set;} 
}
