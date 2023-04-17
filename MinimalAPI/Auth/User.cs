namespace MinimalAPI.Auth;

public record UserModel
{
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required]
    public string Passford { get; set; } = string.Empty;
}

public record UserDTO(string UserName, string Password);
