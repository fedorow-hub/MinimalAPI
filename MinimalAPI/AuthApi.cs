using MinimalAPI.APIs;
using MinimalAPI.Auth;

namespace MinimalAPI;

public class AuthApi : IApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("/login", [AllowAnonymous] async (HttpContext context, ITokenService tokenservice, IUserRepository userReposirory) =>
        {
            UserModel userModel = new()
            {
                UserName = context.Request.Query["username"],
                Passford = context.Request.Query["password"]
            };
            var userDTO = userReposirory.GetUser(userModel);
            if (userDTO == null) return Results.Unauthorized();
            var token = tokenservice.BuildToken(app.Configuration["Jwt:Key"],
                app.Configuration["Jwt:Issuer"], userDTO);
            return Results.Ok(token);
        });
    }
}
