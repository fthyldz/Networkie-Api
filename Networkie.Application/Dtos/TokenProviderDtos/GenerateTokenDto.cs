namespace Networkie.Application.Dtos.TokenProviderDtos;

public record GenerateTokenDto(Guid UserId, string Email, string[] Roles, bool IsEmailVerified, bool IsProfileCompleted);