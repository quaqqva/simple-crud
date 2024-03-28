namespace Backend.Application.Dtos;

public record JwtDto
{
    public required string AccessToken { get; set; }

    public required string RefreshToken { get; set; }
}