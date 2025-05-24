using Networkie.Application.Dtos.TokenProviderDtos;

namespace Networkie.Application.Abstractions.Providers;

public interface ITokenProvider
{
    TokenDto GenerateToken(GenerateTokenDto generateTokenRequestDto);
    RefreshTokenDto GenerateRefreshToken();
}