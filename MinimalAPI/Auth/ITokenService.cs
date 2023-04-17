namespace MinimalAPI.Auth;

public interface ITokenService
{
    string BuildToken(string key, string issuer, UserDTO user);
}
