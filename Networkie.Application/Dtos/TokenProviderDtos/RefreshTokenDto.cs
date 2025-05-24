namespace Networkie.Application.Dtos.TokenProviderDtos;

public record RefreshTokenDto(string RefreshToken, DateTime CreatedAt, DateTime ExpiresAt);