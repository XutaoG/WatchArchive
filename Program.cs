using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WatchArchive.Server.Data;
using WatchArchive.Server.Mappings;
using WatchArchive.Server.Models;
using WatchArchive.Server.Repositories.UserRepo;
using WatchArchive.Server.Repositories.UserSessionRepo;
using WatchArchive.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

string connectionString =
    builder.Configuration.GetConnectionString("WatchArchiveConnectionString")
    ?? throw new InvalidOperationException(
        "Connection string 'WatchArchiveConnectionString' was not found."
    );

builder.Services.AddDbContext<WatchArchiveDbContext>(options =>
    options.UseNpgsql(connectionString)
);

// JWT Authentication
builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;

        IConfigurationSection jwtSection = builder.Configuration.GetSection("Jwt");
        string signingKey =
            jwtSection["SigningKey"]
            ?? throw new InvalidOperationException("JWT SigningKey is not configured.");

        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSection["Issuer"],

            ValidateAudience = true,
            ValidAudience = jwtSection["Audience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["access_token"];
                return Task.CompletedTask;
            },
        };
    });

// JWT Authorization
builder.Services.AddAuthorization();

// Add Services
builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperProfile));
builder.Services.AddScoped<PasswordHasher<User>>();
builder.Services.AddSingleton<IAppConfiguration, AppConfiguration>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPasswordHashService, PasswordHashService>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();

// Add Repositories
builder.Services.AddScoped<IUserRepository, SqlUserRepository>();
builder.Services.AddScoped<IUserSessionRepository, SqlUserSessionRepository>();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapControllers();

app.Run();
