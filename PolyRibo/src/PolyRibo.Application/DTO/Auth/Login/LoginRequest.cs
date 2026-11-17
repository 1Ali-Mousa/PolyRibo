namespace PolyRibo.Application.DTO.Auth.Login;

public record class LoginRequest
{
    public string UserName {get;set;} = null!;
    public string PasswordHashed {get; set;} = null!;
}
