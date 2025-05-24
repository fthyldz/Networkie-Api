namespace Networkie.Application.Dtos.TokenProviderDtos;

public record TokenDto(string Token, DateTime CreatedAt, DateTime ExpiresAt);