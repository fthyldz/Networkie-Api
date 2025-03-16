using Networkie.Application.Dtos.TokenProviderDtos;

namespace Networkie.Application.Abstractions.Providers;

public interface ITokenProvider
{
    TokenDto GenerateTokenAsync(GenerateTokenDto generateTokenRequestDto, CancellationToken cancellationToken = default);
}