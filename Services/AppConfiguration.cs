namespace WatchArchive.Server.Services;

public class AppConfiguration : IAppConfiguration
{
    public string JwtIssuer { get; }

    public string JwtAudience { get; }

    public string JwtSigningKey { get; }

    public int AccessTokenLifetimeMinutes { get; }

    public int RefreshTokenLifetimeDays { get; }

    public AppConfiguration(IConfiguration configuration)
    {
        IConfigurationSection jwtSection = configuration.GetSection("Jwt");

        // Assign configuration values
        JwtIssuer = GetRequiredSetting<string>(jwtSection, "Issuer");
        JwtAudience = GetRequiredSetting<string>(jwtSection, "Audience");
        JwtSigningKey = GetRequiredSetting<string>(jwtSection, "SigningKey");
        AccessTokenLifetimeMinutes = GetRequiredSetting<int>(
            jwtSection,
            "AccessTokenLifetimeMinutes"
        );
        RefreshTokenLifetimeDays = GetRequiredSetting<int>(jwtSection, "RefreshTokenLifetimeDays");
    }

    private static T GetRequiredSetting<T>(IConfigurationSection section, string key)
    {
        return section.GetValue<T>(key)
            ?? throw new InvalidOperationException($"JWT {key} is not configured.");
        ;
    }
}
