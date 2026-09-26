using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchArchive.Server.DTOs.RequestDTOs;
using WatchArchive.Server.DTOs.ResponseDTOs;
using WatchArchive.Server.Models;
using WatchArchive.Server.Repositories.UserRepo;
using WatchArchive.Server.Repositories.UserSessionRepo;
using WatchArchive.Server.Services;

namespace WatchArchive.Server.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(
    IUserRepository userRepository,
    IJwtService jwtService,
    IRefreshTokenService refreshTokenService,
    IAppConfiguration appConfiguration,
    IUserSessionRepository userSessionRepository,
    IMapper mapper
) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        User? user = await userRepository.AuthenticateUser(req.Username, req.Password);

        if (user == null)
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }

        // Generate new tokens
        string accessToken = jwtService.GenerateAccessToken(user);
        string refreshToken = refreshTokenService.GenerateToken();
        string refreshTokenHash = refreshTokenService.HashToken(refreshToken);

        DateTime accessTokenExpiresAt = DateTime.UtcNow.AddMinutes(
            appConfiguration.AccessTokenLifetimeMinutes
        );
        DateTime refreshTokenExpiresAt = DateTime.UtcNow.AddDays(
            appConfiguration.RefreshTokenLifetimeDays
        );

        // Insert new session
        UserSession newSession = new()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            User = user,
            RefreshTokenHash = refreshTokenHash,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = refreshTokenExpiresAt,
        };

        await userSessionRepository.Create(newSession);

        // Add tokens to response cookies
        AppendTokenToResponseCookie(
            accessToken,
            accessTokenExpiresAt,
            refreshToken,
            refreshTokenExpiresAt
        );

        return Ok(new TokenResponse() { AccessToken = accessToken, RefreshToken = refreshToken });
    }

    [Authorize]
    [HttpGet("user")]
    public async Task<IActionResult> GetUser()
    {
        Guid? userId = jwtService.GetUserIdFromClaims([.. HttpContext.User.Claims]);

        if (userId == null)
        {
            return Unauthorized();
        }

        User? foundUser = await userRepository.GetById((Guid)userId);

        if (foundUser == null)
        {
            return NotFound();
        }

        UserResponse res = mapper.Map<UserResponse>(foundUser);

        return Ok(res);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        if (!Request.Cookies.TryGetValue("refresh_token", out string? oldRefreshToken))
        {
            return Unauthorized();
        }

        string oldRefreshTokenHash = refreshTokenService.HashToken(oldRefreshToken);
        UserSession? foundSession = await userSessionRepository.GetByRefreshTokenHash(
            oldRefreshTokenHash
        );

        if (
            foundSession == null
            || foundSession.RevokedAt.HasValue
            || foundSession.ExpiresAt <= DateTime.UtcNow
        )
        {
            return Unauthorized();
        }

        User user = foundSession.User;

        // Generate new tokens
        string newAccessToken = jwtService.GenerateAccessToken(user);
        string newRefreshToken = refreshTokenService.GenerateToken();
        string newRefreshTokenHash = refreshTokenService.HashToken(newRefreshToken);

        DateTime accessTokenExpiresAt = DateTime.UtcNow.AddMinutes(
            appConfiguration.AccessTokenLifetimeMinutes
        );
        DateTime refreshTokenExpiresAt = DateTime.UtcNow.AddDays(
            appConfiguration.RefreshTokenLifetimeDays
        );

        await userSessionRepository.UpdateRefreshTokenHash(
            foundSession.Id,
            newRefreshTokenHash,
            DateTime.UtcNow.AddDays(appConfiguration.RefreshTokenLifetimeDays)
        );

        // Add tokens to response cookies
        AppendTokenToResponseCookie(
            newAccessToken,
            accessTokenExpiresAt,
            newRefreshToken,
            refreshTokenExpiresAt
        );

        return Ok(
            new TokenResponse() { AccessToken = newAccessToken, RefreshToken = newRefreshToken }
        );
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        if (Request.Cookies.TryGetValue("refresh_token", out string? refreshToken))
        {
            string refreshTokenHash = refreshTokenService.HashToken(refreshToken);
            UserSession? foundUserSession = await userSessionRepository.GetByRefreshTokenHash(
                refreshTokenHash
            );

            if (foundUserSession != null && !foundUserSession.RevokedAt.HasValue)
            {
                await userSessionRepository.RevokeRefreshToken(
                    foundUserSession.Id,
                    DateTime.UtcNow
                );
            }
        }

        Response.Cookies.Delete(
            "access_token",
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Path = "/",
            }
        );

        Response.Cookies.Delete(
            "refresh_token",
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Path = "/api/auth",
            }
        );

        return NoContent();
    }

    private void AppendTokenToResponseCookie(
        string accessToken,
        DateTime accessTokenExpiresAt,
        string refreshToken,
        DateTime refreshTokenExpiresAt
    )
    {
        Response.Cookies.Append(
            "access_token",
            accessToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = Request.IsHttps,
                SameSite = SameSiteMode.Lax,
                Expires = accessTokenExpiresAt,
            }
        );

        Response.Cookies.Append(
            "refresh_token",
            refreshToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = Request.IsHttps,
                SameSite = SameSiteMode.Lax,
                Expires = refreshTokenExpiresAt,
            }
        );
    }
}
