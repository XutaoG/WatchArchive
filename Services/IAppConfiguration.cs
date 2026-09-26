namespace WatchArchive.Server.Services;

public interface IAppConfiguration
{
    string JwtIssuer { get; }

    string JwtAudience { get; }

    string JwtSigningKey { get; }

    int AccessTokenLifetimeMinutes { get; }

    int RefreshTokenLifetimeDays { get; }
}
