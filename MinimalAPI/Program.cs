using MinimalAPI;
using MinimalAPI.APIs;
using MinimalAPI.Auth;
using MinimalAPI.Data;

var builder = WebApplication.CreateBuilder(args);

RegisterServices(builder.Services);

var app = builder.Build();

Configure(app);

var apis = app.Services.GetServices<IApi>();
foreach (var api in apis)
{
    if(api ==  null) throw new InvalidProgramException("Api not found");
    api.Register(app);
}

app.Run();

void RegisterServices(IServiceCollection services)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddDbContext<HotelDB>(options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
    });

    services.AddScoped<IHotelRepository, HotelRepository>();
    services.AddSingleton<IUserRepository>(new UserRepository());
    services.AddSingleton<ITokenService>(new TokenService());
    services.AddAuthorization();
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });
    services.AddTransient<IApi, HotelApi>();
    services.AddTransient<IApi, AuthApi>();
}

void Configure(WebApplication app)
{
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseHttpsRedirection();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<HotelDB>();
        db.Database.EnsureCreated();
    }
}

