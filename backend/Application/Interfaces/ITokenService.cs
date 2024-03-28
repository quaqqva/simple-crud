using System.Security.Claims;

namespace Backend.Application.Interfaces;

public interface ITokenService
{
    public Task<string> RefreshToken(string refreshToken);

    public string GenerateAccessToken(IEnumerable<Claim> claims);

    public string GenerateRefreshToken();

    public Task<bool> ValidateToken(string token);
}